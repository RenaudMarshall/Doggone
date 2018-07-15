using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int TotalPoints;
    private string StealthStatus;
    private Canvas UserInterface;

    private void Awake() {
        UserInterface = FindObjectOfType<Canvas>();
    }
	// Use this for initialization
	void Start () {
        TotalPoints = 0;
        StealthStatus = "Hidden";
	}

    private void Update()
    {
        DetectionStatus();
    }

    public void AddPoints(int add)
    {
        TotalPoints += add;
        Debug.Log(TotalPoints);
        UserInterface.BroadcastMessage("UpdateScore", TotalPoints);
    }

    private void DetectionStatus()
    {
        
    }
}
