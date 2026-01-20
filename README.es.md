# Asistente de migración a diversos SGBD
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

## Descripción
<p>
  Esta aplicación, creada utilizando C#, permite a un usuario conectarse a varios Sistemas Gestores de Bases de Datos (SGBD), donde pueden visualizar las bases de datos, 
  tablas, vistas y procedimientos almacenados a los cuales tienen acceso. Es posible ejecutar queries en estos diversos SGBD, debe dar clic sobre el nodo de la conexión que 
  desea utilizar para que el query se ejecute en la conexión correcta, además de migrar una base de datos de un SGBD a otro. La librería en C# "ReglasDeNegocio" contiene 
  clases con los distintos queries o conversiones necesarias para cada SGBD distinto. La conexión a cada SGBD se agrega al selección el servidor, información de inicio de 
  sesión del usuario y la ruta de la base de datos, en el caso de conexiones a Firebase. No necesariamente debe ser una conexión localhost, puede ser una conexión en una red 
  Wi-Fi local, aunque el tiempo de carga es más lento que una conexión por localhost.
</p>

## Funcionalidades del proyecto
- Conectarse a varios SGBD al mismo tiempo
- Cerrar la conexión a un SGBD
- Ejecutar queries
- Migrar bases de datos de un SGBD a otro
- Obtener y ver bases de datos junto con sus tablas, vistas y procedimientos almacenados

## Tecnologías utilizadas
- C#
- Firebird
- Microsoft SQL Server
- MySQL
- PostgreSQL
- Oracle
- Windows Forms App (.NET Framework)
