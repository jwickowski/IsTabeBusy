#include <Arduino.h>


class Button {
private:
      int gpioPin;
      unsigned long debounceDelayInMiliseconds;
      void (*callback)();

      bool previousButtonState;
      unsigned long lastDebounceTime;
      bool shouldLedBeChanged(bool buttonState){
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
public:
      Button(int gpioPin, unsigned long debounceDelayInMiliseconds, void (*aCallback)() );
      void Process();
};

Button::Button( int aGpioPin, unsigned long aDebounceDelayInMiliseconds, void (*aCallback)())
{
  gpioPin = aGpioPin;
  debounceDelayInMiliseconds = aDebounceDelayInMiliseconds;
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


#define LED 04
#define BUTTON_TOP 0

Button* button;

bool ledState;

void changeLed(){
  ledState = !ledState;
  digitalWrite(LED, ledState);
}
void setup()
{
  ledState = LOW;
  pinMode(LED, OUTPUT);
  pinMode(BUTTON_TOP, INPUT);
  digitalWrite(LED, ledState);
  Button* button = new Button(BUTTON_TOP, 50, changeLed);
}


void loop(){
button -> Process();
}
