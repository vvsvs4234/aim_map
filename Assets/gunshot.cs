using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class GunShot : MonoBehaviour
{
    [Header("Weapon")]
    public Camera fpsCamera;
    public float range = 100f;
    public float fireRate = 0.09f;
    public int magazineSize = 30;
    public float reloadTime = 2.2f;
    public float damage = 25f;
    public float headshotMultiplier = 2f;

    [Header("Spray (CS style)")]
    public float sprayStrength = 1.5f;
    public float sprayResetTime = 0.2f;
    public float randomFactor = 0.2f;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public GameObject bloodEffect;
    public GameObject damageTextPrefab;

    [Header("Sound")]
    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    [Header("Animation")]
    public Animator animator;          // <-- ДОДАНО
    private readonly string reloadTrigger = "reload";

    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    // spray
    private int sprayIndex = 0;
    private float lastShotTime;

    private Vector2[] sprayPattern = new Vector2[]
    {
        new Vector2(0f, 0.5f),
        new Vector2(0.1f, 0.8f),
        new Vector2(-0.1f, 1.1f),
        new Vector2(0.2f, 1.4f),
        new Vector2(-0.2f, 1.7f),
        new Vector2(0.3f, 2.0f),
        new Vector2(-0.3f, 2.2f),
        new Vector2(0.4f, 2.5f),
        new Vector2(-0.4f, 2.7f),
        new Vector2(0.5f, 3.0f),
    };

    void Start()
    {
        currentAmmo = magazineSize;
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isReloading)
            return;

        // reset spray якщо перестав стріляти
        if (Time.time - lastShotTime > sprayResetTime)
            sprayIndex = 0;

        // Перезарядка
        if (Keyboard.current.rKey.wasPressedThisFrame && currentAmmo < magazineSize)
        {
            StartCoroutine(Reload());
            return;
        }

        // Стрільба
        if (Mouse.current.leftButton.isPressed && Time.time >= nextTimeToFire)
        {
            if (currentAmmo > 0)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        lastShotTime = Time.time;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);

        Vector2 spray = sprayPattern[Mathf.Min(sprayIndex, sprayPattern.Length - 1)];
        sprayIndex++;

        spray.x += Random.Range(-randomFactor, randomFactor);
        spray.y += Random.Range(-randomFactor, randomFactor);

        Vector3 direction =
            fpsCamera.transform.forward +
            fpsCamera.transform.right * spray.x * 0.01f * sprayStrength +
            fpsCamera.transform.up * spray.y * 0.01f * sprayStrength;

        Debug.DrawRay(fpsCamera.transform.position, direction * range, Color.red, 1f);

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, direction, out hit, range))
        {
            float finalDamage = damage;

            if (hit.collider.CompareTag("Head"))
                finalDamage *= headshotMultiplier;

            Mannequin target = hit.collider.GetComponentInParent<Mannequin>();

            if (target != null)
            {
                target.TakeDamage(finalDamage);

                if (bloodEffect != null)
                {
                    GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(blood, 2f);
                }
            }
            else
            {
                if (hitEffect != null)
                    Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            if (damageTextPrefab != null)
            {
                Vector3 spawnPos = hit.point + Vector3.up * 0.2f;
                GameObject dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

                TextMeshPro tmp = dmgText.GetComponent<TextMeshPro>();
                if (tmp != null)
                    tmp.text = finalDamage.ToString("F0");

                dmgText.transform.LookAt(fpsCamera.transform);
                dmgText.transform.Rotate(0, 180f, 0);

                Destroy(dmgText, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        // 🔥 Запуск анімації
        if (animator != null)
            animator.SetTrigger(reloadTrigger);

        if (audioSource != null && reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        sprayIndex = 0;
        isReloading = false;
    }
}