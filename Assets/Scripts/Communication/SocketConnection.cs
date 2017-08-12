using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SocketConnection : MonoBehaviour  {
	public SocketIOComponent socket;
	public TestBLE testBLE;
	public ConfigurationHUD configHUD;
//	private ErgDataAbstract factoryCommunication;
	// Use this for initialization
	/*
	public void Init (ErgDataAbstract factoryCommunication) {
		this.factoryCommunication = factoryCommunication;
		InitSocket ();
	}
*/
//	private Client socket = null; 

	public void InitSocket(){
		print ("InitSocket");
//		string socketUrl = "http://mysioserver";
//		Debug.Log("socket url: "+socketUrl);
/*
		this.socket = new Client(socketUrl);
		this.socket.Opened += this.SocketOpened;
		this.socket.Message += this.SocketMessage;
		this.socket.SocketConnectionClosed += this.SocketConnectionClosed;
		this.socket.Message += this.SocketError;
		this.socket.Connect();
*/
		
		socket.On ("open", OnConnected);
		socket.On ("disconnect", OnDisconnect);
		socket.On ("ergData", OnErgData);
		socket.On ("strokeData", OnStrokeData);
		socket.Connect ();
		print ("Trying to connect");
		
	}

	void OnDisconnect(SocketIOEvent e){
//		print ("Disconnect");
	}
	void OnConnected(SocketIOEvent e){
		
		print ("Connected");
		socket.Emit ("unityUser", e.data);
		/*
		JSONEvent j = RowerEvents.static_createRower("Rodrigo");
		print (j.obj);
		socket.Emit (j.e,j.obj);
		*/
	}
	public void EmitName(string name){
		print ("Emit Name " + name);
		Dictionary<string,string> dic = new Dictionary<string,string> ();
		dic.Add ("name", name);
		dic.Add ("id", ""+configHUD.getPMChannel ());
		JSONObject jObj = new JSONObject (dic);
		socket.Emit ("name", jObj);
	}
	/*
	protected void OnErgData (ErgData ergData){
//		print (ergData.ToString());
		factoryCommunication.OnErgData(ergData);
	}*/
	void OnStrokeData(SocketIOEvent e){
		
		StrokeData ergData = StrokeData.From (e.data);
		testBLE.onStrokeData (ergData);
	}
	void OnErgData(SocketIOEvent e){
//		print ("ergData");
//		print (e.data);
		ErgData ergData = ErgData.From (e.data);
		testBLE.OnErgData (ergData);
		 
//		print (ergData);
//		factoryCommunication.OnErgData(ergData);
//		OnErgData (ergData);
		// e.
//		Location l = RowerEvents.ergData(e);
		//		print ("sdj");
		//		print (l.time);
//		plController.setLocation (l);
	}

}
