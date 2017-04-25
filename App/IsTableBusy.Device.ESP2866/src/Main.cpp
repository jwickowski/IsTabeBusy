#include <Arduino.h>
#include "Button.h"
#include "Light.h"

#define GREEN_LED 16
#define RED_LED 14
#define BUTTON_TOP 0

bool state = true;
Button* button;
Light* green;
Light* red;

void changeLed()
{
  state = !state;
  if(state){
    green->On();
    red->Off();
  }
  else{
    green->Off();
    red->On();
  }
}

void setup()
{
button = new Button(BUTTON_TOP);
green = new Light(GREEN_LED);
red = new Light(RED_LED);
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    changeLed();
  }
}
