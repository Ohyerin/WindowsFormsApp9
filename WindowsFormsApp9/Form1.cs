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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "백신접종관리";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = "datasource=localhost;port=3306;username=root;password=hyerin453";
                MySqlConnection myConn = new MySqlConnection(myConnection);

                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM manage.manager_tb where id='" + this.textBox1.Text + "' and passwd = '" + this.textBox2.Text + "'", myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = selectCommand.ExecuteReader();
                int count = 0;

                while (myReader.Read())
                {
                    count = count + 1;

                }
                if (count == 1)
                {
                    MessageBox.Show("로그인 되었습니다.");
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("아이디와 패스워드가 일치하지 않습니다.");
                }
                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
    }
}
