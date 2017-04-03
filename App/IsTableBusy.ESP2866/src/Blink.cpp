#include <Arduino.h>

#define LED 04
#define BUTTON_TOP 0
bool previousButtonState;
bool previousLedState;
void setup()
{
  pinMode(LED, OUTPUT);
  pinMode(BUTTON_TOP, INPUT);
  previousButtonState = digitalRead(BUTTON_TOP);
  digitalWrite(LED, LOW);
  previousLedState = LOW;
}

void loop()
{
  bool buttonState =  digitalRead(BUTTON_TOP);
  if(previousButtonState == LOW && buttonState == HIGH)
  {
    if(previousLedState == HIGH){
      digitalWrite(LED, LOW);
      previousLedState = LOW;
    }
    else{
      digitalWrite(LED, HIGH);
      previousLedState = HIGH;
    }
  }
  previousButtonState = buttonState;
}
