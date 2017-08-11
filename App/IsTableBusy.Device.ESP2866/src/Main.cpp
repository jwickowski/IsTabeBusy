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
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    changeLed();
    bool ran = wifiConnector->Run();
    if(ran){
      Serial.println("connected");
      String payload = httpRequester->Get("http://bot.whatismyipaddress.com/");
      Serial.println("got data");
      Serial.println(payload);

    }
    else{
      Serial.println("not-connected");
    }
  }
}
