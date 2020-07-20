using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BroucherButtonList : MonoBehaviour
{
    [SerializeField]
    private Text broucherText;

    [SerializeField]
    private Text broucherDesText;

    [SerializeField]
    private StallUIManager buttonControl;

    private int myKey;

    public void setBroucherKey(int key)
    {
        myKey = key;
    }
    public void setBroucherText(string textString)
    {
        broucherText.text = textString;

    }
    public void setBroucherDesText(string textDesString)
    {
        broucherDesText.text = textDesString;

    }
    public void onClick()
    {
         buttonControl.BroucherButtonClicked(myKey);
    }
}
