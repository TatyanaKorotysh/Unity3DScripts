using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    private CharacterController controller;

    float vertical;
    float horizontal;
    float mouseHorizontal;
    float MoveSpeed = 7f;
    float TurnSpeed = 70f;
     Vector3 moveDirection;
    
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public AudioSource steps;
    public AudioSource run;

    private Canvas loading;
    private Canvas esc;
    private Timer timer;

    void Start()
    {        
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        loading = GameObject.Find("LoadingScreen").GetComponent<Canvas>();
        loading.enabled = false;
        esc = GameObject.Find("Esc").GetComponent<Canvas>();
        esc.enabled = false;
        timer = GameObject.Find("Canvas").transform.Find("Timer").GetChild(0).GetComponent<Timer>();
    }
    
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        mouseHorizontal = Input.GetAxis("Mouse X");
        
        //--------ОЖИДАНИЕ-----------------
        if (vertical == 0) {
            animator.SetBool("Walking", false);
            animator.SetBool("WalkingBack", false);
            animator.SetBool("Running", false);
            
            run.Stop();
            steps.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (esc.enabled)
            {
                esc.enabled = false;
                timer.StartTimer();
            }
            else
            {
                esc.enabled = true;
                timer.StopTimer();
            }

        }

        Move(vertical);
        Turn(horizontal, mouseHorizontal);
        Animate(vertical);
    }

    private void Move(float vertical)
    {
        //--------ВПЕРЕД-----------------
        if ((vertical > 0) && (Input.GetKeyDown(KeyCode.W)))
        {
            animator.SetBool("Walking", true);
            steps.Play();

            Vector3 movement = new Vector3(0f, -1f, vertical);
            movement = movement * MoveSpeed * Time.deltaTime;
            controller.Move(transform.TransformDirection(movement));
        }
        
        //--------БЕГ-----------------
        if ((vertical > 0) && (Input.GetKeyDown(KeyCode.LeftShift)))
        {
            animator.SetBool("Running", true);
            run.Play();

            Vector3 movement = new Vector3(0f, -1f, vertical);
            movement = movement * MoveSpeed * 3 * Time.deltaTime;
            controller.Move(transform.TransformDirection(movement));
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Running", false);
            run.Stop();
            animator.SetBool("Walking", true);
            steps.Play();
        }

        //--------НАЗАД-----------------
        if ((vertical < 0) && (Input.GetKeyDown(KeyCode.S)))
        {
            animator.SetBool("WalkingBack", true);
            steps.Play();

            Vector3 movement = new Vector3(0f, -1f, vertical);
            movement = movement * MoveSpeed / 2 * Time.deltaTime;
            controller.Move(transform.TransformDirection(movement));
        }
    }

    private void Turn(float horizontal, float mouseHorizontal) {        
        //--------ПОВОРОТЫ-----------------
        if (horizontal != 0)
        {
            transform.Rotate(0f, horizontal * TurnSpeed * Time.deltaTime, 0f);
        }
    }

    private void Animate(float vertical)
    {
        //--------ПОДНЯТИЕ ПРЕДМЕТА-----------------        
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("ObjectUp");
        }
   
    }
}
