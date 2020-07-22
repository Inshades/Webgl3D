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

    [Header("Reception Info")]
    public GameObject receptionInfoPanel;
    public GameObject receptionButtonTemplate;

    [Header("Chat")]
    public GameObject chatPanel;

    [Header("Connect")]
    public GameObject connectPanel;

    [Header("My VirtualBag")]
    public GameObject myvirtualBagPanel;
    [Header("My VirtualBag-Connection")]
    public GameObject myconnectionPanel;


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

    private List<GameObject> userActivityObjs = new List<GameObject>();



    [SerializeField]
    private GameObject userActivityObj , userActivityObjBrouch;
    [SerializeField]
    private GameObject usrActiveMyConnect, usrActiveMyDocument, usrActiveMyVideo;

    bool x = true;
    //Logout Button is clicked to open Logout Panel

    public static UiManager instance = null;
    public static UiManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
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

    public void ReceptionInfoPanel()
    {
        receptionInfoPanel.SetActive(true);
        ReceptionDataPopulate();
    }
    public void CloseReception()
    {
        receptionInfoPanel.SetActive(false);
    }
    public void ReceptionDataPopulate()
    {
        if (receptionButtonTemplate.transform.parent.childCount - 1 != ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count)
        {
            for (int i = 1; i < receptionButtonTemplate.transform.parent.childCount; i++)
            {
                Destroy(receptionButtonTemplate.transform.parent.GetChild(i).gameObject);
            }

            for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count; i++)
            {

                GameObject button = Instantiate(receptionButtonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<ReceptionInfoList>().setInfoKey(i);
                button.GetComponent<ReceptionInfoList>().setInfoText(i+1 + "  " + ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsName);
                button.GetComponent<ReceptionInfoList>().setInfoDescription(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsDescription);

              

                StartCoroutine(button.GetComponent<ReceptionInfoList>().InfodownloadImage(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsLogoUrl));
                button.transform.SetParent(receptionButtonTemplate.transform.parent, false);


            }
        }
    }
    public void ExhibitorButton()
    {
        exhibitorPanel.SetActive(true);
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);


        ExhibitorDataPopulate();



    }

    public void ExhibitorDataPopulate()
    {
        if (buttonTemplate.transform.parent.childCount - 1 != ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count)
        {
            for (int i = 1; i < buttonTemplate.transform.parent.childCount; i++)
            {
                Destroy(buttonTemplate.transform.parent.GetChild(i).gameObject);
            }

            for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count; i++)
            {

                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<ExhibitorButtonList>().setKey(i);
                button.GetComponent<ExhibitorButtonList>().setText(i+1 + "  " + ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsName);
                button.GetComponent<ExhibitorButtonList>().setDescription(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsDescription);

                button.GetComponent<ExhibitorButtonList>().Tags = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].searchTags;

                StartCoroutine(button.GetComponent<ExhibitorButtonList>().downloadImage(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsLogoUrl));
                button.transform.SetParent(buttonTemplate.transform.parent, false);


            }
        }
    }



    public void ExhibitorSearch()
    {
        //Exhibitor panel - remove all child
        for (int i = 1; i < buttonTemplate.transform.parent.childCount; i++)
        {
            buttonTemplate.transform.parent.GetChild(i).gameObject.SetActive(false);
        }

        if (searchexhibitor.text == "")
            for (int i = 1; i < buttonTemplate.transform.parent.childCount; i++)
            {
                buttonTemplate.transform.parent.GetChild(i).gameObject.SetActive(true);
            }

        else
        {
            for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList.Count; i++)
            {
                for (int j = 0; j < ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].searchTags.Count; j++)
                {
                    if (ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].searchTags[j].Contains(searchexhibitor.text))
                    {
                        buttonTemplate.transform.parent.GetChild(i + 1).gameObject.SetActive(true);
                        break;
                    }

                }


            }
        }

    }

    public void ButtonClicked(int myKeystring)
    {

        GameObject selectedStall = StallsContainer.transform.GetChild(myKeystring).GetComponent<StallManager>().playerPosition;
        FPS.transform.position = selectedStall.transform.position;
        FPS.transform.rotation = selectedStall.transform.rotation;
        exhibitorPanel.SetActive(false);
        exhibitorPanel.SetActive(false);
        receptionInfoPanel.SetActive(false);
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
    public void MyBoxButton() //virtual bag
    {


        myvirtualBagPanel.SetActive(true);
        MyConnectionButton();
        logoutPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        menuSelectPanel.SetActive(false);

        StartCoroutine(ApiHandler.instance.GetUserActivity());

        if (ApiHandler.instance._userActivityList.Count != userActivityObjs.Count)
        {
            userActivityObjs = new List<GameObject>();

            int ind = userActivityObjs.Count;
            for (int i = ind+1; i < ApiHandler.instance._userActivityList.Count; i++)
            {
               

                switch (ApiHandler.instance._userActivityList[i].activityType)
                {
                    case "VIEW_BUSINESSCARD":
                    case "VISIT_BOOTH":
                    case "CHAT":
                        GameObject listitem = Instantiate(userActivityObj) as GameObject;
                        userActivityObjs.Add(listitem);
                        listitem.SetActive(true);
                        listitem.GetComponent<myconnection_data>().setData(ApiHandler.instance._userActivityList[i].boothId, ApiHandler.instance._userActivityList[i].boothName, ApiHandler.instance._userActivityList[i].activityType);
                         listitem.transform.parent = usrActiveMyConnect.transform;
                        // listitem.transform.SetParent(Listtemplate.transform.parent, false);
                        break;
                    case "DOWNLOAD_BROUCHER":
                         listitem = Instantiate(userActivityObjBrouch) as GameObject;
                        userActivityObjs.Add(listitem);
                        listitem.SetActive(true);
                        listitem.GetComponent<myconnection_data>().setData(ApiHandler.instance._userActivityList[i].boothId, ApiHandler.instance._userActivityList[i].boothName, ApiHandler.instance._userActivityList[i].activityType);
                        listitem.transform.parent = usrActiveMyDocument.transform;
                        // listitem.transform.SetParent(Listtemplate.transform.parent, false);
                        break;
                    case "VIEW_VIDEO":
                         listitem = Instantiate(userActivityObjBrouch) as GameObject;
                        userActivityObjs.Add(listitem);
                        listitem.SetActive(true);
                        listitem.GetComponent<myconnection_data>().setData(ApiHandler.instance._userActivityList[i].boothId, ApiHandler.instance._userActivityList[i].boothName, ApiHandler.instance._userActivityList[i].activityType);
                        listitem.transform.parent = usrActiveMyVideo.transform;
                        // listitem.transform.SetParent(Listtemplate.transform.parent, false);
                        break;
                }


            }
        }
    }

    public void MyConnectionButton()
    {
        myconnectionPanel.SetActive(true);
        documentsPanel.SetActive(false);
        videoPanel.SetActive(false);
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
