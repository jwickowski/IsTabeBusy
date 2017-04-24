#include <Arduino.h>

class Button {
private:
      int gpioPin;
      void (*callback)();

      unsigned long debounceDelayInMiliseconds;
      bool previousButtonState;
      unsigned long lastDebounceTime;
      bool shouldLedBeChanged(bool buttonState);
public:
      Button(int gpioPin, void (*aCallback)());
      void Process();
};
