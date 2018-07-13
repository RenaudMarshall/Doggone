using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed = 7.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
	}

	/*private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "ThingToGet") {
			Debug.Log("got the thing");
		}	
	}*/
}