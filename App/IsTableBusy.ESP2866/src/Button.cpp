#include <Arduino.h>
#include "Button.h"


Button::Button( int aGpioPin, void (*aCallback)())
{
  gpioPin = aGpioPin;
  callback = aCallback;

  debounceDelayInMiliseconds = 50;
  pinMode(gpioPin, INPUT);

  lastDebounceTime = millis();
  previousButtonState = digitalRead(gpioPin);
}

void Button::Process(){
  bool buttonState = digitalRead(gpioPin);
  if(shouldLedBeChanged(buttonState))
  {
    callback();
  }

  if(buttonState != previousButtonState)
  {
    lastDebounceTime = millis();
  }
  previousButtonState = buttonState;
}

bool Button::shouldLedBeChanged(bool buttonState){
  if(lastDebounceTime - millis() <= debounceDelayInMiliseconds)
  {
    return false;
  }

  if(buttonState == HIGH || buttonState == previousButtonState)
  {
    return false;
  }

  return true;
}
