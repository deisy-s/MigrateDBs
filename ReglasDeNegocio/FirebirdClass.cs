using FirebirdSql.Data.FirebirdClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio
{
    public class FirebirdClass : ClassLogin
    {
        public FirebirdClass(string sgbd, string server, string user, string pass, string ruta) : base(sgbd, server, user, pass, ruta)
        {

        }

        public override bool BDIniciarSesion()
        {
            bool bOk = false;
            try
            {
                sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
                // Crear la conexión
                FbConnection conFire = new FbConnection(sConnection);
                conFire.Open(); // Abrir y cerrar la conexión
                conFire.Close();
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
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            databases.Add(ExtractDB());
            return databases;
        }

        public override List<string> GetViews(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand("SELECT TRIM(RDB$RELATION_NAME) FROM RDB$RELATIONS WHERE RDB$VIEW_BLR IS NOT NULL AND RDB$SYSTEM_FLAG = 0;", conFb)) // Query para obtener nombres de las tablas
            {
                conFb.Open();
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

        public override List<string> GetPK(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand("SELECT 'PRIMARY KEY' AS TIPO, TRIM(rc.RDB$CONSTRAINT_NAME) AS NOMBRE_CONSTRAINT, TRIM(rc.RDB$RELATION_NAME) AS TABLA_ORIGEN, ( SELECT FIRST 1 TRIM(sg.RDB$FIELD_NAME) FROM RDB$INDEX_SEGMENTS sg WHERE sg.RDB$INDEX_NAME = rc.RDB$INDEX_NAME ORDER BY sg.RDB$FIELD_POSITION ) AS COLUMNA_PRINCIPAL, NULL AS TABLA_DESTINO, NULL AS COLUMNA_DESTINO_PRINCIPAL FROM RDB$RELATION_CONSTRAINTS rc WHERE rc.RDB$CONSTRAINT_TYPE = 'PRIMARY KEY'", conFb)) // Query para obtener nombres de las tablas
            {
                conFb.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader[1].ToString()); // Agregar el nombre de la tabla a la lista
                    }
                }
            }
            return tables;
        }

        public override List<string> GetFK(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand("SELECT 'FOREIGN KEY' AS TIPO, TRIM(fk.RDB$CONSTRAINT_NAME) AS NOMBRE_CONSTRAINT, TRIM(fk.RDB$RELATION_NAME) AS TABLA_ORIGEN, ( SELECT FIRST 1 TRIM(fsg.RDB$FIELD_NAME) FROM RDB$INDEX_SEGMENTS fsg WHERE fsg.RDB$INDEX_NAME = fk.RDB$INDEX_NAME ORDER BY fsg.RDB$FIELD_POSITION ) AS COLUMNA_PRINCIPAL, TRIM(pk.RDB$RELATION_NAME) AS TABLA_DESTINO, ( SELECT FIRST 1 TRIM(psg.RDB$FIELD_NAME) FROM RDB$INDEX_SEGMENTS psg WHERE psg.RDB$INDEX_NAME = pk.RDB$INDEX_NAME ORDER BY psg.RDB$FIELD_POSITION ) AS COLUMNA_DESTINO_PRINCIPAL FROM RDB$RELATION_CONSTRAINTS fk JOIN RDB$REF_CONSTRAINTS rc ON rc.RDB$CONSTRAINT_NAME = fk.RDB$CONSTRAINT_NAME JOIN RDB$RELATION_CONSTRAINTS pk ON pk.RDB$CONSTRAINT_NAME = rc.RDB$CONST_NAME_UQ WHERE fk.RDB$CONSTRAINT_TYPE = 'FOREIGN KEY'", conFb)) // Query para obtener nombres de las tablas
            {
                conFb.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader[1].ToString()); // Agregar el nombre de la tabla a la lista
                    }
                }
            }
            return tables;
        }

        public override List<string> GetSP(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand("SELECT TRIM(RDB$PROCEDURE_NAME) AS NOMBRE_PROCEDIMIENTO FROM RDB$PROCEDURES WHERE RDB$SYSTEM_FLAG = 0 ORDER BY RDB$PROCEDURE_NAME;", conFb)) // Query para obtener nombres de las tablas
            {
                conFb.Open();
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

        private string ExtractDB()
        {
            var vSplit = sConnection.Split(';'); // Separar la cadena de conexión por ;
            foreach (var v in vSplit)
            {
                if (v.Trim().StartsWith("Database=", StringComparison.InvariantCultureIgnoreCase))
                {
                    return v.Substring("Database=".Length);
                }
            }
            return string.Empty;
        }

        public override List<string> GetTables(string db)
        {
            List<string> tables = new List<string>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand("SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$VIEW_BLR IS NULL AND RDB$SYSTEM_FLAG = 0", conFb)) // Query para obtener nombres de las tablas
            {
                conFb.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader["RDB$RELATION_NAME"].ToString()); // Agregar el nombre de la tabla a la lista
                    }
                }
            }
            return tables;
        }

        public override List<Columna> GetColumns(string sb, string table)
        {
            List<Columna> columns = new List<Columna>();
            sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
            FbConnection conFb = new FbConnection(sConnection);
            using (FbCommand cmd = new FbCommand($"SELECT RF.RDB$FIELD_NAME, F.RDB$FIELD_TYPE, F.RDB$FIELD_LENGTH FROM RDB$RELATION_FIELDS RF JOIN RDB$FIELDS F ON RF.RDB$FIELD_SOURCE = F.RDB$FIELD_NAME WHERE RF.RDB$RELATION_NAME = '{table.ToUpper()}'", conFb)) // Query para obtener los nombres de las columnas
            {
                conFb.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string type;
                        string fieldType = reader["RDB$FIELD_TYPE"].ToString();
                        int length = reader["RDB$FIELD_LENGTH"] != DBNull.Value ? Convert.ToInt32(reader["RDB$FIELD_LENGTH"]) : 0;

                        switch (fieldType)
                        {
                            case "7": type = "SMALLINT"; break;
                            case "8": type = "INTEGER"; break;
                            case "10": type = "FLOAT"; break;
                            case "12": type = "DATE"; break;
                            case "13": type = "TIME"; break;
                            case "14": type = $"CHAR({length})"; break;
                            case "37": type = $"VARCHAR({length})"; break;
                            default: type = "DESCONOCIDO"; break;
                        }

                        columns.Add(new Columna(reader["RDB$FIELD_NAME"].ToString().Trim(), type));
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
                sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
                FbConnection conFb = new FbConnection(sConnection);

                if (conFb.State == ConnectionState.Closed)
                {
                    conFb.Open();
                }

                FbCommand cmd = new FbCommand(query, conFb);

                cmd.ExecuteNonQuery();
                conFb.Close();
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
                sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
                FbConnection conFb = new FbConnection(sConnection);

                if (conFb.State == ConnectionState.Closed)
                {
                    conFb.Open();
                }

                using (FbCommand cmd = new FbCommand(query, conFb))
                {
                    using (FbDataAdapter adapter = new FbDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                conFb.Close();
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
                sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
                FbConnection conFb = new FbConnection(sConnection);

                if (conFb.State == ConnectionState.Closed)
                {
                    conFb.Open();
                }

                string query = $"create table {table} (";
                string length = "";
                foreach (Columna c in columns)
                {
                    if (c.DataT.Contains("varchar"))
                    {
                        length = c.DataT.Replace("varchar", "");
                        if (length.Contains("n"))
                            length = length.Replace("n", "");
                        if (length == "(-1)")
                            length = "(255)";
                        c.DataT = "varchar";
                    } else
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
                    switch (c.DataT)
                    {
                        case "bigint":
                            query += $" {c.Name} INT64,";
                            break;
                        case "binary":
                            query += $" {c.Name} CHAR,";
                            break;
                        case "bit":
                            query += $" {c.Name} CHAR(1),";
                            break;
                        case "char":
                            query += $" {c.Name} CHAR,";
                            break; 
                        case "nchar":
                            query += $" {c.Name} CHAR{length},";
                            break; 
                        case "datetime":
                            query += $" {c.Name} TIMESTAMP,";
                            break;
                        case "decimal":
                            query += $" {c.Name} DECIMAL,";
                            break;
                        case "float":
                            query += $" {c.Name} FLOAT,";
                            break;
                        case "image":
                            query += $" {c.Name} BLOB,";
                            break;
                        case "int":
                            query += $" {c.Name} INTEGER,";
                            break;
                        case "money":
                            query += $" {c.Name} DECIMAL(18, 4),";
                            break;
                        case "numeric":
                            query += $" {c.Name} NUMERIC,";
                            break;
                        case "real":
                            query += $" {c.Name} DOUBLE,";
                            break;
                        case "smalldatetime":
                            query += $" {c.Name} TIMESTAMP,";
                            break;
                        case "smallint":
                            query += $" {c.Name} SMALLINT,";
                            break;
                        case "smallmoney":
                            query += $" {c.Name} DECIMAL(10, 4),";
                            break;
                        case "sql_variant":
                            query += $" {c.Name} BLOB,";
                            break; 
                        case "text":
                            query += $" {c.Name} BLOB SUB_TYPE TEXT,";
                            break;
                        case "timestamp":
                            query += $" {c.Name} INTEGER,";
                            break;
                        case "tinyint":
                            query += $" {c.Name} SMALLINT,";
                            break;
                        case "varbinary":
                            query += $" {c.Name} CHAR,";
                            break;
                        case "varchar":
                            query += $" {c.Name} VARCHAR{length},";
                            break;
                        case "nvarchar":
                            query += $" {c.Name} VARCHAR{length},";
                            break;
                        case "uniqueidentifier":
                            query += $" {c.Name} CHAR(38),";
                            break;
                        default:
                            query += $" {c.Name} {c.DataT},";
                            break;
                    }
                }

                if (sPK.Count > 0)
                {
                    foreach (string pk in sPK)
                    {
                        query += $" CONSTRAINT PK_{pk} PRIMARY KEY ({pk}),";
                    }
                }

                query = query.Remove(query.Length - 1);
                query += ");";
                FbCommand cmd = new FbCommand(query, conFb);

                cmd.ExecuteNonQuery();

                conFb.Close();
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
                sConnection = $@"User={sUser};Password={sPassword};Database={sRuta};DataSource={sServer};Port=3050;Dialect=3;Charset=NONE";
                FbConnection conMySQL = new FbConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                string query = $"ALTER TABLE {table.ToUpper()} ADD FOREIGN KEY {column.ToUpper()} REFERENCES {ogTable.ToUpper()}({ogColumn.ToUpper()});";

                FbCommand cmd = new FbCommand(query, conMySQL);

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
