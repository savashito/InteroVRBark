using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using System.IO;

// 1474441069

public class NetManager : NetworkManager  {


	private List<MatchInfoSnapshot> Lista;
	private int indexGhost = 0;
	private int numFiles=0;
	public string GhostFile="";
	private string usuario = "Usuario";
	private string path;
	private DirectoryInfo dir;
	private FileInfo[] archivos;
	private ConfigurationHUD confHUD; 

	// Use this for initialization
	void Start () {
		StartMatchMaker ();
//		matchMaker = new NetworkMatch();
		path = Application.persistentDataPath + "/" + usuario;
		dir= new DirectoryInfo(path);
		archivos = dir.GetFiles("*.txt");
//		Create ();
//		CreateLocal();
		confHUD = GameObject.Find("ConfigHandler").GetComponent<ConfigurationHUD>();

	}

	public void LanHost(){
		print("LanHost");
		StartHost ();
	}

	public void LanClient(){
		print("LanClient");
		StartClient ();
	}

	public void LanServer(){
		print("LanServer");
		StartServer ();
	}
	/*
	public PlayerNetwork InitRowingScene(bool fpv){
		GameObject player = GameObject.Instantiate (playerPrefab);
		GameObject cam1 = player.transform.Find ("Remero_bote_iRow NET/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/1PCamera").gameObject;
		GameObject cam3 = player.transform.Find ("Remero_bote_iRow NET/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/3PCamera").gameObject;
		GameObject camFree = player.transform.Find ("Remero_bote_iRow NET/FloatCamera").gameObject;
		if (confHUD.isHideRower ()) {
			cam1.SetActive (false);
			cam3.SetActive (false);
			camFree.SetActive (true);
			player.transform.Find ("Remero_bote_iRow NET/cuerpo_remero:cuerpo_Alan1").gameObject.SetActive(false);
			player.transform.Find ("Remero_bote_iRow NET/Character1_Reference").gameObject.SetActive(false);
			player.transform.Find ("Remero_bote_iRow NET/Character1_Ctrl_Reference").gameObject.SetActive(false);
			player.transform.Find ("Remero_bote_iRow NET/remo1").gameObject.SetActive(false);
			player.transform.Find ("Remero_bote_iRow NET/remo2").gameObject.SetActive(false);
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
	}*/

	public void CreateLocalSolo(){
// .GetComponent<PlayerNetwork>().localPlayer = true;
//		pNetwork.InitVR();
		print ("CreateLocalSolo");
		confHUD.isRowingSolo = true;
		confHUD.isOfflineGame = true;
		GameObject player = GameObject.Instantiate (playerPrefab);
//		PlayerNetwork pNetwork = player.GetComponent<PlayerNetwork>();
//		pNetwork.InitVR (true);
//		InitRowingScene (true).InitVR();
//		confHUD.isRowingSolo = true;
	}
	/*
	public void CreateLocalDouble(){
		confHUD.isRowingSolo = false;
		confHUD.toggleHideRower.isOn = false;
		confHUD.isOfflineGame = true;
		GameObject player = GameObject.Instantiate (playerPrefab);

//		playerPrefab.gameObject.GetComponent<PlayerNetwork>().localPlayer = true;
//		GameObject.Instantiate (playerPrefab).gameObject.GetComponent<PlayerNetwork>().localPlayer = true;
//		InitRowingScene (false).InitVR();
//		GameObject player = GameObject.Instantiate (playerPrefab);
//		GameObject cam1 = player.transform.Find ("Remero_bote_iRow NET/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/1PCamera").gameObject;
//		GameObject cam3 = player.transform.Find ("Remero_bote_iRow NET/Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_Neck/CameraController/3PCamera").gameObject;
//		cam1.SetActive (false);
//		cam3.SetActive (true);
//		PlayerNetwork pNetwork = player.GetComponent<PlayerNetwork>(); // .GetComponent<PlayerNetwork>().localPlayer = true;
//		pNetwork.InitVR();
//		confHUD.isRowingSolo = false;
	}
*/
	public void Create(){
		string name="";
		confHUD.isRowingSolo = true;
		confHUD.isOfflineGame = false;
		name = GameObject.Find ("MatchName").GetComponent<InputField> ().text;
//		CreateLocalSolo ();
		if (name == "")
			name = "default";
		print (matchMaker);
		print (name);
//		print (OnMatchCreate); 
		matchMaker.CreateMatch(name, 4, true, "", "", "", 0, 0, OnMatchCreate);
		Debug.Log ("Creando "+name);
	}

	public void ListaMatches(){
		matchMaker.ListMatches(0, 3, "", true, 0, 0, OnMatchList);
		//Debug.Log ("Lista");
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		//Debug.Log ("Uniendo");
		int num=matches.Count;
		if (num > 0)
			GameObject.Find ("Match 1").GetComponentInChildren<Text> ().text = matches [0].name;
		else
			GameObject.Find ("Match 1").SetActive (false);
		if(num > 1)
			GameObject.Find ("Match 2").GetComponentInChildren<Text> ().text = matches [1].name;
		else
			GameObject.Find ("Match 2").SetActive (false);
		if(num > 2)
			GameObject.Find ("Match 3").GetComponentInChildren<Text> ().text = matches [2].name;
		else
			GameObject.Find ("Match 3").SetActive (false);

		Lista = matches;

	}

	public void Join(int index){
		confHUD.isOfflineGame = false;
		matchMaker.JoinMatch (Lista [index].networkId, "", "", "", 0, 0, OnMatchJoined);
	}

	public void ListaGhosts(){
		GameObject botonesGhosts = GameObject.Find ("BotonesGhosts");
		Text[] texto = botonesGhosts.GetComponentsInChildren<Text>();

		numFiles = archivos.Length;
		int i = numFiles-indexGhost-1;
		foreach (Text t in texto){
			if (i < 0) {
				t.text = "";
			}else{
				t.text = archivos[i].Name.Split('.')[0];
				i--;
			}
		}
	}

	public void Next(){
		if(indexGhost+6<numFiles)
			indexGhost += 6;
		ListaGhosts ();
	}

	public void Previous(){
		if(indexGhost>5)
			indexGhost -= 6;
		ListaGhosts ();
	}

	public void selectGhost(int posicion){
		int i = numFiles-indexGhost-1-posicion;
		GhostFile=archivos[i].Name;
	}
		
	// Update is called once per frame
	void Update () {
	}
}
