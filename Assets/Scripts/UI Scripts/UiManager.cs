using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class UiManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Contains Logout_Button and MenuSelector_Button
    public GameObject logoutPanel;   // User decides to quit by yes or No

    public GameObject mobileUiPanel;

    [Header("Menu Selector")]
    public GameObject menuSelectPanel;
    public Button menuSelectButton;
    public Sprite menuSprite;
    public Sprite closeSprite;

    [Header("Exhibitor")]
    public GameObject exhibitorPanel;
    public GameObject buttonTemplate;

    [Header("Chat")]
    public GameObject chatPanel;

    [Header("Connect")]
    public GameObject connectPanel;

    [Header("My VirtualBag")]
    public GameObject myvirtualBagPanel;
    public GameObject myconnectionPanel;
    public GameObject documentsPanel;
    public GameObject videoPanel;

    public GameObject StallsContainer;
    public GameObject FPS;

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
        if (x)
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

        for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.GetComponent<ButtonListButton>().setKey(i);
            button.GetComponent<ButtonListButton>().setText(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsName);
            button.GetComponent<ButtonListButton>().setDescription(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsDescription);

            StartCoroutine(button.GetComponent<ButtonListButton>().downloadImage(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsLogoUrl));
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    public void ButtonClicked(int myKeystring)
    {

        GameObject selectedStall = StallsContainer.transform.GetChild(myKeystring).GetComponent<StallManager>().playerPosition;
        FPS.transform.position = selectedStall.transform.position;
        FPS.transform.rotation = selectedStall.transform.rotation;
        exhibitorPanel.SetActive(false);
        exhibitorPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
        Debug.Log(myKeystring);
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

    public void ConnectButton()
    {
        connectPanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);
    }

    public void ConnectCloseButton()
    {
        connectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }
    public void MyBoxButton()
    {
        myvirtualBagPanel.SetActive(true);
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
        myvirtualBagPanel.SetActive(false);

        mainMenuPanel.SetActive(true);
        menuSelectPanel.SetActive(true);
    }

}
