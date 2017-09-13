using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlanRowing : MonoBehaviour {
	Animator animator;
	float deltaTime = 0.0f;
	public GameObject[] botes;
	public GameObject splash;
	public GameObject PSsplash;
	private Transform referencia1, referencia2;
	private bool bajoAgua;
	private GameObject mySplash1, mySplash2, myPSsplash1, myPSsplash2;
//	private GameObject miBote;
	private int oldBote;
	private float currentSPM = -10.0f;

	// Use this for initialization
	void Start () {
//		InvokeRepeating ("CambiarBote", 1.0f, 1.0f);

		animator = GetComponent<Animator> ();
		animator.SetFloat ("speedStroke", 2.2f );// 12.0f);
		animator.SetFloat ("speedRecovery",1.0f );// 12.0f);

		referencia1 = this.transform.Find("remo1").Find("referencia");
		referencia2 = this.transform.Find("remo2").Find("referencia");

		bajoAgua = false;

		oldBote = 0;
		/*
		miBote=Instantiate(botes[0]);
		miBote.transform.parent = this.gameObject.transform;
		miBote.transform.localPosition = new Vector3 (0, 0, 0);
		miBote.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));*/

	}
	/*
	void CambiarBote(){
		int newBote = (int) this.transform.position.z/7 + 1;

		newBote %= botes.Length;

		if (newBote != oldBote) {
			Destroy (miBote);
			miBote=Instantiate(botes[newBote]);
			miBote.transform.parent = this.gameObject.transform;
			miBote.transform.localPosition = new Vector3 (0, 0, 0);
			miBote.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0));

			oldBote = newBote;
		}
	}
	*/
	public void startStroke(){
//		animator.SetTrigger ("startStroke");
	}
	public void startRecovery(){
//		animator.SetTrigger ("startRecovery");
	}
	// Update is called once per frame
	void Update () {
		
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		if (referencia1.position.y <= 0) {
			if (!bajoAgua) {
				mySplash1 = Instantiate (splash, new Vector3 (referencia1.position.x, 0, referencia1.position.z), Quaternion.Euler (0, -90, 0));
				mySplash2 = Instantiate (splash, new Vector3 (referencia2.position.x, 0, referencia2.position.z), Quaternion.Euler (0, 90, 0));

				myPSsplash1 = Instantiate (PSsplash, new Vector3 (referencia1.position.x, 0, referencia1.position.z), Quaternion.Euler (-90, 0, 0));
				myPSsplash2 = Instantiate (PSsplash, new Vector3 (referencia2.position.x, 0, referencia2.position.z), Quaternion.Euler (-90, 0, 0));

				bajoAgua = true;
			} else {
				mySplash1.transform.position = new Vector3 (referencia1.position.x, 0, referencia1.position.z);
				mySplash2.transform.position = new Vector3 (referencia2.position.x, 0, referencia2.position.z);

				myPSsplash1.transform.position = new Vector3 (referencia1.position.x, 0, referencia1.position.z);
				myPSsplash2.transform.position = new Vector3 (referencia2.position.x, 0, referencia2.position.z);
			}
		}else if(bajoAgua){
			bajoAgua = false;
			Destroy (mySplash1, 2.0f);
			Destroy (mySplash2, 2.0f);

			Destroy (myPSsplash1);
			Destroy (myPSsplash2);

			myPSsplash1 = Instantiate (PSsplash, new Vector3 (referencia1.position.x, 0, referencia1.position.z), Quaternion.Euler (-90, 0, 0));
			myPSsplash2 = Instantiate (PSsplash, new Vector3 (referencia2.position.x, 0, referencia2.position.z), Quaternion.Euler (-90, 0, 0));

			Destroy (myPSsplash1, 2.0f);
			Destroy (myPSsplash2, 2.0f);
		}


	}
	public void setStrokeSpeed(float stroke){
		// receive the stroke duration  in seconds
//		print("setStrokeSpeed");
//		float strokeAni =  1.792f/stroke;
//		animator.SetFloat ("speedStroke", strokeAni );// 12.0f);
	}
	public void setRecoverySpeed(float recovery){
		// receive the stroke duration  in seconds
		//		print("setStrokeSpeed");
//		float recoveryAni = 2.125f/recovery;
//		animator.SetFloat ("speedRecovery",recoveryAni );// 12.0f);
	}
	/*public void setStrokePercentage(float p){
		animator.SetFloat ("speedRecovery",recoveryAni );// 12.0f);
		animator.SetFloat ("speedStroke", strokeAni );// 12.0f);
	}*/

	public void setSPM(float spm,float strokePercent){
		print ("setSPM "+spm);
//		animator.GetCurrentAnimatorClipInfo
		float tRecoveryNorm = 2.125f;
		float tStrokeNorm = 1.792f;
		if (strokePercent < 0.01f) {
			strokePercent = tStrokeNorm / (tStrokeNorm + tRecoveryNorm);
			strokePercent = 0.35f;
//			strokePercent = 0.3f;
		}
//		print ("currentSPM: "+currentSPM);
		if(currentSPM != spm){
	//		print ("Bark "+spm);
			currentSPM = spm;
			/*
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
	//		spm = 22;
	//		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
	//		print (text);
	//		print (spm);
	//		print (fps);
			// *spm / 4.958f
			float tRecoveryNorm = 2.125f;
			float tStrokeNorm = 1.792f;
			float strokePercent = 0.5f;
			float totalDuration;//1.0f* 1.792f+1.0f*2.125f;

			float tRecovery= (1-strokePercent)*tRecoveryNorm,tStroke=tStrokeNorm*strokePercent;
			totalDuration = tRecovery + tStroke;
			print ("tStrokeNorm" + tStrokeNorm+"\tRecoveryNorm" + tRecoveryNorm);
			print ("tStroke" + tStroke+"\ttRecovery" + tRecovery);
			print ("strogDUr"+(tStroke*totalDuration/tStrokeNorm));
			print ("strogDUr"+(tRecovery*totalDuration/tRecoveryNorm));
			*/

			print ("strokePercent " + strokePercent);
			print ("spm " + spm);
			animator.SetFloat ("speedStroke",(spm/60.0f)* tStrokeNorm/strokePercent);// tStroke*totalDuration);
			animator.SetFloat ("speedRecovery",(spm/60.0f)*tRecoveryNorm/(1-strokePercent));// tRecovery*totalDuration);
		/*
			// normailize the duration
			tRecovery /= totalDuration;
			tStroke /= totalDuration;
			float strokePercent = 0.9f;
			// 1.792
			float speedStroke =  (strokePercent*1.0f/tStroke);
			float speedRecovery = (1.0f-strokePercent)*1.0f/tRecovery;
			float totalStrokeSpeed = speedStroke + speedRecovery;
//			animator.SetFloat ("speedStroke",totalStrokeSpeed*speedStroke);
				// *spm/60.0f );// 12.0f);
			// 2.125
//			animator.SetFloat ("speedStroke",tStroke);
//			animator.SetFloat ("speedRecovery",tRecovery);
			print ("speedStroke " + speedStroke + "\t" + "speedRecovery "+ speedRecovery);
				// *spm/60.0f );// 12.0f);
			*/
		}
	}


}


	
