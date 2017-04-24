#include <Arduino.h>

#define LED 04
#define BUTTON_TOP 0

unsigned long lastDebounceTime = millis();
unsigned long deboundeDelay = 50;

bool previousButtonState;
bool ledState = LOW;
void setup()
{
  pinMode(LED, OUTPUT);
  pinMode(BUTTON_TOP, INPUT);
  previousButtonState = digitalRead(BUTTON_TOP);
  digitalWrite(LED, ledState);
}

void changeLed(){
  ledState = !ledState;
  digitalWrite(LED, ledState);
}

bool shouldLedBeChanged(bool buttonState)
{
  if(lastDebounceTime - millis() <= deboundeDelay)
  {
    return false;
  }
  if(buttonState == HIGH || buttonState == previousButtonState)
  {
    return false;
  }

  return true;
}

void loop(){
  bool buttonState =  digitalRead(BUTTON_TOP);
  if(shouldLedBeChanged(buttonState))
  {
    changeLed();
  }

  if(buttonState != previousButtonState)
  {
    lastDebounceTime = millis();
  }
  previousButtonState = buttonState;
}
