using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio
{
    public class SQLServerClass : ClassLogin
    {
        public String sError;

        public SQLServerClass(string sgbd, string server, string user, string pass, string ruta) : base(sgbd, server, user, pass, ruta)
        {

        }

        public override bool BDIniciarSesion()
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
                // Crear la conexión
                SqlConnection conSQL = new SqlConnection(sConnection);
                conSQL.Open(); // Abrir y cerrar la conexión
                conSQL.Close();
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
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases", conSQL)) // Query para obtener los nombres de las BDs
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["name"].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetViews(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"USE {db}; SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS ORDER BY TABLE_NAME;", conSQL)) // Query para obtener las vistas de las BDs
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[1].ToString()); // Agregar el nombre de las vistas a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetFK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"USE {db}; SELECT fk.name AS fk_constraint_name FROM sys.foreign_keys fk INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id CROSS APPLY (SELECT col.[name] + ', ' FROM sys.foreign_key_columns fk_c INNER JOIN sys.columns col ON fk_c.parent_object_id = col.object_id AND fk_c.parent_column_id = col.column_id WHERE fk_c.parent_object_id = fk_tab.object_id AND fk_c.constraint_object_id = fk.object_id ORDER BY col.column_id for xml path ('') ) D (column_names) ORDER BY fk_constraint_name;", conSQL)) // Query para obtener los nombres de las BDs
            {
                conSQL.Open();
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

        public override List<string> GetPK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"USE {db}; SELECT pk.[name] AS pk_name FROM sys.tables tab INNER JOIN sys.indexes pk ON tab.object_id = pk.object_id AND pk.is_primary_key = 1 INNER JOIN sys.index_columns ic ON ic.object_id = pk.object_id AND ic.index_id = pk.index_id INNER JOIN sys.columns col ON pk.object_id = col.object_id AND col.column_id = ic.column_id ORDER BY pk.[name]", conSQL)) // Query para obtener los nombres de los sp
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre de los sp a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetSP(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"SELECT ROUTINE_NAME FROM {db}.INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE';", conSQL)) // Query para obtener los sp de las BDs
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[0].ToString()); // Agregar el nombre del sp a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetTables(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"USE [{db}]; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", conSQL)) // Query para obtener nombres de las tablas
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["TABLE_NAME"].ToString()); // Agregar el nombre de la tabla a la lista
                    }
                }
            }
            return tables;
        }

        public override List<Columna> GetColumns(string db, string table)
        {
            List<Columna> columns = new List<Columna>();
            sConnection = $@"Server={sServer}; Initial Catalog = Master; User ID = {sUser}; Password = {sPassword}";
            SqlConnection conSQL = new SqlConnection(sConnection);
            using (SqlCommand cmd = new SqlCommand($"USE [{db}]; SELECT COLUMN_NAME,DATA_TYPE + CASE WHEN CHARACTER_MAXIMUM_LENGTH IS NOT NULL THEN '(' + CAST(CHARACTER_MAXIMUM_LENGTH AS VARCHAR) + ')' ELSE '' END AS DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =  '{table}'", conSQL)) // Query para obtener los nombres de las columnas
            {
                conSQL.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(new Columna(reader["COLUMN_NAME"].ToString(), reader["DATA_TYPE"].ToString())); // Agregar el nombre de la columna a la lista
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
                SqlConnection conMySQL = new SqlConnection(sConnection);
                using (SqlCommand cmd = new SqlCommand($"SELECT K.TABLE_NAME, K.COLUMN_NAME, K.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS C JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS K ON C.TABLE_NAME = K.TABLE_NAME AND C.CONSTRAINT_CATALOG = K.CONSTRAINT_CATALOG AND C.CONSTRAINT_SCHEMA = K.CONSTRAINT_SCHEMA AND C.CONSTRAINT_NAME = K.CONSTRAINT_NAME WHERE C.CONSTRAINT_TYPE = 'PRIMARY KEY';", conMySQL)) // Query para obtener los nombres de las columnas
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
            bool bOK = false;
            List<string> column = new List<string>();
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                string query = $"SELECT f.name AS foreign_key_name, OBJECT_NAME(f.parent_object_id) AS table_name, COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name, OBJECT_NAME(f.referenced_object_id) AS referenced_object, COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name, f.is_disabled, f.is_not_trusted, f.delete_referential_action_desc, f.update_referential_action_desc FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc ON f.object_id = fc.constraint_object_id WHERE f.parent_object_id = OBJECT_ID('{table}');";

                SqlConnection conSQL = new SqlConnection(sConnection);

                if (conSQL.State == ConnectionState.Closed)
                {
                    conSQL.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conSQL))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                conSQL.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOK;
        }

        public override bool DBQuery(string db, string query)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; Initial Catalog = {db}; User ID = {sUser}; Password = {sPassword}";
                SqlConnection conSQL = new SqlConnection(sConnection);

                if (conSQL.State == ConnectionState.Closed)
                {
                    conSQL.Open();
                }

                SqlCommand cmd = new SqlCommand(query, conSQL);

                cmd.ExecuteNonQuery();
                conSQL.Close();
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
                sConnection = $@"Server={sServer}; Initial Catalog = {db}; User ID = {sUser}; Password = {sPassword}";
                SqlConnection conSQL = new SqlConnection(sConnection);

                if (conSQL.State == ConnectionState.Closed)
                {
                    conSQL.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conSQL))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                conSQL.Close();
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
                sConnection = $@"";
                SqlConnection conMySQL = new SqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                //MySqlCommand cmd = new MySqlCommand(query, conMySQL);

                //cmd.ExecuteNonQuery();
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
                SqlConnection conMySQL = new SqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                string query = $"CREATE TABLE {table} (";
                foreach (Columna c in columns)
                {
                    query += $" {c.Name} {c.DataT},";
                }

                query += ");";
                SqlCommand cmd = new SqlCommand(query, conMySQL);

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

        public override bool MigrateAddFK(string db, string table, string column, string ogTable, string ogColumn)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                SqlConnection conMySQL = new SqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                string query = $"CREATE TABLE {table} (";
               
                SqlCommand cmd = new SqlCommand(query, conMySQL);

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
            bool bOK = false;
            try
            {
                sConnection = $@"Server={sServer}; Initial Catalog = {db}; User ID = {sUser}; Password = {sPassword}";
                string query = $"SELECT DEFINITION FROM sys.sql_modules WHERE object_id = OBJECT_ID('{view}')";
                SqlConnection conSQL = new SqlConnection(sConnection);

                if (conSQL.State == ConnectionState.Closed)
                {
                    conSQL.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conSQL))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                conSQL.Close();
                bOK = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOK;
        }

        public override bool GetSP(ref DataTable dt, string db, string sp)
        {
            bool bOK = false;
            try
            {
                sConnection = $@"Server={sServer}; Initial Catalog = {db}; User ID = {sUser}; Password = {sPassword}";
                string query = $"SELECT OBJECT_DEFINITION(OBJECT_ID('{sp}'))";
                SqlConnection conSQL = new SqlConnection(sConnection);

                if (conSQL.State == ConnectionState.Closed)
                {
                    conSQL.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conSQL))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                conSQL.Close();
                bOK = true;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return bOK;
        }

        public override bool MigrateView(string db, string view, string query)
        {
            return true;
        }

        public override bool MigrateSP(string db, string sp, string query)
        {
            return true;
        }

        public override void MigrateData(ref DataTable dt, string db, string table)
        {
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                SqlConnection conMySQL = new SqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {table}", conMySQL))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }

                conMySQL.Close();
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
        }
    }
}
