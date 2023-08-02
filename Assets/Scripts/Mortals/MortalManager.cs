using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalManager : MonoBehaviour
{
    public static MortalManager Instance;

    [SerializeField] private List<GameObject> mortals=new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }


    void Update()
    {

    }

    public void ScaredCheck()
    {
        
        for (int i = 0; i < mortals.Count; i++)
        {
            
            GameObject mortal = mortals[i];
            if (mortal!=null)
            {
                MortalEscape mortalEscape = mortal.GetComponent<MortalEscape>();
                MortalMovemenrt mortalMovemenrt = mortal.GetComponent<MortalMovemenrt>();
                if (mortalMovemenrt != null)
                {
                    if (mortalMovemenrt.GetFearCount() >= mortalMovemenrt.maxFear)
                    {
                        if (mortalEscape != null)
                        {
                            mortalEscape.Escape();
                        }
                    }
                }
            }
           
            
        }
    }

    public void FearCountAdd(GameObject mortal)
    {
        MortalMovemenrt mortalMovemenrt = mortal.GetComponent<MortalMovemenrt>();
        if (mortalMovemenrt != null)
        {
            mortalMovemenrt.IncreaseFearCount(20);
        }
    }

    public void FaintProbability(GameObject mortal)
    {
        MortalFaint mortalFaint = mortal.GetComponent<MortalFaint>();
        if (mortalFaint != null)
        {
            int randomNum = Random.Range(1, 101);
            mortalFaint.FaintCheck(randomNum,mortal);
        }
    }
}

public enum MortalState
{
    Idle,
    Scared,
    Escape
}