using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public bool isAwake;


    public Animator anim;

    private bool spawned;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ! spawned)
        {
            StartCoroutine(WakeUP());

        }
    }

    private IEnumerator WakeUP()
    {
        spawned = true;
        anim.SetTrigger("spawn");
        anim.SetBool("isAwake",true);
        yield return new WaitForSeconds(2f);
        isAwake = true;
        
    }
}
