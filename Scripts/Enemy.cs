using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public int enemyHP;
    // public float hitTime;

    public float hitForce = 20f;
    public float backTime = 0.05f;
    public ParticleSystem hitPs;
    private Animator anim;
    void Start()
    {
        hitPs.Stop();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // public void HitBack()
    // {
    //     
    // }

    public IEnumerator HitBack()
    {
        // var origSpeed = rb.velocity;
        Vector2 disX = - new Vector2(GameObject.FindWithTag("Player").transform.position.x 
             - transform.position.x,0) ;
        rb.AddForce(disX.normalized * hitForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(backTime);
        rb.velocity = new Vector2(0, 0);
    }

    public void doHit()
    {
        hitPs.Play();
        anim.SetTrigger("hit");
        if (Input.GetAxis("Vertical") > -0.2)
        {
            StartCoroutine(HitBack());
            Debug.Log("hahahah");
        }
        
    }

    public void GetDamaged(int damage)
    {
        enemyHP -= damage;
    }
}
