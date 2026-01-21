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
  Esta es una herramienta robusta de gestión y migración de bases de datos desarrollada en C#. Sirve como un hub central para que los desarrolladores interactúen con múltiples sistemas de gestión de bases de datos (SGBD) simultáneamente. Esta aplicación cuenta con un motor de migración personalizado ubicado en la biblioteca "ReglasDeNegocio". Esta biblioteca gestiona la lógica de la traducción de consultas y tipos de datos entre diferentes motores de bases de datos (de Microsoft SQL Server hacia Firebird, MySQL, PostgreSQL u Oracle). La aplicación admite conexiones locales y de red local (Wi-Fi), y proporciona un explorador de vista de árbol para navegar por los esquemas de bases de datos, incluyendo tablas, vistas y procedimientos almacenados.
  <br><br>
  Nota de rendimiento: Hay mayor latencia al abrir una conexión a través de una red local
</p>

## Funcionalidades del proyecto
### **Conectividad Universal**
- **Compatibilidad con Múltiples Motores:** Establece y mantiene conexiones activas con varios SGBD diferentes simultáneamente
- **Versatilidad de Red:** Admite conexiones locales y acceso remoto a través de una red Wi-Fi compartida

### **Explorador de Esquemas**
- **Explorador de Objetos:** Visualice una vista de árbol jerárquica de todas las bases de datos accesibles
- **Inspección de Metadatos:** Explore en profundidad Tablas, Vistas y Procedimientos Almacenados específicos para cualquier conexión activa

### **Motor de Consultas y Migración**
- **Ejecución Contextual:** Ejecute consultas seleccionando el nodo de conexión específico necesario en la interfaz de usuario
- **Migración de Bases de Datos:** Automatiza la transferencia de esquemas y datos completos de un proveedor de SGBD a otro
- **Capa de Lógica de Negocio:** Utiliza la biblioteca "Reglas de negocio" para la conversión de datos y la traducción de consultas estandarizadas en diferentes plataformas

## Tecnologías utilizadas
- C#
- Firebird
- Microsoft SQL Server
- MySQL
- PostgreSQL
- Oracle
- Windows Forms App (.NET Framework)
