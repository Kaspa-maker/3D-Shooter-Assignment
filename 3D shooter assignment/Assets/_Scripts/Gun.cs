
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float damage = 15f;
    public float range = 5f;
    public float force = 100f;
    public float firerate = 15f;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public GameObject Barrel;

    private float nextTimeToFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
            GetComponent<AudioSource>().Play();
        }


    }

    void Shoot() 
    {

        MuzzleFlash.Play();
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
