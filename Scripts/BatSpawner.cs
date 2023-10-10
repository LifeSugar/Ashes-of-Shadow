using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatSpawner : MonoBehaviour
{
    public GameObject whatIsBat;

    // private Vector3 pos = new Vector3(Random.Range(-11f, 11f), 9f, 0f);
    // private Quaternion rot = new Quaternion(0, 0, 0, 0);
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Instantiate(whatIsBat);
        }
    }
}
