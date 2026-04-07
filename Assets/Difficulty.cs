using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class DifficultyManager : MonoBehaviour
{
    public static Difficulty currentDifficulty;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetEasy()
    {
        currentDifficulty = Difficulty.Easy;
    }

    public void SetMedium()
    {
        currentDifficulty = Difficulty.Medium;
    }

    public void SetHard()
    {
        currentDifficulty = Difficulty.Hard;
    }
}