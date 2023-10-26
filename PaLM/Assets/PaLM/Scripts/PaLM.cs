using System.Net;
using System.IO;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Networking;
using System.Threading.Tasks;

public static class PaLM



{
    public static async Task<PaLMJSON> GetPaLMResponse(string requestBody, string apiKey, Action OnRequestCompleted = null, Action OnRequestFailed = null)
    {
        string requestMethod = "POST";
        string requestUrl = "https://generativelanguage.googleapis.com/v1beta2/models/text-bison-001:generateText?key=" + apiKey;
        string requestHeader = "application/json";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);

        UnityWebRequest webRequest = new UnityWebRequest(requestUrl, requestMethod);
        webRequest.SetRequestHeader("Content-Type", requestHeader);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Request Error: " + webRequest.error);
            OnRequestFailed?.Invoke();
            return null; 
        }
        else
        {
            string responseText = webRequest.downloadHandler.text;
            PaLMJSON paLMResponse = JsonUtility.FromJson<PaLMJSON>(responseText);
            OnRequestCompleted?.Invoke();
            return paLMResponse;
        }
    }


    #region Regular WebRequest

    public static PaLMJSON GetPaLMResponse(string requestBody, string apiKey)
    {
        string requestMethod = "POST";
        string requestUrl = "https://generativelanguage.googleapis.com/v1beta2/models/text-bison-001:generateText?key=" + apiKey;
        string requestHeader = "application/json";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);


        try
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.Method = requestMethod;
            webRequest.ContentType = requestHeader;

            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(bodyRaw, 0, bodyRaw.Length);
            }

            using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string responseText = reader.ReadToEnd();
                        Debug.Log(responseText);
                        Debug.Log(JsonUtility.FromJson<PaLMJSON>(responseText).candidates[0].output);

                        return JsonUtility.FromJson<PaLMJSON>(responseText);
                    }
                }
                else
                {
                    Console.WriteLine("Web Request Error: " + webResponse.StatusDescription);
                    return null;
                }
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return null;
        }
    }
    #endregion
}
