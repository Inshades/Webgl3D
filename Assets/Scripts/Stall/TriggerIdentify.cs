using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerIdentify : MonoBehaviour
{
    public GameObject trigger;

    public StallUIManager stall_uimanager;
    public luckydraw luckydraw_key;
    public GameObject Canvas;

    private void OnEnable()
    {

        Canvas = GameObject.Find("Canvas");
        stall_uimanager = GameObject.Find("StallUIManager").GetComponent<StallUIManager>();
        luckydraw_key = GameObject.Find("StallUIManager").GetComponent<luckydraw>();
    }
    private void OnTriggerEnter(Collider other)
    {

        //B1_1
        Canvas.SetActive(true);
        //  Debug.Log("trigger");
        Canvas.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log(transform.parent.GetSiblingIndex() + "get sibiling");

        int key = transform.parent.GetSiblingIndex();
        

        luckydraw_key.checker(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0].ToString());
        Debug.Log("key" + key + "boothid" + ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0].ToString());

        //  string triggerName = trigger.name;
        //  triggerName = triggerName.Substring(triggerName.IndexOf("_") + 1, triggerName.IndexOf("(") - (triggerName.IndexOf("_") + 1));
        // int key = int.Parse(triggerName);
        //   key = key - 1;
        stall_uimanager.Setkey(key);
     //   setUserActivity(userActivityType.VISIT_BOOTH, "Visit Booth", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0], ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsId);

        StartCoroutine(ApiHandler.instance.internetcheck((callBack) =>
        {
            if(callBack)
            {

                setUserActivity(userActivityType.VISIT_BOOTH, "Visit Booth", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0], ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsId);

            }
        }));
    }

    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
        stall_uimanager.videoSelectPanel.SetActive(false);
        stall_uimanager.infoPanel.SetActive(false);
        stall_uimanager.emailPanel.SetActive(false);
        stall_uimanager.chatPanel.SetActive(false);
        stall_uimanager.businessCardPanel.SetActive(false);
        stall_uimanager.broucherPanel.SetActive(false);
        int key = transform.parent.GetSiblingIndex();
        stall_uimanager.Setkey(key);

      //  setUserActivity(userActivityType.VISIT_BOOTH, "Booth Exit", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0], ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsId);

        StartCoroutine(ApiHandler.instance.internetcheck((callBack) =>
        {
            if (callBack)
            {
                setUserActivity(userActivityType.VISIT_BOOTH, "Booth Exit", ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsName, ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsBoothId[0], ApiHandler.instance._metaDataUrlContent._collegeDataClassList[key].exhibhitorsId);

            }
        }));

    }
    void setUserActivity(userActivityType _userActivity, string activityData, string boothName, string boothId, string exhibitorId)
    {
        StartCoroutine(ApiHandler.instance.SaveUserActivity(_userActivity, activityData, boothName, boothId, exhibitorId, (callBack) =>
        {
            switch (callBack._apiResponseType)
            {
                case apiResponseType.SUCCESS:
                    break;

                case apiResponseType.FAIL:

                    break;
                case apiResponseType.SEVER_ERROR:

                    break;
            }
        }));
    }
}
