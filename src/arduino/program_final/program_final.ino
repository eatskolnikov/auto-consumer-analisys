#include <SoftwareSerial.h>
#include <SPI.h>        
#include <Ethernet.h>
#include <EthernetUdp.h>

#define rxPin 6
#define txPin 5
#define ledPin 13
#define baud_rate 38400

#define BTINIT 0
#define BTSETCLASS 1
#define BTINQ 2
#define BTADDRESS 3
#define BTINQLOOP 4

byte mac[] = { 0x90, 0xA2, 0xDA, 0x00, 0x1A, 0x40 };
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x0D, 0x53, 0x40 };
//IPAddress ip(10, 0, 0, 21);
IPAddress ip(10, 0, 0, 21);
IPAddress server_ip(10, 0, 0, 107);

unsigned int localPort = 8888;
char packetBuffer[UDP_TX_PACKET_MAX_SIZE];
EthernetUDP Udp;

SoftwareSerial bluetoothInterface(rxPin, txPin);

int checked = 0;
String inputString = "";
int cmd = BTINIT;

void setup() {
  Ethernet.begin(mac,ip);
  Udp.begin(localPort);
  
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
    }
    while(inChar !='\n');
    if(inputString != "OK")
    {
      char charBuff[inputString.length()];
      inputString.toCharArray(charBuff, inputString.length());
      Udp.beginPacket(server_ip, localPort);
      Udp.write(charBuff);
      Udp.endPacket();
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
    bluetoothInterface.write("AT+IPSCAN=128,64,1024,512\r\n");
    cmd = BTINQ;
    break;
  case BTINQ:
    bluetoothInterface.write("AT+INQ\r\n");
    cmd = -1;
    break;
  }
}
