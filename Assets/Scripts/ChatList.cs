using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatList : MonoBehaviour
{
    [SerializeField]
    private Text mytext;

    public void setText(string myChatText)
    {
        mytext.text = myChatText;
        mytext.alignment = TextAnchor.MiddleRight;
        mytext.color = Color.red;
    }

    public void setAPIText(string myChatText)
    {
        mytext.text = myChatText;
        mytext.alignment = TextAnchor.MiddleLeft;
        mytext.color = Color.blue;
    }


}
