using UnityEngine;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    public Camera fpsCamera;
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 10f;

    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    public InputAction shootAction;

    private float nextTimeToFire = 0f;

    void OnEnable()
    {
        shootAction.Enable();
    }

    void OnDisable()
    {
        shootAction.Disable();
    }

    void Update()
    {
        if (shootAction.IsPressed() && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
            muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Health target = hit.transform.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hitEffect != null)
            {
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
    }
}
