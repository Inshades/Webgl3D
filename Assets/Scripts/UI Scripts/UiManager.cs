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
    public InputField searchexhibitor;

    [Header("Chat")]
    public GameObject chatPanel;

    [Header("Connect")]
    public GameObject connectPanel;

    [Header("My VirtualBag")]
    public GameObject myvirtualBagPanel;
    [Header("My VirtualBag-Connection")]
    public GameObject myconnectionPanel;
    public GameObject Listtemplate;

    public GameObject businessPanel;
    public Text businessName;
    public Text businessPhone;
    public Text businessEmail;
    public Text businessWeb;
    public Text businessAddress;

    public GameObject emailPanel;
    public InputField subject;
    public InputField bodyText;


    public GameObject documentsPanel;
    public GameObject videoPanel;

    public GameObject StallsContainer;
    public GameObject FPS;


    private List<int> exhibator_list = new List<int>();

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


        ExhibitorDataPopulate("");



    }

    public void ExhibitorDataPopulate(string searchexhibitor)
    {
        if (exhibator_list.Count == 0)
        {
            for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count; i++)
            {
                var tagmatched = true;

                //get the taglists of "i" college
                // var tags =  //api 

                //foreach(var tag in tags)
                //{
                //    if(tag == searchexhibitor)
                //    {
                //        tagmatched = true;
                //        break;

                //    }
                //}

                if (tagmatched)
                {
                    exhibator_list.Add(i);
                    GameObject button = Instantiate(buttonTemplate) as GameObject;
                    button.SetActive(true);

                    button.GetComponent<ExhibitorButtonList>().setKey(i);
                    button.GetComponent<ExhibitorButtonList>().setText(i + "" + ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsName);
                    button.GetComponent<ExhibitorButtonList>().setDescription(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsDescription);

                    StartCoroutine(button.GetComponent<ExhibitorButtonList>().downloadImage(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsLogoUrl));
                    button.transform.SetParent(buttonTemplate.transform.parent, false);
                }
              

            }
        }
    }

    public void ExhibitorSearch()
    {
        //Exhibitor panel - remove all child

        exhibator_list = null;
        ExhibitorDataPopulate(searchexhibitor.text);

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

        //reuse  ...................

        for (int i = 0; i < 10; i++)
        {
            GameObject listitem = Instantiate(Listtemplate) as GameObject;
            listitem.SetActive(true);
            listitem.GetComponent<myconnection_data>().setData(i+"", "boothname", "datetime");
            listitem.transform.SetParent(Listtemplate.transform.parent, false);
        }
    }
    public void BusinessCard_ConnectionPanel(int key)
    {
        businessPanel.SetActive(true);


        Debug.Log(key);
        businessName.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName.ToString();
        businessPhone.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsTagLine.ToString();
        businessEmail.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressStreet.ToString();
        businessWeb.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressCity.ToString();
        businessAddress.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressState.ToString();
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
