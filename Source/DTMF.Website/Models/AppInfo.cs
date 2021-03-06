﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DTMF.Models
{
    public class AppInfo
    {
        public AppInfo()
        {
            BuildOutputDatabases = new List<DatabaseInfo>();
            DestinationPaths = new List<string>();
        }

        [DisplayName("Application Name")]
        public string AppName { get; set; }
        public string BuildOutputBasePath { get; set; }
        public string BuildOutputRelativeWebPath { get; set; }
        public List<DatabaseInfo> BuildOutputDatabases { get; set; }
        public List<string> DestinationPaths { get; set; }
        public string Powershell { get; set; }

        private string _lastDeployed;
        [DisplayName("Deployed")]
        public string LastDeployed
        {
            get
            {
                if (string.IsNullOrEmpty(_lastDeployed)) return "Never";
                return _lastDeployed;
            }
            set { _lastDeployed = value; }
        }

        public string PendingRequest { get; set; }
        public string RobocopyExcludedFiles { get; set; }
        public string RobocopyExcludedFolders { get; set; }

        [DisplayName("Fast App Offline")]
        public bool FastAppOffine { get; set; }
        public string HipChatRoomID { get; set; }
    }
}