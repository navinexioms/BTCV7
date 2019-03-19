﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using SimpleJSON;
using UnityEngine.EventSystems;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Photon.Pun.UtilityScripts
{
	public class FourPlayerGameLogic : MonoBehaviourPunCallbacks,IPunTurnManagerCallbacks 
	{


		private PunTurnManager turnManager;

		public string temp=null;

		public int OccuranceOfSix;
		public int TrialOFTossing;

		public int TriggeredTime;
		public int TriggerCounter;
		public int timer;
		public int playerCounter;

		public int TriggeredTime2;
		public int TriggerCounter2;
		public int timer2;

		public int ImageFillingCounter;

		public int OnlyOneBluePiceCanMove;
		public int OnlyOneGreenPiceCanMove;
		public int OnlyOneYellowPiceCanMove;
		public int OnlyOneRedPiceCanMove;

		public GameObject DisconnectPanel;
		public GameObject WinPanel, LosePanel;
		public GameObject QuitPanel;

		public Text DisconnectText;
		public Text DisconnectText1;
		public GameObject ReconnectButton;


		JSONNode rootNode=new JSONClass();
		JSONNode childNode = new JSONClass ();
		JSONNode BlankTurn1 = new JSONClass ();
		JSONNode BlankTurn2=new JSONClass();

		public Image TimerImage;

		public Image ImageOne;
		public Image ImageTwo;
		public Image ImageThree;
		public Image ImageFour;

		public Vector3 TimerOnePosition;
		public Vector3 TimerTwoPosition;
		public Vector3 TimerThreePosition;
		public Vector3 TimerFourPosition;

		private int totalYellowInHouse,totalBlueInHouse,totalRedInHouse ,totalGreenInHouse;

		public GameObject BlueFrame,RedFrame ,GreenFrame, YellowFrame;

		public GameObject BluePlayerI_Border,BluePlayerII_Border,BluePlayerIII_Border,BluePlayerIV_Border;
		public GameObject GreenPlayerI_Border,GreenPlayerII_Border,GreenPlayerIII_Border,GreenPlayerIV_Border;
		public GameObject RedPlayerI_Border,RedPlayerII_Border,RedPlayerIII_Border,RedPlayerIV_Border;
		public GameObject YellowPlayerI_Border, YellowPlayerII_Border, YellowPlayerIII_Border, YellowPlayerIV_Border;

		public Sprite[] DiceSprite=new Sprite[7];

		public Vector3[] BluePlayers_Pos;
		public Vector3[] GreenPlayers_Pos;
		public Vector3[] RedPlayers_Pos;
		public Vector3[] YellowPlayers_Pos;


		public Button BluePlayerI_Button, BluePlayerII_Button, BluePlayerIII_Button, BluePlayerIV_Button;
		public Button GreenPlayerI_Button,GreenPlayerII_Button,GreenPlayerIII_Button,GreenPlayerIV_Button;
		public Button RedPlayerI_Button,RedPlayerII_Button,RedPlayerIII_Button,RedPlayerIV_Button;
		public Button YellowPlayerI_Button,YellowPlayerII_Button,YellowPlayerIII_Button,YellowPlayerIV_Button;

		public string playerTurn="BLUE";

		public Transform diceRoll;

		public Button DiceRollButton;

		public Transform BlueDiceRollPosition, GreenDiceRollPosition,RedDiceRollPosition,YellowDicePosition;

		private string currentPlayer="none";
		private string currentPlayerName = "none";

		public GameObject BluePlayerI, BluePlayerII, BluePlayerIII, BluePlayerIV;
		public GameObject GreenPlayerI, GreenPlayerII, GreenPlayerIII, GreenPlayerIV;
		public GameObject RedPlayerI, RedPlayerII, RedPlayerIII, RedPlayerIV;
		public GameObject YellowPlayerI,YellowPlayerII,YellowPlayerIII,YellowPlayerIV;


		public List<int> BluePlayer_Steps=new List<int>();
		public List<int> GreenPlayer_Steps=new List<int>();
		public List<int> RedPlayer_Steps=new List<int>();
		public List<int> YellowPlayer_Steps=new List<int>();

		//----------------Selection of dice number Animation------------------
		private int selectDiceNumAnimation;

		//Players movement corresponding to blocks
		public List<GameObject> blueMovemenBlock=new List<GameObject>();
		public List<GameObject> greenMovementBlock=new List<GameObject>();
		public List<GameObject> redMovementBlock = new List<GameObject> ();
		public List<GameObject> yellowMovementBlock = new List<GameObject> ();

		public List<Vector3> YellowPiceSafeHouse;
		public List<Vector3> BluePiceSafeHouse;
		public List<Vector3> RedPiceSafeHouse;
		public List<Vector3> GreenPiceSafeHouse;

		public List<GameObject> YellowPiceSafeHouseGO;
		public List<GameObject> BluePiceSafeHouseGO;
		public List<GameObject> GreenPiceSafeHouseGO;
		public List<GameObject> RedPiceSafeHouseGo;

		public List<GameObject> BluePlayers=new List<GameObject>();
		public List<GameObject> GreenPlayers=new List<GameObject>();
		public List<GameObject> RedPlayers = new List<GameObject> ();
		public List<GameObject> YellowPlayers = new List<GameObject> ();

		private System.Random randomNo;

		public bool isMyTurn;
		public bool PlayerCanPlayAgain;
		public bool ActualPlayerCanPlayAgain;







		#region Pices and its border enabling and Disabling methods

		void DisablingBluePlayer()
		{
			BluePlayerI.SetActive (false);
			BluePlayerII.SetActive (false);
			BluePlayerIII.SetActive (false);
			BluePlayerIV.SetActive (false);
		}

		void DisablingRedPlayer()
		{
			RedPlayerI.SetActive (false);
			RedPlayerII.SetActive (false);
			RedPlayerIII.SetActive (false);
			RedPlayerIV.SetActive (false);
		}

		void DisablingGreenPlayer()
		{
			GreenPlayerI.SetActive (false);
			GreenPlayerII.SetActive (false);
			GreenPlayerIII.SetActive (false);
			GreenPlayerIV.SetActive (false);
		}



		void EnablingBluePlayer()
		{
			BluePlayerI.SetActive (true);
			BluePlayerII.SetActive (true);
			BluePlayerIII.SetActive (true);
			BluePlayerIV.SetActive (true);
		}

		void EnablingRedPlayer()
		{
			RedPlayerI.SetActive (true);
			RedPlayerII.SetActive (true);
			RedPlayerIV.SetActive (true);
			RedPlayerIV.SetActive (true);
		}

		void EnablingGreenPlayer()
		{
			GreenPlayerI.SetActive (true);
			GreenPlayerII.SetActive (true);
			GreenPlayerIII.SetActive (true);
			GreenPlayerIV.SetActive (true);
		}




		void DisablingBorderOfYellowPlayer()
		{
			YellowPlayerI_Border.SetActive (false);
			YellowPlayerII_Border.SetActive (false);
			YellowPlayerIII_Border.SetActive (false);
			YellowPlayerIV_Border.SetActive (false);
		}

		void DisablingBordersOFBluePlayer()
		{
			BluePlayerI_Border.SetActive (false);
			BluePlayerII_Border.SetActive (false);
			BluePlayerIII_Border.SetActive (false);
			BluePlayerIV_Border.SetActive (false);
		}

		void DisablingBorderOrRedPlayer()
		{
			RedPlayerI_Border.SetActive (false);
			RedPlayerII_Border.SetActive (false);
			RedPlayerIII_Border.SetActive (false);
			RedPlayerIV_Border.SetActive (false);
		}

		void DisablingBordersOFGreenPlayer ()
		{
			GreenPlayerI_Border.SetActive (false);
			GreenPlayerII_Border.SetActive (false);
			GreenPlayerIII_Border.SetActive (false);
			GreenPlayerIV_Border.SetActive (false);
		}




		void DisablingButtonsOFBluePlayes()
		{
			BluePlayerI_Button.interactable = false;
			BluePlayerII_Button.interactable = false;
			BluePlayerIII_Button.interactable = false;
			BluePlayerIV_Button.interactable = false;
		}

		void DisablingButtonsOfGreenPlayers()
		{
			GreenPlayerI_Button.interactable = false;
			GreenPlayerII_Button.interactable = false;
			GreenPlayerIII_Button.interactable = false;
			GreenPlayerIV_Button.interactable = false;	
		}

		void DisablingButtonsOfRedPlayer()
		{
			RedPlayerI_Button.interactable = false;
			RedPlayerII_Button.interactable = false;
			RedPlayerIII_Button.interactable = false;
			RedPlayerIV_Button.interactable = false;
		}

		void DisablingButtonsOfYellowPlayer()
		{
			YellowPlayerI_Button.interactable = false;
			YellowPlayerII_Button.interactable = false;
			YellowPlayerIII_Button.interactable = false;
			YellowPlayerIV_Button.interactable = false;
		}




		void DisablingYellowPlayerRaycast()
		{
			YellowPlayerI_Button.GetComponent<Image> ().raycastTarget = false;
			YellowPlayerII_Button.GetComponent<Image> ().raycastTarget = false;
			YellowPlayerIII_Button.GetComponent<Image> ().raycastTarget = false;
			YellowPlayerIV_Button.GetComponent<Image> ().raycastTarget = false;
		}

		void DisablingBluePlayersRaycast()
		{
			BluePlayerI_Button.GetComponent<Image> ().raycastTarget = false;
			BluePlayerII_Button.GetComponent<Image> ().raycastTarget = false;
			BluePlayerIII_Button.GetComponent<Image> ().raycastTarget = false;
			BluePlayerIV_Button.GetComponent<Image> ().raycastTarget = false;
		}

		void DisablingRedPlayerRaycast()
		{
			RedPlayerI_Button.GetComponent<Image> ().raycastTarget = false;
			RedPlayerII_Button.GetComponent<Image> ().raycastTarget = false;
			RedPlayerIII_Button.GetComponent<Image> ().raycastTarget = false;
			RedPlayerIV_Button.GetComponent<Image> ().raycastTarget = false;
		}

		void DisablingGreenPlayerRaycast()
		{
			GreenPlayerI_Button.GetComponent<Image> ().raycastTarget = false;
			GreenPlayerII_Button.GetComponent<Image> ().raycastTarget = false;
			GreenPlayerIII_Button.GetComponent<Image> ().raycastTarget = false;
			GreenPlayerIV_Button.GetComponent<Image> ().raycastTarget = false;
		}

		void EnablingYellowPlayerRaycast()
		{
			YellowPlayerI_Button.GetComponent<Image> ().raycastTarget = true;
			YellowPlayerII_Button.GetComponent<Image> ().raycastTarget = true;
			YellowPlayerIII_Button.GetComponent<Image> ().raycastTarget = true;
			YellowPlayerIV_Button.GetComponent<Image> ().raycastTarget = true;
		}

		void EnablingBluePlayersRaycast()
		{
			BluePlayerI_Button.GetComponent<Image> ().raycastTarget = true;
			BluePlayerII_Button.GetComponent<Image> ().raycastTarget = true;
			BluePlayerIII_Button.GetComponent<Image> ().raycastTarget = true;
			BluePlayerIV_Button.GetComponent<Image> ().raycastTarget = true;
		}

		void EnablingRedPlayerRaycast()
		{
			RedPlayerI_Button.GetComponent<Image> ().raycastTarget = true;
			RedPlayerII_Button.GetComponent<Image> ().raycastTarget = true;
			RedPlayerIII_Button.GetComponent<Image> ().raycastTarget = true;
			RedPlayerIV_Button.GetComponent<Image> ().raycastTarget = true;
		}

		void EnablingGreenPlayerRaycast()
		{
			GreenPlayerI_Button.GetComponent<Image> ().raycastTarget = true;
			GreenPlayerII_Button.GetComponent<Image> ().raycastTarget = true;
			GreenPlayerIII_Button.GetComponent<Image> ().raycastTarget = true;
			GreenPlayerIV_Button.GetComponent<Image> ().raycastTarget = true;
		}


		void EnableFrameAndBorderForFirstTime()
		{
			GreenPlayerI.SetActive (true);
			GreenPlayerII.SetActive (true);
			GreenPlayerIII.SetActive (true);
			GreenPlayerIV.SetActive (true);

		

			BlueFrame.SetActive (true);
		}

		#endregion




		#region Player winning and beating is done in this section

		void InitializeDice()
		{
			print ("InitializeDice()");
			//	print ("Dice interactable becomes true");
			//	print ("Dice interactable becomes true");
			DiceRollButton.interactable = true;
			DiceRollButton.GetComponent<Button> ().enabled = true;


			//==========================================When any one Player wins==============================================
			if (totalYellowInHouse > 3 && totalBlueInHouse < 4 && totalRedInHouse < 4 && totalGreenInHouse < 4 && playerTurn.Equals ("YELLOW")) {
				print ("Yellow Player Wins");
				playerTurn = "BLUE";
			}
			if (totalBlueInHouse > 3 && totalRedInHouse < 4 && totalGreenInHouse < 4 && totalYellowInHouse < 4 && playerTurn.Equals ("BLUE")) {
				print ("Yellow Player Wins");
				playerTurn = "BLUE";
			}
			if (totalRedInHouse > 3 && totalGreenInHouse < 4 && totalYellowInHouse < 4 && totalBlueInHouse < 4 && playerTurn.Equals ("RED")) {
				print ("Yellow Player Wins");
				playerTurn = "BLUE";
			}
			if (totalGreenInHouse > 3 && totalYellowInHouse < 4 && totalBlueInHouse < 4 && totalRedInHouse < 4 && playerTurn.Equals ("GREEN")) {
				print ("Yellow Player Wins");
				playerTurn = "BLUE";
			}

			//=============getting currentPlayer Value===========//

			if (currentPlayerName.Contains ("YELLOW PLAYER")) {
				if (currentPlayerName == "YELLOW PLAYER I") 
					currentPlayer = YellowPlayerI_Script.YellowPlayerI_ColName;
				if (currentPlayerName == "YELLOW PLAYER II") 
					currentPlayer = YellowPlayerII_Script.YellowPlayerII_ColName;
				if (currentPlayerName == "YELLOW PLAYER III") 
					currentPlayer = YellowPlayerIII_Script.YellowPlayerIII_ColName;
				if (currentPlayerName == "YELLOW PLAYER IV") 
					currentPlayer = YellowPlayerIV_Script.YellowPlayerIV_ColName;
			}

			if (currentPlayerName.Contains ("BLUE PLAYER")) 
			{
				if (currentPlayerName == "BLUE PLAYER I") 
					currentPlayer = BluePlayerI_Script.BluePlayerI_ColName;
				if (currentPlayerName == "BLUE PLAYER II") 
					currentPlayer = BluePlayerII_Script.BluePlayerII_ColName;
				if (currentPlayerName == "BLUE PLAYER III") 
					currentPlayer = BluePlayerIII_Script.BluePlayerIII_ColName;
				if (currentPlayerName == "BLUE PLAYER IV") 
					currentPlayer = BluePlayerIV_Script.BluePlayerIV_ColName;
			}

			if (currentPlayerName.Contains ("RED PLAYER")) 
			{
				if (currentPlayerName == "RED PLAYER I") 
					currentPlayer = RedPlayerI_Script.RedPlayerI_ColName;
				if (currentPlayerName == "RED PLAYER II") 
					currentPlayer = RedPlayerII_Script.RedPlayerII_ColName;
				if (currentPlayerName == "RED PLAYER III") 
					currentPlayer = RedPlayerIII_Script.RedPlayerIII_ColName;
				if (currentPlayerName == "RED PLAYER IV") 
					currentPlayer = RedPlayerIV_Script.RedPlayerIV_ColName;
			}

			if (currentPlayerName.Contains ("GREEN PLAYER"))
			{
				if (currentPlayerName == "GREEN PLAYER I") 
					currentPlayer = GreenPlayerI_Script.GreenPlayerI_ColName;
				if (currentPlayerName == "GREEN PLAYER II") 
					currentPlayer = GreenPlayerII_Script.GreenPlayerII_ColName;
				if (currentPlayerName == "GREEN PLAYER III") 
					currentPlayer = GreenPlayerIII_Script.GreenPlayerIII_ColName;
				if (currentPlayerName == "GREEN PLAYER IV") 
					currentPlayer = GreenPlayerIV_Script.GreenPlayerIV_ColName;
			}

			if(currentPlayerName.Contains(""))


				//============Player vs Opponent=============//
			if (currentPlayer != "none") 
			{
				int i = 0;
				if (currentPlayerName.Contains ("BLUE PLAYER")) 
				{
					for (i = 0; i < 4; i++) {
						if ((i == 0 && currentPlayer == GreenPlayerI_Script.GreenPlayerI_ColName && (currentPlayer != "Star" && GreenPlayerI_Script.GreenPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == GreenPlayerII_Script.GreenPlayerII_ColName && (currentPlayer != "Star" && GreenPlayerII_Script.GreenPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == GreenPlayerIII_Script.GreenPlayerIII_ColName && (currentPlayer != "Star" && GreenPlayerIII_Script.GreenPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == GreenPlayerIV_Script.GreenPlayerIV_ColName && (currentPlayer != "Star" && GreenPlayerIV_Script.GreenPlayerIV_ColName != "Star"))) {
							print (" BluePlayer  Beaten GreenPlayer"+(i+1));
							GreenPlayers [i].transform.position = GreenPlayers_Pos [i];
							GreenPlayer_Steps [i] = 0;
							playerTurn = "BLUE";
							if (i == 0) {
								GreenPlayerI_Script.GreenPlayerI_ColName = "none";
							} else if (i == 1) {
								GreenPlayerII_Script.GreenPlayerII_ColName = "none";	
							} else if (i == 2) {
								GreenPlayerIII_Script.GreenPlayerIII_ColName = "none";	
							} else if (i == 3) {
								GreenPlayerIV_Script.GreenPlayerIV_ColName = "none";
							}
							if (PhotonNetwork.IsMasterClient) {

							}
							else 
							{
								PlayerCanPlayAgain = true;
							}
						}



						if ((i == 0 && currentPlayer == RedPlayerI_Script.RedPlayerI_ColName && (currentPlayer != "Star" && RedPlayerI_Script.RedPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == RedPlayerII_Script.RedPlayerII_ColName && (currentPlayer != "Star" && RedPlayerII_Script.RedPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == RedPlayerIII_Script.RedPlayerIII_ColName && (currentPlayer != "Star" && RedPlayerIII_Script.RedPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == RedPlayerIV_Script.RedPlayerIV_ColName && (currentPlayer != "Star" && RedPlayerIV_Script.RedPlayerIV_ColName != "Star"))) {
							print (" BluePlayer  Beaten RedPlayer"+(i+1));
							RedPlayers [i].transform.position = RedPlayers_Pos [i];
							RedPlayer_Steps [i] = 0;
							playerTurn = "BLUE";
							if (i == 0) {
								RedPlayerI_Script.RedPlayerI_ColName = "none";
							} else if (i == 1) {
								RedPlayerII_Script.RedPlayerII_ColName = "none";	
							} else if (i == 2) {
								RedPlayerIII_Script.RedPlayerIII_ColName = "none";	
							} else if (i == 3) {
								RedPlayerIV_Script.RedPlayerIV_ColName = "none";
							}
							if (PhotonNetwork.IsMasterClient) {

							}
							else 
							{
								PlayerCanPlayAgain = true;
							}
						}


						if ((i == 0 && currentPlayer == YellowPlayerI_Script.YellowPlayerI_ColName && (currentPlayer != "Star" && YellowPlayerI_Script.YellowPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == YellowPlayerII_Script.YellowPlayerII_ColName && (currentPlayer != "Star" && YellowPlayerII_Script.YellowPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == YellowPlayerIII_Script.YellowPlayerIII_ColName && (currentPlayer != "Star" && YellowPlayerIII_Script.YellowPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == YellowPlayerIV_Script.YellowPlayerIV_ColName && (currentPlayer != "Star" && YellowPlayerIV_Script.YellowPlayerIV_ColName != "Star"))) {
							print (" BluePlayer  Beaten YellowPlayer"+(i+1));
							YellowPlayers [i].transform.position = YellowPlayers_Pos [i];
							YellowPlayer_Steps [i] = 0;
							playerTurn = "BLUE";
							if (i == 0) {
								YellowPlayerI_Script.YellowPlayerI_ColName = "none";
							} else if (i == 1) {
								YellowPlayerII_Script.YellowPlayerII_ColName = "none";	
							} else if (i == 2) {
								YellowPlayerIII_Script.YellowPlayerIII_ColName = "none";	
							} else if (i == 3) {
								YellowPlayerIV_Script.YellowPlayerIV_ColName = "none";
							}
							if (PhotonNetwork.IsMasterClient) {

							}
							else 
							{
								PlayerCanPlayAgain = true;
							}
						}
					}
				}

				if (currentPlayerName.Contains ("GREEN PLAYER")) 
				{
					i = 0;
					for (i = 0; i < 4; i++) {
						if ((i == 0 && currentPlayer == BluePlayerI_Script.BluePlayerI_ColName && (currentPlayer != "Star" && BluePlayerI_Script.BluePlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == BluePlayerII_Script.BluePlayerII_ColName && (currentPlayer != "Star" && BluePlayerII_Script.BluePlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == BluePlayerIII_Script.BluePlayerIII_ColName && (currentPlayer != "Star" && BluePlayerIII_Script.BluePlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == BluePlayerIV_Script.BluePlayerIV_ColName && (currentPlayer != "Star" && BluePlayerIV_Script.BluePlayerIV_ColName != "Star"))) {
							print (" GreenPlayer  Beaten BluePlayer"+(i+1));
							BluePlayers [i].transform.position = BluePlayers_Pos [i];
							if (i == 0) {
								BluePlayerI_Script.BluePlayerI_ColName = "none";
							} else if (i == 1) {
								BluePlayerII_Script.BluePlayerII_ColName = "none";
							} else if (i == 2) {
								BluePlayerIII_Script.BluePlayerIII_ColName = "none";
							} else if (i == 3) {
								BluePlayerIV_Script.BluePlayerIV_ColName = "none";
							}
							BluePlayer_Steps [i] = 0;
							playerTurn = "GREEN";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
						if ((i == 0 && currentPlayer == RedPlayerI_Script.RedPlayerI_ColName && (currentPlayer != "Star" && RedPlayerI_Script.RedPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == RedPlayerII_Script.RedPlayerII_ColName && (currentPlayer != "Star" && RedPlayerII_Script.RedPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == RedPlayerIII_Script.RedPlayerIII_ColName && (currentPlayer != "Star" && RedPlayerIII_Script.RedPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == RedPlayerIV_Script.RedPlayerIV_ColName && (currentPlayer != "Star" && RedPlayerIV_Script.RedPlayerIV_ColName != "Star"))) {
							print (" GreenPlayer  Beaten RedPlayerI");
							RedPlayers [i].transform.position = RedPlayers_Pos [i];
							if (i == 0) {
								RedPlayerI_Script.RedPlayerI_ColName = "none";
							} else if (i == 1) {
								RedPlayerII_Script.RedPlayerII_ColName = "none";
							} else if (i == 2) {
								RedPlayerIII_Script.RedPlayerIII_ColName = "none";
							} else if (i == 3) {
								RedPlayerIV_Script.RedPlayerIV_ColName = "none";
							}
							RedPlayer_Steps [i] = 0;
							playerTurn = "GREEN";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}

						if ((i == 0 && currentPlayer == YellowPlayerI_Script.YellowPlayerI_ColName && (currentPlayer != "Star" && YellowPlayerI_Script.YellowPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == YellowPlayerII_Script.YellowPlayerII_ColName && (currentPlayer != "Star" && YellowPlayerII_Script.YellowPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == YellowPlayerIII_Script.YellowPlayerIII_ColName && (currentPlayer != "Star" && YellowPlayerIII_Script.YellowPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == YellowPlayerIV_Script.YellowPlayerIV_ColName && (currentPlayer != "Star" && YellowPlayerIV_Script.YellowPlayerIV_ColName != "Star"))) {
							print (" GreenPlayer  Beaten YellowPlayer"+(i+1));
							YellowPlayers [i].transform.position = YellowPlayers_Pos [i];
							if (i == 0) {
								YellowPlayerI_Script.YellowPlayerI_ColName = "none";
							} else if (i == 1) {
								YellowPlayerII_Script.YellowPlayerII_ColName = "none";
							} else if (i == 2) {
								YellowPlayerIII_Script.YellowPlayerIII_ColName = "none";
							} else if (i == 3) {
								YellowPlayerIV_Script.YellowPlayerIV_ColName = "none";
							}
							YellowPlayer_Steps [i] = 0;
							playerTurn = "GREEN";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
					}
				}



				if (currentPlayerName.Contains ("RED PLAYER")) 
				{
					i = 0;
					for (i = 0; i < 4; i++) {
						if ((i == 0 && currentPlayer == BluePlayerI_Script.BluePlayerI_ColName && (currentPlayer != "Star" && BluePlayerI_Script.BluePlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == BluePlayerII_Script.BluePlayerII_ColName && (currentPlayer != "Star" && BluePlayerII_Script.BluePlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == BluePlayerIII_Script.BluePlayerIII_ColName && (currentPlayer != "Star" && BluePlayerIII_Script.BluePlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == BluePlayerIV_Script.BluePlayerIV_ColName && (currentPlayer != "Star" && BluePlayerIV_Script.BluePlayerIV_ColName != "Star"))) {
							print (" RED  Beaten BluePlayer"+(i+1));
							BluePlayers [i].transform.position = BluePlayers_Pos [i];
							if (i == 0) {
								BluePlayerI_Script.BluePlayerI_ColName = "none";
							} else if (i == 1) {
								BluePlayerII_Script.BluePlayerII_ColName = "none";
							} else if (i == 2) {
								BluePlayerIII_Script.BluePlayerIII_ColName = "none";
							} else if (i == 3) {
								BluePlayerIV_Script.BluePlayerIV_ColName = "none";
							}
							BluePlayer_Steps [i] = 0;
							playerTurn = "RED";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
						if ((i == 0 && currentPlayer == GreenPlayerI_Script.GreenPlayerI_ColName && (currentPlayer != "Star" && GreenPlayerI_Script.GreenPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == GreenPlayerII_Script.GreenPlayerII_ColName && (currentPlayer != "Star" && GreenPlayerII_Script.GreenPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == GreenPlayerIII_Script.GreenPlayerIII_ColName && (currentPlayer != "Star" && GreenPlayerIII_Script.GreenPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == GreenPlayerIV_Script.GreenPlayerIV_ColName && (currentPlayer != "Star" && GreenPlayerIV_Script.GreenPlayerIV_ColName != "Star"))) {
							print (" RED  Beaten GreenPlayer"+(i+1));
							GreenPlayers [i].transform.position = GreenPlayers_Pos [i];
							if (i == 0) {
								GreenPlayerI_Script.GreenPlayerI_ColName = "none";
							} else if (i == 1) {
								GreenPlayerII_Script.GreenPlayerII_ColName = "none";
							} else if (i == 2) {
								GreenPlayerIII_Script.GreenPlayerIII_ColName = "none";
							} else if (i == 3) {
								GreenPlayerIV_Script.GreenPlayerIV_ColName = "none";
							}
							GreenPlayer_Steps [i] = 0;
							playerTurn = "RED";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}

						if ((i == 0 && currentPlayer == YellowPlayerI_Script.YellowPlayerI_ColName && (currentPlayer != "Star" && YellowPlayerI_Script.YellowPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == YellowPlayerII_Script.YellowPlayerII_ColName && (currentPlayer != "Star" && YellowPlayerII_Script.YellowPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == YellowPlayerIII_Script.YellowPlayerIII_ColName && (currentPlayer != "Star" && YellowPlayerIII_Script.YellowPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == YellowPlayerIV_Script.YellowPlayerIV_ColName && (currentPlayer != "Star" && YellowPlayerIV_Script.YellowPlayerIV_ColName != "Star"))) {
							print (" RED  Beaten YellowPlayer"+(i+1));
							YellowPlayers [i].transform.position = YellowPlayers_Pos [i];
							if (i == 0) {
								YellowPlayerI_Script.YellowPlayerI_ColName = "none";
							} else if (i == 1) {
								YellowPlayerII_Script.YellowPlayerII_ColName = "none";
							} else if (i == 2) {
								YellowPlayerIII_Script.YellowPlayerIII_ColName = "none";
							} else if (i == 3) {
								YellowPlayerIV_Script.YellowPlayerIV_ColName = "none";
							}
							YellowPlayer_Steps [i] = 0;
							playerTurn = "RED";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
					}
				}
				if (currentPlayerName.Contains ("YELLOW PLAYER")) 
				{
					i = 0;
					for (i = 0; i < 4; i++) {
						if ((i == 0 && currentPlayer == BluePlayerI_Script.BluePlayerI_ColName && (currentPlayer != "Star" && BluePlayerI_Script.BluePlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == BluePlayerII_Script.BluePlayerII_ColName && (currentPlayer != "Star" && BluePlayerII_Script.BluePlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == BluePlayerIII_Script.BluePlayerIII_ColName && (currentPlayer != "Star" && BluePlayerIII_Script.BluePlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == BluePlayerIV_Script.BluePlayerIV_ColName && (currentPlayer != "Star" && BluePlayerIV_Script.BluePlayerIV_ColName != "Star"))) {
							print (" YELLOW  Beaten BluePlayer"+(i+1));
							BluePlayers [i].transform.position = BluePlayers_Pos [i];
							if (i == 0) {
								BluePlayerI_Script.BluePlayerI_ColName = "none";
							} else if (i == 1) {
								BluePlayerII_Script.BluePlayerII_ColName = "none";
							} else if (i == 2) {
								BluePlayerIII_Script.BluePlayerIII_ColName = "none";
							} else if (i == 3) {
								BluePlayerIV_Script.BluePlayerIV_ColName = "none";
							}
							BluePlayer_Steps [i] = 0;
							playerTurn = "YELLOW";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
						if ((i == 0 && currentPlayer == GreenPlayerI_Script.GreenPlayerI_ColName && (currentPlayer != "Star" && GreenPlayerI_Script.GreenPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == GreenPlayerII_Script.GreenPlayerII_ColName && (currentPlayer != "Star" && GreenPlayerII_Script.GreenPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == GreenPlayerIII_Script.GreenPlayerIII_ColName && (currentPlayer != "Star" && GreenPlayerIII_Script.GreenPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == GreenPlayerIV_Script.GreenPlayerIV_ColName && (currentPlayer != "Star" && GreenPlayerIV_Script.GreenPlayerIV_ColName != "Star"))) {
							print (" YELLOW  Beaten GreenPlayer"+(i+1));
							GreenPlayers [i].transform.position = GreenPlayers_Pos [i];
							if (i == 0) {
								GreenPlayerI_Script.GreenPlayerI_ColName = "none";
							} else if (i == 1) {
								GreenPlayerII_Script.GreenPlayerII_ColName = "none";
							} else if (i == 2) {
								GreenPlayerIII_Script.GreenPlayerIII_ColName = "none";
							} else if (i == 3) {
								GreenPlayerIV_Script.GreenPlayerIV_ColName = "none";
							}
							GreenPlayer_Steps [i] = 0;
							playerTurn = "YELLOW";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}

						if ((i == 0 && currentPlayer == RedPlayerI_Script.RedPlayerI_ColName && (currentPlayer != "Star" && RedPlayerI_Script.RedPlayerI_ColName != "Star")) ||
							(i == 1 && currentPlayer == RedPlayerII_Script.RedPlayerII_ColName && (currentPlayer != "Star" && RedPlayerII_Script.RedPlayerII_ColName != "Star")) ||
							(i == 2 && currentPlayer == RedPlayerIII_Script.RedPlayerIII_ColName && (currentPlayer != "Star" && RedPlayerIII_Script.RedPlayerIII_ColName != "Star")) ||
							(i == 3 && currentPlayer == RedPlayerIV_Script.RedPlayerIV_ColName && (currentPlayer != "Star" && RedPlayerIV_Script.RedPlayerIV_ColName != "Star"))) {
							print (" YELLOW  Beaten RedPlayer"+(i+1));
							RedPlayers [i].transform.position = RedPlayers_Pos [i];
							if (i == 0) {
								RedPlayerI_Script.RedPlayerI_ColName = "none";
							} else if (i == 1) {
								RedPlayerII_Script.RedPlayerII_ColName = "none";
							} else if (i == 2) {
								RedPlayerIII_Script.RedPlayerIII_ColName = "none";
							} else if (i == 3) {
								RedPlayerIV_Script.RedPlayerIV_ColName = "none";
							}
							RedPlayer_Steps [i] = 0;
							playerTurn = "YELLOW";
							if (PhotonNetwork.IsMasterClient) {
								PlayerCanPlayAgain = true;
							}
							else 
							{

							}
						}
					}
				}
			}








			if (playerTurn == "BLUE") 
			{
				diceRoll.position = BlueDiceRollPosition.position;
				DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [6];
				TimerImage.transform.position = TimerTwoPosition;
				EnablingBluePlayersRaycast ();
				DisablingGreenPlayerRaycast ();
				DisablingRedPlayerRaycast ();
				DisablingGreenPlayerRaycast ();

				BlueFrame.SetActive (true);
				GreenFrame.SetActive (false);
				RedFrame.SetActive (false);
				YellowFrame.SetActive (false);
				TimerImage.fillAmount = 1;
			}
			if (playerTurn == "GREEN") 
			{
				diceRoll.position = GreenDiceRollPosition.position;
				DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [6];
				TimerImage.transform.position = TimerFourPosition;
				EnablingGreenPlayerRaycast ();

				DisablingYellowPlayerRaycast ();
				DisablingBluePlayersRaycast ();
				DisablingRedPlayerRaycast ();

				BlueFrame.SetActive (false);
				GreenFrame.SetActive (true);
				RedFrame.SetActive (false);
				YellowFrame.SetActive (false);
				TimerImage.fillAmount = 1;
			}
			if (playerTurn == "YELLOW")
			{
				diceRoll.position = YellowDicePosition.position;
				DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [6];
				TimerImage.transform.position = TimerOnePosition;
				EnablingYellowPlayerRaycast ();

				DisablingBluePlayersRaycast ();
				DisablingRedPlayerRaycast ();
				DisablingGreenPlayerRaycast ();

				YellowFrame.SetActive (true);
				BlueFrame.SetActive (false);
				GreenFrame.SetActive (false);
				RedFrame.SetActive (false);
				TimerImage.fillAmount = 1;
			}
			if(playerTurn=="RED")
			{
				diceRoll.position = YellowDicePosition.position;
				DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [6];
				TimerImage.transform.position = TimerThreePosition;
				EnablingRedPlayerRaycast ();

				DisablingYellowPlayerRaycast ();
				DisablingBluePlayersRaycast ();
				DisablingGreenPlayerRaycast ();

				RedFrame.SetActive (true);
				BlueFrame.SetActive (false);
				GreenFrame.SetActive (false);
				YellowFrame.SetActive (false);
				TimerImage.fillAmount = 1;
			}
			//=================disabling buttons==============//
			DisablingBordersOFBluePlayer();
			DisablingButtonsOFBluePlayes ();
			DisablingBordersOFGreenPlayer ();
			DisablingButtonsOfGreenPlayers ();
			selectDiceNumAnimation = 0;
			TimerImage.fillAmount = 1;
		}

		#endregion




		#region DiceRolling Related Methods


		public void DiceRoll()
		{
			if (isMyTurn) {
				print ("Hello");
				DiceRollButton.interactable = false;
				DiceRollButton.GetComponent<Button> ().enabled = false;
				selectDiceNumAnimation = Random.Range (0, 6);
				selectDiceNumAnimation += 1;

				CheckToChangePossibilityOfTwoPlayerATSamePosition ();

				if ((selectDiceNumAnimation == 6 ) && TrialOFTossing < 4) {

					OccuranceOfSix += 1;
					if (OccuranceOfSix == 1) {
						print ("One Time got 1 or 6");
					}
					if (OccuranceOfSix == 2) {
						print ("Two Times got 1 or 6");
					}
					if (OccuranceOfSix == 3) {
						print ("Occurance of six or One three times");
						selectDiceNumAnimation = Random.Range (1, 6);
						print ("Changed Number And Got" + selectDiceNumAnimation);
						DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation-1];
						TrialOFTossing = 0;
						OccuranceOfSix = 0;
					}
				} else {
					print ("TrialOFTossing" + TrialOFTossing + " Reset TrialOFTossing");
					TrialOFTossing = 0;
					OccuranceOfSix = 0;
				}

				print ("selectDiceNumAnimation:" + selectDiceNumAnimation);
				rootNode.Add ("DiceNumber", selectDiceNumAnimation.ToString(temp));
				temp = rootNode.ToString ();

				StartCoroutine (PlayersNotInitializedForTimeDelay (temp));
			}
		}


		void CheckToChangePossibilityOfTwoPlayerATSamePosition ()
		{
			if (playerTurn == "BLUE") {
				if (BluePlayer_Steps [0] > 0 &&
					((BluePlayer_Steps [0] + selectDiceNumAnimation == BluePlayer_Steps [1]) || (BluePlayer_Steps [0] + selectDiceNumAnimation == BluePlayer_Steps [2]) || (BluePlayer_Steps [0] + selectDiceNumAnimation == BluePlayer_Steps [3]))) {
					if (BluePlayer_Steps [0] + selectDiceNumAnimation != 9 && BluePlayer_Steps [0] + selectDiceNumAnimation != 14 && BluePlayer_Steps [0] + selectDiceNumAnimation != 22 && BluePlayer_Steps [0] + selectDiceNumAnimation != 27 &&
						BluePlayer_Steps [0] + selectDiceNumAnimation != 35 && BluePlayer_Steps [0] + selectDiceNumAnimation != 40 && BluePlayer_Steps [0] + selectDiceNumAnimation != 48 && BluePlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if ((BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [3]) &&
								BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [3] && 
								BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [3] &&
								BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (BluePlayer_Steps [1] > 0
					&& ((BluePlayer_Steps [1] + selectDiceNumAnimation == BluePlayer_Steps [0]) || (BluePlayer_Steps [1] + selectDiceNumAnimation == BluePlayer_Steps [2]) || (BluePlayer_Steps [1] + selectDiceNumAnimation == BluePlayer_Steps [3]))) {
					if (BluePlayer_Steps [1] + selectDiceNumAnimation != 9 && BluePlayer_Steps [1] + selectDiceNumAnimation != 14 && BluePlayer_Steps [1] + selectDiceNumAnimation != 22 && BluePlayer_Steps [1] + selectDiceNumAnimation != 27 &&
						BluePlayer_Steps [1] + selectDiceNumAnimation != 35 && BluePlayer_Steps [1] + selectDiceNumAnimation != 40 && BluePlayer_Steps [1] + selectDiceNumAnimation != 48 && BluePlayer_Steps [1] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if ((BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [3]) &&
								BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [3] && 
								BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [3] &&
								BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (BluePlayer_Steps [2] > 0 &&
					((BluePlayer_Steps [2] + selectDiceNumAnimation == BluePlayer_Steps [0]) || (BluePlayer_Steps [2] + selectDiceNumAnimation == BluePlayer_Steps [1]) || (BluePlayer_Steps [2] + selectDiceNumAnimation == BluePlayer_Steps [3]))) {
					if (BluePlayer_Steps [2] + selectDiceNumAnimation != 9 && BluePlayer_Steps [2] + selectDiceNumAnimation != 14 && BluePlayer_Steps [2] + selectDiceNumAnimation != 22 && BluePlayer_Steps [2] + selectDiceNumAnimation != 27 &&
						BluePlayer_Steps [2] + selectDiceNumAnimation != 35 && BluePlayer_Steps [2] + selectDiceNumAnimation != 40 && BluePlayer_Steps [2] + selectDiceNumAnimation != 48 && BluePlayer_Steps [2] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if ((BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [3]) &&
								BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [3] && 
								BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [3] &&
								BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (BluePlayer_Steps [3] > 0 &&
					((BluePlayer_Steps [3] + selectDiceNumAnimation == BluePlayer_Steps [0]) || (BluePlayer_Steps [3] + selectDiceNumAnimation == BluePlayer_Steps [1]) || (BluePlayer_Steps [3] + selectDiceNumAnimation == BluePlayer_Steps [2]))) {
					if (BluePlayer_Steps [3] + selectDiceNumAnimation != 9 && BluePlayer_Steps [3] + selectDiceNumAnimation != 14 && BluePlayer_Steps [3] + selectDiceNumAnimation != 22 && BluePlayer_Steps [3] + selectDiceNumAnimation != 27 &&
						BluePlayer_Steps [3] + selectDiceNumAnimation != 35 && BluePlayer_Steps [3] + selectDiceNumAnimation != 40 && BluePlayer_Steps [3] + selectDiceNumAnimation != 48 && BluePlayer_Steps [3] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if ((BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [0] + selectDiceNumAnimation != BluePlayer_Steps [3]) &&
								BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [2] && BluePlayer_Steps [1] + selectDiceNumAnimation != BluePlayer_Steps [3] && 
								BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [2] + selectDiceNumAnimation != BluePlayer_Steps [3] &&
								BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [0] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [1] && BluePlayer_Steps [3] + selectDiceNumAnimation != BluePlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				}
			}

			if (playerTurn == "GREEN") {
				if (GreenPlayer_Steps [0] > 0 &&
					((GreenPlayer_Steps [0] + selectDiceNumAnimation == GreenPlayer_Steps [1]) || (GreenPlayer_Steps [0] + selectDiceNumAnimation == GreenPlayer_Steps [2]) || (GreenPlayer_Steps [0] + selectDiceNumAnimation == GreenPlayer_Steps [3]))) {
					if (GreenPlayer_Steps [0] + selectDiceNumAnimation != 9 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 14 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 22 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 27 &&
						GreenPlayer_Steps [0] + selectDiceNumAnimation != 35 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 40 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 48 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [3] && 
								GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (GreenPlayer_Steps [1] > 0 &&
					((GreenPlayer_Steps [1] + selectDiceNumAnimation == GreenPlayer_Steps [0]) || (GreenPlayer_Steps [1] + selectDiceNumAnimation == GreenPlayer_Steps [2]) || (GreenPlayer_Steps [1] + selectDiceNumAnimation == GreenPlayer_Steps [3]))) {
					if (GreenPlayer_Steps [1] + selectDiceNumAnimation != 9 && GreenPlayer_Steps [1] + selectDiceNumAnimation != 14 && GreenPlayer_Steps [1] + selectDiceNumAnimation != 22 && GreenPlayer_Steps [1] + selectDiceNumAnimation != 27 &&
						GreenPlayer_Steps [1] + selectDiceNumAnimation != 35 && GreenPlayer_Steps [1] + selectDiceNumAnimation != 40 && GreenPlayer_Steps [1] + selectDiceNumAnimation != 48 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [3] && 
								GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [2]){
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (GreenPlayer_Steps [2] > 0 &&
					((GreenPlayer_Steps [2] + selectDiceNumAnimation == GreenPlayer_Steps [0]) || (GreenPlayer_Steps [2] + selectDiceNumAnimation == GreenPlayer_Steps [1]) || (GreenPlayer_Steps [2] + selectDiceNumAnimation == GreenPlayer_Steps [3]))) {
					if (GreenPlayer_Steps [2] + selectDiceNumAnimation != 9 && GreenPlayer_Steps [2] + selectDiceNumAnimation != 14 && GreenPlayer_Steps [2] + selectDiceNumAnimation != 22 && GreenPlayer_Steps [2] + selectDiceNumAnimation != 27 &&
						GreenPlayer_Steps [2] + selectDiceNumAnimation != 35 && GreenPlayer_Steps [2] + selectDiceNumAnimation != 40 && GreenPlayer_Steps [2] + selectDiceNumAnimation != 48 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [3] && 
								GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (GreenPlayer_Steps [3] > 0 &&
					((GreenPlayer_Steps [3] + selectDiceNumAnimation == GreenPlayer_Steps [0]) || (GreenPlayer_Steps [3] + selectDiceNumAnimation == GreenPlayer_Steps [1]) || (GreenPlayer_Steps [3] + selectDiceNumAnimation == GreenPlayer_Steps [2]))) {
					if (GreenPlayer_Steps [3] + selectDiceNumAnimation != 9 && GreenPlayer_Steps [3] + selectDiceNumAnimation != 14 && GreenPlayer_Steps [3] + selectDiceNumAnimation != 22 && GreenPlayer_Steps [3] + selectDiceNumAnimation != 27 &&
						GreenPlayer_Steps [3] + selectDiceNumAnimation != 35 && GreenPlayer_Steps [3] + selectDiceNumAnimation != 40 && GreenPlayer_Steps [3] + selectDiceNumAnimation != 48 && GreenPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [0] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [2] && GreenPlayer_Steps [1] + selectDiceNumAnimation != GreenPlayer_Steps [3] &&
								GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [2] + selectDiceNumAnimation != GreenPlayer_Steps [3] && 
								GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [0] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [1] && GreenPlayer_Steps [3] + selectDiceNumAnimation != GreenPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				}
			}



			if (playerTurn == "YELLOW") {
				if (YellowPlayer_Steps [0] > 0 &&
					((YellowPlayer_Steps [0] + selectDiceNumAnimation == YellowPlayer_Steps [1]) || (YellowPlayer_Steps [0] + selectDiceNumAnimation == YellowPlayer_Steps [2]) || (YellowPlayer_Steps [0] + selectDiceNumAnimation == YellowPlayer_Steps [3]))) {
					if (YellowPlayer_Steps [0] + selectDiceNumAnimation != 9 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 14 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 22 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 27 &&
						YellowPlayer_Steps [0] + selectDiceNumAnimation != 35 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 40 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 48 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [3] && 
								YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (YellowPlayer_Steps [1] > 0 &&
					((YellowPlayer_Steps [1] + selectDiceNumAnimation == YellowPlayer_Steps [0]) || (YellowPlayer_Steps [1] + selectDiceNumAnimation == YellowPlayer_Steps [2]) || (YellowPlayer_Steps [1] + selectDiceNumAnimation == YellowPlayer_Steps [3]))) {
					if (YellowPlayer_Steps [1] + selectDiceNumAnimation != 9 && YellowPlayer_Steps [1] + selectDiceNumAnimation != 14 && YellowPlayer_Steps [1] + selectDiceNumAnimation != 22 && YellowPlayer_Steps [1] + selectDiceNumAnimation != 27 &&
						YellowPlayer_Steps [1] + selectDiceNumAnimation != 35 && YellowPlayer_Steps [1] + selectDiceNumAnimation != 40 && YellowPlayer_Steps [1] + selectDiceNumAnimation != 48 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [3] && 
								YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [2]){
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (YellowPlayer_Steps [2] > 0 &&
					((YellowPlayer_Steps [2] + selectDiceNumAnimation == YellowPlayer_Steps [0]) || (YellowPlayer_Steps [2] + selectDiceNumAnimation == YellowPlayer_Steps [1]) || (YellowPlayer_Steps [2] + selectDiceNumAnimation == YellowPlayer_Steps [3]))) {
					if (YellowPlayer_Steps [2] + selectDiceNumAnimation != 9 && YellowPlayer_Steps [2] + selectDiceNumAnimation != 14 && YellowPlayer_Steps [2] + selectDiceNumAnimation != 22 && YellowPlayer_Steps [2] + selectDiceNumAnimation != 27 &&
						YellowPlayer_Steps [2] + selectDiceNumAnimation != 35 && YellowPlayer_Steps [2] + selectDiceNumAnimation != 40 && YellowPlayer_Steps [2] + selectDiceNumAnimation != 48 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [3] && 
								YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (YellowPlayer_Steps [3] > 0 &&
					((YellowPlayer_Steps [3] + selectDiceNumAnimation == YellowPlayer_Steps [0]) || (YellowPlayer_Steps [3] + selectDiceNumAnimation == YellowPlayer_Steps [1]) || (YellowPlayer_Steps [3] + selectDiceNumAnimation == YellowPlayer_Steps [2]))) {
					if (YellowPlayer_Steps [3] + selectDiceNumAnimation != 9 && YellowPlayer_Steps [3] + selectDiceNumAnimation != 14 && YellowPlayer_Steps [3] + selectDiceNumAnimation != 22 && YellowPlayer_Steps [3] + selectDiceNumAnimation != 27 &&
						YellowPlayer_Steps [3] + selectDiceNumAnimation != 35 && YellowPlayer_Steps [3] + selectDiceNumAnimation != 40 && YellowPlayer_Steps [3] + selectDiceNumAnimation != 48 && YellowPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [0] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [2] && YellowPlayer_Steps [1] + selectDiceNumAnimation != YellowPlayer_Steps [3] &&
								YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [2] + selectDiceNumAnimation != YellowPlayer_Steps [3] && 
								YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [0] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [1] && YellowPlayer_Steps [3] + selectDiceNumAnimation != YellowPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				}
			}

			if (playerTurn == "RED") {
				if (RedPlayer_Steps [0] > 0 &&
					((RedPlayer_Steps [0] + selectDiceNumAnimation == RedPlayer_Steps [1]) || (RedPlayer_Steps [0] + selectDiceNumAnimation == RedPlayer_Steps [2]) || (RedPlayer_Steps [0] + selectDiceNumAnimation == RedPlayer_Steps [3]))) {
					if (RedPlayer_Steps [0] + selectDiceNumAnimation != 9 && RedPlayer_Steps [0] + selectDiceNumAnimation != 14 && RedPlayer_Steps [0] + selectDiceNumAnimation != 22 && RedPlayer_Steps [0] + selectDiceNumAnimation != 27 &&
						RedPlayer_Steps [0] + selectDiceNumAnimation != 35 && RedPlayer_Steps [0] + selectDiceNumAnimation != 40 && RedPlayer_Steps [0] + selectDiceNumAnimation != 48 && RedPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [3] && 
								RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (RedPlayer_Steps [1] > 0 &&
					((RedPlayer_Steps [1] + selectDiceNumAnimation == RedPlayer_Steps [0]) || (RedPlayer_Steps [1] + selectDiceNumAnimation == RedPlayer_Steps [2]) || (RedPlayer_Steps [1] + selectDiceNumAnimation == RedPlayer_Steps [3]))) {
					if (RedPlayer_Steps [1] + selectDiceNumAnimation != 9 && RedPlayer_Steps [1] + selectDiceNumAnimation != 14 && RedPlayer_Steps [1] + selectDiceNumAnimation != 22 && RedPlayer_Steps [1] + selectDiceNumAnimation != 27 &&
						RedPlayer_Steps [1] + selectDiceNumAnimation != 35 && RedPlayer_Steps [1] + selectDiceNumAnimation != 40 && RedPlayer_Steps [1] + selectDiceNumAnimation != 48 && RedPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [3] && 
								RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [2]){
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (RedPlayer_Steps [2] > 0 &&
					((RedPlayer_Steps [2] + selectDiceNumAnimation == RedPlayer_Steps [0]) || (RedPlayer_Steps [2] + selectDiceNumAnimation == RedPlayer_Steps [1]) || (RedPlayer_Steps [2] + selectDiceNumAnimation == RedPlayer_Steps [3]))) {
					if (RedPlayer_Steps [2] + selectDiceNumAnimation != 9 && RedPlayer_Steps [2] + selectDiceNumAnimation != 14 && RedPlayer_Steps [2] + selectDiceNumAnimation != 22 && RedPlayer_Steps [2] + selectDiceNumAnimation != 27 &&
						RedPlayer_Steps [2] + selectDiceNumAnimation != 35 && RedPlayer_Steps [2] + selectDiceNumAnimation != 40 && RedPlayer_Steps [2] + selectDiceNumAnimation != 48 && RedPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [3] && 
								RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				} else if (RedPlayer_Steps [3] > 0 &&
					((RedPlayer_Steps [3] + selectDiceNumAnimation == RedPlayer_Steps [0]) || (RedPlayer_Steps [3] + selectDiceNumAnimation == RedPlayer_Steps [1]) || (RedPlayer_Steps [3] + selectDiceNumAnimation == RedPlayer_Steps [2]))) {
					if (RedPlayer_Steps [3] + selectDiceNumAnimation != 9 && RedPlayer_Steps [3] + selectDiceNumAnimation != 14 && RedPlayer_Steps [3] + selectDiceNumAnimation != 22 && RedPlayer_Steps [3] + selectDiceNumAnimation != 27 &&
						RedPlayer_Steps [3] + selectDiceNumAnimation != 35 && RedPlayer_Steps [3] + selectDiceNumAnimation != 40 && RedPlayer_Steps [3] + selectDiceNumAnimation != 48 && RedPlayer_Steps [0] + selectDiceNumAnimation != 57) {
						print ("Possibility was there AnimationNum" + selectDiceNumAnimation);
						selectDiceNumAnimation = 1;
						bool TempFlag = false;
						while (selectDiceNumAnimation != 6) {
							if (RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [0] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [2] && RedPlayer_Steps [1] + selectDiceNumAnimation != RedPlayer_Steps [3] &&
								RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [2] + selectDiceNumAnimation != RedPlayer_Steps [3] && 
								RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [0] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [1] && RedPlayer_Steps [3] + selectDiceNumAnimation != RedPlayer_Steps [2]) {
								selectDiceNumAnimation = selectDiceNumAnimation;
								DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [selectDiceNumAnimation - 1];
								TempFlag = true;
								break;
							} else {
								selectDiceNumAnimation++;
							}
							if (TempFlag)
								break;
						}
					}
				}
			}
		}


		IEnumerator PlayersNotInitializedForTimeDelay(string temp)
		{
			yield return new WaitForSeconds (0);
			switch (playerTurn) 
			{
			case "BLUE":
				//=============condition for border glow=============

				if ((blueMovemenBlock.Count - BluePlayer_Steps [0]) >= selectDiceNumAnimation && BluePlayer_Steps [0] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [0])) {
					BluePlayerI_Border.SetActive (true);
					BluePlayerI_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					BluePlayerI_Border.SetActive (false);
					BluePlayerI_Button.interactable = false;

				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [1]) >= selectDiceNumAnimation && BluePlayer_Steps [1] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [1])) {
					BluePlayerII_Border.SetActive (true);
					BluePlayerII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					BluePlayerII_Border.SetActive (false);
					BluePlayerII_Button.interactable = false;


				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [2]) >= selectDiceNumAnimation && BluePlayer_Steps [2] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [2])) {
					BluePlayerIII_Border.SetActive (true);
					BluePlayerIII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					BluePlayerIII_Border.SetActive (false);
					BluePlayerIII_Button.interactable = false;

				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [3]) >= selectDiceNumAnimation && BluePlayer_Steps [3] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [3])) {
					BluePlayerIV_Border.SetActive (true);
					BluePlayerIV_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
					print ("not Skipping");
				} else {
					BluePlayerIV_Border.SetActive (false);
					BluePlayerIV_Button.interactable = false;

				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && (BluePlayer_Steps [0] == 0 || BluePlayer_Steps [1] == 0 || BluePlayer_Steps [2] == 0 || BluePlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && BluePlayer_Steps [0] == 0) {
						BluePlayerI_Border.SetActive (true);
						BluePlayerI_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && BluePlayer_Steps [1] == 0) {
						BluePlayerII_Border.SetActive (true);
						BluePlayerII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && BluePlayer_Steps [2] == 0) {
						BluePlayerIII_Border.SetActive (true);
						BluePlayerIII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && BluePlayer_Steps [3] == 0) {
						BluePlayerIV_Border.SetActive (true);
						BluePlayerIV_Button.interactable = true;
					}
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Added:" + temp);
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!BluePlayerI_Border.activeInHierarchy && !BluePlayerII_Border.activeInHierarchy &&
					!BluePlayerIII_Border.activeInHierarchy && !BluePlayerIV_Border.activeInHierarchy) {
					rootNode.Add ("WaitingTime", "DontSkip");
					temp = rootNode.ToString ();
					print ("not Skipping");
				}

				BluePlayerI_Border.SetActive (false);
				BluePlayerI_Button.interactable = false;

				BluePlayerII_Border.SetActive (false);
				BluePlayerII_Button.interactable = false;

				BluePlayerIII_Border.SetActive (false);
				BluePlayerIII_Button.interactable = false;

				BluePlayerIV_Border.SetActive (false);
				BluePlayerIV_Button.interactable = false;

				break;
			case "GREEN":
				//=============condition for border glow=============

				if ((greenMovementBlock.Count - GreenPlayer_Steps [0]) >= selectDiceNumAnimation && GreenPlayer_Steps [0] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [0])) {
					GreenPlayerI_Border.SetActive (true);
					GreenPlayerI_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					GreenPlayerI_Border.SetActive (false);
					GreenPlayerI_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [1]) >= selectDiceNumAnimation && GreenPlayer_Steps [1] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [1])) {
					GreenPlayerII_Border.SetActive (true);
					GreenPlayerII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					GreenPlayerII_Border.SetActive (false);
					GreenPlayerII_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [2]) >= selectDiceNumAnimation && GreenPlayer_Steps [2] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [2])) {
					GreenPlayerIII_Border.SetActive (true);
					GreenPlayerIII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					GreenPlayerIII_Border.SetActive (false);
					GreenPlayerIII_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [3]) >= selectDiceNumAnimation && GreenPlayer_Steps [3] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [3])) {
					GreenPlayerIV_Border.SetActive (true);
					GreenPlayerIV_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					GreenPlayerIV_Border.SetActive (false);
					GreenPlayerIV_Button.interactable = false;
				}

				//===============Players border glow When Opening===============//

				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && (GreenPlayer_Steps [0] == 0 || GreenPlayer_Steps [1] == 0 || GreenPlayer_Steps [2] == 0 || GreenPlayer_Steps [3] == 0)) {
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [0] == 0) {
						GreenPlayerI_Border.SetActive (true);
						GreenPlayerI_Button.interactable = true;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [1] == 0) {
						GreenPlayerII_Border.SetActive (true);
						GreenPlayerII_Button.interactable = true;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [2] == 0) {
						GreenPlayerIII_Border.SetActive (true);
						GreenPlayerIII_Button.interactable = true;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [3] == 0) {
						GreenPlayerIV_Border.SetActive (true);
						GreenPlayerIV_Button.interactable = true;
					}
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
				}
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!GreenPlayerI_Border.activeInHierarchy && !GreenPlayerII_Border.activeInHierarchy &&
					!GreenPlayerIII_Border.activeInHierarchy && !GreenPlayerIV_Border.activeInHierarchy && selectDiceNumAnimation!=6) 
				{
					rootNode.Add ("WaitingTime", "DontSkip");
					temp = rootNode.ToString ();
					print ("not Skipping");
				}

				GreenPlayerI_Border.SetActive (false);
				GreenPlayerI_Button.interactable = false;

				GreenPlayerII_Border.SetActive (false);
				GreenPlayerII_Button.interactable = false;

				GreenPlayerIII_Border.SetActive (false);
				GreenPlayerIII_Button.interactable = false;

				GreenPlayerIV_Border.SetActive (false);
				GreenPlayerIV_Button.interactable = false;

				break;



			case "YELLOW":
				//=============condition for border glow=============

				if ((yellowMovementBlock.Count - YellowPlayer_Steps [0]) >= selectDiceNumAnimation && YellowPlayer_Steps [0] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [0])) {
					YellowPlayerI_Border.SetActive (true);
					YellowPlayerI_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					YellowPlayerI_Border.SetActive (false);
					YellowPlayerI_Button.interactable = false;

				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [1]) >= selectDiceNumAnimation && YellowPlayer_Steps [1] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [1])) {
					YellowPlayerII_Border.SetActive (true);
					YellowPlayerII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					YellowPlayerII_Border.SetActive (false);
					YellowPlayerII_Button.interactable = false;


				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [2]) >= selectDiceNumAnimation && YellowPlayer_Steps [2] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [2])) {
					YellowPlayerIII_Border.SetActive (true);
					YellowPlayerIII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					YellowPlayerIII_Border.SetActive (false);
					YellowPlayerIII_Button.interactable = false;

				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [3]) >= selectDiceNumAnimation && YellowPlayer_Steps [3] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [3])) {
					YellowPlayerIV_Border.SetActive (true);
					YellowPlayerIV_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
					print ("not Skipping");
				} else {
					YellowPlayerIV_Border.SetActive (false);
					YellowPlayerIV_Button.interactable = false;

				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && (YellowPlayer_Steps [0] == 0 || YellowPlayer_Steps [1] == 0 || YellowPlayer_Steps [2] == 0 || YellowPlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && YellowPlayer_Steps [0] == 0) {
						YellowPlayerI_Border.SetActive (true);
						YellowPlayerI_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && YellowPlayer_Steps [1] == 0) {
						YellowPlayerII_Border.SetActive (true);
						YellowPlayerII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && YellowPlayer_Steps [2] == 0) {
						YellowPlayerIII_Border.SetActive (true);
						YellowPlayerIII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && YellowPlayer_Steps [3] == 0) {
						YellowPlayerIV_Border.SetActive (true);
						YellowPlayerIV_Button.interactable = true;
					}
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Added:" + temp);
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!YellowPlayerI_Border.activeInHierarchy && !YellowPlayerII_Border.activeInHierarchy &&
					!YellowPlayerIII_Border.activeInHierarchy && !YellowPlayerIV_Border.activeInHierarchy) {
					rootNode.Add ("WaitingTime", "DontSkip");
					temp = rootNode.ToString ();
					print ("not Skipping");
				}

				YellowPlayerI_Border.SetActive (false);
				YellowPlayerI_Button.interactable = false;

				YellowPlayerII_Border.SetActive (false);
				YellowPlayerII_Button.interactable = false;

				YellowPlayerIII_Border.SetActive (false);
				YellowPlayerIII_Button.interactable = false;

				YellowPlayerIV_Border.SetActive (false);
				YellowPlayerIV_Button.interactable = false;

				break;


			case "RED":
				//=============condition for border glow=============

				if ((redMovementBlock.Count - RedPlayer_Steps [0]) >= selectDiceNumAnimation && RedPlayer_Steps [0] > 0 && (yellowMovementBlock.Count > RedPlayer_Steps [0])) {
					RedPlayerI_Border.SetActive (true);
					RedPlayerI_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					RedPlayerI_Border.SetActive (false);
					RedPlayerI_Button.interactable = false;

				}
				if ((redMovementBlock.Count - RedPlayer_Steps [1]) >= selectDiceNumAnimation && RedPlayer_Steps [1] > 0 && (redMovementBlock.Count > RedPlayer_Steps [1])) {
					RedPlayerII_Border.SetActive (true);
					RedPlayerII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					RedPlayerII_Border.SetActive (false);
					RedPlayerII_Button.interactable = false;


				}
				if ((redMovementBlock.Count - RedPlayer_Steps [2]) >= selectDiceNumAnimation && RedPlayer_Steps [2] > 0 && (redMovementBlock.Count > RedPlayer_Steps [2])) {
					RedPlayerIII_Border.SetActive (true);
					RedPlayerIII_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
				} else {
					RedPlayerIII_Border.SetActive (false);
					RedPlayerIII_Button.interactable = false;

				}
				if ((redMovementBlock.Count - RedPlayer_Steps [3]) >= selectDiceNumAnimation && RedPlayer_Steps [3] > 0 && (redMovementBlock.Count > RedPlayer_Steps [3])) {
					RedPlayerIV_Border.SetActive (true);
					RedPlayerIV_Button.interactable = true;
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Border glowing");
					print ("not Skipping");
				} else {
					RedPlayerIV_Border.SetActive (false);
					RedPlayerIV_Button.interactable = false;

				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && (RedPlayer_Steps [0] == 0 || RedPlayer_Steps [1] == 0 || RedPlayer_Steps [2] == 0 || RedPlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && RedPlayer_Steps [0] == 0) {
						RedPlayerI_Border.SetActive (true);
						RedPlayerI_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && RedPlayer_Steps [1] == 0) {
						RedPlayerII_Border.SetActive (true);
						RedPlayerII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && RedPlayer_Steps [2] == 0) {
						RedPlayerIII_Border.SetActive (true);
						RedPlayerIII_Button.interactable = true;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && RedPlayer_Steps [3] == 0) {
						RedPlayerIV_Border.SetActive (true);
						RedPlayerIV_Button.interactable = true;
					}
					rootNode.Add ("WaitingTime", "Skip");
					temp = rootNode.ToString ();
					print ("Added:" + temp);
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!RedPlayerI_Border.activeInHierarchy && !RedPlayerII_Border.activeInHierarchy &&
					!RedPlayerIII_Border.activeInHierarchy && !RedPlayerIV_Border.activeInHierarchy) {
					rootNode.Add ("WaitingTime", "DontSkip");
					temp = rootNode.ToString ();
					print ("not Skipping");
				}

				RedPlayerI_Border.SetActive (false);
				RedPlayerI_Button.interactable = false;

				RedPlayerII_Border.SetActive (false);
				RedPlayerII_Button.interactable = false;

				RedPlayerIII_Border.SetActive (false);
				RedPlayerIII_Button.interactable = false;

				RedPlayerIV_Border.SetActive (false);
				RedPlayerIV_Button.interactable = false;

				break;
			}

			this.MakeTurn (temp);
		}



		IEnumerator DiceToss(int DiceNumber)
		{
			int randomDice = 0;
			for (int i = 0; i < 8;i++) 
			{
				randomDice = Random.Range (0, 6);
				DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [randomDice];
				yield return new WaitForSeconds(.12f);
			}
			DiceRollButton.GetComponent<Image> ().sprite = DiceSprite [DiceNumber-1];
			selectDiceNumAnimation = DiceNumber;

			StartCoroutine (PlayersNotInitialized ());
		}
		//====================Checks Which player can move After sending the data=====================//
		IEnumerator PlayersNotInitialized()
		{
			yield return new WaitForSeconds (.8f);
			//game start initial position of each player(green and blue)
			switch (playerTurn) 
			{



			case "YELLOW":
				//=============condition for border glow=============

				if ((yellowMovementBlock.Count - YellowPlayer_Steps [0]) >= selectDiceNumAnimation && YellowPlayer_Steps [0] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [0])) {
					YellowPlayerI_Border.SetActive (true);
					YellowPlayerI_Button.interactable = true;
					OnlyOneYellowPiceCanMove += 1;
				} else {
					YellowPlayerI_Border.SetActive (false);
					YellowPlayerI_Button.interactable = false;
				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [1]) >= selectDiceNumAnimation && YellowPlayer_Steps [1] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [1])) {
					YellowPlayerII_Border.SetActive (true);
					YellowPlayerII_Button.interactable = true;
					OnlyOneYellowPiceCanMove += 1;
				} else {
					YellowPlayerII_Border.SetActive (false);
					YellowPlayerII_Button.interactable = false;
				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [2]) >= selectDiceNumAnimation && YellowPlayer_Steps [2] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [2])) {
					YellowPlayerIII_Border.SetActive (true);
					YellowPlayerIII_Button.interactable = true;
					OnlyOneYellowPiceCanMove += 1;
				} else {
					YellowPlayerIII_Border.SetActive (false);
					YellowPlayerIII_Button.interactable = false;
				}
				if ((yellowMovementBlock.Count - YellowPlayer_Steps [3]) >= selectDiceNumAnimation && YellowPlayer_Steps [3] > 0 && (yellowMovementBlock.Count > YellowPlayer_Steps [3])) {
					YellowPlayerIV_Border.SetActive (true);
					YellowPlayerIV_Button.interactable = true;
					OnlyOneYellowPiceCanMove += 1;
				} else {
					YellowPlayerIV_Border.SetActive (false);
					YellowPlayerIV_Button.interactable = false;
				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && (YellowPlayer_Steps [0] == 0 || YellowPlayer_Steps [1] == 0 || YellowPlayer_Steps [2] == 0 || YellowPlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [0] == 0) {
						YellowPlayerI_Border.SetActive (true);
						YellowPlayerI_Button.interactable = true;
						OnlyOneYellowPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [1] == 0) {
						YellowPlayerII_Border.SetActive (true);
						YellowPlayerII_Button.interactable = true;
						OnlyOneYellowPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [2] == 0) {
						YellowPlayerIII_Border.SetActive (true);
						YellowPlayerIII_Button.interactable = true;
						OnlyOneYellowPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [3] == 0) {
						YellowPlayerIV_Border.SetActive (true);
						YellowPlayerIV_Button.interactable = true;
						OnlyOneYellowPiceCanMove += 1;
					}
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!YellowPlayerI_Border.activeInHierarchy && !YellowPlayerII_Border.activeInHierarchy &&
					!YellowPlayerIII_Border.activeInHierarchy && !YellowPlayerIV_Border.activeInHierarchy)
				{
					print ("YELLOW PLAYER DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN");
					DisablingButtonsOfYellowPlayer ();
					playerTurn = "BLUE";
					InitializeDice ();
				}
				break;





			case "BLUE":
				//=============condition for border glow=============

				if ((blueMovemenBlock.Count - BluePlayer_Steps [0]) >= selectDiceNumAnimation && BluePlayer_Steps [0] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [0])) {
					BluePlayerI_Border.SetActive (true);
					BluePlayerI_Button.interactable = true;
					OnlyOneBluePiceCanMove += 1;
				} else {
					BluePlayerI_Border.SetActive (false);
					BluePlayerI_Button.interactable = false;
				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [1]) >= selectDiceNumAnimation && BluePlayer_Steps [1] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [1])) {
					BluePlayerII_Border.SetActive (true);
					BluePlayerII_Button.interactable = true;
					OnlyOneBluePiceCanMove += 1;
				} else {
					BluePlayerII_Border.SetActive (false);
					BluePlayerII_Button.interactable = false;
				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [2]) >= selectDiceNumAnimation && BluePlayer_Steps [2] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [2])) {
					BluePlayerIII_Border.SetActive (true);
					BluePlayerIII_Button.interactable = true;
					OnlyOneBluePiceCanMove += 1;
				} else {
					BluePlayerIII_Border.SetActive (false);
					BluePlayerIII_Button.interactable = false;
				}
				if ((blueMovemenBlock.Count - BluePlayer_Steps [3]) >= selectDiceNumAnimation && BluePlayer_Steps [3] > 0 && (blueMovemenBlock.Count > BluePlayer_Steps [3])) {
					BluePlayerIV_Border.SetActive (true);
					BluePlayerIV_Button.interactable = true;
					OnlyOneBluePiceCanMove += 1;
				} else {
					BluePlayerIV_Border.SetActive (false);
					BluePlayerIV_Button.interactable = false;
				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && (BluePlayer_Steps [0] == 0 || BluePlayer_Steps [1] == 0 || BluePlayer_Steps [2] == 0 || BluePlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [0] == 0) {
						BluePlayerI_Border.SetActive (true);
						BluePlayerI_Button.interactable = true;
						OnlyOneBluePiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [1] == 0) {
						BluePlayerII_Border.SetActive (true);
						BluePlayerII_Button.interactable = true;
						OnlyOneBluePiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [2] == 0) {
						BluePlayerIII_Border.SetActive (true);
						BluePlayerIII_Button.interactable = true;
						OnlyOneBluePiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [3] == 0) {
						BluePlayerIV_Border.SetActive (true);
						BluePlayerIV_Button.interactable = true;
						OnlyOneBluePiceCanMove += 1;
					}
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!BluePlayerI_Border.activeInHierarchy && !BluePlayerII_Border.activeInHierarchy &&
					!BluePlayerIII_Border.activeInHierarchy && !BluePlayerIV_Border.activeInHierarchy)
				{

					DisablingButtonsOFBluePlayes ();
					playerTurn = "RED";
					print ("BLUE PLAYER DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN");
					InitializeDice ();
				}


				if ((BluePlayerI_Border.activeInHierarchy || BluePlayerII_Border.activeInHierarchy || BluePlayerIII_Border.activeInHierarchy || BluePlayerIV_Border.activeInHierarchy) && OnlyOneBluePiceCanMove == 1) {
					StartCoroutine (BluePiceAutoMove ());
				}


				break;




			case "RED":
				//=============condition for border glow=============

				if ((redMovementBlock.Count - RedPlayer_Steps [0]) >= selectDiceNumAnimation && RedPlayer_Steps [0] > 0 && (redMovementBlock.Count > RedPlayer_Steps [0])) {
					RedPlayerI_Border.SetActive (true);
					RedPlayerI_Button.interactable = true;
					OnlyOneRedPiceCanMove += 1;
				} else {
					RedPlayerI_Border.SetActive (false);
					RedPlayerI_Button.interactable = false;
				}
				if ((redMovementBlock.Count - RedPlayer_Steps [1]) >= selectDiceNumAnimation && RedPlayer_Steps [1] > 0 && (redMovementBlock.Count > RedPlayer_Steps [1])) {
					RedPlayerII_Border.SetActive (true);
					RedPlayerII_Button.interactable = true;
					OnlyOneRedPiceCanMove += 1;
				} else {
					RedPlayerII_Border.SetActive (false);
					RedPlayerII_Button.interactable = false;
				}
				if ((redMovementBlock.Count - RedPlayer_Steps [2]) >= selectDiceNumAnimation && RedPlayer_Steps [2] > 0 && (redMovementBlock.Count > RedPlayer_Steps [2])) {
					RedPlayerIII_Border.SetActive (true);
					RedPlayerIII_Button.interactable = true;
					OnlyOneRedPiceCanMove += 1;
				} else {
					RedPlayerIII_Border.SetActive (false);
					RedPlayerIII_Button.interactable = false;
				}
				if ((redMovementBlock.Count - RedPlayer_Steps [3]) >= selectDiceNumAnimation && RedPlayer_Steps [3] > 0 && (redMovementBlock.Count > RedPlayer_Steps [3])) {
					RedPlayerIV_Border.SetActive (true);
					RedPlayerIV_Button.interactable = true;
					OnlyOneRedPiceCanMove += 1;
				} else {
					RedPlayerIV_Border.SetActive (false);
					RedPlayerIV_Button.interactable = false;
				}
				//===============Players border glow When Opening===============//
				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && (RedPlayer_Steps [0] == 0 || RedPlayer_Steps [1] == 0 || RedPlayer_Steps [2] == 0 || RedPlayer_Steps [3] == 0)) {
					print ("Players border glow When Opening");
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps [0] == 0) {
						RedPlayerI_Border.SetActive (true);
						RedPlayerI_Button.interactable = true;
						OnlyOneRedPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps [1] == 0) {
						RedPlayerII_Border.SetActive (true);
						RedPlayerII_Button.interactable = true;
						OnlyOneRedPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps [2] == 0) {
						RedPlayerIII_Border.SetActive (true);
						RedPlayerIII_Button.interactable = true;
						OnlyOneRedPiceCanMove += 1;
					}

					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps [3] == 0) {
						RedPlayerIV_Border.SetActive (true);
						RedPlayerIV_Button.interactable = true;
						OnlyOneRedPiceCanMove += 1;
					}
				}	
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!RedPlayerI_Border.activeInHierarchy && !RedPlayerII_Border.activeInHierarchy &&
					!RedPlayerIII_Border.activeInHierarchy && !RedPlayerIV_Border.activeInHierarchy)
				{
					print ("RED PLAYER DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN");
					DisablingButtonsOfRedPlayer();
					playerTurn = "GREEN";
					InitializeDice ();
				}
				break;




			case "GREEN":
				//=============condition for border glow=============

				if ((greenMovementBlock.Count - GreenPlayer_Steps [0]) >= selectDiceNumAnimation && GreenPlayer_Steps [0] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [0])) {
					GreenPlayerI_Border.SetActive (true);
					GreenPlayerI_Button.interactable = true;
					OnlyOneGreenPiceCanMove += 1;
					//	PlayerCanPlayAgain = true;

					print ("Glowing 1st green Player");
				} else {
					GreenPlayerI_Border.SetActive (false);
					GreenPlayerI_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [1]) >= selectDiceNumAnimation && GreenPlayer_Steps [1] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [1])) {
					GreenPlayerII_Border.SetActive (true);
					GreenPlayerII_Button.interactable = true;
					OnlyOneGreenPiceCanMove += 1;
					print ("Glowing 2nd green Player");
				} else {
					GreenPlayerII_Border.SetActive (false);
					GreenPlayerII_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [2]) >= selectDiceNumAnimation && GreenPlayer_Steps [2] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [2])) {
					GreenPlayerIII_Border.SetActive (true);
					GreenPlayerIII_Button.interactable = true;
					OnlyOneGreenPiceCanMove += 1;
					print ("Glowing 3rd green Player");
				} else {
					GreenPlayerIII_Border.SetActive (false);
					GreenPlayerIII_Button.interactable = false;
				}
				if ((greenMovementBlock.Count - GreenPlayer_Steps [3]) >= selectDiceNumAnimation && GreenPlayer_Steps [3] > 0 && (greenMovementBlock.Count > GreenPlayer_Steps [3])) {
					GreenPlayerIV_Border.SetActive (true);
					GreenPlayerIV_Button.interactable = true;
					OnlyOneGreenPiceCanMove += 1;
					print ("Glowing 4th green Player");
				} else {
					GreenPlayerIV_Border.SetActive (false);
					GreenPlayerIV_Button.interactable = false;
				}

				//===============Players border glow When Opening===============//

				if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && (GreenPlayer_Steps [0] == 0 || GreenPlayer_Steps [1] == 0 || GreenPlayer_Steps [2] == 0 || GreenPlayer_Steps [3] == 0)) {
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [0] == 0) {
						print ("Glowing 1st green Player");
						GreenPlayerI_Border.SetActive (true);
						GreenPlayerI_Button.interactable = true;
						OnlyOneGreenPiceCanMove += 1;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [1] == 0) {
						print ("Glowing 2nd green Player");
						GreenPlayerII_Border.SetActive (true);
						GreenPlayerII_Button.interactable = true;
						OnlyOneGreenPiceCanMove += 1;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [2] == 0) {
						print ("Glowing 3rd green Player");
						GreenPlayerIII_Border.SetActive (true);
						GreenPlayerIII_Button.interactable = true;
						OnlyOneGreenPiceCanMove += 1;
					}
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation == 1) && GreenPlayer_Steps [3] == 0) {
						print ("Glowing 4th green Player");
						GreenPlayerIV_Border.SetActive (true);
						GreenPlayerIV_Button.interactable = true;
						OnlyOneGreenPiceCanMove += 1;
					}
				}
				//=========================PLAYERS DON'T HAVE ANY MOVES , SWITCH TO NEXT PLAYER'S TURN=========================//

				if (!GreenPlayerI_Border.activeInHierarchy && !GreenPlayerII_Border.activeInHierarchy &&
					!GreenPlayerIII_Border.activeInHierarchy && !GreenPlayerIV_Border.activeInHierarchy && selectDiceNumAnimation!=6) 
				{
					DisablingButtonsOfGreenPlayers ();
					print ("GREEN PLAYER DON'T HAVE OPTION TO MOVE , SWITCH TO NEXT PLAYER TURN");
					DisablingButtonsOfGreenPlayers ();
					playerTurn = "YELLOW";
					InitializeDice ();
				}

				break;
			}
		}


		IEnumerator YellowPiceAutoMove()
		{
			yield return new WaitForSeconds (1);
			if (YellowPlayerI_Border.activeInHierarchy && OnlyOneYellowPiceCanMove == 1) {
				YellowPlayerI_UI_Method ();
			}else if (YellowPlayerII_Border.activeInHierarchy && OnlyOneYellowPiceCanMove == 1) {
				YellowPlayerII_UI_Method ();
			}else if (YellowPlayerIII_Border.activeInHierarchy && OnlyOneYellowPiceCanMove == 1) {
				YellowPlayerIII_UI_Method ();
			}else if (YellowPlayerIV_Border.activeInHierarchy && OnlyOneYellowPiceCanMove == 1) {
				YellowPlayerIV_UI_Method ();
			}
		}

		IEnumerator BluePiceAutoMove()
		{
			yield return new WaitForSeconds (1);
			if (BluePlayerI_Border.activeInHierarchy && OnlyOneBluePiceCanMove == 1) {
				BluePlayerI_UI_Method ();
			} else if (BluePlayerII_Border.activeInHierarchy && OnlyOneBluePiceCanMove == 1) {
				BluePlayerII_UI_Method();
			} else if (BluePlayerIII_Border.activeInHierarchy && OnlyOneBluePiceCanMove == 1) {
				BluePlayerIII_UI_Method();
			} else if (BluePlayerI_Border.activeInHierarchy && OnlyOneBluePiceCanMove == 1) {
				BluePlayerIV_UI_Method();
			}
		}

		IEnumerator RedPiceAutoMove()
		{
			yield return new WaitForSeconds (1);
			if (RedPlayerI_Border.activeInHierarchy && OnlyOneRedPiceCanMove == 1) {
				RedPlayerI_UI_Method ();
			}else if (RedPlayerII_Border.activeInHierarchy && OnlyOneRedPiceCanMove == 1) {
				RedPlayerII_UI_Method ();
			}else if (RedPlayerIII_Border.activeInHierarchy && OnlyOneRedPiceCanMove == 1) {
				RedPlayerIII_UI_Method ();
			}else if (RedPlayerIV_Border.activeInHierarchy && OnlyOneRedPiceCanMove == 1) {
				RedPlayerIV_UI_Method ();
			}
		}

		IEnumerator GreenPiceAutoMove()
		{
			yield return new WaitForSeconds(1);
			if (GreenPlayerI_Border.activeInHierarchy && OnlyOneGreenPiceCanMove == 1) {
				GreenPlayerI_UI_Method ();
			} else if (GreenPlayerII_Border.activeInHierarchy && OnlyOneGreenPiceCanMove == 1) {
				GreenPlayerII_UI_Method ();
			} else if (GreenPlayerIII_Border.activeInHierarchy && OnlyOneGreenPiceCanMove == 1) {
				GreenPlayerIII_UI_Method ();
			} else if (GreenPlayerIV_Border.activeInHierarchy && OnlyOneGreenPiceCanMove == 1) {
				GreenPlayerIV_UI_Method ();
			}
		}

		#endregion



		public void YellowPlayerI_UI()
		{
			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}
			print ("YellowPlayerI_UI()");
			//disabling YellowPlayer border and Button script
			if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [0]) > selectDiceNumAnimation) 
			{
				if (YellowPlayer_Steps [0] > 0) 
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [0] + i].transform.position;
					}

					YellowPlayer_Steps [0] += selectDiceNumAnimation;
					//============Keeping the Turn to Yellow Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "YELLOW";	
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "BLUE";
					}
					currentPlayerName = "YELLOW PLAYER I";
					MovingBlueOrGreenPlayer (YellowPlayerI, yellowPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [0] == 0)
					{
						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}
						print ("YELLOW PlayerI Player moving its first move");
						Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];
						yellowPlayer_Path [0] = yellowMovementBlock [YellowPlayer_Steps [0]].transform.position;
						YellowPlayer_Steps [0] += 1;
						playerTurn = "YELLOW";
						currentPlayerName = "YELLOW PLAYER I";
						iTween.MoveTo (YellowPlayerI, iTween.Hash ("position", yellowPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [0]) == selectDiceNumAnimation)
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [0] + i].transform.position;
					}

					YellowPlayer_Steps [0] += selectDiceNumAnimation;

					playerTurn = "YELLOW";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (YellowPlayerI, YellowPiceSafeHouse[0] );
					totalYellowInHouse += 1;
					print ("Cool");
					YellowPlayerI_Button.enabled = false;

					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (yellowMovementBlock.Count - YellowPlayer_Steps [0]).ToString () + "To enter in safe house");
					if (YellowPlayer_Steps [1] + YellowPlayer_Steps [2] + YellowPlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}

		//This is YellowPlayer 2n Pice method by Executing this 2nd pice of YellowPlayer can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of Yellow player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void YellowPlayerII_UI()
		{
			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}
			print ("YellowPlayerII_UI()");
			//disabling YellowPlayer border and Button script
			if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [1]) > selectDiceNumAnimation) 
			{
				if (YellowPlayer_Steps [1] > 0) 
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [1] + i].transform.position;
					}

					YellowPlayer_Steps [1] += selectDiceNumAnimation;
					//============Keeping the Turn to Yellow Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "YELLOW";	
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "BLUE";
					}
					currentPlayerName = "YELLOW PLAYER II";
					MovingBlueOrGreenPlayer (YellowPlayerII, yellowPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [1] == 0)
					{
						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}
						print ("YELLOWPlayerI Player moving its first move");
						Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];
						yellowPlayer_Path [1] = yellowMovementBlock [YellowPlayer_Steps [1]].transform.position;
						YellowPlayer_Steps [1] += 1;
						playerTurn = "YELLOW";
						currentPlayerName = "YELLOW PLAYER II";
						iTween.MoveTo (YellowPlayerII, iTween.Hash ("position", yellowPlayer_Path [1], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [1]) == selectDiceNumAnimation)
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [1] + i].transform.position;
					}

					YellowPlayer_Steps [1] += selectDiceNumAnimation;

					playerTurn = "YELLOW";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (YellowPlayerI, YellowPiceSafeHouse[1] );
					totalYellowInHouse += 1;
					print ("Cool");
					YellowPlayerII_Button.enabled = false;

					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (yellowMovementBlock.Count - YellowPlayer_Steps [1]).ToString () + "To enter in safe house");
					if (YellowPlayer_Steps [0] + YellowPlayer_Steps [2] + YellowPlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}



		public void YellowPlayerIII_UI()
		{
			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}
			print ("YellowPlayerIII_UI()");
			//disabling YellowPlayer border and Button script
			if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [2]) > selectDiceNumAnimation) 
			{
				if (YellowPlayer_Steps [2] > 0) 
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [2] + i].transform.position;
					}

					YellowPlayer_Steps [2] += selectDiceNumAnimation;
					//============Keeping the Turn to Yellow Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "YELLOW";	
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "BLUE";
					}
					currentPlayerName = "YELLOW PLAYER III";
					MovingBlueOrGreenPlayer (YellowPlayerIII, yellowPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [2] == 0)
					{
						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}
						print ("YELLOWPlayerI Player moving its first move");
						Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];
						yellowPlayer_Path [2] = yellowMovementBlock [YellowPlayer_Steps [2]].transform.position;
						YellowPlayer_Steps [2] += 1;
						playerTurn = "YELLOW";
						currentPlayerName = "YELLOW PLAYER III";
						iTween.MoveTo (YellowPlayerIII, iTween.Hash ("position", yellowPlayer_Path [2], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [2]) == selectDiceNumAnimation)
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [2] + i].transform.position;
					}

					YellowPlayer_Steps [2] += selectDiceNumAnimation;

					playerTurn = "YELLOW";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (YellowPlayerI, YellowPiceSafeHouse[2] );
					totalYellowInHouse += 1;
					print ("Cool");
					YellowPlayerIII_Button.enabled = false;

					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (yellowMovementBlock.Count - YellowPlayer_Steps [2]).ToString () + "To enter in safe house");
					if (YellowPlayer_Steps [0] + YellowPlayer_Steps [1] + YellowPlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}



		public void YellowPlayerIV_UI()
		{
			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}
			print ("YellowPlayerIV_UI()");
			//disabling YellowPlayer border and Button script
			if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [3]) > selectDiceNumAnimation) 
			{
				if (YellowPlayer_Steps [3] > 0) 
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [3] + i].transform.position;
					}

					YellowPlayer_Steps [3] += selectDiceNumAnimation;
					//============Keeping the Turn to Yellow Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "YELLOW";	
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "BLUE";
					}
					currentPlayerName = "YELLOW PLAYER IV";
					MovingBlueOrGreenPlayer (YellowPlayerIV, yellowPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && YellowPlayer_Steps [3] == 0)
					{
						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}
						print ("YELLOWPlayerI Player moving its first move");
						Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];
						yellowPlayer_Path [3] = yellowMovementBlock [YellowPlayer_Steps [3]].transform.position;
						YellowPlayer_Steps [3] += 1;
						playerTurn = "YELLOW";
						currentPlayerName = "YELLOW PLAYER III";
						iTween.MoveTo (YellowPlayerIV, iTween.Hash ("position", yellowPlayer_Path [3], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "YELLOW" && (yellowMovementBlock.Count - YellowPlayer_Steps [3]) == selectDiceNumAnimation)
				{
					Vector3[] yellowPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						yellowPlayer_Path [i] = yellowMovementBlock [YellowPlayer_Steps [3] + i].transform.position;
					}

					YellowPlayer_Steps [3] += selectDiceNumAnimation;

					playerTurn = "YELLOW";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (YellowPlayerI, YellowPiceSafeHouse[3] );
					totalYellowInHouse += 1;
					print ("Cool");
					YellowPlayerIV_Button.enabled = false;

					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (yellowMovementBlock.Count - YellowPlayer_Steps [3]).ToString () + "To enter in safe house");
					if (YellowPlayer_Steps [0] + YellowPlayer_Steps [1] + YellowPlayer_Steps [2] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}



		//This is BluePlayer 1st Pice method by Executing this 1st pice of BluePlayer can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of blue player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void BluePlayerI_UI()
		{
//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}
			print ("BluePlayerI_UI()");
			//disabling BluePlayer border and Button script
			if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [0]) > selectDiceNumAnimation) 
			{
				if (BluePlayer_Steps [0] > 0) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [0] + i].transform.position;
					}

					BluePlayer_Steps [0] += selectDiceNumAnimation;
					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "BLUE";	
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "GREEN";
					}
					currentPlayerName = "BLUE PLAYER I";
					MovingBlueOrGreenPlayer (BluePlayerI, bluePlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [0] == 0)
					{
//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}
						print ("BluePlayerI Player moving its first move");
						Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];
						bluePlayer_Path [0] = blueMovemenBlock [BluePlayer_Steps [0]].transform.position;
						BluePlayer_Steps [0] += 1;
						playerTurn = "BLUE";
						currentPlayerName = "BLUE PLAYER I";
						iTween.MoveTo (BluePlayerI, iTween.Hash ("position", bluePlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [0]) == selectDiceNumAnimation)
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [0] + i].transform.position;
					}

					BluePlayer_Steps [0] += selectDiceNumAnimation;

					playerTurn = "BLUE";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (BluePlayerI, BluePiceSafeHouse[0] );
					totalBlueInHouse += 1;
					print ("Cool");
					BluePlayerI_Button.enabled = false;

					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (blueMovemenBlock.Count - BluePlayer_Steps [0]).ToString () + "To enter in safe house");
					if (BluePlayer_Steps [1] + BluePlayer_Steps [2] + BluePlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "GREEN";
						InitializeDice();
					}
				}
			}
		}

		//This is BluePlayer 2n Pice method by Executing this 2nd pice of BluePlayer can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of blue player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void BluePlayerII_UI()
		{
//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}
			print ("BluePlayerII_UI()");
			if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [1]) > selectDiceNumAnimation)
			{
				if (BluePlayer_Steps [1] > 0) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [1] + i].transform.position;
					}

					BluePlayer_Steps [1] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "BLUE";
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "BLUE PLAYER II";
					MovingBlueOrGreenPlayer (BluePlayerII, bluePlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [1] == 0)
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];
						bluePlayer_Path [0] = blueMovemenBlock [BluePlayer_Steps [1]].transform.position;
						BluePlayer_Steps [1] += 1;
						playerTurn = "BLUE";
						currentPlayerName = "BLUE PLAYER II";
						iTween.MoveTo (BluePlayerII, iTween.Hash ("position", bluePlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [1]) == selectDiceNumAnimation) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [1] + i].transform.position;
					}

					BluePlayer_Steps [1] += selectDiceNumAnimation;

					playerTurn = "BLUE";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (BluePlayerI, BluePiceSafeHouse[1] );
					totalBlueInHouse += 1;
					print ("Cool");
					BluePlayerII_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (blueMovemenBlock.Count - BluePlayer_Steps [1]).ToString () + "To enter in safe house");
					if (BluePlayer_Steps [0] + BluePlayer_Steps [2] + BluePlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "GREEN";
						InitializeDice();
					}
				}
			}
		}

		//This is BluePlayer 3rd Pice method by Executing this 3rd pice of BluePlayer can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of blue player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public  void BluePlayerIII_UI()
		{
			print ("BluePlayerIII_UI()");
			if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [2]) > selectDiceNumAnimation) 
			{

//				if (soundValue.Equals ("on")) {
//					this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//					this.gameObject.GetComponent<AudioSource> ().Play ();
//				}

				if (BluePlayer_Steps [2] > 0) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [2] + i].transform.position;
					}

					BluePlayer_Steps [2] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "BLUE";
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "BLUE PLAYER III";
					MovingBlueOrGreenPlayer (BluePlayerIII, bluePlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [2] == 0)
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];
						bluePlayer_Path [0] = blueMovemenBlock [BluePlayer_Steps [2]].transform.position;
						BluePlayer_Steps [2] += 1;
						playerTurn = "BLUE";
						currentPlayerName = "BLUE PLAYER III";
						iTween.MoveTo (BluePlayerIII, iTween.Hash ("position", bluePlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;

					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [2]) == selectDiceNumAnimation) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [2] + i].transform.position;
					}

					BluePlayer_Steps [2] += selectDiceNumAnimation;

					playerTurn = "BLUE";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (BluePlayerI, BluePiceSafeHouse[2] );
					totalBlueInHouse += 1;
					print ("Cool");
					BluePlayerIII_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (blueMovemenBlock.Count - BluePlayer_Steps [2]).ToString () + "To enter in safe house");
					if (BluePlayer_Steps [0] + BluePlayer_Steps [1] + BluePlayer_Steps [3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "GREEN";
						InitializeDice();
					}
				}
			}
		}

		//This is BluePlayer 4th Pice method by Executing this 4th pice of BluePlayer can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of blue player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void BluePlayerIV_UI()
		{
//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}

			print ("BluePlayerIV_UI()");
			if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [3]) > selectDiceNumAnimation) 
			{
				if (BluePlayer_Steps [3] > 0) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [3] + i].transform.position;
					}

					BluePlayer_Steps [3] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "BLUE";
						PlayerCanPlayAgain = true;
					} 
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "BLUE PLAYER IV";

					MovingBlueOrGreenPlayer (BluePlayerIV, bluePlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && BluePlayer_Steps [3] == 0)
					{
//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];
						bluePlayer_Path [0] = blueMovemenBlock [BluePlayer_Steps [3]].transform.position;
						BluePlayer_Steps [3] += 1;
						playerTurn = "BLUE";
						currentPlayerName = "BLUE PLAYER IV";
						iTween.MoveTo (BluePlayerIV, iTween.Hash ("position", bluePlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house
				if (playerTurn == "BLUE" && (blueMovemenBlock.Count - BluePlayer_Steps [3]) == selectDiceNumAnimation) 
				{
					Vector3[] bluePlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						bluePlayer_Path [i] = blueMovemenBlock [BluePlayer_Steps [3] + i].transform.position;
					}

					BluePlayer_Steps [3] += selectDiceNumAnimation;

					playerTurn = "BLUE";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (BluePlayerI, BluePiceSafeHouse[3] );
					totalBlueInHouse += 1;
					print ("Cool");
					BluePlayerIV_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {

					}
					else 
					{
						PlayerCanPlayAgain = true;
					}
				}
				else 
				{
					print ("You need" + (blueMovemenBlock.Count - BluePlayer_Steps [3]).ToString () + "To enter in safe house");
					if (BluePlayer_Steps [0] + BluePlayer_Steps [1] + BluePlayer_Steps [2] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "GREEN";
						InitializeDice();
					}
				}
			}
		}



		//==============================Red Player Movement=========================//
		public void RedPlayerI_UI()
		{

			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}

			print ("Executing RedPlayerI_UI()");
			if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[0]) > selectDiceNumAnimation) 
			{
				if (RedPlayer_Steps[0] > 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[0] + i].transform.position;
					}

					RedPlayer_Steps[0] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "RED";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "RED PLAYER I";

					MovingBlueOrGreenPlayer (RedPlayerI, redPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps[0] == 0) 
					{

						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}

						print ("Activating 1st RED player 1st time");
						Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
						redPlayer_Path [0] = redMovementBlock [RedPlayer_Steps[0]].transform.position;
						RedPlayer_Steps[0] += 1;
						playerTurn = "RED";
						currentPlayerName = "RED PLAYER I";
						iTween.MoveTo (RedPlayerI, iTween.Hash ("position", redPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[0]) == selectDiceNumAnimation) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[0] + i].transform.position;
					}

					RedPlayer_Steps[0] += selectDiceNumAnimation;

					playerTurn = "RED";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (RedPlayerI, RedPiceSafeHouse[0] );
					totalRedInHouse += 1;
					print ("Cool");
					RedPlayerI_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (redMovementBlock.Count - RedPlayer_Steps[0]).ToString () + "To enter in safe house");
					if (RedPlayer_Steps[1] + RedPlayer_Steps[2] + RedPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}

		public void RedPlayerII_UI()
		{

			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}

			print ("Executing RedPlayerII_UI()");
			if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[1]) > selectDiceNumAnimation) 
			{
				if (RedPlayer_Steps[1] > 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[1] + i].transform.position;
					}

					RedPlayer_Steps[1] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "RED";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "RED PLAYER II";

					MovingBlueOrGreenPlayer (RedPlayerII, redPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps[1] == 0) 
					{

						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}

						print ("Activating 1st RED player 1st time");
						Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
						redPlayer_Path [1] = redMovementBlock [RedPlayer_Steps[1]].transform.position;
						RedPlayer_Steps[1] += 1;
						playerTurn = "RED";
						currentPlayerName = "RED PLAYER II";
						iTween.MoveTo (RedPlayerII, iTween.Hash ("position", redPlayer_Path [1], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[1]) == selectDiceNumAnimation) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[1] + i].transform.position;
					}

					RedPlayer_Steps[1] += selectDiceNumAnimation;

					playerTurn = "RED";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (RedPlayerII, RedPiceSafeHouse[1] );
					totalRedInHouse += 1;
					print ("Cool");
					RedPlayerII_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (redMovementBlock.Count - RedPlayer_Steps[1]).ToString () + "To enter in safe house");
					if (RedPlayer_Steps[0] + RedPlayer_Steps[2] + RedPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}



		public void RedPlayerIII_UI()
		{

			//			if (soundValue.Equals ("on")) {
			//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
			//				this.gameObject.GetComponent<AudioSource> ().Play ();
			//			}

			print ("Executing RedPlayerIII_UI()");
			if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[2]) > selectDiceNumAnimation) 
			{
				if (RedPlayer_Steps[2] > 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[2] + i].transform.position;
					}

					RedPlayer_Steps[2] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "RED";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "GREEN";
					}

					currentPlayerName = "RED PLAYER II";

					MovingBlueOrGreenPlayer (RedPlayerII, redPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && RedPlayer_Steps[2] == 0) 
					{

						//						if (soundValue.Equals ("on")) {
						//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
						//							this.gameObject.GetComponent<AudioSource> ().Play ();
						//						}

						print ("Activating 1st RED player 1st time");
						Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
						redPlayer_Path [2] = redMovementBlock [RedPlayer_Steps[2]].transform.position;
						RedPlayer_Steps[2] += 1;
						playerTurn = "RED";
						currentPlayerName = "RED PLAYER I";
						iTween.MoveTo (RedPlayerII, iTween.Hash ("position", redPlayer_Path [2], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "RED" && (redMovementBlock.Count - RedPlayer_Steps[2]) == selectDiceNumAnimation) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						redPlayer_Path [i] = redMovementBlock [RedPlayer_Steps[2] + i].transform.position;
					}

					RedPlayer_Steps[2] += selectDiceNumAnimation;

					playerTurn = "RED";

					//					if (soundValue.Equals ("on")) {
					//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
					//						this.gameObject.GetComponent<AudioSource> ().Play ();
					//					}

					MovingBlueOrGreenPlayerInSafeHouse (RedPlayerII, RedPiceSafeHouse[1] );
					totalRedInHouse += 1;
					print ("Cool");
					RedPlayerII_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (redMovementBlock.Count - RedPlayer_Steps[1]).ToString () + "To enter in safe house");
					if (RedPlayer_Steps[0] + RedPlayer_Steps[2] + RedPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}




		//==================Green player Movement==================//

		//This is Green player 1st Pice method by Executing this 1st pice of Green player can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of Green player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void GreenPlayerI_UI()
		{

//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}

			print ("Executing GreenPlayerI_UI()");
			if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[0]) > selectDiceNumAnimation) 
			{
				if (GreenPlayer_Steps[0] > 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[0] + i].transform.position;
					}

					GreenPlayer_Steps[0] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "GREEN";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "BLUE";
					}

					currentPlayerName = "GREEN PLAYER I";

					MovingBlueOrGreenPlayer (GreenPlayerI, greenPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && GreenPlayer_Steps[0] == 0) 
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						print ("Activating 1st green player 1st time");
						Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
						greenPlayer_Path [0] = greenMovementBlock [GreenPlayer_Steps[0]].transform.position;
						GreenPlayer_Steps[0] += 1;
						playerTurn = "GREEN";
						currentPlayerName = "GREEN PLAYER I";
						iTween.MoveTo (GreenPlayerI, iTween.Hash ("position", greenPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[0]) == selectDiceNumAnimation) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[0] + i].transform.position;
					}

					GreenPlayer_Steps[0] += selectDiceNumAnimation;

					playerTurn = "GREEN";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (GreenPlayerI, GreenPiceSafeHouse[0] );
					totalGreenInHouse += 1;
					print ("Cool");
					GreenPlayerI_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (greenMovementBlock.Count - GreenPlayer_Steps[0]).ToString () + "To enter in safe house");
					if (GreenPlayer_Steps[1] + GreenPlayer_Steps[2] + GreenPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}

		//This is Green player 2nd Pice method by Executing this 2nd pice of Green player can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of Green player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public void GreenPlayerII_UI()
		{
			print ("Executing GreenPlayerII_UI()");
			if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[1]) > selectDiceNumAnimation) 
			{
//				if (soundValue.Equals ("on")) {
//					this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//					this.gameObject.GetComponent<AudioSource> ().Play ();
//				}

				if (GreenPlayer_Steps[1] > 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[1] + i].transform.position;
					}

					GreenPlayer_Steps[1] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "GREEN";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "BLUE";
					}

					currentPlayerName = "GREEN PLAYER II";

					MovingBlueOrGreenPlayer (GreenPlayerII, greenPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && GreenPlayer_Steps[1] == 0) 
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						print ("Activating 1st green player 1st time");
						Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
						greenPlayer_Path [0] = greenMovementBlock [GreenPlayer_Steps[1]].transform.position;
						GreenPlayer_Steps[1] += 1;
						playerTurn = "GREEN";
						currentPlayerName = "GREEN PLAYER II";
						iTween.MoveTo (GreenPlayerII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[1]) == selectDiceNumAnimation) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[1] + i].transform.position;
					}

					GreenPlayer_Steps[1] += selectDiceNumAnimation;

					playerTurn = "GREEN";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [2];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (GreenPlayerI, GreenPiceSafeHouse[1] );
					totalGreenInHouse += 1;
					print ("Cool");
					GreenPlayerII_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (greenMovementBlock.Count - GreenPlayer_Steps[1]).ToString () + "To enter in safe house");
					if (GreenPlayer_Steps[0] + GreenPlayer_Steps[2] + GreenPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}

		//This is Green player 3rd Pice method by Executing this 3rd pice of Green player can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of Green player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side
		public  void GreenPlayerIII_UI()
		{
//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}

			print ("Executing GreenPlayerIII_UI()");
			if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[2]) > selectDiceNumAnimation) 
			{
				if (GreenPlayer_Steps[2] > 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[2] + i].transform.position;
					}

					GreenPlayer_Steps[2] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "GREEN";	
						print ("Keeping the Turn to blue Players side");
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "BLUE";
					}

					currentPlayerName = "GREEN PLAYER III";

					MovingBlueOrGreenPlayer (GreenPlayerIII, greenPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && GreenPlayer_Steps[2] == 0) 
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						print ("Activating 3rd green player 1st time");
						Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
						greenPlayer_Path [0] = greenMovementBlock [GreenPlayer_Steps[2]].transform.position;
						GreenPlayer_Steps[2] += 1;
						playerTurn = "GREEN";
						currentPlayerName = "GREEN PLAYER III";
						iTween.MoveTo (GreenPlayerIII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[2]) == selectDiceNumAnimation) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[2] + i].transform.position;
					}

					GreenPlayer_Steps[2] += selectDiceNumAnimation;

					playerTurn = "GREEN";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (GreenPlayerI, GreenPiceSafeHouse[2] );
					totalGreenInHouse += 1;
					print ("Cool");
					GreenPlayerIV_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (greenMovementBlock.Count - GreenPlayer_Steps[2]).ToString () + "To enter in safe house");
					if (GreenPlayer_Steps[0] + GreenPlayer_Steps[1] + GreenPlayer_Steps[2] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}

		//This is Green player 4th Pice method by Executing this 4th pice of Green player can move 
		//if  this pice is already came out from home and got 6 by tossing the turn then the turn remains at this player side
		//if three pice of Green player is already came out from home and this 1 pice is in house and got 1 or 6 by tossing the dice then also the turn remains at this player side

		public void GreenPlayerIV_UI()
		{

//			if (soundValue.Equals ("on")) {
//				this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//				this.gameObject.GetComponent<AudioSource> ().Play ();
//			}

			print ("Executing GreenPlayerIV_UI()");
			if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[3]) > selectDiceNumAnimation) 
			{
				if (GreenPlayer_Steps[3] > 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++) 
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[3] + i].transform.position;
					}

					GreenPlayer_Steps[3] += selectDiceNumAnimation;

					//============Keeping the Turn to blue Players side============//
					if (selectDiceNumAnimation == 6) 
					{
						playerTurn = "GREEN";	
						PlayerCanPlayAgain = true;
					}
					else 
					{
						playerTurn = "BLUE";
					}

					currentPlayerName = "GREEN PLAYER IV";

					MovingBlueOrGreenPlayer (GreenPlayerIV, greenPlayer_Path);
				} 
				else 
				{
					if ((selectDiceNumAnimation == 6 || selectDiceNumAnimation==1) && GreenPlayer_Steps[3] == 0) 
					{

//						if (soundValue.Equals ("on")) {
//							this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//							this.gameObject.GetComponent<AudioSource> ().Play ();
//						}

						print ("Activating 1st green player 1st time");
						Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
						greenPlayer_Path [0] = greenMovementBlock [GreenPlayer_Steps[3]].transform.position;
						GreenPlayer_Steps[3] += 1;
						playerTurn = "GREEN";
						currentPlayerName = "GREEN PLAYER IV";
						iTween.MoveTo (GreenPlayerIV, iTween.Hash ("position", greenPlayer_Path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
						PlayerCanPlayAgain = true;
					}
				}
			}
			else 
			{
				//condition when player coin is reached successfully in house

				if (playerTurn == "GREEN" && (greenMovementBlock.Count - GreenPlayer_Steps[3]) == selectDiceNumAnimation) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

					for (int i = 0; i < selectDiceNumAnimation; i++)
					{
						greenPlayer_Path [i] = greenMovementBlock [GreenPlayer_Steps[3] + i].transform.position;
					}

					GreenPlayer_Steps[3] += selectDiceNumAnimation;

					playerTurn = "GREEN";

//					if (soundValue.Equals ("on")) {
//						this.gameObject.GetComponent<AudioSource> ().clip = AudioClips [0];
//						this.gameObject.GetComponent<AudioSource> ().Play ();
//					}

					MovingBlueOrGreenPlayerInSafeHouse (GreenPlayerI, GreenPiceSafeHouse[3] );
					totalGreenInHouse += 1;
					print ("Cool");
					GreenPlayerIV_Button.enabled = false;
					if (PhotonNetwork.IsMasterClient) {
						PlayerCanPlayAgain = true;
					}
					else 
					{

					}
				}
				else 
				{
					print ("You need" + (greenMovementBlock.Count - GreenPlayer_Steps[3]).ToString () + "To enter in safe house");
					if (GreenPlayer_Steps[0] + GreenPlayer_Steps[1] + GreenPlayer_Steps[3] == 0 && selectDiceNumAnimation != 6) 
					{
						playerTurn = "BLUE";
						InitializeDice();
					}
				}
			}
		}


		public void YellowPlayerI_UI_Method()
		{
			if (isMyTurn && playerTurn.Equals ("YELLOW")) {
				DisablingButtonsOfYellowPlayer ();
				DisablingBorderOfYellowPlayer ();

				childNode.Add ("MethodName", "YellowPlayerI_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}

		public void YellowPlayerII_UI_Method()
		{
			if (isMyTurn && playerTurn.Equals ("YELLOW")) {
				DisablingButtonsOfYellowPlayer ();
				DisablingBorderOfYellowPlayer ();

				childNode.Add ("MethodName", "YellowPlayerII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}


		public void YellowPlayerIII_UI_Method()
		{
			if (isMyTurn && playerTurn.Equals ("YELLOW")) {
				DisablingButtonsOfYellowPlayer ();
				DisablingBorderOfYellowPlayer ();

				childNode.Add ("MethodName", "YellowPlayerIII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}

		public void YellowPlayerIV_UI_Method()
		{
			if (isMyTurn && playerTurn.Equals ("YELLOW")) {
				DisablingButtonsOfYellowPlayer ();
				DisablingBorderOfYellowPlayer ();

				childNode.Add ("MethodName", "YellowPlayerIV_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}

		public void BluePlayerI_UI_Method()
		{
			//this method calls the BluePlayerI_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "BluePlayerI_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void BluePlayerII_UI_Method()
		{
			//this method calls the BluePlayerII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "BluePlayerII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void BluePlayerIII_UI_Method()
		{
			//this method calls the BluePlayerIII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "BluePlayerIII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void BluePlayerIV_UI_Method()
		{
			//this method calls the BluePlayerIV_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "BluePlayerIV_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}


		public void RedPlayerI_UI_Method()
		{
			//this method calls the RedPlayerI_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "RedPlayerI_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}


		public void RedPlayerII_UI_Method()
		{
			//this method calls the RedPlayerII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "RedPlayerII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}


		public void RedPlayerIII_UI_Method()
		{
			//this method calls the RedPlayerIII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "RedPlayerIII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}


		public void RedPlayerIV_UI_Method()
		{
			//this method calls the RedPlayerIV_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("BLUE")) {
				DisablingButtonsOFBluePlayes ();
				DisablingBordersOFBluePlayer ();

				childNode.Add ("MethodName", "RedPlayerIV_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}

		public void GreenPlayerI_UI_Method()
		{
			//this method calls the GreenPlayerI_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("GREEN")) {
				DisablingBordersOFGreenPlayer ();
				DisablingButtonsOfGreenPlayers ();

				childNode.Add ("MethodName", "GreenPlayerI_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void GreenPlayerII_UI_Method()
		{
			//this method calls the GreenPlayerII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("GREEN")) {
				DisablingBordersOFGreenPlayer ();
				DisablingButtonsOfGreenPlayers ();

				childNode.Add ("MethodName", "GreenPlayerII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void GreenPlayerIII_UI_Method()
		{
			//this method calls the GreenPlayerIII_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("GREEN")) {
				DisablingBordersOFGreenPlayer ();
				DisablingButtonsOfGreenPlayers ();

				childNode.Add ("MethodName", "GreenPlayerIII_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}
		public void GreenPlayerIV_UI_Method()
		{
			//this method calls the GreenPlayerIV_UI() by sending the RandomNumber and MethodName;
			if (isMyTurn && playerTurn.Equals ("GREEN")) {
				DisablingBordersOFGreenPlayer ();
				DisablingButtonsOfGreenPlayers ();

				childNode.Add ("MethodName", "GreenPlayerIV_UI");
				childNode.Add ("RandomNumber", selectDiceNumAnimation.ToString ());
				childNode.Add ("Wait", "2.5");

				temp = childNode.ToString ();
				this.MakeTurn (temp);
			}
		}

		void MovingBlueOrGreenPlayer(GameObject Player,Vector3[] path)
		{
			if (path.Length > 1) 
			{
				iTween.MoveTo (Player, iTween.Hash ("path", path, "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
			} 
			else
			{
				iTween.MoveTo (Player, iTween.Hash ("position", path [0], "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
			}
		}
		void MovingBlueOrGreenPlayerInSafeHouse(GameObject Player,Vector3 path)
		{
			iTween.MoveTo (Player, iTween.Hash ("position", path, "speed", 400, "time", 0.5f, "easetype", "elastic", "looptype", "none", "oncomplete", "InitializeDice", "oncompletetarget", this.gameObject));
		}



		#region room Quiting methods

		public void EnableQuitTheRoom()
		{
			QuitPanel.GetComponent<Animator> ().SetInteger ("Counter", 1);
		}

		public void CancelQuiting()
		{
			QuitPanel.GetComponent<Animator> ().SetInteger ("Counter", 2);
		}

		public void QuitTheRoom()
		{
			PhotonNetwork.LeaveRoom ();
			PhotonNetwork.LeaveLobby ();
			PhotonNetwork.Disconnect ();

			StartCoroutine (QuitTheRoom1 ());
		}

		IEnumerator QuitTheRoom1()
		{
			yield return new WaitForSeconds (.5f);
			SceneManager.LoadScene ("GameMenu");

			Destroy (GameObject.Find("SceneSwitchController"));

			Destroy (this.gameObject);
		}

		#endregion


		#region Start methods which Executes only once
		void Start () 
		{
			PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 5000 ;
			GameObject OneOnOneConnectionManagerController = GameObject.Find ("SceneSWitchController");
			this.turnManager = this.gameObject.AddComponent<PunTurnManager> ();
			this.turnManager.TurnManagerListener = this;
			string name = null;
			if(PhotonNetwork.InRoom)
				name=PhotonNetwork.CurrentRoom.Name;
			print (name);

			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = 30;
			randomNo = new System.Random ();

			TimerOnePosition = ImageOne.transform.position;
			TimerTwoPosition = ImageTwo.transform.position;
			TimerThreePosition = ImageThree.transform.position;
			TimerFourPosition = ImageFour.transform.position;

			//Player initial positions...........
			BluePlayers_Pos[0]=BluePlayerI.transform.position;
			BluePlayers_Pos[1] = BluePlayerII.transform.position;
			BluePlayers_Pos[2] = BluePlayerIII.transform.position;
			BluePlayers_Pos[3] = BluePlayerIV.transform.position;

			GreenPlayers_Pos[0] = GreenPlayerI.transform.position;
			GreenPlayers_Pos[1] = GreenPlayerII.transform.position;
			GreenPlayers_Pos[2] = GreenPlayerIII.transform.position;
			GreenPlayers_Pos[3] = GreenPlayerIV.transform.position;

			RedPlayers_Pos [0] = RedPlayerI.transform.position;
			RedPlayers_Pos [1] = RedPlayerII.transform.position;
			RedPlayers_Pos [2] = RedPlayerIII.transform.position;
			RedPlayers_Pos [3] = RedPlayerIV.transform.position;

			YellowPlayers_Pos [0] = YellowPlayerI.transform.position;
			YellowPlayers_Pos [1] = YellowPlayerII.transform.position;
			YellowPlayers_Pos [2] = YellowPlayerIII.transform.position;
			YellowPlayers_Pos [3] = YellowPlayerIV.transform.position;



			DisablingBordersOFBluePlayer ();

			DisablingBordersOFGreenPlayer ();

			DisablingBorderOfYellowPlayer ();

			DisablingBorderOrRedPlayer ();

			playerTurn = "YELLOW";
			BlueFrame.SetActive (false);
			GreenFrame.SetActive (false);

			TimerImage.transform.position = TimerOnePosition;
			diceRoll.position = YellowDicePosition.position;

			switch(PhotonNetwork.CurrentRoom.PlayerCount)
			{
			case 1:
				print ("First player Entered Room");
//				DisablingBluePlayer ();
//				DisablingRedPlayer ();
//				DisablingGreenPlayer ();
				DisconnectPanel.SetActive (true);
				ReconnectButton.SetActive(false);
				DisconnectText.text="WAIT TILL THE OTHER PLAYER TO CONNECT";
				break;
			case 2:
//				EnablingBluePlayer ();
//				DisablingRedPlayer ();
//				DisablingGreenPlayer ();
				DisconnectPanel.SetActive (true);
				ReconnectButton.SetActive(false);
				DisconnectText.text="WAIT TILL THE OTHER TO PLAYER CONNECT";
				break;
			case 3:
//				EnablingBluePlayer ();
//				EnablingRedPlayer ();
//				DisablingGreenPlayer ();
				DisconnectPanel.SetActive (true);
				ReconnectButton.SetActive(false);
				DisconnectText.text="WAIT TILL THE OTHER TO PLAYER CONNECT";
				break;
			case 4:
				DisconnectPanel.SetActive (false);
				ReconnectButton.SetActive(false);
				DisconnectText.text = null;
				YellowFrame.SetActive (true);
//				EnablingBluePlayer ();
//				EnablingRedPlayer ();
//				EnablingGreenPlayer ();
				break;
			}
		}

		#endregion 




		void Update()
		{
			print(PhotonNetwork.CurrentRoom.PlayerCount);
		}





















		#region Photon Callback Methods of sending and receiving data

		public void OnTurnBegins(int turn)
		{}
		public void OnTurnCompleted(int turn)
		{}
		public void OnPlayerMove(Player player, int turn, object move)
		{}
		public void OnPlayerFinished(Player player, int turn, object move)
		{}
		public void OnTurnTimeEnds(int turn)
		{}

		public void MakeTurn(string data)
		{
			string temp1 = null;

			temp = data;

			this.turnManager.SendMove (data as object, true);
		}

		#endregion

		public override void OnPlayerEnteredRoom (Player newPlayer)
		{
			switch(PhotonNetwork.CurrentRoom.PlayerCount)
			{
			case 1:
				DisablingBluePlayer ();
				DisablingRedPlayer ();
				DisablingGreenPlayer ();
				break;
			case 2:
				print ("Second player Entered Room");
				EnablingBluePlayer ();
				DisablingRedPlayer ();
				DisablingGreenPlayer ();
				break;
			case 3:
				print ("Third player Entered Room");
				EnablingBluePlayer ();
				EnablingRedPlayer ();
				DisablingGreenPlayer ();
				break;
			case 4:
				print ("Fourth player Entered Room");
//				YellowFrame.SetActive (true);
//				EnablingBluePlayer ();
//				EnablingRedPlayer ();
//				EnablingGreenPlayer ();
				DisconnectPanel.SetActive (false);
				ReconnectButton.SetActive(false);
				DisconnectText.text = null;
				YellowFrame.SetActive (true);
				if (this.turnManager.Turn == 0) {
					isMyTurn = true;
				}
				break;
			}
		}
	}
}
