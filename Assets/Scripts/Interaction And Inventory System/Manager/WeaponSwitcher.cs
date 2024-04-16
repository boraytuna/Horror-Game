// // using UnityEngine;

// // public class WeaponSwitcher : MonoBehaviour {

// //     [Header("References")]
// //     [SerializeField] private Transform[] weapons;

// //     [Header("Keys")]
// //     [SerializeField] private KeyCode[] keys;

// //     [Header("Settings")]
// //     [SerializeField] private float switchTime;

// //     private int selectedWeapon;
// //     private float timeSinceLastSwitch;

// //     private void Start() {
// //         SetWeapons();
// //         Select(selectedWeapon);

// //         timeSinceLastSwitch = 0f;
// //     }

// //     private void SetWeapons() {
// //         weapons = new Transform[transform.childCount];

// //         for (int i = 0; i < transform.childCount; i++)
// //             weapons[i] = transform.GetChild(i);

// //         if (keys == null) keys = new KeyCode[weapons.Length];
// //     }

// //     private void Update() {
// //         int previousSelectedWeapon = selectedWeapon;

// //         for (int i = 0; i < keys.Length; i++)
// //             if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
// //                 selectedWeapon = i;

// //         if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

// //         timeSinceLastSwitch += Time.deltaTime;
// //     }

// //     private void Select(int weaponIndex) {
// //         for (int i = 0; i < weapons.Length; i++)
// //             weapons[i].gameObject.SetActive(i == weaponIndex);

// //         timeSinceLastSwitch = 0f;

// //         OnWeaponSelected();
// //     }

// //     private void OnWeaponSelected() {  }
// // }
//  using UnityEngine;
// public class WeaponSwitcher : MonoBehaviour {

//     [Header("References")]
//     [SerializeField] private Transform[] weapons;

//     [Header("Keys")]
//     [SerializeField] private KeyCode[] keys;

//     [Header("Settings")]
//     [SerializeField] private float switchTime;

//     private int selectedWeapon;
//     private float timeSinceLastSwitch;

//     private void Start() {
//         SetWeapons();
//         Select(selectedWeapon);
//         timeSinceLastSwitch = 0f;
//     }

//     private void SetWeapons() {
//         weapons = new Transform[transform.childCount];

//         for (int i = 0; i < transform.childCount; i++)
//             weapons[i] = transform.GetChild(i);

//         if (keys == null || keys.Length < weapons.Length)
//             keys = new KeyCode[weapons.Length];  // Ensure there is a key assigned for each weapon
//     }

//     private void Update() {
//         int previousSelectedWeapon = selectedWeapon;

//         for (int i = 0; i < keys.Length; i++)
//             if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
//                 selectedWeapon = i;

//         if (previousSelectedWeapon != selectedWeapon)
//             Select(selectedWeapon);

//         timeSinceLastSwitch += Time.deltaTime;
//     }

//     private void Select(int weaponIndex) {
//         for (int i = 0; i < weapons.Length; i++)
//             weapons[i].gameObject.SetActive(i == weaponIndex);

//         timeSinceLastSwitch = 0f;
//         OnWeaponSelected();
//     }

//     private void OnWeaponSelected() {
//         // Optional: Add logic that happens when a new weapon is selected
//         Debug.Log("Weapon switched to: " + weapons[selectedWeapon].name);
//     }
// }
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameObject[] weapons;  // Use GameObject to easily enable/disable components

    [Header("Keys")]
    [SerializeField] private KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start() {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons() {
        weapons = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i).gameObject;

        if (keys == null || keys.Length < weapons.Length)
            keys = new KeyCode[weapons.Length];  // Ensure there is a key assigned for each weapon
    }

    private void Update() {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < keys.Length; i++)
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime)
                selectedWeapon = i;

        if (previousSelectedWeapon != selectedWeapon)
            Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex) {
        for (int i = 0; i < weapons.Length; i++) {
            if (i == weaponIndex) {
                weapons[i].SetActive(true);
                OnWeaponSelected(i);
            } else {
                weapons[i].SetActive(false);
                OnWeaponDeactivated(i);
            }
        }
        timeSinceLastSwitch = 0f;
    }

    private void OnWeaponSelected(int index) {
        Debug.Log("Weapon switched to: " + weapons[index].name);
    }

    private void OnWeaponDeactivated(int index) {
        // Call deactivation/reset methods specific to each weapon
        if (index == 0) {  // Assuming 0 is the gun
            weapons[index].GetComponent<Gun>().DeactivateGun();
        }
        // Add similar calls for other weapons if necessary
    }
}
