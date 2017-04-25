#include <Arduino.h>
#include "Button.h"

Button::Button(int aGpioPin)
{
  gpioPin = aGpioPin;

  debounceDelayInMiliseconds = 100;
  pinMode(gpioPin, INPUT);

  lastDebounceTime = millis();
  previousButtonState = digitalRead(gpioPin);
}

bool Button::IsClicked(){
  bool buttonState = digitalRead(gpioPin);
  bool isClicked = IsClicked(buttonState);

  if(buttonState != previousButtonState)
  {
    lastDebounceTime = millis();
  }
  previousButtonState = buttonState;
  return isClicked;
}

bool Button::IsClicked(bool buttonState){
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
