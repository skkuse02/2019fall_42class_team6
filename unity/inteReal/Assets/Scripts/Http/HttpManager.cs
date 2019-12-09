using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.IO;
using System.Net;


public class HttpManager : MonoBehaviour
{
    public delegate void ResponseCallback(DownloadHandler body);

    public string Get(string endpoint, Dictionary<string, string> parameters) {
        HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp(MakeFullURL(endpoint, parameters));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return json;
    }

    public IEnumerator GetRoutine(string endpoint, Dictionary<string, string> parameters, ResponseCallback callback) {
        UnityWebRequest uwr = UnityWebRequest.Get(MakeFullURL(endpoint, parameters));
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError) {
            Debug.Log("Error while GET routine: " + uwr.error);
        }
        else {
            Debug.Log("GET routine success");
        }

        callback(uwr.downloadHandler);
    }

    //public void Get(string endpoint, Dictionary<string, string> parameters, ResponseCallback callback) {
    //    UnityWebRequest uwr = UnityWebRequest.Get(MakeFullURL(endpoint, parameters));

    //    StartCoroutine(WaitForRequest(uwr, callback));
    //}

    private string MakeFullURL(string endpoint, Dictionary<string, string> parameters) {
        List<string> paramString = new List<string>();
        foreach (var p in parameters) {
            paramString.Add(string.Format("{0}={1}", p.Key, p.Value));
        }
        if (parameters.Count > 0)
            endpoint += "?";
        return endpoint + string.Join("&", paramString);
    }

    private IEnumerator WaitForRequest(UnityWebRequest uwr, ResponseCallback callback) {
        yield return uwr.SendWebRequest();

        callback(uwr.downloadHandler);
    }
}
