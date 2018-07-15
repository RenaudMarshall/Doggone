using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int TotalPoints;
    public AudioClip audioStealth;
    public AudioClip audioChase;
    private AudioSource audioClips;
    private Canvas UserInterface;
    private int currentTrack;

    private void Awake() {
        UserInterface = FindObjectOfType<Canvas>();
    }
	// Use this for initialization
	void Start () {
        TotalPoints = 0;
        audioClips = GetComponent<AudioSource>();
        DetectionStatus(0);
        currentTrack = 0;

    }

    public void AddPoints(int add)
    {
        TotalPoints += add;
        Debug.Log(TotalPoints);
        UserInterface.BroadcastMessage("UpdateScore", TotalPoints);
    }

    private void LateUpdate()
    {
        Debug.Log(currentTrack);
    }
    public void DetectionStatus(int data)
    {
        if (currentTrack != data)
            audioClips.Stop();
        if (!audioClips.isPlaying)
        {
            switch (data)
            {
                case 0:
                    audioClips.clip = audioStealth;
                    currentTrack = 0;

                    break;
                case 1:
                    audioClips.clip = audioChase;
                    currentTrack = 1;
                    break;

            }
            audioClips.Play();
        }
    }
}
