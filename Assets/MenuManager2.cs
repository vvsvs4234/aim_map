using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager2 : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject mapSelectPanel;
    public GameObject settingsPanel;

    // 🔧 SETTINGS
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // ▶️ PLAY (відкрити карти)
    public void OnPlayPressed()
    {
        mainMenuPanel.SetActive(false);
        mapSelectPanel.SetActive(true);
    }

    // 🔙 BACK
    public void OnBackPressed()
    {
        mapSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 🎮 ВИБІР КАРТИ (ГОЛОВНЕ)
    public void LoadMap(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ❌ EXIT
    public void OnExitPressed()
    {
        Debug.Log("Game Closed");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}