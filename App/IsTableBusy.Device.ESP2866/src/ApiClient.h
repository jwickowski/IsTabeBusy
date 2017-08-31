#include "HttpRequester.h"

class ApiClient {
private: 
      HttpRequester *httpRequester;
      char *url;
public:
      ApiClient(char* urlParam);
      bool GetBusy();
      void SetBusy(bool isBusy);
};
