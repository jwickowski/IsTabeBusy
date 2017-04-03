/*
 * Blink
 * Turns on an LED on for one second,
 * then off for one second, repeatedly.
 */

#include <Arduino.h>

#define LED 04
#define BUTTON_TOP 0

void setup()
{
  // initialize LED digital pin as an output.
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
