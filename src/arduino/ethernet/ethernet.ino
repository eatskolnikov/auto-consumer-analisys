#include <SPI.h>        
#include <Ethernet.h>
#include <EthernetUdp.h>

byte mac[] = { 0x90, 0xA2, 0xDA, 0x00, 0x1A, 0x40 };
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x0D, 0x53, 0x40 };
IPAddress ip(10, 0, 0, 21);
//IPAddress ip(10, 0, 0, 22);
IPAddress server_ip(10, 0, 0, 107);

unsigned int localPort = 8888;
char packetBuffer[UDP_TX_PACKET_MAX_SIZE];
EthernetUDP Udp;

void setup() {
  // start the Ethernet and UDP:
  Ethernet.begin(mac,ip);
  Udp.begin(localPort);
}

void sendmessage(char* message)
{
    Udp.beginPacket(server_ip, localPort);
    Udp.write(message);
    Udp.endPacket();
}

void loop() {
  sendmessage("Test\r\n");
  delay(10);
}
