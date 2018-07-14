using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 7.0f;
    private string sceneToGoTo = "GameOver";

    void Update()
    {
        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);

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