using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using System.Configuration;
using System.Web;
using System.Data;

namespace SAPConnectorService
{
   public class SAPConnectorInterface
    {
        private RfcDestination rfcDestination;
        public bool TestConnection(string destinationName)
        {
            bool result = false;
            try
            {

                // Get Destination 
                rfcDestination = RfcDestinationManager.GetDestination(destinationName);

                // IRFC Repository
                RfcRepository repo = rfcDestination.Repository;

                // Function to Get Company Code List in IRFC table Format.
                IRfcFunction testfn = repo.CreateFunction("BAPI_COMPANYCODE_GETLIST");
                    // 
                testfn.Invoke(rfcDestination);

                // Get Company Code
                var companyCodeList = testfn.GetTable("COMPANYCODE_LIST");

                foreach (IRfcStructure row in companyCodeList)
                {
                    for (int element = 0; element < companyCodeList.ElementCount; element++)
                    {
                        RfcElementMetadata metadata = companyCodeList.GetElementMetadata(element);
                        // row.GetString(metadata.Name); // get value from row's field
                    }

                    // You have your data from a row here..
                }

                DataTable dtCompanyCodes = new DataTable();

                // Convert IRFC Data to data Table.
                dtCompanyCodes = CreateDataTable(companyCodeList);

                if (rfcDestination!=null)
                {
                    rfcDestination.Ping();
                    result = true;
                }
            }
            catch(Exception ex)
            {
                result = false;
                throw new Exception("Connection Failure Error : " + ex.Message);
            }
            return result;
        }

        public static DataTable CreateDataTable(IRfcTable rfcTable)
        {
            var dataTable = new DataTable();

            for (int element = 0; element < rfcTable.ElementCount; element++)
            {
                RfcElementMetadata metadata = rfcTable.GetElementMetadata(element);
                dataTable.Columns.Add(metadata.Name);
            }

            foreach (IRfcStructure row in rfcTable)
            {
                DataRow newRow = dataTable.NewRow();
                for (int element = 0; element < rfcTable.ElementCount; element++)
                {
                    RfcElementMetadata metadata = rfcTable.GetElementMetadata(element);
                    newRow[metadata.Name] = row.GetString(metadata.Name);
                }
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }
    }
}
