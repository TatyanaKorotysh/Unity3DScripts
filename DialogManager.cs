using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public Transform dialogWindow;
    public Transform taskWindow;

    public Image mainImage;
    public Image firtsImage;

    private Queue<string> sentences;
    bool playerName = false;
    string NPCname;
    private Dialog dialogCurrent;

    private AudioSource button;

    void Start()
    {
        sentences = new Queue<string>();
        button = GameObject.Find("MainManager").GetComponent<AudioSource>();
    }

    public void StartDialog(Dialog dialog) {
        dialogCurrent = dialog;
        dialogWindow.gameObject.SetActive(true);
        firtsImage.GetComponent<Image>().sprite = dialog.img;
        mainImage.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<Image>().sprite;
        
       sentences.Clear();

        foreach (string sentence in dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        NPCname = dialog.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        button.Play();
        if (playerName) {
            nameText.text = "Вы";
            mainImage.gameObject.SetActive(true);
            firtsImage.gameObject.SetActive(false);
            playerName = false; 
        }
        else
        {
            nameText.text = NPCname;
            mainImage.gameObject.SetActive(false);
            firtsImage.gameObject.SetActive(true);
            playerName = true;
        }
        if (sentences.Count == 0) {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialog() {
        playerName = false;
        mainImage.gameObject.SetActive(false);
        firtsImage.gameObject.SetActive(false);
        dialogWindow.gameObject.SetActive(false);
        if (dialogCurrent.getTask == false) taskWindow.gameObject.SetActive(true);
    }

    public void StopDialog()
    {
        dialogWindow.gameObject.SetActive(false);
        firtsImage.gameObject.SetActive(false);
        mainImage.gameObject.SetActive(false);
    }
    
}
