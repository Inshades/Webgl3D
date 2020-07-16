using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlay : MonoBehaviour
{
    public VideoPlayer[] videoPlayer;
    public string[] name;
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        for(int i =0; i<videoPlayer.Length; i++)
        {
            videoPlayer[i].url = System.IO.Path.Combine(Application.streamingAssetsPath, name[i]);
            videoPlayer[i].Play();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < videoPlayer.Length; i++)
        {
            videoPlayer[i].Stop();
        }
    }
}
