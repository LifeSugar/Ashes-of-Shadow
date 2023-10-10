using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCam : MonoBehaviour
{
    public GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("cao");
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("cnmd");
            virtualCam.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}
