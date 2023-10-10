using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoutAI : MonoBehaviour
{
    private Animator _animator;
    private Detect _detect;
    private RaycastHit2D _raycastHit2D;
    private Enemy _enemy;
    private Rigidbody2D rb;
    private bool attackReady = true;
    private Collider2D coll;

    public GameObject damgerTrigger;
    

    private bool isHit;
    private bool isAttacking;
    
    

    public LayerMask _LayerMask;
    public Transform player;
    public Transform ghoulGFX;
    public float attackInterval = 3f;
    public GameObject alert;
    public float movespeed = 2;

    public int side = 1;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _detect = GetComponentInChildren<Detect>();
        _enemy = GetComponent<Enemy>();
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        
    }

    private void Update()
    {
        if (_detect.isAwake)
        {
            Vector2 dir = new Vector2(side, 0);
            
            _raycastHit2D = Physics2D.Raycast(transform.position, dir, 200, _LayerMask);
            Debug.DrawRay(transform.position, dir, Color.cyan);
            if (_enemy.enemyHP > 0 && !isAttacking)
            {
                
                // rb.velocity = new Vector2(side * movespeed, 0);
                if (_raycastHit2D.collider.tag == "Player" )
                {
                    if ((player.position - transform.position).magnitude < 2.7f && attackReady)
                    {
                        _animator.SetBool("isWalking", false);
                        StartCoroutine(Attack());
                    }
                    else
                    {
                        _animator.SetBool("isWalking", true);
                        rb.velocity = new Vector2(side * movespeed, 0);
                    }
                    
                }
                else
                {
                    rb.velocity = new Vector2(side * movespeed, 0);
                    _animator.SetBool("isWalking", true);
                    if (_raycastHit2D.distance < 0.2f)
                    {
                        
                        Debug.Log("wall!" + _raycastHit2D.collider.name);
                        TurnAround();
                    }
                }
                
            }

            if (_enemy.enemyHP <= 0)
            {
                StartCoroutine(Die());
            }
        }
        
        
        
    }

    private IEnumerator Attack()
    {
        
        rb.velocity = new Vector2(0, 0);
        isAttacking = true;
        
        attackReady = false;
        alert.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        alert.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        alert.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        alert.GetComponent<SpriteRenderer>().enabled = false;


        
        _animator.SetTrigger("attack");
        yield return new WaitForSeconds(1.2f);
        isAttacking = false;
        yield return new WaitForSeconds(attackInterval);
        attackReady = true;
    }

    public void TurnAround()
    {
        
        ghoulGFX.localScale =new Vector3(-ghoulGFX.localScale.x, 1, 1) ;
        side = -side;
    }

    private IEnumerator Die()
    {
        _animator.SetBool("isDead", true);
        _enemy.enabled = false;
        damgerTrigger.GetComponent<CapsuleCollider2D>().enabled = false;
        
        yield return new WaitForSeconds(1.2f);
        GameObject.Destroy(gameObject);
    }


}
