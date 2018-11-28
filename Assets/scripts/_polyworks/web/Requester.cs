namespace Polyworks {
	using UnityEngine;
	using UnityEngine.Networking;
	using System.Collections;
	using System.Collections.Generic;
		
	public class Requester : MonoBehaviour
	{
/* 
		public delegate void RequestCompleteHandler(string response);
		public event RequestCompleteHandler OnRequestCompleted;

		public void Get(string url, string type = "text") {
			if (type == "text") {
				StartCoroutine (GetText (url));
			} else {

			}
		}

		public void PostJSON(string url, string json) {
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers.Add("Content-Type", "application/json");
			Debug.Log ("Requester/PostJSON");
			var formData = System.Text.Encoding.UTF8.GetBytes(json);

			WWW www = new WWW(url, formData, headers);

			StartCoroutine(WaitForRequest(www));
		}

		IEnumerator GetText(string url) {
			Debug.Log ("Request/GetText, url = " + url);
			using(UnityWebRequest www = UnityWebRequest.Get(url)) {
				yield return www.Send();

				if(www.isNetworkError) {
					Debug.Log(www.error);
				}
				else {
					// Show results as text
//					Debug.Log(www.downloadHandler.text);
					if (OnRequestCompleted != null) {
						OnRequestCompleted (www.downloadHandler.text);
					}
					// Or retrieve results as binary data
//					byte[] results = www.downloadHandler.data;
				}
			}
		}

		IEnumerator WaitForRequest(WWW data) {
			yield return data; // Wait until the download is done
			if (data.error != null)
			{
				Debug.Log("There was an error sending request: " + data.error);
			}
			else
			{
				Debug.Log("WWW Request complete ");
				if (OnRequestCompleted != null) {
					OnRequestCompleted (data.text);
				}
			}		
		}
*/
	}

}

