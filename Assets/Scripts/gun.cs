
using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10;
    public float range = 100f;
    public float fireRate = 100f;
    public ParticleSystem Muzzle;

    public Camera fpsCam;
    private float nextTimeToFire = 0f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            Muzzle.Play();
            
        }
    }
    
    


    public void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            enemyHealth enemy = hit.transform.GetComponent<enemyHealth>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
