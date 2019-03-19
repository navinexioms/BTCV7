using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;
namespace Photon.Pun.UtilityScripts
{
	public class PlayWithFriendSceneManager : Photon.Pun.MonoBehaviourPun
	{


		public Toggle TwoPlayerToggle, FourPlayerToggle;
		public GameObject TwoPlayerGameObject, FourPlayerGameObject,TwoPlayerButton,FourPlayerButton;
		public bool isSelectedGameType;
		public GameObject TwoPlayerCreateRoomButton;
		public GameObject TwoPlayerWarningButton;
		public GameObject TwoplayerJoinRoomButton;
		public GameObject RoomnameField;
		public Text TwoPlayerText;
		public Text FourPlayerText;
		public Text WarningText;
		public Text TwoPlayerSelectionText;
		public GameObject LoaddingImage;
		void Start () 
		{
			PlayerPrefs.SetString ("roomname", null);
		}




		#region General Methods

		public void GamePlayOptionSelection()
		{
			if (!isSelectedGameType) {
				StartCoroutine (RoomNameWarning ("Please select the gametype",1.5f));
			} else {
				TwoPlayerWarningButton.SetActive (false);
			}
		}

		public IEnumerator RoomNameWarning(string warn, float time)
		{
			if (warn.Equals ("please enter room name to join room")) {
				TwoPlayerSelectionText.text = warn;
			} else {
				WarningText.text = warn;
			}
			yield return new WaitForSeconds (time);
			WarningText.text = null;
			TwoPlayerSelectionText.text = null;
		}
		public void LoadMainGameSceneFromExtraScene()
		{
			SceneManager.LoadScene ("GameMenu");
			Destroy (this.gameObject);
		}

		#endregion
		#region For Two player methods

		public void SelectTwoPlayerGamePlay()
		{
			TwoPlayerWarningButton.SetActive (false);
			isSelectedGameType = true;
			TwoPlayerGameObject.SetActive (true);
			FourPlayerGameObject.SetActive (false);
			//			TwoPlayerButton.SetActive (true);
			FourPlayerButton.SetActive (false);
		}

		public void TwoPlayerJoinRoomOption()
		{
			TwoPlayerSelectionText.text="you are selected Join room Option";
			TwoplayerJoinRoomButton.SetActive (true);
			RoomnameField.SetActive (true);
			TwoPlayerCreateRoomButton.SetActive (false);
		}


		public void TwoPlayerCreateRoomOption()
		{
			TwoPlayerSelectionText.text="you are selected Create room Option";
			TwoPlayerCreateRoomButton.SetActive (true);
			TwoplayerJoinRoomButton.SetActive (false);
			RoomnameField.SetActive (false);
			//			SceneManager.LoadScene ("BettingAmountFor2PlayerPlayWithFriends");
		}

		public void TakeTwoPlayerRoomNameToJoin(string roomname)
		{
			PlayerPrefs.SetString ("roomname", roomname);
		}

		public void TwoPlayerCreateRoom()
		{
			StartCoroutine (GettingRoomName ());
		}

		public void TwoPlayerJoinRoomMethod()
		{
			string name = PlayerPrefs.GetString ("roomname");
			LoaddingImage.SetActive (true);
			print (name);
			if (name.Length == 0 || name.Length < 1) {
				LoaddingImage.SetActive (false);
				StartCoroutine (RoomNameWarning ("please enter room name to join room",1.5f));
			} 

			else {
				StartCoroutine (CheckRoomAvailability());
			}
		}
		#endregion


		#region API's

		IEnumerator CheckRoomAvailability()
		{
			UnityWebRequest www = new UnityWebRequest ("http://apienjoybtc.exioms.me/api/Room/roomcheck?struserid="+809+"&strgamesessionid=1&roomid="+PlayerPrefs.GetString("roomname"));
			www.chunkedTransfer = false;
			www.downloadHandler = new DownloadHandlerBuffer ();
			yield return www.SendWebRequest ();
			if (www.error != null) {
				print ("Soemthing went wrong");
			} else {
				print (www.downloadHandler.text);
				string msg = www.downloadHandler.text;
				msg = msg.Substring (1,msg.Length-2);
				print (msg);
				JSONNode jn = SimpleJSON.JSONData.Parse (msg);
				msg = jn [0];
				if (msg.Equals ("Roomisavailable")) {
					SceneManager.LoadScene ("BettingAmountFor2PlayerPlayWithFriends");
				} else {
					StartCoroutine (RoomNameWarning ("No room room by this name please enter room name properly",2));
				}
			}
		}


		IEnumerator GettingRoomName()
		{
			print ("Hello");
			LoaddingImage.SetActive (true);
			UnityWebRequest www =new UnityWebRequest ("http://apienjoybtc.exioms.me/api/Room/roomcreatebyuser?struserid="+ PlayerPrefs.GetString("userid")+"&strgamesessionid=1&intgametype=2&dblamount="+PlayerPrefs.GetString("amount"));
			www.chunkedTransfer = false;
			www.downloadHandler = new DownloadHandlerBuffer ();
			yield return www.SendWebRequest ();
			if (www.error != null) {
				print ("Someting went wrong");
			} else {
				print (www.downloadHandler.text);
				string msg = www.downloadHandler.text;
				msg = msg.Substring (1, msg.Length - 2);
				JSONNode jn = SimpleJSON.JSONData.Parse (msg);
				msg = jn [0];
				if (msg.Contains ("2PLDO")) {
					SceneManager.LoadScene ("BettingAmountFor2PlayerPlayWithFriends");
					PlayerPrefs.SetString ("roomname", msg);
				} else {
					LoaddingImage.SetActive (false);
				}
			}
		}

		#endregion


		//http://apienjoybtc.exioms.me/api/Room/roomcheck?struserid=809&strgamesessionid=1&roomid=abhbfbc&dblamt=100




		// Use this for initialization
		//http://apienjoybtc.exioms.me/api/Room/roomallusers?struserid=2&strgamesessionid=1&intgametype=2&roomid=abc&dblamt=100&date=2019-01-01
	}
}
