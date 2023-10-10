using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showbackgroud : MonoBehaviour
{
    private SpriteRenderer bg;

    public GameObject button;

    private void Start()
    {
        bg = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GetComponentInParent<PlayerData>().playerHP <= 0)
        {
            bg.enabled = true;
            button.SetActive(true);
        }
    }
}
