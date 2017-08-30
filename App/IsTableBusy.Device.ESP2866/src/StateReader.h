#include "HttpRequester.h"

class StateReader {
private: 
      HttpRequester *httpRequester;
      char *url;
public:
      StateReader(char* urlParam);
      bool IsBusy();
};
