using UnityEngine;

public class Mannequin : MonoBehaviour
{
    public float health = 100f;

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
        // Можна додати анімацію смерті
        Debug.Log(name + " мертвий");
        Destroy(gameObject, 2f); // видаляємо через 2 сек
    }
}
