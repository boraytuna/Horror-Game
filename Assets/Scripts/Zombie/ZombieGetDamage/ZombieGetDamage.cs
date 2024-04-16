using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGetDamage : MonoBehaviour, IDamagable
{
    [Header("Health")]
    public float health;
    public float maxHealth;

    private void Start()
    {
        health = maxHealth;
    }
    public void Damage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            zombieDie();
        }
    }

    public void zombieDie()
    {
        Debug.Log("Zombie Killed");

        Destroy(gameObject);
    }
}
