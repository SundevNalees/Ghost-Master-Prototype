
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class MortalEscape : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform mortalTransform;
    [SerializeField] private Transform exitTransform;
    [SerializeField] private float runSpeed=20.0f;

    private bool escape;
    private Vector3 target;

    void Start()
    {
        escape = false;
        
    }

    
    void Update()
    {
        if (escape)
        {
            EscapeFromBuilding();
        }
        
    }

    public void Escape()
    {
        escape = true;
    }
    private void EscapeFromBuilding()
    {
        if (agent.destination!= exitTransform.position)
        {
            agent.SetDestination(exitTransform.position);
            target = exitTransform.position;
            agent.isStopped = false;
            agent.speed = runSpeed;
            animator.SetBool("isScaredRun", true);
            Debug.Log("des:" + agent.destination + "..target:" + exitTransform.position);

            //escape = false;
            
        }
        
    }

    
    
}
