using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebClient : MonoBehaviour
{
    public string baseUrl;
    private string token;

    public void SetToken(string token)
    {
        this.token = token;
    }

    public async Awaitable<IWebRequestReponse> SendGetRequest(string route)
    {
        UnityWebRequest webRequest = CreateWebRequest("GET", route, "");
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendPostRequest(string route, string data)
    {
        UnityWebRequest webRequest = CreateWebRequest("POST", route, data);
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendPutRequest(string route, string data)
    {
        UnityWebRequest webRequest = CreateWebRequest("PUT", route, data);
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendDeleteRequest(string route)
    {
        UnityWebRequest webRequest = CreateWebRequest("DELETE", route, "");
        return await SendWebRequest(webRequest);
    }

    private UnityWebRequest CreateWebRequest(string type, string route, string data)
    {
        // combine baseUrl and route safely
        string url = string.IsNullOrEmpty(route)
            ? baseUrl
            : baseUrl.TrimEnd('/') + "/" + route.TrimStart('/');

        Debug.Log("Creating " + type + " request to " + url + " with data: " + data);

        UnityWebRequest webRequest;

        if (type == "GET")
        {
            webRequest = UnityWebRequest.Get(url);
        }
        else if (type == "DELETE")
        {
            // Use UnityWebRequest.Delete and do NOT attach an upload handler/body
            webRequest = UnityWebRequest.Delete(url);
        }
        else
        {
            webRequest = new UnityWebRequest(url, type);

            // only attach body for POST/PUT when data is present
            if (!string.IsNullOrEmpty(data))
            {
                byte[] dataInBytes = new UTF8Encoding().GetBytes(data);
                webRequest.uploadHandler = new UploadHandlerRaw(dataInBytes);
                webRequest.SetRequestHeader("Content-Type", "application/json");
            }
        }

        // ensure download handler exists for all methods
        if (webRequest.downloadHandler == null)
            webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Accept", "application/json");
        AddToken(webRequest);
        return webRequest;
    }

    private async Awaitable<IWebRequestReponse> SendWebRequest(UnityWebRequest webRequest)
    {
        await webRequest.SendWebRequest();

        long statusCode = webRequest.responseCode;
        string responseBody = webRequest.downloadHandler != null ? webRequest.downloadHandler.text : string.Empty;
        string logMsg = $"Request {webRequest.method} {webRequest.url} => status {statusCode}, result {webRequest.result}";

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(logMsg + " - body: " + responseBody);
            return new WebRequestData<string>(responseBody);
        }
        else
        {
            Debug.LogWarning(logMsg + " - error: " + webRequest.error + " - body: " + responseBody);
            return new WebRequestError($"{webRequest.error} | status: {statusCode} | body: {responseBody}");
        }
    }

    private void AddToken(UnityWebRequest webRequest)
    {
        if (!string.IsNullOrEmpty(token))
            webRequest.SetRequestHeader("Authorization", "Bearer " + token);
    }

    private string RemoveIdFromJson(string json)
    {
        return json.Replace("\"id\":\"\",", "").Replace("\"Id\":\"\",", "");
    }

}

[Serializable]
public class Token
{
    public string tokenType;
    public string accessToken;
}