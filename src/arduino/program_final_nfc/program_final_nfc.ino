#include <SoftwareSerial.h>
#include <PN532.h>
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

#define PN532_CS 9
#define ETHNET_CS 10

char* uint32_to_char_array(uint32_t num);

byte mac[] = { 0x90, 0xA2, 0xDA, 0x00, 0x1A, 0x40 };
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x0D, 0x53, 0x40 };
//IPAddress ip(10, 0, 0, 21);
IPAddress ip(10, 0, 0, 21);
IPAddress server_ip(10, 0, 0, 107);

unsigned int localPort = 8888;
char packetBuffer[UDP_TX_PACKET_MAX_SIZE];
EthernetUDP Udp;
PN532 nfc(PN532_CS);
SoftwareSerial bluetoothInterface(rxPin, txPin);

int checked = 0;
String inputString = "";
int cmd = BTINIT;

void spiSelect(int CS){
  pinMode(PN532_CS,OUTPUT);
  pinMode(ETHNET_CS,OUTPUT);
  digitalWrite(PN532_CS,HIGH);
  digitalWrite(ETHNET_CS,HIGH);
  // enable the chip we want
  digitalWrite(CS,LOW);  
}

void setup() {
  spiSelect(ETHNET_CS);
  SPI.setBitOrder(MSBFIRST);
  Ethernet.begin(mac,ip);
  Udp.begin(localPort);
  
  spiSelect(PN532_CS);
  SPI.setBitOrder(LSBFIRST);
  nfc.begin();
  nfc.SAMConfig();
  Serial.begin(baud_rate);
}
void loop() {
  spiSelect(PN532_CS);
  SPI.setBitOrder(LSBFIRST);
    
  uint32_t id;
  id = nfc.readPassiveTargetID(PN532_MIFARE_ISO14443A);
  if (id != 0)
  {
      Serial.println(id);
      char charBuff[] ={'1','2','3'};//uint32_to_char_array(id);
      spiSelect(ETHNET_CS);
      SPI.setBitOrder(MSBFIRST);
      Udp.beginPacket(server_ip, localPort);
      Udp.write(charBuff);
      Udp.endPacket();
  }
}

char* uint32_to_char_array(uint32_t num){
  char str[11];
  int i=0;
  for(i=0;i<11;++i){ str[i] = '0'; }
  while(num > 0){
     str[i] = '0'+num%10;
     num = num/10;
  }
  return str;
}
