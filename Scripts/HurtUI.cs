using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HurtUI : MonoBehaviour
{
    private PlayerData playerdata;

    private float minor = 0;
    bool getMax = false;
    

    public Image red;

    

    private void Start()
    {
        red.color = new Color(255, 255, 255, 0);
        playerdata = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
        
    }

    void Update()
    {
        if (playerdata.playerHP > 1)
        {
            Show();
        }

        if (playerdata.playerHP == 1)
        {
            Debug.Log("shining");
            Shining();
        }
    }

    private void Show()
    {
        if (playerdata.isHurting)
        {
            
            minor += 0.6f * Time.deltaTime;
            red.color = new Color(255, 255, 255, (0.3f - minor));
            Debug.Log("red");
            
            // red.color = new Color(255, 255, 255, 0f);
            
        }

        if (!playerdata.isHurting)
        {
            minor = 0;
            red.color = new Color(255, 255, 255, 0);
        }
    }

    private void Shining()
    {
        
        

        if (minor > 0.29f)
        {
            getMax = true;
            
        }

        if (minor < 0.01f)
        {
            
            getMax = false;
        }

        if (!getMax)
        {
            minor += 0.3f * Time.deltaTime;
        }

        if (getMax)
        {
            minor -=  0.3f * Time.deltaTime;
        }
        
        red.color = new Color(255, 255, 255, minor);

    }
}
