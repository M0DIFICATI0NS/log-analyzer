# About The Project 
**PI System Log Analyzer** is designed to analyze a large dataset of user login records, which aims to identify inactive users from PI System, and report the result to a formatted .CSV file. The process involves reading, processing, and querying data from both an extracted CSV file from PI Data Archive with command-line utility .\pigetmsg and Active Directory to retrieve an employees' [displayName], [email], [manager] through LDAP IP Protocol. The purpose of the project is to ensure the security and keep the PI System environment clean.

# Built With
* [C#]
* [.NET Framework]
* [OSIsoft PI]

# Getting Started
To obtain a local copy of the repository and set it up, please follow these steps:

### Software Dependencies
* Install .NET SDK 8.0
* Install C# Dev Kit Extension

### Clone the Repository
* Open your terminal or command prompt.
* Execute the following command, replacing URL with the repository’s URL:
  1. Clone the Repository:
     ```
     git clone **Confidential**
     ```
  2. Install dependencies:
     ```
     dotnet add package System.DirectoryServices --version 8.0.0
     dotnet add package System.DirectoryServices.AccountManagement --version 8.0.0
     ```
  4. Ensure the local path for both exporting and analyzing, as well as the API:

     https://github.com/sultanjke/log-analyzer/blob/85d25ae50bc0f28b9368b72237fa188d2d44dd65/ExportMessageLog.cs#L11
     https://github.com/sultanjke/log-analyzer/blob/85d25ae50bc0f28b9368b72237fa188d2d44dd65/ExportMessageLog.cs#L19
     https://github.com/sultanjke/log-analyzer/blob/85d25ae50bc0f28b9368b72237fa188d2d44dd65/ExportMessageLog.cs#L21
     https://github.com/sultanjke/log-analyzer/blob/85d25ae50bc0f28b9368b72237fa188d2d44dd65/AnalyzeMessageLog.cs#L14
     https://github.com/sultanjke/log-analyzer/blob/85d25ae50bc0f28b9368b72237fa188d2d44dd65/AnalyzeMessageLog.cs#L75
     
  3. Run the Project:
     ```
     dotnet run
     ```

# Overview
MessageLog.csv - user log-ins data retrieved from .\pigetmsg command-line utility with 100k+ records;
InactiveUsers.csv - script's report based on **MessageLog.csv**

<img width="100" alt="image" src="https://github.com/user-attachments/assets/75fb2a7b-0d4f-4638-a332-0bafa0a2f632">



# Contribute
Any contributions you make are **greatly appreciated**.

# Contact
[LinkedIn](www.linkedin.com/in/sultan-mecheyev-3b459a328)
