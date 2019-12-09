using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class LoginManager : MonoBehaviour
{
    const string username = "user1";
    const string password = "user1";
    string host = "34.66.144.16";
    string port = "3000";

    public HttpRequest http;

    // Start is called before the first frame update
    void Start()
    {
        //http = new HttpRequest();
        //Login(username, password);
        //GetRoomInfo(username, password);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Login(string username, string password) {
        Dictionary<HttpRequestHeader, string> headerOpt = new Dictionary<HttpRequestHeader, string>();
        Dictionary<string, string> bodyOpt = new Dictionary<string, string>();

        headerOpt.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
        headerOpt.Add(HttpRequestHeader.Accept, "application/json");
        bodyOpt.Add("function", "CheckLogin");
        bodyOpt.Add("user_id", username);
        bodyOpt.Add("password", password);

        http.Post("http://" + host + ":" + port + "/user", headerOpt, bodyOpt, new Dictionary<string, string>());
        //if (http.last_code != 200) {
        //    Debug.Log("Login Failed! code: " + http.last_code);
        //    return false;
        //}
        return true;
    }

    public void GetRoomInfo(string username, string password) {
        //if (!Login(username, password))
        //    return;

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetRoomInfofile");
        parameters.Add("model_id", "model_5");

        http.Get("http://" + host + ":" + port + "/model", parameters);
    }
}
