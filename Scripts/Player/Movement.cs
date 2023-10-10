using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector] 
    public Rigidbody2D rb;

    private AnimatonScript anim;

    private CapsuleCollider2D caps;

    private PlayerAttack attack;

    public bool isJumpingUp;

    private bool attackBaned;

    private PlayerData _playerData;

    private bool cameraShaking;

    private bool isDead;

    private CinemachineImpulseSource _source;





    [Space] 
    
    [Header("Properties")]
    public float speed = 10;

    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space] 
    public bool canAttack;

    // public bool isAttacking1;
    // public bool isAttacking2;
    // public bool isAttacking3;
    // public bool isAttaking;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Space] public float invincibleTime = 0.9f;


    private void Start()
    {
        

        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimatonScript>();
        attack = GetComponent<PlayerAttack>();


        _source = GetComponent<CinemachineImpulseSource>();
        
        caps = GetComponent<CapsuleCollider2D>();

        _playerData = GetComponentInChildren<PlayerData>();
        
        dashParticle.Stop();




    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxis("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        isJumpingUp = (Input.GetButton("Jump") && rb.velocity.y > 0);

        // if (Input.anyKeyDown)
        // {
        //     Debug.Log(x);
        //     Debug.Log(rb.velocity);
        // }

        if (!_playerData.isHurting && !isDead)
        {

            cameraShaking = false;
            // Camera.main.transform.position = new Vector3(-1.03f, 0, -10);
            
            if (!isDashing)
            {
                Walk(dir);
            }
            anim.SetHorizontalMovement(x, y, rb.velocity.y);

            // if (coll.onWall && Input.GetButton("Fire3") && canMove)
            // {
            //     if(side != coll.wallSide)
            //         anim.Flip(side* -1);
            //     wallGrab = true;
            //     wallSlide = false;
            // }

            if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
            {
                wallGrab = false;
                wallSlide = false;
            }

            if (coll.onGround && !isDashing)
            {
                wallJumped = false;
                GetComponent<BetterJumping>().enabled = true;
            }
            
            if (wallGrab && !isDashing)
            {
                // rb.gravityScale = 0;
                // if(x > .2f || x < -.2f)
                // rb.velocity = new Vector2(rb.velocity.x, 0);
                //
                // float speedModifier = y > 0 ? .5f : 1;
                //
                // rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            }
            else
            {
                rb.gravityScale = 2.5f;
            }

            if(coll.onWall && !coll.onGround)
            {
                hasDashed = false;
                if (x != 0 && !wallGrab )
                {
                    if (!isJumpingUp)
                    {
                        // anim.Flip((side * -1));
                        wallSlide = true;
                        WallSlide();
                    }
                }
            }

            if (!coll.onWall || coll.onGround)
                wallSlide = false;

            if (Input.GetButtonDown("Jump") && !isDashing && !anim.isAttackingDown && !GetComponent<BetterJumping>().isBouncing)
            {
                anim.SetTrigger("jump");
                

                if (coll.onGround)
                    Jump(Vector2.up, false);
                if (coll.onWall && !coll.onGround)
                    WallJump();
            }

            if (Input.GetButtonDown("Fire1") && !hasDashed)
            {
                // if(xRaw != 0 )
                //     Dash();
                Dash();
            }

            if (coll.onGround && !groundTouch)
            {
                GroundTouch();
                groundTouch = true;
                
            }

            if(!coll.onGround && groundTouch)
            {
                groundTouch = false;
            }

            WallParticle(y);

            if (wallGrab || wallSlide || !canMove)
                return;

            if(x > 0 && !wallSlide)
            {
                side = 1;
                anim.Flip(side);
                caps.offset = new Vector2(0.15f, -0.05f);



            }
            if (x < 0 && !wallSlide)
            {
                side = -1;
                anim.Flip(side);
                caps.offset = new Vector2(-0.18f, -0.05f);
            }

            if (Input.GetButtonDown("Fire2") && y > -0.2f && !attackBaned)
            {
                attackBaned = true;
                anim.SetTrigger("attack");
                attack.Attack();
                StartCoroutine(AttackDelay());

            }

            if (Input.GetButtonDown("Fire2") && y < -0.2f && !coll.onGround)
            {
                attackBaned = true;
                anim.SetTrigger("AttackDown");
                attack.Attack();
                StartCoroutine(AttackDelay());
            }

            if ((anim.isAttacking1 && coll.onGround) || anim.isAttacking2 || anim.isAttacking3)
            {
                speed = 2f;
            }
            else
            {
                speed = 10;
            }
                  
            }

        if (_playerData.isHurting)
        {
            if (!cameraShaking)
            {
                // Camera.main.transform.DOShakePosition(0.3f, new Vector3(0.2f, 0.2f, 0));
                _source.GenerateImpulse();
                
                cameraShaking = true;
            }
            if (_playerData.playerHP > 0)
            {
                StartCoroutine(Hurting());

                
            }
            else
            {
                StartCoroutine(Death());
            }
        }

        
        
        
    }

    void GroundTouch()
    {
        hasDashed = false;
        
        // isDashing = false;
        
        // side = anim.sr.flipX ? -1 : 1;
        
        jumpParticle.Play();
        StartCoroutine(EndBounce());
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
            
        }
        
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        
        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
        
        

    }
    
    private void WallSlide()
    {
        if(coll.wallSide != side)
            anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }
    
    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }
    
    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    
    void RigidbodyDrag(float x)
    {
        rb.drag = x;

    }
    
    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }
    
    
    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
    
    private void Dash()
    {
        // Camera.main.transform.DOComplete();
        // Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        // FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        //
        hasDashed = true;
        
        anim.SetTrigger("dash");
        
        // rb.velocity = Vector2.zero;
        // Vector2 dir = new Vector2(x, 0);

        rb.velocity = Vector2.right * side * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
        // hasDashed = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    IEnumerator EndBounce()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BetterJumping>().isBouncing = false;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        attackBaned = false;
    }

    IEnumerator Hurting()
    {
        
        rb.gravityScale = 3;
        rb.velocity = new Vector2(_playerData.dir.normalized.x * 0.5f, -2f);
        yield return new WaitForSeconds(invincibleTime);
        _playerData.isHurting = false;
    }

    IEnumerator Death()
    {
        isDead = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(invincibleTime);
        anim.SetTrigger("Death");

    }
}
