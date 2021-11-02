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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Text = "백신접종관리";
            ComboBox(); //초기화
            loadDb(); //초기화
        }
        //백신콤보박스
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
        //save 등록
        private void button1_Click(object sender, EventArgs e)
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            string Query = "INSERT INTO manage.person_tb (name,phoneNum,birth,vaccine,date) value('" + this.textBox1.Text
                + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.comboBox1.Text + "','" + this.dateTimePicker1.Text + "')";
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;
            if ((textBox1.Text == "" || textBox2.Text == "") || (textBox3.Text == "" || comboBox1.Text == ""))
            {
                MessageBox.Show("입력하지 않은 항목이 있습니다.");
            }
            else
            {
                try
                {
                    conDataBase.Open();
                    myReader = cmdDatabase.ExecuteReader();

                    MessageBox.Show("등록되었습니다.");

                    while (myReader.Read())
                    {

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                loadDb();
            }
        }
        //update 수정
        private void button2_Click(object sender, EventArgs e)
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            string Query = "UPDATE manage.person_tb SET name='" + this.textBox1.Text + 
                "',phoneNum='" + this.textBox2.Text + "',birth='" + this.textBox3.Text + "'," +
                "vaccine='" + this.comboBox1.Text + "',date='" + this.dateTimePicker1.Text + "'" +
                "where name='" + this.textBox1.Text+ "';";
                
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("수정되었습니다.");

                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            loadDb();
        }
        //삭제 delete
        private void button3_Click(object sender, EventArgs e)
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            string Query = "DELETE FROM manage.person_tb where name='" + this.textBox1.Text +"'";
            

            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDatabase.ExecuteReader();
                MessageBox.Show("삭제되었습니다.");

                while (myReader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
    
            loadDb();
            
        }
        //datagridview로 테이블 보이게하기
        public void loadDb()
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT name, phoneNum, birth, vaccine, date FROM manage.person_tb;", conDataBase);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmdDatabase;
                DataTable table = new DataTable();
                adapter.Fill(table);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = table;
                dataGridView1.DataSource = bSource;
                dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending); //날짜순대로 정렬
                adapter.Update(table);
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        //선택된 셀의 값 보이기
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0) //셀을 선택했을 때. 값이 있을 때
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex]; //선택된 셀의 값 보이기

                textBox1.Text = row.Cells["name"].Value.ToString();
                textBox2.Text = row.Cells["phoneNum"].Value.ToString();
                textBox3.Text = row.Cells["birth"].Value.ToString();
                comboBox1.Text = row.Cells["vaccine"].Value.ToString();
                dateTimePicker1.Text = row.Cells["date"].Value.ToString();
                //
            }
        }
      

        //검색
        public void SearchData(string searchValue)
        {
            string Constring = "datasource=localhost;port=3306;username=root;password=hyerin453";
            MySqlConnection conDataBase = new MySqlConnection(Constring);
            string Query = "SELECT * FROM manage.person_tb WHERE name LIKE '%" + searchValue + "%'"; 
            MySqlCommand cmdDatabase = new MySqlCommand(Query, conDataBase);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDatabase);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        { //공백일때 다시 체크
            if(textBox4.Text == "")
            {
                MessageBox.Show("다시 검색해주세요");
            }
           
            else
            {
                string searchValue = textBox4.Text.ToString();
                SearchData(searchValue);
                textBox4.Text = "";
               
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        //clear 버튼
        private void button6_Click(object sender, EventArgs e)
        {
            loadDb();
        }
    }
}
