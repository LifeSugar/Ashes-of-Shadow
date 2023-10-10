using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButton : MonoBehaviour
{
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _button.enabled = !_button.enabled;
        }
    }
}
