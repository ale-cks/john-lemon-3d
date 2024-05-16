using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public float timeValue = 120;
    public bool timerIsRunning;
    public CanvasGroup timerBackgroundImageCanvasGroup;

    [SerializeField]
    public TextMeshProUGUI timedText;

    //ending components
    public AudioSource caughtAudio;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    float m_Timer;

    void Start()
    {
        timerIsRunning = true;
    }


    // Update is called once per frame
    void Update()
    { if (timerIsRunning)
        {
            if (timeValue > 1)
            {
                timeValue -= Time.deltaTime;
                DisplayTime(timeValue);
            }
            else
            {
               Debug.Log("Time has run out!");
                timeValue = 0;
                timerIsRunning=false;

                EndLevel(timerBackgroundImageCanvasGroup, true);
            }
        }
    }
    void DisplayTime (float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timedText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}

