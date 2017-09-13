using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/*
#if UNITY_STANDALONE || UNITY_EDITOR
SocketConnection
#else
BLEConnection
#endif
*/
// 
// 
// SocketConnection

public class PlayerController : ErgDataAbstract //: FactoryCommunication
{

//	public float kD, kP;
	//public TextUtil textUtil;
	public FactoryCommunication fact;
	public AlanRowing alan;
	public float kP=1.0f, kD=1.0f;
//	public CameraController cameraController;
	public ConfigurationHUD confHUD; 
//	public Toggle toggleRecenterOnStroke;
//	public FactoryCommunication fact
	//	public float pos;
	private Location previousLocation;
	private Location currentLocation;
	private Rigidbody mRigidBody;﻿
	private bool mUpdatePosition = false;
	private string archivo;
	public string usuario = "Usuario";
	private string path;
	public bool iniciar = true;
	public ErgDisplayController displayController;
	//	public Animation anim;

	void Start () {
//		base.Start ();
		print("StartPlayerController");
//		GvrViewer.Instance.Recenter ();
//		alan.setSPM (22,-0.0f);
		alan.setSPM (0,-0.0f);
		confHUD = GameObject.Find("ConfigHandler").GetComponent<ConfigurationHUD>();
		//fact=GameObject.Find("Connection Logic").GetComponent<FactoryCommunication>();
//		textUtil=GameObject.Find("HUD").GetComponent<TextUtil>();
		//fact.Init(this);
//		Application.targetFrameRate = 24;
		mRigidBody = GetComponent<Rigidbody> ();
		currentLocation = new Location (0, 0);
		previousLocation = (Location)null;

		path = Application.persistentDataPath + "/" + usuario;

		if (!Directory.Exists(path)){
			Directory.CreateDirectory (path);
		}

		archivo = string.Format("{0:yyyy-MM-dd-hh_mm}.txt", System.DateTime.Now);
		/*
		if (confHUD.isFPV) {
			cameraController.setFPV ();
		} else {
			cameraController.setTPV ();
		}*/
	}


	public void setFPV(){
//		Camera fpv;
//		Camera tpv;
//
//		GameObject o = transform.Find ("1PCamera");
//		print ("setFPV" + o);
//	
////			.gameObject.GetComponent<Camera>();
////		tpv = transform.Find("3PCamera").gameObject.GetComponent<Camera>();
//		//
//		fpv.enabled = true;
//		tpv.enabled = false;
	}
	public void OnStrokeData(StrokeData strokeData){
//		print (strokeData);
		if (strokeData.driveTime < 0.01f) {
			// stroke started
//			if(toggleRecenterOnStroke.isOn)
			if(confHUD.isRecenterOnStroke())
//				cameraController.Recenter();
//				GvrViewer.Instance.Recenter ();
			alan.startStroke ();
//			print ("startStroke");
			alan.setRecoverySpeed (strokeData.strokeRecoveryTime);
		}

		if (strokeData.strokeRecoveryTime < 0.01f) {
			// recovery started
			alan.startRecovery();
//			print ("startRecovery");
			alan.setStrokeSpeed (strokeData.driveTime);
		}
	}

	public void OnErgDataPosition(ErgData ergData){
		currentLocation.copy (ergData);

		if (mUpdatePosition) {
			float s = confHUD.isRowBackwardsOn() ? -1 : 1;
			mRigidBody.position = new Vector3 (s*currentLocation.mDistance, mRigidBody.position.y, mRigidBody.position.z);
			mUpdatePosition = false;
		} else {
			//			if(previousLocation.distance == currentLocation.distance)
			//				alan.setSPM (0);
			setLocation (currentLocation);
		}
	}

	public void OnErgDataAnimation(ErgData ergData){
		alan.setSPM (ergData.spm,-1.0f);
	}
	//	displayController.SetFields (ergData);
	public override void OnErgData(ErgData ergData){
//		print ("spm--> " + ergData.spm);
		//+" " +ergData.distance );
		//textUtil.SetFields (ergData);
//		alan.setSPM (ergData.spm);
//		print ("onr eegr "+ergData);
		if (currentLocation == null)
			return; 
//		print ("displayController ");
//		print (ergData);
		displayController.SetFields (ergData);
//		print ("bark1");
		OnErgDataPosition(ergData);
		//OnErgDataAnimation (erg);
		print ("displayController ");
		SPMInfo spmInfo = confHUD.getSPMFrom (ergData);
//		print ("displayController ");
		alan.setSPM (spmInfo.spm,spmInfo.strokePercentage);
		print ("spm--> " + spmInfo.spm);
//		;
//		if (confHUD.isRowingSolo)
//			alan.setSPM (ergData.spm);
//		else {
//			if()
//			alan.setSPM (confHUD.getSPMFrom( ergData.distance));
//			alan.setSPM (confHUD.getSPMFrom( ergData.time));
//		}

//		print ("bark2");
//		print (currentLocation);
// playerController init has not been called yet

		File.AppendAllText(path+"/"+archivo, ergData.time+" "+ergData.distance+" "+ergData.spm+" "+ergData.power+" "+ergData.pace+"\n");

	}

	public void Update(){
		/*
		if (currentLocation.distance > 0 && currentLocation.distance <100) {
			alan.setSPM (20);
		}
		else if (currentLocation.distance > 100) {
			alan.setSPM (28);
		}*/
//		print("Mow");
//		mRigidBody = GetComponent<Rigidbody> ();
		// mRigidBody.velocity = new Vector3 (1.0f, 0.0f, 0.0f);
		/*
		currentLocation.mDistance += 0.1f; 
		currentLocation.time = Time.fixedTime;
		setLocation (currentLocation);
		print (currentLocation);
		*/
	}


	public void setLocation(Location location){
		if(mRigidBody==null)
			mRigidBody = GetComponent<Rigidbody> ();
		//		print ("setLocation");
		if (previousLocation == null){
			// set the previous location to the current location
			previousLocation = new Location(location);
			// set the location og the boat to the previousLocation
			mRigidBody.position = new Vector3 (previousLocation.mDistance, mRigidBody.position.y, mRigidBody.position.z);
			// set the speed of boat to zero
			mRigidBody.velocity = new Vector3(0,0,0);
			// set the 
			previousLocation.mDistance = mRigidBody.position.x;
			//print ("Prev "+previousLocation.ToString());
			return;
		}
		//		anim["Rowering"].speed = -1.0f;
		//		print ("Not set");
		//		print (location.distance);
		//		 this.time - previousLocation.time,0.00001f)
		float dT = location.time - previousLocation.time;
		if (dT < 0.0001f)
			return;
		float deltaDistance = location.mDistance - previousLocation.mDistance;
		float speed = deltaDistance / dT;

		/*
		print (previousLocation.time);
		print (location.time);
		print (dT);
		print (deltaDistance);
		print (speed);
*/
		//		print (location.getAcceleration (previousLocation));
		//		previousLocation.mDistance = mRigidBody.position.z;
		// location.getAcceleration(previousLocation)


//		print (location);
		float deltaForce = kD*speed + kP*(location.distance-mRigidBody.position.x);// + (location.distance-mRigidBody.position.z);//*correctionParameter;
		//print (deltaForce);
		//		float force = location.distance-mRigidBody.position.x;
		// 392.8198
		// print (mRigidBody.position.z);
		//		print (location.mDistance);
		//		mRigidBody.position = new Vector3(mRigidBody.position.x+deltaForce,mRigidBody.position.y,mRigidBody.position.z);
		//		mRigidBody.position = new Vector3(-location.mDistance,mRigidBody.position.y,mRigidBody.position.z);
		//		mRigidBody.velocity = new Vector3(deltaForce,mRigidBody.velocity.y,mRigidBody.velocity.z);
		//		mRigidBody.velocity = new Vector3(deltaForce,mRigidBody.velocity.y,mRigidBody.velocity.z);
		mRigidBody.velocity = new Vector3(deltaForce,mRigidBody.velocity.y,mRigidBody.velocity.z);

		previousLocation.copy(location);
//		previousLocation.mDistance = mRigidBody.position.x;
		//		print("z");
	}


	/*
	public void setLocation(Location location){
		if (previousLocation == null){
			previousLocation = new Location(location);
//			previousLocation.mDistance = mRigidBody.position.x;
			mRigidBody.position = new Vector3 (previousLocation.mDistance, mRigidBody.position.y, mRigidBody.position.z);
			mRigidBody.velocity = new Vector3(0,0,0);
			print ("Prev "+previousLocation.ToString());
			return;
		}

		float dT =location.time - previousLocation.time;
		if (dT < 0.0001f)
			return;
		print ("pos: " +mRigidBody.position);
		float deltaDistance = location.mDistance - previousLocation.mDistance;
		float speed = deltaDistance / dT;
		print ("deltaDistance: "+deltaDistance);
		print ("dt: "+dT);
		print ("peed:" +speed );
		//		mRigidBody.velocity = new Vector3(speed,mRigidBody.velocity.y,mRigidBody.velocity.z);
		mRigidBody.velocity = new Vector3(deltaDistance,mRigidBody.velocity.y,mRigidBody.velocity.z);
		// Update position of previous location
		previousLocation.time = location.time;
		previousLocation.mDistance = mRigidBody.position.x;
		/*
		float dT = Mathf.Max(location.time - previousLocation.time,0.0001f);
		float deltaDistance = location.mDistance - previousLocation.mDistance;
		float speed = deltaDistance / dT;
		print ("prevT: "+previousLocation.time);
		print ("currentT: "+location.time);
		print ("dt: "+dT);
		print ("eltaD: "+deltaDistance);
		print ("m/s: "+speed);
		mRigidBody.velocity = new Vector3(-speed,mRigidBody.velocity.y,mRigidBody.velocity.z);
//		previousLocation.copy(location);
		// Update position of previous location
		previousLocation.time = location.time;
		previousLocation.mDistance = mRigidBody.position.x;
		*/
//	}
}
