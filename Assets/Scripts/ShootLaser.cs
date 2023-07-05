using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootLaser : MonoBehaviour
{
    public bool on;
    public LaserBeam laserBeam;

    public LaserBeam referenceLaser;
    public LineRenderer lineRenderer;
    //laser Parameters

    [SerializeField] private bool loggerOn = false;
    private Logger logger;


    // Update is called once per frame
    void Update()
    {
        if (laserBeam != null)
        {
            Destroy(laserBeam.laserObj);
        }

        if (!on)        // check if turned on, if not on ignore
        {
            return;
        }

        laserBeam = new LaserBeam(gameObject.transform.position, gameObject.transform.up, 
                                    referenceBeam: referenceLaser, 
                                    referenceLineRenderer: lineRenderer,
                                    maxLenght : 100);


    }


    public void button()
    {
        on = !on;
    }
}