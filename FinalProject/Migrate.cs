using DevExpress.Export;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;
using ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace FinalProject
{
    public partial class Migrate: Form
    {
        private string sSGBD, sServer, sUser, sPass, sRoute;
        private ClassLogin login;
        private List<ClassLogin> lLogin = new List<ClassLogin>();
        private string ssgbd, sDestiny;

        // ocupas pasar la lista de conexiones parece
        public Migrate(List<ClassLogin> logins)
        {
            InitializeComponent();
            cbDbs.Items.Clear();
            lLogin = logins;
        }

        private void rbSQLServer1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSQLServer1.Checked)
            {
                cbDbs.Text = "";
                cbDbs.Items.Clear();
                clbTables.Items.Clear();
                ssgbd = "SQL Server";
                foreach (var l in lLogin)
                {
                    login = l;
                    if (login.sSGBD == ssgbd)
                    {
                        List<string> databases = login.GetDBs();

                        foreach(string db in databases)
                        {
                            cbDbs.Items.Add(db);
                        }
                    }
                }
            }
        }

        private void cbDbs_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                clbTables.Items.Clear();
                foreach (var l in lLogin)
                {
                    login = l;
                    if (login.sSGBD == ssgbd)
                    {
                        List<string> databases = login.GetDBs();

                        foreach (string db in databases)
                        {
                            if(cbDbs.Text == db.ToString())
                            {
                                List<string> tables = login.GetTables(db);

                                foreach (string t in tables)
                                {
                                    clbTables.Items.Add(t);
                                }
                            }
                        }
                    }
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbMySQL2_CheckedChanged(object sender, EventArgs e)
        {
            sDestiny = "MySQL";
        }

        private void rbPostgres2_CheckedChanged(object sender, EventArgs e)
        {
            sDestiny = "PostgreSQL";
        }

        private void rbFirebird2_CheckedChanged(object sender, EventArgs e)
        {
            sDestiny = "Firebird";
        }

        private void rbOracle2_CheckedChanged(object sender, EventArgs e)
        {
            sDestiny = "Oracle";
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                string sDB = cbDbs.Text;
                ClassLogin lOrigin, lDestiny;
                this.sSGBD = "SQL Server";
                this.sServer = "localhost";
                this.sUser = "sa";
                this.sPass = "Deisy29";
                lOrigin = CreateConnection();
                lDestiny = CreateConnection();

                if (rbMySQL2.Checked || rbOracle2.Checked || rbPostgres2.Checked || rbFirebird2.Checked)
                {

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un SGBD destino");
                    return;
                }

                foreach (var l in lLogin)
                {
                    login = l;
                    if (login.sSGBD == sDestiny)
                    {
                        lDestiny = login;
                        switch (sDestiny)
                        {
                            case "MySQL":
                                sDB = cbDbs.Text;
                                if (!login.MigrateDB(cbDbs.Text))
                                {
                                    MessageBox.Show(login.sError);
                                }
                                break;
                            case "Firebird":
                                List<string> db = lDestiny.GetDBs();
                                foreach (string d in db)
                                {
                                    sDB = d;
                                }
                                break;
                            case "PostgreSQL":
                                sDB = cbDbs.Text;
                                if (!login.MigrateDB(cbDbs.Text))
                                {
                                    MessageBox.Show(login.sError);
                                }
                                break;
                            case "Oracle":
                                List<string> db2 = lDestiny.GetDBs();
                                foreach (string d in db2)
                                {
                                    sDB = d;
                                }
                                break;
                        }
                        break;
                    }
                }

                foreach (var l in lLogin)
                {
                    login = l;
                    if (login.sSGBD == ssgbd)
                    {
                        lOrigin = login;
                        break;
                    }
                }

                List<Columna> lColumns = new List<Columna>();
                DataTable dFK = new DataTable();

                foreach(object item in clbTables.CheckedItems)
                {
                    string table = item.ToString();
                    if(table.Contains(" "))
                    {
                        table = $"[{table}]";
                    }
                    lColumns = lOrigin.GetColumns(cbDbs.Text, item.ToString());
                    List<string> sPK = lOrigin.GetPK(cbDbs.Text, item.ToString());
                    DataTable dt = new DataTable();
                    lOrigin.MigrateData(ref dt, cbDbs.Text, table);
                    if (!lDestiny.MigrateTable(sDB, item.ToString(), lColumns, sPK, dt))
                    {
                        MessageBox.Show(lDestiny.sError);
                    }
                }

                foreach (object item in clbTables.CheckedItems)
                {
                    string table = item.ToString();
                    lColumns = lOrigin.GetColumns(cbDbs.Text, table);
                    lOrigin.GetFK(ref dFK, cbDbs.Text, table);
                    foreach (Columna c in lColumns)
                    {
                        foreach (DataRow row in dFK.Rows)
                        {
                            if (c.Name == row[2].ToString())
                            {
                                if (!lDestiny.MigrateAddFK(sDB, table, c.Name, row[3].ToString(), row[4].ToString()))
                                    MessageBox.Show(lDestiny.sError);
                            }
                        }
                    }
                    
                }

                MessageBox.Show(sDB + " migrada");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ClassLogin CreateConnection()
        {
            switch (sSGBD)
            {
                case "SQL Server":
                    return new SQLServerClass(sSGBD, sServer, sUser, sPass, "");
                case "MySQL":
                    return new MySQLClass(sSGBD, sServer, sUser, sPass, "");
                case "PostgreSQL":
                    return new PostgreSQLClass(sSGBD, sServer, sUser, sPass, "");
                case "Firebird":
                    return new FirebirdClass(sSGBD, sServer, sUser, sPass, sRoute);
                case "Oracle":
                    return new OracleClass(sSGBD, sServer, sUser, sPass, "");
                default:
                    return null;
            }
        }
    }
}
