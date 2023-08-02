using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MortalFaint : MonoBehaviour
{
    [SerializeField] private float faintTime = 4.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    public void FaintCheck(int num,GameObject mortal)
    {
        if (num > 50)
        {
            agent.SetDestination(transform.position);
            MortalMovemenrt mortalMovemenrt = mortal.GetComponent<MortalMovemenrt>();
            //mortalMovemenrt.isWalking = false;
            FaintMortal();
            StartCoroutine(WakeMortal(mortal));
            
        }
    }

    private void FaintMortal()
    {
        
        animator.SetTrigger("isFaint");
    }


    private IEnumerator WakeMortal(GameObject mortal)
    {
        yield return new WaitForSeconds(faintTime);
        MortalMovemenrt mortalMovemenrt = mortal.GetComponent<MortalMovemenrt>();
        mortalMovemenrt.isWalking = true;
        animator.SetTrigger("isWakingUp");
    }
}
