using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float currentTime;
    private bool isTiming;
    public GameObject countdownPanel;
    public Text countdownText;
    public Text bestTime;
    public Text bestTimeResult;
   
    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetTime()
    {
        return currentTime;
    }
    void Update()
    {
        if (isTiming == true)
       {
            currentTime += Time.deltaTime;
        }
    }

    public IEnumerator StartCountdown(sceneController sceneController)
    {
       
        

        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }

}
