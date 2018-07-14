using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 7.0f;
    public BoxCollider2D AreaBounds;
    private BoxCollider2D PlayerBounds;
    private string sceneToGoTo = "GameOver";

    private void Start()
    {
        PlayerBounds = GetComponent<BoxCollider2D>();
    }
    void Update()
    {

        gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.Escape))
        {
            sceneToGoTo = "MainMenu";
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(PlayerBounds.size.x, AreaBounds.bounds.min.x, AreaBounds.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, AreaBounds.bounds.min.y, AreaBounds.bounds.max.y);

        transform.position = clampedPosition;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(sceneToGoTo);
    }
}