using UnityEngine;

public class DifficultyInteract : MonoBehaviour
{
    public Difficulty difficulty;
    public GameObject pressText;

    private bool isNear = false;

    void Update()
    {
        if (isNear)
        {
            Debug.Log("Player near " + difficulty);

            pressText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E on " + difficulty);
                DifficultyManager.currentDifficulty = difficulty;
            }
        }
        else
        {
            pressText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger");

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED");
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER EXIT");
            isNear = false;
        }
    }
}