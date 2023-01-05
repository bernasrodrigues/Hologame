using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{

    public Material laserMaterial;
    public bool on;
    LaserBeam laserBeam;

    // Update is called once per frame
    void Update()
    {
        if (!on)        // check if turned on
        {
            return;
        }



        if (laserBeam != null)
        {
            Destroy(laserBeam.laserObj);
        }
        laserBeam = new LaserBeam(gameObject.transform.position, gameObject.transform.up, laserMaterial);


    }
}
