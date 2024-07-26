using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_walking : MonoBehaviour
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

    // ai walking
    public float timeForWalk = 2f;
    public float walkSpeed = 2f;
    void Start()
    {
        aSource.volume = 0f;
        currentAmmo = maxAmmo;
        //leftHandMac.SetActive(false);
        anim.SetBool("Walking", true);
        StartCoroutine(MoveAndRotate());
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
                anim.SetBool("Walking", false);
                isPlayerNear = true;
                direction = (player.transform.position - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                lookRotation *= Quaternion.Euler(0f, 45f, 0f); //add 45 degree rotation in y-axis
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            }
            else
            {
                //  anim.SetBool("Walking", true);
                isPlayerNear = false;
                
            }

            if (currentAmmo <= 0)
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

    IEnumerator MoveAndRotate()
    {
        while (true)
        {
            if (isPlayerNear | anim.enabled == false)
            {
                break;
            }
                
            // Move forward for 2 seconds
            float startTime = Time.time;
            while (Time.time - startTime < timeForWalk)
            {
                transform.position += transform.forward * walkSpeed * Time.deltaTime;
                yield return null;
            }

            // Rotate 180 degrees
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
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
