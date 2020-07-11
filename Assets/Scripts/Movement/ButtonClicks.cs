using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClicks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool flag;
    private string s;
    public void OnPointerDown(PointerEventData eventData)
    {
        flag = true;
        s = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(this.gameObject.name + " Was Clicked.");
        Debug.Log(s);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        flag = false;
        if(s == "RotateLeft")
        {
            Movement_Script.Rot_Lft = 0;
        }
        if (s == "RotateRight")
        {
            Movement_Script.Rot_Right = 0;
        }
        if (s == "MoveUp")
        {
            Movement_Script.Move_Up = 0;
        }
        if (s == "MoveDown")
        {
            Movement_Script.Move_Down = 0;
        }
        if (s == "MoveLeft")
        {
            Movement_Script.Move_Left = 0;
        }
        if (s == "MoveRight")
        {
            Movement_Script.Move_Right = 0;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            if (s == "RotateLeft")
            {
                Movement_Script.Rot_Lft = 1;
            }
            if (s == "RotateRight")
            {
                Movement_Script.Rot_Right = 1;
            }
            if (s == "MoveUp")
            {
                Movement_Script.Move_Up = 1;
            }
            if (s == "MoveDown")
            {
                Movement_Script.Move_Down = 1;
            }
            if (s == "MoveLeft")
            {
                Movement_Script.Move_Left = 1;
            }
            if (s == "MoveRight")
            {
                Movement_Script.Move_Right = 1;
            }
        }
    }
}
