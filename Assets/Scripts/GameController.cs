using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private int TotalPoints;
    public AudioClip audioStealth;
    public AudioClip audioChase;
    public AudioClip audioPitterPatter;
    public AudioClip audioPickup;
    private AudioSource audioClips;
    private AudioSource pitterSource;
    private Canvas UserInterface;
    private int currentTrack;

    public Image blackout;

    public bool IsGameOver = false;

    public int detectors = 0;
    private int pDetectors = 0;

    private void Awake() {
        UserInterface = FindObjectOfType<Canvas>();
    }
	// Use this for initialization
	void Start () {
        TotalPoints = 0;
        AudioSource[] srcs = GetComponents<AudioSource>();
        audioClips = srcs[0];
        pitterSource = srcs[1];
        //DetectionStatus(0);
        currentTrack = 0;
        pitterSource.clip = audioPitterPatter;
    }

    public void AddPoints(int add)
    {
        TotalPoints += add;
        Debug.Log(TotalPoints);
        UserInterface.BroadcastMessage("UpdateScore", TotalPoints);
    }


    public void GameOver()
    {
        IsGameOver = true;
    }

    public void PlayPickup()
    {
        audioClips.PlayOneShot(audioPickup);
    }

    public void PlayPitterPatter(bool play)
    {
        if (play && !pitterSource.isPlaying)
            pitterSource.Play();
        else
            pitterSource.Stop();
    }

    private void LateUpdate()
    {
        if (IsGameOver)
        {
            if(blackout.color.a < 1)
            {
                blackout.color = new Color(0,0,0, blackout.color.a + Time.deltaTime / 3);
            }else
            SceneManager.LoadScene("GameOver");
        }

        DetectionStatus();
       // Debug.Log(currentTrack);
    }
    public void DetectionStatus()
    {
        if (pDetectors == 0 && detectors != 0 || pDetectors != 0 && detectors == 0)
            audioClips.Stop();
        if (!audioClips.isPlaying)
        {
            if(detectors > 0)
            {

                audioClips.clip = audioChase;
                currentTrack = 1;
            }
            else
            {

                audioClips.clip = audioStealth;
                currentTrack = 0;
            }
            audioClips.Play();
        }
        pDetectors = detectors;
    }
}
