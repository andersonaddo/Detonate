using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {

    public bool isPlayer;
    public Collider2D playerHitbox;

    public int maxHealth;
    public int currentHealth { get; private set; }
    [HideInInspector] public bool isDead;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
            healthSlider.gameObject.SetActive(false);
        }
    }

    public void incrementHealthBy(int change)
    {
        if (healthSlider != null && !healthSlider.gameObject.activeSelf) healthSlider.gameObject.SetActive(true);
        if (isDead) return;
        currentHealth += change;
        if (currentHealth <= 0) killObject();
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = currentHealth;
    }

    public void killObject()
    {
        isDead = true;
        if (healthSlider != null) healthSlider.enabled = false;
        if (isPlayer)
        {
            gameObject.SetActive(false);
            playerHitbox.enabled = false;
            gameManager.Instance.endGame(true);
        }
        Destroy(gameObject);
    }
}
