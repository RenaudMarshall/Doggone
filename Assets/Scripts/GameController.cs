using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private int TotalPoints;
	// Use this for initialization
	void Start () {
        TotalPoints = 0;
	}
	
	public void AddPoints(int add)
    {
        TotalPoints += add;
        Debug.Log(TotalPoints);
    }

    private void DetectionStatus()
    {
        // to set the game music
    }
}
