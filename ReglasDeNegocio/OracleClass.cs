using MySqlX.XDevAPI.Relational;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasDeNegocio
{
    public class OracleClass : ClassLogin
    {
        public OracleClass(string sgbd, string server, string user, string pass, string ruta) : base(sgbd, server, user, pass, ruta)
        {

        }

        public override bool BDIniciarSesion()
        {
            bool bOk = false;
            try
            {
                sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
                // Crear la conexión
                OracleConnection conOracle = new OracleConnection(sConnection);
                conOracle.Open(); // Abrir y cerrar la conexión
                conOracle.Close();
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
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT GLOBAL_NAME FROM GLOBAL_NAME", conO)) // Query para obtener los nombres de las BDs
            {
                conO.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["GLOBAL_NAME"].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetViews(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT view_name FROM user_views", conO)) // Query para obtener los nombres de las BDs
            {
                conO.Open();
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
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT constraint_name FROM user_constraints WHERE constraint_type = 'P'", conO)) // Query para obtener los nombres de las BDs
            {
                conO.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["CONSTRAINT_NAME"].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetFK(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT constraint_name FROM user_constraints WHERE constraint_type = 'R'", conO)) // Query para obtener los nombres de las BDs
            {
                conO.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        databases.Add(reader["CONSTRAINT_NAME"].ToString()); // Agregar el nombre de la BD a la lista
                    }
                }
            }
            return databases;
        }

        public override List<string> GetSP(string db)
        {
            List<string> databases = new List<string>();
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT object_name FROM user_procedures WHERE object_type = 'PROCEDURE'", conO)) // Query para obtener los nombres de las BDs
            {
                conO.Open();
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
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand("SELECT TABLE_NAME FROM USER_TABLES", conO)) // Query para obtener nombres de las tablas
            {
                conO.Open();
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

        public override List<Columna> GetColumns(string sb, string table)
        {
            List<Columna> columns = new List<Columna>();
            sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
            OracleConnection conO = new OracleConnection(sConnection);
            using (OracleCommand cmd = new OracleCommand($"SELECT COLUMN_NAME, DATA_TYPE || '(' || DATA_LENGTH || ')' AS DATA_TYPE FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '{table.ToUpper()}'", conO)) // Query para obtener los nombres de las columnas
            {
                cmd.Parameters.Add(new OracleParameter("table", table.ToUpper()));
                conO.Open();
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
                sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
                OracleConnection conO = new OracleConnection(sConnection);

                if (conO.State == ConnectionState.Closed)
                {
                    conO.Open();
                }

                OracleCommand cmd = new OracleCommand(query, conO);

                cmd.ExecuteNonQuery();
                conO.Close();
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
                sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
                OracleConnection conO = new OracleConnection(sConnection);

                if (conO.State == ConnectionState.Closed)
                {
                    conO.Open();
                }

                using (OracleCommand cmd = new OracleCommand(query, conO))
                {
                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }

                conO.Close();
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
                sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
                OracleConnection conMySQL = new OracleConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                // Crear la tabla en Oracle
                string createScript = GenerarCreateTable(table, columns, sPK);

                OracleCommand cmd = new OracleCommand(createScript, conMySQL);

                cmd.ExecuteNonQuery();

                // Insertar los datos


                foreach (DataRow fila in dt.Rows)
                {
                    string insert = this.GenerarInsert(table, columns, fila);
                    OracleCommand cmd2 = new OracleCommand(insert, conMySQL);

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

        private string TraducirTipoSQLServerAOracle(string tipoSQL)
        {
            tipoSQL = tipoSQL.ToLower();

            if (tipoSQL.StartsWith("int")) return "NUMBER(10)";
            if (tipoSQL.StartsWith("bigint")) return "NUMBER(19)";
            if (tipoSQL.StartsWith("smallint")) return "NUMBER(5)";
            if (tipoSQL.StartsWith("tinyint")) return "NUMBER(3)";
            if (tipoSQL.StartsWith("bit")) return "NUMBER(1)";
            if (tipoSQL.StartsWith("decimal")) return tipoSQL.Replace("decimal", "NUMBER");
            if (tipoSQL.StartsWith("numeric")) return tipoSQL.Replace("numeric", "NUMBER");
            if (tipoSQL.StartsWith("money")) return "NUMBER(19,4)";
            if (tipoSQL.StartsWith("smallmoney")) return "NUMBER(10,4)";
            if (tipoSQL.StartsWith("float")) return "FLOAT(126)";
            if (tipoSQL.StartsWith("real")) return "FLOAT(63)";
            if (tipoSQL.StartsWith("char")) return tipoSQL.Replace("char", "CHAR");
            if (tipoSQL.StartsWith("varchar")) return tipoSQL.Replace("varchar", "VARCHAR2");
            if (tipoSQL.StartsWith("nvarchar")) return tipoSQL.Replace("nvarchar", "NVARCHAR2");
            if (tipoSQL.StartsWith("nchar")) return tipoSQL.Replace("nchar", "NCHAR");
            if (tipoSQL.StartsWith("text")) return "CLOB";
            if (tipoSQL.StartsWith("ntext")) return "NCLOB";
            if (tipoSQL.StartsWith("binary")) return "RAW(2000)";
            if (tipoSQL.StartsWith("varbinary")) return "RAW(2000)";
            if (tipoSQL.StartsWith("image")) return "BLOB";
            if (tipoSQL.StartsWith("datetime2")) return "TIMESTAMP";
            if (tipoSQL.StartsWith("datetime")) return "DATE";
            if (tipoSQL.StartsWith("smalldatetime")) return "DATE";
            if (tipoSQL.StartsWith("date")) return "DATE";
            if (tipoSQL.StartsWith("time")) return "VARCHAR2(8)";
            if (tipoSQL.StartsWith("timestamp")) return "RAW(8)";
            if (tipoSQL.StartsWith("uniqueidentifier")) return "VARCHAR2(36)";
            if (tipoSQL.StartsWith("xml")) return "CLOB";
            if (tipoSQL.StartsWith("sql_variant")) return "VARCHAR2(1000)";

            return "VARCHAR2(1000)"; // Tipo por defecto
        }
        private string FormatearValorParaOracle(object valor, string tipoDato)
        {
            if (valor == null || valor == DBNull.Value || valor.ToString().Trim().ToUpper() == "NULL")
                return "NULL";

            tipoDato = tipoDato.ToLower();
            string valorTexto = valor.ToString().Trim();

            // Cadenas de texto
            if (tipoDato.StartsWith("varchar") || tipoDato.StartsWith("nvarchar") ||
                tipoDato.StartsWith("char") || tipoDato.StartsWith("nchar"))
            {
                return $"'{valorTexto.Replace("'", "''")}'";
            }

            // Fechas
            if (tipoDato.StartsWith("date") || tipoDato.StartsWith("datetime") ||
                tipoDato.StartsWith("smalldatetime"))
            {
                if (DateTime.TryParse(valorTexto, out DateTime fecha))
                    return $"TO_DATE('{fecha:yyyy-MM-dd HH:mm:ss}', 'YYYY-MM-DD HH24:MI:SS')";
                else
                    return "NULL";
            }

            // Tiempo (lo pasamos como texto)
            if (tipoDato.StartsWith("time"))
            {
                try
                {
                    TimeSpan hora;
                    if (valor is TimeSpan)
                        hora = (TimeSpan)valor;
                    else
                        hora = TimeSpan.Parse(valorTexto);
                    return $"'{hora:hh\\:mm\\:ss}'";
                }
                catch
                {
                    return "NULL";
                }
            }

            // Booleanos (bit / bool)
            if (tipoDato.StartsWith("bit") || tipoDato == "boolean")
            {
                string val = valorTexto.ToLower();
                if (val == "true" || val == "1")
                    return "1";
                else if (val == "false" || val == "0")
                    return "0";
                else
                    return "NULL";
            }

            // GUID / uniqueidentifier
            if (tipoDato.StartsWith("uniqueidentifier"))
            {
                return $"'{valorTexto}'";
            }

            // Números
            if (tipoDato.StartsWith("float") || tipoDato.StartsWith("real") ||
                tipoDato.StartsWith("decimal") || tipoDato.StartsWith("numeric") ||
                tipoDato.StartsWith("money") || tipoDato.StartsWith("int") ||
                tipoDato.StartsWith("tinyint") || tipoDato.StartsWith("smallint") ||
                tipoDato.StartsWith("bigint"))
            {
                // Reemplaza coma decimal por punto para Oracle
                if (decimal.TryParse(valorTexto.Replace(",", "."), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal numero))
                {
                    return numero.ToString(System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    return "NULL";
                }
            }

            // Fallback (texto)
            return $"'{valorTexto.Replace("'", "''")}'";
        }

        public override bool MigrateAddFK(string db, string table, string column, string ogTable, string ogColumn)
        {
            bool bOk = false;
            try
            {
                sConnection = $@"User ID={sUser}; Password={sPassword}; Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sServer})(PORT=1521)))(CONNECT_DATA=(SID=XE)));";
                OracleConnection conMySQL = new OracleConnection(sConnection);

                if (conMySQL.State == ConnectionState.Closed)
                {
                    conMySQL.Open();
                }

                var tablasNormalizadas = table.Select(t => SimpleName(table).ToUpper()).ToList();

                if (!tablasNormalizadas.Contains(SimpleName(column).ToUpper()) || !tablasNormalizadas.Contains(SimpleName(ogColumn).ToUpper()))
                {
                }

                string query = $"ALTER TABLE \"{SimpleName(table).ToUpper()}\" ADD CONSTRAINT fk_{column} FOREIGN KEY ({column}) REFERENCES {SimpleName(ogTable)} ({ogColumn})";

                OracleCommand cmd = new OracleCommand(query, conMySQL);

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

        // Genera el CREATE TABLE en Oracle sin usar comillas dobles
        private string GenerarCreateTable(string tabla, List<Columna> columnas, List<string> pk)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"CREATE TABLE {SimpleName(tabla)} (");
            int Z = 0;

            // Verificamos si es llave primaria compuesta
            bool esCompuesta = pk != null && pk.Count > 1;

            for (int i = 0; i < columnas.Count; i++)
            {
                var col = columnas[i];
                string tipoDato = TraducirTipoSQLServerAOracle(col.DataT);

                // Verificamos si esta columna está en la PK
                bool esPartePK = pk != null && pk.Any(c => c.Replace("\"", "").Equals(col.Name, StringComparison.OrdinalIgnoreCase));

                sb.Append($"  {col.Name} {tipoDato}");

                // Si solo hay una columna en la PK, se declara inline
                if (esPartePK && !esCompuesta)
                    sb.Append(" PRIMARY KEY");

                if (i < columnas.Count - 1 || esCompuesta)
                    sb.Append(",");

                sb.AppendLine();
            }

            int Y = 0;
            // Si hay llave primaria compuesta, agregarla al final
            if (esCompuesta)
            {
                foreach (string p in pk)
                {
                    if(Y == 0)
                        sb.AppendLine($"  CONSTRAINT pk_{p}{Z} PRIMARY KEY (");
                    string spk = p.Replace("\"", "");
                    sb.AppendLine($"{spk}, ");
                    Y++;
                    Z++;
                }
                sb.Remove(sb.Length - 4, 2);
                sb.Append(")");
            }

            sb.Append(")");
            return sb.ToString();
        }

        // Genera el INSERT en Oracle
        private string GenerarInsert(string tabla, List<Columna> cols, DataRow fila)
        {
            var noms = new List<string>();
            var vals = new List<string>();
            foreach (var c in cols)
            {
                noms.Add(c.Name);
                vals.Add(FormatearValorParaOracle(fila[c.Name], c.DataT));
            }
            return $"INSERT INTO \"{SimpleName(tabla).ToUpper()}\" ({string.Join(", ", noms)}) VALUES ({string.Join(", ", vals)})";
        }

        // Si tabla tiene esquema, solo retorna la parte después del punto
        private string SimpleName(string tabla)
            => tabla.Contains(".") ? tabla.Split('.')[1] : tabla;


    }
}
