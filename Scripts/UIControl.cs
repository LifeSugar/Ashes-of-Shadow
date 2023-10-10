using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{

    public GameObject player;
    public Transform spawnPos;
    public Transform spawnPos1;
    public GameObject ghost;
    public GameObject heart;

    public GameObject tutorial;
    
    
    public void HideUI()
    {
        GameObject.Find("title").SetActive(false);
        heart.SetActive(true);
        tutorial.SetActive(true);
    }

    public void IntialPlayer()
    {
        Instantiate(player, spawnPos);
        Instantiate(ghost, spawnPos1);
    }
}
