namespace LogAnalyzer
{
    public class ADUser
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Supervisor { get; set; }
        public string Email { get; set; }

        public ADUser(string userID, string fullName, string supervisor, string email)
        {
            UserID = userID;
            FullName = fullName;
            Supervisor = supervisor;
            Email = email;
        }
    }
}



// This file contains the ADUser class, which represents a user in Active Directory (AD). It includes properties for storing user information such as UserID, FullName, Supervisor, and Email.