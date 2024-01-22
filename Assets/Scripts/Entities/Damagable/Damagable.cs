using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public void damage(int value) {
        currentHealth = Mathf.Max(currentHealth - value, 0);
        if (currentHealth == 0) {
            die();
        }
    }

    public void heal(int value) {
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    private void die() {
        Destroy(this.gameObject);
    }
}
