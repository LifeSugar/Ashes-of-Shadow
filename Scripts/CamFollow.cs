using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class CamFollow : MonoBehaviour
{
    public Transform playerPos;

    private void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = playerPos;
        if (playerPos.tag != "Player")
        {
            playerPos = GameObject.FindWithTag("Player").transform;
        }
        
    }

    private void Update()
    {
        if (playerPos == null)
        {
            GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
            
        }
    }
}
