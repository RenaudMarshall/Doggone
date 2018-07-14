using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterSight : MonoBehaviour
{

    WaiterMovement waiterMovement;

    private void Awake()
    {
        waiterMovement = GetComponentInParent<WaiterMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You were seen");
            waiterMovement.target = other.gameObject;
        }
    }
}
