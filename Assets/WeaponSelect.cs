using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    public GameObject selectedEffect; // наприклад outline або підсвітка

    private bool isSelected = false;

    public void Select()
    {
        isSelected = true;

        // включаємо ефект
        if (selectedEffect != null)
            selectedEffect.SetActive(true);

        Debug.Log("Вибрано: " + gameObject.name);
    }

    public void Deselect()
    {
        isSelected = false;

        if (selectedEffect != null)
            selectedEffect.SetActive(false);
    }
}