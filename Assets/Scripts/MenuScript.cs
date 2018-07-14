using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public string sceneToGoTo;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey == true || Input.GetMouseButton(0) == true)
        {
            SceneManager.LoadScene(sceneToGoTo);
        }
    }
}
