using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public GameObject lightInterior;
    public GameObject lightLamp;

    private Material lightInteriorMaterial;
    private Material lightLampMaterial;

    public Color emissionColor;
    public float emissionIntensity = 1.0f;

    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = lightInterior.GetComponent<MeshRenderer>();
        lightInteriorMaterial = renderer.material;

        renderer = lightLamp.GetComponent<MeshRenderer>();
        lightLampMaterial = renderer.material;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void TurnOn()
    {
        light.intensity = 1.5f;

        Material onlightInteriorMaterial = lightInteriorMaterial;
        Material onlightLampMaterial = lightLampMaterial;





    }


    void TurnOff()
    {
        light.intensity = 0f;



    }



}
