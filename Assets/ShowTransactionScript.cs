using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
public class ShowTransactionScript : MonoBehaviour {
	public GameObject RowData;
	public GameObject ParentObj;
	public GameObject ParentObj1;
	// Use this for initialization
	void Start () {
		StartCoroutine (HitTransactionApi ());
	}
	IEnumerator HitTransactionApi(){
		UnityWebRequest www = new UnityWebRequest ("http://apienjoybtc.exioms.me/api/Balance/gametransaction?userid=" + PlayerPrefs.GetString ("userid") + "&gamesessionid=1"); 
		www.chunkedTransfer = false;
		www.downloadHandler = new DownloadHandlerBuffer ();
		yield return www.SendWebRequest ();
		if (www.error != null) {
			print ("Something went wrong");
		} else {
			string msg = www.downloadHandler.text;
			print (msg);
			msg = msg.Substring (1, msg.Length - 2);
			print (msg);
			if (!msg.Contains ("ul")) {
				msg = msg.Insert (0, "[");
				msg = msg.Insert (msg.Length, "]");
				print (msg);
				JSONNode jn = SimpleJSON.JSONData.Parse (msg);

				print (jn);
				int num1 = 0, num2 = 1, num3 = 2, num4 = 3;
				foreach (JSONNode jn1 in jn.Childs) {
					print (jn1);

					print (jn1 [num1] + " " + jn1 [num2] + " " + jn1 [num3] + " " + jn1 [num4]);

					GameObject data = Instantiate (RowData, ParentObj.transform.position, ParentObj.transform.rotation, ParentObj1.transform);
					data.transform.localScale = ParentObj.transform.localScale;
					if (jn1 [num1].Value.Equals ("1")) {
						data.transform.GetComponent<Text> ().text = "  " + jn1 [num2] + "  " + "win" + "  " + jn1 [num3];
					} else if (jn1 [num1].Value.Equals ("2")) {
						data.transform.GetComponent<Text> ().text = "  " + jn1 [num2] + "  " + "loss" + "  " + jn1 [num3];
					} else if (jn1 [num1].Value.Equals ("3")) {
						data.transform.GetComponent<Text> ().text = "  " + jn1 [num2] + "  " + "Returned" + "  " + jn1 [num3];
					}

//				num1+=1;
//				num2 += 1;
//				num3 += 1;
//				num4 += 1;
				}
			}
		}
	}
	// Update is called once per frame
}
