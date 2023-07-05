using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitter : BaseObject
{

    public bool isOn = false;
    public LaserBeam referenceLaser = null;
    public List<ShootLaser> ShootLaserExits = new List<ShootLaser>();


    // Update is called once per frame
    void Update()
    {
        foreach (ShootLaser shootLaser in ShootLaserExits)
        {
            //shootLaser.setLaser(incomingLaserBeam);
            shootLaser.on = isOn;
            shootLaser.referenceLaser = referenceLaser;
        }

        isOn = false;
        //referenceLaser = null;

    }



    public override void HandleTouchLaser(LaserBeam incomingLaserBeam)
    {
        isOn = true;

        if (this.referenceLaser != null && this.referenceLaser.laserObj != null)
        {
            Destroy(this.referenceLaser.laserObj);
            this.referenceLaser = null;
        }

        this.referenceLaser = incomingLaserBeam.DeepClone();

    }
}
