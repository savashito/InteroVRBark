using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWOGController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	WOGModel getCreateWOGModel(){
		WOGModel cWOGModel = new WOGModel (true,"land","morning");

		return cWOGModel;
	}
	public void OnOk(){
		WOGModel cWOGModel = getCreateWOGModel ();

	}
}

public class WOGModel
{
	bool joinAnyTime;
	string terrain;
	string dayTime;
	public WOGModel(bool j, string t, string d){
		joinAnyTime = j;
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
		obj.AddField("terrain",terrain);
		obj.AddField("dayTime",dayTime);
		return obj;
	}
}