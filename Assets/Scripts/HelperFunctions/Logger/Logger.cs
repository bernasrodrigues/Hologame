using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger
{
    private bool isOn;
    
    public Logger(bool isOn)
    {
        this.isOn = isOn;
    }




    public void Log(string message)
    {
        if (isOn)
        {
            Debug.Log(message);
        }
    }

}
