using System;
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

public enum apiResponseType
{
    SUCCESS,
    FAIL,
    SEVER_ERROR
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
    private string logoutUrl = urlHeader + "/v1/logout";
    [SerializeField]
    private string generateTokenUrl = urlHeader + "/v1/getLuckyDrawCoupon";
    [SerializeField]
    private string getLuckyDrayExhibitorsUrl = urlHeader + "/v1/getLuckDrawExhibitors";

    [SerializeField]
    public List<LuckyDrawExhibitorCollege> _listLuckyDrawExhibitorCollege = new List<LuckyDrawExhibitorCollege>();

    public string luckyCupon;



    [SerializeField]
    public string userTokeId;

    [SerializeField]
    private string registerUserUrl = "https://vxrvenue.herokuapp.com/v1/registerUser";

    [SerializeField]
    private string getRefferalCode = urlHeader + "/v1/getRefferalCode";

    [SerializeField]
    public metaDataUrlData _metaDataUrlContent;

    [SerializeField]
    public List<UserActivity> _userActivityList = new List<UserActivity>();


    public List<string> usersActivityMaintainList = new List<string>();

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
    public IEnumerator internetcheck(Action<bool> callBack)
    {
        UnityWebRequest internetcheckurl = UnityWebRequest.Get("https://www.google.com/");

        yield return internetcheckurl.SendWebRequest();

        if (internetcheckurl.isNetworkError || internetcheckurl.isHttpError)
        {
            Debug.Log(internetcheckurl.error);
            callBack(false);
        }
        else
        {
            callBack(true);
        }
    }
    private void Start()
    {

        // StartCoroutine(ApiHandler.instance.GetUserActivity());

        //for (int i = 0; i < ApiHandler.instance._userActivityList.Count; i++)
        //{
        //    usersActivityMaintainList.Add(ApiHandler.instance._userActivityList[i].userToken + ApiHandler.instance._userActivityList[i].activityType + ApiHandler.instance._userActivityList[i].boothName);
        //}
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


    public IEnumerator authenticateUser(Action<APICallHandlerResponse> callBack)
    {
        APICallHandlerResponse _apiCallHandlerResponse = new APICallHandlerResponse();


        Dictionary<string, string> urlHeaderKeys = new Dictionary<string, string>();
        urlHeaderKeys.Add("userId", "vickynexus.5@gmail.com");
        urlHeaderKeys.Add("password", "welcome1");
        UnityWebRequest apiRequest = UnityWebRequest.Post(authenticateUserUrl, urlHeaderKeys);

        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);

            _apiCallHandlerResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _apiCallHandlerResponse.responseMessage = "Server failed to process this info";
            callBack(_apiCallHandlerResponse);
        }
        else
        {
            Debug.Log("authenticateUserUrl Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> authenticationDataDict = new Dictionary<string, object>();
            authenticationDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            if ((authenticationDataDict["success"].ToString()).ToLower() == "true")
            {
                if ((authenticationDataDict["isAuthenticated"].ToString()).ToLower() == "true")
                {
                    userTokeId = authenticationDataDict["token"].ToString();
                    _apiCallHandlerResponse._apiResponseType = apiResponseType.SUCCESS;
                    _apiCallHandlerResponse.responseMessage = "User is Authenticated";
                    callBack(_apiCallHandlerResponse);
                }
                else
                {
                    _apiCallHandlerResponse._apiResponseType = apiResponseType.FAIL;
                    _apiCallHandlerResponse.responseMessage = "User is not Authenticated";
                    callBack(_apiCallHandlerResponse);
                }
            }
            else
            {
                _apiCallHandlerResponse._apiResponseType = apiResponseType.FAIL;
                _apiCallHandlerResponse.responseMessage = "Server failed to complete the process at this time.";
                callBack(_apiCallHandlerResponse);
            }
        }
    }

    public IEnumerator SaveUserActivity(userActivityType activityType, string activityData, string boothName, string boothId, string exhibitorId, Action<setUserDataResponse> callBack)
    {
        setUserDataResponse _setUserDataResponse = new setUserDataResponse();


        //  string str = userTokeId + activityType + boothName;
        //  if (!usersActivityMaintainList.Contains(str))
        {
            //   usersActivityMaintainList.Add(userTokeId + activityType + boothName);
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

                _setUserDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
                _setUserDataResponse.responseMessage = apiRequest.error;
                callBack(_setUserDataResponse);
            }
            else
            {
                Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
                registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;
                Debug.Log(registrationResponse["successs"]);

                if ((registrationResponse["successs"].ToString()).ToLower() == "true")
                {
                    _setUserDataResponse._apiResponseType = apiResponseType.SUCCESS;
                    _setUserDataResponse.responseMessage = (registrationResponse["successs"].ToString()).ToLower();
                    callBack(_setUserDataResponse);
                }
                else
                {
                    _setUserDataResponse._apiResponseType = apiResponseType.FAIL;
                    _setUserDataResponse.responseMessage = (registrationResponse["successs"].ToString()).ToLower();
                    callBack(_setUserDataResponse);
                }
            }
        }
    }

    public IEnumerator GetUserActivity(Action<getUserActivityResponse> callBack)
    {
        getUserActivityResponse _getUserActivityResponse = new getUserActivityResponse();

        UnityWebRequest apiRequest = UnityWebRequest.Get(getUserActivityUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            _getUserActivityResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getUserActivityResponse.responseMessage = apiRequest.error;
            callBack(_getUserActivityResponse);
        }
        else
        {
            Debug.Log(apiRequest.downloadHandler.text);
            Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
            registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log(registrationResponse["success"]);

            if ((registrationResponse["success"].ToString()).ToLower() == "true")
            {
                _userActivityList = new List<UserActivity>();

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

                _getUserActivityResponse._apiResponseType = apiResponseType.SUCCESS;
                _getUserActivityResponse.responseMessage = "success";
                _getUserActivityResponse._userActivityList = _userActivityList;
                callBack(_getUserActivityResponse);
            }
            else
            {
                _getUserActivityResponse._apiResponseType = apiResponseType.FAIL;
                _getUserActivityResponse.responseMessage = "Cannot Get User Activity Right Now.";
                callBack(_getUserActivityResponse);
            }
        }
    }
    //Get luckydraw stalls
    public IEnumerator LuckyDrawExhibhitorList(Action<luckyDrawExhibitorResponse> callBack)
    {
        luckyDrawExhibitorResponse _luckyDrawExhibitorResponse = new luckyDrawExhibitorResponse();

        UnityWebRequest apiRequest = UnityWebRequest.Get(getLuckyDrayExhibitorsUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            _luckyDrawExhibitorResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _luckyDrawExhibitorResponse.responseMessage = apiRequest.error;
            callBack(_luckyDrawExhibitorResponse);
        }
        else
        {
            Debug.Log("metaDataUrl Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("Logout  " + registerUserDataDict["success"]);

            List<object> listObj = new List<object>();
            listObj = registerUserDataDict["exhibitors"] as List<object>;

            Dictionary<string, object> innerDict = new Dictionary<string, object>();

            _listLuckyDrawExhibitorCollege = new List<LuckyDrawExhibitorCollege>();

            foreach (object item in listObj)
            {
                innerDict = item as Dictionary<string, object>;

                Debug.Log("LuckyDrawExhibhitorList  " + innerDict["name"]);
                LuckyDrawExhibitorCollege _luckyDrawDetails = new LuckyDrawExhibitorCollege();
                _luckyDrawDetails.luckyDrawExhibitorId = innerDict["name"].ToString();
                _luckyDrawDetails.boothId = innerDict["boothId"].ToString();
                _listLuckyDrawExhibitorCollege.Add(_luckyDrawDetails);
            }

            _luckyDrawExhibitorResponse._apiResponseType = apiResponseType.SUCCESS;
            _luckyDrawExhibitorResponse.responseMessage = "success";
            _luckyDrawExhibitorResponse._luckyDrawExhibitorList = _listLuckyDrawExhibitorCollege;
            callBack(_luckyDrawExhibitorResponse);
        }
    }
    // Generate Cupon once all stalls reached
    public IEnumerator GenerateLuckyCupon(Action<generateLuckyDrawCode> cuponCode)
    {
        generateLuckyDrawCode _generateLuckyDrawCode = new generateLuckyDrawCode();

        UnityWebRequest apiRequest = UnityWebRequest.Get(generateTokenUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            _generateLuckyDrawCode._apiResponseType = apiResponseType.SEVER_ERROR;
            _generateLuckyDrawCode.responseMessage = apiRequest.error;
            cuponCode(_generateLuckyDrawCode);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("Logout  " + registerUserDataDict["success"]);

            if ((registerUserDataDict["success"].ToString().ToLower() == "true"))
            {
                luckyCupon = registerUserDataDict["couponCode"].ToString();

                _generateLuckyDrawCode._apiResponseType = apiResponseType.SEVER_ERROR;
                _generateLuckyDrawCode.responseMessage = luckyCupon;
                cuponCode(_generateLuckyDrawCode);
            }
            else
            {
                _generateLuckyDrawCode._apiResponseType = apiResponseType.FAIL;
                _generateLuckyDrawCode.responseMessage = "Code not generated at this time.";
                cuponCode(_generateLuckyDrawCode);
            }




            Debug.Log("luckyCupon  " + luckyCupon);
        }
    }

    public IEnumerator GenerateRefferalCode(Action<generateRefferalCode> callBack)
    {
        generateRefferalCode generateRefferalCode = new generateRefferalCode();

        UnityWebRequest apiRequest = UnityWebRequest.Get(getRefferalCode);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            generateRefferalCode._apiResponseType = apiResponseType.SEVER_ERROR;
            generateRefferalCode.responseMessage = apiRequest.error;
            callBack(generateRefferalCode);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("Logout  " + registerUserDataDict["success"]);

            if ((registerUserDataDict["success"].ToString()).ToLower() == "true")
            {
                generateRefferalCode._apiResponseType = apiResponseType.SUCCESS;
                generateRefferalCode.responseMessage = registerUserDataDict["refferalCode"].ToString();
                callBack(generateRefferalCode);
            }
            else
            {
                generateRefferalCode._apiResponseType = apiResponseType.FAIL;
                generateRefferalCode.responseMessage = "Refferal Code uable to generate at this time";
                callBack(generateRefferalCode);
            }
        }

        //Use this method to call 
        //StartCoroutine(ApiHandler.instance.GenerateRefferalCode((callBack) =>
        //{
        //    Debug.Log("My Refferal Code is :  " + callBack);

        //}));
    }

    public IEnumerator logoutApiCall(Action<logoutResponse> callBack)
    {
        logoutResponse _logoutResponse = new logoutResponse();

        UnityWebRequest apiRequest = UnityWebRequest.Get(logoutUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            _logoutResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _logoutResponse.responseMessage = apiRequest.error;
            callBack(_logoutResponse);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("Logout  " + registerUserDataDict["success"]);

            if ((registerUserDataDict["success"].ToString()).ToLower() == "true")
            {
                _logoutResponse._apiResponseType = apiResponseType.SUCCESS;
                _logoutResponse.responseMessage = (registerUserDataDict["success"].ToString()).ToLower();
                callBack(_logoutResponse);
            }
            else
            {
                _logoutResponse._apiResponseType = apiResponseType.FAIL;
                _logoutResponse.responseMessage = (registerUserDataDict["success"].ToString()).ToLower();
                callBack(_logoutResponse);
            }
        }
    }

    public IEnumerator sendEmail(string emailSubject, string emailMessage, string eventId, string exhibitorId, Action<mailSendResponse> callBack)
    {
        mailSendResponse _mailSendResponse = new mailSendResponse();

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
            _mailSendResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _mailSendResponse.responseMessage = apiRequest.error;
            callBack(_mailSendResponse);
        }
        else
        {
            Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
            registerUserDataDict = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;

            Debug.Log("RegisterUSer  " + registerUserDataDict["success"]);
            if ((registerUserDataDict["success"].ToString()).ToLower() == "true")
            {
                _mailSendResponse._apiResponseType = apiResponseType.SUCCESS;
                _mailSendResponse.responseMessage = (registerUserDataDict["success"].ToString()).ToLower();
                callBack(_mailSendResponse);
            }
            else
            {
                _mailSendResponse._apiResponseType = apiResponseType.FAIL;
                _mailSendResponse.responseMessage = (registerUserDataDict["success"].ToString()).ToLower();
                callBack(_mailSendResponse);
            }

        }
    }

    public IEnumerator stallDetaitsParser(Action<getMetaDataResponse> callBack)
    {
        getMetaDataResponse _getMetaDataResponse = new getMetaDataResponse();

        UnityWebRequest apiRequest = UnityWebRequest.Get(metaDataUrl);
        apiRequest.SetRequestHeader("Content-Type", "application/json");
        apiRequest.SetRequestHeader("Authorization", "Bearer " + userTokeId);
        yield return apiRequest.SendWebRequest();

        if (apiRequest.isNetworkError || apiRequest.isHttpError)
        {
            Debug.Log(apiRequest.error);
            _getMetaDataResponse._apiResponseType = apiResponseType.SEVER_ERROR;
            _getMetaDataResponse.responseMessage = apiRequest.error;
            callBack(_getMetaDataResponse);
        }
        else
        {
            Debug.Log("metaDataUrl Response : " + apiRequest.downloadHandler.text);

            Dictionary<string, object> registrationResponse = new Dictionary<string, object>();
            registrationResponse = MiniJSON.Json.Deserialize(apiRequest.downloadHandler.text) as Dictionary<string, object>;
            _metaDataUrlContent = new metaDataUrlData();

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

                //  _collegeDataClass.exhibhitorsWebsiteUrl = innerDict["website"].ToString();

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
#if UNITY_WEBGL && !UNITY_EDITOR
        BrowserCommunicationManager.instance.callLoaded();
#endif
        _getMetaDataResponse._apiResponseType = apiResponseType.SUCCESS;
        _getMetaDataResponse.responseMessage = "Success";
        _getMetaDataResponse._metaDataUrlData = _metaDataUrlContent;
        callBack(_getMetaDataResponse);


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

    public string exhibhitorsWebsiteUrl;

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
public class metaDataUrlData
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

[System.Serializable]
public class LuckyDrawExhibitorCollege
{
    public string luckyDrawExhibitorId;
    public string boothId;
}

public class APICallHandlerResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public bool isAuthenticated;
}


public class getMetaDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public metaDataUrlData _metaDataUrlData;
}

public class luckyDrawExhibitorResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public List<LuckyDrawExhibitorCollege> _luckyDrawExhibitorList;
}


public class mailSendResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
}


public class logoutResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
}

public class generateRefferalCode
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
}


public class generateLuckyDrawCode
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
}


public class getUserActivityResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
    public List<UserActivity> _userActivityList;
}


public class setUserDataResponse
{
    public apiResponseType _apiResponseType;
    public string responseMessage;
}