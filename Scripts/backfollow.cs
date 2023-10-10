using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backfollow : MonoBehaviour
{
    
    void Update()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
    }
}
