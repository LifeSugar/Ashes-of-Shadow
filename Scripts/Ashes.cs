using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ashes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
    }

    
}
