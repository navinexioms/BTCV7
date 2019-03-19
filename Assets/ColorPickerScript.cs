using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ColorPickerScript : MonoBehaviour {
	public bool isColorSelected,TimeTrigger,isTimeEnded,CanProceed=true,isAutoSelected,isGotResponce;
	public int TriggeredTime,CountDownTime,ActualTime;
	public string ColorNumber=null;
	public Text CountDown,MessageBx;
	public List<GameObject> Colors;
	// Use this for initialization
	void Start () {
		StartCoroutine (ColorAvailableAPI ());
	}
	IEnumerator ColorAvailableAPI(){
		UnityWebRequest www = new UnityWebRequest ("http://apienjoybtc.exioms.me/api/Room/colorpick?struserid="+PlayerPrefs.GetString("userid")+"&strgamesessionid=1&roomid="+PlayerPrefs.GetString("roomid")  );
		www.chunkedTransfer = false;
		www.downloadHandler = new DownloadHandlerBuffer ();
		yield return www.SendWebRequest ();
		if (www.error != null) {
			print ("Something went wrong");
		} else {
			isGotResponce = true;
			print (www.downloadHandler.text);
			string msg = www.downloadHandler.text;
			msg = msg.Substring (1, msg.Length - 2);
			JSONNode jn = SimpleJSON.JSONData.Parse (msg);
			print (jn [0] + " " + jn [1] + " " + jn [2] + " " + jn [3]); 
			string value1 = jn [0];
			string value2 = jn [1];
			string value3 = jn [2];
			string value4 = jn [3];
			if (value1.Equals ("2")) {
				print ("Yellow Is already selected");
			} else if (!value1.Equals ("2")) {
				GameObject pice = GameObject.Find ("1") as GameObject;
				Colors.Add (pice);
				pice.GetComponent<Toggle>().interactable = true;
			}

			if (value2.Equals ("2")) {
				print ("Blue is Already selected");
			} else if (!value2.Equals ("2")) {
				GameObject pice = GameObject.Find ("2") as GameObject;
				Colors.Add (pice);
				pice.GetComponent<Toggle>().interactable = true;
			}

			if (value3.Equals ("2")) {
				print ("Red is already selected");
			}else if (!value3.Equals ("2")) {
				GameObject pice = GameObject.Find ("3") as GameObject;
				Colors.Add (pice);
				pice.GetComponent<Toggle>().interactable = true;
			}

			if (value4.Equals ("2")) {
				print ("Green is Already selected");
			}else if (!value4.Equals ("2")) {
				GameObject pice = GameObject.Find ("4") as GameObject;
				Colors.Add (pice);
				pice.GetComponent<Toggle>().interactable = true;
			}

			if (Colors.Count == 1) {
				GameObject pice = Colors [0].gameObject;
				ColorNumber = pice.gameObject.name;
				StartCoroutine (ColorUpdateAPI ());
			}
		}
	}


	public void ColorPickAPI()
	{
		isColorSelected = true;
		GameObject pice= EventSystem.current.currentSelectedGameObject;
		ColorNumber = "" + EventSystem.current.currentSelectedGameObject.name;
		int num = int.Parse (ColorNumber);
		pice.GetComponent<Toggle> ().enabled = false;
		Colors.Remove (pice);
		foreach (GameObject tg in Colors) {
			tg.GetComponent<Toggle> ().interactable = false;
		}
	}
	public void FourPlayerGame()
	{
		if (isColorSelected) {
			
			CanProceed = false;
		
			StartCoroutine (ColorUpdateAPI ());

		} else {
			StartCoroutine (ColorTelling ("Please select your color"));
		}
	}
//
	IEnumerator ColorUpdateAPI()
	{
		if (isColorSelected) {
			UnityWebRequest www = new UnityWebRequest ("http://apienjoybtc.exioms.me/api/Room/colorupdation?strroomid=" + PlayerPrefs.GetString ("roomid") + "&intcolor=" + ColorNumber);
			www.chunkedTransfer = false;
			www.downloadHandler = new DownloadHandlerBuffer ();
			yield return www.SendWebRequest ();
			if (www.error != null) {
				print (www.error);
			} else {
				print (www.downloadHandler.text);
				string msg = www.downloadHandler.text;
				msg = msg.Substring (1, msg.Length - 2);
				JSONNode jn = SimpleJSON.JSONData.Parse (msg);
				msg = jn [0];
				print (msg);
				if (msg.Equals ("Successful")) {
					StartCoroutine (ColorTelling ("Your Color is"));
				}
			}
		} else {
			print ("Please select the Color");
			StartCoroutine (ColorTelling ("Please select the color"));
		}
	}
//
	IEnumerator ColorTelling(string msg)
	{
		string SelectedColor = null;
		int timer = 1;
		if (ColorNumber.Length == 1) {
			switch (ColorNumber) {
			case null:
				MessageBx.text = msg;
				break;
			case "1":
				SelectedColor = "Yellow";
				PlayerPrefs.SetString ("Color", SelectedColor);
				print ("Color"+PlayerPrefs.GetString ("Color"));
				MessageBx.text = msg + " " + SelectedColor;
				timer = 2;
			
				yield return new WaitForSeconds (timer);

				SceneManager.LoadScene ("FourPlayerGameScene");
				break;
			case "2":
				SelectedColor = "Blue";
				PlayerPrefs.SetString ("Color", SelectedColor);
				print ("Color"+PlayerPrefs.GetString ("Color"));
				MessageBx.text = msg + " " + SelectedColor;

				timer = 2;
				yield return new WaitForSeconds (timer);
				SceneManager.LoadScene ("FourPlayerGameScene");
				break;
			case "3":
				SelectedColor = "Red";
				PlayerPrefs.SetString ("Color", SelectedColor);
				print ("Color"+PlayerPrefs.GetString ("Color"));

				MessageBx.text = msg + " " + SelectedColor;
				timer = 2;
				yield return new WaitForSeconds (timer);
				SceneManager.LoadScene ("FourPlayerGameScene");
				break;
			case "4":
				SelectedColor = "Green";
				PlayerPrefs.SetString ("Color", SelectedColor);
				print ("Color"+PlayerPrefs.GetString ("Color"));

				MessageBx.text = msg + " " + SelectedColor;
				timer = 2;
				yield return new WaitForSeconds (timer);
				SceneManager.LoadScene ("FourPlayerGameScene");
				break;
			}
			
		} else {
			MessageBx.text = msg;
			yield return new WaitForSeconds (timer);
			MessageBx.text = null;
		}
	}
//
	void Update()
	{
		if (isGotResponce) {
			if (!isColorSelected && !TimeTrigger) {
				TimeTrigger = true;
				TriggeredTime = (int)Time.time;
				ActualTime = TriggeredTime + 10;
			}
			if (CountDownTime < 10 && CanProceed == true) {
				CountDownTime = (int)(Time.time - TriggeredTime);
				CountDown.text = "" + (int)(ActualTime - Time.time);
			}
			if (CountDownTime == 10 && !isTimeEnded) {
				isTimeEnded = true;
				if (!isColorSelected) {
					int num = Random.Range (0, Colors.Count);
					num += 1;
					GameObject pice = GameObject.Find ("" + num);
					ColorNumber = pice.gameObject.name;
					pice.GetComponent<Toggle> ().enabled = false;
					Colors.Remove (pice);
				}
				foreach (GameObject tg in Colors) {
					tg.GetComponent<Toggle> ().interactable = false;
				}
				isColorSelected = true;
				StartCoroutine (ColorUpdateAPI ());
			}
		}
	}
	// Update is called once per frame
}
