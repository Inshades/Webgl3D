using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum userActivityType
{
    DOWNLOAD_BROUCHER,
    VIEW_BUSINESSCARD,
    VIEW_VIDEO,
    VISIT_BOOTH,
    CHAT,
}

public class ApiHandler : MonoBehaviour
{
    private static string urlHeader = "https://vxrvenue.herokuapp.com";
    [SerializeField]
    private string authenticateUserUrl = urlHeader + "/v1/authenticate";
    [SerializeField]
    private string getUserActivityUrl = urlHeader + "/v1/activity";
    [SerializeField]
    private string SaveUserActivityUrl = urlHeader + "/v1/activity";
    [SerializeField]
    private string emailUrl = urlHeader + "/v1/sendEmail";
    [SerializeField]
    private string metaDataUrl = urlHeader + "/v2/getEventMetaData?eventId=eduFair2020";
    [SerializeField]
    private string userTokeId;

    [SerializeField]
    private string registerUserUrl = "https://vxrvenue.herokuapp.com/v1/registerUser";

    [SerializeField]
    public metaDataUlData _metaDataUrlContent;


    public List<UserActivity> _userActivityList = new List<UserActivity>();

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
        StartCoroutine(authenticateUser());
        // StartCoroutine(stallDetaitsParser());
    }

    IEnumerator RegisterNewUser()
    {
        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("name", "VigneshG");
        urlHeaderKeys.Add("emailId", "vickynexus.5@gmail.com");
        urlHeaderKeys.Add("phoneNo", "9884290609");
        urlHeaderKeys.Add("userType", "Visitor");
        urlHeaderKeys.Add("eventName", "Edu Fair 2020");
        urlHeaderKeys.Add("engineeringCutOff", "");
        urlHeaderKeys.Add("password", "U2FsdGVkX1+T+YtgYju0H++zWUEbZveZS7fYEiBEi40=");//welcome1
        urlHeaderKeys.Add("roleName", "Visitor");

        UnityWebRequest apiRequest = UnityWebRequest.Post(registerUserUrl, urlHeaderKeys);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("RegisterUSer  " + registerUserDataDict["success"]);
        }
    }


    IEnumerator authenticateUser()
    {
        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("userId", "vickynexus.5@gmail.com");
        urlHeaderKeys.Add("password", "welcome1");
        UnityWebRequest apiRequest = UnityWebRequest.Post(authenticateUserUrl, urlHeaderKeys);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            Debug.Log("authenticateUserUrl Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> authenticationDataDict = new Dictionary<string, object>();
            authenticationDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            //if (authenticationDataDict["isAuthenticated"].ToString() == "true")
            {
                userTokeId = authenticationDataDict["token"].ToString();
            }

            StartCoroutine(stallDetaitsParser());
            //  StartCoroutine(SaveUserActivity(userActivityType.DOWNLOAD_BROUCHER, "https://helpx.adobe.com/acrobat/using/links-attachments-pdfs.html", "Brochure Download", "https://helpx.adobe.com/acrobat/using/links-attachments-pdfs.html"));
              StartCoroutine(GetUserActivity());

        }
    }

    public IEnumerator SaveUserActivity(userActivityType activityType, string activityData, string boothName, string boothId, string exhibitorId)
    {
        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("activityType", activityType.ToString());
        urlHeaderKeys.Add("activityData", activityData);
        urlHeaderKeys.Add("boothName", boothName);
        urlHeaderKeys.Add("boothId", boothId);
        urlHeaderKeys.Add("exhibitorId", exhibitorId);
        urlHeaderKeys.Add("token", userTokeId);

        UnityWebRequest apiRequest = UnityWebRequest.Post(SaveUserActivityUrl, urlHeaderKeys);
        //apiRequest.SetRequestHeader("Content-Type", "application/json");
        //apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
            registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;
            Debug.Log(registrationResponse["successs"]);


        }
    }

    public IEnumerator GetUserActivity()
    {
        UnityWebRequest apiRequest = UnityWebRequest.Get(getUserActivityUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            _userActivityList = new List<UserActivity>();

            Debug.Log(apiRequest.downloadHandler.text);
            Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
            registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(registrationResponse["success"]);

            List<object> innerListObj = new List<object>();
            innerListObj = registrationResponse["useractivities"] as List<object>;
            Dictionary<string, object> innerDict;
            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;

                UserActivity _userActivity = new UserActivity();
                _userActivity.activityType = innerDict["activityType"].ToString();
                _userActivity.activityData = innerDict["activityData"].ToString();
                _userActivity.boothName = innerDict["boothName"].ToString();
                _userActivity.boothId = innerDict["boothId"].ToString();
                _userActivity.userToken = innerDict["token"].ToString();
                _userActivity.userId = innerDict["userId"].ToString();
                _userActivity.eventName = innerDict["eventName"].ToString();
                _userActivity.activityTime = innerDict["activityTime"].ToString();

                _userActivityList.Add(_userActivity);
            }
        }
    }

    public IEnumerator sendEmail(string emailSubject, string emailMessage, string eventId, string exhibitorId)
    {
        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("subject", emailSubject);
        urlHeaderKeys.Add("message", emailMessage);
        urlHeaderKeys.Add("eventId", eventId);
        urlHeaderKeys.Add("exhibitorId", exhibitorId);
        urlHeaderKeys.Add("token", userTokeId);

        UnityWebRequest apiRequest = UnityWebRequest.Post(registerUserUrl, urlHeaderKeys);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("RegisterUSer  " + registerUserDataDict["success"]);
        }
    }
    IEnumerator stallDetaitsParser()
    {
        UnityWebRequest apiRequest = UnityWebRequest.Get(metaDataUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
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
            }

            innerListObj = new List<object>();
            innerListObj = innerDict2["themes"] as List<object>;

            foreach (object obj in innerListObj)
            {
                innerDict = new Dictionary<string, object>();
                innerDict = obj as Dictionary<string, object>;
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

                collegeDatas _collegeDataClass = new collegeDatas();
                _collegeDataClass.exhibhitorsName = innerDict["name"].ToString();
                _collegeDataClass.exhibhitorsEventId = innerDict["eventId"].ToString();
                _collegeDataClass.exhibhitorsId = innerDict["exhibitorId"].ToString();
                _collegeDataClass.exhibhitorsType = innerDict["type"].ToString();
                _collegeDataClass.exhibhitorsDescription = innerDict["description"].ToString();
                _collegeDataClass.exhibhitorsPhoneNumber = innerDict["phoneNo"].ToString();

                Dictionary<string, object> innerDict3 = new Dictionary<string, object>();
                innerDict3 = innerDict["address"] as Dictionary<string, object>;

                _collegeDataClass.exhibhitorsAddressStreet = innerDict3["street"].ToString();
                _collegeDataClass.exhibhitorsAddressCity = innerDict3["city"].ToString();
                _collegeDataClass.exhibhitorsAddressState = innerDict3["state"].ToString();
                _collegeDataClass.exhibhitorsAddressZipCode = innerDict3["zipcode"].ToString();

                _collegeDataClass.exhibhitorsLogoUrl = innerDict["logo"].ToString();
                _collegeDataClass.exhibhitorsTagLine = innerDict["tagline"].ToString();


                List<object> innerListObj1 = new List<object>();
                innerListObj1 = innerDict["booths"] as List<object>;
                Dictionary<string, object> innerDict4;
                foreach (object obj1 in innerListObj1)
                {
                    innerDict3 = new Dictionary<string, object>();
                    innerDict3 = obj1 as Dictionary<string, object>;

                    _collegeDataClass.exhibhitorsBoothId.Add(innerDict3["id"].ToString());
                    _collegeDataClass.exhibhitorsBoothSubVenue.Add(innerDict3["subVenue"].ToString());
                    _collegeDataClass.exhibhitorsBoothNumber.Add(innerDict3["number"].ToString());
                    _collegeDataClass.exhibhitorsBoothType.Add(innerDict3["type"].ToString());
                    _collegeDataClass.exhibhitorsBoothModel.Add(innerDict3["model"].ToString());
                    _collegeDataClass.exhibhitorsBoothBaseColor.Add(innerDict3["baseColor"].ToString());

                    List<object> innerListObj2 = new List<object>();
                    innerListObj2 = innerDict3["amenities"] as List<object>;

                    innerDict4 = new Dictionary<string, object>();

                    foreach (object obj2 in innerListObj2)
                    {
                        innerDict4 = new Dictionary<string, object>();
                        innerDict3 = obj2 as Dictionary<string, object>;
                        collegeAmenities _collegeAmenitiesClass = new collegeAmenities();
                        _collegeAmenitiesClass.exhibhitorsBoothAmenitiesName = innerDict3["name"].ToString();
                        _collegeAmenitiesClass.exhibhitorsBoothAmenitiesType = innerDict3["type"].ToString();
                        _collegeAmenitiesClass.exhibhitorsBoothAmenitiesSourceUrl = innerDict3["source"].ToString();

                        _collegeDataClass._collegeAmenities.Add(_collegeAmenitiesClass);
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

                    exhibhitorsMetaDataMedia _exhibhitorsMetaDataMediaClass = new exhibhitorsMetaDataMedia();
                    _exhibhitorsMetaDataMediaClass.exhibhitorsMetaDataMediaName = innerDict4["name"].ToString();
                    _exhibhitorsMetaDataMediaClass.exhibhitorsMetaDataMediaUrl = innerDict4["url"].ToString();
                    _exhibhitorsMetaDataMediaClass.exhibhitorsMetaDataMediaType = innerDict4["type"].ToString();
                    _exhibhitorsMetaDataMediaClass.exhibhitorsMetaDataMediaCategory = innerDict4["category"].ToString();

                    _collegeDataClass._exhibhitorsMetaDataMediaClassList.Add(_exhibhitorsMetaDataMediaClass);
                }

                List<object> innerListObj4 = new List<object>();
                innerListObj3 = innerDict5["tags"] as List<object>;

                foreach (object obj3 in innerListObj3)
                {
                    _collegeDataClass.searchTags.Add(obj3.ToString());
                }

                _collegeDataClass.exhibhitorsMetaDataStartDate = innerDict["startDate"].ToString();
                _collegeDataClass.exhibhitorsMetaDataEndDate = innerDict["endDate"].ToString();
                _collegeDataClass.exhibhitorsMetaDataStartTime = innerDict["startTime"].ToString();
                _collegeDataClass.exhibhitorsMetaDataEndTime = innerDict["endTime"].ToString();
                _metaDataUrlContent._collegeDataClassList.Add(_collegeDataClass);
            }
        }

        StartCoroutine(Manager.instance.GenarateStalls());
       // StartCoroutine(Manager.instance.GenarateHall_1_Stalls());
        //StartCoroutine(Manager.instance.GenarateHall_2_Stalls());
        //StartCoroutine(Manager.instance.GenarateHall_3_Stalls());
    }


}
[System.Serializable]
public class collegeAmenities
{
    public string exhibhitorsBoothAmenitiesName;
    public string exhibhitorsBoothAmenitiesType;
    public string exhibhitorsBoothAmenitiesSourceUrl;
}
[System.Serializable]
public class exhibhitorsMetaDataMedia
{
    public string exhibhitorsMetaDataMediaName;
    public string exhibhitorsMetaDataMediaUrl;
    public string exhibhitorsMetaDataMediaType;
    public string exhibhitorsMetaDataMediaCategory;
}

[System.Serializable]
public class UserActivity
{
    public string activityType;
    public string activityData;
    public string boothName;
    public string boothId;
    public string userToken;
    public string userId;
    public string eventName;
    public string activityTime;
}

[System.Serializable]
public class collegeDatas
{
    public string exhibhitorsName;
    public string exhibhitorsEventId;
    public string exhibhitorsId;
    public string exhibhitorsType;
    public string exhibhitorsDescription;
    public string exhibhitorsPhoneNumber;

    public string exhibhitorsAddressStreet;
    public string exhibhitorsAddressCity;
    public string exhibhitorsAddressState;
    public string exhibhitorsAddressZipCode;

    public string exhibhitorsLogoUrl;
    public string exhibhitorsTagLine;

    public List<string> searchTags = new List<string>();

    public List<string> exhibhitorsBoothId = new List<string>();
    public List<string> exhibhitorsBoothSubVenue = new List<string>();
    public List<string> exhibhitorsBoothNumber = new List<string>();
    public List<string> exhibhitorsBoothType = new List<string>();
    public List<string> exhibhitorsBoothModel = new List<string>();
    public List<string> exhibhitorsBoothBaseColor = new List<string>();

    public List<collegeAmenities> _collegeAmenities = new List<collegeAmenities>();

    public List<exhibhitorsMetaDataMedia> _exhibhitorsMetaDataMediaClassList = new List<exhibhitorsMetaDataMedia>();

    public string exhibhitorsMetaDataStartDate;
    public string exhibhitorsMetaDataEndDate;
    public string exhibhitorsMetaDataStartTime;
    public string exhibhitorsMetaDataEndTime;
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

    [SerializeField]
    public List<collegeDatas> _collegeDataClassList = new List<collegeDatas>();
}