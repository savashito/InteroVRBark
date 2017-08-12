using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GhostController : MonoBehaviour {

	private AlanRowing alan;
	private Location previousLocation;
	private Location currentLocation;
	private Rigidbody mRigidBody;
	public float kP=1.0f, kD=1.0f;
	private string nombre = "ghost.txt";
	public string usuario = "Usuario";
	private StreamReader archivo;
	private bool end;
	public TextAsset ghost;

	// Use this for initialization
	void Start () {

		string path;

		mRigidBody = GetComponent<Rigidbody> ();
		currentLocation = new Location (0, 0);
		previousLocation = (Location)null;
		alan = GetComponent<AlanRowing> ();

		nombre=GameObject.Find ("Net Manager").GetComponent<NetManager> ().GhostFile;

		path = Application.persistentDataPath + "/" + usuario+ "/" + nombre;

		if (!File.Exists(path)){
			File.WriteAllText (path, ghost.text);
		}
			
		archivo = new StreamReader(path, Encoding.Default);
		end = false;

		updatePosition();
//		InvokeRepeating ("updatePosition", 0f, 0.5f);

	}
	private float prevTime =0.0f;
	private void updatePosition(){
		float time = 0, distance;
		
		int spm;

		if (end)
			return;

		string linea= archivo.ReadLine();

		if (linea != null) {
			string[] datos = linea.Split(' ');
			time = float.Parse(datos [0]);
			distance = float.Parse(datos [1]);
			spm = int.Parse(datos [2]);
			alan.setSPM (spm,-1.0f);
			currentLocation.copy (time, distance);
			setLocation (currentLocation);
			
		} else {
			alan.setSPM (0,-1.0f);
			end = true;
			archivo.Close();
		}
		Invoke ("updatePosition", time-prevTime);
		prevTime = time;
	}

	private void setLocation(Location location){
		if (previousLocation == null){
			// set the previous location to the current location
			previousLocation = new Location(location);
			// set the location og the boat to the previousLocation
			mRigidBody.position = new Vector3 (previousLocation.mDistance, mRigidBody.position.y, mRigidBody.position.z);
			// set the speed of boat to zero
			mRigidBody.velocity = new Vector3(0,0,0);
			// set the 
			previousLocation.mDistance = mRigidBody.position.x;
			return;
		}
		float dT = location.time - previousLocation.time;
		if (dT < 0.0001f)
			return;
		float deltaDistance = location.mDistance - previousLocation.mDistance;
		float speed = deltaDistance / dT;

		float deltaForce = kD*speed + kP*(location.distance-mRigidBody.position.x);// + (location.distance-mRigidBody.position.z);//*correctionParameter;

		mRigidBody.velocity = new Vector3(deltaForce,mRigidBody.velocity.y,mRigidBody.velocity.z);

		previousLocation.copy(location);
	}

}
