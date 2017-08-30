#include "HttpRequester.h"

class StateReader {
private: 
      HttpRequester *httpRequester;
public:
      StateReader();
      bool IsBusy();
};
