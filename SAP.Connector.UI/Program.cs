using SAPConnectorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAP.Connector.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            SAPConnector sapConnector = new SAPConnector();
            sapConnector.GetSAPData();
            MessageBox.Show("Welcome TO SAP Connector_GST1111");
        }
    }
}
