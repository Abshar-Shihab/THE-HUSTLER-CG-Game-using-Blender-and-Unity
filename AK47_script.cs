using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47_script : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public GameObject AK;

    //ammo
    public int maxAmmo = 30;
    private int currentAmmo;
    private float reloadTime = 2f;
    private bool isReloading = false;
    public Animator anim;

    // hiding the mac while relaoding and give another mac in other hand
    public GameObject defaultMac;
    public GameObject leftHandMac;

    // to hit the target
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public AudioSource aSource;
    // Update is called once per frame

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = maxAmmo;
        leftHandMac.SetActive(false);
    }

    void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);
    }

    void Update()
    {
        
        if (AK.activeInHierarchy) {
            if (isReloading)
                return;

            if (currentAmmo <= 0 | Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo)
            {
                leftHandMac.SetActive(true);
                defaultMac.SetActive(false);

                StartCoroutine(Reload());
                return; //return used if this shoot function work then below function won't work
            }

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
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

        leftHandMac.SetActive(false);
        defaultMac.SetActive(true);

        anim.SetBool("Reloading", false);

        // finish the reloading sound loop
        FindObjectOfType<AudioManager>().PlayReload("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        aSource.Play();

        RaycastHit hit;

        currentAmmo--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            // removing the object creating wile the impact
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
