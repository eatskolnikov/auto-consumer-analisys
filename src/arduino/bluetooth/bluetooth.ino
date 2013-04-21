#include <SoftwareSerial.h>
#define rxPin 6
#define txPin 5
#define ledPin 13
#define baud_rate 38400

#define BTINIT 0
#define BTSETCLASS 1
#define BTINQ 2
#define BTADDRESS 3
#define BTINQLOOP 4

SoftwareSerial bluetoothInterface(rxPin, txPin);

int checked = 0;
String inputString = "";
int cmd = BTINIT;

void setup() {
  pinMode(13, OUTPUT);
  Serial.begin(baud_rate);
  bluetoothInterface.begin(baud_rate);
  delay(2000);
  bluetoothInterface.write("AT+INIT\r\n");
  inputString.reserve(512);
}

void loop() {
  //delay(1000);
  //digitalWrite(13, HIGH);
  //delay(1000);
  //digitalWrite(13, LOW);
}

void serialEvent() {
  inputString = "";
  if(Serial.available())
  {
    char inChar = ' ';
    do {
      inChar = (char)Serial.read();
      if(inChar >= 32 && inChar <127)
        inputString += inChar;
    }
    while(inChar !='\n');
    if(inputString != "OK")
    {
      Serial.println(inputString);  
    }
    if(cmd == -1)
      cmd = BTINQ;
  }
  switch (cmd)
  {
  case BTINIT:
    bluetoothInterface.write("AT+ROLE=1\r\n");
    cmd = BTSETCLASS;
    break;
  case BTSETCLASS:
    bluetoothInterface.write("AT+CLASS=0\r\n");
    bluetoothInterface.write("AT+INQM=1,9,10\r\n");
    //+IPSCAN:1024,512,1024,512 <- Defaults
    bluetoothInterface.write("AT+IPSCAN=128,64,1024,512\r\n");
    cmd = BTINQ;
    break;
  case BTINQ:
    bluetoothInterface.write("AT+INQ\r\n");
    cmd = -1;
    break;
  }
}

