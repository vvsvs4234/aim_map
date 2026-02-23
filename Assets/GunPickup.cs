using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Camera playerCamera;
    public float distance = 5f;
    public Transform gunHolder;

    private GameObject currentGun;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance))
            {
                WallGun wallGun = hit.collider.GetComponent<WallGun>();

                if (wallGun != null)
                {
                    TakeGun(wallGun.handGunPrefab);
                }
            }
        }
    }

    void TakeGun(GameObject gunPrefab)
    {
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        currentGun = Instantiate(gunPrefab, gunHolder.position, gunHolder.rotation);
        currentGun.transform.SetParent(gunHolder);
    }
}
