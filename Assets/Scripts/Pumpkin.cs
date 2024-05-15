using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public GameObject PumpkinPrefab;
    public Transform spawnPoint;
    private float shootInterval = 2.5f;
    public float projectileForce = 80.0f;
    private bool isShooting = false;

    private Animator animator;
    //private BoosterBar boosterBar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ShootPeriodically());
    }

    private IEnumerator ShootPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            StartPumpkin();
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!isShooting) 
        {
            GameObject newProjectile = Instantiate(PumpkinPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(newProjectile.transform.forward * projectileForce, ForceMode.Impulse);
                if (PumpkinPrefab.activeSelf){
                    animator.SetTrigger("Throw");
                    isShooting = true;}
            }
        }
    }

    private void StartPumpkin()
    {
        isShooting = false; // Reset the shooting flag
        animator.SetBool("Pumpkin", true);
    }
}
