using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTextureHandler : MonoBehaviour
{
    public Camera mirrorCamera;
    public MeshRenderer meshRenderer;

    // On start create a new render texture and apply it to the mirror materials
    void Start()
    {
        // Create a new render texture with the specified width and height
        RenderTexture renderTexture = new RenderTexture(512, 512, 24);

        // Set the name of the render texture
        //renderTexture.name = "MyRenderTexture";

        // Apply the render texture to a camera's target texture
        mirrorCamera.targetTexture = renderTexture;

        meshRenderer.material.mainTexture = renderTexture;
        //meshRenderer.materials = renderTexture;
    }



    void Update()
    {
        //CalculateCameraRotation();

    }



    void CalculateCameraRotation()
    {
        Vector3 dir = (WorldInfo.Instance.playerCameraTransform.position - this.transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        rot.eulerAngles = this.transform.eulerAngles - rot.eulerAngles;

        mirrorCamera.transform.localRotation = rot;

        //mirrorCamera.transform.RotateAround(transform.position, transform.up, 180f);

    }
}
