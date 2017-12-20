#include "WifiConnector.h"
#include "ESP8266WiFiMulti.h"

ESP8266WiFiMulti WiFiMulti;
void WifiConnector::AddConnectionData(char *ssid, char *password)
{
  WiFiMulti.addAP(ssid, password);
}
bool WifiConnector::Run()
{
  int status = WiFiMulti.run();
  if ((status == WL_CONNECTED))
  {
    return true;
  }
  return false;
}
