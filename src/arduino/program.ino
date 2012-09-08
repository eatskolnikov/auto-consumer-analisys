#include <SoftwareSerial.h>
#define rxPin 3
#define txPin 4
#define ledPin 13
#define baud_rate 38400
#define baud_rate_bt 9600

#define BTINIT 0
#define BTSETCLASS 1
#define BTINQ 2
#define BTADDRESS 3
#define BTINQLOOP 4


SoftwareSerial bluetoothInterface = SoftwareSerial(rxPin, txPin);

int checked = 0;
String inputString = "", addr="";
int cmd = BTINIT;

void setup() {
  bluetoothInterface.begin(baud_rate);
  Serial.begin(baud_rate);
  while (!Serial);
  bluetoothInterface.write("AT+INIT\r\n");
  addr.reserve(30);
  inputString.reserve(512);
}

void loop() {
}

void serialEvent() {
  //inputString = "";
  inputString = "";
  
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    inputString += inChar;
  }
  if(!(inputString == ""))
  {
    Serial.print(addr);
    Serial.print(inputString);
  }
  switch (cmd)
  {
    case BTINIT:
      bluetoothInterface.write("AT+ROLE=1\r\n");
      cmd = BTSETCLASS;
    break;
    case BTSETCLASS:
      bluetoothInterface.write("AT+INQM=1,9,48\r\n");
      cmd = BTINQ;
    break;
    case BTINQ:
      bluetoothInterface.write("AT+ADDR?\r\n");
      cmd = BTADDRESS;
      break;
    case BTADDRESS:
      addr += inputString;
      cmd = BTINQLOOP;
      //addr += '|';
    default:
      bluetoothInterface.write("AT+INQ\r\n");
  }
}
