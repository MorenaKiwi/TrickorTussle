using System.Collections;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    private float shootInterval = 0.5f;
    private float projectileForce = 50.0f;
    private bool shootStraight = true; // Variable to toggle shooting style

    private void Start()
    {
        StartCoroutine(ShootPeriodically());
    }

    private IEnumerator ShootPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            if (shootStraight)
            {
                Shoot();
            }
            else
            {
                ShootWithAngle(-90);
            }

            // Toggle shooting style for the next iteration
            shootStraight = !shootStraight;
        }
    }

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(newProjectile.transform.forward * projectileForce, ForceMode.Impulse);
        }
    }

    public void ShootWithAngle(float angle)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 shootingDirection = Quaternion.Euler(0, angle, 0) * newProjectile.transform.forward;
            rb.AddForce(shootingDirection * projectileForce, ForceMode.Impulse);
        }
    }
}
