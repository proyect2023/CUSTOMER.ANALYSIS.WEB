
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities
{
    public static class Crypto
    {
        public static string CifrarId(long Id, string modulo = "=", string key = DomainConstants.ENCRIPTA_KEY, string salt = "SISINV")
        {
            try
            {
                string stringToEncrypt = Id.ToString();
                string data = GSCrypto.CifrarClave(stringToEncrypt, key);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    return data.Replace("/", "SIFM").Replace("+", salt).Replace("=", modulo);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string CifrarId(string Id, string modulo = "=", string key = DomainConstants.ENCRIPTA_KEY, string salt = "SISINV" )
        {
            try
            {
                string stringToEncrypt = Id.ToString();
                string data = GSCrypto.CifrarClave(stringToEncrypt, key);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    return data.Replace("/", "SIFM").Replace("+", salt).Replace("=", modulo);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string CifrarId(int Id, string modulo = "=", string key = DomainConstants.ENCRIPTA_KEY, string salt = "SISINV")
        {
            try
            {
                string stringToEncrypt = Id.ToString();
                string data = GSCrypto.CifrarClave(stringToEncrypt, key);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    return data.Replace("/", "SIFM").Replace("+", salt).Replace("=", modulo);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string DescifrarId(string? encryptedString, string modulo = "=", string key = DomainConstants.ENCRIPTA_KEY, string salt = "SISINV")
        {
            if (string.IsNullOrEmpty(encryptedString))
            {
                encryptedString = "";
            }

            encryptedString = encryptedString.Replace("SIFM", "/").Replace(salt, "+").Replace(modulo, "=");

            var result = GSCrypto.DescifrarClave(encryptedString, key);
            if (string.IsNullOrEmpty(result))
            {
                return "0";
            }

            return result;
        }

        
    }
}
