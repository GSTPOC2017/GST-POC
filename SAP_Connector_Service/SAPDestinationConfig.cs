using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using System.Configuration;
using System.Web;

namespace SAPConnectorService
{
    public class SAPDestinationConfig : IDestinationConfiguration
    {

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.Name, "E6D");
            parms.Add(RfcConfigParameters.AppServerHost, "sapbrillio.cloudapp.net");// ConfigurationManager.AppSettings["SAP_APPSERVERHOST"]);
            parms.Add(RfcConfigParameters.User,          "CHANDRAS");// ConfigurationManager.AppSettings["SAP_USERNAME"]);
            parms.Add(RfcConfigParameters.Password, "init123");// ConfigurationManager.AppSettings["SAP_PASSWORD"]);
            parms.Add(RfcConfigParameters.Client, "100");//ConfigurationManager.AppSettings["SAP_CLIENT"]);
            parms.Add(RfcConfigParameters.SystemNumber,  "0");//ConfigurationManager.AppSettings["SAP_SYSTEMNUM"]);
            parms.Add(RfcConfigParameters.Language,      "EN");//ConfigurationManager.AppSettings["SAP_LANGUAGE"]);
            parms.Add(RfcConfigParameters.PoolSize,      "10");//ConfigurationManager.AppSettings["SAP_POOLSIZE"]);
            return parms;
        }

        public bool ChangeEventsSupported()
        {
            return false;
        }
    }
}
