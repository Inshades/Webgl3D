using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;



public class luckydraw : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonSend;

    [SerializeField]
    private GameObject[] images = new GameObject[6];

    [SerializeField]
    private Text[] collegeLuckydrawlist = new Text[6];



  


    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    public void GenerateRandomList()
    {
        for (int i = 0; i < ApiHandler.instance._listLuckyDrawExhibitorCollege.Count; i++)
        {
            collegeLuckydrawlist[i].text = ApiHandler.instance._listLuckyDrawExhibitorCollege[i].boothId.ToString();
        }

    }
    List<string> luckyDrawVisitedList = new List<string>();
    public void checker(string CurrentexhibhitorsName)
    {
        if (!luckyDrawVisitedList.Contains(CurrentexhibhitorsName))
        {

            Debug.Log("triggg" + CurrentexhibhitorsName);
            for (int i = 0; i < ApiHandler.instance._listLuckyDrawExhibitorCollege.Count; i++)
            {
                if (ApiHandler.instance._listLuckyDrawExhibitorCollege[i].boothId == CurrentexhibhitorsName)
                {
                    luckyDrawVisitedList.Add(CurrentexhibhitorsName);

                    images[i].SetActive(false);

                    Debug.Log(CurrentexhibhitorsName);
                }
            }
        }
        if (luckyDrawVisitedList.Count == ApiHandler.instance._listLuckyDrawExhibitorCollege.Count)
        {
            buttonSend.SetActive(true);
        }

        //if (RandomList.Count == 0)
        //{
        //    buttonSend.SetActive(true);
        //}

    }

    public void buttonLuckyDraw()
    {
        Debug.Log("You are eligible for lucky draw");
       

        StartCoroutine(ApiHandler.instance.GenerateLuckyCupon((callBack) =>
        {
            switch (callBack._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    Debug.Log("Generated luckyDraw code is : "+ callBack.responseMessage);
                    break;

                case apiResponseType.FAIL:

                    break;
                case apiResponseType.SEVER_ERROR:

                    break;
            }
        }));
    }

}
