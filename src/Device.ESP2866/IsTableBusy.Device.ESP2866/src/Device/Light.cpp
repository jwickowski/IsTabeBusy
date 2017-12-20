#include <Arduino.h>
#include "Light.h"

Light::Light(int aGpioPin){
  gpioPin = aGpioPin;

  pinMode(gpioPin, OUTPUT);
}

void Light::On(){
  digitalWrite(gpioPin, HIGH);
}

void Light::Off(){
  digitalWrite(gpioPin, LOW);
}
