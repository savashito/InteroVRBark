using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class laneManager : NetworkBehaviour {
	[SyncVar]
	public int lineaDisponible;

	void OnTriggerEnter(Collider other) {
		if (!isServer)
			return;
		this.transform.position += Vector3.forward * 7;
		lineaDisponible=(int)this.transform.position.z;
	}

	// Use this for initialization
	void Start () {
		if (!isServer)
			return;
		lineaDisponible=(int)this.transform.position.z;
	}

}
