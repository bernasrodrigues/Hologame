using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitter : BaseObject
{

    public bool isOn = false;
    public List<ShootLaser> ShootLaserExits = new List<ShootLaser>();
    

    // Update is called once per frame
    void Update()
    {
        foreach (ShootLaser shootLaser in ShootLaserExits)
        {
            //shootLaser.setLaser(incomingLaserBeam);
            shootLaser.on = isOn;
        }


        isOn = false;
    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        isOn = true;

    }

}
