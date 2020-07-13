using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerPoint : MonoBehaviour
{
    public StallManager _stallManager;
   // public string name;
    private void Start()
    {
      //  _stallManager = GameObject.Find(name).GetComponent<StallManager>(); 
    }
    private void OnTriggerEnter(Collider other)
    {

        _stallManager.mainVideoCanvas.SetActive(true);
       

    }
    private void OnTriggerExit(Collider other)
    {
        _stallManager.mainVideoCanvas.SetActive(false);
        _stallManager.videoSelectPanel.SetActive(false);
        _stallManager.videoPlayPanel.SetActive(false);
        _stallManager.infoPanel.SetActive(false);
        _stallManager.businessCardPanel.SetActive(false);
        _stallManager.docsPanel.SetActive(false);
        _stallManager.webPaneel.SetActive(false);
        _stallManager.videoPlayer.Stop();
    }
}
