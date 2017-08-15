using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour {

	void Update(){
		if (Input.touchCount > 0)
			print (Input.touchCount);

	}

}
