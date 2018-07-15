using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour
{
    private GameController gameController;

    public int Points = 10;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("got the thing");
            gameController.AddPoints(Points);
            Destroy(gameObject);
        }
    }
}
