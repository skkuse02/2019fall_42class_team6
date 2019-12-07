using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class HttpRequest
{
    public string Get(string endpoint, Dictionary<string, string> parameters) {
        return GetTestJSON();
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
