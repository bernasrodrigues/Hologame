using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderObject : BaseObject
{
    public bool isOn = false;
    public ShootLaser ExpanderExit;

    public LaserBeam incomingLaserBeam;        // laser Beam that touches the object


    // Update is called once per frame
    void Update()
    {
        ExpanderExit.on = isOn;
        ExpanderExit.referenceLaser = incomingLaserBeam;

        isOn = false;
        incomingLaserBeam = null;
    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        isOn = true;
        this.incomingLaserBeam = incomingLaserBeam.DeepClone();

    }

}