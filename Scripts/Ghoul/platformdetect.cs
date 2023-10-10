using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformdetect : MonoBehaviour
{
    private GhoutAI _ghoutAI;

    private void Start()
    {
        _ghoutAI = GetComponentInParent<GhoutAI>();
    }

    private void Update()
    {
        transform.localPosition = new Vector3(_ghoutAI.side * 1.315f, -0.856f, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("dfkjdkfjakdsfjklds");
        _ghoutAI.TurnAround();
    }
}
