using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static string text;
    public static bool isUI;
    
    public enum ProjectMode { Tooltip3D = 0, Tooltip2D = 1 };
    public ProjectMode tooltipMode = ProjectMode.Tooltip3D;
    private int maxWidth = 450; // максимальная ширина Tooltip
    public RectTransform box;
    public Text boxText;
    private Camera _camera;
    public float speed = 10; 

    private Image img;
    private Color BGColorFade;
    private Color textColorFade;

    void Awake()
    {
        img = box.GetComponent<Image>();
        isUI = false;
    }

    void LateUpdate()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bool show = false;

        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.GetComponent<TooltipText>())
                {
                    text = hit.transform.GetComponent<TooltipText>().text;
                    show = true;
                }
            }

        boxText.text = text;
        float width = maxWidth;
        if (boxText.preferredWidth <= maxWidth) width = boxText.preferredWidth;
        box.sizeDelta = new Vector2(width, boxText.preferredHeight);

        float arrowShift = width / 4; // сдвиг позиции стрелки по Х

        if (show || isUI)
        {
            img.enabled = true;
            boxText.enabled = true;

            float curY = Input.mousePosition.y + box.sizeDelta.y / 2 + 50;
            Vector3 arrowScale = new Vector3(1, 1, 1);
            if (curY + box.sizeDelta.y / 2 > Screen.height) // если Tooltip выходит за рамки экрана, в данном случаи по высоте
            {
                curY = Input.mousePosition.y - box.sizeDelta.y / 2 - 50;
                arrowScale = new Vector3(1, -1, 1); // отражение по вертикале
            }

            float curX = Input.mousePosition.x + arrowShift - 350;
            if (curX + box.sizeDelta.x / 2 > Screen.width)
            {
                curX = Input.mousePosition.x - arrowShift + 350;
            }

            box.anchoredPosition = new Vector2(curX, curY);            
        }
        else
        {
            img.enabled = false;
            boxText.enabled = false;
        }
    }
}
