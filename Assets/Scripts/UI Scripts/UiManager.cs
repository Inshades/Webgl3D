using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UiManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Contains Logout_Button and MenuSelector_Button
    public GameObject logoutPanel;   // User decides to quit by yes or No

    [Header("Menu Selector")]
    public GameObject menuSelectPanel;
    public Button menuSelectButton;
    public Sprite menuSprite;
    public Sprite closeSprite;

    public GameObject exhibitorPanel;
    public GameObject chatPanel;
    public GameObject conferencePanel;

    [Header("My Box Items")]
    public GameObject myboxPanel;
    public GameObject myconnectionPanel;
    public GameObject documentsPanel;
    public GameObject videoPanel;

    bool x = true;
   //Logout Button is clicked to open Logout Panel
   public void LogoutButton()
   {
        logoutPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
   }
   
   //In logout panel, yes clicked to close application
   public void Yes_LogoutButton()
   {
        Application.Quit();
   }
    //In Logout panel, no clicked to continue the application
    public void No_LogoutButton()
    {
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void MenuSelectButton()
    {
        if(x)
        {
            menuSelectPanel.SetActive(true);
            menuSelectButton.GetComponent<Image>().sprite = closeSprite;
            x = !x;
        }
        else
        {
            menuSelectPanel.SetActive(false);
            menuSelectButton.GetComponent<Image>().sprite = menuSprite;
            x = !x;
        }
    }
    public void ExhibitorButton()
    {
        exhibitorPanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);
    }
    public void ExhibitorCloseButton()
    {
        exhibitorPanel.SetActive(false);
       
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }

    public void ChatButton()
    {
        chatPanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);
    }

    public void ChatCloseButton()
    {
        chatPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }

    public void ConferenceButton()
    {
        conferencePanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);
    }

    public void ConferenceCloseButton()
    {
        conferencePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }
    public void MyBoxButton()
    {
        myboxPanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);
    }
    public void MyConnectionButton()
    {
        myconnectionPanel.SetActive(true);
        documentsPanel.SetActive(false);
        videoPanel.SetActive(false);
    }
    public void DocumentsButton()
    {
        documentsPanel.SetActive(true);
        videoPanel.SetActive(false);
        myconnectionPanel.SetActive(false);
    }
    public void VideoButton()
    {
        videoPanel.SetActive(true);
        documentsPanel.SetActive(false);
        myconnectionPanel.SetActive(false);
    }
    public void MyBoxCloseButton()
    {
        myboxPanel.SetActive(false);
        
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }

}
