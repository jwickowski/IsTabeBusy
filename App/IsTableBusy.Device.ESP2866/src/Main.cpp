#include <Arduino.h>

#include "States/StateHandler.h"

#define GREEN_LED 16 //D0
#define RED_LED 14   //D5
#define BUTTON_TOP 0 //D3

StateHandler *stateHandler;

void setup()
{
  Serial.begin(9600);
  Button *button = new Button(BUTTON_TOP);
  Light *green = new Light(GREEN_LED);
  Light *red = new Light(RED_LED);

  Configuration *configuration = new Configuration();
  WifiConnector *wifiConnector = new WifiConnector();
  wifiConnector->AddConnectionData(configuration->GetWifiSsid(), configuration->GetWifiPassword());
  UrlPreparer *urlPreparer = new UrlPreparer();
  char *url = urlPreparer->PrepareUrl();
  ApiClient *apiClient = new ApiClient(url);
  stateHandler = new StateHandler(button, green, red, wifiConnector, apiClient);
}

void loop()
{
  stateHandler->handle();
}
