using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Coroutine end; // ссылка на запущенную корутину, чтобы не проиграть после выигрыша
    private Canvas loseWindow;
    private Canvas canva;
    private Canvas esc;
    private Timer timer;

    private PlayerMove player;
    private Animator animator;

    public void Start()
    {
        loseWindow = GameObject.Find("LoseWindow").GetComponent<Canvas>();
        canva = GameObject.Find("Canvas").GetComponent<Canvas>();
        timer = GameObject.Find("Canvas").transform.Find("Timer").GetChild(0).GetComponent<Timer>();
        esc = GameObject.Find("Esc").GetComponent<Canvas>();
    }

    public void LoseGame() // в случае проигрыша
    {
        if (end == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            player.enabled = false;
            animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            animator.SetBool("Walking", false);
            animator.SetBool("WalkingBack", false);
            animator.SetBool("Running", false);
            player.run.Stop();
            player.steps.Stop();
            loseWindow.enabled = true;
            canva.enabled = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ReturnPlay()
    {
        esc.enabled = false;
        timer.StartTimer();
    }
}
