using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class InteroServerConnection : MonoBehaviour {
	private SocketIOComponent socket;
	public ConfigurationHUD configHUD = null;
	bool hasConnected = false;
	public int pmChannel = 0;
	public TestBLE testBLE = null;
	public CanvasController canvasController = null;
	// Use this for initialization
	void Start () {
		
		socket = GetComponentInParent<SocketIOComponent> ();
		socket.On ("open", OnConnected);
		socket.On ("disconnect", OnDisconnect);
		socket.On ("rowers", onRowers);
		socket.On ("RowerEvent", onRowerEvent);
		socket.On ("WOGEvent", OnWOGEvent);
		socket.Connect ();
		print ("Trying to connect");
//		configHUD = GameObject.Find ("ConfigHandler").GetComponent<ConfigurationHUD> ();
		if (configHUD == null) {
			configHUD = new ConfigurationHUD ();
			configHUD.setPMChannel (pmChannel);
		}
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();

	}


	void onRowerEvent(SocketIOEvent e){
//		print ("onRowerEvent"+ e.data);
		string rowerId = "";
		int lane =0;
		e.data.GetField(ref rowerId,"rowerId");
		e.data.GetField(ref lane,"lane");
		JSONObject o = e.data;
		JSONObject payload = o.GetField("data");
		ErgData erg = ErgData.From(payload);

		erg.i = lane;// configHUD.getChannelFromID (rowerId);
		print ("Placebo" + erg);
		testBLE.OnErgData (erg);

	}

	void OnDisconnect(SocketIOEvent e){
		print (pmChannel+"Disconnect");
		hasConnected = false;
	}
	void OnConnected(SocketIOEvent e){
//		print (pmChannel+"Connected");
		if (hasConnected)
			return;
		// check insert log correct id, or is hardcoded, configurationHUD
		hasConnected = true;
		print (pmChannel+"Connected");
		GetTestRower ();
//		GetTeamWorkouts ();
	}
	void onRowers(SocketIOEvent e){
		print (pmChannel+" OnRowers "+e.data);
		configHUD.setRower (e.data);
		// set socket of rower
		socket.Emit("RowerEvent",configHUD.getRower());

		// test create workout

		// Test Join Team
		//		JoinTeam(0);
		/*
		if (pmChannel == 1) {
			InvokeRepeating("SendDumyErgData", 0, 1.0f);
		}*/
	}

	// WORKOUT functions
	public void GetTeamWorkouts(){
		JSONObject obj = new JSONObject();
		obj.AddField("data",configHUD.getRower ());
		obj.AddField("event","listWorkouts");
		//	getRower
		socket.Emit ("WorkoutGroupStaticEvent",obj);
	}
	public void CreateTeamWorkouts(WOGModel wogModel){

//		wog = rower.sendEvent("createWOG",wogObj);
		// let wogObj = {name:"Workout1"};

//		JSONObject rower = configHUD.getRower ();
//		rower.AddField("data",id);
//		rower.AddField("event","createWOG");
		socket.Emit("RowerEvent",wogModel.ToJSONEvent(configHUD.getRower()));
		/*
		JSONObject obj = new JSONObject();
		obj.AddField("data",configHUD.getRower ());
		obj.AddField("event","creatWorkout");
		//	getRower
		socket.Emit ("WorkoutGroupStaticEvent",obj);
		*/
	}
	void OnWOGEvent(SocketIOEvent e){
		print ("OnWOGEvent"+ e.data);
		string ev = "";
		JSONObject o = e.data.GetField("data");
		e.data.GetField(ref ev,"event");


		switch (ev) {
		// list workout responce	
		case "listWorkouts":
			print ("listWorkouts" + o);
			if (o.Count > 0) {
				print ("joing WOG");
//				o [0];
			} else {
				print ("Take to create WOG");
				canvasController.DisplayCreateWOGView ();
			}

			break;

		}
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
	}


	public void SendErgData(ErgData e){
		socket.Emit("RowerEvent",e.ToJSONEvent(configHUD.getRower()));
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
		print ("LoginUser");
		obj.AddField ("event", "loginFB");
		obj.AddField ("data", configHUD.setUserFB (id,name));
		socket.Emit ("UserStaticEvent", obj);//e.ToJSONEvent(configHUD.getRower()));
		print("Socket just emited");
	}

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
}

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