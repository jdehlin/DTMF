﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Web.Configuration;

namespace DTMF.Models.Authentication
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

        public bool HasValidUsernameAndPassword
        {
            get
            {
                return ValidateLogin(Username, Password);
            }
        }

        public bool ValidateLogin(string userName, string pwd)
        {
            //always return true when active directory domain is empty
            if (WebConfigurationManager.AppSettings["ActiveDirectoryPath"] == "")
                return true;
            try
            {
                var enTry = new DirectoryEntry(WebConfigurationManager.AppSettings["ActiveDirectoryPath"],userName, pwd, AuthenticationTypes.Secure & AuthenticationTypes.FastBind);
                var mySearcher = new DirectorySearcher(enTry) { Filter = "(&(objectClass=*)(samAccountName=" + userName + "))" };
                mySearcher.PropertiesToLoad.Add("samAccountName");
                var result = mySearcher.FindOne();
                if ((result == null)) return false;

                var ldapResult = result.Properties["samAccountName"][0].ToString();
                return !string.IsNullOrEmpty(ldapResult);
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}