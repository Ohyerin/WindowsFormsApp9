using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp9
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "잔여 백신";
            ComboBox();
        }
        
        void ComboBox()
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            string Query = "SELECT * FROM manage.vaccine_tb";
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();


                while (myReader.Read())
                {
                    string sName = myReader.GetString("name");
                    comboBox1.Items.Add(sName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            string Query = "SELECT * FROM manage.vaccine_tb where name ='" +comboBox1.Text+"';";
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();


                while (myReader.Read())
                {
                    string sCount = myReader.GetInt32("count").ToString();

                    this.textBox1.Text = sCount;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
