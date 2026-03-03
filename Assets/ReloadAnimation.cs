using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadAnimation : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Animation Settings")]
    public string reloadTriggerName = "reload";

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            PlayReloadAnimation();
        }
    }

    void PlayReloadAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(reloadTriggerName);
        }
    }
}