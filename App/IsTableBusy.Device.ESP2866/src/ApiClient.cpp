#include "ApiClient.h"
#include <Arduino.h>
#include "ArduinoJson.h"

StaticJsonBuffer<200> jsonBuffer;

ApiClient::ApiClient(char *urlParam){
httpRequester = new HttpRequester();
url = urlParam;
}

bool ApiClient::GetBusy()
{
  String payload = httpRequester -> Get(url);
  Serial.println("payload:");
  Serial.println(payload);

  JsonObject& root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}
