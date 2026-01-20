using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;
using Oracle.ManagedDataAccess.Client;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mysqlx.Crud;
using Org.BouncyCastle.Tls;
using FirebirdSql.Data.Services;
using System.Data;

namespace ReglasDeNegocio
{
    public abstract class ClassLogin
    {
        public String sError, sConnection;
        public string sServer, sUser, sPassword, sRuta, sSGBD;

        public ClassLogin(string sgbd, string server, string user, string pass, string ruta)
        {
            sSGBD = sgbd;
            sServer = server;
            sUser = user;
            sPassword = pass;
            sRuta = ruta;
        }

        public abstract bool BDIniciarSesion();

        public abstract List<string> GetDBs();

        public abstract List<string> GetTables(string db);

        public abstract List<string> GetViews(string db);

        public abstract List<string> GetPK(string db);

        public abstract List<string> GetFK(string db);

        public abstract List<string> GetSP(string db);

        public abstract List<Columna> GetColumns(string db, string table);

        public abstract List<string> GetPK(string db, string table);

        public abstract bool GetFK(ref DataTable dt, string db, string table);

        public abstract bool DBQuery(string db, string query);

        public abstract bool DBQuery(ref DataTable table, string db, string query);

        public abstract bool MigrateDB(string db);

        public abstract bool MigrateTable(string db, string table, List<Columna> columns, List<string> sPK, DataTable dt);

        public abstract bool MigrateAddFK(string db, string table, string column, string ogTable, string ogColumn);

        public abstract bool GetView(ref DataTable dt, string db, string view);

        public abstract bool GetSP(ref DataTable dt, string db, string sp);

        public abstract bool MigrateView(string db, string view, string query);

        public abstract bool MigrateSP(string db, string sp, string query);

        public abstract void MigrateData(ref DataTable dt, string db, string table);
    }

    public class Columna
    {
        public string Name;
        public string DataT;
        public Columna(string name, string datat)
        {
            Name = name;
            DataT = datat;
        }
    }
}
