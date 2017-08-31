#include <Arduino.h>
#include "Button.h"
#include "Light.h"
#include "WifiConnector.h"
#include "ApiClient.h"
#include "UrlPreparer.h"

#define GREEN_LED 16 //D0
#define RED_LED 14 //D5
#define BUTTON_TOP 0 //D3

Configuration* configuration;
bool isBusy = true;
WifiConnector* wifiConnector;
Button* button;
Light* green;
Light* red;
ApiClient *apiClient;
UrlPreparer *urlPreparer;


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


void setup()
{
Serial.begin(9600);
button = new Button(BUTTON_TOP);
green = new Light(GREEN_LED);
red = new Light(RED_LED);
wifiConnector = new WifiConnector();
configuration = new Configuration();
urlPreparer = new UrlPreparer();

wifiConnector->AddConnectionData(configuration -> GetWifiSsid(), configuration -> GetWifiPassword());
char* url = urlPreparer -> PrepareUrl();
apiClient = new ApiClient(url);
}

void loop(){
  bool isClicked = button->IsClicked();
  if(isClicked){
    Serial.println("click");
    bool ran = wifiConnector->Run();
    if(ran){
      isBusy = apiClient -> GetBusy();
      apiClient -> SetBusy(!isBusy);
      applyLed();
    }
    else{
      Serial.println("not-connected");
    }
  }
}
