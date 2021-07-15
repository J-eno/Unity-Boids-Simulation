using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        TheBrain.AlmightyBrain.DestroyAll += Die;
    }

    public int GetHealth() {
        return currentHealth;
    }
    public void SetHealth(int hp) {
        currentHealth = hp;
    }
    public void TakeDamage() {
        currentHealth--;
        if(currentHealth <= 0) {
            Die();
        }
    }
    void Die() {
        TheBrain.AlmightyBrain.DestroyAll -= Die;
        Destroy(gameObject);
    }

}
