using System;


public struct StrokeData
{
	public float time;
	public float distance;
	public float driveLength;
	public float driveTime;
	public float strokeRecoveryTime;
	float strokeRecoveryDistance;
	float peakDriveForce;
	float avgDriveForce;
	float strokeCount;
	float strokePower;
	float strokeCalories;
	public int i;

	public StrokeData (float time, float distance, float driveLength, float driveTime, float strokeRecoveryTime, float strokeRecoveryDistance)
	{
		this.time = time;
		this.distance = distance;
		this.driveLength = driveLength;
		this.driveTime = driveTime;
		this.strokeRecoveryTime = strokeRecoveryTime;
		this.strokeRecoveryDistance = strokeRecoveryDistance;
		peakDriveForce = 0.0f;
		avgDriveForce = 0.0f;
		strokeCount = 0.0f;
		strokePower = 0.0f;
		strokeCalories = 0.0f;
		i = 0;
	}

	public static StrokeData From(JSONObject data){
		float time=0.0f;
		float distance=0.0f;
		float driveLength=0.0f;
		float driveTime=0.0f;
		float strokeRecoveryTime=0.0f;
		float strokeRecoveryDistance=0.0f;
		int i = 0;

		data.GetField(ref i,"i");
		data.GetField(ref distance,"distance");
		data.GetField(ref time,"time");
		data.GetField(ref driveLength,"driveLength");
		data.GetField(ref driveTime,"driveTime");
		data.GetField(ref strokeRecoveryTime,"strokeRecoveryTime");
		data.GetField(ref strokeRecoveryDistance,"strokeRecoveryDistance");

		StrokeData l = new StrokeData(time,distance,driveLength,driveTime,strokeRecoveryTime,strokeRecoveryDistance);
		l.i = i;
		return l;
	}
	override public string ToString(){
		return string.Format("[t: {0},d: {1},drive: {2}, recovery: {3}]",time,distance,driveTime,strokeRecoveryTime);
	}
	public float getStrokeTime(){
		return strokeRecoveryTime+driveTime;
	}

}

/*

<Buffer 37 20 00 49 0e 00 7c 3c 8e 00 da 03 19 07 fc 03 08 15 27 00> { time: 82.47,
  distance: 365.70000000000005,
  driveLength: 1.24,
  driveTime: 0.6,
  strokeRecoveryTime: 1.42,
  strokeRecoveryDistance: 9.86,
  peakDriveForce: 181.70000000000002,
  avgDriveForce: 102,
  strokeCount: 5384 }
<Buffer 37 20 00 f1 00 69 04 27 00 00 00 00 0c 1f 00> { time: 82.47,
  strokePower: 241,
  strokeCalories: 1129,
  strokeCount: 39 }


*/
