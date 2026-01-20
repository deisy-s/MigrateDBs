using DevExpress.Data.Entity;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace FinalProject
{
    public partial class MainPage : Form
    {
        private string sSGBD, sServer, sUser, sPass, sRoute;
        private ClassLogin login;
        private List<ClassLogin> lLogin = new List<ClassLogin>();

        // Pasar las variables necesarias a MainPage
        public MainPage(string sgbd, string server, string user, string pass, string route)
        {
            InitializeComponent();
            AddTree(sgbd, server, user, pass, route);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvResults.DataSource = null;
            tbQuery.Clear();
            cbDB.Text = "";
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // Guardar los valores del query ingresado y la bd seleccionada
                string sQry = tbQuery.Text;
                string sDB = cbDB.Text;
                DataTable dtSelect = new DataTable();

                // Si el ComboBox de la bds o el TextBox del query se encuentran vacíos, mandar un error
                if (string.IsNullOrEmpty(sQry) || string.IsNullOrEmpty(sDB))
                {
                    MessageBox.Show("Por favor ingrese los datos correctos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Evaluar si el query contiene un create o un select
                    if (sQry.ToUpper().Contains("SELECT"))
                    {
                        // Llamar al método correspondiente
                        if (login.DBQuery(ref dtSelect, sDB, sQry))
                        {
                            // Llenar el DataGridView vacío con el DataTable retornado
                            dgvResults.DataSource = dtSelect;
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Llamar al método correspondiente
                        if (login.DBQuery(sDB, sQry))
                        {
                            MessageBox.Show("Instrucción ejecutada", "Finalizado", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrió un error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddConnect_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void tvDBs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is ClassLogin connection)
            {
                login = connection;
                LoadCB();
            }
        }

        private void btnDeleteConnect_Click(object sender, EventArgs e)
        {
            // obtener el nodo seleccionado
            string name = tvDBs.SelectedNode.Text;
            // obtener solo el nombre de servdor
            // corregir esto para que también revise el sgbd
            int index = name.IndexOf(" ");
            if (index >= 0)
                name = name.Substring(0, index);
            for(int i = 0; i< lLogin.Count; i++)
            {
                ClassLogin redo = lLogin[i];
                if(redo.sServer.ToString() == name)
                {
                    // eliminar la conexión de la lista si es la que quiero eliminar
                    lLogin.Remove(redo);
                    // recargar el panel
                    tvDBs.SelectedNode.Remove();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // obtener el nodo seleccionado
            string name = tvDBs.SelectedNode.Text;
            tvDBs.SelectedNode.Remove();
            // obtener solo el nombre de servdor
            // corregir esto para que también revise el sgbd
            int index = name.IndexOf(" ");
            if (index >= 0)
                name = name.Substring(0, index);
            for (int i = 0; i < lLogin.Count; i++)
            {
                ClassLogin redo = lLogin[i];
                if (redo.sServer.ToString() == name)
                {
                    LoadTree2(redo);
                }
            }
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            // Revisar si ya existe una ventana de MainPage
            var check = Application.OpenForms.OfType<Migrate>().FirstOrDefault();
            //var check = main.ShowDialog();
            if (check != null)
            {
                
            }
            else
            {
                Migrate migrate = new Migrate(lLogin);
                var mainCheck = migrate.ShowDialog();
                if (mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                {
                    migrate.Close();
                }
            }
        }

        private void LoadCB()
        {
            cbDB.Items.Clear();

            // Guardar las BDs en una variable
            List<string> databases = login.GetDBs();
            
            foreach (string db in databases)
            {
                cbDB.Items.Add(db);
            }
        }

        public void AddTree(string sgbd, string server, string user, string pass, string route)
        {
            this.sSGBD = sgbd;
            this.sServer = server;
            this.sUser = user;
            this.sPass = pass;
            this.sRoute = route;
            login = CreateConnection();
            if(login != null)
            {
                // Agregar conexión a mi lista
                lLogin.Add(login);
                LoadTree2(login);
            } else
            {
                MessageBox.Show("Error");
            }
        }

        private void LoadTree2(ClassLogin login)
        {
            try
            {
                // Guardar las BDs en una variable
                List<string> databases = login.GetDBs();
                cbDB.Items.Clear();

                // Agregar un nodo del nombre de servidor con etiqueta
                TreeNode cnnode = new TreeNode();
                cnnode.Tag = login;
                cnnode.Text = $"{login.sServer} | {login.sSGBD}";
                cnnode.Name = $"{login.sServer} | {login.sSGBD}"; // Nombre del nodo

                // verificar si ya existe este nodo, sino -> lo agrega
                // Este método evalúa el nombre del nodo padre
                if (!tvDBs.Nodes.ContainsKey($"{login.sServer} | {login.sSGBD}"))
                {
                    tvDBs.Nodes.Add(cnnode);
                    foreach (string db in databases)
                    {
                        // Agregar un nodo con etiqueta
                        var dbnode = cnnode.Nodes.Add(db);
                        dbnode.Tag = "DATABASE";
                        cbDB.Items.Add(db);

                        List<string> tables = login.GetTables(db);

                        var tnoden = dbnode.Nodes.Add("TABLAS");
                        tnoden.Tag = "TABLE";

                        foreach (string table in tables)
                        {
                            // Agregar un hijo al nodo padre con etiqueta
                            var tnode = tnoden.Nodes.Add(table);
                            tnode.Tag = "TABLE";

                            // Guardar las columnas de la tabla
                            List<Columna> columns = login.GetColumns(db, table);
                            foreach (Columna column in columns)
                            {
                                // Agregar un hijo al nodo tabla con etiqueta
                                var cnode = tnode.Nodes.Add($"{column.Name}, {column.DataT}");
                                cnode.Tag = "COLUMN";
                            }
                        }

                        List<string> views = login.GetViews(db);

                        var vnoden = dbnode.Nodes.Add("VISTAS");
                        vnoden.Tag = "VIEW";

                        foreach (string view in views)
                        {
                            // Agregar un hijo al nodo padre con etiqueta
                            var vnode = vnoden.Nodes.Add(view);
                            vnode.Tag = "VIEW";
                        }

                        List<string> keys = login.GetPK(db);

                        var knoden = dbnode.Nodes.Add("LLAVES");
                        knoden.Tag = "KEYS";

                        var pknoden = knoden.Nodes.Add("PRIMARY KEYS");
                        pknoden.Tag = "PK";

                        foreach (string key in keys)
                        {
                            // Agregar un hijo al nodo padre con etiqueta
                            var knode = pknoden.Nodes.Add(key);
                            knode.Tag = "PK";
                        }

                        List<string> fkeys = login.GetFK(db);

                        var fknoden = knoden.Nodes.Add("FOREIGN KEYS");
                        fknoden.Tag = "FK";

                        foreach (string key in fkeys)
                        {
                            // Agregar un hijo al nodo padre con etiqueta
                            var knode = fknoden.Nodes.Add(key);
                            knode.Tag = "FK";
                        }

                        List<string> sps = login.GetSP(db);

                        var spnoden = dbnode.Nodes.Add("STORED PROCEDURES");
                        spnoden.Tag = "STORED PROCEDURE";

                        foreach (string sp in sps)
                        {
                            // Agregar un hijo al nodo padre con etiqueta
                            var spnode = spnoden.Nodes.Add(sp);
                            spnode.Tag = "STORED PROCEDURE";
                        }
                    }
                    cnnode.Expand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTree()
        {
            try
            {
                // recorrer la lista de conexiones
                foreach(var l in lLogin)
                {
                    // asignar el valor de la conexión
                    login = l;
                    // Guardar las BDs en una variable
                    List<string> databases = login.GetDBs();
                    cbDB.Items.Clear();

                    // Agregar un nodo del nombre de servidor con etiqueta
                    TreeNode cnnode = new TreeNode();
                    cnnode.Tag = login;
                    cnnode.Text = $"{login.sServer} | {login.sSGBD}";
                    cnnode.Name = $"{login.sServer} | {login.sSGBD}"; // Nombre del nodo

                    // verificar si ya existe este nodo, sino -> lo agrega
                    // Este método evalúa el nombre del nodo padre
                    if (!tvDBs.Nodes.ContainsKey($"{login.sServer} | {login.sSGBD}"))
                    {
                        tvDBs.Nodes.Add(cnnode);
                        foreach (string db in databases)
                        {
                            // Agregar un nodo con etiqueta
                            var dbnode = cnnode.Nodes.Add(db);
                            dbnode.Tag = "DATABASE";
                            cbDB.Items.Add(db);

                            List<string> tables = login.GetTables(db);

                            var tnoden = dbnode.Nodes.Add("TABLAS");
                            tnoden.Tag = "TABLE";

                            foreach (string table in tables)
                            {
                                // Agregar un hijo al nodo padre con etiqueta
                                var tnode = tnoden.Nodes.Add(table);
                                tnode.Tag = "TABLE";

                                // Guardar las columnas de la tabla
                                List<Columna> columns = login.GetColumns(db, table);
                                foreach (Columna column in columns)
                                {
                                    // Agregar un hijo al nodo tabla con etiqueta
                                    var cnode = tnode.Nodes.Add($"{column.Name}, {column.DataT}");
                                    cnode.Tag = "COLUMN";
                                }
                            }

                            List<string> views = login.GetViews(db);

                            var vnoden = dbnode.Nodes.Add("VISTAS");
                            vnoden.Tag = "VIEW";

                            foreach (string view in views)
                            {
                                // Agregar un hijo al nodo padre con etiqueta
                                var vnode = vnoden.Nodes.Add(view);
                                vnode.Tag = "VIEW";
                            }

                            List<string> keys = login.GetPK(db);

                            var knoden = dbnode.Nodes.Add("LLAVES");
                            knoden.Tag = "KEYS";

                            var pknoden = knoden.Nodes.Add("PRIMARY KEYS");
                            pknoden.Tag = "PK";

                            foreach (string key in keys)
                            {
                                // Agregar un hijo al nodo padre con etiqueta
                                var knode = pknoden.Nodes.Add(key);
                                knode.Tag = "PK";
                            }

                            List<string> fkeys = login.GetFK(db);

                            var fknoden = knoden.Nodes.Add("FOREIGN KEYS");
                            fknoden.Tag = "FK";

                            foreach (string key in fkeys)
                            {
                                // Agregar un hijo al nodo padre con etiqueta
                                var knode = fknoden.Nodes.Add(key);
                                knode.Tag = "FK";
                            }

                            List<string> sps = login.GetSP(db);

                            var spnoden = dbnode.Nodes.Add("STORED PROCEDURES");
                            spnoden.Tag = "STORED PROCEDURE";

                            foreach (string sp in sps)
                            {
                                // Agregar un hijo al nodo padre con etiqueta
                                var spnode = spnoden.Nodes.Add(sp);
                                spnode.Tag = "STORED PROCEDURE";
                            }
                        }
                        cnnode.Expand();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
