using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{

    private Vector3 pos;
    private Vector3 dir;

    public GameObject laserObj;

    private LineRenderer laser;
    
    private List<Vector3> laserIndices = new List<Vector3>();
    
    private int maxLenght = 100;

    //Logger
    [SerializeField] private bool loggerOn = false;
    private Logger logger;


    public LaserBeam(LaserBeam referenceBeam)
    {
        this.laserObj = new GameObject("Laser Beam");
        this.laser = this.laserObj.AddComponent<LineRenderer>();

        CopyValues(referenceBeam);
    }





    public LaserBeam(Vector3 pos, Vector3 dir, LaserBeam referenceBeam = null,  LineRenderer referenceLineRenderer = null ,   int maxLenght = 100)
    {
        this.pos = pos;
        this.dir = dir;



        // Create Laser Beam object
        this.laserObj = new GameObject("Laser Beam");
        this.laser = this.laserObj.AddComponent<LineRenderer>();

        CopyValues(referenceBeam);


        if (referenceLineRenderer != null)
        {
            this.laser.startWidth = referenceLineRenderer.startWidth;
            this.laser.endWidth = referenceLineRenderer.endWidth;
            this.laser.widthCurve = referenceLineRenderer.widthCurve;

            this.laser.startColor = referenceLineRenderer.startColor;
            this.laser.endColor = referenceLineRenderer.endColor;

            this.laser.numCapVertices = referenceLineRenderer.numCapVertices;
            this.laser.numCornerVertices = referenceLineRenderer.numCornerVertices;

            this.laser.material = referenceLineRenderer.material;

            this.laser.material.EnableKeyword("_EMISSION");
            this.laser.material.SetColor("_EmissionColor", this.laser.startColor);

        }
        else
        {
            this.laser.startWidth = 0.1f;
            this.laser.endWidth = 0.1f;

            this.laser.startColor = Color.red;
            this.laser.endColor = Color.red;


            this.laser.numCornerVertices = 6;
            this.laser.numCapVertices = 6;
        }


        if (maxLenght > -1)
        {
            this.maxLenght = maxLenght;
        }



        CastRay(pos, dir, laser);
    }



    private void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {

        //if (laserIndices.Count < maxLenght)           // check if number of positions is lower than allowed number of positions
        //{
            laserIndices.Add(pos);

            Ray ray = new Ray(pos, dir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300, 1)) // Get laser hit
            {
                CheckHit(hit, dir, laser);
            }
            else
            {
                laserIndices.Add(ray.GetPoint(300)); // If no target hit, add point at the end of the laser
                UpdateLaser();
            }

            UpdateLaser();
        //}
    }





    public void Update()
    {
        //CastRay(this.pos, this.dir, laser);



    }

    public void Clear()
    {
        laser.positionCount = 0;
    }








    // After calculating a hit update the laser indices and set the points in the line renderer
    private void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            if (count < maxLenght)
            {
                laser.SetPosition(count, idx);
                count++;
            }
            else
                break;
        }


        laser.positionCount = count;

    }

    private void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        BaseObject obj = hitInfo.collider.transform.GetComponent<BaseObject>(); // Check if it hit a material that it can interact with

        if (obj != null)
        {
            if (obj.reflective == ReflectiveType.Reflective) // Check material types
            {
                Vector3 pos = hitInfo.point;
                Vector3 dir = Vector3.Reflect(direction, hitInfo.normal); // Calculate reflection angle

                CastRay(pos, dir, laser);
            }
            else if (obj.reflective == ReflectiveType.Refractive)
            {
                Vector3 pos = hitInfo.point;
                laserIndices.Add(pos);

                Vector3 newPos1 = new Vector3(
                    Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.0001f + pos.x,
                    Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.0001f + pos.y,
                    Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.0001f + pos.z); // Calculate new point close to hitpoint

                // Get values for refraction
                float material_1 = WorldInfo.Instance.refraction_index;
                float material_2 = obj.refractionIndex;

                Vector3 norm = hitInfo.normal;
                Vector3 incident = direction;
                Vector3 refractedVector = Refract(material_1, material_2, norm, incident); // Calculate refracted vector

                // Create new ray because the ray only collides with collider once (and wouldn't refract when leaving the object)
                Ray ray1 = new Ray(newPos1, (refractedVector == Vector3.zero) ? direction : refractedVector); // If no refraction occurs, continue with original direction
                CastRay(newPos1, refractedVector, laser); // Cast new ray with refracted direction
            }
            else if (obj.reflective == ReflectiveType.nonReflective)
            {
                obj.HandleTouchLaser(this);
                laserIndices.Add(hitInfo.point);
                UpdateLaser();
            }
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    private Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        float dot = Vector3.Dot(-norm, incident);

        // Check if n2 < n1, and if so, swap the norm and incident direction vectors
        if (n2 < n1)
        {
            norm = -norm;
            float temp = n1;
            n1 = n2;
            n2 = temp;
        }

        float refractiveIndexRatio = n1 / n2;
        float discriminant = 1.0f - refractiveIndexRatio * refractiveIndexRatio * (1 - dot * dot);

        if (discriminant > 0)
        {
            Vector3 refractedDirection = refractiveIndexRatio * incident + (refractiveIndexRatio * dot - Mathf.Sqrt(discriminant)) * norm;

            return refractedDirection.normalized; // Normalize the refracted direction
        }
        else
        {
            // Return the incident direction as the refracted direction when total internal reflection occurs
            return incident.normalized;
        }
    }



    private void CopyValues(LaserBeam referenceLaserBeam)
    {   if (referenceLaserBeam != null)
        {
            //this.laser = referenceLaserBeam.laser;

            this.laser.startWidth = referenceLaserBeam.laser.startWidth;
            this.laser.endWidth = referenceLaserBeam.laser.endWidth;
            this.laser.widthCurve = referenceLaserBeam.laser.widthCurve;

            this.laser.startColor = referenceLaserBeam.laser.startColor;
            this.laser.endColor = referenceLaserBeam.laser.endColor;

            this.laser.numCapVertices = referenceLaserBeam.laser.numCapVertices;
            this.laser.numCornerVertices = referenceLaserBeam.laser.numCornerVertices;

            this.laser.material = referenceLaserBeam.laser.material;

            this.laser.material.EnableKeyword("_EMISSION");
            this.laser.material.SetColor("_EmissionColor", this.laser.startColor);


            this.laser.SetPositions(new Vector3[0]);        // clear indices of line renderer

            this.maxLenght = referenceLaserBeam.maxLenght - referenceLaserBeam.laserIndices.Count;
            //logger.Log("Copied values from: " + referenceLaserBeam);
        }
    }



    public LaserBeam DeepClone()
    {
        LaserBeam newLaserBeam = new LaserBeam(this);
        newLaserBeam.laserIndices = new List<Vector3>();


        return newLaserBeam;
    }
}