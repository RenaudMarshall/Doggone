using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 7.0f;
    private string sceneToGoTo = "GameOver";

<<<<<<< HEAD
    void Update()
    {
        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
=======
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
	}
>>>>>>> 7e6197653a4038b931a5784b4ab4470d92f84887

        if (Input.GetKey(KeyCode.Escape))
        {
            sceneToGoTo = "MainMenu";
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(sceneToGoTo);
    }
}