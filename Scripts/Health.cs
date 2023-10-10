using System;
using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public GameObject player;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        
        numOfHearts = player.GetComponent<PlayerData>().maxHP;
    }

    private void Update()
    {
        health = player.GetComponent<PlayerData>().playerHP;
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
    }

    public void SetPlayer()
    {
        player = GameObject.FindWithTag("Player");
    }
}
