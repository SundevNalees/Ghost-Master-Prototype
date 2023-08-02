using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private GameObject particleEffect;


    private Transform centerPoint;
    private float angle = 0f;

    private ParticleSystem particleSystem;

    public delegate void PowerEffect();
    public static event PowerEffect OnPowerEffectCalled;

    private void OnEnable()
    {
        UIButton.OnAttackFunctionCalled += Attack;      
    }

    private void OnDisable()
    {
        UIButton.OnAttackFunctionCalled -= Attack;
    }

    void Start()
    {
        particleSystem = particleEffect.GetComponent<ParticleSystem>();
        centerPoint = new GameObject("CenterPoint").transform;
        centerPoint.position = transform.position;
        particleEffect.SetActive(false);
        
    }

    
    void Update()
    {
        centerPoint.position = transform.position;

       
         float x = centerPoint.position.x + radius * Mathf.Cos(angle);
         float z = centerPoint.position.z + radius * Mathf.Sin(angle);

         transform.position = new Vector3(x,transform.position.y, z);

         angle += speed * Time.deltaTime;
         if (angle >= 360f)
         {
             angle -= 360f;
         }
    }

    private void Attack()
    {
        if (particleEffect.activeInHierarchy == true)
        {
            particleSystem.Play();
            if (OnPowerEffectCalled != null)
            {
                OnPowerEffectCalled();
            }

        }
        else
        {
            particleEffect.SetActive(true);
            if (OnPowerEffectCalled != null)
            {
                OnPowerEffectCalled();
            }
        }
        
    }
}
