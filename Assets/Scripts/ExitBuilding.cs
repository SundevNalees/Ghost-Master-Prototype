using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBuilding : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected");
        if (collision.gameObject.GetComponent<MortalMovemenrt>()!=null)
        {
            Destroy(collision.gameObject);
        }
    }
}
