using UnityEngine;

public class BuyMenuManager : MonoBehaviour
{
    public GameObject buyMenu;
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuyMenu();
        }
    }

    void ToggleBuyMenu()
    {
        isOpen = !isOpen;
        buyMenu.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f; // Пауза
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f; // Продовжити гру
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}