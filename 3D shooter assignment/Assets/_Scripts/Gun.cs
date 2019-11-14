
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Gun : MonoBehaviour
{
    public int ammo;
    public int maxAmmo;
    public float damage = 15f;
    public float range = 5f;
    public float force = 100f;
    public float firerate = 15f;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public GameObject Barrel;

    private float nextTimeToFire = 0f;
    // Update is called once per frame


    void Start() {
        currentAmmo = ammo;
    }
    void Update()
    {

        if (isReloading)
            return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
 
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {

            nextTimeToFire = Time.time + 1f / firerate;
            //if (ammo >= 0)
            //{
            Shoot();
            GameObject.Find("AmmoVar").GetComponent<Text>().text = "" + currentAmmo + "/100";
            GetComponent<AudioSource>().Play();
            //}
        }

    }

    IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = ammo;

        GameObject.Find("AmmoVar").GetComponent<Text>().text = "" + currentAmmo + "/100";
        isReloading = false;
    }

    void Shoot() 
    {

        MuzzleFlash.Play();

        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);
        }
    }
}
