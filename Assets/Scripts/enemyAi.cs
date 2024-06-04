using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public Animator anim;
    public AudioSource crawl;
    public AudioClip clip;
    public AudioSource scream;

    bool soundcool = false;
    bool walkSound = false;
    public void Start()
    {
       NavMeshAgent nav = GetComponent<NavMeshAgent>();
        
    }


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            

        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            
        }
    }
    private void Patroling()
    {
        agent.speed = 8;
        if (!walkPointSet) SearchWalkPoint();
        {
            agent.SetDestination(walkPoint);
        }
       // anim.SetBool("Run", true);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
        }
        

    }
    private void SearchWalkPoint()
    {
        soundcool = false;
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
      //  anim.SetBool("Run", false);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 1f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("Attack", false);
        agent.speed = 10;
        if (soundcool == false)
        {
            scream.Play();
            soundcool = true;
        }


    }

    private void AttackPlayer()
    {
        soundcool = false;
        //Attack code goes here
       anim.SetBool("Attack", true);
        
        
        agent.SetDestination(transform.position);

        transform.LookAt(player);

       
    }
   

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 2);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }

    public void Crawling()
    {
        crawl.Play();
        
    }
    
}
