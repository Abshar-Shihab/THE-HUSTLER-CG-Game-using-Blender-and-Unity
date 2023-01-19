using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float detectRange = 1f;

    public GameObject AK;
    public GameObject player;

    //ammo
    public int maxAmmo = 30;
    private int currentAmmo;
    private float reloadTime = 2f;
    private bool isReloading = false;
    public Animator anim;

    // hiding the mac while relaoding and give another mac in other hand
   // public GameObject defaultMac;
   // public GameObject leftHandMac;

    // to hit the target
    //public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource aSource;

    private float nextTimeToFire = 0f;
    private bool isPlayerNear = false;
    private Quaternion lookRotation;
    private Vector3 direction;

    void Start()
    {
        aSource.volume = 0f;
        currentAmmo = maxAmmo;
        //leftHandMac.SetActive(false);
    }

    void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);
    }

    void Update()
    {
        if (AK.activeInHierarchy)
        {
            if (isReloading)
                return;

            // check if player is in range
            if (Vector3.Distance(transform.position, player.transform.position) < detectRange && anim.enabled)
            {
                isPlayerNear = true;
                direction = (player.transform.position - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                lookRotation *= Quaternion.Euler(0f, 45f, 0f); //add 45 degree rotation in y-axis
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            }
            else
            {
                isPlayerNear = false;
            }

            if (currentAmmo <= 0 )
            {
               // leftHandMac.SetActive(true);
               // defaultMac.SetActive(false);

                StartCoroutine(Reload());
                return;
            }

            if (isPlayerNear && Time.time >= nextTimeToFire && anim.enabled)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        anim.SetBool("Reloading", true);

        // reload sound effects
        FindObjectOfType<AudioManager>().PlayReload("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

       // leftHandMac.SetActive(false);
        //defaultMac.SetActive(true);

        anim.SetBool("Reloading", false);

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

        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        //{
          //  Debug.Log(hit.transform.name);
           // Target target = hit.transform.GetComponent<Target>();
          //  if (target != null)
        ////    {
        //        target.TakeDamage(damage);
        //    }

        //    if (hit.rigidbody != null)
         //   {
          //      hit.rigidbody.AddForce(-hit.normal * impactForce);
          //  }
            // removing the object creating wile the impact
          //  GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //    Destroy(impactGO, 2f);
        //}
    }
}
