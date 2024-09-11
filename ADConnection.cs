using System;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace LogAnalyzer
{
    public class ADConnection
    {
        private readonly string ldapDomain;

        public ADConnection(string ldapDomain)
        {
            this.ldapDomain = ldapDomain;
        }

        public ADUser GetUserByUserID(string userID)
        {
            try
            {
                userID = userID.Replace("!", "");

                using (var de = new DirectoryEntry(ldapDomain))
                using (var ds = new DirectorySearcher(de)
                {
                    Filter = $"(&(objectClass=user)(sAMAccountName={userID}))"
                })
                {
                    ds.PropertiesToLoad.Add("displayName");
                    ds.PropertiesToLoad.Add("manager");
                    ds.PropertiesToLoad.Add("mail");

                    var result = ds.FindOne();

                    if (result != null)
                    {
                        string fullName = result.Properties["displayName"][0].ToString();
                        string supervisorFullName = result.Properties.Contains("manager")
                            ? GetSupervisorFullName(result.Properties["manager"][0].ToString())
                            : null;
                        string email = result.Properties.Contains("mail")
                            ? result.Properties["mail"][0].ToString()
                            : null;

                        return new ADUser(userID, fullName, supervisorFullName, email);
                    }
                    else
                    {
                        Console.WriteLine($"User not found: {userID}");
                        return null;
                    }
                }
            }
            catch (COMException comEx)
            {
                Console.WriteLine($"COMException for user {userID}: {comEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception for user {userID}: {ex.Message}");
                return null;
            }
        }

        private string GetSupervisorFullName(string supervisorDN)
        {
            try
            {
                using (var de = new DirectoryEntry(ldapDomain))
                using (var ds = new DirectorySearcher(de)
                {
                    Filter = $"(&(objectClass=user)(distinguishedName={supervisorDN}))"
                })
                {
                    ds.PropertiesToLoad.Add("displayName");

                    var result = ds.FindOne();

                    return result != null ? result.Properties["displayName"][0].ToString() : "Supervisor not found.";
                }
            }
            catch (COMException comEx)
            {
                Console.WriteLine($"COMException: {comEx.Message}");
                return "Error retrieving supervisor.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return "Error retrieving supervisor.";
            }
        }
    }
}



// This file contains the ADConnection class, which handles the connection to Active Directory and retrieves user information. It includes methods for fetching user details and supervisor details from AD.