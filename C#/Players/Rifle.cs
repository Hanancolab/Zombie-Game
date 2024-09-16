using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things ")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    float nextTimeTohoot = 0f;
    public Player playerScript;
    public Transform hand;
    public Animator animator;

    [Header("Rifle ammo")]
    int maxAmmo = 32;
    public int mag = 10;
    int presentAmmo;
    public float reloadingTime = 1.3f;
    bool setReloading = false;
    public GameObject rifleUI;
    public GameObject ammoFinish;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject woodEffect;
    public GameObject goreEffect;

    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        transform.SetParent(hand);
        rifleUI.SetActive(true);
        presentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (setReloading)
            return;
        if(presentAmmo <= 0 )
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeTohoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeTohoot = Time.time + 1f / fireCharge;
            Shoot();
        }else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);

        }else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }

    private void Shoot()
    {
        if(mag == 0)
        {
            StartCoroutine(AmmoFinish());
            return;
        }
        presentAmmo--;
        if(presentAmmo == 0)
        {
            mag--;
        }

        //Mag UI 
        Ammo.occurrence.UpdateAmmoText(presentAmmo);
        Ammo.occurrence.UpdateMagText(mag);


        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>(); 
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>(); 
            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject impactwood = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactwood, 1f);
            }
            else if(zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if(zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        playerScript.playerSpeed = 0f;
        playerScript.playerSprint = 0f; 
        setReloading = true; 
        //Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmo = maxAmmo;
        playerScript.playerSpeed = 1.9f;
        playerScript.playerSprint = 3f;
        setReloading = false;
    }
    IEnumerator AmmoFinish()
    {
        ammoFinish.SetActive(true);
        yield return new WaitForSeconds(3f);
        ammoFinish.SetActive(false);
    }
}