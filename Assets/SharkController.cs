using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour {

	Animator sharkAnimator;
	// Use this for initialization
	void Start () {
		sharkAnimator = GetComponent<Animator> ();
//		transform.position = new Vector3 (10.0f, transform.position, transform.position);
	}
	public void Attack(){
		sharkAnimator.SetBool ("attack", true);
	}
	public void Swim(){
		sharkAnimator.SetBool ("attack", false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
