using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListContrl : MonoBehaviour
{
    public GameObject buttonTemplate;   
    // Start is called before the first frame update
    void Start()
    {

        ////based on json change text , description and image 
        //for(int i= 0; i<10;i++)
        //{
        //    GameObject button = Instantiate(buttonTemplate) as GameObject;
        //    button.SetActive(true);
            
        //    button.GetComponent<ButtonListButton>().setText(ApiHandler.instance._metaDataUrlContent.exhibhitorsName[i].ToString());
        //    button.GetComponent<ButtonListButton>().setDescription("Description #" + i);
        //   // button.GetComponent<ButtonListButton>().setImage("");

        //    button.transform.SetParent(buttonTemplate.transform.parent, false);
           
        //}
    }

    public void Exhibitor()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

           // button.GetComponent<ButtonListButton>().setText(ApiHandler.instance._metaDataUrlContent.exhibhitorsName[i].ToString());
            button.GetComponent<ButtonListButton>().setDescription("Description #" + i);
            // button.GetComponent<ButtonListButton>().setImage("");

            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

    public void ButtonClicked(string myKeystring)
    {
        //script for teleport
        Debug.Log(myKeystring);
    }
    //IEnumerator downloadImage()
    //{


    //    string url = myItem.url;

    //    UnityWebRequest www = UnityWebRequest.Get(url);


    //    DownloadHandler handle = www.downloadHandler;

    //    //Send Request and wait
    //    yield return www.Send();

    //    if (www.isNetworkError)
    //    {

    //        UnityEngine.Debug.Log("Error while Receiving: " + www.error);
    //    }
    //    else
    //    {
    //        UnityEngine.Debug.Log("Success");
    //        //Load Image
    //        Texture2D texture2d = new Texture2D(8, 8);
    //        Sprite sprite = null;
    //        if (texture2d.LoadImage(handle.data))
    //        {
    //            sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);
    //        }
    //        if (sprite != null)
    //        {
    //            imageToUpdate.sprite = sprite;
    //        }
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
