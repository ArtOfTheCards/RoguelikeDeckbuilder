using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private GameObject damageIndicator;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public void damage(int value) {
        currentHealth = Mathf.Max(currentHealth - value, 0);
        if (currentHealth == 0) {
            die();
        }

        // show damage indicator
        showDamageIndicator(value);
    }

    void showDamageIndicator(int value) {
        if (damageIndicator == null) return;

        var text = Instantiate(damageIndicator, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = value.ToString();
    }

    public void heal(int value) {
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    private void die() {
        Destroy(this.gameObject);
    }
}
