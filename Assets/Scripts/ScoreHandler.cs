using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

	private int displayScore;
	private int tempScore;
	private int totalScore;
	private float t = 0;
	private Text text;

	private void Awake() {
		text = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		// lerp the number here
		t += Time.deltaTime;
		displayScore = (int)Mathf.Lerp(tempScore, totalScore, t);
		text.text = displayScore.ToString();
	}

	private void UpdateScore(int score) {
		tempScore = displayScore;
		totalScore = score;
		t = 0;
	}
}
