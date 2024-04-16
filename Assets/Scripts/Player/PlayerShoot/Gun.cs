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

    float timeSinceLastShot;

    private void Start()
    {
        gunData.currentAmmo = 0;
        gunData.reloading = false;
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (!gameObject.activeInHierarchy) return;
    }

    public void StartReload()
    {
        if (!gunData.reloading && gunData.currentAmmo < gunData.magSize)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
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
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
            {
                IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.Damage(gunData.damage);
                    InstantiateBullet(hitInfo.point);
                }
            }

            gunData.currentAmmo--;
            timeSinceLastShot = 0;
            OnGunShot();
        }
    }

    private void InstantiateBullet(Vector3 targetPosition)
    {
        var bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(targetPosition - muzzle.position));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = (targetPosition - muzzle.position).normalized * 3000; // Adjust the magnitude as necessary
            rb.AddForce(force);
        }
        Destroy(bullet, 2.0f); // Destroy bullet after some time to avoid clutter
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

    public void DeactivateGun()
    {
        // Ensure no shooting happens when this method is called
        timeSinceLastShot = float.MaxValue;  // Prevent shooting immediately after switching
    }

}
