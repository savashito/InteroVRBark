//
//  PM5Controller.cpp
//  BLEScanner
//
//  Created by Rodrigo Savage on 3/4/17.
//  Copyright © 2017 Michael Lehman. All rights reserved.
//

#include "PM5Controller.h"

PM5Controller::PM5Controller()
    : mPM5Buffer(NULL){
//    mPM5Buffer(NULL);
}

void PM5Controller::readCharacteristic31(){
    float time;
    float distance;
    float flags;
    float totalWOGDistance;
    float totalWOGTime;
    float WOGTimeType;
    float drag;
    
    //        char* s = z->getDebugByteString();
    //        NSLog(@"%s",s);
    time =  mPM5Buffer.readTime();
    distance = mPM5Buffer.readDistance();
    flags = mPM5Buffer.readUnimportantFlags(5);
    totalWOGDistance = mPM5Buffer.readDistance();
    totalWOGTime = mPM5Buffer.readTime();
    WOGTimeType = mPM5Buffer.readByte();
    drag = mPM5Buffer.readByte();
    mErgData.distance = distance;
    mErgData.time = time;
    
    //        readTalkMe();
//    NSLog(@"Value updated %f, %f, %f, %f, %f, %f ,%f  ",time, distance, flags, totalWOGDistance, totalWOGTime, WOGTimeType, drag);
}
void PM5Controller::readCharacteristic32(){
    float time  ;
    float speed ;
    float spm ;
    float hr ;
    float pace ;
    float avgPace ;
    float restDistance ;
    float restTime ;

    time  = mPM5Buffer.readTime(),
    speed = mPM5Buffer.read2Bytes()*0.001f,
    spm = mPM5Buffer.readByte(),
    hr = mPM5Buffer.readByte(),
    pace = mPM5Buffer.readPace(),
    avgPace = mPM5Buffer.readPace(),
    restDistance = mPM5Buffer.read2Bytes(),
    restTime =mPM5Buffer.readTime();
    mErgData.time = time;
    mErgData.spm = spm;
    mErgData.pace = pace;
}
StrokeData* PM5Controller::readCharacteristic35(){
    StrokeData* sd = new StrokeData();
       
    sd->time  =mPM5Buffer.readTime();
    sd->distance =mPM5Buffer.readDistance();
    sd->driveLength =mPM5Buffer.readByte()*0.01f;
    sd->driveTime =mPM5Buffer.readByte()*0.01f;
    sd->strokeRecoveryTime =mPM5Buffer.read2Bytes()*0.01f;
    sd->strokeRecoveryDistance =mPM5Buffer.read2Bytes()*0.01f;
    sd->peakDriveForce =mPM5Buffer.read2Bytes()*0.1f;
    sd->avgDriveForce =mPM5Buffer.read2Bytes()*0.1f;
    sd->strokeCount =mPM5Buffer.read2Bytes();
    return sd;
}
void PM5Controller::readCharacteristic36(){
    float time;
    float strokePower;
    float strokeCalories;
    float strokeCount;
    time =  mPM5Buffer.readTime();
    strokePower = mPM5Buffer.read2Bytes();
    strokeCalories = mPM5Buffer.read2Bytes();
    strokeCount = mPM5Buffer.read2Bytes();
    mErgData.power = strokePower;
}
