using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons; // Масив зброї
    private int currentWeapon = 0;

    void Start()
    {
        // Вмикаємо першу зброю
        SelectWeapon(currentWeapon);
    }

    void Update()
    {
        // Перевірка клавіш 1,2,3
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);

        // Альтернатива колесо миші
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentWeapon++;
            if (currentWeapon >= weapons.Length) currentWeapon = 0;
            SelectWeapon(currentWeapon);
        }
        if (scroll < 0f)
        {
            currentWeapon--;
            if (currentWeapon < 0) currentWeapon = weapons.Length - 1;
            SelectWeapon(currentWeapon);
        }
    }

    void SelectWeapon(int index)
    {
        if (index >= weapons.Length) return;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }

        currentWeapon = index;
    }
}
