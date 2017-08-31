#include "ApiClient.h"
#include <Arduino.h>
#include "ArduinoJson.h"

StaticJsonBuffer<200> jsonBuffer;

ApiClient::ApiClient(char *urlParam)
{
  httpRequester = new HttpRequester();
  url = urlParam;
}

bool ApiClient::GetBusy()
{
  String payload = httpRequester->Get(url);
  Serial.println("payload:");
  Serial.println(payload);

  JsonObject &root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}

void ApiClient::SetBusy(bool isBusy)
{
  Serial.println("Set BUSY : ");
  Serial.println(isBusy);
  JsonObject &root = jsonBuffer.createObject();
  root["isBusy"] = isBusy ? "true" : "false";
  char *output = (char *)malloc(100);

  root.prettyPrintTo(output, 100);

  Serial.println("output");
  Serial.println(output);
}
