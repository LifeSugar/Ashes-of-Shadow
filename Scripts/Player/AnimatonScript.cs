using System;
using System.Collections;
using System.Collections.Generic;
using Edgar.Unity.Examples.CurrentRoomDetection;
using UnityEngine;

public class AnimatonScript : MonoBehaviour
{


    private Animator anim;
    private Movement move;
    private Collision coll;
    private PlayerData data;
    [HideInInspector] 
    public SpriteRenderer sr;

    public float normalizedTime;

    public bool isAttacking1;
    public bool isAttacking2;
    public bool isAttacking3;
    public bool isAttackingDown;
    public bool isAttacking;




    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
        data = GetComponentInParent<PlayerData>();


    }

    private void Update()
    {
        isAttacking1 = GetAnimationInfo("Attack1");
        isAttacking2 = GetAnimationInfo("Attack2");
        isAttacking3 = GetAnimationInfo("Attack3");
        isAttackingDown = GetAnimationInfo("AttackDown");

        if (isAttacking1 || isAttacking2 || isAttacking3 || isAttackingDown)
        {
            isAttacking = true;
        }
        normalizedTime = GetAnimationNormalizedTime();
        
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);
        anim.SetBool("onRightWall", coll.onRightWall);
        anim.SetBool("wallGrab", move.wallGrab);
        anim.SetBool("wallSlide", move.wallSlide);
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isDashing", move.isDashing);
        anim.SetBool("isHurting", data.isHurting);
        
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);
        
    }
    
    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (move.wallGrab || move.wallSlide)
        {
            if (side == -1 && sr.flipX)
                return;

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }
        
        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }

    public bool GetAnimationInfo(string animName)
    {
        var currentState = anim.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animName);
    }

    public float GetAnimationNormalizedTime()
    {
        var currentState = anim.GetCurrentAnimatorStateInfo(0);
        return currentState.normalizedTime;
    }

    
}
