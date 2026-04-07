using UnityEngine;

public class BotController : MonoBehaviour
{
    private float speed;
    private bool canMove;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        switch (DifficultyManager.currentDifficulty)
        {
            case Difficulty.Easy:
                canMove = false;
                break;

            case Difficulty.Medium:
                canMove = true;
                speed = 2f;
                break;

            case Difficulty.Hard:
                canMove = true;
                speed = 5f;
                break;
        }
    }

    void Update()
    {
        if (canMove)
        {
            float move = Mathf.Sin(Time.time * speed);
            transform.position = startPos + new Vector3(move, 0, 0);
        }
    }
}