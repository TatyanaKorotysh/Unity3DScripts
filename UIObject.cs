using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    [SerializeField]
    Image progress;
        
    public bool State { get; set; } // автоматич свойство состояние подобран/не подобран этот объект


    public void UpdateImage() // обновить картинку в зависимости от состояния
    {
        if (State) // если объект активен (подобран)
        {
            progress.fillAmount += 0.2f;
        }
    }
}
