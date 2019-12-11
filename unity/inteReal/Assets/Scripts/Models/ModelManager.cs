using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ModelManager : MonoBehaviour
{
    public LoginManager login;
    HttpRequest http;

    private void Start() {
        http = new HttpRequest();
    }

    public void GetAllModelFiles(string product) {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetProductfileList");
        parameters.Add("product_id", product);

        string json = http.Get("http://" + LoginManager.host + ":" + LoginManager.port + "/keyword", parameters);

        // Debug.Log("Model Manager: " + json);
        List<ModelFileJSON> files = JsonConvert.DeserializeObject<List<ModelFileJSON>>(json);
        //List<ModelFileJSON> files = JsonUtility.FromJson<List<ModelFileJSON>>(json);

        if (!Directory.Exists("models")) {
            Directory.CreateDirectory("models");
        }
        string dir = Path.Combine("models", product);
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        foreach (ModelFileJSON file in files) {
            parameters = new Dictionary<string, string>();

            parameters.Add("function", "GetFile");
            parameters.Add("filename", file.product_file);

            string path = Path.Combine(dir, file.product_file);
            http.Download(path, "http://" + LoginManager.host + ":" + LoginManager.port + "/keyword", parameters);
            //http.Get("http://" + host + ":" + port + "/keyword", parameters);

            //string path = Path.Combine(dir, file.product_file);

            //File.WriteAllBytes(path, http.last_data);
        }
    }
}
