using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour {

    private bool _isSatIn;

    private BoxCollider2D _sittingAtCollider;

    public bool IsSatIn
    {
        get
        {
            return _isSatIn;
        }
        set
        {
            _isSatIn = value;
            _sittingAtCollider.enabled = value;
        }
    }

    private 

	// Use this for initialization
	void Start () {
        _sittingAtCollider = GetComponent<BoxCollider2D>();
        IsSatIn = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
