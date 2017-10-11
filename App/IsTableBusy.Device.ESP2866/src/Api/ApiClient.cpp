#include "ApiClient.h"
#include <Arduino.h>
#include "ArduinoJson.h"

ApiClient::ApiClient(char *urlParam)
{
  httpRequester = new HttpRequester();
  url = urlParam;
}

bool ApiClient::GetBusy()
{
  StaticJsonBuffer<200> jsonBuffer;
  String payload = httpRequester->Get(url);
  JsonObject &root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}

void ApiClient::SetBusy(bool isBusy)
{
  StaticJsonBuffer<200> jsonBuffer;
  JsonObject &root = jsonBuffer.createObject();
  root["isBusy"] = isBusy;
  char *output = (char *)malloc(100);

  root.prettyPrintTo(output, 100);
  jsonBuffer.clear();
  httpRequester->Post(url, output);
}
