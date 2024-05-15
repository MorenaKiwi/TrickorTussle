using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float projectileForce = 25.0f;
    public bool isOnGround = false;
    public float shootInterval = .75f; 
    public int shotsBeforeRecharge = 2;
    private int shotsFired = 0; 
    private bool isShooting = false; 

    private void Start()
    {
        StartCoroutine(ShootPeriodically());
    }

    public IEnumerator ShootPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            if (shotsFired < shotsBeforeRecharge && !isShooting)
            {
                Shoot();
                shotsFired++;
            }
            else if (shotsFired >= shotsBeforeRecharge)
            {
                isShooting = true; 
                shotsFired = 0;
                yield return new WaitForSeconds(shootInterval); // Recharge time
                isShooting = false; // Reset for next shot cycle
            }
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(newProjectile.transform.forward * projectileForce, ForceMode.Impulse);
        }
    }
    
    
}
