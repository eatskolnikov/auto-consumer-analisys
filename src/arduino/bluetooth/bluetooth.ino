#include <SoftwareSerial.h>
#define rxPin 5
#define txPin 4
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
  Serial.begin(baud_rate);
  bluetoothInterface.begin(baud_rate);
  bluetoothInterface.write("AT+INIT\r\n");
  inputString.reserve(512);
  delay(1000);
}

void loop() {
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
    }while(inChar !='\n');
    Serial.println(inputString);  
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
      bluetoothInterface.write("AT+CLASS=1\r\n");
      bluetoothInterface.write("AT+INQM=1,9,10\r\n");
     //+IPSCAN:1024,512,1024,512 <- Defaults
      bluetoothInterface.write("AT+IPSCAN=128,64,1024,512\r\n");
      cmd = BTINQ;
    break;
    case BTINQ:
      //1234,500,1200,250
      //bluetoothInterface.write("AT+IPSCAN=100,50,1024,512\r\n");
      bluetoothInterface.write("AT+INQ\r\n");
      cmd = -1;
      break;
  }
}
