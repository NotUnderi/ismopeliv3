using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    public Glock weapon;
    public M4A1 weapon1;
    public WeaponSwitch wSwitch;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    public Animator animator_glock;
    public Animator animator_m4a1;
    public ParticleSystem blood;
    public Text ammoText;
    public Text gunText;
    public Text reloadprompt;

    [SerializeField]
    private LayerMask mask;
    
    [SerializeField]
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
        weapon.firesound = GameObject.FindGameObjectWithTag("Glock").GetComponent<AudioSource>();
        weapon1.firesound = GameObject.FindGameObjectWithTag("M4A1").GetComponent<AudioSource>();
        weapon.currentAmmo = weapon.mag + 1;
        weapon1.currentAmmo = weapon1.mag + 1;
    }
    void OnEnable()
    {
        isReloading = false;

        animator_m4a1.SetBool("reloading", false);
        animator_glock.SetBool("reloading", false);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
        updateAmmo();
        if (isReloading)
            return;
        if (wSwitch.selectedWeapon == 0)
        {
            gunText.text = weapon.model;
            if (weapon.currentAmmo <= 0)
            {
                StartCoroutine(Reload());
            }

            if (weapon.firemode == 1)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {

                    animator_glock.SetBool("shooting", true);
                    nextTimeToFire = Time.time + 1f / weapon.firerateauto;
                    Shoot();
                    weapon.currentAmmo--;
                    //Debug.Log(nextTimeToFire);
                    //Debug.Log(Time.time);
                    //animator_glock.SetBool("shooting", false);
                }
            }
            if (weapon.firemode == 0)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    animator_glock.SetBool("shooting", true);
                    nextTimeToFire = Time.time + 1f / weapon.firerate;
                    Shoot();
                    weapon.currentAmmo--;
                    //Debug.Log(nextTimeToFire);
                    //Debug.Log(Time.time);
                    //animator_glock.SetBool("shooting", false);
                }
            }
            if (Input.GetKeyDown("v"))
            {
                weapon.firemode = 1 - weapon.firemode;
            }
            if (Input.GetKeyDown("r"))
                StartCoroutine(Reload());
            
        }
        if (wSwitch.selectedWeapon == 1)
        {
            gunText.text = weapon1.model;
            if (weapon1.currentAmmo <= 0)
            {
                StartCoroutine(ReloadM4());
            }
            if (weapon1.firemode == 1)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    animator_m4a1.SetBool("shooting", true);
                    nextTimeToFire = Time.time + 1f / weapon1.firerateauto;
                    Shoot();
                    weapon1.currentAmmo--;
                    Debug.Log(nextTimeToFire);
                    Debug.Log(Time.time);
                    //animator_m4a1.SetBool("shooting", false);
                }
            }
            if (weapon1.firemode == 0)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    animator_m4a1.SetBool("shooting", true);
                    nextTimeToFire = Time.time + 1f / weapon1.firerate;
                    Shoot();
                    weapon1.currentAmmo--;
                    Debug.Log(nextTimeToFire);
                    Debug.Log(Time.time);
                    //animator_m4a1.SetBool("shooting", false);
                }
            }
            if (Input.GetKeyDown("v"))
            {
                weapon1.firemode = 1 - weapon1.firemode;
            }
            if (Input.GetKeyDown("r"))
                StartCoroutine(ReloadM4());
        }

        void Shoot()
        {
            if (wSwitch.selectedWeapon == 0)
            {

                weapon.firesound.Play();
                weapon.muzzleFlash.Play();
                animator_glock.Play("PistolShoot");
            }
            if(wSwitch.selectedWeapon == 1)
            {

                weapon1.firesound.Play();
                weapon1.muzzleFlash.Play();
                animator_m4a1.Play("M4A1Shoot");
            }
            RaycastHit _hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
            {
                //Hit smth
                Debug.Log("We hit " + _hit.collider.gameObject);
                Enemy enemy = _hit.transform.GetComponent<Enemy>();
                if (_hit.transform.CompareTag("EnemyTag"))
                {
                   /* if (wSwitch.selectedWeapon == 0)
                        enemy.takeDamage(weapon.damage);
                    if (wSwitch.selectedWeapon == 1)*/
                        enemy.takeDamage(weapon1.damage);
                    EnemyShot(_hit.collider.name, weapon.damage);
                    ParticleSystem BloodGO = Instantiate(blood, _hit.point, Quaternion.LookRotation(_hit.normal));
                    Destroy(BloodGO, 2f);//blood.Play();
                }
                /*
                if (_hit.collider.tag == "EnemyTag")
                {
                    EnemyShot(_hit.collider.name, weapon.damage);
                }
                */
                if (_hit.rigidbody!= null)
                {
                    _hit.rigidbody.AddForce(-_hit.normal * weapon.force);
                }
                if (enemy == null)
                {
                    GameObject ImpactGO = Instantiate(weapon.impactEffect, _hit.point, Quaternion.LookRotation(_hit.normal));
                    Destroy(ImpactGO, 2f);
                }

                
            }

            animator_glock.SetBool("shooting", false);
            animator_m4a1.SetBool("shooting", false);
        }
        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading Glock");
            animator_glock.SetBool("reloading", true);
            yield return new WaitForSeconds(weapon.reloadTime - 0.25f);

            animator_glock.SetBool("reloading", false);
            yield return new WaitForSeconds(0.25f);
            if (weapon.currentAmmo > 0)
                weapon.currentAmmo = weapon.mag + 1;
            else
                weapon.currentAmmo = weapon.mag;
            isReloading = false;

        }
        IEnumerator ReloadM4()
        {
            isReloading = true;
            Debug.Log("Reloading M4A1");

            animator_m4a1.SetBool("reloading", true);
            yield return new WaitForSeconds(weapon1.reloadTime - 0.25f);
            animator_m4a1.SetBool("reloading", false);
            yield return new WaitForSeconds(weapon1.reloadTime - 0.25f);
            if (weapon1.currentAmmo > 0)
                weapon1.currentAmmo = weapon1.mag + 1;
            else
                weapon1.currentAmmo = weapon1.mag;
            isReloading = false;


        }
        void EnemyShot(string _enemy, int _damage)
        {
            Debug.Log(_enemy+" has been shot for "+_damage + " damage.");

        }
        void updateAmmo()
        {
            ammoText.text = "";
            if (wSwitch.selectedWeapon == 0)
            {
                ammoText.text = weapon.currentAmmo.ToString() + "/" + weapon.mag;
                if (weapon.currentAmmo < 5) 
                    reloadprompt.text = "Press R to reload";
                else
                    reloadprompt.text = "";

            }
            if (wSwitch.selectedWeapon == 1)
            {
                ammoText.text = weapon1.currentAmmo.ToString() + "/" + weapon1.mag;
                if (weapon1.currentAmmo < 10) 
                    reloadprompt.text = "Press R to reload";
                else 
                    reloadprompt.text = "";
            }
        }
    }
}
