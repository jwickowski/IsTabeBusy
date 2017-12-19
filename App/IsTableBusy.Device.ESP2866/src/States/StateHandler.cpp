#include "States.h"
#include "StateHandler.h"

StateHandler::StateHandler(Button *button, Light *greenLight, Light *redLight, WifiConnector *wifiConnector, ApiClient *apiClient)
{
  this->currentState = States::Initializing;
  this->button = button;
  this->greenLight = greenLight;
  this->redLight = redLight;
  this->wifiConnector = wifiConnector;
  this->apiClient = apiClient;
}

void StateHandler::handle()
{
  switch (currentState)
  {
  case States::Initializing:
  {
    this->initialize();
    break;
  }
  case States::Registering:
  {
    this->registerDevice();
    break;
  }
  case States::WaitingForClick:
  {
    this->checkClick();
    break;
  }
  case States::UpdatingStatusToBusy:
  {
    this->setIsBusy(true);
    break;
  }
  case States::UpdatingStatusToFree:
  {
    this->setIsBusy(false);
    break;
  }
  }
}

void StateHandler::initialize()
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

void StateHandler::registerDevice()
{
  this->isBusy = apiClient->GetBusy();
  applyLed(isBusy);
  currentState = States::WaitingForClick;
}

void StateHandler::checkClick()
{
  bool isClicked = button->IsClicked();
  if (isClicked)
  {
    if (this->isBusy)
    {
      currentState = States::UpdatingStatusToFree;
    }
    else
    {
      currentState = States::UpdatingStatusToBusy;
    }
  }
}

void StateHandler::setIsBusy(bool newIsBusyValue)
{
  this->isBusy = newIsBusyValue;
  apiClient->SetBusy(isBusy);
  applyLed(isBusy);
  currentState = States::WaitingForClick;
}

void StateHandler::applyLed(bool isBusy)
{
  if (isBusy)
  {
    this->greenLight->Off();
    this->redLight->On();
  }
  else
  {
    this->greenLight->On();
    this->redLight->Off();
  }
}
