using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoalInfo : MonoBehaviour
{
    public List<GoalObject> GoalArray = new List<GoalObject>();

    public InputActionReference AbuttonReference;

    public bool debug = true;


    public void Update()
    {
        
    }

    public void Start()
    {
        AbuttonReference.action.performed += OnAButtonPressed;
    }



    private bool checkIfCanProced()
    {
        foreach (GoalObject g in GoalArray)
        {
            if (!(g.timeCount == g.timeTillFull))
                return false;
        }
        return true;
    }


    private void OnAButtonPressed(InputAction.CallbackContext obj)
    {
        if (checkIfCanProced() || debug)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       }


    }
}
