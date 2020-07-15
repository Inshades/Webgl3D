using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, name);
        videoPlayer.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
