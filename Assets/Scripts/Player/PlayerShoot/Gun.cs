using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip shootSound;  // Assign this in the Unity Editor
    [SerializeField] private AudioSource audioSource;  // Assign this in the Unity Editor
    
    [Header("Aiming")]
    [SerializeField] private Vector3 aimPositionOffset;  // Position offset when aiming
    [SerializeField] private float aimFOV = 40f;  // Field of view when aiming
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalFOV;
    private bool isAiming = false;

    float timeSinceLastShot;
    private bool isActive = false;

    private void Start()
    {
        gunData.currentAmmo = 0;
        gunData.reloading = false;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation; 
        originalFOV = cam.GetComponent<Camera>().fieldOfView;
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (!gameObject.activeInHierarchy) return;

        HandleAiming();
    }

    public void StartReload()
    {
        if (gameObject.activeInHierarchy && !gunData.reloading && gunData.currentAmmo < gunData.magSize)
        {
            Inventory inventory = FindObjectOfType<Inventory>(); // Find the Inventory instance
            if (inventory != null)
            {
                gunData.ReloadFromInventory(inventory);
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot >= 1f / gunData.fireRate;

    public void Shoot()
    {
        if (gunData.currentAmmo > 0 && CanShoot())
        {
            if(isActive)
            {
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        InstantiateBullet(hitInfo.point);
                        damagable.Damage(gunData.damage);
                    }
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void InstantiateBullet(Vector3 targetPosition)
    {
        Vector3 startPosition = muzzle.position;  
        Vector3 direction = (targetPosition - startPosition).normalized;

        // Calculate correct orientation taking into account the prefab's initial rotation
        Quaternion bulletRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);  // Adjust these Euler angles to suit your prefab's needs

        GameObject bullet = Instantiate(bulletPrefab, startPosition, bulletRotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(direction * 6000);  // You may consider using ForceMode.Impulse here
        }

        Destroy(bullet, 1.0f); // Adjust this time as necessary
    }


    private void OnGunShot()
    {
        // Play the gunshot sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        // Optionally, play animations or other effects
    }

    public void SetActive(bool active)
    {
        isActive = active;
        if (!isActive) timeSinceLastShot = float.MaxValue;  // Reset timeSinceLastShot to prevent immediate shooting
    }

    private void HandleAiming()
    {
        if (Input.GetMouseButton(1))  // Right mouse button
        {
            if (!isAiming)
            {
                StartAiming();
            }
        }
        else
        {
            if (isAiming)
            {
                StopAiming();
            }
        }
    }

    private void StartAiming()
    {
        transform.localPosition = aimPositionOffset;  // Set to a new offset that centers the gun
        transform.localRotation = Quaternion.Euler(0, 0, 0);  // Align rotation if needed
        cam.GetComponent<Camera>().fieldOfView = aimFOV;
        isAiming = true;
    }

    private void StopAiming()
    {
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;  // Restore original rotation
        cam.GetComponent<Camera>().fieldOfView = originalFOV;
        isAiming = false;
    }

}
