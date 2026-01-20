using Microsoft.Win32;
using MySqlX.XDevAPI.Relational;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio
{
    public class PostgreSQLClass : ClassLogin
    {
        public PostgreSQLClass(string sgbd, string server, string user, string pass, string ruta) : base(sgbd, server, user, pass, ruta)
        {

        }

        public override bool BDIniciarSesion()
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
                // Crear la conexión
                NpgsqlConnection conPostgres = new NpgsqlConnection(sConnection);
                conPostgres.Open(); // Abrir y cerrar la conexión
                conPostgres.Close();
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
            sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT datname FROM pg_database WHERE datistemplate = false;", conPost)) // Query para obtener los nombres de las BDs
            {
                conPost.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["datname"].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetViews(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT table_schema, table_name FROM information_schema.views WHERE table_schema NOT IN ('pg_catalog', 'information_schema') AND table_catalog = '{db}'", conPost)) // Query para obtener los nombres de las BDs
            {
                conPost.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader[1].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetPK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT constraint_name FROM information_schema.table_constraints WHERE constraint_type IN('PRIMARY KEY') AND table_catalog='{db}';", conPost)) // Query para obtener los nombres de las BDs
            {
                conPost.Open();
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
            sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT constraint_name FROM information_schema.table_constraints WHERE constraint_type IN('FOREIGN KEY') AND table_catalog='{db}';", conPost)) // Query para obtener los nombres de las BDs
            {
                conPost.Open();
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
            sConnection = $@"Host={sServer}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT p.proname AS procedure_name FROM pg_proc p LEFT JOIN pg_namespace n ON p.pronamespace = n.oid LEFT JOIN pg_language l ON p.prolang = l.oid WHERE p.prokind = 'p'  -- 'p' para procedures, 'f' para functions AND n.nspname NOT IN ('pg_catalog', 'information_schema');", conPost)) // Query para obtener los nombres de las BDs
            {
                conPost.Open();
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

        public override List<string> GetTables(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"Host={sServer}; Database={db}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE' ", conPost)) // Query para obtener nombres de las tablas
            {
                conPost.Open();
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
            sConnection = $@"Host={sServer}; Database={db}; User Id={sUser}; Password={sPassword}";
            NpgsqlConnection conPost = new NpgsqlConnection(sConnection);
            using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT column_name, data_type ||CASE WHEN character_maximum_length IS NOT NULL THEN '(' || character_maximum_length || ')' ELSE '' END AS data_type FROM information_schema.columns WHERE table_name = '{table}';", conPost)) // Query para obtener los nombres de las columnas
            {
                conPost.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(new Columna(reader["column_name"].ToString(), reader["data_type"].ToString())); // Agregar el nombre de la columna a la lista
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
                sConnection = $@"Host={sServer}; Database={db}; User Id={sUser}; Password={sPassword}";
                NpgsqlConnection conPost = new NpgsqlConnection(sConnection);

                if (conPost.State == ConnectionState.Closed)
                {
                    conPost.Open();
                }

                NpgsqlCommand cmd = new NpgsqlCommand(query, conPost);

                cmd.ExecuteNonQuery();
                conPost.Close();
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
                sConnection = $@"Host={sServer}; Database={db}; User Id={sUser}; Password={sPassword}";
                NpgsqlConnection conPost = new NpgsqlConnection(sConnection);

                if (conPost.State == ConnectionState.Closed)
                {
                    conPost.Open();
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conPost))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                conPost.Close();
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
                sConnection = $@"Host={sServer}; Database={db}; User Id={sUser}; Password={sPassword}";
                NpgsqlConnection conMySQL = new NpgsqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                string query = $"create database {db}";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conMySQL);

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
                NpgsqlConnection conMySQL = new NpgsqlConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                if (table.Contains(" "))
                {
                    table = table.Replace(" ", "_");
                }

                string query = $"CREATE TABLE {table} (";
                foreach (Columna c in columns)
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
                    if (c.DataT.Contains("binary"))
                    {
                        c.DataT = "bytea";
                    }
                    if (c.DataT.Contains("image"))
                    {
                        c.DataT = "image";
                    }
                    switch (c.DataT.ToUpper())
                    {
                        case "BIT":
                            query += $" {c.Name} boolean,";
                            break;
                        case "DATETIME":
                            query += $" {c.Name} timestamp(3),";
                            break;
                        case "DATETIME2":
                            query += $" {c.Name} timestamp(3),";
                            break;
                        case "DATETIMEOFFSET":
                            query += $" {c.Name} timestamp(3),";
                            break;
                        case "SMALLDATETIME":
                            query += $" {c.Name} timestamp(0),";
                            break;
                        case "IMAGE":
                            query += $" {c.Name} bytea,";
                            break;
                        case "NCHAR":
                            query += $" {c.Name} char{length},";
                            break;
                        case "NTEXT":
                            query += $" {c.Name} text,";
                            break;
                        case "NVARCHAR":
                            query += $" {c.Name} varchar{length},";
                            break;
                        case "SMALLMONEY":
                            query += $" {c.Name} money,";
                            break;
                        case "UNIQUEIDENTIFIER":
                            query += $" {c.Name} char(16),";
                            break;
                        default:
                            query += $" {c.Name} {c.DataT},";
                            break;
                    }
                }

                query = query.Remove(query.Length - 1);

                query += ");";

                query += $"ALTER TABLE {table} ADD PRIMARY KEY (";

                foreach(string pk in sPK)
                {
                    query += $"{pk}, ";
                }

                query = query.Remove(query.Length - 1);

                query += ");";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conMySQL);

                cmd.ExecuteNonQuery();

                //string query2 = $"INSERT INTO {table} (";

                //foreach (Columna c in columns)
                //{
                //    query2 += $"{c.Name}, ";
                //}

                //query2 = query2.Remove(query2.Length - 2);
                //query2 += ") VALUES (";

                string columnasInsert = string.Join(",", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                foreach (DataRow filas in dt.Rows)
                {
                    string registrosInsert = string.Join(",", filas.ItemArray.Select(val => $"'{val}'"));
                    string query2 = $"insert into {table}({columnasInsert}) values({registrosInsert})";
                    NpgsqlCommand cmd2 = new NpgsqlCommand(query2, conMySQL);
                    cmd2.ExecuteNonQuery();
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

        public override bool MigrateAddFK(string db, string table, string column, string ogTable, string ogColumn)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"Server={sServer}; database={db}; UID={sUser}; password={sPassword}";
                NpgsqlConnection conMySQL = new NpgsqlConnection(sConnection);

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

                string query = $"ALTER TABLE {table} ADD CONSTRAINT fk_{column} FOREIGN KEY ({column}) REFERENCES {ogTable}({ogColumn});";

                NpgsqlCommand cmd = new NpgsqlCommand(query, conMySQL);

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
            return true;
        }

        public override bool MigrateSP(string db, string sp, string query)
        {
            return true;
        }
        public override void MigrateData(ref DataTable dt, string db, string table) { }
    }
}
