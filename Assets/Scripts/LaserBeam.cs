/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;
    public GameObject laserObj;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();
    

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject
        {
            name = "Laser Beam"
        };


        this.pos = pos;
        this.dir = dir;

        // line renderer atributes
        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;
        this.laser.numCornerVertices = 6;

        CastRay(pos, dir, laser);

    }




    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);


        Ray ray = new Ray(pos, dir);
        RaycastHit hit;


        if (Physics.Raycast(ray , out hit , 300 , 1))       // get laser hit
        {

            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(300));            // if no target hit, add point at the end of the laser
            UpdateLaser();
        }

        UpdateLaser();

    }


    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }









    void CheckHit(RaycastHit hitInfo , Vector3 direction , LineRenderer laser)
    {
        BaseObject obj = hitInfo.collider.transform.GetComponent(typeof(BaseObject)) as BaseObject;       // Check if it hit a material that it can interact with
        if (obj != null)
        {
            if (obj.reflective == ReflectiveType.Reflective)    // Check material types
            {
                Vector3 pos = hitInfo.point;
                Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);       // calculate reflection angle

                CastRay(pos, dir, laser);
            }
            else if (obj.reflective == ReflectiveType.Refractive)
            {
                Vector3 pos = hitInfo.point;
                laserIndices.Add(pos);

                Vector3 newPos1 = new Vector3(
                    Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.0001f + pos.x, 
                    Mathf.Abs(direction.y) / (direction.y + 0.0001f) * 0.0001f + pos.y, 
                    Mathf.Abs(direction.z) / (direction.z + 0.0001f) * 0.0001f + pos.z) ;       // Calculate new point close to hitpoint


                // Get values for refraction
                float material_1 = WorldInfo.Instance.refraction_index;
                float material_2 = obj.refractionIndex;

                Vector3 norm = hitInfo.normal;
                Vector3 incident = direction;
                Vector3 refractedVector = Refract(material_1 , material_2 , norm , incident);       // Calculate refracted vector


                // Have to create new ray because they ray only collides with collider once (and wouldn't refract when leaving the object)
                Ray ray1 = new Ray(newPos1, refractedVector);
                Vector3 newRayStartPos = ray1.GetPoint(1.5f);


                Ray ray2 = new Ray(newRayStartPos, -refractedVector);
                RaycastHit hit2; 

                if(Physics.Raycast(ray2 , out hit2 , 1.6f , 1))
                {
                    laserIndices.Add(hit2.point);
                }

                UpdateLaser();

                Vector3 refractedVector2 = Refract(material_2, material_1, -hit2.normal, refractedVector);
                CastRay(hit2.point,refractedVector2,laser);
                //CastRay(newPos1, refractedVector, laser);
            }



            else if (obj.reflective == ReflectiveType.nonReflective)        // For special objects (beam expader, splitter ...)
            {
                laserIndices.Add(hitInfo.point);
                obj.HandleTouchLaser(this);
                UpdateLaser();
            }
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }


    // Snell's law  
    Vector3 Refract (float material_1 , float material_2 , Vector3 normal , Vector3 incident)
    {
        incident.Normalize();
        // normal is already normalized

        Vector3 refractedVector = (
            material_1 / material_2 * Vector3.Cross(normal, Vector3.Cross(-normal, incident)) 
            - normal * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(normal, incident) 
            * (material_1 / material_2 * material_1 / material_2), 
            Vector3.Cross(normal, incident)))).normalized;

        return refractedVector;
    }
}   
*/


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



    // TODO REMOVE THIS AND UPDATE TO NEW CONSTRUCTOR WITH MORE PARAMETERS
    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.pos = pos;
        this.dir = dir;

        // Create Laser Beam object
        this.laserObj = new GameObject("Laser Beam");
        this.laser = this.laserObj.AddComponent<LineRenderer>();

        // Set Line Renderer attributes
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        this.laser.numCapVertices = 6;
        this.laser.numCornerVertices = 6;

        CastRay(pos, dir, laser);
    }




    public LaserBeam(Vector3 pos, Vector3 dir, Material material, LineRenderer ln = null, int maxLenght = 100)
    {
        this.pos = pos;
        this.dir = dir;

        // Create Laser Beam object
        this.laserObj = new GameObject("Laser Beam");
        this.laser = this.laserObj.AddComponent<LineRenderer>();

        this.laser.material = material;



        if (ln != null)
        {
            this.laser.startWidth = ln.startWidth;
            this.laser.endWidth = ln.endWidth;
            this.laser.widthCurve = ln.widthCurve;

            this.laser.startColor = ln.startColor;
            this.laser.endColor = ln.endColor;


            this.laser.numCapVertices = ln.numCapVertices;
            this.laser.numCornerVertices = ln.numCornerVertices;

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

        this.maxLenght = maxLenght;


        CastRay(pos, dir, laser);
    }



    private void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {

        if (laserIndices.Count < maxLenght)           // check if number of positions is lower than allowed number of positions
        {
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
        }
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
}