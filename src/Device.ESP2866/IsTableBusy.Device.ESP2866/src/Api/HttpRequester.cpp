#include <ESP8266HTTPClient.h>
#include "HttpRequester.h"

String HttpRequester::Get(char* url){
  String payload = "";
    HTTPClient http;

  http.begin(url);

  int httpCode = http.GET();

  if(httpCode > 0) {
      if(httpCode == HTTP_CODE_OK) {
          payload = http.getString();
      }
  }

  http.end();
  return payload;
}


void HttpRequester::Post(char* url, char* body){
  HTTPClient http;
  http.begin(url);
  http.addHeader("Content-Type", "application/json");

  int httpCode = http.POST(body);
  http.end();
}