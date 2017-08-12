using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FBController : MonoBehaviour {
	public Image fbImage;
	// Use this for initialization
	void Awake () {
		FB.Init (SetInit, OnHideUnity);
	}
	void SetInit(){
		if (FB.IsLoggedIn) {
			Debug.Log ("FB is logged in");
		} else {
			Debug.Log ("FB is Not! logged in");
		}
		DealWithFBMenus (FB.IsLoggedIn);
	}
	void OnHideUnity(bool isGameShow){
		if (!isGameShow) {
			Time.timeScale = 0;
		}else
			Time.timeScale = 0;
	}
	public void FBLogin(){
		List<string> perm = new List<string> ();
		perm.Add ("public_profile");
		FB.LogInWithReadPermissions (perm, AuthCallback);
	}
	void AuthCallback(IResult res){
		if (res.Error!=null) {
			Debug.Log (res.Error);
		} else {
			SetInit ();
		}
	}
	public GameObject DialogUsername;
	public GameObject DiagLoggedIn;
	public GameObject DiagLoggedOut;
	void DealWithFBMenus(bool isLoggedIn){
		if (isLoggedIn) {
			DiagLoggedIn.SetActive (false);
			DiagLoggedOut.SetActive (true);
			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			//			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
//			FB.API ("/me/picture", HttpMethod.GET, DisplayProfilePic);
		} else {
			DiagLoggedIn.SetActive (true);
			DiagLoggedOut.SetActive (false);
		}
	}
	public InteroServerConnection interoServerConnection;
	void DisplayUsername(IResult res){
		Text UserName = DialogUsername.GetComponent<Text> ();
		if (res.Error!=null && res.Error.Length>1) {
			Debug.Log (res.Error);

		} else {
			UserName.text = "hello " + res.ResultDictionary ["first_name"];
			/*
			foreach (KeyValuePair<string, object> item in res.ResultDictionary)
			{
				Debug.Log (item.Key);
//				string varName = item.Value.varName;
			}*/
			Debug.Log (res.ResultDictionary["id"]); 
			Debug.Log (res.ResultDictionary ["first_name"]);
			print (interoServerConnection);
			interoServerConnection.LoginUser (res.ResultDictionary ["id"].ToStringNullOk(), res.ResultDictionary ["first_name"].ToStringNullOk());
			//			fbImage.
		}
	}

	void DisplayProfilePic(IGraphResult res){
		if (res.Texture==null){// && res.Error.Length>1) {
			Debug.Log (res.Error);
			Debug.Log (res.ResultList);
		} else {
			fbImage.sprite = Sprite.Create(res.Texture, new Rect (0,0,128,128),new Vector2());
			//			
			//			UserName.text = "hello " + res.ResultDictionary ["first_name"];
			//			fbImage.
		}
	}

}
