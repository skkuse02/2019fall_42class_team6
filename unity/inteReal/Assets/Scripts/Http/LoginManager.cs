using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoginManager : MonoBehaviour
{
    public const string host = "34.66.144.16";
    public const string port = "3000";
    public const string cachedFile = "temp.txt";

    public string user_id;
    public string model_id;

    public bool loaded;

    void Start() {
        if (!File.Exists(Path.Combine(Directory.GetParent(Application.dataPath).FullName, cachedFile))) {
            Debug.LogError("Login Failed!");
            new WaitForSecondsRealtime(3f);
            Application.Quit();
            return;
        }

        StreamReader reader = new StreamReader(cachedFile);
        user_id = reader.ReadLine().Trim();
        model_id = reader.ReadLine().Trim();
        Debug.Log("Login Success: " + user_id);

        loaded = true;
    }
}
