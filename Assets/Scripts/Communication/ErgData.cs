using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using SocketIO;

public struct ErgData: IData
{
	

	public float distance;
	public float power;
	public float pace;
	public float spm;
	public float time;
	public float calhr;
	public float calories;
	public int i;
	public string getEvent() {
		return "logWorkoutEntry";
	}
	public ErgData (float time,float distance, int spm,float power,float pace){
		i = 0;
		this.time = time;
		this.distance = distance;
		this.spm = spm;
		this.power = power;
		this.pace = pace;
		calhr = 0.0f;
		calories = 0.0f;
	}
	public static ErgData FromBytes(byte[] data){
		ErgData l = new ErgData(0,0,0,0,0);
		if(data.Length>15){//data[0]==0x69 && data[1] == 0x69 && data[2]== 1){
			// correct header <3
			// array is in littleEndian format
			if (BitConverter.IsLittleEndian == false)
			{
				Array.Reverse(data); // Convert big endian to little endian
			}
			int spm = data [2];//BitConverter.To(data, 2);
			float distance = BitConverter.ToSingle(data, 3+4*0);
			float power = BitConverter.ToSingle(data, 3+4*1);
			float pace = BitConverter.ToSingle(data, 3+4*2);
			float time = BitConverter.ToSingle(data, 3+4*3);
			l = new ErgData(time,distance,spm,power,pace);
		}
		return l;
	}
	public JSONObject ToJSONEvent(JSONObject userIdObj){
//		JSONObject ergDataJSON = ToJSON();
//		JSONObject obj = new JSONObject();
		userIdObj.AddField("event","logWorkoutEntry");
//		obj.AddField("rowerId",id);
		userIdObj.AddField("data",ToJSON());
//		const rowerId = obj.rowerId;
//		const rower = RowerController.Get(rowerId);
//		console.log(ev,obj.data);
		return userIdObj;
	}
	public JSONObject ToJSON(){
		JSONObject obj = new JSONObject();
		obj.AddField("time",time);
		obj.AddField("distance",distance);
		obj.AddField("spm",spm);
		obj.AddField("power",power);
		obj.AddField("pace",pace);
		return obj;
	}
	public static ErgData From(JSONObject data){
//		JSONObject data = e.data;
		float d = 0f, t = 0f;
		float pace = 0f,power = 0f;
		int i=0;
//		byte c_spm = 0;
		int spm = 0;
		data.GetField(ref d,"distance");
		data.GetField(ref i,"i");
		data.GetField(ref t,"time");
		data.GetField(ref spm,"spm");
		data.GetField(ref power,"power");
		data.GetField(ref pace,"pace");
		data.GetField(ref spm,"spm");

		ErgData l = new ErgData(t,d,spm,power,pace);
		l.i = i;
		return l;
	}
	override public string ToString(){
		return string.Format("[pace: {4},t: {0},d: {1},spm: {2}, power: {3}, i: {5}]",time,distance,spm,power,pace,i);
	}
}


