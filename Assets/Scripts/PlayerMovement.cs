using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 7.0f;
    public BoxCollider2D AreaBounds;
    private string sceneToGoTo = "GameOver";
    private static GameController gameController;

    private void Start()
    {

        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (!gameController.IsGameOver)
        {
            Vector3 vect = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);

            if (vect.magnitude > 0)
            {
                gameObject.transform.position += vect;
                gameController.PlayPitterPatter(true);
            }
            else
            {
                gameController.PlayPitterPatter(false);
            }
        }
        else
        {
            gameController.PlayPitterPatter(false);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            sceneToGoTo = "MainMenu";
            Defeat();
        }
    }

    private void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, AreaBounds.bounds.min.x + GetComponent<BoxCollider2D>().size.x, AreaBounds.bounds.max.x - GetComponent<BoxCollider2D>().size.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, AreaBounds.bounds.min.y + GetComponent<BoxCollider2D>().size.y, AreaBounds.bounds.max.y - GetComponent<BoxCollider2D>().size.y);

        transform.position = clampedPosition;
    }

    public void Defeat()
    {
        SceneManager.LoadScene(sceneToGoTo);
    }
}