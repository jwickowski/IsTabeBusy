#include <Arduino.h>
#include <QueueArray.h>
#include "Device/Button.h"
#include "Device/Light.h"

#include "Api/ApiClient.h"
#include "Api/UrlPreparer.h"
#include "State.h"
#include "Command.h"
#include "Commands/ConnectToWifiCommand.h"

#define GREEN_LED 16 //D0
#define RED_LED 14 //D5
#define BUTTON_TOP 0 //D3


//QueueArray <Command> queue;
Configuration* configuration;
bool isBusy = true;

Button* button;
Light* green;
Light* red;
ApiClient *apiClient;
UrlPreparer *urlPreparer;
State currentState = State::initializing;
ConnectToWifiCommand *connectToWifiCommand;
void applyLed()
{
  if(isBusy){
    green->Off();
    red->On();
  }
  else{
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
connectToWifiCommand = new ConnectToWifiCommand(configuration -> GetWifiSsid(), configuration -> GetWifiPassword());
  char* url = urlPreparer -> PrepareUrl();
  apiClient = new ApiClient(url);
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    Serial.println("click");
    int ran = connectToWifiCommand->Execute();
    if(ran == 0){
      isBusy = apiClient -> GetBusy();
      apiClient -> SetBusy(!isBusy);
      applyLed();
    }
    else{
      Serial.println("not-connected");
    }
  }
}
