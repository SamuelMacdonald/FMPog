
using UnityEngine;
using TMPro;

public class gun : MonoBehaviour
{
    public float damage = 10;
    public float range = 100f;
    public float fireRate = 1000f;
    public ParticleSystem Muzzle;
    public AudioSource shot;
    public AudioSource shell;
    public float bullets;
    public Animator an;
    public bool reloading;
    public TextMeshProUGUI ammoDisplay;

    bool soundShell = false;

    public Camera fpsCam;
    private float nextTimeToFire = 0f;
    private void Update()
    {
        ammoDisplay.text = bullets.ToString();
        an.SetBool("GunShot", false);

        if (Input.GetKeyDown("r") && reloading == false && bullets == 0)
        {
           an.SetBool("Reloading", true);
            
            reloading = true;
            shell.Play();


        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && !pauseMenu.GameIsPaused && bullets >= 1 && reloading == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            an.SetBool("GunShot", true);
            Shoot();
            Muzzle.Play();
            shot.Play();
            bullets--;

        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            enemyHealth enemy = hit.transform.GetComponent<enemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                
            }
            
        }
        
    }
    public void stopanim()
    {
            an.SetBool("Reloading", false);
        reloading = false;
        bullets = bullets + 2;
       

    } 
}
