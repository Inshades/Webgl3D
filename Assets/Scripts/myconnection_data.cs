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
    private Text Datetime;

    public UiManager UiManager;
   


    private int myKeystring;

    public void setData( string Boothid, string BoothName, string Datetime)
    {
        myKeystring = int.Parse( Boothid);
        this.Boothid.text = Boothid;
        this.BoothName.text = BoothName;
        this.Datetime.text =  Datetime;

        Debug.Log(this.Boothid.text);
    }

    public void bussiness_buttonClick()
    {

        UiManager.BusinessCard_ConnectionPanel(myKeystring);
    }


     
}
