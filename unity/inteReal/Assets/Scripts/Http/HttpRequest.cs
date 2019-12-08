using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.IO;

public class HttpRequest: MonoBehaviour {
    public long last_code;
    public string last_text;
    public byte[] last_data;
    public Dictionary<string, string> cookie;

    //public void Get(string endpoint, Dictionary<string, string> parameters) {
    //    IEnumerator e = _Get(endpoint, parameters);
    //    while (e.MoveNext())
    //        if (e.Current != null)
    //            Debug.Log(e.Current as string);
    //}

    public IEnumerator Get(string endpoint, Dictionary<string, string> parameters) {
        /* call StartCoroutine(Get()); to where the request is needed */
        endpoint = MakeFullURL(endpoint, parameters);

        UnityWebRequest uwr = UnityWebRequest.Get(endpoint);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("Error while GET: " + uwr.error);
        }
        else {
            last_code = uwr.responseCode;
            last_text = uwr.downloadHandler.text;
            last_data = uwr.downloadHandler.data;
            cookie = uwr.GetResponseHeaders();
            Debug.Log("in get function: " + uwr.downloadHandler.text);
            yield return null;
        }
        yield return new WaitForFixedUpdate();
    }

    //public void Post(string endpoint, Dictionary<string, string> headerData, Dictionary<string, string> bodyData, Dictionary<string, string> parameters) {
    //    IEnumerator e = _Post(endpoint, headerData, bodyData, parameters);
    //    while (e.MoveNext())
    //        if (e.Current != null)
    //            Debug.Log(e.Current as string);
    //}

    public IEnumerator Post(string endpoint, Dictionary<string, string> headerData, Dictionary<string, string> bodyData, Dictionary<string, string> parameters) {
        endpoint = MakeFullURL(endpoint, parameters);
        Debug.Log("endpoint: " + endpoint);

        WWWForm form = new WWWForm();
        foreach (var p in bodyData) {
            form.AddField(p.Key, p.Value);
        }

        UnityWebRequest uwr = UnityWebRequest.Post(endpoint, form);
        foreach (var p in headerData) {
            uwr.SetRequestHeader(p.Key, p.Value);
        }
        //if (cookie != null) {
        //    foreach (var p in cookie) {
        //        uwr.SetRequestHeader(p.Key, p.Value);
        //    }
        //}
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError) {
            Debug.Log("Error while POST: " + uwr.error + uwr.responseCode);
        }
        else {
            last_code = uwr.responseCode;
            last_text = uwr.downloadHandler.text;
            last_data = uwr.downloadHandler.data;
            cookie = uwr.GetResponseHeaders();
            Debug.Log("in post function: " + uwr.downloadHandler.text);
            //Debug.Log("in post function-data: " + uwr.downloadHandler.data);
            yield return null;
        }
        yield return new WaitForFixedUpdate();
    }

    //public IEnumerator DownloadFile(string url, string path) {
    //    UnityWebRequest uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
    //    uwr.downloadHandler = new DownloadHandlerFile(path);
    //    yield return uwr
    //}

    private string MakeFullURL(string endpoint, Dictionary<string, string> parameters) {
        List<string> paramString = new List<string>();
        foreach (var p in parameters) {
            paramString.Add(string.Format("{0}={1}", p.Key, p.Value));
        }
        if (parameters.Count > 0)   endpoint += "?";
        return endpoint + string.Join("&", paramString);
    }

    [MenuItem("Tools/Read file")]
    public string GetTestJSON() {
        string path = "Assets/Resources/test_room.json";

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        return json;
    }
}
