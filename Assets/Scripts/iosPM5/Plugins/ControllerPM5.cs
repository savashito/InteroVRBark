using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPM5 : MonoBehaviour{

	public Text textErgData;
	public Text textStrokeData;
	ErgData ergData;
	StrokeData strokeData;
	public InteroServerConnection interoServerConnection;
//	Location currentLocation;
	PlayerController player = null;

	void Start(){
		StartBLE (0);
	}
	public void StartBLE (int channel){
//		SendMe
//		currentLocation = new Location(0,0);
//		player = null;
		print("Hello");
		textErgData.text = ("Hello world");
		textStrokeData.text = "Mewo ";
		ergData = new ErgData ();
		strokeData = new StrokeData ();
		print("Hello1");
		PM5EventHandler.connectToPM5 (channel);
		print("Hello3");
	}
	public void SetPlayer(PlayerController p){
		player = p;
	}
	public void Update(){
		//		print("Mow");
		//		mRigidBody = GetComponent<Rigidbody> ();
		// mRigidBody.velocity = new Vector3 (1.0f, 0.0f, 0.0f);
		/*
		if (player) {
			currentLocation.mDistance += 0.1f; 
			currentLocation.time = Time.fixedTime;
			player.setLocation (currentLocation);
			print (currentLocation);
		}
		*/

	}
	/*
	void onBLEInitilized(string s){

	}*/
	void onBLEOn(string s){
		textErgData.text =  ("BLE On");


	}
	public void onPM5Connected(string s){
		textErgData.text =  ("Successfully connected");
	}
//	public ErgDisplayController displayController;
//	displayController.SetFields (ergData);
	public void onErgDataReady(string s){
//		print ("onErgDataReady");
		textErgData.text = "onErgDataReady ";

		ergData.distance = PM5EventHandler.getDistance();
		ergData.power = PM5EventHandler.getPower();
		ergData.pace = PM5EventHandler.getPace();
		ergData.spm = PM5EventHandler.getSPM();
		ergData.time = PM5EventHandler.getTime();
		ergData.calhr = PM5EventHandler.getCalhr();
		ergData.calories = PM5EventHandler.getCalories();
		// inform the server of what we just read

		// ErgData* ergData = PM5EventHandler.readErgData ();
		//text.text = "onErgDataReady "+ ergData->ToString ();
		textErgData.text =  ergData.ToString ();
		if (player) {
			
			if(true){
//			if(player.iniciar){
//			print("OnergDataReady");
				player.OnErgData (ergData);
//				displayController.SetFields (ergData);
	//			currentLocation.mDistance = ergData.distance;
	//			currentLocation.time = ergData.time;
	//			player.setLocation (currentLocation);
	//			print (currentLocation);
			}
		}
		interoServerConnection.SendErgData (ergData);
	//	print ("onErgDataReady" + ergData.ToString ());

	}
	public void onStrokeDataReady(string s){
		textStrokeData.text = "onStrokeDataReady ";
		strokeData.time = PM5EventHandler.getTime();
		strokeData.distance = PM5EventHandler.getDistance();
		strokeData.driveLength = PM5EventHandler.getDriveLength();
		strokeData.driveTime = PM5EventHandler.getDriveTime();
		strokeData.strokeRecoveryTime = PM5EventHandler.getStrokeRecoveryTime();
		textStrokeData.text =  strokeData.ToString ();
//		print ("onStrokeDataReady");
		if (player) {
			player.OnStrokeData (strokeData);
		}
		/*
		strokeData.time = PM5EventHandler.getTime();
		strokeData.distance = PM5EventHandler.getDistance();
		strokeData.driveLength = PM5EventHandler.getDriveLength();
		strokeData.driveTime = PM5EventHandler.getDriveTime();
		strokeData.strokeRecoveryTime = PM5EventHandler.getStrokeRecoveryTime();
		//StrokeData* strokeData = PM5EventHandler.readStrokeData ();

		print ("onStrokeDataReady" + strokeData.ToString ());
		*/
	}
}
