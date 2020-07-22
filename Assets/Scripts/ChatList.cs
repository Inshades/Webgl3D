using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatList : MonoBehaviour
{
    [SerializeField]
    private Text mytext;
    [SerializeField]
    private Image image;

    public void setText(string myChatText)
    {
        mytext.text = myChatText;
        mytext.alignment = TextAnchor.MiddleRight;
        mytext.color = Color.white;
        image.color = Color.gray;
    }

    public void setAPIText(string myChatText)
    {
        mytext.text = myChatText;
        mytext.alignment = TextAnchor.MiddleLeft;
        mytext.color = Color.black;
        image.color = new Color32(217,212,207,255);
    }


}
