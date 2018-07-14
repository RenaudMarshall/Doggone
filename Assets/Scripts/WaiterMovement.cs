using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterMovement : MonoBehaviour
{

    public GameObject target;
    public float speed = 4.0f;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You got caught");
            target = null;
            Destroy(other.gameObject);
        }
    }
}
