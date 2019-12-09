using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class Save : MonoBehaviour {

    public HttpRequest http;
    private Button btn;
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(SaveGame);
    }

    void SaveGame() {
        Dictionary<HttpRequestHeader, string> headerOpt = new Dictionary<HttpRequestHeader, string>();
        Dictionary<string, string> bodyOpt = new Dictionary<string, string>();

        headerOpt.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
        headerOpt.Add(HttpRequestHeader.Accept, "application/json");
        bodyOpt.Add("function", "CheckLogin");
        /* need to change */
        //bodyOpt.Add("user_id", username);
        //bodyOpt.Add("password", password);

        //http.Post("http://" + host + ":" + port + "/user", headerOpt, bodyOpt, new Dictionary<string, string>());
        ////if (http.last_code != 200) {
        ////    Debug.Log("Login Failed! code: " + http.last_code);
        ////    return false;
        ////}
        //return true;
    }
}