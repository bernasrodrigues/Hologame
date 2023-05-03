using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserExpander : ShootLaser
{
    public LineRenderer ln;



    private void Update()
    {
        if (laserBeam != null)
        {
            Destroy(laserBeam.laserObj);
        }

        if (!on)        // check if turned on, if not on ignore
        {
            return;
        }

        laserBeam = new LaserBeam(gameObject.transform.position, gameObject.transform.up, laserMaterial,ln,  maxLenght : 2);


    }
}
