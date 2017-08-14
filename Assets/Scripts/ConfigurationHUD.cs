using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

public class ConfigurationHUD : MonoBehaviour {


	public Toggle toggleRecenterOnStroke;
	public Toggle toggleHideRower;
	public Toggle toggleUltraOn;
	public Toggle toggleWIFIOn;

	public Toggle toggleVROn;
	public bool isRowingSolo;
//	bool isHideRowerFlag;
	public bool isOfflineGame = false;
//	public bool isConstraintDistance; // TODO
	public Dropdown dropdownEnviroment,dropdownPM3Channel;

	public Material manana,tarde,noche;
//	RangoSPM[] ranges = new RangoSPM[3];
	public ConfiguracionDoble confDoble;
//	public bool isFPV = false;
	private JSONObject rowerJSON = null;
	public bool isRecenterOnStroke(){
		return toggleRecenterOnStroke.isOn;
	}
	public int getLane(){
		return getPMChannel()*7;
	}
	public bool isHideRower(){
		return toggleHideRower.isOn;
	}
	public bool isVROn(){
		return toggleVROn.isOn;
	}
	public bool isUltraOn(){
		return toggleUltraOn.isOn;
	}
	public bool isWIFIOn(){
		return toggleWIFIOn.isOn;
	}
	private int pmChannel = 0;
	public void setPMChannel(int c){
		pmChannel = c;
	}
	public int getPMChannel(){
		if (dropdownPM3Channel == null)
			return pmChannel;
		return dropdownPM3Channel.value;
	}
	public void Start(){
		VRSettings.enabled = false;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
//		QualitySettings.antiAliasing = 8;
	}
	// returns true if the rower was newly created.
	// False if the rower already existed
	public bool setRower(JSONObject jObj){
		string id="",team ="";
		rowerJSON = new JSONObject();
		jObj.GetField(ref id,"_id");
		jObj.GetField(ref team,"team");
		print ("setRower " + id);
		rowerJSON.AddField("rowerId",id);
		rowerJSON.AddField("lane",getPMChannel());
		rowerJSON.AddField("team",team);
		return (team.Length<3);
	}
	public string getTeam(){
		string team="";
		rowerJSON.GetField(ref team,"team");
		return team;
	}
	public JSONObject getRower(){
		// if null, send test obj
		if (rowerJSON == null) {
			JSONObject obj = new JSONObject();
//			obj.AddField("team","blue");
//			obj.AddField("stupid","sexy");
			return obj;
		}
		return rowerJSON;
		//		Dictionary<string,string> dic = new Dictionary<string,string> ();obj.rowerId
//		dic.Add ("rowerId",));

	}
	public int getChannelFromID(string userID){
		return int.Parse(""+ userID [userID.Length - 1]);
	}
//	string mUserIdFB=null; 
	public JSONObject setUserFB(string userIdFB,string name){
		Dictionary<string,string> dic = new Dictionary<string,string> ();
		dic.Add ("userIdFB", userIdFB);
		dic.Add ("name", name);
		return new JSONObject (dic);
	}
	/*
	public JSONObject getUser(){
		Dictionary<string,string> dic = new Dictionary<string,string> ();
//		switch(getPMChannel ())
		dic.Add ("userId", "4edd40c86762e0fb1200000"+getPMChannel ());
//		dic.Add ("id", ""+configHUD.getPMChannel ());
		JSONObject jObj = new JSONObject (dic);
//		socket.Emit ("name", jObj);
//		data.userId
		return jObj;
	}*/

	public Material getSkybox(){
		switch (dropdownEnviroment.value) {
		case 0:
			return manana;
		case 1:
			return tarde;
		case 2:
			return noche;
		}
		return manana;
	}
	public void ConstraintDistance(){
		
		// change labels
		confDoble.setDistance();
	}
	public void ConstraintTime(){
//		isConstraintDistance = false;
		// change labels to distance
		confDoble.setTime();
	}

	public void configOnOK(){
//		isHideRowerFlag = toggleHideRower.isOn;
	}
	public void onOK(){
		confDoble.getRangeArray ();
//		print( ranges[0].ToString());
//		print( ranges[1].ToString());
//		print( ranges[2].ToString());
	}
	private SPMInfo SPMinfo = new SPMInfo ();
	public SPMInfo getSPMFrom(ErgData ergData){
		float spm = ergData.spm;
		print (spm);
		if (isRowingSolo==false){
			spm = confDoble.getSPMFrom (ergData);
		}
//		print ("1");
		SPMinfo.spm = spm;
		SPMinfo.strokePercentage = -1.0f;
//		print ("2");
		return SPMinfo;
	}

	public float getSPMFromConstraint( float distance){
		return confDoble.getSPMFromConstraint (distance);
		//		for (int i =0; i<ranges.Length;++i){
//			// inside range, return SPM
//			if (ranges[i]!=null && distance > ranges[i].startRange && distance <ranges[i].endRange) {
//				return ranges[i].spm;
//			}
//		}
//		// default 
//		return 20.0f;
	}

}

public class SPMInfo{
	public float spm,strokePercentage;
}