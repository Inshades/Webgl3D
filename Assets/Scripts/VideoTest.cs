using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTest : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    // Start is called before the first frame update
    void Start()
    {
     
    }
    public void PlayVid()
    {
        string url = ApiHandler.instance._metaDataUrlContent.exhibhitorsBoothAmenitiesSourceUrl[2].ToString();
        
        Debug.Log(url);
        VideoPlayer.url = url;
        VideoPlayer.Play();
    }
    public void PlayVid2()
    {
        string url = ApiHandler.instance._metaDataUrlContent.exhibhitorsBoothAmenitiesSourceUrl[2].ToString();

        Debug.Log(url);
        VideoPlayer.url = url;
        VideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
