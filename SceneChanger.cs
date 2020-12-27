using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private Tasks tasks;
    private Timer timer;
    private GameObject[] old;
    private Canvas canva;
    private Canvas loading;
    private Slider bar;
    private GameObject startWindow;
    private Canvas esc;
    private Canvas preferences;
    private Canvas control;


    private AudioSource button;
    private AudioSource yes;
    private AudioSource win;

    private PlayerMove player;

    public void Start()
    {
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
        timer = GameObject.Find("Canvas").transform.Find("Timer").GetChild(0).GetComponent<Timer>();
        canva = GameObject.Find("Canvas").GetComponent<Canvas>();
        loading = GameObject.Find("LoadingScreen").GetComponent<Canvas>();
        loading.enabled = false;
        bar = GameObject.Find("Slider").GetComponent<Slider>();
        startWindow = GameObject.Find("StartWindow");
        win = GameObject.Find("Canvas").transform.Find("Timer").GetComponent<AudioSource>();
        yes = GameObject.Find("Canvas").transform.Find("Task").GetComponent<AudioSource>();
        button = GameObject.Find("MainManager").GetComponent<AudioSource>();
        esc = GameObject.Find("Esc").GetComponent<Canvas>();
        esc.enabled = false;
        preferences = GameObject.Find("Preferences").GetComponent<Canvas>();
        preferences.enabled = false;
        control = GameObject.Find("Control").GetComponent<Canvas>();
        control.enabled = false;
    }
    
    public void OpenNewScene(int index) // метод для смены сцены
    {
        loading.enabled = true;
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
    }

    IEnumerator AsyncLoad(int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        
        while (!ready.isDone) // пока сцена не загрузилась
        {
            bar.value = ready.progress;
            yield return null; // ждём следующий кадр
        }
        if (index == 1 || index == 2)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            if (startWindow.activeSelf) player.enabled = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (this.gameObject.tag) {
            case "firstScene":
                OpenNewScene(1);                    
                break;
            case "secondScene":
                if (tasks.Task1) OpenNewScene(2);
                else MainManager.Messenger.WriteMessage("Данная локация пока закрыта. Для открытия необходимо получить задание");
                break;
        }
    }

    public void OpenFirstScene() {
        canva.enabled = false;
        button.Play();
        startWindow.SetActive(true);
        OpenNewScene(1);
       
    }

    public void StartPlay() {
        player.enabled = true;
        yes.Play();
        timer.StartTimer();
        startWindow.SetActive(false);
        canva.enabled = true;
        MainManager.Messenger.WriteMessage("Найди кого-нибудь, чтобы спросить дорогу");
    }

    public void OpenSceneAfterAnimation() {
        win.Play();
        canva.enabled = false;
        MainManager.sceneChanger.OpenNewScene(3);
    }

    public void ChangeCharacter() {
        button.Play();
        MainManager.sceneChanger.OpenNewScene(4);
    }

    public void Menu()
    {
        button.Play();
        StartCoroutine(AfterButton());
    }

    IEnumerator AfterButton()
    {
        yield return new WaitForSeconds(0.2f);
        MainManager.sceneChanger.OpenNewScene(0);
        old = GameObject.FindGameObjectsWithTag("save");
        foreach (GameObject obj in old)
        {
            Destroy(obj);
        }
    }

    public void OpenPreferences() {
        preferences.enabled = true;
    }

    public void ClosePreferences()
    {
        preferences.enabled = false;
    }

    public void OpenControl()
    {
        control.enabled = true;
    }

    public void CloseControl()
    {
        control.enabled = false;
    }
}