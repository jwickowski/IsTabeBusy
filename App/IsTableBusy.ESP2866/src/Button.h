class Button {
private:
      int gpioPin;

      unsigned long debounceDelayInMiliseconds;
      bool previousButtonState;
      unsigned long lastDebounceTime;
      bool IsClicked(bool buttonState);
public:
      Button(int gpioPin);
      bool IsClicked();
};
