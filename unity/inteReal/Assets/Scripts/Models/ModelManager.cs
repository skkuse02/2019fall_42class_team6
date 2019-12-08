using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ModelManager : MonoBehaviour
{
    const string username = "user1";
    const string password = "user1";
    string host = "34.66.144.16";
    string port = "3000";

    public HttpRequest httpRequest;

    private void Start() {
        httpRequest = new HttpRequest();
    }

    public void GetAllModelFiles(string product) {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetProductfileList");
        parameters.Add("product_id", product);

        StartCoroutine(httpRequest.Get("http://" + host + ":" + port + "/keyword", parameters));

        string json = httpRequest.last_text;

        Debug.Log("Model Manager: " + json);
        List<ModelFileJSON> files = JsonConvert.DeserializeObject<List<ModelFileJSON>>(json);

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

            StartCoroutine(httpRequest.Get("http://" + host + ":" + port + "/keyword", parameters));

            string path = Path.Combine(dir, file.product_file);

            File.WriteAllBytes(path, httpRequest.last_data);
        }
    }
}
