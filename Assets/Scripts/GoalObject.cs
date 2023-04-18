using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObject : BaseObject
{
    public bool isTouching;
    public float timeOut = 0.3f;
    public float timeOutCount;
    public float timeTillFull = 3f;
    public float timeCount;

    public override void HandleTouchLaser(LaserBeam laserBeam)
    {
        isTouching = true;
    }



    public void Update()
    {
        if (isTouching)
        {
          timeCount = Math.Min(timeCount + Time.deltaTime, timeTillFull);             // what value is smaller timetillfull or the timecount (no point in making the time count larger than time full)
       
        
        
        }
        else
        {
            timeOutCount = Math.Min(timeOutCount + Time.deltaTime, timeOut);
            if (timeOutCount == timeOut)
            {
                timeCount = 0;
                timeOutCount = 0;
            }

        }



        isTouching = false;
    }



}
