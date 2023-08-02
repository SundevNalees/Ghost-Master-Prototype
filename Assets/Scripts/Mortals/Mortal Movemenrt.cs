using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MortalMovemenrt : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float range;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private string targetTag="Ghost";
    [SerializeField] private float attackDistance=5f;

    [SerializeField] private float minFear=10;
    public float maxFear=100;

    
    [SerializeField]private float fearCount;

    public bool isWalking;

    private void OnEnable()
    {
        GhostControl.OnPowerEffectCalled += Scared;
    }

    private void OnDisable()
    {
        GhostControl.OnPowerEffectCalled -= Scared;
    }
    void Start()
    {
        fearCount = minFear;
        isWalking = true;
    }

    
    void Update()
    {
        if (isWalking)
        {
            Movement();
           
        }
        else
        {
            StopMoving();
        }
        
    }

    private void StopMoving()
    {
        //agent.SetDestination(transform.position);
        //agent.isStopped = true;
    }
    private void Movement()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (agent.isStopped == true)
            {
                agent.isStopped = false;
            }
            
            Vector3 point;
            if(RandomPoint(centerPoint.position,range,out point))
            {
                
                Debug.DrawRay(point, Vector3.up, Color.red, 1.0f);
                agent.SetDestination(point);
                
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }

    bool RandomPoint(Vector3 center,float range,out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint,out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            animator.SetBool("IsWalking", true);
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void IncreaseFearCount(int amount)
    {
        fearCount += amount;
    }

    public float GetFearCount()
    {
        return fearCount;
    }
    private void Scared()
    {
        Debug.Log("Ayoo");
        GameObject ghost = GameObject.FindGameObjectWithTag(targetTag);
        if (ghost == null)
        {
            return;
        }
        float distanceToTarget = Vector3.Distance(transform.position, ghost.transform.position);

        if (distanceToTarget < attackDistance)
        {
            agent.SetDestination(transform.position);
            animator.SetTrigger("isScared");
            animator.SetBool("IsWalking", false);

            isWalking = false;
            //agent.isStopped = true;

            if (fearCount < maxFear)
            {
                MortalManager.Instance.FearCountAdd(gameObject);
                MortalManager.Instance.FaintProbability(gameObject);
                
                if (fearCount >= maxFear)
                {
                    MortalManager.Instance.ScaredCheck();
                }
            }
            //animator.SetBool("IsWalking", false);
        }
        
    }

    public void ScareAnimationComplete()
    {
        isWalking = true;
    }
}
