/// <summary>
/// This script is for managing stall properties and UI
/// The video names are Effort Best Motive Belive
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class StallUIManager : MonoBehaviour
{

    private int key;

    private UiManager uiManager;




    //[Header("Video Player")]
    //public VideoPlayer videoPlayer;

    //[Header("VideoCanvas and Panel")]
    //public GameObject mainVideoCanvas; //Canvas that holds video components
    //public GameObject videoSelectPanel; //Panel that contains Options to select Video
    //public GameObject videoPlayPanel;   //Panel, Where the video Plays


    [Header("3D clicakbel GameObject Name")]
    public string infoName; //name of the clickable gameobject
    public string videoName;
    public string broucherName;

    [Header("Exhibitor Info")]
    public GameObject infoPanel;
    public Text exhibName;
    public Text exhibType;
    public Text exhibDescp;

    [Header("Broucher")]
    public GameObject broucherPanel;
    public GameObject buttonTemplate;

    [Header("Video")]
    public GameObject videoSelectPanel;
    public GameObject videoPlayPanel;
    public VideoPlayer videoPlayer;
    public GameObject videobuttonTemplate;

    [Header("Email")]
    public GameObject emailPanel;

    [Header("Chat")]
    public GameObject chatPanel;

    //[Header("Trigger Panels")]
    //  public GameObject infoPanel;
    [Header("Stall Menu")]
    public GameObject menuSelectPanel;



    [Header("Business Card")]
    public GameObject businessCardPanel;
    public Text businessCardName;
    public Text businessCardPhone;
    public Text businessCardEmail;
    public Text businessCardWeb;
    public Text businessCardAddress;

    public GameObject slideShowPanel;
    public Image slideShowImage;
    //public GameObject webPanel;

    


    public void start()
    {
        uiManager = GameObject.Find("UI_Manager").GetComponent<UiManager>();
    }
    //private void OnEnable()
    //{
    //    if (key != null)
    //        setUserActivity(userActivityType.VISIT_BOOTH, "Visit Booth", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0]);
    //}
    //private void OnDisable()
    //{
    //    if (key !=null)
    //        setUserActivity(userActivityType.VISIT_BOOTH, "Booth Exit", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0]);
    //}
    public void Setkey(int key)
    {
        this.key = key;
        Debug.Log("Keys" + key);

    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);

    private void Start()
    {


    }

    // Update is called once per frame
    /// <summary>
    /// Raycast to select the clickable object to play the video
    /// </summary>
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null)
                {
                    // Debug.Log(hit.transform.gameObject.tag);
                    if (hit.transform.gameObject.tag == "Info")
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        InfoButton();

                    }
                    if (hit.transform.gameObject.tag == "Product")
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        //SlideShowButton();
                        BroucherButton();

                    }
                    if (hit.transform.gameObject.tag == "Video")
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        videoButton();

                    }
                    if (hit.transform.gameObject.tag == "ReceptionInfo")
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        //  videoButton();
                        UiManager.instance.ReceptionInfoPanel();

                    }

                }
            }
        }
    }
    public void InfoButton()
    {
        infoPanel.SetActive(true);
        exhibName.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName.ToString();
        exhibType.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsType.ToString();
        exhibDescp.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsTagLine.ToString();
        menuSelectPanel.SetActive(false);

    }

    public void BroucherButton()
    {
        for (int i = 1; i < buttonTemplate.transform.parent.childCount; i++)
        {
            Destroy(buttonTemplate.transform.parent.GetChild(i).gameObject);
        }
        broucherPanel.SetActive(true);
        menuSelectPanel.SetActive(false);
        //for (int i = 0; i < 2; i++)
        //{
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);

        button.GetComponent<BroucherButtonList>().setBroucherKey(key);
        button.GetComponent<BroucherButtonList>().setBroucherText(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key]._collegeAmenities[0].exhibhitorsBoothAmenitiesName);
        button.GetComponent<BroucherButtonList>().setBroucherDesText(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key]._collegeAmenities[0].exhibhitorsBoothAmenitiesType);

        button.transform.SetParent(buttonTemplate.transform.parent, false);

        //}
    }
    public void BroucherButtonClicked(int myKeystring)
    {

        //Application.ExternalEval("window.open('https://google.com');");
        // openWindow(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeystring]._collegeAmenities[2].exhibhitorsBoothAmenitiesSourceUrl);
#if !UNITY_EDITOR
        openWindow(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeystring]._collegeAmenities[0].exhibhitorsBoothAmenitiesSourceUrl);

#endif
        //Application.OpenURL(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeystring]._collegeAmenities[0].exhibhitorsBoothAmenitiesSourceUrl);
        Debug.Log(myKeystring + "BroucherButtonClicked");

        setUserActivity(userActivityType.DOWNLOAD_BROUCHER, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeystring]._collegeAmenities[0].exhibhitorsBoothAmenitiesSourceUrl, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0]);
    }

    public void videoButton()
    {
        for (int i = 1; i < videobuttonTemplate.transform.parent.childCount; i++)
        {
            Destroy(videobuttonTemplate.transform.parent.GetChild(i).gameObject);
        }
        videoSelectPanel.SetActive(true);
        menuSelectPanel.SetActive(false);
        //for condition
        GameObject videobutton = Instantiate(videobuttonTemplate) as GameObject;
        videobutton.SetActive(true);
        Debug.Log("Videeo");
        videobutton.GetComponent<VideoButtonList>().setVideoKey(key);
        videobutton.GetComponent<VideoButtonList>().setVideoText(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key]._collegeAmenities[2].exhibhitorsBoothAmenitiesName);
        videobutton.GetComponent<VideoButtonList>().setVideoDesText(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key]._collegeAmenities[2].exhibhitorsBoothAmenitiesType);
        videobutton.transform.SetParent(videobuttonTemplate.transform.parent, false);

    }
    public void VideoPlayButton(int myKeyString)
    {
        videoPlayPanel.SetActive(true);
        videoPlayer.url = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeyString]._collegeAmenities[2].exhibhitorsBoothAmenitiesSourceUrl;
        videoPlayer.Play();

        setUserActivity(userActivityType.VIEW_VIDEO, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[myKeyString]._collegeAmenities[2].exhibhitorsBoothAmenitiesSourceUrl, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0]);
    }
    public void SlideShowButton()
    {
        slideShowPanel.SetActive(true);
        menuSelectPanel.SetActive(false);


    }


    public void EmailButton()
    {
        emailPanel.SetActive(true);
        menuSelectPanel.SetActive(false);


    }

    public void ChatButton()
    {
        chatPanel.SetActive(true);
        menuSelectPanel.SetActive(false);


    }
    //public void CloseMainButton()
    //{
    //    // MainVideoCanvas.SetActive(false);
    //    videoSelectPanel.SetActive(false);

    //}
    public void CloseVideoButton()
    {
        videoPlayer.Stop();
        videoPlayPanel.SetActive(false);
        videoSelectPanel.SetActive(true);
    }


    public void BusinessCardButton()
    {
        menuSelectPanel.SetActive(false);

        businessCardPanel.SetActive(true);
        businessCardName.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName.ToString();
        businessCardPhone.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsTagLine.ToString();
        businessCardEmail.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressStreet.ToString();
        businessCardWeb.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressCity.ToString();
        businessCardAddress.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressState.ToString();

        setUserActivity(userActivityType.VIEW_BUSINESSCARD, "Businesscard viewed", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0]);

    }


    public void CloseTriggerButton()
    {
        businessCardPanel.SetActive(false);
        menuSelectPanel.SetActive(true);
        broucherPanel.SetActive(false);
        videoSelectPanel.SetActive(false);
        infoPanel.SetActive(false);
        emailPanel.SetActive(false);
        chatPanel.SetActive(false);
        slideShowPanel.SetActive(false);

    }

    void setUserActivity(userActivityType _userActivity, string activityData, string boothName, string boothId)
    {
        StartCoroutine(ApiHandler.instance.SaveUserActivity(_userActivity, activityData, boothName, boothId));
    }
}