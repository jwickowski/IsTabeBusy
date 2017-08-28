#include <Arduino.h>
#include "Button.h"
#include "Light.h"
#include "WifiConnector.h"
#include "HttpRequester.h"
#include "Configuration.h"
#define GREEN_LED 16 //D0
#define RED_LED 14 //D5
#define BUTTON_TOP 0 //D3


Configuration* configuration;
bool state = true;
WifiConnector* wifiConnector;
HttpRequester* httpRequester;
Button* button;
Light* green;
Light* red;
char* url;

void changeLed()
{
  Serial.println("changing LED");
  state = !state;
  if(state){
    green->On();
    red->Off();
  }
  else{
    green->Off();
    red->On();
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
setUrl();
}



bool getBusy()
{
  String payload = httpRequester->Get(url);
  Serial.println("payload:");
  Serial.println(payload);
  return false;
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    
    changeLed();
    bool ran = wifiConnector->Run();
    if(ran){
      state = getBusy();
      Serial.println(state);
    }
    else{
      Serial.println("not-connected");
    }
  }
}
