using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using ReglasDeNegocio;

namespace FinalProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que los campos de usuario y contraseña no se encuentren vacíos
                if (tbServerName.Text != string.Empty && cbSGBD.Text != string.Empty && tbUser.Text != string.Empty && tbPassword.Text != string.Empty)
                {
                    if(cbSGBD.Text == "SQL Server")
                    {
                        SQLServerClass sqls = new SQLServerClass("SQL Server", tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                        if (sqls.BDIniciarSesion())
                        {
                            this.Hide();
                            MessageBox.Show($"Conexión exitosa a {cbSGBD.Text}");
                            // Guardar la cadena de conexión
                            string connection = sqls.sConnection;
                            // Revisar si ya existe una ventana de MainPage
                            var check = Application.OpenForms.OfType<MainPage>().FirstOrDefault();
                            //var check = main.ShowDialog();
                            if(check != null)
                            {
                                check.AddTree(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                            } else
                            {
                                MainPage mainP = new MainPage(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                                var mainCheck = mainP.ShowDialog();
                                if(mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                                {
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(sqls.sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else if(cbSGBD.Text == "MySQL")
                    {
                        MySQLClass msql = new MySQLClass("MySQL", tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                        if (msql.BDIniciarSesion())
                        {
                            this.Hide();
                            MessageBox.Show($"Conexión exitosa a {cbSGBD.Text}");
                            // Guardar la cadena de conexión
                            string connection = msql.sConnection;
                            // Revisar si ya existe una ventana de MainPage
                            var check = Application.OpenForms.OfType<MainPage>().FirstOrDefault();
                            if (check != null)
                            {
                                check.AddTree(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                            }
                            else
                            {
                                MainPage mainP = new MainPage(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                                var mainCheck = mainP.ShowDialog();
                                if (mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                                {
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(msql.sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else if(cbSGBD.Text == "PostgreSQL")
                    {
                        PostgreSQLClass psql = new PostgreSQLClass("PostgreSQL", tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                        if (psql.BDIniciarSesion())
                        {
                            this.Hide();
                            MessageBox.Show($"Conexión exitosa a {cbSGBD.Text}");
                            // Guardar la cadena de conexión
                            string connection = psql.sConnection;
                            // Revisar si ya existe una ventana de MainPage
                            var check = Application.OpenForms.OfType<MainPage>().FirstOrDefault();
                            if (check != null)
                            {
                                check.AddTree(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                            }
                            else
                            {
                                MainPage mainP = new MainPage(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                                var mainCheck = mainP.ShowDialog();
                                if (mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                                {
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(psql.sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else if(cbSGBD.Text == "Firebird")
                    {
                        if(tbRoute.Text != string.Empty)
                        {
                            FirebirdClass frbrd = new FirebirdClass("Firebird", tbServerName.Text, tbUser.Text, tbPassword.Text, tbRoute.Text);
                            if (frbrd.BDIniciarSesion())
                            {
                                this.Hide();
                                MessageBox.Show($"Conexión exitosa a {cbSGBD.Text}");
                                // Guardar la cadena de conexión
                                string connection = frbrd.sConnection;
                                // Revisar si ya existe una ventana de MainPage
                                var check = Application.OpenForms.OfType<MainPage>().FirstOrDefault();
                                if (check != null)
                                {
                                    check.AddTree(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, tbRoute.Text);
                                    this.Hide();
                                }
                                else
                                {
                                    MainPage mainP = new MainPage(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, tbRoute.Text);
                                    this.Hide();
                                    var mainCheck = mainP.ShowDialog();
                                    if (mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                                    {
                                        this.Close();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(frbrd.sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        } else
                        {
                            MessageBox.Show("Por favor llene todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    } else if(cbSGBD.Text == "Oracle")
                    {
                        OracleClass or = new OracleClass("Oracle", tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                        if (or.BDIniciarSesion())
                        {
                            this.Hide();
                            MessageBox.Show($"Conexión exitosa a {cbSGBD.Text}");
                            // Guardar la cadena de conexión
                            string connection = or.sConnection;
                            // Revisar si ya existe una ventana de MainPage
                            var check = Application.OpenForms.OfType<MainPage>().FirstOrDefault();
                            if (check != null)
                            {
                                check.AddTree(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                            }
                            else
                            {
                                MainPage mainP = new MainPage(cbSGBD.Text, tbServerName.Text, tbUser.Text, tbPassword.Text, "");
                                this.Hide();
                                var mainCheck = mainP.ShowDialog();
                                if (mainCheck == DialogResult.OK || mainCheck == DialogResult.Cancel)
                                {
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(or.sError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else
                    {
                        MessageBox.Show("SGBD no valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor llene todos los campos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConnect_Click(sender, e);
            }
        }

        private void cbSGBD_Leave(object sender, EventArgs e)
        {
            if(cbSGBD.Text == "Firebird")
            {
                tbRoute.Enabled = true;
            } else
            {
                tbRoute.Enabled = false;
            }
        }

        private void tbRoute_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnConnect_Click(sender, e);
            }
        }
    }
}
