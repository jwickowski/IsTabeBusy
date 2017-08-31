#include <SPI.h>
#include <Arduino.h>

class HttpRequester {
public:
      String Get(char* url);
      void Post(char* url,  char* body);
};
