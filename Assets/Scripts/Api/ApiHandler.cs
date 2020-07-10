using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    
    private string metaDataUrl = "https://vxrvenue.herokuapp.com/getEventMetaData?eventName=Edu%20Fair%20-%20July%202020";

    public Text name;
    public Text eventId;
    public Text eventType;
    public Text description;


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

            metaDataUlData _metaDataUrlContent = new metaDataUlData();

            _metaDataUrlContent.eventName = registrationResponse["name"].ToString();
            _metaDataUrlContent.eventId = registrationResponse["eventId"].ToString();
            _metaDataUrlContent.eventType = registrationResponse["type"].ToString();
            _metaDataUrlContent.eventDestription = registrationResponse["description"].ToString();
            _metaDataUrlContent.eventLogoUrl = registrationResponse["logo"].ToString();
            _metaDataUrlContent.eventTagline = registrationResponse["tagline"].ToString();

            name.text = _metaDataUrlContent.eventName;
            eventId.text = _metaDataUrlContent.eventId;
            eventType.text = _metaDataUrlContent.eventType;
            description.text = _metaDataUrlContent.eventDestription;


            Dictionary<string, object> innerDataParse = new Dictionary<string, object>();
            innerDataParse = registrationResponse["layout"] as Dictionary<string, object>;

            _metaDataUrlContent.layoutName = innerDataParse["layoutName"].ToString();
            _metaDataUrlContent.numberOfBooths = innerDataParse["noOfBooths"].ToString();

            innerDataParse = new Dictionary<string, object>();
            //  innerDataParse = registrationResponse["metadata"] as Dictionary<string, object>;

            Dictionary<string, object> innerListData =new Dictionary<string, object>();
            innerListData =registrationResponse["metadata"] as Dictionary<string, object>;

            List<Dictionary<string, object>> innerjsonData = new List<Dictionary<string, object>>();
            innerjsonData = innerListData["media"] as List<Dictionary<string, object>>;

            // Debug.Log(innerjsonData[0]["name"]);

           
        }
    }
}
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

}