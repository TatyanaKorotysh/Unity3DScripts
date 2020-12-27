using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class JoinAnimation : MonoBehaviour
    {
        private Animator dooranimator;
        private Transform target;//ссылка на точку для начала анимации

    private Tasks tasks;

        Quaternion newrot;   
        Animator anim;
        bool secondturn = false;
        States state;  
        enum States
        {
            wait,
            turn, 
            walk
        }

    private GameObject portal;

        void Start()
        {
            anim = GetComponent<Animator>();
            state = States.wait; 
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            portal = GameObject.Find("Plane_House");
            portal.SetActive(false);
        }
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
        }

        void Update()
        {
        if (SceneManager.GetActiveScene().buildIndex == 2 && tasks.Task4)
        {
            if (Input.GetKeyDown(KeyCode.Space)) GoToPoint();
            switch (state)
            {
                case States.turn://при повороте к точке
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation, newrot, Time.deltaTime * 2);//интерполируем между начальным поворотом и требуемым
                        if (Mathf.Abs(Mathf.Round(newrot.y * 100)) == Mathf.Abs(Mathf.Round(transform.rotation.y * 100)))//проверяем когда персонаж повернулся
                        {
                            transform.rotation = newrot;//для избежания погрешности
                            if (!secondturn)
                            {
                                state = States.walk;
                                anim.SetBool("Walking", true);      
                            }
                            else
                            {
                                portal.SetActive(true);
                                dooranimator.SetTrigger("openDoor");
                                anim.SetTrigger("OpenDoor");
                                secondturn = !secondturn;
                                state = States.wait;
                            }
                        }
                        break;
                    }
                case States.walk:
                    {
                        transform.position = transform.position + transform.forward * Time.deltaTime;//перемещаем персонажа прямо                
                        if (Vector3.Distance(transform.position, target.position) <= 2.5f)
                        {
                            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);//для исключения погрешности ставим в требуемую точку
                            anim.SetBool("Walking", false);
                            secondturn = true;
                            state = States.wait;
                            GoToPoint();
                        }
                        break;
                    }
            }
        }
    }
    
    public void GoToPoint()//функция для начала выполнения 
    {
        target = GameObject.Find("Cube").transform;
        dooranimator = GameObject.Find("Петли").GetComponent<Animator>();

            if (state == States.wait)
            {
                state = States.turn;
                Vector3 relativePos = new Vector3();
                if (!secondturn)
                {
                    relativePos = target.position - transform.position;//вычисляем координату куда нужно будет повернуться
                }
                else
                {
                Vector3 forward = target.transform.position + target.transform.forward;
                    relativePos = new Vector3(forward.x, transform.position.y, forward.z) - transform.position;
                }
                newrot = Quaternion.LookRotation(relativePos);//указываем нужный поворот
            }
        }
    }

