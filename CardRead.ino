#include <deprecated.h>
#include <SPI.h>
#include <MFRC522.h>
#include "pitches.h"

#define RST_PIN         9          // Configurable, see typical pin layout above
#define SS_PIN          10         // Configurable, see typical pin layout above

MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance (RFID reader)
byte readCard[4];                  // Length of UID

String MasterTag = "B6C01B2B";     // Test ID for verification
String tagID = "";                 // ID obtained from RFID card

int IRSensor4 = 4;                 // IR Sensor in Pin 4
int IRSensor5 = 5;                 // IR Sensor in Pin 5
int IRSensor7 = 7;                 // IR Sensor in Pin 7
int IRSensor8 = 8;                 // IR Sensor in Pin 8

int Buzzer = 6;                    // Buzzer in pin 6 for audio (MUST BE ANALOG)
//int Tone = 32;
//------------------------------------------------------------------------------------------------------------

void setup() {
  //-------------------Serial & RFID Setup-----------------------------------
  Serial.begin(9600);		// Initialize serial communications with the PC
  SPI.begin();			// Init SPI bus
  mfrc522.PCD_Init();		// Init MFRC522
  // Serial.println(F("Scan PICC to see UID, SAK, type, and data blocks..."));
  //--------------------Pins setup ------------------------------------------
  pinMode(IRSensor7, INPUT);
  pinMode(IRSensor8, INPUT);

  pinMode(Buzzer, OUTPUT);
}

void loop() {

  // Reset loop while no card is present, so the interrupt pin can automatically turn on the reader.
  while (getID())
    Serial.println(tagID + "|m"); //Add |m for newline character in c# code, to prevent carriage return
    song();
    delay(2000); //Add a delay to prevent IRSensor from spamming Buzzer after sucessful scan
  }
  // Make buzzer noises if all IRSensors detect presence
  if (getPrescence() > 0)
  {
    int indicatedTone = getPrescence();
    tone(Buzzer, indicatedTone, 250);
    delay(500);
    /*
      if (Tone <= 2500)
      Tone += Tone * (5 / 7);
      else
      Tone = 32;
    */
  }
}

boolean getID()
{
  // Getting ready for Reading PICCs
  if ( ! mfrc522.PICC_IsNewCardPresent()) { //If a new PICC placed to RFID reader continue
    return false;
  }
  if ( ! mfrc522.PICC_ReadCardSerial()) { //Verify valid card
    return false;
  }
  tagID = "";
  for ( uint8_t i = 0; i < 4; i++) { // MIFARE cards have a 4 byte UID
    readCard[i] = mfrc522.uid.uidByte[i];
    tagID.concat(String(mfrc522.uid.uidByte[i], HEX)); // Adds the 4 bytes in a single String variable
  }
  tagID.toUpperCase();
  mfrc522.PICC_HaltA(); // Stop reading
  return true;
}

//Detects prescence if all IRSensors read positive
int getPrescence()
{
  // Detect if at least two motion trackers are being detected
  int sum = digitalRead(IRSensor7) + digitalRead(IRSensor8) + digitalRead(IRSensor4) + digitalRead(IRSensor5);
  // Returns certain tones based on how many sensors are triggered
  if (sum == 3)
  {
    return 0;
  }
  else if (sum == 2) 
    return 850;
  else if (sum == 1)
    return 950;
  else if (sum == 0)
    return 1000;
  else
    return 0;
}
//Confirmation tone for RFID
void song() {
  tone (Buzzer, N_C6, 100);
  delay(50);
  tone (Buzzer, N_G6, 100);
  delay(50);


}
