#include <Arduino.h>

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
