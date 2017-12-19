#include "Command.h"
#include "Device/WifiConnector.h"

class ConnectToWifiCommand : public Command
{
	private: 
	 WifiConnector *wifiConnector;
public:
ConnectToWifiCommand(char* ssid, char* password){
 	wifiConnector = new WifiConnector();
 	wifiConnector->AddConnectionData(ssid,password);
	}

	int Execute()
	{	
		 bool ran = wifiConnector->Run();
		 if(ran){
			 return 0;
		 }
		 return 1;
	}
};


