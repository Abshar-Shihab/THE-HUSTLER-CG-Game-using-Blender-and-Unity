using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Cinemachine;

public class Sniper : MonoBehaviour
{
    public float damage = 500f;
    public float range = 1000f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    //ammo
    public int maxAmmo = 5;
    private int currentAmmo;
    private float reloadTime = 2f;
    private bool isReloading = false;
    public Animator anim;


    //checker
   // public GameObject sniper;
   // public GameObject AK;

    // to hit the target
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource aSource;
    // Update is called once per frame

    private float nextTimeToFire = 0f;

    void Start()
    {
        //  sniper.SetActive(true);
        aSource.volume = 0f;
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);
    }

    void Update()
    {
        /*if (Input.GetKey(KeyCode.Alpha1))
        {
            AK.SetActive(true);
            sniper.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            AK.SetActive(false);
            sniper.SetActive(true);
        }*/

        if (isReloading)
        {
            return;
        }

            if (currentAmmo <= 0 | Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo)
            {
                StartCoroutine(Reload());
                return; //return used if this shoot function work then below function won't work
            }

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Sniper_load");

        anim.SetBool("Sniper_load", true);

        // reload sound effects
        FindObjectOfType<AudioManager>().PlayReload("Reloading", true);

        yield return new WaitForSeconds(reloadTime);


        anim.SetBool("Sniper_load", false);

        // finish the reloading sound loop
        FindObjectOfType<AudioManager>().PlayReload("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        aSource.volume = 0.5f;
        aSource.Play();

        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
            // script accessing
            Target target = hit.transform.GetComponent<Target>();
        
        //script accesing
        EnemyController enemyController = hit.collider.GetComponentInParent<EnemyController>();
            Vector3 direction = hit.point;
            if (enemyController)
            {
                enemyController.OnEnemyShot(direction, hit.collider.GetComponent<Rigidbody>());
            }
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            // giving effect to the enemy
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            // removing the object creating wile the impact
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}

