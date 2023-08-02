using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private LayerMask groundLayer;

    
    private bool spawn = false;
    private int count = 0;

    private GameObject ghost;
    private void Start()
    {
        count = 0;
        
    }
    void Update()
    {
        
        
            SpawnPlayer();
        
        
    }

    private void SpawnPlayer()
    {
        if (Input.GetMouseButtonDown(0)&&spawn)
        {
            
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
             {
                Vector3 spawnPosition = hit.point;

                ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
                spawn = false;
                count++;
                
             }
            
                   
            
               
        }
    }

    public void SpawnButton()
    {
        if (count <= 0)
        {
            spawn = true;
        }
        
    }

    public void Bench()
    {
        count = 0;
        Destroy(ghost);
    }
}
