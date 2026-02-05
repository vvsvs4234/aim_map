using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public Vector2[] sprayPattern;

    public float snappiness = 25f;
    public float returnSpeed = 10f;

    Vector3 currentRotation;
    Vector3 targetRotation;

    int shotIndex = 0;

    void Start()
    {
        // AK-style spray
        sprayPattern = new Vector2[]
        {
            new Vector2(0.0f, 1.5f),
            new Vector2(0.2f, 1.7f),
            new Vector2(-0.2f, 1.9f),
            new Vector2(0.3f, 2.1f),
            new Vector2(-0.3f, 2.3f),

            new Vector2(0.6f, 2.2f),
            new Vector2(0.8f, 2.1f),
            new Vector2(1.0f, 2.0f),
            new Vector2(1.2f, 1.9f),

            new Vector2(-1.0f, 1.8f),
            new Vector2(-1.2f, 1.7f),
            new Vector2(-1.4f, 1.6f)
        };
    }

    void Update()
    {
        targetRotation = Vector3.Lerp(
            targetRotation,
            Vector3.zero,
            returnSpeed * Time.deltaTime
        );

        currentRotation = Vector3.Slerp(
            currentRotation,
            targetRotation,
            snappiness * Time.deltaTime
        );

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void AddRecoil()
    {
        Vector2 recoil = sprayPattern[
            Mathf.Min(shotIndex, sprayPattern.Length - 1)
        ];

        targetRotation += new Vector3(
            -recoil.y,
            recoil.x,
            0f
        );

        shotIndex++;
    }

    public void ResetSpray()
    {
        shotIndex = 0;
    }
}
