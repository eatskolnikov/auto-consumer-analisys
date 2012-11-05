#include <SPI.h>        
#include <Ethernet.h>
#include <EthernetUdp.h>
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

//#define MAC { 0x90, 0xA2, 0xDA, 0x0D, 0x53, 0x40 }
#define MAC { 0x90, 0xA2, 0xDA, 0x00, 0x1A, 0x40 } 
//#define IP 10, 0, 0, 101
#define IP 10, 0, 0, 102
#define SERVER_IP 10, 0, 0, 21
#define LOCALPORT 8888

byte mac[] = MAC;
IPAddress ip(IP);
IPAddress server_ip(SERVER_IP);
unsigned int localPort = LOCALPORT;   // local port to listen on
char packetBuffer[UDP_TX_PACKET_MAX_SIZE]; //buffer to hold incoming packet,
EthernetUDP Udp;

SoftwareSerial bluetoothInterface = SoftwareSerial(rxPin, txPin);
int checked = 0;
String inputString = "", addr="";
int cmd = BTINIT;

void setup() {
  // start the Ethernet and UDP:
  Ethernet.begin(mac,ip);
  Udp.begin(localPort);

  //setup the bluetooth
  bluetoothInterface.begin(baud_rate);
  Serial.begin(baud_rate);
  while (!Serial);
  bluetoothInterface.write("AT+INIT\r\n");
  addr.reserve(30);
  inputString.reserve(512);
}
void sendmessage(char* message)
{
    Udp.beginPacket(server_ip, localPort);
    Udp.write(message);
    Udp.endPacket();
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
    //Serial.print(inputString);
    Udp.beginPacket(server_ip, localPort);
    char *arr;
    addr.toCharArray(arr, addr.length(), addr.length());
    Udp.write(arr);
    Udp.endPacket();
    addr = "";
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
    default:
      bluetoothInterface.write("AT+INQ\r\n");
  }
}
