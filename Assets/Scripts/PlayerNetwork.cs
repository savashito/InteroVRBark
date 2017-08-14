using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.VR;
using UnityStandardAssets.Water;
public class PlayerNetwork: MonoBehaviour {

//	public GameObject Gvr;
//	public bool localPlayer;
	public GameObject myGhost;
	public GameObject myPlayer;
	public CameraController camController;
	private ConfigurationHUD confHUD;


	// Use this for initialization
//	void Start () {
//
//	}
//	public void initBLE(){
////		enderSettings.skybox = otherSkybox;
//	}
//
//	public void InitVR(){
	public void Start () {
//		disableVR ();
		print("Mack");
		confHUD = GameObject.Find ("ConfigHandler").gameObject.GetComponent<ConfigurationHUD> ();
		InitVR (confHUD.isRowingSolo);
	}
	public void InitVR(bool fpv){
		Camera[] camaras;
		AudioListener[] audios;
		print("Player NetworkStart");
		/*
		if(confHUD.isOfflineGame)
			localPlayer = true;
		else
			localPlayer = isLocalPlayer;*/
//		GameObject.Find("Net Manager").GetComponent<>().set
//		localPlayer = isLocalPlayer;
		/*
		print ("localPlayer " + localPlayer);
		if (!localPlayer) {

			camaras = GetComponentsInChildren<Camera> ();
			foreach (Camera camara in camaras) {
				camara.enabled = false;
			}

			audios = GetComponentsInChildren<AudioListener> ();
			foreach (AudioListener audio in audios) {
				audio.enabled = false;
			}

		} else {*/
			GameObject.Destroy (GameObject.Find ("Camera"));
			print (myPlayer);
			myPlayer.transform.rotation = Quaternion.Euler (0f, -90.0f, 0f);

			// .InitVR();;


			int lineaDisponible ;
			lineaDisponible = confHUD.getLane();
		/*	
		if(confHUD.isOfflineGame)
				lineaDisponible = confHUD.getLane();//GameObject.Find ("Lane").GetComponent<laneManager> ().lineaDisponible;
			else 
				lineaDisponible = GameObject.Find ("Lane").GetComponent<laneManager> ().lineaDisponible;
			myPlayer.transform.position = new Vector3 (-1.0f, 0.0f, lineaDisponible); 

			/*
			if (GameObject.Find ("Net Manager").GetComponent<NetManager> ().GhostFile == "") {
				myGhost.transform.position = Vector3.down * 100;
				myGhost.SetActive (false);
			} else {
				myGhost.transform.position = new Vector3 (-1.0f, 0.0f, lineaDisponible);
				lineaDisponible+=7;
			}*/
			myGhost.transform.position = Vector3.down * 100;
			myGhost.SetActive (false);

			// set the gameplayer on scripts
			GameObject.Find("Boyes").GetComponent<BouyeCreator>().player=myPlayer.GetComponent<Rigidbody>();
			PlayerController plController = myPlayer.GetComponent<PlayerController> ();
			plController.enabled = true;
			// bark
//			plController.iniciar = true;
//			myGhost.GetComponent<GhostController> ().enabled = true;
//			myPlayer.GetComponent<Animator> ().SetBool ("iniciar", true);
			// end bark
			GameObject.Find ("BLEReceiver").GetComponent<ControllerPM5> ().SetPlayer(plController);
			GameObject ergDisplayFront = myPlayer.transform.Find ("ErgDisplay").gameObject ;
			GameObject ergDisplayBack = myPlayer.transform.Find ("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/ErgDisplay").gameObject; 
			if(confHUD.isRowingSolo){
				plController.displayController = ergDisplayFront.GetComponent<ErgDisplayController>();
				ergDisplayBack.SetActive(false);
			}
			else{
				plController.displayController = ergDisplayBack.GetComponent<ErgDisplayController>();
				ergDisplayFront.SetActive(false);
			}
			//			plController.cameraController = .Recenter();
			// ErgDisplayController ergDisplay = player.transform.Find ("Remero_bote_iRow NET/ErgDisplay").gameObject.GetComponent<ErgDisplayController>;
			myPlayer.transform.Find ("ErgDisplay").gameObject.SetActive(true);
			// ControllerPM5
			GameObject water = myPlayer.transform.Find ("WaterProDaytime").gameObject;
			Water  waterScript = water.GetComponent<Water> ();
			if(confHUD.isUltraOn())
				waterScript.waterMode = Water.WaterMode.Refractive;
			else
				waterScript.waterMode = Water.WaterMode.Simple;
//			waterScript.reflectLayers = Water.;
			water.SetActive (true);

			InitRowingScene (fpv, myPlayer);
//			myPlayer.transform.FindChild ("3D Canvas").gameObject.SetActive (true);
			// new stuffy 
			print ("Iniciando GVR "+confHUD.isVROn ());
			camController.SetVR (confHUD.isVROn ());
			/*
			if(confHUD.isVROn ()){
			camController.SeenableVR ();
//				GameObject.Instantiate (Gvr);

//				GvrViewer.Instance.VRModeEnabled = true;
//				GvrViewer.Instance.Recenter ();
//				print ("GVR iniciado");
			}else{
				disableVR();
			}
			*/
			// enable wifi
		/*
			if (confHUD.isWIFIOn ()) {
				GameObject g =GameObject.Find ("RowingVRGame/ConnectionLogic");//.SetActive (true);
				print("WIFI");
				print(g);
			}*/

//		}
	}

//	public void enableVR(){
//		VRSettings.enabled = true;
//		//		VRDevice.
//		//		VRDevice.
//		//		GvrControllerInput g;
//		//		g.re
//		//		UnityEditor.PlayerSettings.virtualRealitySupported = true;
//		print ("VR on");
//	}
//	public void disableVR(){
//		//		UnityEditor.PlayerSettings.virtualRealitySupported = false;
//		VRSettings.enabled = false;
//		Camera.main.ResetAspect ();
//		print ("VR off");
//	}

	public PlayerNetwork InitRowingScene(bool fpv,GameObject player){
		//		GameObject player = GameObject.Instantiate (playerPrefab);
		Transform t = player.transform.Find ("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/1PCamera");
		print (t);
		GameObject cam1 = t.gameObject;
		GameObject cam3 = player.transform.Find ("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/3PCamera").gameObject;
		GameObject camFree = player.transform.Find ("FloatCamera").gameObject;
		confHUD = GameObject.Find("ConfigHandler").GetComponent<ConfigurationHUD>();

		if (confHUD.isHideRower ()) {
			cam1.SetActive (false);
			cam3.SetActive (false);
			camFree.SetActive (true);
			player.transform.Find ("cuerpo_remero:cuerpo_Alan1").gameObject.SetActive(false);
			player.transform.Find ("Character1_Reference").gameObject.SetActive(false);
			player.transform.Find ("Character1_Ctrl_Reference").gameObject.SetActive(false);
			player.transform.Find ("remo1").gameObject.SetActive(false);
			player.transform.Find ("remo2").gameObject.SetActive(false);
		} else {
			cam1.SetActive (fpv); 
			cam3.SetActive (!fpv);
			camFree.SetActive (false);
		}
		PlayerNetwork pNetwork = player.GetComponent<PlayerNetwork>(); 
		RenderSettings.skybox = confHUD.getSkybox();
		confHUD.isRowingSolo = fpv;
		ControllerPM5 pm5BLE = GameObject.Find ("BLEReceiver").GetComponent<ControllerPM5>(); 
		// ErgDisplayController ergDisplay = player.transform.Find ("Remero_bote_iRow NET/ErgDisplay").gameObject.GetComponent<ErgDisplayController>;
		// ControllerPM5
		//		pm5BLE.player = player;
		pm5BLE.StartBLE (confHUD.getPMChannel());
		print ("InitROwer "+confHUD.getPMChannel());
		return pNetwork;
	}

	// Update is called once per frame
	void Update () {

		if (myGhost != null) {
			if (myGhost.transform.position == Vector3.down * 100) {
				myGhost.SetActive (false);
			}
		}

	}
}
