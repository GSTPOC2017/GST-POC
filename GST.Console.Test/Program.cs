using FlatFileConnectorService;
using SAPConnectorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASquare.WindowsTaskScheduler.Models;
using ASquare.WindowsTaskScheduler;

namespace GST.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //This will create Daily trigger to run every 10 minutes for a duration of 18 hours
            SchedulerResponse response = WindowTaskScheduler
                .Configure()
                .CreateTask("SAP Connector", @"C:\Users\aditya.agnihotri\Desktop\GST-POC\SAP.Connector.UI\bin\x64\Debug\SAP.Connector.UI.exe")
                .RunDaily()
                .RunEveryXMinutes(10)
                .RunDurationFor(new TimeSpan(18, 0, 0))
                .SetStartDate(new DateTime(2017, 2, 7))
                .SetStartTime(new TimeSpan(13, 42, 0))
                .Execute();
        }
    }
}
