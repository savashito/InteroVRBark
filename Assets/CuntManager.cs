﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuntManager : MonoBehaviour {
	private ConfigurationHUD confHUD; 
	public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		confHUD = GameObject.Find("ConfigHandler").GetComponent<ConfigurationHUD>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void CreateLocalSolo(){
		print ("CreateLocalSolo");
		confHUD.isRowingSolo = true;
		confHUD.isOfflineGame = true;
		GameObject player = GameObject.Instantiate (playerPrefab);
		player.SetActive (true);
		// player.GetComponent<PlayerNetwork> ().Start();
	}


	public void CreateLocalDouble(){
		confHUD.isRowingSolo = false;
		confHUD.toggleHideRower.isOn = false;
		confHUD.isOfflineGame = true;
		GameObject player = GameObject.Instantiate (playerPrefab);
		player.SetActive (true);
	}

}
