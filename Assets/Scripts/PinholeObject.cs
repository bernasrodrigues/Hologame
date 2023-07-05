using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinholeObject : BaseObject
{
    public ShootLaser PinholeExit;


    public bool isOn = false;

    // Update is called once per frame
    void Update()
    {
        PinholeExit.on = isOn;
        isOn = false;
    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        isOn = true;
        PinholeExit.referenceLaser = incomingLaserBeam.DeepClone();

    }


}
