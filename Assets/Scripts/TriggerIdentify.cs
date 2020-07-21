using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerIdentify : MonoBehaviour
{
   // public GameObject trigger;

    public StallUIManager stall_uimanager;
    public GameObject Canvas;

    private void OnEnable()
    {

        Canvas = GameObject.Find("Canvas");
        stall_uimanager = GameObject.Find("StallUIManager").GetComponent<StallUIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {

        //B1_1
        Canvas.SetActive(true);
        //  Debug.Log("trigger");
        Canvas.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log(transform.parent.GetSiblingIndex() + "get sibiling");

        int key = transform.parent.GetSiblingIndex();
      //  string triggerName = trigger.name;
      //  triggerName = triggerName.Substring(triggerName.IndexOf("_") + 1, triggerName.IndexOf("(") - (triggerName.IndexOf("_") + 1));
       // int key = int.Parse(triggerName);
     //   key = key - 1;
        stall_uimanager.Setkey(key);

    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }
}
