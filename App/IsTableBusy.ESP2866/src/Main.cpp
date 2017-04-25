#include <Arduino.h>
#include "Button.h"
#include "Light.h"

#define GREEN_LED 04
#define BUTTON_TOP 0

bool state = true;
Light green = Light(GREEN_LED);

void changeLed()
{
  state = !state;
  if(state){
    green.On();
  }
  else{
    green.Off();
  }
}

Button* button;

void setup()
{
button = new Button(BUTTON_TOP);
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    changeLed();
  }
}
