using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoButtonList : MonoBehaviour
{
    [SerializeField]
    private Text VideoText;
    [SerializeField]
    private Text VideoDesText;

    [SerializeField]
    private StallUIManager buttonControl;

    private int myKey;

    public void setVideoKey(int key)
    {
        myKey = key;
    }
    public void setVideoText(string textString)
    {
        VideoText.text = textString;

    }
    public void setVideoDesText(string textDesString)
    {
        VideoDesText.text = textDesString;

    }

    public void onClick()
    {
        buttonControl.VideoPlayButton(myKey);
    }
}
