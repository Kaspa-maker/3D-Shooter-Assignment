using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform gunTr = GameObject.Find("LaserSight").transform;
        lr.SetPosition(0, gunTr.position);
        lr.SetPosition(1, gunTr.forward*150);
        RaycastHit hit;










        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider)
            {
               // Transform gunTr = GameObject.Find("LaserSight").transform;
               // lr.SetPosition(0, gunTr.position);
               // lr.SetPosition(1, hit.point);
                //lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else {
                //lr.SetPosition(1, new Vector3(0, 0, 5000));
              //  Transform gunTr = GameObject.Find("LaserSight").transform;
              //  lr.SetPosition(0, gunTr.position);
            }
        }
    }

}
