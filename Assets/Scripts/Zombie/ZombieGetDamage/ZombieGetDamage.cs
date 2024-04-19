// using UnityEngine;

// public class ZombieGetDamage : MonoBehaviour, IDamagable
// {
//     [Header("Health")]
//     public float health;
//     public float maxHealth;

//     private void Start()
//     {
//         GameManager.Instance.RegisterZombie();
//         health = maxHealth;
//     }
//     public void Damage(float damage)
//     {
//         health -= damage;

//         if(health <= 0)
//         {
//             zombieDie();
//         }
//     }

//     public void zombieDie()
//     {
//         FindAnyObjectByType<AudioManager>().Play("ZombieDie");
//         Debug.Log("Zombie Killed");
//         Destroy(gameObject);
//         GameManager.Instance.ZombieKilled(); // Notify GameManager
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("MeleeAttack")) 
//         {
//             Debug.Log("Hit by Melee Attack");
//             float damageAmount = 50; 
//             Damage(damageAmount); 
//         }
//     }
// }
using UnityEngine;

public class ZombieGetDamage : MonoBehaviour, IDamagable
{
    [Header("Health")]
    public float health;
    public float maxHealth;

    private void Start()
    {
        GameManager.Instance.RegisterZombie();
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            zombieDie();
        }
    }

    public void zombieDie()
    {
        FindAnyObjectByType<AudioManager>().Play("ZombieDie");
        Debug.Log("Zombie Killed");
        Destroy(gameObject);
        GameManager.Instance.ZombieKilled();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MeleeAttack"))
        {
            KnifeAttack knife = other.GetComponent<KnifeAttack>();
            if (knife != null && knife.isAttacking)
            {
                Debug.Log("Hit by Melee Attack");
                float damageAmount = 50;
                Damage(damageAmount);
            }
        }
    }
}
