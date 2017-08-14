using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRowerEvent : MonoBehaviour {
	public InteroServerConnection interoServer;
	bool firstTime;

	void Start (){
		firstTime = true;
	}
	// Use this for initialization
	void Update () {
		//		GetTestRower ();
		//		GetTeamWorkouts ();
		if (firstTime && interoServer.hasConnected) {
			firstTime = false;
			TestLogin ();
		}
	}
	void TestLogin(){
		interoServer.LoginUser ("10156192670307119", "Rodrigo");
	}

}

/*
	// Get Rowers
void GetTestRower (){
	JSONObject obj = new JSONObject();
	obj.AddField("data",configHUD.getRower ());
	obj.AddField("event","listRowers");
	//	getRower
	socket.Emit ("RowerStaticEvent",obj);
	print ("getTestRower"+obj);
}

/*
	ErgData ergData = new ErgData(5,10,22,200,120);
	void SendDumyErgData(){
		

//		for (int i = 0; i < 100; i++) {
		ergData.distance += 5.0f;
		ergData.time += 0.5f;
		//		logWorkoutEntry
		socket.Emit("RowerEvent",ergData.ToJSONEvent(configHUD.getRower()));
//		}

	}

/*
		if (pmChannel == 1) {
			InvokeRepeating("SendDumyErgData", 0, 1.0f);
		}*/