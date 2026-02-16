using UnityEngine;
using System.Collections;

public class Mannequin : MonoBehaviour
{
    public float health = 100f;
    private float maxHealth;
    private Renderer[] renderers;
    private Collider[] colliders;

    void Start()
    {
        maxHealth = health;
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(name + " отримав " + amount + " damage. Здоров’я: " + health);

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " мертвий");
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        // Ховаємо манекен
        foreach (Renderer r in renderers)
            r.enabled = false;

        foreach (Collider c in colliders)
            c.enabled = false;

        yield return new WaitForSeconds(5f); // Чекаємо 5 секунд

        // Відновлюємо здоров’я
        health = maxHealth;

        // Показуємо манекен знову
        foreach (Renderer r in renderers)
            r.enabled = true;

        foreach (Collider c in colliders)
            c.enabled = true;

        Debug.Log(name + " відродився!");
    }
}
