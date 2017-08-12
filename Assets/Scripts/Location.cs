using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class Location {
	public float time;
	public float mDistance;
//	public int spm;

	public Location (ErgData erg){
		time = erg.time;
		mDistance = erg.distance;
	}
	public Location (float time, float distance){
		this.time = time;
		this.mDistance = distance;
	}

	public Location (Location obj){
		time = obj.time;
		mDistance = obj.mDistance;
//		spm = obj.spm;
	}
	public void copy (ErgData erg){
		time = erg.time;
		mDistance = erg.distance;
	}
	public void copy (Location obj){
		time = obj.time;
		mDistance = obj.mDistance;
	}

	public void copy (float time, float distance){
		this.time = time;
		this.mDistance = distance;
	}

	public float getAcceleration(Location previousLocation){
		float deltaD = -(this.mDistance - previousLocation.mDistance);
		float deltaT = Mathf.Max( this.time - previousLocation.time,0.00001f);
		return deltaD/ deltaT;
	}
	public override string ToString(){
		return string.Format ("t: {0} d: {1} ", time, mDistance);
	}
	public float distance {
		get {
			return this.mDistance;
		}
	}
}