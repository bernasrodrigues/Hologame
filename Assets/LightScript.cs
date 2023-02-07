using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool isOn;
    public Light light;



    public GameObject GlassExterior;
    public Material onLampMaterial;
    public Material offLampMaterial;
    private Renderer LampRenderer;



    // Start is called before the first frame update
    void Start()
    {
        LampRenderer = GlassExterior.GetComponent<Renderer>();
    }


    void TurnOn()
    {
        light.enabled = true;

        LampRenderer.material = onLampMaterial;
        isOn = true;
    }


    void TurnOff()
    {
        light.enabled = false;
        LampRenderer.material = offLampMaterial;
        isOn = false;
    }


    public void button()
    {
        if (isOn)
        {
            TurnOff();
        }
        else TurnOn();
    }


}
