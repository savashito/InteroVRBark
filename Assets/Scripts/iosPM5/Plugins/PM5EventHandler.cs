using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PM5EventHandler {
	public static float Time;
	public static float Distance;
	public static float Power;
	public static float Pace;
	public static float SPM;
	public static float Calhr;
	public static float Calories;
	public static float DriveLength;
	public static float DriveTime;
	public static float StrokeRecoveryTime;
	public static float StrokeRecoveryDistance;
	public static float PeakDriveForce;
	public static float AvgDriveForce;
	public static float StrokeCount;
	#if UNITY_STANDALONE || UNITY_EDITOR


	public static void connectToPM5(int channel){
		
	}
	// ERGDATA
	public static float getTime(){
		return Time;
	}

	public static float getDistance(){
		return Distance;
	}

	public static float getPower(){
		return Power;
	}

	public static float getPace(){
		return Pace;
	}

	public static float getSPM(){
		return SPM;
	}

	public static float getCalhr(){
		return Calhr;
	}

	public static float getCalories(){
		return Calories;
	}

	// STROKE DATA
	public static float getDriveLength(){
		return DriveLength;
	}

	public static float getDriveTime(){
		return DriveTime;
	}

	public static float getStrokeRecoveryTime(){
		return StrokeRecoveryTime;
	}

	public static float getStrokeRecoveryDistance(){
		return StrokeRecoveryDistance;
	}

	public static float getPeakDriveForce(){
		return PeakDriveForce;
	}

	public static float getAvgDriveForce(){
		return AvgDriveForce;
	}

	public static float getStrokeCount(){
		return StrokeCount;
	}
//	public static void requestBLEAccess (){
//		
//	}
	#else

//	[DllImport ("__Internal")]
//	public static extern void requestBLEAccess ();

	[DllImport ("__Internal")]
	public static extern void connectToPM5 (int channel);

	[DllImport ("__Internal")]
	public static extern StrokeData readStrokeData ();

	[DllImport ("__Internal")]
	public static extern ErgData readErgData ();
	// ERGDATA
	[DllImport ("__Internal")]
	public static extern float getTime();

	[DllImport ("__Internal")]
	public static extern float getDistance();

	[DllImport ("__Internal")]
	public static extern float getPower();

	[DllImport ("__Internal")]
	public static extern float getPace();

	[DllImport ("__Internal")]
	public static extern float getSPM();

	[DllImport ("__Internal")]
	public static extern float getCalhr();

	[DllImport ("__Internal")]
	public static extern float getCalories();

	// STROKE DATA
	[DllImport ("__Internal")]
	public static extern float getDriveLength();

	[DllImport ("__Internal")]
	public static extern float getDriveTime();

	[DllImport ("__Internal")]
	public static extern float getStrokeRecoveryTime();

	[DllImport ("__Internal")]
	public static extern float getStrokeRecoveryDistance();

	[DllImport ("__Internal")]
	public static extern float getPeakDriveForce();

	[DllImport ("__Internal")]
	public static extern float getAvgDriveForce();

	[DllImport ("__Internal")]
	public static extern float getStrokeCount();

	#endif

}

