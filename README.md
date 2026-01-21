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
  This is a robust Database Management & Migration Tool developed in C#. It serves as a centralized hub for developers to interact with multiple Database Management Systems (DBMS) simultaneously. This application features a custom-built migration engine located in the "ReglasDeNegocio" library. This library handles the complex logic of translating queries and data types between different database engines (from Microsoft SQL Server to Firebird, MySQL, PostgreSQL or Oracle). The application supports both localhost and Local Network (Wi-Fi) connections, providing a tree-view explorer to navigate database schemas, including tables, views, and stored procedures.
  <br><br>
  Performance note: There is higher latency when opening a connection through a local network
</p>

## Project Functionalities
### **Universal Connectivity**
- **Multi-Engine Support:** Establish and maintain active connections to several different DBMS concurrently
- **Network Versatility:** Supports local machine connections and remote access over a shared Wi-Fi network

### **Schema Explotarion**
- **Object Explorer:** View a hierarchical tree-view of all accessible databases
- **Metadata Inspection:** Drill down into specific Tables, Views, and Stored Procedures for any active connection

### **Query and Migration Engine**
- **Context-Aware Execution:** Execute queries by selecting the specific connection node needed in the UI
- **Database Migration:** Automates the transfer of entire schemas and data from one DBMS provider to another
- **Business Logic Layer:** Utilizes the "ReglasDeNegocio" library for standardized data conversion and query translation across platforms

## Technologies used
- C#
- Firebird
- Microsoft SQL Server
- MySQL
- PostgreSQL
- Oracle
- Windows Forms App (.NET Framework)
