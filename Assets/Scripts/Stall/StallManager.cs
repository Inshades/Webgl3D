using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using UnityEngine.Video;
public class StallManager : MonoBehaviour
{
    public GameObject playerPosition;
  //  public List<Sprite> slideShowimages;
    public List<string> url;
    public string boothId;
    public stallType boothType;

    public List<string> spriteUrl = new List<string>();
    public List<SpriteRenderer> stallSpriteSource;
    public List<Texture> stallSprite;

    public List<MeshRenderer> _meshRenderer;

    public string currentIndex;

    public bool loadedStatus = false;
   

    private void Start()
    {
        int currentStallIndex = 0; 

        int.TryParse(currentIndex, out currentStallIndex);

        for (int i = 0; i < ApiHandler.instance._metaDataUrlContent._collegeDataClassList[currentStallIndex]._exhibhitorsMetaDataMediaClassList.Count; i++)
        {
            spriteUrl.Add(ApiHandler.instance._metaDataUrlContent._collegeDataClassList[currentStallIndex]._exhibhitorsMetaDataMediaClassList[i].exhibhitorsMetaDataMediaUrl);
        }

        applyTexture();
    }

    void applyTexture()
    {

        StartCoroutine(downloadSlideShowImage(spriteUrl, stallSprite, callBackHandler =>
        {
            if (stallSprite.Count == spriteUrl.Count)
            {
                for (int i = 0; i < _meshRenderer.Count; i++)
                {
                    _meshRenderer[i].material.mainTexture = callBackHandler.spriteList[i];
                    loadedStatus = true;
                }
            }

        }));

    }

    IEnumerator downloadSlideShowImage(List<string> url, List<Texture> spriteList, Action<downloadImagesData> callBackList)
    {
        for (int i = 0; i < url.Count; i++)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url[i]);

            yield return www.SendWebRequest();
            Debug.Log(www.downloadProgress);

            if (www.isNetworkError)
            {
                Debug.Log("Error while Receiving: " + www.error);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                spriteList.Add(myTexture);
                Debug.Log("Success");
            }
        }

        downloadImagesData _callBack = new downloadImagesData();

        _callBack.spriteList = spriteList;
        callBackList(_callBack);

    }

}
// StartCoroutine(SaveUserActivity(userActivityType.DOWNLOAD_BROUCHER, "https://helpx.adobe.com/acrobat/using/links-attachments-pdfs.html", "Brochure Download", "https://helpx.adobe.com/acrobat/using/links-attachments-pdfs.html"));
// StartCoroutine(GetUserActivity());

//(userActivityType activityType, string activityData, string boothName, string boothId)
[SerializeField]
public class downloadImagesData
{
    public List<Texture> spriteList;
}