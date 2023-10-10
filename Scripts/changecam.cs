using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecam : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cc;
    public GameObject ccc;

    private Camera cam1;
    private Camera cam2;


    private void Start()
    {
        cam1 = cc.GetComponent<Camera>();
        cam2 = ccc.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (cam2.enabled == false)
            {
                cam2.enabled = true;
                cam1.enabled = false;
            }
            else
            {
                cam1.enabled = true;
                cam2.enabled = false;
            }
            
        }
    }
}
