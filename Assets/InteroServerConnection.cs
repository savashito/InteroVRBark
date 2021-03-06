﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class InteroServerConnection : MonoBehaviour {
	private SocketIOComponent socket = null;
	public ConfigurationHUD configHUD = null;
	public bool hasConnected;//{ public get; private set;}
	public int pmChannel = 0;
	public TestBLE testBLE = null;
	public CanvasController canvasController = null;
	public RowSessionManager rowSessionManager;
	// Use this for initialization
	void Start () {
//		GameObject canvas = GameObject.Find ("Canvas");
//		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		socket = GetComponentInParent<SocketIOComponent> ();
		socket.On ("open", OnConnected);
		socket.On ("disconnect", OnDisconnect);
		socket.On ("RowerEvent", OnRowerEvent);
		socket.On ("RowerStaticEvent", OnRowerStaticEvent);
		socket.On ("WorkoutGroupStaticEvent", OnWorkoutGroupStaticEvent);
		socket.On ("loginFB", OnloginFB);
		socket.On ("configuration", OnConfiguration);

		socket.Connect ();
		print ("Trying to connect");
//		configHUD = GameObject.Find ("ConfigHandler").GetComponent<ConfigurationHUD> ();
		hasConnected = false;
		if (configHUD == null) {
			configHUD = new ConfigurationHUD ();
			configHUD.setPMChannel (pmChannel);
		}
//		print (canvas);
//		print (canvasController);
	}
	void OnConnected(SocketIOEvent e){
		if (hasConnected)
			return;
		hasConnected = true;
		print (pmChannel+"Connected");
	}
	void OnDisconnect(SocketIOEvent e){
		print (pmChannel+"Disconnect");
		hasConnected = false;
	}
	void OnConfiguration(SocketIOEvent e){
		print ("OnConfiguration"+ e.data);
		InteroServerPayload p = new InteroServerPayload (e.data);
		configHUD.LoadCofiguration (p.obj);
	}
	void OnRowerEvent(SocketIOEvent e){
		print ("OnRowerEvent"+ e.data);

//		string rowerId = "";
		int lane =0;
		InteroServerPayload p = new InteroServerPayload (e.data);
		switch (p.ev) {
		case "":
			
			break;
		case "logWorkoutEntryResponse":
			/*
			JSONObject o = e.data;
//			o.GetField (ref rowerId, "rowerId");
			o.GetField (ref lane, "lane");
//			JSONObject payload = o.GetField("data");
			ErgData erg = ErgData.From(p.obj);
			erg.i = lane;
//			print ("Placebo" + erg);
			testBLE.OnErgData (erg);
			*/
			break;
		case "recievedWorkoutEntry":
			JSONObject o = e.data;
			//			o.GetField (ref rowerId, "rowerId");
			o.GetField (ref lane, "lane");
			//			JSONObject payload = o.GetField("data");
			ErgData erg = ErgData.From(p.obj);
			 erg.i = lane;
			print ("Placebo" + erg);
			testBLE.OnErgData (erg);
			break;
		case "createWOGResponse":
//				:{"__v":1,"joinAnyTime":"true","terrain":"land","dayTime":"morning","name":"Rodrigo_1382017","team":"green","inProgress":true,"_id":"5990be26527a983809c1f1cd","rowers":[{"ready":false,"rowerId":"5990b97e527a983809c1f1cc","workoutId":"5990be26527a983809c1f1ce","_id":"5990be26527a983809c1f1cf"}
//			canvasController.Hide ();
			print ("createWOGResponse");
			SendCofiguration ();
//			throw new ArgumentNullException();
			object m = null;
			string s = m.ToString();
			break;
		}
	}

	void OnRowerStaticEvent(SocketIOEvent e){
		print ("OnRowerStaticEvent");
		print (pmChannel+" OnRowerStaticEvent "+e.data);
		InteroServerPayload payload = new InteroServerPayload (e.data);

		switch(payload.ev){
		case "listRowersResponse":
			print ("listRowersResponse" + payload.obj);
			SetRower (payload.obj [0]);
			break;
		}
		// test create workout
//		WOGModel cWOGModel = new WOGModel (true,"land","morning");
//		CreateTeamWorkouts (cWOGModel);
		// Test Join Team
		//		JoinTeam(0);
	
	}

	void OnWorkoutGroupStaticEvent(SocketIOEvent e){
		print ("OnWorkoutGroupStaticEvent"+ e.data);
		InteroServerPayload p = new InteroServerPayload (e.data);
//		string ev = "";
//		JSONObject o = e.data.GetField("data");
//		e.data.GetField(ref ev,"event");


		switch (p.ev) {
		// list workout responce	
		case "listWorkoutsResponse":
			print ("listWorkoutsResponse " + p.obj);
			if (p.obj.Count > 0) {
//				print ("joing WOG");
				JSONObject wog = p.obj [0];

				string s="";
				wog.GetField (ref s,"_id");
				//JoinWog (s);


				rowSessionManager.InitRowingSession (s);
//				canvasController.Hide();
				//				o [0];
			} /*else {
				print ("Take to create WOG");
				canvasController.DisplayCreateWOGView ();
			}*/

			break;

		}

	}

	void OnloginFB(SocketIOEvent e){
		print ("OnLogingFB");
		SetRower (e.data);
		// check if the user is in anyworkout
		GetCurrentWorkouts();
	}
	public void SetRower (JSONObject rower){
//		if(canvasController==null)
//			canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
		// is it the first time in?
		if (configHUD.setRower (rower)) {
			print("show init menu");
			// show configuration page
			canvasController.DisplayInitialSetup();

		} else {
			print("show main menu "+canvasController);
			print (canvasController);
			// show main menu
			canvasController.SetRower(configHUD.getRower());
			canvasController.DisplayMainMenuView();
		}
		// set socket of rower
		socket.Emit("RowerEvent",configHUD.getRower());
	}

	public void SaveQuestions (bool hasRowedCrew,bool hasVR, float workoutTime){
		JSONObject obj = new JSONObject();
		JSONObject rower = configHUD.getRower ();
		obj.AddField("workoutTime",workoutTime);
		obj.AddField("hasRowedCrew",hasRowedCrew);
		obj.AddField("hasVR",hasVR);
		rower.AddField("data",obj);
		rower.AddField("event","saveQuestions");
		print ("SaveQuestions");
		socket.Emit ("RowerEvent",rower);
	}
	public  void SendCofiguration(){
		JSONObject obj = new JSONObject();
		JSONObject rower = configHUD.getRower ();
//		obj.AddField("name",buttonName);
//		obj.AddField("text",buttonText);
		rower.AddField("data",configHUD.getJSON());
		rower.AddField("event","configuration");
		print ("SendCofiguration");
		socket.Emit ("RowerEvent",rower);
	}
	public void Send(string ev,string data){
		JSONObject obj = new JSONObject();
		JSONObject rower = configHUD.getRower ();
		obj.AddField("data",data);
		obj.AddField("event",ev);
		rower.AddField("data",obj);
		rower.AddField("event","unityEvent");
		print ("Send "+ev);
		if(socket!=null)
			socket.Emit ("RowerEvent",rower);
	}

	public void SendEndWOG(){
		JSONObject obj = new JSONObject();
		JSONObject rower = configHUD.getRower ();
//		endWOG
		rower.AddField("event","endWOG");
		print ("endWOG");
		socket.Emit ("RowerEvent",rower);

	}

	public void SendButtonClick(string buttonName,string buttonText){
		JSONObject obj = new JSONObject();
		JSONObject rower = configHUD.getRower ();
		obj.AddField("name",buttonName);
		obj.AddField("text",buttonText);
		rower.AddField("data",obj);
		rower.AddField("event","buttonCliked");
		print ("SendButtonClick");
		socket.Emit ("RowerEvent",rower);
	}
	/// <summary>
	/// //////
	/// 
	/// 
	/// 
	/// </summary>
	// WORKOUT functions
	public void GetTeamWorkouts(){
		JSONObject obj = new JSONObject();
		//		obj.AddField("data",configHUD.getRower ());
		JSONObject queryWOG = new JSONObject();
//		JSONObject rowerJSON = configHUD.getRower ();
		queryWOG.AddField("privateWOG",false);
		queryWOG.AddField("inProgress",true);
		queryWOG.AddField("team",configHUD.getTeam());
		obj.AddField("data",queryWOG);
		obj.AddField("event","listWorkouts");
		print ("GetTeamWorkouts");
		socket.Emit ("WorkoutGroupStaticEvent",obj);
	}
	public void GetCurrentWorkouts(){
		JSONObject obj = new JSONObject();
		JSONObject rowerJSON = configHUD.getRower ();
		JSONObject queryWOG = new JSONObject();
		queryWOG.AddField("inProgress",true);
		//		obj.AddField("data",configHUD.getRower ());
		string id = "";
		rowerJSON.GetField(ref id,"rowerId");
		queryWOG.AddField("rowerId",id);
		//		queryWOG.
		obj.AddField("data",queryWOG);
		obj.AddField("event","listWorkouts");
		print ("GetCurrentWorkouts");
		socket.Emit ("WorkoutGroupStaticEvent",obj);
		// update side menu
		canvasController.UpdateSideMenuHud(rowerJSON);
	}
	public void GetRivalWorkouts(){
		JSONObject obj = new JSONObject();
		JSONObject teamJSON = new JSONObject();
		string team = configHUD.getTeam ();
		teamJSON.AddField("inProgress",true);
		teamJSON.AddField("notteam",team);
//		string rivalTeam = "red";
		obj.AddField("data",teamJSON);
		obj.AddField("event","listWorkouts");
		print ("GetRivalWorkouts "+team);
		socket.Emit ("WorkoutGroupStaticEvent",obj);
	}
	public void EndWorkout(){
		JSONObject obj = new JSONObject();
		//		obj.AddField("data",configHUD.getRower ());
		obj.AddField("data",configHUD.getRower ());
		obj.AddField("event","listWorkouts");
		print ("GetTeamWorkouts");
		socket.Emit ("WorkoutGroupStaticEvent",obj);
	}
	public void CreateTeamWorkouts(WOGModel wogModel){
		socket.Emit("RowerEvent",wogModel.ToJSONEvent(configHUD.getRower()));
	}



	public void SendErgData(ErgData e){
		JSONObject rower=configHUD.getRower();
		if(rower!=null)
		socket.Emit("RowerEvent",e.ToJSONEvent(rower));
	}
	public void JoinWog(string wogId){
		print ("JoinWog");
		// The rower from configHUD should get updated , by getting the obj reference
		JSONObject rower = configHUD.getRower ();
		rower.AddField("data",wogId);
		rower.AddField("event","joinWOG");
		socket.Emit("RowerEvent",rower);
		//		obj.AddField("rowerId",id);
	}
	public void JoinTeam(string id){
		print ("JoinTeam");
		// The rower from configHUD should get updated , by getting the obj reference
		JSONObject rower = configHUD.getRower ();
		rower.AddField("data",id);
		rower.AddField("event","joinTeam");
		socket.Emit("RowerEvent",rower);
		//		obj.AddField("rowerId",id);
	}
	public void LoginUser(string id,string name){
		// 10156192670307119 
		// Rodrigo
		JSONObject obj = new JSONObject();
		print ("LoginUser "+id+" "+name);
		obj.AddField ("event", "loginFB");
		obj.AddField ("data", configHUD.setUserFB (id,name));
		socket.Emit ("UserStaticEvent", obj);//e.ToJSONEvent(configHUD.getRower()));
		print("Socket UserStaticEvent LoginUser emited");
	}
}
class InteroServerPayload{
	public string ev;
	public JSONObject obj;
	public InteroServerPayload(JSONObject data){
		obj = data.GetField("data");
		ev = "";
		data.GetField(ref ev,"event");
	}
}
//		wog = rower.sendEvent("createWOG",wogObj);
// let wogObj = {name:"Workout1"};

//		JSONObject rower = configHUD.getRower ();
//		rower.AddField("data",id);
//		rower.AddField("event","createWOG");

/*
		JSONObject obj = new JSONObject();
		obj.AddField("data",configHUD.getRower ());
		obj.AddField("event","creatWorkout");
		//	getRower
		socket.Emit ("WorkoutGroupStaticEvent",obj);
		*/

/*string rowerId = "";
		int lane =0;
		e.data.GetField(ref rowerId,"rowerId");
		e.data.GetField(ref lane,"lane");
		JSONObject o = e.data;
		JSONObject payload = o.GetField("data");
		ErgData erg = ErgData.From(payload);

		erg.i = lane;// configHUD.getChannelFromID (rowerId);
		print ("Placebo" + erg);
		testBLE.OnErgData (erg);
		*/
	/*
	// emails: 
	// apology
	// questions: have you killed? 
	// make emails with no format
	// people are consumers of our content.
	/*
	public void EmitName(string name){
		print ("Emit Name " + name);
		Dictionary<string,string> dic = new Dictionary<string,string> ();
		dic.Add ("name", name);
		dic.Add ("id", ""+configHUD.getPMChannel ());
		JSONObject jObj = new JSONObject (dic);
		socket.Emit ("name", jObj);
	}*/

//	void onWorkoutEntry(SocketIOEvent e){
//		print (pmChannel+"onWorkoutEntry "+e.data);
////		configHUD.setRower (e.data);
//
////			SendDumyErgData ();
//		//		e.data.GetField ("");
//	}

//		LoginUser("69","lordKevin");
//		SendDumyErgData ();

//		socket.Emit ("unityUser", e.data);
/*
		JSONEvent j = RowerEvents.static_createRower("Rodrigo");
		print (j.obj);
		socket.Emit (j.e,j.obj);
		*/
/*

*/