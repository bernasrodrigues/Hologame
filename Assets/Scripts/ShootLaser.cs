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
        if (laserBeam != null)
        {
            Destroy(laserBeam.laserObj);
        }

        if (!on)        // check if turned on
        {
            return;
        }



        laserBeam = new LaserBeam(gameObject.transform.position, gameObject.transform.up, laserMaterial);


    }


    public void button()
    {
        on = !on;
    }


}
