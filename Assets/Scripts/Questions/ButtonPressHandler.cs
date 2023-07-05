using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ButtonPressHandler : MonoBehaviour
{
    public float chargeUpTime = 2f;  // Time required to charge up the button in seconds
    public bool isCharging = false; // Flag to indicate if the button is currently being charged
    public float chargeTimer = 0f;  // Timer to track the button charge progress
    public bool areControllersInside = false;


    private void OnTriggerEnter(Collider other)
    {
        print(other);

        XRController controller = other.GetComponent<XRController>();
        if (controller != null)
        {
            areControllersInside = true;
            // Do something when the controllers enter the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print(other);
        XRController controller = other.GetComponent<XRController>();
        if (controller != null)
        {
            areControllersInside = false;
            // Do something when the controllers exit the trigger
        }
    }

    private void Update()
    {
        if (areControllersInside)
        {
            if (!isCharging)
            {
                // Start charging
                isCharging = true;
                chargeTimer = 0f;
                Debug.Log("Button charging...");
            }

            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeUpTime)
            {
                // Button fully charged
                Debug.Log("Button fully charged!");
                // Add your custom logic here

                // Reset the charging state
                isCharging = false;
                chargeTimer = 0f;
            }
        }
    }
}