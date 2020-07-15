using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerIdentify : MonoBehaviour
{
    public GameObject trigger;
    public StallUIManager stall_manager;
    public GameObject Canvas;

    private void OnEnable()
    {
        Canvas=GameObject.Find("Canvas");
        stall_manager = GameObject.Find("StallUIManager").GetComponent<StallUIManager>();
    }
    private void OnTriggerEnter(Collider other)
    {

        //B1_1
        Canvas.SetActive(true);
        Debug.Log("trigger");
        Canvas.transform.GetChild(0).gameObject.SetActive(true);
            string triggerName = trigger.name;
        Debug.Log(triggerName);

       //stall B1_20(clone)

       // -   == 8  (  == 10   suns ( 9 , len )    // len = 2 , len = 1 \
                                                   // ( 11    , ( 10         -  9 ,  2 or 1

        



       triggerName = triggerName.Substring( triggerName.IndexOf("_") +1 , triggerName.IndexOf("(")  -( triggerName.IndexOf("_") + 1) );

        Debug.Log(triggerName);
            int key = int.Parse(triggerName);
           key = key - 1;
      
        Debug.Log(key);
          stall_manager.Setkey(key); 





    }
    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }
}
