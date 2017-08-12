//
//
#import "UnityBLEPM5.h"
// #import "ViewController.h"
//#import "PM5Controller.hpp"
//

extern "C" {
    UnityBLEPM5 *blePM5 = nil;
    void connectToPM5(int channel){
        char addr31[5] = {'0','0','3','1',0};
        char addr32[5] = {'0','0','3','2',0};
        addr31[1] += channel;
        addr32[1] += channel;
        
        printf("Channel %d\n",channel);
        blePM5 = [UnityBLEPM5 new];
//        - (void)insertObject:(id)anObject atIndex:(NSUInteger)index;
//        [array insertObject:obj atIndex:index];
        [blePM5 initialize: addr31 :addr32];
        
    }
    float getTime(){
        return blePM5.pm5Controller->mErgData.time;
    }
    float getDistance(){
        return blePM5.pm5Controller->mErgData.distance;
    }
    float getPower(){
        return blePM5.pm5Controller->mErgData.power;
    }
    float getPace(){
        return blePM5.pm5Controller->mErgData.pace;
    }
    float getSPM(){
        return blePM5.pm5Controller->mErgData.spm;
    }
    float getCalhr(){
        return blePM5.pm5Controller->mErgData.calhr;
    }
    float getCalories(){
        return blePM5.pm5Controller->mErgData.calories;
    }
    ///
    float getDriveLength(){
        return blePM5.strokeData->driveLength;
    }
    float getDriveTime(){
        return blePM5.strokeData->driveTime;
    }
    float getStrokeRecoveryTime(){
        return blePM5.strokeData->strokeRecoveryTime;
    }
    float getStrokeRecoveryDistance(){
        return blePM5.strokeData->strokeRecoveryDistance;
    }
    float getPeakDriveForce(){
        return blePM5.strokeData->peakDriveForce;
    }
    float getAvgDriveForce(){
        return blePM5.strokeData->avgDriveForce;
    }
    float getStrokeCount(){
        return blePM5.strokeData->strokeCount;
    }
    StrokeData* readStrokeData(){
//        StrokeData* sd = new
        
        return blePM5.strokeData;
    }
    ErgData* readErgData(){
        //        StrokeData* sd = new
        return &blePM5.pm5Controller->mErgData;
//        return blePM5.strokeData;
    }
}

void UnitySendMessageWrapper (NSString* method, NSString* name){//( char* method, char* name){
//    UnitySendMessage("BluetoothLEReceiver"
    const char* cMethod = [method UTF8String];
    const char* cName = [name UTF8String];
//    [ViewController sendUnityMessage:method name:name];
    
    UnitySendMessage("BLEReceiver", cMethod, cName);
}

CBUUID* toLongUUID(NSString* shortUUID){
    return [CBUUID UUIDWithString:[NSString stringWithFormat:@"ce06%@-43e5-11e4-916c-0800200c9a66",shortUUID]];
}
CBUUID* toLongUUID(char* shortUUID){
    return [CBUUID UUIDWithString:[NSString stringWithFormat:@"ce06%s-43e5-11e4-916c-0800200c9a66",shortUUID]];
}
//
@interface UnityBLEPM5 ()
@property (strong, nonatomic) CBCentralManager *centralManager;
@property (strong, nonatomic) CBPeripheral *discoveredPeripheral;
@property  char *ch31, *ch32;
@property StrokeData *strokeData;
@property (nonatomic) PM5Controller *pm5Controller;
@property BOOL bluetoothOn;
@property BOOL autoConnectPM5;
@end

@implementation UnityBLEPM5


/*
-(StrokeData*) strokeData {
    return self.strokeData;
}*/

- (void)initialize:(char*)ch31 : (char*)ch32;
{
    NSLog(@"Diente Azul inicializado Start %s %s",ch31,ch32);
    char names[24];
    self.ch31 = (char*)malloc(5);
    self.ch32 = (char*)malloc(5);
    strcpy (self.ch31,ch31);
    strcpy (self.ch32,ch32);
//    self.ch31 = ch31;
//    self.ch32 = ch32;
    self.bluetoothOn = NO;
    self.centralManager = [[CBCentralManager alloc] initWithDelegate: self queue:nil];
    self.strokeData = nil;
    self.autoConnectPM5 = true;
    self.pm5Controller = new PM5Controller();
    NSLog(@"Diente Azul inicializado ");
}

-(void)centralManagerDidUpdateState:(CBCentralManager *)central
{
    NSLog(@"centralManagerDidUpdateState");
    if(central.state != CBCentralManagerStatePoweredOn){
        // [self tLog:@"Diente azul apagado"];
        NSLog(@"bluetoothOff");
        self.bluetoothOn = NO;
    }else{
        // [self tLog:@"Diente azul Prendido"];
        NSLog(@"bluetoothOn");
        UnitySendMessageWrapper(@"onBLEOn",@"Happend");
        self.bluetoothOn = YES;
        if(self.autoConnectPM5){
            [self startScan];
        }
    }
//    NSString *s = [NSString stringWithFormat:@"%li",(long)central.state];
//    UnitySendMessageWrapper(@"BLEChangeState",s);
}

- (IBAction)startScan //:(id)sender
{
    NSLog(@"startScan");
    if(!self.bluetoothOn){
        NSLog(@"Bluetooth is OFF");
        return;
    }
    [self.centralManager scanForPeripheralsWithServices:nil options:@{CBCentralManagerScanOptionAllowDuplicatesKey: @NO}];
}


// - (void)didReceiveMemoryWarning
// {
//     [super didReceiveMemoryWarning];
// }

-(void) centralManager:(CBCentralManager *)central
 didDiscoverPeripheral:(CBPeripheral *)peripheral
     advertisementData:(NSDictionary *)advertisementData
                  RSSI:(NSNumber *)RSSI
{
    NSString* name = [advertisementData objectForKey:@"kCBAdvDataLocalName"];
    NSLog(@"didDiscoverPeripheral");
    NSLog(@"%@",[NSString stringWithFormat:@"Descubri %@, RSSI %@\n",name,RSSI]);
    // [self tLog:[NSString stringWithFormat:@"Descubri %@, RSSI %@\n",name,RSSI]];
    if ([name containsString:@"PM5"]) {
        NSLog(@"PM5 Periferial discovered");
        self.discoveredPeripheral = peripheral;
//        if([self verboseMode]){
        [self.centralManager connectPeripheral:peripheral options:nil];
//        }
    }
}

- (void) centralManager:(CBCentralManager *)central
didFailToConnectPeripheral:(CBPeripheral *)peripheral
                  error:(NSError *)error
{
    NSLog(@"didFailToConnectPeripheral");
}

-(void) centralManager:(CBCentralManager *)central didConnectPeripheral:(CBPeripheral *)peripheral
{
    peripheral.delegate = self;
    [peripheral discoverServices:nil];
    
    UnitySendMessageWrapper(@"onPM5Connected", @"OK");
}

-(void)peripheral:(CBPeripheral *)peripheral didDiscoverServices:(NSError *)error
{
    if(error){
        // [self tLog:[error description]];

        NSLog(@"didDiscoverService %@",[error description]);

        return;
    }
    for(CBService *service in peripheral.services){
        // [self tLog:[NSString stringWithFormat:@"Discovered service %@",[service description]]];
        [peripheral discoverCharacteristics:nil forService:service];
    }
}



-(void)peripheral:(CBPeripheral *)peripheral didDiscoverCharacteristicsForService:(CBService *)service error:(NSError *)error
{
    if(error){
        NSLog(@"didDiscoverCharacteristicsForService Error %@",[error description]);
//        [self tLog:[error description]];
        return;
    }
    for(CBCharacteristic *characteristic in service.characteristics){
        // [self tLog:[NSString stringWithFormat:@"Discovered Characteristic %@",[characteristic description]]];
        NSLog(@"didDiscoverCharacteristicsForService");
        if([characteristic.UUID isEqual: toLongUUID(self.ch31)]){
            [peripheral setNotifyValue:YES forCharacteristic:characteristic];
        }else if([characteristic.UUID isEqual: toLongUUID(self.ch32)]){
            [peripheral setNotifyValue:YES forCharacteristic:characteristic];
        }else if([characteristic.UUID isEqual: toLongUUID(@"0035")]){
            [peripheral setNotifyValue:YES forCharacteristic:characteristic];
        }else if([characteristic.UUID isEqual: toLongUUID(@"0036")]){
            [peripheral setNotifyValue:YES forCharacteristic:characteristic];
        }
//        [peripheral discoverCharacteristics:nil forService:service];
    }
}



- (void) peripheral:(CBPeripheral *)peripheral
didUpdateValueForCharacteristic:(CBCharacteristic *)characteristic
              error:(NSError *)error
{
//    NSLog(@"didUpdateValueForCharacteristic");
//    NSLog(@"Value updated %@",characteristic.UUID);
//    static CBufferPM5* mPM5Buffer= new CBufferPM5(NULL);
    PM5Controller* pm5Controller = self.pm5Controller;// new PM5Controller();
    if(error){
//        NSLog(@"didUpdateValueForCharacteristicError %@",[error description]);
        // [self tLog:[error description]];
        return;
    }
    pm5Controller->mPM5Buffer.setByteArray((void *)[characteristic.value bytes]);
    if([characteristic.UUID isEqual: toLongUUID(self.ch31)]){
        pm5Controller->readCharacteristic31();
        NSLog(@"Read31 %s",pm5Controller->mErgData.ToString());
    }
    else if([characteristic.UUID isEqual: toLongUUID(self.ch32)]){
        pm5Controller->readCharacteristic32();
        NSLog(@"Read32 %s",pm5Controller->mErgData.ToString());
        NSString *s = [NSString stringWithFormat:@"%s",pm5Controller->mErgData.ToString()];
        
        UnitySendMessageWrapper(@"onErgDataReady", s);
    }
    else if([characteristic.UUID isEqual: toLongUUID(@"0035")]){
        StrokeData* sd = pm5Controller->readCharacteristic35();
        NSString *s = [NSString stringWithFormat:@"%s",sd->ToString()];
        UnitySendMessageWrapper(@"onStrokeDataReady", s);
        self.strokeData = sd;
        NSLog(@"Read35 %s",sd->ToString());
        
    }
    else if([characteristic.UUID isEqual: toLongUUID(@"0036")]){
        pm5Controller->readCharacteristic36();
        NSString *s = [NSString stringWithFormat:@"%s",pm5Controller->mErgData.ToString()];
        NSLog(@"Read36 %@",s);
//        UnitySendMessageWrapper(@"onStrokeDataReady", s);
    }
    
}    
//        read31();
//        CBufferPM5* z;
//        mPM5Buffer->setByteArray([characteristic.value bytes]
//        z = new CBufferPM5( (void *) [characteristic.value bytes]);
//

 /*
 
 UInt8* b = (UInt8*)[characteristic.value bytes];
 //    NSLog(@"Value updated %hhu",b[0]);
 
 NSString *sFromData = [[NSString alloc] initWithData:characteristic.value encoding:NSUTF8StringEncoding];
 [self tLog: [NSString stringWithFormat:@"CHar updaterd: %@ %hhu",sFromData,b[0]]];
 //    self.valueLabel.text = @"CHar updaterd: %@;
 */

 
 




@end
