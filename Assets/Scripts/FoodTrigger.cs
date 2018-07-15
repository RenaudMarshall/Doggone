using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour
{
    private static GameController gameController;

    public int Points = 10;

    private void Start()
    {
        if(!gameController)
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("got the thing");
            gameController.AddPoints(Points);
            gameController.PlayPickup();
            Destroy(gameObject);
        }
    }
}
