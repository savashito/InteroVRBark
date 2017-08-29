using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowSessionManager : MonoBehaviour {
	private ConfigurationHUD confHUD; 
	public GameObject playerPrefab;
	CanvasController canvasController;
	GameObject playerPool;
	public InteroServerConnection interoServerConnection;
	// Use this for initialization
	void Start () {
		confHUD = GameObject.Find("ConfigHandler").GetComponent<ConfigurationHUD>();
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		playerPool = null;
		// init vr
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void InitRowingSession(){
		CreateSoloSession ();
	}
	public void EndRowingSession(){
		// send that the session ended successfully or something, 
		// show how many points were obtain and success of session
	}
	public void CreateSoloSession(){
		print ("CreateLocalSolo");
		confHUD.isRowingSolo = true;
		confHUD.isOfflineGame = true;
		if (playerPool == null) {
			print ("Crater Player");
			playerPool = GameObject.Instantiate (playerPrefab);

		} else {
			playerPool.GetComponent<PlayerNetwork> ().UpdateScene ();
		}
		canvasController.Hide ();
		playerPool.SetActive (true);
		// player.GetComponent<PlayerNetwork> ().Start();
		// send the configuration to the server

		interoServerConnection.SendCofiguration ();
	}


	public void CreateDoubleSession(){
		confHUD.isRowingSolo = false;
		confHUD.toggleHideRower.isOn = false;
		confHUD.isOfflineGame = true;
		if (playerPool == null) {
			playerPool = GameObject.Instantiate (playerPrefab);
		} else {
			playerPool.GetComponent<PlayerNetwork> ().UpdateScene ();
		}
		canvasController.Hide ();
		playerPool.SetActive (true);
	}

}
