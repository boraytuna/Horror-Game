using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGetDamageByMelee : MonoBehaviour
{
    private void onTriggerEnter(Collider other)
    {
        if(other.tag == "MeleeAttack")
            Debug.Log("It touched");
            Destroy(gameObject);
    }
}
