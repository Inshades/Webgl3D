/// <summary>
/// This script is for managing stall properties and UI
/// The video names are Effort Best Motive Belive
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class StallUIManager : MonoBehaviour
{

    private int key;

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

    [Header("Video")]
    public GameObject videoSelectPanel;

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

    //public GameObject docsPanel;
    //public GameObject webPanel;



   

    


    public void Setkey(int key)
    {
        this.key = key;
        Debug.Log("Keys" + key);
    }
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
                        BroucherButton();

                    }
                    if (hit.transform.gameObject.tag == "Video")
                    {
                        Debug.Log(hit.transform.gameObject.tag);
                        videoButton();

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
        broucherPanel.SetActive(true);
        menuSelectPanel.SetActive(false);
    }

    public void videoButton()
    {
        videoSelectPanel.SetActive(true);
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
    //public void CloseVideoButton()
    //{
    //    videoPlayer.Stop();
    //    videoPlayPanel.SetActive(false);
    //    videoSelectPanel.SetActive(true);
    //}


    public void BusinessCardButton()
    {
        menuSelectPanel.SetActive(false);
        businessCardPanel.SetActive(true);
        businessCardName.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName.ToString();
        businessCardPhone.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsTagLine.ToString();
        businessCardEmail.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressStreet.ToString();
        businessCardWeb.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressCity.ToString();
        businessCardAddress.text = ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsAddressState.ToString();
      
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
    }

}