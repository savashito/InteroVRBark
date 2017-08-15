using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteroServerBex : MonoBehaviour {

	public InteroServerConnection interoServer;
	bool firstTime;
	bool startSendingData;
	void Start (){
		firstTime = true;
		startSendingData = false;
	}
	// Use this for initialization
	void Update () {
		//		GetTestRower ();
		//		GetTeamWorkouts ();
		if (firstTime && interoServer.hasConnected) {
			firstTime = false;
			startSendingData = true;
			TestLogin ();
//			SendDumyErgData ();
//			SendDumyErgData ();
			InvokeRepeating("SendDumyErgData", 0, 1.0f);
		}
	}
	void TestLogin(){
		interoServer.LoginUser ("90156192670307119", "Becca");
	}

	ErgData ergData = new ErgData(5,10,22,200,120);
	void SendDumyErgData(){

		//		for (int i = 0; i < 100; i++) {
		ergData.distance += 5.0f;
		ergData.time += 0.5f;
		//		logWorkoutEntry
		// print ("Sent" +ergData);
		interoServer.SendErgData(ergData);
//		socket.Emit("RowerEvent",ergData.ToJSONEvent(configHUD.getRower()));
		//		}
	}


	// after success loging in, send request to server

}
