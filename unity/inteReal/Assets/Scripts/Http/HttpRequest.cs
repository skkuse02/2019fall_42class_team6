using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.IO;

public class HttpRequest: MonoBehaviour {
    public static IEnumerator Get(string endpoint, Dictionary<string, string> parameters) {
        /* call StartCoroutine(Get()); to where the request is needed */
        List<string> paramString = new List<string>();
        foreach (var p in parameters) {
            paramString.Add(string.Format("{0}={1}", p.Key, p.Value));
        }
        endpoint += "?" + string.Join("&", paramString);

        UnityWebRequest uwr = UnityWebRequest.Get(endpoint);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError) {
            Debug.Log("Error while GET: " + uwr.error);
        }
        else {
            yield return uwr.downloadHandler.text;
        }
    }

    [MenuItem("Tools/Read file")]
    public static string GetTestJSON() {
        string path = "Assets/Resources/test_room.json";

        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();

        return json;
    }
}
