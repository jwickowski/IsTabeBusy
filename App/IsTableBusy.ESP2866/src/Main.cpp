#include <Arduino.h>
#include "Button.h"
#define LED 04
#define BUTTON_TOP 0



bool ledState;

void changeLed(){
  ledState = !ledState;
  digitalWrite(LED, ledState);
}
Button button =Button(BUTTON_TOP, changeLed);


void setup()
{
  ledState = HIGH;
  pinMode(LED, OUTPUT);

  digitalWrite(LED, ledState);
}

void loop(){
button.Process();
}
