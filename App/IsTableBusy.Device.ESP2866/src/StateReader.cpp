#include "StateReader.h"
#include <Arduino.h>
#include "ArduinoJson.h"

StaticJsonBuffer<200> jsonBuffer;

StateReader::StateReader(char *urlParam){
httpRequester = new HttpRequester();
url = urlParam;
}

bool StateReader::IsBusy()
{
  String payload = httpRequester -> Get(url);
  Serial.println("payload:");
  Serial.println(payload);

  JsonObject& root = jsonBuffer.parseObject(payload);
  bool isBusy = root["isBusy"];
  jsonBuffer.clear();
  return isBusy;
}
