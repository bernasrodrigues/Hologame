using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject itemToSpawn;



    public void Spawn()
    {
        Instantiate(itemToSpawn , this.transform);



    }
}
