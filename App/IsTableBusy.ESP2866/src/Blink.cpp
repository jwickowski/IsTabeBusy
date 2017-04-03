#include <Arduino.h>

#define LED 04
#define BUTTON_TOP 0

void setup()
{
  pinMode(LED, OUTPUT);
  pinMode(BUTTON_TOP, INPUT);
}

void loop()
{
  bool buttonStatus =  digitalRead(BUTTON_TOP);
  if(buttonStatus == HIGH){
    digitalWrite(LED, LOW);
  }
  else{
    digitalWrite(LED, HIGH);
  }
}
