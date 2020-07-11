using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class StallCanvasManager : MonoBehaviour
{
    /// <summary>
    /// This script is for sample video play in UI
    /// The video names are Effort Best Motive Belive
    /// </summary>
    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    [Header("VideoCanvas and Panel")]
    public GameObject mainVideoCanvas; //Canvas that holds video components
    public GameObject videoSelectPanel; //Panel that contains Options to select Video
    public GameObject videoPlayPanel;   //Panel, Where the video Plays

    [Header("info GameObject Name")]
    public string name; //name of the clickable gameobject

    [Header("Trigger Panels")]
    public GameObject infoPanel;
    public GameObject businessCardPanel;
    public GameObject docsPanel;
    public GameObject webPaneel;

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
                   
                    if (hit.transform.gameObject.name == name)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        VideoInfoButton();
                    }
                }
            }
        }
    }
    public void VideoInfoButton()
    {
      //  MainVideoCanvas.SetActive(true);
        videoSelectPanel.SetActive(true);
        videoPlayPanel.SetActive(false);
        videoPlayer.Stop();
    }

    public void Video1()
    {
        videoSelectPanel.SetActive(false);
        videoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Effort.mp4");
        videoPlayer.Play();
        
    }
    public void Video2()
    {
        videoSelectPanel.SetActive(false);
        videoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Best.mp4");
        videoPlayer.Play();
        
    }
    public void Video3()
    {
        videoSelectPanel.SetActive(false);
        videoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Motive.mp4");
        videoPlayer.Play();
       
    }
    public void Video4()
    {
        videoSelectPanel.SetActive(false);
        videoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Belive.mp4");
        videoPlayer.Play();
      
    }
    public void CloseMainButton()
    {
        // MainVideoCanvas.SetActive(false);
        videoSelectPanel.SetActive(false);
     
    }
    public void CloseVideoButton()
    {
        videoPlayer.Stop();
        videoPlayPanel.SetActive(false);
        videoSelectPanel.SetActive(true);
    }
   
    public void InfoButton()
    {
        infoPanel.SetActive(true);
        businessCardPanel.SetActive(false);
        docsPanel.SetActive(false);
        webPaneel.SetActive(false);
    }
    public void BusinessCardButton()
    {
        businessCardPanel.SetActive(true);
        docsPanel.SetActive(false);
        webPaneel.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void DocsButton()
    {
        docsPanel.SetActive(true);
        businessCardPanel.SetActive(false);
        infoPanel.SetActive(false);
        webPaneel.SetActive(false);
    }
    public void WebButton()
    {
        webPaneel.SetActive(true);
        businessCardPanel.SetActive(false);
        docsPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void CloseTriggerButton()
    {
        businessCardPanel.SetActive(false);
        docsPanel.SetActive(false);
        webPaneel.SetActive(false);
        infoPanel.SetActive(false);
    }
}
