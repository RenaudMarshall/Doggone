using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("got the thing");
			Destroy(gameObject);
		}	
	}
}
