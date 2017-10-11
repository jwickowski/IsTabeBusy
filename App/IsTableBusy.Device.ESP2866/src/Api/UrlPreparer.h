#include "Configuration.h"

class UrlPreparer {
private: 
      Configuration *configuration;
public:
      UrlPreparer();
      char *PrepareUrl();
};
