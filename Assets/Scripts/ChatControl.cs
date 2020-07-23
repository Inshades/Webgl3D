using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ChatControl : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR
     [DllImport("__Internal")]
    private static extern void OnsendClick(string msg);
#endif

    public GameObject chatTemplate;
    public Scrollbar ScrollbarVertical;

    public InputField mychattyped;

    public void Start()
    {
        ScrollbarVertical.value = 0f;
    }

    public void Update()
    {
        //get text from API and call messageFromAPI (text)
        if(mychattyped.text!="")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                string MessageToSend = mychattyped.text;
                ScrollbarVertical.value = 0f;
                OnSendClick();
#if UNITY_WEBGL && !UNITY_EDITOR
               
                OnsendClick(MessageToSend); 
#endif

            }
        }

       
    }


    public void OnSendClick()
    {
        string MessageToSend = mychattyped.text;
        Debug.Log("Sending message to Javascript:" + MessageToSend);
        GameObject chat = Instantiate(chatTemplate) as GameObject;
        chat.SetActive(true);
        chat.GetComponent<ChatList>().setText(mychattyped.text);
        chat.transform.SetParent(chatTemplate.transform.parent, false);
#if UNITY_WEBGL && !UNITY_EDITOR
        OnsendClick(MessageToSend);
        //s ScrollbarVertical.value = 0f;
#endif

        mychattyped.text = "";
    }
    public void messagrFromApi(string text)
    {
        GameObject chat = Instantiate(chatTemplate) as GameObject;
        chat.SetActive(true);
        chat.GetComponent<ChatList>().setAPIText(mychattyped.text);
        chat.transform.SetParent(chatTemplate.transform.parent, false);
       // ScrollbarVertical.value = 0f;

    }
}
