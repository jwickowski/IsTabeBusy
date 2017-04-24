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

Button button = Button(BUTTON_TOP, changeLed);

void setup()
{

}

void loop(){
  button.Process();
}
