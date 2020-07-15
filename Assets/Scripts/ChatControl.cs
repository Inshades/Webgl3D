using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatControl : MonoBehaviour
{

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
                OnsendClick(); ScrollbarVertical.value = 0f;

            }
        }

       
    }


    public void OnsendClick()
    {
           
            GameObject chat = Instantiate(chatTemplate) as GameObject;
            chat.SetActive(true);
            chat.GetComponent<ChatList>().setText(mychattyped.text);
            chat.transform.SetParent(chatTemplate.transform.parent, false);
            mychattyped.text = "";
           //s ScrollbarVertical.value = 0f;
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
