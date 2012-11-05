#include <SPI.h>        
#include <Ethernet.h>
#include <EthernetUdp.h>
// Enter a MAC address and IP address for your controller below.
// The IP address will be dependent on your local network:
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x00, 0x1A, 0x40 };
byte mac[] = { 0x90, 0xA2, 0xDA, 0x0D, 0x53, 0x40 };
IPAddress ip(10, 0, 0, 21);
IPAddress server_ip(10, 0, 0, 107);

unsigned int localPort = 8888;      // local port to listen on
char packetBuffer[UDP_TX_PACKET_MAX_SIZE]; //buffer to hold incoming packet,
EthernetUDP Udp;

void setup() {
  // start the Ethernet and UDP:
  Ethernet.begin(mac,ip);
  Udp.begin(localPort);
}

void sendmessage(char* message)
{
    // send a reply, to the IP address and port that sent us the packet we received
    Udp.beginPacket(server_ip, localPort);
    Udp.write(message);
    Udp.endPacket();
}

void loop() {
  sendmessage("Test\r\n");
  delay(10);
}
