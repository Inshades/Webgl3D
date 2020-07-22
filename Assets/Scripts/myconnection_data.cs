using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myconnection_data : MonoBehaviour
{
    [SerializeField]
    private Text Boothid;
    [SerializeField]
    private Text BoothName;
    [SerializeField]
    private Text ActivityType;

    public UiManager UiManager;
   


    private int myKeystring;

    public void setData( string Boothid, string BoothName, string ActivityType)
    {
      //  myKeystring = int.Parse( Boothid);
        this.Boothid.text = Boothid;
        this.BoothName.text = BoothName;
        this.ActivityType.text = ActivityType;

      //  Debug.Log(this.Boothid.text);


    }

    public void bussiness_buttonClick()
    {

       

        //H1-2
        string triggerName = this.Boothid.text;
        triggerName = triggerName.Substring(triggerName.IndexOf("-") + 1, triggerName.Length - (triggerName.IndexOf("-") + 1));
        int key = int.Parse(triggerName);
        key = key - 1;
       
        UiManager.BusinessCard_ConnectionPanel(key);

         
    }


     
}
