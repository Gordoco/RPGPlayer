using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class RestAPI : MonoBehaviour
{
    private string URL = "https://api.openai.com/v1/images/generations";

    private string RevisedPropmt = "";

    private string modelName = "dall-e-3";

    private string inputPrompt = GM.promptToUse;
    public Image YourRawImage;

    [DllImport("__Internal")]
    private static extern void EmbedImage(string url);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeAPICall());
    }

    [System.Serializable]
    class RequestData
    {
        public string model;
        public string prompt;
        public string quality;
        public int n;
        public string size;
        public string style;
    }

    [System.Serializable]
    class Data
    {
        public string revised_prompt;
        public string url;
    }

    [System.Serializable]
    class JsonResponse
    {
        public int created;
        public Data[] data;
    }

    IEnumerator MakeAPICall()
    {
        RequestData requestData = new RequestData()
        {
            model = modelName,
            prompt = inputPrompt,
            quality = "hd",
            n = 1,
            size = "1024x1024",
            style = "vivid"
        };
        
        string jsonData = JsonUtility.ToJson(requestData);
        Debug.Log(jsonData);

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = UnityWebRequest.Post(URL, jsonData);
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + "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");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError) Debug.Log("COULDN'T CONNECT TO REST API");
        else
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);
            JsonResponse url = JsonUtility.FromJson<JsonResponse>(json);
            Debug.Log(url.data.Length);
            RevisedPropmt = url.data[0].revised_prompt;

            StartCoroutine(DownloadImage(url.data[0].url));
            GameObject text = GameObject.FindGameObjectWithTag("URLText");
            TMPro.TMP_Text IF = text.GetComponent<TMPro.TMP_Text>();
            IF.text = url.data[0].url;
        }
    }

    public void SaveCharacter()
    {
        
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log(request.error);
        else
        {
            YourRawImage.sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(new Vector2(), new Vector2(1024, 1024)), new Vector2(0, 0), 1);
            string generatedName = Random.Range(0, 9999) + "_" + Random.Range(0, 9999) + "_" + Random.Range(0, 9999);
            var imgData = ((DownloadHandlerTexture)request.downloadHandler).texture.EncodeToJPG();
            if (imgData.Length > 1)
            {
                File.WriteAllBytes("Assets/SavedImages/" + generatedName + ".jpg", imgData);
            }
            //AssetDatabase.CreateAsset(((DownloadHandlerTexture)request.downloadHandler).texture, "Assets/SavedImages/" + generatedName + ".asset");
        }
    }
}

