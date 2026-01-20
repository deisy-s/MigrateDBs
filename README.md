# Migration assistant to various DBMS
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
<a href="https://docs.mindsdb.com/data-integrations/firebird"><img src="https://img.shields.io/badge/Firebird-F40D12?logo=Firebird&logoColor=fff&style=for-the-badge" alt="Firebird Badge"></a>
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![MySQL](https://img.shields.io/badge/mysql-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)
![Oracle](https://img.shields.io/badge/Oracle-F80000?style=for-the-badge&logo=oracle&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
---

<div style="text-align: right;">
  <a href="README.md">English</a> | <a href="README.es.md">Español</a>
</div>

## Description
<p>
  This application, created using C#, allows a user to connect to several Database Management Systems (DBMS), where they can view the databases, tables, views, and stored 
  procedures that they have access to. It is possible to execute queries on these different DBMS; you must click on the node of the connection you wish to utilize so the 
  query is run on the correct connection. Additionally, the application allows migrating a database from one DBMS to another. The C# library "ReglasDeNegocio" contains 
  classes with the various queries or conversions necessary for each different DBMS. The connection to each DBMS is added by selecting the server, the user's login 
  information, and database path in the case of Firebase connections. It doesn't necessarily have to be a localhost connection, it can be a connection on a local Wi-Fi 
  network, although the loading time is slower than a localhost connection.
</p>

## Project Functionalities
- Connect to several DBMS at the same time
- Close the connection to a DBMS
- Execute queries
- Migrate a database from one DBMS to another
- View databases with their tables, views, and stored procedures

## Technologies used
- C#
- Firebird
- Microsoft SQL Server
- MySQL
- PostgreSQL
- Oracle
- Windows Forms App (.NET Framework)
