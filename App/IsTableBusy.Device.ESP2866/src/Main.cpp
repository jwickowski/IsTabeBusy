#include <Arduino.h>
#include <QueueArray.h>
#include "Device/Button.h"
#include "Device/Light.h"
#include "Device/WifiConnector.h"

#include "Api/ApiClient.h"
#include "Api/UrlPreparer.h"

#include "States/States.h"

#define GREEN_LED 16 //D0
#define RED_LED 14   //D5
#define BUTTON_TOP 0 //D3

Button *button;
Configuration *configuration;
Light *green;
Light *red;
ApiClient *apiClient;
UrlPreparer *urlPreparer;
WifiConnector *wifiConnector;
States currentState = States::Setuping;
bool isBusy = true;

void applyLed(bool isBusy)
{
  if (isBusy)
  {
    green->Off();
    red->On();
  }
  else
  {
    green->On();
    red->Off();
  }
}

void setup()
{
  Serial.begin(9600);
  button = new Button(BUTTON_TOP);
  green = new Light(GREEN_LED);
  red = new Light(RED_LED);

  configuration = new Configuration();
  urlPreparer = new UrlPreparer();
  wifiConnector->AddConnectionData(configuration->GetWifiSsid(), configuration->GetWifiPassword());
  char *url = urlPreparer->PrepareUrl();
  apiClient = new ApiClient(url);
  currentState = States::Initializing;
}

void Initialize()
{
  bool ran = wifiConnector->Run();
  if (ran)
  {
    currentState = States::Registering;
  }
  else
  {
    delay(1000);
  }
}

void Register()
{
  isBusy = apiClient->GetBusy();
  applyLed(isBusy);
  currentState = States::WaitingForClick;
}

void CheckClick()
{
  bool isClicked = button->IsClicked();
  if (isClicked)
  {
    if (isBusy)
    {
      currentState = States::UpdatingStatusToFree;
    }
    else
    {
      currentState = States::UpdatingStatusToBusy;
    }
  }
}

void SetIsBusy(bool newIsBusyValue)
{
  isBusy = newIsBusyValue;
  apiClient->SetBusy(isBusy);
  applyLed(isBusy);
  currentState = States::WaitingForClick;
}

void loop()
{
  switch (currentState)
  {
  case States::Initializing:
  {
    Initialize();
    break;
  }
  case States::Registering:
  {
    Register();
    break;
  }
  case States::WaitingForClick:
  {
    CheckClick();
    break;
  }
  case States::UpdatingStatusToBusy:
  {
    SetIsBusy(true);
    break;
  }
  case States::UpdatingStatusToFree:
  {
    SetIsBusy(false);
    break;
  }
  }
}
