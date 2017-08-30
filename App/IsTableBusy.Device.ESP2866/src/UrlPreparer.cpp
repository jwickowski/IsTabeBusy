#include <Arduino.h>
#include "UrlPreparer.h"

UrlPreparer::UrlPreparer(){
  configuration = new Configuration();
}

char *UrlPreparer::PrepareUrl()
{
  char* apiUrl = configuration -> GetApiUrl();
  char* device = "/device/";
  char* deviceId = configuration -> GetDeviceId();
  char* state =  "/state";
  int lenght = 1 + strlen(apiUrl) + strlen(device) + strlen(deviceId) + strlen(state);

  char *url = (char *) malloc(lenght);

  strcpy(url, apiUrl);
  strcat(url, device);
  strcat(url, deviceId);
  strcat(url, state);
  return url;
}


