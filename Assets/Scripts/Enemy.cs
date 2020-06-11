
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public int health = 50;
    public void takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0 )
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(gameObject);
    }
}
