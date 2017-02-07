using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPConnectorService
{
    public class SAPConnector
    {
        public SAPConnector()
        {

        }
        public void GetSAPData()
        {
            string destinationConfigName = "E6D";


            IDestinationConfiguration destinationConfig = null;
            bool destinationIsInitiated = false;
            if (!destinationIsInitiated)
            {
                destinationConfig = new SAPDestinationConfig();
                destinationConfig.GetParameters(destinationConfigName);
                if (RfcDestinationManager.TryGetDestination(destinationConfigName) == null)
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(destinationConfig);
                    destinationIsInitiated = true;
                }
            }

            SAPConnectorInterface sapTest = new SAPConnectorInterface();
            sapTest.TestConnection(destinationConfigName);

            //GetTableByRfcCall("E6D", 100);
        }
        public IRfcTable GetTableByRfcCall(string destName, int rowCount)
        {
            // get the destination 
            RfcDestination dest = RfcDestinationManager.GetDestination(destName);
            // create a function object 
            IRfcFunction func = dest.Repository.CreateFunction("STFC_STRUCTURE");
            //prepare input parameters 
            IRfcStructure impStruct = func.GetStructure("IMPORTSTRUCT");
            impStruct.SetValue("RFCFLOAT", 12345.6789);
            impStruct.SetValue("RFCCHAR1", "A");
            impStruct.SetValue("RFCCHAR2", "AB");
            impStruct.SetValue("RFCCHAR4", "NCO3");
            impStruct.SetValue("RFCINT4", 12345);
            impStruct.SetValue("RFCHEX3", new byte[] { 0x41, 0x42, 0x43 });
            impStruct.SetValue("RFCDATE", DateTime.Today.ToString("yyyy-MM-dd"));
            impStruct.SetValue("RFCDATA1", "Hello World");
            // fill the table parameter 
            IRfcTable rfcTable = func.GetTable("RFCTABLE");
            for (int i = 0; i < rowCount; i++)
            { // make a copy of impStruct 
                IRfcStructure row = (IRfcStructure)impStruct.Clone();
                // make such changes to the fields of the cloned structure                
                impStruct.SetValue("RFCFLOAT", 12345.6789 + i);

                row.SetValue("RFCINT1", i);
                row.SetValue("RFCINT2", i * 2);
                row.SetValue("RFCINT4", i * 4);
                impStruct.SetValue("RFCTIME", string.Empty);//DateTime.Now.ToString());
                row.SetValue("RFCDATA1", i.ToString() + row.GetString("RFCDATA1"));
                rfcTable.Append(row);
            }
            // submit the RFC call            
            func.Invoke(dest);
            // Return the table. The backend has added one more line to it.            
            rfcTable = func.GetTable("RFCTABLE");
            return rfcTable;
        }
    }
}
