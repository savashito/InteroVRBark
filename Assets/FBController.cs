using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FBController : MonoBehaviour {
	public Image fbImage;
	CanvasController canvasController;
	public InteroServerConnection interoServerConnection;
	// Use this for initialization
	void Start(){
		canvasController = GameObject.Find ("Canvas").GetComponent<CanvasController> ();
	}
	void Awake () {
		FB.Init (SetInit, OnHideUnity);
	}
	void SetInit(){
		/*
		if (FB.IsLoggedIn) {
			Debug.Log ("FB is logged in");
		} else {
			Debug.Log ("FB is Not! logged in");
		}*/
		DealWithFBMenus (FB.IsLoggedIn);
	}
	public void TestLogin1(){
		interoServerConnection.LoginUser ("10156192670307119", "Rodrigo");
	}
	public void TestLoginNoInternet(){
//		interoServerConnection.LoginUser ("10156192670307119", "Rodrigo");
		string id="",team ="",name="";
		JSONObject rowerJSON = new JSONObject();
//		JSONObject config = jObj.GetField ("lastConfiguration");
//		LoadCofiguration (config);
//		jObj.GetField(ref id,"_id");
//		jObj.GetField(ref name,"name");
//		jObj.GetField(ref team,"team");
		print ("setRower " + id);
		rowerJSON.AddField("_id","1223");
		rowerJSON.AddField("name","Rodrigo");
		rowerJSON.AddField("lane",0);
		rowerJSON.AddField("team","red");
		interoServerConnection.SetRower (rowerJSON);
//		return (team.Length<3);
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
//	public GameObject DiagLoggedIn;
//	public GameObject DiagLoggedOut;
	void DealWithFBMenus(bool isLoggedIn){
		if (isLoggedIn) {
//			canvasController.DisplayMainMenuView ();
			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			//			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
//			FB.API ("/me/picture", HttpMethod.GET, DisplayProfilePic);
		} else {
//			canvasController.DisplayFBLogingView();
		}
	}

	void DisplayUsername(IResult res){
		Text UserName = DialogUsername.GetComponent<Text> ();
		if (res.Error!=null && res.Error.Length>1) {
			Debug.Log (res.Error);

		} else {
			UserName.text =  ""+ res.ResultDictionary ["first_name"];
			/*
			foreach (KeyValuePair<string, object> item in res.ResultDictionary)
			{
				Debug.Log (item.Key);
//				string varName = item.Value.varName;
			}*/
			Debug.Log (res.ResultDictionary["id"]); 
			Debug.Log (res.ResultDictionary ["first_name"]);
//			print (interoServerConnection);
			interoServerConnection.LoginUser (res.ResultDictionary ["id"].ToStringNullOk(), res.ResultDictionary ["first_name"].ToStringNullOk());
			//			fbImage.
		}
	}
	/*
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
*/
}
