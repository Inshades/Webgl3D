using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlay : MonoBehaviour
{
    /// <summary>
    /// This script is for sample video play in UI
    /// The video names are Effort Best Motive Belive
    /// </summary>
    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    [Header("VideoCanvas and Panel")]
    public GameObject MainVideoCanvas; //Canvas that holds video components
    public GameObject VideoSelectPanel; //Panel that contains Options to select Video
    public GameObject VideoPlayPanel;   //Panel, Where the video Plays

    [Header("info GameObject Name")]
    public string name; //name of the clickable gameobject
   


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
        MainVideoCanvas.SetActive(true);
        VideoSelectPanel.SetActive(true);
        VideoPlayPanel.SetActive(false);
        videoPlayer.Stop();
    }

    public void Video1()
    {
        VideoSelectPanel.SetActive(false);
        VideoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Effort.mp4");
        videoPlayer.Play();
    }
    public void Video2()
    {
        VideoSelectPanel.SetActive(false);
        VideoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Best.mp4");
        videoPlayer.Play();
    }
    public void Video3()
    {
        VideoSelectPanel.SetActive(false);
        VideoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Motive.mp4");
        videoPlayer.Play();
    }
    public void Video4()
    {
        VideoSelectPanel.SetActive(false);
        VideoPlayPanel.SetActive(true);
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Belive.mp4");
        videoPlayer.Play();
    }
    public void CloseMainButton()
    {
        MainVideoCanvas.SetActive(false);
        Debug.Log("Click");
    }
    public void CloseVideoButton()
    {
        videoPlayer.Stop();
        VideoPlayPanel.SetActive(false);
        VideoSelectPanel.SetActive(true);
    }
   
}
