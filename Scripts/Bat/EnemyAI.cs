using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    public float speed = 220f;
    public float nextWayPointDistance = 3f;
    public Transform enemyGFX;
    public float attackIterval = 7f;
    public float attackspeed = 200f;
    public float attackDis = 3f;

    public GameObject alert;

    public float detectArea = 20f;

    public GameObject demageTrigger;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker _seeker;
    private Rigidbody2D rb;
    private BatAnimation _batAnimation;

    private Vector3 dis;

    private bool attackReady = true;

    private Enemy _enemy;
    
    
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        _batAnimation = GetComponentInChildren<BatAnimation>();

        // InvokeRepeating("UpdatePath", 0f,0.5f);
        demageTrigger.GetComponent<CircleCollider2D>().enabled = false;

        GetComponent<TrailRenderer>().enabled = false;
        _enemy = GetComponent<Enemy>();

        target = GameObject.FindWithTag("Player").transform;

    }

    void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(rb.position, target.position + 2 * Vector3.up + Vector3.right * Input.GetAxisRaw("Horizontal"), OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    
    void FixedUpdate()
    {
        
        if (_enemy.enemyHP > 0)
        {
            dis = transform.position - (target.position);
            if (dis.magnitude < detectArea)
            {
                Invoke("UpdatePath",0.5f);
            }


            if (path == null)
            {
                // Debug.Log("no path!");
                return;
            }
                
                    
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
                Debug.Log("on the path!");
                Debug.Log((Vector2)path.vectorPath[currentWaypoint]);
            }
            
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint+1] - (Vector2)path.vectorPath[currentWaypoint]).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);
                    
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            
            if (distance < nextWayPointDistance)
            {
                currentWaypoint++;
            }
            
            if (force.x >= 0.01f)
            {
                demageTrigger.transform.localPosition = new Vector3(0.18f, 0, 0);
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (force.x <= -0.01f)
            {
                demageTrigger.transform.localPosition = new Vector3(-0.18f, 0, 0);
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }

            
            
            if (dis.magnitude < attackDis && attackReady)
            {
                // Debug.Log("Bite!");
                StartCoroutine(Bite());
            }
        }
            
        if (_enemy.enemyHP <= 0)
        {
            GetComponent<TrailRenderer>().enabled = false;
            demageTrigger.GetComponent<Collider2D>().enabled = false;
            _batAnimation.SetTrigger("dead");
            rb.gravityScale = 3;
            StartCoroutine(Exit());
        }

    }

    private IEnumerator Bite()
    {
        attackReady = false;
        alert.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        alert.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        alert.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        alert.GetComponent<SpriteRenderer>().enabled = false;
        
        
        GetComponent<TrailRenderer>().enabled = true;
        rb.velocity =new Vector2(rb.velocity.x + dis.normalized.x * attackspeed, rb.velocity.y + dis.normalized.y * attackspeed);
        _batAnimation.SetTrigger("isBiting");
        demageTrigger.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<TrailRenderer>().enabled = false;
        demageTrigger.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(attackIterval);
        attackReady = true;
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(1.2f);
        GameObject.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, detectArea);
    }

    // private IEnumerator DoBlink()
    // {
    //     for (int i = 0; i < 4; i++)
    //     {
    //         alert.GetComponent<SpriteRenderer>().enabled = !alert.GetComponent<SpriteRenderer>().enabled;
    //         yield return new WaitForSeconds(0.1f);
    //     }
    //     alert.GetComponent<SpriteRenderer>().enabled = false;
    // }
}
