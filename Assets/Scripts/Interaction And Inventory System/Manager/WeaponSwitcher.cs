using UnityEngine;
public class WeaponSwitcher : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject[] weapons;  // Use GameObject to easily enable/disable components

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private KeyCode reloadKey = KeyCode.R;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start() {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0f;
    }

    // private void Update() {
    //     int previousSelectedWeapon = selectedWeapon;

    //     for (int i = 0; i < keys.Length; i++)
    //         if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
    //             selectedWeapon = i;

    //     if (previousSelectedWeapon != selectedWeapon)
    //         Select(selectedWeapon);

    //     HandleReloadInput();

    //     timeSinceLastSwitch += Time.deltaTime;
    // }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
                selectedWeapon = i;

        if (previousSelectedWeapon != selectedWeapon)
            Select(selectedWeapon);

        HandleReloadInput();
        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            bool isActive = (i == weaponIndex);
            weapons[i].SetActive(isActive);

            if (weapons[i].GetComponent<Gun>() != null)
                weapons[i].GetComponent<Gun>().SetActive(isActive);
            else if (weapons[i].GetComponent<Flashlight>() != null && isActive)
                weapons[i].GetComponent<Flashlight>().Recharge(600);  // Example recharge call
        }
        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons() {
        weapons = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i).gameObject;

        if (keys == null || keys.Length < weapons.Length)
            keys = new KeyCode[weapons.Length];  // Ensure there is a key assigned for each weapon
    }  

    private void HandleReloadInput() {
        if (Input.GetKeyDown(reloadKey)) {
            if (weapons[selectedWeapon].GetComponent<Gun>() != null) {
                weapons[selectedWeapon].GetComponent<Gun>().StartReload();
            } else if (weapons[selectedWeapon].GetComponent<Flashlight>() != null) {
                // Optionally, handle "recharging" the flashlight or similar action
                weapons[selectedWeapon].GetComponent<Flashlight>().Recharge(600);  // Example recharge call
            }
        }
    }

}
