using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBLE : MonoBehaviour {
//	#if UNITY_STANDALONE || UNITY_EDITOR
	ControllerPM5 controllerPM5;
	public ConfigurationHUD configHUD;
	public GameObject rowerPrefab;
	public PlayerController[] players = new PlayerController[4] {null,null,null,null};
	public GameObject[] playerGameobjs = new GameObject[4] {null,null,null,null};

	// Use this for initialization
	void Start () {
		controllerPM5 = GameObject.Find("BLEReceiver").GetComponent<ControllerPM5>();
		/*InvokeRepeating("callStroke", 0.0f, 3.0f);
		InvokeRepeating("callRecovery", 1.0f, 3.0f);
*/
		// InvokeRepeating("UpdateT", 0.0f, 0.10f);
	}


	public void OnErgData(ErgData erg){
		int channel = configHUD.getPMChannel ();
		print ("OnErgData "+erg);
		if (channel == erg.i) {
			/*
			// We are now in this lane, delete the adversary on it
			if (players [erg.i] != null) {
				print ("Player " + erg.i + " Schedule for deletion");
				Destroy (playerGameobjs [erg.i]);
				playerGameobjs [erg.i] = null;
				players [erg.i] = null;
				print ("Player " + erg.i + " deleted");
			}
//			print (erg);
			PM5EventHandler.Distance = erg.distance; 
			//print (PM5EventHandler.Distance);
			PM5EventHandler.Time = erg.time;
			PM5EventHandler.SPM = erg.spm;
			PM5EventHandler.Power = erg.power;
			PM5EventHandler.Pace = erg.pace;
			// call the controller and advise that there was astoke
			controllerPM5.onErgDataReady ("Meow");
			*/
		} else {
			// the others are rowers I compite against and should exist in the world
			if (players [erg.i]!=null) {
//				Destroy(players [erg.i]);
//				players [erg.i] = null;
//				print ("Meow "+erg);
//				print (erg);
				players [erg.i].OnErgDataPosition (erg);
				players [erg.i].OnErgDataAnimation (erg);
			} else {
				print ("init new rower");
//				print (players [erg.i]);
				// init the plaeyr
				GameObject player = GameObject.Instantiate (rowerPrefab);

				player.transform.position = new Vector3 (-1.0f, 0.0f, 7.0f * erg.i); 
				print ("Inited rower");
				PlayerController pltController = player.transform.Find ("Remero_bote").gameObject.GetComponent<PlayerController>();
				print (pltController);
				players [erg.i] = pltController;
				playerGameobjs [erg.i] = player;
//				players [erg.i] = 1;
					
			}
		}
//		PM5EventHandler.StrokeRecoveryTime = 2.0f;
	}

	public void onStrokeData(StrokeData stroke){
//		print ("ononStrokeData");
//		print (stroke);
		int channel = configHUD.getPMChannel ();
		//		print ("OnErgDataChannrl "+channel);
		if (channel == stroke.i) {
			PM5EventHandler.DriveTime = stroke.driveTime;
			PM5EventHandler.StrokeRecoveryTime = stroke.strokeRecoveryTime;
			controllerPM5.onStrokeDataReady ("bark");
		}
	}
	void UpdateT () {
		PM5EventHandler.Distance += 0.1f; 
		//print (PM5EventHandler.Distance);
		PM5EventHandler.Time = Time.fixedTime;
		PM5EventHandler.SPM = 25;
		controllerPM5.onErgDataReady ("Meow");
		PM5EventHandler.StrokeRecoveryTime = 2.0f;

	}
	/*
	// Update is called once per frame

	// function is called every second
	void callStroke(){
		PM5EventHandler.DriveTime = 1.0f;
		PM5EventHandler.StrokeRecoveryTime = 0.0f;
		controllerPM5.onStrokeDataReady ("bark");
		print ("callStroke");
	}
	// function is called two second
	void callRecovery(){
		PM5EventHandler.DriveTime = 0.0f;
		PM5EventHandler.StrokeRecoveryTime = 2.0f;
		controllerPM5.onStrokeDataReady ("bark");
		print ("callRecovery");
	}*/

//	#endif
}
