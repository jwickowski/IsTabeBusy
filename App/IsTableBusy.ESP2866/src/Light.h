class Light
{
private:
  int gpioPin;

public:
  Light(int aGpioPin);
  void On();
  void Off();
};
