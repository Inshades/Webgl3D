using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerPoint : MonoBehaviour
{
    private StallCanvasManager _stallCanvasManager;
    public string name;
    private void Start()
    {
        _stallCanvasManager = GameObject.Find(name).GetComponent<StallCanvasManager>(); 
    }
    private void OnTriggerEnter(Collider other)
    {

        _stallCanvasManager.mainVideoCanvas.SetActive(true);
       

    }
    private void OnTriggerExit(Collider other)
    {
        _stallCanvasManager.mainVideoCanvas.SetActive(false);
        _stallCanvasManager.videoSelectPanel.SetActive(false);
        _stallCanvasManager.videoPlayPanel.SetActive(false);
        _stallCanvasManager.infoPanel.SetActive(false);
        _stallCanvasManager.businessCardPanel.SetActive(false);
        _stallCanvasManager.docsPanel.SetActive(false);
        _stallCanvasManager.webPaneel.SetActive(false);
    _stallCanvasManager.videoPlayer.Stop();
    }
}
