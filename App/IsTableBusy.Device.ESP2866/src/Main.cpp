#include <Arduino.h>
#include "Button.h"
#include "Light.h"
#include "WifiConnector.h"
#include "HttpRequester.h"
#include "Configuration.h"
#include "ArduinoJson.h"
#include "StateReader.h"

#define GREEN_LED 16 //D0
#define RED_LED 14 //D5
#define BUTTON_TOP 0 //D3

Configuration* configuration;
bool isBusy = true;
WifiConnector* wifiConnector;
HttpRequester* httpRequester;
Button* button;
Light* green;
Light* red;
StateReader *stateReader;
char* url;

StaticJsonBuffer<200> jsonBuffer;

void applyLed()
{
  Serial.println("applying LED");
  Serial.println(isBusy);
  Serial.println("---");
  if(isBusy){
    green->Off();
    red->On();
  }
  else{
    green->On();
    red->Off();
  }
}
void setUrl(){
  char* apiUrl = configuration -> GetApiUrl();
  char* device = "/device/";
  char* deviceId = configuration -> GetDeviceId();
  char* state =  "/state";
  int lenght = 1 + strlen(apiUrl) + strlen(device) + strlen(deviceId) + strlen(state);

  url = (char *) malloc(lenght);

  strcpy(url, apiUrl);
  strcat(url, device);
  strcat(url, deviceId);
  strcat(url, state);
  Serial.println(url);
}

void setup()
{
Serial.begin(9600);
button = new Button(BUTTON_TOP);
green = new Light(GREEN_LED);
red = new Light(RED_LED);
wifiConnector = new WifiConnector();
configuration = new Configuration();
wifiConnector->AddConnectionData(configuration -> GetWifiSsid(), configuration -> GetWifiPassword());
httpRequester = new HttpRequester();
stateReader = new StateReader();
setUrl();
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    Serial.println("click");
    bool ran = wifiConnector->Run();
    if(ran){
      isBusy = stateReader -> IsBusy();
      applyLed();
    }
    else{
      Serial.println("not-connected");
    }
  }
}
