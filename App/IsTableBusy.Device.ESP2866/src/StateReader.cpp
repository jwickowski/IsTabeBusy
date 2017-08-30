#include "StateReader.h"

bool StateReader::IsBusy()
{
  String payload = httpRequester->Get(url);
  Serial.println("payload:");
  Serial.println(payload);

  JsonObject& root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}
