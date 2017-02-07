using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace FlatFileConnectorService
{
    public class FlatFileConnector
    {
        // To Do Get CSV File Extract Here.

        public FlatFileConnector()
        {
            string[] cols;
            string[] rows;

            StreamReader sr = new StreamReader(@"C:\Users\aditya.agnihotri\Desktop\GST DOC\GSTFlatFile.csv");  //SOURCE FILE
            StreamWriter sw = new StreamWriter(@"C:\Users\aditya.agnihotri\Desktop\GST DOC\GSTFlatFile.json");  // DESTINATION FILE

            string line = sr.ReadLine();


            cols = Regex.Split(line, ",");

            DataTable table = new DataTable();
            for (int i = 0; i < cols.Length; i++)
            {
                table.Columns.Add(cols[i], typeof(string));
            }
            while ((line = sr.ReadLine()) != null)
            {
                table.Rows.Clear();

                int i;
                string row = string.Empty;
                rows = Regex.Split(line, ",");
                DataRow dr = table.NewRow();

                for (i = 0; i < rows.Length; i++)
                {
                    dr[i] = rows[i];

                }
                table.Rows.Add(dr);

                string json = JsonConvert.SerializeObject(table, Formatting.Indented);
                sw.Write(json);
            }


            sw.Close();
            sr.Close();


        }
    }
}
