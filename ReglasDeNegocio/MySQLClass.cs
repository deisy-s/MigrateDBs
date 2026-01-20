using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx;
using MySqlX.XDevAPI.Relational;
using Oracle.ManagedDataAccess.Client;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Transactions;
using System.Web;

namespace ReglasDeNegocio
{
    public class MySQLClass : ClassLogin
    {
        public MySQLClass(string sgbd, string server, string user, string pass, string ruta) : base(sgbd, server, user, pass, ruta)
        {

        }

        public override bool BDIniciarSesion()
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
                // Crear la conexión
                MySqlConnection conMySQL = new MySqlConnection(sConnection);
                conMySQL.Open(); // Abrir y cerrar la conexión
                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override List<string> GetDBs()
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand("SHOW DATABASES", conMySQL)) // Query para obtener los nombres de las BDs
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetViews(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand($"SHOW FULL TABLES IN {db} WHERE TABLE_TYPE LIKE 'VIEW';", conMySQL)) // Query para obtener los nombres de las vistas
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre de la vista a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetPK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand($"SELECT sta.column_name FROM information_schema.tables AS tab INNER JOIN information_schema.statistics AS sta ON sta.table_schema = tab.table_schema AND sta.table_name = tab.table_name AND sta.index_name = 'primary' WHERE tab.table_schema = '{db}' AND tab.table_type = 'BASE TABLE' ORDER BY column_name;", conMySQL)) // Query para obtener los nombres de las BDs
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetFK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand($"SELECT fks.constraint_name FROM information_schema.referential_constraints fks JOIN information_schema.key_column_usage kcu ON fks.constraint_schema = kcu.table_schema AND fks.table_name = kcu.table_name AND fks.constraint_name = kcu.constraint_name WHERE fks.constraint_schema = '{db}';", conMySQL)) // Query para obtener los nombres de las BDs
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetSP(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand($"SHOW PROCEDURE STATUS WHERE Db = '{db}';", conMySQL)) // Query para obtener los nombres de los sp
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[1].ToString()); // Agregar el nombre de los sp la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetTables(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            // usar ` en vez de ' para Mysql, sino no agarra
            using (MySqlCommand cmd = new MySqlCommand($"USE `{db}`; SHOW TABLES", conMySQL)) // Query para obtener nombres de las tablas
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader[0].ToString()); // Agregar el nombre de la tabla a la lista
                    }
                }
            }
            return tables;
        }

        public override List<Columna> GetColumns(string db, string table)
        {
            List<Columna> columns = new List<Columna>();
            sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
            MySqlConnection conMySQL = new MySqlConnection(sConnection);
            using (MySqlCommand cmd = new MySqlCommand($"USE `{db}`; DESCRIBE `{table}`", conMySQL)) // Query para obtener los nombres de las columnas
            {
                conMySQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(new Columna(reader["Field"].ToString(), reader["Type"].ToString())); // Agregar el nombre de la columna a la lista
                    }
                }
            }
            return columns;
        }

        public override List<string> GetPK(string db, string table)
        {
            List<string> column = new List<string>();
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);
                using (MySqlCommand cmd = new MySqlCommand($"SELECT K.TABLE_NAME, K.COLUMN_NAME, K.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS C JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS K ON C.TABLE_NAME = K.TABLE_NAME AND C.CONSTRAINT_CATALOG = K.CONSTRAINT_CATALOG AND C.CONSTRAINT_SCHEMA = K.CONSTRAINT_SCHEMA AND C.CONSTRAINT_NAME = K.CONSTRAINT_NAME WHERE C.CONSTRAINT_TYPE = 'PRIMARY KEY';", conMySQL)) // Query para obtener los nombres de las columnas
                {
                    conMySQL.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[0].ToString() == table)
                            {
                                column.Add(reader[1].ToString());
                            }
                        }
                    }
                }

                conMySQL.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return column;
        }

        public override bool GetFK(ref DataTable dt, string db, string table)
        {
            return true;
        }

        public override bool DBQuery(string db, string query)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();
                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override bool DBQuery(ref DataTable table, string db, string query)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conMySQL))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
                
                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override bool MigrateDB(string db)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database=sys; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                string query = $"CREATE DATABASE {db}";
                MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();

                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override bool MigrateTable(string db, string table, List<Columna> columns, List<string> sPK, DataTable dt)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                if(table.Contains(" "))
                {
                    table = table.Replace(" ", "_");
                }

                string query = $"CREATE TABLE {table} (";
                foreach(Columna c in columns)
                {
                    string length = "";
                    if (c.DataT.Contains("nvarchar"))
                    {
                        length = c.DataT.Replace("nvarchar", "");
                        if (length == "(-1)")
                            c.DataT = "text";
                        else
                            c.DataT = "nvarchar";
                    }
                    else
                    {
                        if (c.DataT.Contains("nchar"))
                        {
                            length = c.DataT.Replace("nchar", "");
                            if (length == "(-1)")
                                length = "(255)";
                            c.DataT = "nchar";
                        }
                    }
                    if (c.DataT.Contains("image"))
                    {
                        c.DataT = "image";
                    }
                    switch (c.DataT.ToUpper())
                    {
                        case "SMALLDATETIME":
                            query += $" {c.Name} datetime,";
                            break;
                        case "SMALLINT":
                            query += $" {c.Name} smallint(255),";
                            break;
                        case "DATETIME2":
                            query += $" {c.Name} datetime,";
                            break;
                        case "DATETIMEOFFSET":
                            query += $" {c.Name} datetime,";
                            break;
                        case "ROWVERSION":
                            query += $" {c.Name} timestamp,";
                            break;
                        case "MONEY":
                            query += $" {c.Name} decimal(19,4),";
                            break;
                        case "SMALL MONEY":
                            query += $" {c.Name} decimal(10,4),";
                            break;
                        case "SYSNAME":
                            query += $" {c.Name} char(256),";
                            break;
                        case "BIT":
                            query += $" {c.Name} tinyint(1),";
                            break;
                        case "IMAGE":
                            query += $" {c.Name} longblob,";
                            break;
                        case "REAL":
                            query += $" {c.Name} float,";
                            break;
                        case "NUMERIC":
                            query += $" {c.Name} decimal,";
                            break;
                        case "TEXT":
                            query += $" {c.Name} longtext,";
                            break;
                        case "NTEXT":
                            query += $" {c.Name} longtext,";
                            break;
                        case "NCHAR":
                            query += $" {c.Name} char{length},";
                            break;
                        case "NVARCHAR":
                            query += $" {c.Name} varchar{length},";
                            break;
                        case "UNIQUEIDENTIFIER":
                            query += $" {c.Name} varchar(64),";
                            break;
                        case "XML":
                            query += $" {c.Name} text,";
                            break;
                        default:
                            query += $" {c.Name} {c.DataT},";
                            break;
                    }
                }

                int i = 0;
                if(sPK.Count > 0)
                {
                    query += $" PRIMARY KEY (";
                    foreach (string pk in sPK)
                    {
                        if(i > 0)
                        {
                            query += ", ";
                        }
                        query += $"{pk}";
                        i++;
                    }

                    query += ")";
                }

                if(sPK.Count == 0)
                {
                    query = query.Remove(query.Length - 1);
                }

                query += ");";

                MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();

                string query2 = "";
                List<bool> bs = new List<bool>();
                List<bool> bdt = new List<bool>();
                // para verificar si ocupan '' para ingresar el dato
                foreach (Columna c in columns)
                {
                    if (c.DataT.Contains("varchar"))
                    {
                        c.DataT = "varchar";
                    }
                    else
                    {
                        if (c.DataT.Contains("char"))
                        {
                            c.DataT = "char";
                        }
                    }
                    switch (c.DataT.ToUpper())
                    {
                        case "CHAR":
                            bs.Add(true);
                            bdt.Add(false);
                            break;
                        case "VARCHAR":
                            bs.Add(true);
                            bdt.Add(false);
                            break;
                        case "TEXT":
                            bs.Add(true);
                            bdt.Add(false);
                            break;
                        case "LONGTEXT":
                            bs.Add(true);
                            bdt.Add(false);
                            break;
                        case "SMALLDATETIME":
                            bs.Add(true);
                            bdt.Add(true);
                            break;
                        case "DATE":
                            bs.Add(true);
                            bdt.Add(true);
                            break;
                        case "DATETIME":
                            bs.Add(true);
                            bdt.Add(true);
                            break;
                        case "DATETIME2":
                            bs.Add(true);
                            bdt.Add(true);
                            break;
                        case "DATETIMEOFFSET":
                            bs.Add(true);
                            bdt.Add(true);
                            break;
                        default:
                            bs.Add(false);
                            bdt.Add(false);
                            break;
                    }
                }

                int r = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int c = 0;
                    query2 += $"INSERT INTO {table} VALUES (";

                    foreach(DataColumn column in dt.Columns)
                    {

                        if (bs[c])
                        {
                            query2 += "\'";

                            if (dt.Rows[r][column].ToString().Contains("  "))
                            {
                                if (bdt[c])
                                {
                                    string[] date = dt.Rows[r][column].ToString().Split('/');
                                    int snum = 0;
                                    string day = "", month = "", year = "", time = "";
                                    foreach (string s in date)
                                    {
                                        switch (snum)
                                        {
                                            case 0:
                                                month = s;
                                                break;
                                            case 1:
                                                day = s;
                                                break;
                                            case 2:
                                                year = s;
                                                break;
                                        }
                                        snum++;
                                    }
                                    time = year.Substring(4, year.Length);
                                    time = time.Replace("AM", "");
                                    time = time.Replace("PM", "");
                                    time = time.Trim(' ');
                                    year = year.Substring(0, 4);
                                    query2 += $"{year}-{month}-{day} {time}";
                                    query2 = query2.Trim(' ');
                                    query2 += "\', ";
                                }
                                else
                                {
                                    if (dt.Rows[r][column].ToString().Contains("'"))
                                    {
                                        string sn = dt.Rows[r][column].ToString().Replace("'", "");
                                        query2 += $"{sn}";
                                        query2 = query2.Trim(' ');
                                        query2 += "\', ";
                                    }
                                    else
                                    {
                                        query2 += $"{dt.Rows[r][column]}";
                                        query2 = query2.Trim(' ');
                                        query2 += "\', ";
                                    }
                                }
                            }
                            else
                            {
                                if (bdt[c])
                                {
                                    string[] date = dt.Rows[r][column].ToString().Split('/');
                                    int snum = 0;
                                    string day = "", month = "", year = "", time = "";
                                    foreach (string s in date)
                                    {
                                        switch (snum)
                                        {
                                            case 0:
                                                month = s;
                                                break;
                                            case 1:
                                                day = s;
                                                break;
                                            case 2:
                                                year = s;
                                                break;
                                        }
                                        snum++;
                                    }
                                    time = year;
                                    time = time.Substring(4, 12);
                                    time = time.Replace("AM", "");
                                    time = time.Replace("PM", "");
                                    time = time.Trim(' ');
                                    year = year.Substring(0, 4);
                                    query2 += $"{year}-{month}-{day} {time}";
                                    query2 = query2.Trim(' ');
                                    query2 += "\', ";
                                }
                                else
                                {
                                    if (dt.Rows[r][column].ToString().Contains("'"))
                                    {
                                        string sn = dt.Rows[r][column].ToString().Replace("'", "");
                                        query2 += $"{sn}\', ";
                                    }
                                    else
                                        query2 += $"{dt.Rows[r][column]}\', ";
                                }
                            }
                        }
                        else
                        {
                            if (dt.Rows[r][column].ToString().Contains("  "))
                            {
                                if (dt.Rows[r][column].ToString().Contains("'"))
                                {
                                    string sn = dt.Rows[r][column].ToString().Replace("'", "");
                                    query2 += $"{sn}";
                                    query2 = query2.Trim(' ');
                                    query2 += ", ";
                                }
                                else
                                {
                                    query2 += $"{dt.Rows[r][column]}";
                                    query2 = query2.Trim(' ');
                                    query2 += ", ";
                                }
                            }
                            else
                            {
                                if (dt.Rows[r][column].ToString().Contains("'"))
                                {
                                    string sn = dt.Rows[r][column].ToString().Replace("'", "");
                                    query2 += $"{sn}, ";
                                }
                                else
                                    query2 += $"{dt.Rows[r][column]}, ";
                            }
                        }
                        

                        c++;
                    }

                    if (query2.Contains("\""))
                    {
                        query2 = query2.Replace("\"", "");
                    }

                    query2 = query2.Remove(query2.Length - 2);
                    query2 += ");";
                    r++;
                }

                MySqlCommand cmd2 = new MySqlCommand(query2, conMySQL);

                cmd2.ExecuteNonQuery();

                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override bool MigrateAddFK(string db, string table, string column, string ogTable, string ogColumn)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                if (table.Contains(" "))
                {
                    table = table.Replace(" ", "_");
                }
                if (ogTable.Contains(" "))
                {
                    ogTable = ogTable.Replace(" ", "_");
                }

                string query = $"";

                query += $"ALTER TABLE {table} ADD FOREIGN KEY ({column}) REFERENCES {ogTable}({ogColumn});";

                MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();

                conMySQL.Close();
                bOk = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOk;
        }

        public override bool GetView(ref DataTable dt, string db, string view)
        {
            return true;
        }

        public override bool GetSP(ref DataTable dt, string db, string sp)
        {
            return true;
        }

        public override bool MigrateView(string db, string view, string query)
        {
            bool bOK = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                query = query.Replace($"\"{view}\"", "[");

                view = view.Replace(" ", "_");

                query = query.Replace("[", view);

                int index = query.IndexOf("-");
                if (index >= 0)
                    query = query.Substring(0, index);

                MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();

                conMySQL.Close();
                bOK = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOK;
        }

        public override bool MigrateSP(string db, string sp, string query)
        {
            bool bOK = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                MySqlConnection conMySQL = new MySqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                if (query.Contains("\""))
                    query = query.Replace($"\"{sp}\"", $"{sp} (");
                else
                    query = query.Replace(sp, $"{sp} (");

                if (query.Contains("nvarchar"))
                {
                    query = query.Replace("nvarchar", "varchar");
                }
                if (query.Contains("smalldatetime"))
                {
                    query = query.Replace("smalldatetime", "datetime");
                }
                if (query.Contains("smallint"))
                {
                    query = query.Replace("smallint", "smallint(255)");
                }
                if (query.Contains("datetime2"))
                {
                    query = query.Replace("datetime2", "datetime");
                }
                if (query.Contains("datetimeoffset"))
                {
                    query = query.Replace("datetimeoffset", "datetime");
                }
                if (query.Contains("rowversion"))
                {
                    query = query.Replace("rowversion", "timestamp");
                }
                if (query.Contains("money"))
                {
                    query = query.Replace("money", "decimal(19,4)");
                }
                if (query.Contains("small money"))
                {
                    query = query.Replace("small money", "decimal(10,4)");
                }
                if (query.Contains("sysname"))
                {
                    query = query.Replace("sysname", "char(256)");
                }
                if (query.Contains("bit"))
                {
                    query = query.Replace("bit", "tinyint(1)");
                }
                if (query.Contains("image"))
                {
                    query = query.Replace("image", "longblob");
                }
                if (query.Contains("real"))
                {
                    query = query.Replace("real", "float");
                }
                if (query.Contains("numeric"))
                {
                    query = query.Replace("numeric", "decimal");
                }
                if (query.Contains("text"))
                {
                    query = query.Replace("text", "longtext");
                }
                if (query.Contains("ntext"))
                {
                    query = query.Replace("ntext", "longtext");
                }
                if (query.Contains("nchar"))
                {
                    query = query.Replace("nchar", "char");
                }
                if (query.Contains("uniqueidentifier"))
                {
                    query = query.Replace("uniqueidentifier", "varchar(64)");
                }
                if (query.Contains("xml"))
                {
                    query = query.Replace("xml", "text");
                }

                if (query.Contains("AS"))
                {
                    int i = query.IndexOf("AS");
                    query = query.Remove(i, "AS".Length);
                    query = query.Insert(i, ") BEGIN");
                }
                query += " END;";

                int index = query.IndexOf(sp);
                string q = query;
                if (index >= 0)
                    q = q.Substring(index + sp.Length, q.Length - index - sp.Length);
                index = query.IndexOf("(");
                string newquery = "";
                if (index >= 0)
                    newquery = query.Substring(0, index);
                string[] s = q.Split(',');
                int j = 0;

                foreach (string ss in s)
                {
                    if (ss.Contains("OUTPUT"))
                    {
                        newquery += ss.Replace("@", "OUT ");
                        newquery = newquery.Replace("OUTPUT", "");
                        newquery += ", ";
                    } else
                    {
                        int ibeg = query.IndexOf(") BEGIN");
                        int ias = ss.IndexOf("AS");
                        if (ss.Contains("AS") && ias > ibeg)
                        {
                            newquery += ss.Replace("@", "IN ");
                            newquery += ", ";
                            j++;
                        } else
                        {
                            newquery += ss.Replace("@", "");
                            newquery += ", ";
                        }
                    }
                }

                if (sp.Contains(" "))
                {
                    string nsp = "";
                    nsp = sp.Replace(" ", "_");
                    newquery = newquery.Replace(sp, nsp);
                }

                if (newquery.Contains("["))
                {
                    int start = newquery.IndexOf("[");
                    int end = newquery.IndexOf(']');
                    string tname = newquery.Substring(start, end - start+1);
                    newquery = newquery.Replace($"{tname}", "#");
                    tname = tname.Replace(" ", "_");
                    tname = tname.Remove(0, 1);
                    end = tname.IndexOf(']');
                    tname = tname.Remove(end,1);
                    newquery = newquery.Replace("#", tname);
                }

                var foundIndexes = new List<int>();
                for (int i = newquery.IndexOf('\"'); i > -1; i = newquery.IndexOf('\"', i + 1))
                {
                    foundIndexes.Add(i);
                }

                int ij = 0, ik = 1;
                if (foundIndexes.Count > 0)
                {
                    for (int i = 0; i <= 0; i++)
                    {
                        if (i > 0)
                        {
                            ij = ij + 2;
                            ik = ik + 2;
                        }
                        string text = newquery.Substring(foundIndexes[ij], foundIndexes[ik] - foundIndexes[ij] + 1);
                        newquery = newquery.Replace(text, "#");
                        if (text.Contains(" "))
                        {
                            text = text.Replace(" ", "_");
                            text = text.Replace("\"", "");
                            newquery = newquery.Replace("#", text);
                        }
                        i++;
                    }
                }

                int fin = 0;

                if(newquery.Contains("SET ROWCOUNT"))
                {
                    int irc = newquery.IndexOf("ROWCOUNT");
                    string l = newquery.Substring(irc + 9, 3);
                    newquery = newquery.Remove(irc - 4, 15);
                    fin = newquery.IndexOf("END");
                    newquery = newquery.Insert(fin - 1, $"LIMIT {l}");
                }
                
                foundIndexes.Clear();
                for (int i = newquery.IndexOf("END"); i > -1; i = newquery.IndexOf("END", i + 1))
                {
                    foundIndexes.Add(i);
                }

                ij = 0;
                for (int i = 0; i < foundIndexes.Count; i++)
                {
                    newquery = newquery.Remove(foundIndexes[ij], 3);
                    newquery = newquery.Insert(foundIndexes[ij], ";END;");
                    ij++;
                }

                index = newquery.LastIndexOf("END;");
                newquery = newquery.Substring(0, index+4);

                MySqlCommand cmd = new MySqlCommand(newquery, conMySQL);

                cmd.ExecuteNonQuery();

                conMySQL.Close();
                bOK = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOK;
        }

        public override void MigrateData(ref DataTable dt, string db, string table) { }
    }
}
