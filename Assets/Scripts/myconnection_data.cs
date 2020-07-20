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
        UiManager.BusinessCard_ConnectionPanel(transform.GetSiblingIndex());
    }


     
}
