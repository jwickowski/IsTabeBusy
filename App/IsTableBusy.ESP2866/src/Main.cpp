#include <Arduino.h>

#define LED 04
#define BUTTON_TOP 0

class Button {
private:
      int gpioPin;
      unsigned long debounceDelayInMiliseconds = 50;
      void (*callback)();
      bool previousButtonState;
      unsigned long lastDebounceTime;
      bool shouldLedBeChanged(bool buttonState);
public:
      Button(int gpioPin, void (*aCallback)() );
      void Process();
};

Button::Button( int aGpioPin, void (*aCallback)())
{
  gpioPin = aGpioPin;
  callback = aCallback;
  pinMode(gpioPin, INPUT);
  lastDebounceTime = millis();
  previousButtonState = digitalRead(gpioPin);
}

void Button::Process(){
  bool buttonState =  digitalRead(gpioPin);
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
