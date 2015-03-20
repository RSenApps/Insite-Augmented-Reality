using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

public class UploadScreenshots : MonoBehaviour {
	float uploadRefreshRate = 3;
	float lastUploadTime = 3;
	public static bool updated = false;
	string lastId = "";
	bool newImage = true;
	bool updating = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!updating) {
			if (Time.time - lastUploadTime > uploadRefreshRate) {

				if (updated) {
					updated = false;
					updating = true;
					StartCoroutine (Upload ());
				}
			}
		}
	}
	IEnumerator Upload() {

		byte[] bytes = ((Texture2D) GetComponent<Renderer>().material.mainTexture).EncodeToPNG();
		
		
		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("frameCount", Time.frameCount.ToString());
		form.AddField("image", System.Convert.ToBase64String(bytes));
		
		byte[] rawData = form.data;
		string url = "https://api.imgur.com/3/image";
		
		// Add a custom header to the request.
		// In this case a basic authentication to access a password protected resource.
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Client-ID 153e6b4f156c38c");
		
		// Post a request to an URL with our custom headers
		WWW www = new WWW(url, rawData, headers);
		yield return www;
	
		string id = "";
		try {
			id = Regex.Split(Regex.Split(www.text, "id\":\"")[1], "\"")[0];
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
		www = new WWW ("https://metadesk.firebaseio.com/img.json");

		yield return www;
		string imageList = www.text;
		if (newImage) {
			imageList = id + "," + imageList.Split('"')[1];
		} else {
			string tempImageList = id;
			bool firstImage = true;
			foreach (string image in imageList.Split(','))
			{
				string imageclipped = image.Replace("\"", string.Empty);
				if (firstImage)
				{
					if (lastId != imageclipped)
					{
						www = new WWW("http://i.imgur.com/" + imageclipped + ".png");
						
						// wait until the download is done
						yield return www;
						
						// assign the downloaded image to the main texture of the object
						www.LoadImageIntoTexture((Texture2D) GetComponent<Renderer>().material.mainTexture);
					}
					firstImage = false;

					continue;
				}
				tempImageList += "," + imageclipped;
			}
			imageList = tempImageList;
		}

		lastId = id;

		imageList = "\"" + imageList + "\"";
		ServicePointManager.ServerCertificateValidationCallback = Validator;
		
		// Create a request using a URL that can receive a post. 
		var request = (HttpWebRequest)WebRequest.Create("https://metadesk.firebaseio.com/img.json");
		
		var data = Encoding.ASCII.GetBytes(imageList);
		request.UserAgent = "test.net";
		request.Accept = "application/json";
		request.Method = "PUT";
		request.ContentType = "raw";
		request.ContentLength = data.Length;
		
		using (var stream = request.GetRequestStream())
		{
			stream.Write(data, 0, data.Length);
		}
		newImage = false;
		var response = (HttpWebResponse)request.GetResponse();
		lastUploadTime = Time.time;
		
		updating = false;
		//Debug.Log (response);
	}
	public static bool Validator (object sender, X509Certificate certificate, X509Chain chain,
	                              SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}
}
