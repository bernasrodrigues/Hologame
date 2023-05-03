using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderObject : BaseObject
{
    public bool isOn = false;
    public ShootLaser ExpanderExit;


    // Update is called once per frame
    void Update()
    {
        ExpanderExit.on = isOn;

        isOn = false;
    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        isOn = true;

    }

}