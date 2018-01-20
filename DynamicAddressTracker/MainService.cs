using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAddressTracker
{
    public partial class MainService : ServiceBase
    {
        //Variables for the service
        private string configFile = "config.txt";
        private string logLocation = " "; //TODO: set this to Microsoft's default DHCP log location

        public MainService()
        {
            InitializeComponent();
        }

        //Main operation on service start
        protected override void OnStart(string[] args)
        {
            //read config for file location (false means an error occured reading the config file)
            if (!readConfig(configFile))
            {
                //write error to application log
                //stop service
            }
            //setup configuration service (so that a front end application can update the configuration)
            //setup file monitor for DHCP log
        }

        //Main operation on service stop...primarily cleanup.
        protected override void OnStop()
        {
            //close file reader
            //save any settings changes to the config file
        }

        //Read and load the configuration file parameters
        private bool readConfig(string filename)
        {
            try
            {
                string fileContents = File.ReadAllText(filename);
                for (string line in fileContents)
                {
                    switch (line.Substring(0,line.IndexOf(' ')))
                    {
                        case "logType":
                            break;
                        case "logLocation":
                            logLocation = line.Substring(line.IndexOf('='), line.Length - line.IndexOf('=')).Trim();
                            break;
                        default:
                            //ignore any non defined lines
                            break;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //write log

        //update records in a database type file (mac address, dns name, IP, first seen, lease ended)

    }
}
