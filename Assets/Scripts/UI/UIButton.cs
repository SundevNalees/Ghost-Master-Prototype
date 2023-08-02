using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private float attackCoolDown = 3f;
    private bool isCooldownActive = false;

    public delegate void AttackFunction();

    public static event AttackFunction OnAttackFunctionCalled;


    private IEnumerator StartCoolDown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(attackCoolDown);

        isCooldownActive = false;
    }

    public void Attack()
    {
        if (isCooldownActive == false)
        {
            if (OnAttackFunctionCalled != null)
            {
                OnAttackFunctionCalled();
                StartCoroutine(StartCoolDown());
            }
        }
        
        
    }
}
