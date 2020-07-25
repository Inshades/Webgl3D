/*
 * This Script is responsible for communicating with the browser
 */
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BrowserCommunicationManager : MonoBehaviour
{
    public static BrowserCommunicationManager instance = null;
    public static BrowserCommunicationManager Instance
    {
        get { return instance; }
    }

    public Text nameText;
    public Text eventIdText;
    public Text userIdText;
    public Text genderText;
    public Text tokenText;

    // Import the JSLib as following. Make sure the
    // names match with the JSLib file we've just created.

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void logout(string logout);
    private static extern void loaded();
#endif
    // Then create a function that is going to trigger
    // the imported function from our JSLib.

    public void CallLogout()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        logout("logout");
#endif
    }

    public void callLoaded()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        loaded();
#endif
    }

    public void login(string jsonString)
    {
        Dictionary<string, object> registerUserDataDict = new Dictionary<string, object>();
        registerUserDataDict = MiniJSON.Json.Deserialize(jsonString) as Dictionary<string, object>;

        Debug.Log("Name :  " + registerUserDataDict["name"]);

        nameText.text = registerUserDataDict["name"].ToString();
        eventIdText.text = registerUserDataDict["eventID"].ToString();
        userIdText.text = registerUserDataDict["userID"].ToString();
        genderText.text = registerUserDataDict["gender"].ToString();
        tokenText.text = registerUserDataDict["token"].ToString();
    }
}

/*
{
	"name": "Adheep M",
	"eventID": "eduigui2020",
	"userID": "adheep@ymail.com",
	"gender": "male",
	"token": "eyJhbGciOiJSUzUxMiIsInR5cCI6IkpXVCIsImtpZCI6InJzYV9wcml2X2tleSJ9.eyJfaWQiOiI1ZjBmNzEzYjM5MjU4ZDAwMDRhMzY2MTEiLCJuYW1lIjoiQWRoZWVwIE1vaGFtZWQiLCJlbWFpbElkIjoiYWRoZWVwQHltYWlsLmNvbSIsInBob25lTm8iOiIrOTEgOTk3NjkyMDI4NSIsInVzZXJUeXBlIjoiUGFyZW50IiwiZXZlbnROYW1lIjoiRWR1IEZhaXIgMjAyMCIsImVuZ2luZWVyaW5nQ3V0T2ZmIjoiMTk0IiwicmVnaXN0cmF0aW9uVGltZSI6MTU5NDg0NzU0NzQ3NSwicm9sZU5hbWUiOiJWaXNpdG9yIiwiaXNBY3RpdmUiOiJZIiwiaXNBdXRoIjp0cnVlLCJyb2xlIjp7Im5hbWUiOiJWaXNpdG9yIiwiZGVzY3JpcHRpb24iOiJSb2xlIGRlZmluZWQgZm9yIGV2ZW50IHZpc2l0b3IiLCJwcml2aWxlZ2VzIjpbIkpPSU4gRVZFTlQiXSwiaXNBY3RpdmUiOiJZIn0sImlhdCI6MTU5NTM1OTQyMSwiZXhwIjoxNTk1NDQ1ODIxfQ.YaDqOCMPAMuXX4dlJ18NXcoL9v0QdVRAvFg-Ue3mqo8tf0o-Cmk0F1K1imHylpfbzTKpLtY2d6nbdU4831tIUYanXUiibi91c2Vi2wnUiBlu-bQDEPNZ8rWm_O7nPiJH7P5wiq_GnzS4dnrnzuTvAfV-FiuiPjLxRL1GpozZL_QBY2QZkFbKEfeUexF_G_f_vlO4T3S0wOk1k06TOeyHadicqWgrXK5CFJXZrUQKnHPYQB-c4UKokLmOOyaxpu1e9hKuoaP5hRDoukwzg-GlmaPL1HMXNnCGol72ZJB-u17NrmyCbUH9pXvgwDc2Eo2mE-n09ngPXLG4hKcM5jow4A"
}
*/
