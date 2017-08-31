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
  JsonObject &root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}

void ApiClient::SetBusy(bool isBusy)
{
  JsonObject &root = jsonBuffer.createObject();
  root["isBusy"] = isBusy;
  char *output = (char *)malloc(100);

  root.prettyPrintTo(output, 100);
  jsonBuffer.clear();
  httpRequester->Post(url, output);
}
