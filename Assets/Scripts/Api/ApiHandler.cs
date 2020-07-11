﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    private string metaDataUrl = "https://vxrvenue.herokuapp.com/getEventMetaData?eventName=Edu%20Fair%20-%20July%202020";
    public static ApiHandler instance = null;
    public static ApiHandler Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        StartCoroutine(stallDetaitsParser());
    }

    IEnumerator stallDetaitsParser()
    {
        UnityWebRequest apiRequest = UnityWebRequest.Get(metaDataUrl);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("metaDataUrl Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
            registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;
            _metaDataUrlContent = new metaDataUlData();


            _metaDataUrlContent.eventName = registrationResponse["name"].ToString();
            _metaDataUrlContent.eventId = registrationResponse["eventId"].ToString();
            _metaDataUrlContent.eventType = registrationResponse["type"].ToString();
            _metaDataUrlContent.eventDestription = registrationResponse["description"].ToString();
            _metaDataUrlContent.eventLogoUrl = registrationResponse["logo"].ToString();
            _metaDataUrlContent.eventTagline = registrationResponse["tagline"].ToString();

            Dictionary<string, object> innerDict = new Dictionary<string, object>();
            innerDict = registrationResponse["layout"] as Dictionary<string, object>;

            _metaDataUrlContent.layoutName = innerDict["layoutName"].ToString();
            _metaDataUrlContent.numberOfBooths = innerDict["noOfBooths"].ToString();

            innerDict = new Dictionary<string, object>();
            //  innerDataParse = registrationResponse["metadata"] as Dictionary<string, object>;

            Dictionary<string, object> innerDict2 = new Dictionary<string, object>();
            innerDict2 = registrationResponse["metadata"] as Dictionary<string, object>;

            List<object> innerListObj = new List<object>();
            innerListObj = innerDict2["media"] as List<object>;

            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;

                _metaDataUrlContent.mediaName = innerDict["name"].ToString();
                _metaDataUrlContent.mediaUrl = innerDict["url"].ToString();
                _metaDataUrlContent.mediaType = innerDict["type"].ToString();
                _metaDataUrlContent.mediaCategory = innerDict["category"].ToString();

            }

            innerListObj = new List<object>();
            innerListObj = innerDict2["tags"] as List<object>;

            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;
                // _metaDataUrlContent.mediaName = innerDataParse["name"].ToString();
            }

            innerListObj = new List<object>();
            innerListObj = innerDict2["themes"] as List<object>;

            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;
                // _metaDataUrlContent.mediaName = innerDataParse["name"].ToString();
            }

            _metaDataUrlContent.startDate = registrationResponse["startDate"].ToString();
            _metaDataUrlContent.endDate = registrationResponse["endDate"].ToString();
            _metaDataUrlContent.startTime = registrationResponse["startTime"].ToString();
            _metaDataUrlContent.endTime = registrationResponse["endTime"].ToString();

            innerListObj = new List<object>();
            innerListObj = registrationResponse["exhibitors"] as List<object>;

            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;
                _metaDataUrlContent.exhibhitorsName = innerDict["name"].ToString();
                _metaDataUrlContent.exhibhitorsEventId = innerDict["eventId"].ToString();
                _metaDataUrlContent.exhibhitorsType = innerDict["type"].ToString();
                _metaDataUrlContent.exhibhitorsDescription = innerDict["description"].ToString();
                _metaDataUrlContent.exhibhitorsPhoneNumber = innerDict["phoneNo"].ToString();

                Dictionary<string, object> innerDict3 = new Dictionary<string, object>();
                innerDict3 = innerDict["address"] as Dictionary<string, object>;
                _metaDataUrlContent.exhibhitorsAddressStreet = innerDict3["street"].ToString();
                _metaDataUrlContent.exhibhitorsAddressCity = innerDict3["city"].ToString();
                _metaDataUrlContent.exhibhitorsAddressState = innerDict3["state"].ToString();
                _metaDataUrlContent.exhibhitorsAddressZipCode = innerDict3["zipcode"].ToString();

                _metaDataUrlContent.exhibhitorsLogoUrl = innerDict["logo"].ToString();
                _metaDataUrlContent.exhibhitorsTagLine = innerDict["tagline"].ToString();

                List<object> innerListObj1 = new List<object>();
                innerListObj1 = innerDict["booths"] as List<object>;
                Dictionary<string, object> innerDict4;
                foreach (object obj1 in innerListObj1)
                {
                    innerDict3 = new Dictionary<string, object>();
                    innerDict3 = obj1 as Dictionary<string, object>;

                    _metaDataUrlContent.exhibhitorsBoothNumber.Add(innerDict3["number"].ToString());
                    _metaDataUrlContent.exhibhitorsBoothType.Add(innerDict3["type"].ToString());
                    _metaDataUrlContent.exhibhitorsBoothModel.Add(innerDict3["model"].ToString());
                    _metaDataUrlContent.exhibhitorsBoothBaseColor.Add(innerDict3["baseColor"].ToString());

                    List<object> innerListObj2 = new List<object>();
                    innerListObj2 = innerDict3["amenities"] as List<object>;

                    innerDict4 = new Dictionary<string, object>();

                    foreach (object obj2 in innerListObj2)
                    {
                        innerDict4 = new Dictionary<string, object>();
                        innerDict3 = obj2 as Dictionary<string, object>;
                        _metaDataUrlContent.exhibhitorsBoothAmenitiesName.Add(innerDict3["name"].ToString());
                        _metaDataUrlContent.exhibhitorsBoothAmenitiesType.Add(innerDict3["type"].ToString());
                        _metaDataUrlContent.exhibhitorsBoothAmenitiesSourceUrl.Add(innerDict3["source"].ToString());


                    }

                }
                Dictionary<string, object> innerDict5 = new Dictionary<string, object>();
                innerDict5 = innerDict["metadata"] as Dictionary<string, object>;
                List<object> innerListObj3 = new List<object>();
                innerListObj3 = innerDict5["media"] as List<object>;
                innerDict4 = new Dictionary<string, object>();
                foreach (object obj3 in innerListObj3)
                {
                    innerDict4 = new Dictionary<string, object>();
                    innerDict4 = obj3 as Dictionary<string, object>;

                    _metaDataUrlContent.exhibhitorsMetaDataMediaName = innerDict4["name"].ToString();
                    _metaDataUrlContent.exhibhitorsMetaDataMediaUrl = innerDict4["url"].ToString();
                    _metaDataUrlContent.exhibhitorsMetaDataMediaType = innerDict4["type"].ToString();
                    _metaDataUrlContent.exhibhitorsMetaDataMediaCategory = innerDict4["category"].ToString();

                }
                _metaDataUrlContent.exhibhitorsMetaDataStartDate = innerDict["startDate"].ToString();
                _metaDataUrlContent.exhibhitorsMetaDataEndDate = innerDict["endDate"].ToString();
                _metaDataUrlContent.exhibhitorsMetaDataStartTime = innerDict["startTime"].ToString();
                _metaDataUrlContent.exhibhitorsMetaDataEndTime = innerDict["endTime"].ToString();
                Debug.Log(innerDict["startDate"]);

            }
        }
    }
    // Debug.Log(innerDataParse["name"]);
    [SerializeField]
    public metaDataUlData _metaDataUrlContent;
}



[System.Serializable]
public class metaDataUlData
{
    public string eventName;
    public string eventId;
    public string eventType;
    public string eventDestription;

    public string eventLogoUrl;
    public string eventTagline;

    public string layoutName;
    public string numberOfBooths;

    public string mediaName;
    public string mediaUrl;
    public string mediaType;
    public string mediaCategory;

    public string startDate;
    public string endDate;
    public string startTime;
    public string endTime;

    public string exhibhitorsName;
    public string exhibhitorsEventId;
    public string exhibhitorsType;
    public string exhibhitorsDescription;
    public string exhibhitorsPhoneNumber;

    public string exhibhitorsAddressStreet;
    public string exhibhitorsAddressCity;
    public string exhibhitorsAddressState;
    public string exhibhitorsAddressZipCode;

    public string exhibhitorsLogoUrl;
    public string exhibhitorsTagLine;

    public List<string> exhibhitorsBoothNumber = new List<string>();
    public List<string> exhibhitorsBoothType = new List<string>();
    public List<string> exhibhitorsBoothModel = new List<string>();
    public List<string> exhibhitorsBoothBaseColor = new List<string>();

    public List<string> exhibhitorsBoothAmenitiesName = new List<string>();
    public List<string> exhibhitorsBoothAmenitiesType = new List<string>();
    public List<string> exhibhitorsBoothAmenitiesSourceUrl = new List<string>();

    public string exhibhitorsMetaDataMediaName;
    public string exhibhitorsMetaDataMediaUrl;
    public string exhibhitorsMetaDataMediaType;
    public string exhibhitorsMetaDataMediaCategory;

    public string exhibhitorsMetaDataStartDate;
    public string exhibhitorsMetaDataEndDate;
    public string exhibhitorsMetaDataStartTime;
    public string exhibhitorsMetaDataEndTime;
}