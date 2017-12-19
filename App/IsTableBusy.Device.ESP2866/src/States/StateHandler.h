#include "Device/Button.h"
#include "Device/Light.h"
#include "Device/WifiConnector.h"

#include "Api/ApiClient.h"
#include "Api/UrlPreparer.h"
#include "States.h"

class StateHandler
{
  public:
    StateHandler(Button *button, Light *greenLight, Light *redLight, WifiConnector *wifiConnector, ApiClient *apiClient);
    void handle();

  private:
    bool isBusy;
    States currentState;
    Button *button;
    Light *greenLight;
    Light *redLight;
    ApiClient *apiClient;
    WifiConnector *wifiConnector;
    void initialize();
    void registerDevice();
    void checkClick();
    void setIsBusy(bool isBusy);
    void applyLed(bool isBusy);
};