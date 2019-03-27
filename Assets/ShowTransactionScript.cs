using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
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
			string msg = ""+www.downloadHandler.text;
			print (msg);
			msg = msg.Substring (1, msg.Length - 2);
			print (msg);
			msg = "" + msg;
			msg = msg.Substring (0, msg.Length - 1);
			if (!msg.Contains ("ul")) {
				msg = msg.Insert (0, "[");
				msg = msg.Insert (msg.Length, "]");
				print (msg);

				JSONNode jn = SimpleJSON.JSONData.Parse (msg);

				print (jn);

				string msg2 = null;

				int num1 = 0, num2 = 1, num3 = 2, num4 = 3;
				foreach (JSONNode jn1 in jn.Childs) {
					print (jn1);
					if (jn1 [num1].Value.Equals ("SessionisLogout")) {
						SceneManager.LoadScene ("Home");
					}
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
//				msg2="{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/26/2019 7:09:59 AM\"}";
				msg2="{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3-26-2019 7:09:59 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3-26-2019 6:03:50 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3-26-2019 5:50:11 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3-26-2019 4:01:43 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3-25-2019 7:14:29 AM\"},{\"wallet_type\":\"2\",\"win_loss_amount\":\"100\",\"date\":\"3-25-2019 2:26:46 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/25/2019 2:15:00 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/25/2019 2:04:24 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/25/2019 1:51:36 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/25/2019 1:36:14 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/25/2019 12:59:49 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/25/2019 12:37:46 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/25/2019 12:33:17 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/20/2019 2:34:04 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/20/2019 1:23:42 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/20/2019 11:21:44 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/19/2019 3:22:16 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/19/2019 1:12:50 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 6:26:02 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 6:16:09 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 6:03:37 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:51:37 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:45:44 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:37:59 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:37:35 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:35:44 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 5:35:36 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 3:19:36 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 3:13:35 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 3:10:11 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/18/2019 2:49:48 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 1:03:19 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/18/2019 12:57:26 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 6:18:49 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 5:32:54 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 12:58:47 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 12:48:45 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 11:35:22 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 11:35:12 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 11:33:56 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/15/2019 11:33:43 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 5:59:19 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 5:53:33 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 5:31:48 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 5:30:26 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 5:13:42 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 3:03:23 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 2:56:49 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 2:39:50 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 2:10:39 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 12:49:36 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 12:01:13 PM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 11:48:55 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/14/2019 10:39:59 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 6:54:37 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 6:24:38 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 6:19:14 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 6:12:13 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 5:41:15 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 3:30:34 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 3:28:54 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/13/2019 2:36:25 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/12/2019 3:52:44 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/12/2019 3:41:47 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/12/2019 11:41:31 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 11:37:09 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:49:51 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:32:01 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:28:56 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:18:43 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:16:03 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:14:31 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 7:12:24 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 6:55:00 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 6:53:11 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 3:40:10 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/11/2019 3:14:56 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/10/2019 2:12:05 AM\"},{\"wallet_type\":\"3\",\"win_loss_amount\":\"100\",\"date\":\"3/8/2019 5:48:18 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/8/2019 4:23:00 AM\"}";
				msg2 = msg2.Substring (0, msg2.Length - 1);
				print (msg2);
				msg2 = msg2.Insert (0, "[");
				msg2 = msg2.Insert (msg2.Length, "]");
				print (msg2);

				//{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/26/2019 7:09:59 AM\"},{\"wallet_type\":\"1\",\"win_loss_amount\":\"80\",\"date\":\"3/26/2019 6:03:50 AM\"}
				JSONNode jn11 = SimpleJSON.JSONData.Parse (msg2);
				foreach(JSONNode jn111 in jn11.Childs){
					print (jn111[0]+" "+jn111[1]+" "+jn111[2]);
				}
			}
		}
	}
	// Update is called once per frame
}
