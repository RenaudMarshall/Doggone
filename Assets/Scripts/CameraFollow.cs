using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public BoxCollider2D AreaBounds;
    private Vector3 _min, _max;
    private float x, y;

    private void Start()
    {
        _min = AreaBounds.bounds.min;
        _max = AreaBounds.bounds.max;
    }

    // Update is called once per frame
    void Update () {

        if (player)
        {
            x = player.transform.position.x;
            y = player.transform.position.y;
        }

        float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

        // lock the camera to the right or left bound if we are touching it
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);

        // lock the camera to the top or bottom bound if we are touching it
        y = Mathf.Clamp(y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);

    }
}
