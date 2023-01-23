
using System.EnterpriseServices;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class API
{
    private static string url= "http://89.208.222.66:8080/user/name/";
    public static Person getPerson(string name)
    {
        url += name;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader=new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Person>(json);
        // yield return request.SendWebRequest();

    }
}
