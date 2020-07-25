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



    private List<string> RandomList = new List<string>();

    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        //GenerateRandomList();
        
    }

    // Update is called once per frame
    public void GenerateRandomList()
    {

        //RandomList.Add("Columbia University");
        //RandomList.Add("Duke University");
        //RandomList.Add("McGill University");
        //RandomList.Add("Princeton University");
        //RandomList.Add("Stanford University");
        //RandomList.Add("UC Berkeley");
        //Debug.Log(RandomList);
    }
    List<string> luckyDrawVisitedList = new List<string>();
    public void checker(string CurrentExhibhitorId)
    {
        if (!luckyDrawVisitedList.Contains(CurrentExhibhitorId))
        {

            Debug.Log("triggg" + CurrentExhibhitorId);
            for (int i = 0; i < ApiHandler.instance._listLuckyDrawExhibitorCollege.Count; i++)
            {
                if (ApiHandler.instance._listLuckyDrawExhibitorCollege[i].luckyDrawExhibitorId == CurrentExhibhitorId)
                {
                    luckyDrawVisitedList.Add(CurrentExhibhitorId);

                    images[count].SetActive(false);
                    count++;
                    Debug.Log(CurrentExhibhitorId);
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
            Debug.Log("My Lucky Cupon is :  " + callBack);

        }));
    }

}
