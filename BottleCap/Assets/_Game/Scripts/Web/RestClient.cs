using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    public static RestClient Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"Instance of {nameof(RestClient)} already exists");
        }
    }

    public async Task GetAsync(string url, System.Action<string> deserializeCallback)
    {
        string json = await _Get(url);
        deserializeCallback.Invoke(json);
    }

    private async Task<string> _Get(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        return await SendRequestAsync(www);
    }

    public async Task<string> PostAsync(string url, AnswerDto postObject)
    {
        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(postObject);
        UnityWebRequest www = UnityWebRequest.Post(url, jsonData);

        www.SetRequestHeader("Content-type", "application/json");
        www.uploadHandler.contentType = "application/json";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bytes);

        return await SendRequestAsync(www);
    }

    private async Task<string> SendRequestAsync(UnityWebRequest request)
    {
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = request.downloadHandler.text;
            Debug.Log(jsonResult);
            return jsonResult;
        }
        else
        {
            Debug.LogError(request.error);
            return request.error;
        }
    }
}

public static class ExtensionMethods
{
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += obj => { tcs.SetResult(null); };
        return ((Task)tcs.Task).GetAwaiter();
    }
}
