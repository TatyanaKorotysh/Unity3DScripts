using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text;
    private DateTime timer;
    private bool t;
    public AudioSource clock;
    public AudioSource lose;

    IEnumerator coroutine;

    public void Start()
    {
        timer = new DateTime(1, 1, 1, 0, 10, 0); // задаем стартовое время таймера
        coroutine = Timerenumerator();
    }

    public void StartTimer() {
        t = true;
        StartCoroutine(coroutine);
    }

    public void StopTimer() {
        t = false;
        StopCoroutine(coroutine);
    }

    public IEnumerator Timerenumerator() 
    {
        while (t)
        {
            text.text = timer.Minute.ToString() + " : " + timer.Second.ToString();            

            if (timer.Second == 30 && timer.Minute == 0)
            {
                clock.Play();
                text.color = new Color(1, 0, 0); 
            }

            if (timer.Second == 0 && timer.Minute == 0) 
            {
                text.text = "00 : 00"; 
                clock.Stop();
                lose.Play(); MainManager.game.LoseGame();
                break; 
            }

            timer = timer.AddSeconds(-1); 

            yield return new WaitForSeconds(1); 
        }
    }

}
