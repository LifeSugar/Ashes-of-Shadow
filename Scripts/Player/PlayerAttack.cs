using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AnimatonScript anim;
    private Movement move;
    private Collision coll;

    public Transform attackPosL;
    public Transform attackPosR;
    public Transform attackPosD;
    public LayerMask whatIsEnimies;
    public LayerMask whatIsTheInteroperable;
    // public float attackRangeLR;
    public float attackRangeD;
    public Vector2 attackRangeLR;

    public float bouceForceOnEnemy;
    public float bouceForceOnIterm;

    public float delay;

    public int damage;

    private Collider2D[] enemiesHitL;
    private Collider2D[] itermsHitL;
    
    private Collider2D[] enemiesHitR;
    private Collider2D[] itermsHitR;
    
    private Collider2D[] enemiesHitD;
    private Collider2D[] itermsHitD;
    
    


    private void Start()
    {
        anim = GetComponentInChildren<AnimatonScript>();
        move = GetComponentInChildren<Movement>();
        coll = GetComponent<Collision>();

        
    }

    private void FixedUpdate()
    {
        enemiesHitL = Physics2D.OverlapBoxAll(attackPosL.position,
            attackRangeLR, 0,whatIsEnimies);
        
        itermsHitL = Physics2D.OverlapBoxAll(attackPosL.position,
            attackRangeLR, 0,whatIsTheInteroperable);
        
        enemiesHitR = Physics2D.OverlapBoxAll(attackPosR.position,
            attackRangeLR, 0,whatIsEnimies);
        itermsHitR = Physics2D.OverlapBoxAll(attackPosR.position,
            attackRangeLR, 0,whatIsTheInteroperable);
        
        enemiesHitD = Physics2D.OverlapCircleAll(attackPosD.position,
            attackRangeD, whatIsEnimies);
        itermsHitD = Physics2D.OverlapCircleAll(attackPosD.position,
            attackRangeD, whatIsTheInteroperable);

    }

    public void Attack()
    {
        
        
        if (Input.GetAxis("Vertical") < -0.2f && !coll.onGround)
        {
            if (true)
            {
                if (enemiesHitD.Length != 0)
                {
                    StartCoroutine(HitEnemies(enemiesHitD));
                    GetComponent<BetterJumping>().isBouncing = true;
                    move.rb.velocity = new Vector2(0, bouceForceOnEnemy);
                }
                
                if (itermsHitD.Length != 0)
                {
                    StartCoroutine(HitIterms(itermsHitD));
                    GetComponent<BetterJumping>().isBouncing = true;
                    move.rb.velocity = new Vector2(0, bouceForceOnIterm);
                }
                if ((enemiesHitD.Length != 0 || itermsHitD.Length != 0) && !coll.onGround)
                {
                    Debug.Log("111111111`1");
                    
                }
            }
            
            
            // attackRange = 1.2f;
            // attackPos.localPosition = new Vector3(0f * move.side, -1f, 0);
            // if (true )
            // {
            //     Debug.Log("333");
            //     HitEnemies();
            //     HitIterms();
            //
            //     if ((enemiesHit.Length != 0 || itermsHit.Length != 0)&& !coll.onGround )
            //     {
            //         GetComponent<BetterJumping>().isBouncing = true;
            //         Debug.Log("2222");
            //         move.rb.velocity = new Vector2(0, 20f);
            //         
            //     }
            //
            // }

        }
        else if (move.side == 1)
        {
            if (true)
            {
                if (enemiesHitR.Length != 0)
                {
                    Debug.Log("RightEnemy");
                    StartCoroutine(HitEnemies(enemiesHitR));
                }

                if (itermsHitR.Length != 0)
                {
                    Debug.Log("RightIterm");
                    StartCoroutine(HitIterms(itermsHitR));
                }
            }
            
            // attackRange = 0.5f;
            // attackPos.localPosition = new Vector3(1.65f * move.side, 0, 0);
            // if (anim.GetAnimationNormalizedTime() > 0.2f && anim.GetAnimationNormalizedTime() < 0.8f)
            // {
            //     HitEnemies();
            //     HitIterms();
            // }
        }
        else if (move.side != 1)
        {
            if (true)
            {
                if (enemiesHitL.Length != 0)
                {
                    Debug.Log("LiftEnemy");
                    StartCoroutine(HitEnemies(enemiesHitL));
                }

                if (itermsHitL.Length != 0)
                {
                    Debug.Log("LiftIterm");
                    StartCoroutine(HitIterms(itermsHitL));
                }
            }
            
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(attackPosL.position, attackRangeLR);
        Gizmos.DrawWireCube(attackPosR.position, attackRangeLR);
        Gizmos.DrawWireSphere(attackPosD.position, 1.2f);
    }

    public IEnumerator HitEnemies(Collider2D[] Hithing)
    {
        // Hithing= Physics2D.OverlapCircleAll(attackPos.position,
        //     attackRange, whatIsEnimies);
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < Hithing.Length; i++)
        {
            var hitThen = Hithing[i].GetComponent<Enemy>();
            hitThen.doHit();
            hitThen.GetDamaged(damage);
        }
    }

    public IEnumerator HitIterms(Collider2D[] Hithing)
    {
        yield return new WaitForSeconds(delay);
        // // Hithing = Physics2D.OverlapCircleAll(attackPos.position,
        //     attackRange, whatIsTheInteroperable);
        for (int i = 0; i < Hithing.Length; i++)
        {
            
        }
    }
}
