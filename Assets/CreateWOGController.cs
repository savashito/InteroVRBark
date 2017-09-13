using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWOGController : MonoBehaviour {
	public InteroServerConnection interoServer;
	// Use this for initialization
	void Start () {
		
	}
	
	WOGModel getCreateWOGModel(){
		WOGModel cWOGModel = new WOGModel (true,"land","morning",false);

		return cWOGModel;
	}
	public void CreateWOG(bool pWOG){
		WOGModel cWOGModel = getCreateWOGModel ();
		cWOGModel.privateWOG = pWOG;
		interoServer.CreateTeamWorkouts (cWOGModel);
	}
}

public class WOGModel
{
	bool joinAnyTime;
	public bool privateWOG;
	string terrain;
	string dayTime;
	public WOGModel(bool j, string t, string d,bool pWog){
		joinAnyTime = j;
		privateWOG = pWog;
		terrain = t;
		dayTime = d;
	}
	public JSONObject ToJSONEvent(JSONObject userObj){
		userObj.AddField("event","createWOG");
		//		obj.AddField("rowerId",id);
		userObj.AddField("data",ToJSON());
		return userObj;
	}
	public JSONObject ToJSON(){
		JSONObject obj = new JSONObject();
		obj.AddField("joinAnyTime",joinAnyTime);
		obj.AddField("privateWOG",privateWOG);
		obj.AddField("terrain",terrain);
		obj.AddField("dayTime",dayTime);
		return obj;
	}
}