using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDforKids
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        int age_group;
        int growth_group;
        int weight_group;
        double growth;
        double weight;
        bool sex;

        int tooth;
        string tooth_param;

        double MaF;
        double PF;
        double AxF;
        double MeF;
        double AxM;
        double PM;
        double LM;
        double VM;
        double FM;
        double SumSex;
        string SumSex_param;
        public Form1()
        {
            InitializeComponent();
        }

        //удаление
        private async void button3_Click(object sender, EventArgs e)
        {
            if (label9.Visible)
                label9.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [TableKids] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox6.Text);
                await command.ExecuteNonQueryAsync();

            }
            else
            {
                label9.Visible = true;
                label9.Text = "Полe 'ID' должно быть заполнено";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\ForBD\DB.mdf; Integrated Security = True; Connect Timeout = 30";
            sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [TableKids]", sqlConnection);

            try
            {
                sqlReader =  command.ExecuteReader();
                listBox1.Items.Add("|  ID  | Номер |                      ФИО                      |       Пол       |  Дата рождения  | Рост | Вес | Возрастная группа | Оценка роста | Оценка веса | Норма зубов | Половое развитие");
                listBox1.Items.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                while (sqlReader.Read())
                {
                    listBox1.Items.Add("|   " + Convert.ToString(sqlReader["Id"]) + "   |   " + Convert.ToString(sqlReader["IdN"]) + "    |  " + Convert.ToString(sqlReader["Name"]) + "  |   " + Convert.ToString(sqlReader["Sex"]) + "  |      " + Convert.ToString(sqlReader["Age"]) + "      |   " + Convert.ToString(sqlReader["Growth"]) + "   |   " + Convert.ToString(sqlReader["Weight"]) + "  |                " + Convert.ToString(sqlReader["gAge"]) + "                |           " + Convert.ToString(sqlReader["gGrowth"]) + "            |           " + Convert.ToString(sqlReader["gWeight"]) + "            | " + Convert.ToString(sqlReader["pTooth"]) + " | " + Convert.ToString(sqlReader["pSumSex"]) + "("+ Convert.ToString(sqlReader["SumSex"])+")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [TableKids] (IdN, Name, Sex, Age, Growth, Weight, gAge, gGrowth, gWeight, pTooth, SumSex, pSumSex)Values(@IdN, @Name,@Sex,@Age,@Growth,@Weight,@gAge,@gGrowth,@gWeight,@pTooth,@SumSex,@pSumSex)", sqlConnection);
                
                //определение возрастной группы

                DateTime date = DateTime.Now;
                DateTime date1 = DateTime.Parse(textBox2.Text);
                TimeSpan dif = date - date1;
                if (dif.TotalDays >= 16 && dif.TotalDays <= 45)
                    age_group = 1;
                if (dif.TotalDays >= 46 && dif.TotalDays <= 75)
                    age_group = 2;
                if (dif.TotalDays >= 76 && dif.TotalDays <= 105)
                    age_group = 3;
                if (dif.TotalDays >= 106 && dif.TotalDays <= 135)
                    age_group = 4;
                if (dif.TotalDays >= 136 && dif.TotalDays <= 165)
                    age_group = 5;
                if (dif.TotalDays >= 166 && dif.TotalDays <= 195)
                    age_group = 6;
                if (dif.TotalDays >= 196 && dif.TotalDays <= 225)
                    age_group = 7;
                if (dif.TotalDays >= 226 && dif.TotalDays <= 255)
                    age_group = 8;
                if (dif.TotalDays >= 256 && dif.TotalDays <= 285)
                    age_group = 9;
                if (dif.TotalDays >= 286 && dif.TotalDays <= 315)
                    age_group = 10;
                if (dif.TotalDays >= 316 && dif.TotalDays <= 345)
                    age_group = 11;
                if (dif.TotalDays >= 346 && dif.TotalDays <= 405)
                    age_group = 12;
                if (dif.TotalDays >= 406 && dif.TotalDays <= 495)
                    age_group = 13;
                if (dif.TotalDays >= 496 && dif.TotalDays <= 585)
                    age_group = 14;
                if (dif.TotalDays >= 586 && dif.TotalDays <= 675)
                    age_group = 15;
                if (dif.TotalDays >= 676 && dif.TotalDays <= 765)
                    age_group = 16;
                if (dif.TotalDays >= 766 && dif.TotalDays <= 855)
                    age_group = 17;
                if (dif.TotalDays >= 856 && dif.TotalDays <= 945)
                    age_group = 18;
                if (dif.TotalDays >= 946 && dif.TotalDays <= 1035)
                    age_group = 19;
                if (dif.TotalDays >= 1036 && dif.TotalDays <= 1169)
                    age_group = 20;
                if (dif.TotalDays >= 1170 && dif.TotalDays <= 1349)
                    age_group = 21;
                if (dif.TotalDays >= 1350 && dif.TotalDays <= 1529)
                    age_group = 22;
                if (dif.TotalDays >= 1530 && dif.TotalDays <= 1709)
                    age_group = 23;
                if (dif.TotalDays >= 1710 && dif.TotalDays <= 1889)
                    age_group = 24;
                if (dif.TotalDays >= 1890 && dif.TotalDays <= 2069)
                    age_group = 25;
                if (dif.TotalDays >= 2070 && dif.TotalDays <= 2249)
                    age_group = 26;
                if (dif.TotalDays >= 2250 && dif.TotalDays <= 2429)
                    age_group = 27;
                if (dif.TotalDays >= 2430 && dif.TotalDays <= 2699)
                    age_group = 28;
                if (dif.TotalDays >= 2700 && dif.TotalDays <= 3059)
                    age_group = 29;
                if (dif.TotalDays >= 3060 && dif.TotalDays <= 3419)
                    age_group = 30;
                if (dif.TotalDays >= 3420 && dif.TotalDays <= 3779)
                    age_group = 31;
                if (dif.TotalDays >= 3780 && dif.TotalDays <= 4139)
                    age_group = 32;
                if (dif.TotalDays >= 4140 && dif.TotalDays <= 4499)
                    age_group = 33;
                if (dif.TotalDays >= 4500 && dif.TotalDays <= 4859)
                    age_group = 34;
                if (dif.TotalDays >= 4860 && dif.TotalDays <= 5219)
                    age_group = 35;
                if (dif.TotalDays >= 5220 && dif.TotalDays <= 5579)
                    age_group = 36;
                if (dif.TotalDays >= 5580 && dif.TotalDays <= 5939)
                    age_group = 37;
                if (dif.TotalDays >= 5940 && dif.TotalDays <= 6299)
                    age_group = 38;
                if (dif.TotalDays >= 6300 && dif.TotalDays <= 6659)
                    age_group = 39;
                if (dif.TotalDays >= 6660)
                    age_group = 40;

                //конец определения

                //определение пола
                if (radioButton1.Checked)
                    sex = true;//женский
                else
                    sex = false;//мужской

                growth = Convert.ToDouble(textBox8.Text);
                weight = Convert.ToDouble(textBox7.Text);
                if (sex)
                {
                    //разделение для глаз
                    if (age_group == 1)
                    {
                        if (growth < 50)
                            growth_group = 1;
                        if (growth >= 50 && growth < 51)
                            growth_group = 2;
                        if (growth >= 51 && growth < 52)
                            growth_group = 3;
                        if (growth >= 52 && growth < 54)
                            growth_group = 4;
                        if (growth >= 54 && growth < 55)
                            growth_group = 5;
                        if (growth >= 55 && growth < 56)
                            growth_group = 6;
                        if (growth >= 56 && growth < 58)
                            growth_group = 7;
                        if (growth >= 58)
                            growth_group = 8;

                        if (weight < 3.24)
                            weight_group = 1;
                        if (weight >= 3.24 && weight < 3.36)
                            weight_group = 2;
                        if (weight >= 3.36 && weight < 3.67)
                            weight_group = 3;
                        if (weight >= 3.67 && weight < 4.1)
                            weight_group = 4;
                        if (weight >= 4.1 && weight < 4.45)
                            weight_group = 5;
                        if (weight >= 4.45 && weight < 4.7)
                            weight_group = 6;
                        if (weight >= 4.7 && weight < 5.1)
                            weight_group = 7;
                        if (weight >= 5.1)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 2)
                    {
                        if (growth < 53)
                            growth_group = 1;
                        if (growth >= 53 && growth < 55)
                            growth_group = 2;
                        if (growth >= 55 && growth < 56)
                            growth_group = 3;
                        if (growth >= 56 && growth < 57)
                            growth_group = 4;
                        if (growth >= 57 && growth < 59)
                            growth_group = 5;
                        if (growth >= 59 && growth < 60)
                            growth_group = 6;
                        if (growth >= 60 && growth < 62)
                            growth_group = 7;
                        if (growth >= 62)
                            growth_group = 8;

                        if (weight < 4.1)
                            weight_group = 1;
                        if (weight >= 4.1 && weight < 4.25)
                            weight_group = 2;
                        if (weight >= 4.25 && weight < 4.7)
                            weight_group = 3;
                        if (weight >= 4.7 && weight < 5.1)
                            weight_group = 4;
                        if (weight >= 5.1 && weight < 5.5)
                            weight_group = 5;
                        if (weight >= 5.5 && weight < 5.9)
                            weight_group = 6;
                        if (weight >= 5.9 && weight < 6.5)
                            weight_group = 7;
                        if (weight >= 6.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 3)
                    {
                        if (growth < 55)
                            growth_group = 1;
                        if (growth >= 55 && growth < 57)
                            growth_group = 2;
                        if (growth >= 57 && growth < 57)
                            growth_group = 3;
                        if (growth >= 59 && growth < 60)
                            growth_group = 4;
                        if (growth >= 60 && growth < 62)
                            growth_group = 5;
                        if (growth >= 62 && growth < 63)
                            growth_group = 6;
                        if (growth >= 63 && growth < 64)
                            growth_group = 7;
                        if (growth >= 64)
                            growth_group = 8;

                        if (weight < 4.6)
                            weight_group = 1;
                        if (weight >= 4.6 && weight < 5)
                            weight_group = 2;
                        if (weight >= 5 && weight < 5.5)
                            weight_group = 3;
                        if (weight >= 5.5 && weight < 5.9)
                            weight_group = 4;
                        if (weight >= 5.9 && weight < 6.3)
                            weight_group = 5;
                        if (weight >= 6.3 && weight < 6.8)
                            weight_group = 6;
                        if (weight >= 6.8 && weight < 7.2)
                            weight_group = 7;
                        if (weight >= 7.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 4)
                    {
                        if (growth < 58)
                            growth_group = 1;
                        if (growth >= 58 && growth < 60)
                            growth_group = 2;
                        if (growth >= 60 && growth < 62)
                            growth_group = 3;
                        if (growth >= 62 && growth < 62)
                            growth_group = 4;
                        if (growth >= 63 && growth < 64)
                            growth_group = 5;
                        if (growth >= 64 && growth < 65)
                            growth_group = 6;
                        if (growth >= 65 && growth < 66)
                            growth_group = 7;
                        if (growth >= 66)
                            growth_group = 8;

                        if (weight < 5.4)
                            weight_group = 1;
                        if (weight >= 5.4 && weight < 5.7)
                            weight_group = 2;
                        if (weight >= 5.7 && weight < 6.1)
                            weight_group = 3;
                        if (weight >= 6.1 && weight < 6.6)
                            weight_group = 4;
                        if (weight >= 6.6 && weight < 7.1)
                            weight_group = 5;
                        if (weight >= 7.1 && weight < 7.7)
                            weight_group = 6;
                        if (weight >= 7.7 && weight < 8.4)
                            weight_group = 7;
                        if (weight >= 8.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 5)
                    {
                        if (growth < 60)
                            growth_group = 1;
                        if (growth >= 60 && growth < 62)
                            growth_group = 2;
                        if (growth >= 62 && growth < 64)
                            growth_group = 3;
                        if (growth >= 64 && growth < 65)
                            growth_group = 4;
                        if (growth >= 65 && growth < 66)
                            growth_group = 5;
                        if (growth >= 66 && growth < 67)
                            growth_group = 6;
                        if (growth >= 67 && growth < 69)
                            growth_group = 7;
                        if (growth >= 69)
                            growth_group = 8;

                        if (weight < 6)
                            weight_group = 1;
                        if (weight >= 6 && weight < 6.2)
                            weight_group = 2;
                        if (weight >= 6.2 && weight < 6.5)
                            weight_group = 3;
                        if (weight >= 6.5 && weight < 7.1)
                            weight_group = 4;
                        if (weight >= 7.1 && weight < 7.7)
                            weight_group = 5;
                        if (weight >= 7.7 && weight < 8.5)
                            weight_group = 6;
                        if (weight >= 8.5 && weight < 9)
                            weight_group = 7;
                        if (weight >= 9)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 6)
                    {
                        if (growth < 62)
                            growth_group = 1;
                        if (growth >= 62 && growth < 64)
                            growth_group = 2;
                        if (growth >= 64 && growth < 65)
                            growth_group = 3;
                        if (growth >= 65 && growth < 66)
                            growth_group = 4;
                        if (growth >= 66 && growth < 68)
                            growth_group = 5;
                        if (growth >= 68 && growth < 69)
                            growth_group = 6;
                        if (growth >= 69 && growth < 70)
                            growth_group = 7;
                        if (growth >= 70)
                            growth_group = 8;

                        if (weight < 6.4)
                            weight_group = 1;
                        if (weight >= 6.4 && weight < 6.7)
                            weight_group = 2;
                        if (weight >= 6.7 && weight < 7.03)
                            weight_group = 3;
                        if (weight >= 7.03 && weight < 7.7)
                            weight_group = 4;
                        if (weight >= 7.7 && weight < 8.33)
                            weight_group = 5;
                        if (weight >= 8.33 && weight < 9)
                            weight_group = 6;
                        if (weight >= 9 && weight < 9.4)
                            weight_group = 7;
                        if (weight >= 9.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 7)
                    {
                        if (growth < 63)
                            growth_group = 1;
                        if (growth >= 63 && growth < 65)
                            growth_group = 2;
                        if (growth >= 65 && growth < 67)
                            growth_group = 3;
                        if (growth >= 67 && growth < 69)
                            growth_group = 4;
                        if (growth >= 69 && growth < 70)
                            growth_group = 5;
                        if (growth >= 70 && growth < 71)
                            growth_group = 6;
                        if (growth >= 71 && growth < 73)
                            growth_group = 7;
                        if (growth >= 73)
                            growth_group = 8;

                        if (weight < 6.8)
                            weight_group = 1;
                        if (weight >= 6.8 && weight < 7.2)
                            weight_group = 2;
                        if (weight >= 7.2 && weight < 7.48)
                            weight_group = 3;
                        if (weight >= 7.48 && weight < 8.1)
                            weight_group = 4;
                        if (weight >= 8.1 && weight < 8.93)
                            weight_group = 5;
                        if (weight >= 8.93 && weight < 9.4)
                            weight_group = 6;
                        if (weight >= 9.4 && weight < 10)
                            weight_group = 7;
                        if (weight >= 10)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 8)
                    {
                        if (growth < 65)
                            growth_group = 1;
                        if (growth >= 65 && growth < 66)
                            growth_group = 2;
                        if (growth >= 66 && growth < 69)
                            growth_group = 3;
                        if (growth >= 69 && growth < 70)
                            growth_group = 4;
                        if (growth >= 70 && growth < 72)
                            growth_group = 5;
                        if (growth >= 72 && growth < 73)
                            growth_group = 6;
                        if (growth >= 73 && growth < 74)
                            growth_group = 7;
                        if (growth >= 74)
                            growth_group = 8;

                        if (weight < 7.2)
                            weight_group = 1;
                        if (weight >= 7.2 && weight < 7.5)
                            weight_group = 2;
                        if (weight >= 7.5 && weight < 7.8)
                            weight_group = 3;
                        if (weight >= 7.8 && weight < 8.5)
                            weight_group = 4;
                        if (weight >= 8.5 && weight < 9.4)
                            weight_group = 5;
                        if (weight >= 9.4 && weight < 10)
                            weight_group = 6;
                        if (weight >= 10 && weight < 10.6)
                            weight_group = 7;
                        if (weight >= 10.6)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 9)
                    {
                        if (growth < 66)
                            growth_group = 1;
                        if (growth >= 66 && growth < 68)
                            growth_group = 2;
                        if (growth >= 68 && growth < 69)
                            growth_group = 3;
                        if (growth >= 69 && growth < 71)
                            growth_group = 4;
                        if (growth >= 71 && growth < 73)
                            growth_group = 5;
                        if (growth >= 73 && growth < 74)
                            growth_group = 6;
                        if (growth >= 74 && growth < 75)
                            growth_group = 7;
                        if (growth >= 75)
                            growth_group = 8;

                        if (weight < 7.5)
                            weight_group = 1;
                        if (weight >= 7.5 && weight < 7.9)
                            weight_group = 2;
                        if (weight >= 7.9 && weight < 8.2)
                            weight_group = 3;
                        if (weight >= 8.2 && weight < 9)
                            weight_group = 4;
                        if (weight >= 9 && weight < 9.55)
                            weight_group = 5;
                        if (weight >= 9.55 && weight < 10.2)
                            weight_group = 6;
                        if (weight >= 10.2 && weight < 10.7)
                            weight_group = 7;
                        if (weight >= 10.7)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 10)
                    {
                        if (growth < 66)
                            growth_group = 1;
                        if (growth >= 66 && growth < 69)
                            growth_group = 2;
                        if (growth >= 69 && growth < 72)
                            growth_group = 3;
                        if (growth >= 72 && growth < 73)
                            growth_group = 4;
                        if (growth >= 73 && growth < 75)
                            growth_group = 5;
                        if (growth >= 75 && growth < 76)
                            growth_group = 6;
                        if (growth >= 76 && growth < 77)
                            growth_group = 7;
                        if (growth >= 77)
                            growth_group = 8;

                        if (weight < 7.9)
                            weight_group = 1;
                        if (weight >= 7.9 && weight < 8.2)
                            weight_group = 2;
                        if (weight >= 8.2 && weight < 8.6)
                            weight_group = 3;
                        if (weight >= 8.6 && weight < 9.35)
                            weight_group = 4;
                        if (weight >= 9.35 && weight < 99)
                            weight_group = 5;
                        if (weight >= 9.9 && weight < 10.6)
                            weight_group = 6;
                        if (weight >= 10.6 && weight < 11.4)
                            weight_group = 7;
                        if (weight >= 11.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 11)
                    {
                        if (growth < 67)
                            growth_group = 1;
                        if (growth >= 67 && growth < 71)
                            growth_group = 2;
                        if (growth >= 71 && growth < 73)
                            growth_group = 3;
                        if (growth >= 73 && growth < 75)
                            growth_group = 4;
                        if (growth >= 75 && growth < 76)
                            growth_group = 5;
                        if (growth >= 76 && growth < 77)
                            growth_group = 6;
                        if (growth >= 77 && growth < 79)
                            growth_group = 7;
                        if (growth >= 79)
                            growth_group = 8;

                        if (weight < 8)
                            weight_group = 1;
                        if (weight >= 8 && weight < 8.5)
                            weight_group = 2;
                        if (weight >= 8.5 && weight < 8.96)
                            weight_group = 3;
                        if (weight >= 8.96 && weight < 9.64)
                            weight_group = 4;
                        if (weight >= 9.64 && weight < 10.5)
                            weight_group = 5;
                        if (weight >= 10.5 && weight < 11.2)
                            weight_group = 6;
                        if (weight >= 11.2 && weight < 11.5)
                            weight_group = 7;
                        if (weight >= 11.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 12)
                    {
                        if (growth < 70)
                            growth_group = 1;
                        if (growth >= 70 && growth < 72)
                            growth_group = 2;
                        if (growth >= 72 && growth < 73)
                            growth_group = 3;
                        if (growth >= 73 && growth < 75)
                            growth_group = 4;
                        if (growth >= 75 && growth < 77)
                            growth_group = 5;
                        if (growth >= 77 && growth < 78)
                            growth_group = 6;
                        if (growth >= 78 && growth < 80)
                            growth_group = 7;
                        if (growth >= 80)
                            growth_group = 8;

                        if (weight < 8.5)
                            weight_group = 1;
                        if (weight >= 8.5 && weight < 9)
                            weight_group = 2;
                        if (weight >= 9 && weight < 9.3)
                            weight_group = 3;
                        if (weight >= 9.3 && weight < 10)
                            weight_group = 4;
                        if (weight >= 10 && weight < 10.8)
                            weight_group = 5;
                        if (weight >= 10.8 && weight < 11.6)
                            weight_group = 6;
                        if (weight >= 11.6 && weight < 12.4)
                            weight_group = 7;
                        if (weight >= 12.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 13)
                    {
                        if (growth < 72)
                            growth_group = 1;
                        if (growth >= 72 && growth < 73)
                            growth_group = 2;
                        if (growth >= 73 && growth < 74)
                            growth_group = 3;
                        if (growth >= 74 && growth < 76.5)
                            growth_group = 4;
                        if (growth >= 76.5 && growth < 79.5)
                            growth_group = 5;
                        if (growth >= 79.5 && growth < 81)
                            growth_group = 6;
                        if (growth >= 81 && growth < 82)
                            growth_group = 7;
                        if (growth >= 82)
                            growth_group = 8;

                        if (weight < 8)
                            weight_group = 1;
                        if (weight >= 8 && weight < 9)
                            weight_group = 2;
                        if (weight >= 9 && weight < 9.4)
                            weight_group = 3;
                        if (weight >= 9.4 && weight < 10)
                            weight_group = 4;
                        if (weight >= 10 && weight < 11)
                            weight_group = 5;
                        if (weight >= 11 && weight < 11.6)
                            weight_group = 6;
                        if (weight >= 11.6 && weight < 12)
                            weight_group = 7;
                        if (weight >= 12)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 14)
                    {
                        if (growth < 75)
                            growth_group = 1;
                        if (growth >= 75 && growth < 76)
                            growth_group = 2;
                        if (growth >= 76 && growth < 78)
                            growth_group = 3;
                        if (growth >= 78 && growth < 80)
                            growth_group = 4;
                        if (growth >= 80 && growth < 83)
                            growth_group = 5;
                        if (growth >= 83 && growth < 86)
                            growth_group = 6;
                        if (growth >= 86 && growth < 88)
                            growth_group = 7;
                        if (growth >= 88)
                            growth_group = 8;

                        if (weight < 8.5)
                            weight_group = 1;
                        if (weight >= 8.5 && weight < 10)
                            weight_group = 2;
                        if (weight >= 10 && weight < 11)
                            weight_group = 3;
                        if (weight >= 11 && weight < 11.4)
                            weight_group = 4;
                        if (weight >= 11.4 && weight < 12)
                            weight_group = 5;
                        if (weight >= 12 && weight < 13)
                            weight_group = 6;
                        if (weight >= 13 && weight < 14)
                            weight_group = 7;
                        if (weight >= 14)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 15)
                    {
                        if (growth < 79)
                            growth_group = 1;
                        if (growth >= 79 && growth < 80)
                            growth_group = 2;
                        if (growth >= 80 && growth < 82)
                            growth_group = 3;
                        if (growth >= 82 && growth < 83)
                            growth_group = 4;
                        if (growth >= 83 && growth < 85)
                            growth_group = 5;
                        if (growth >= 85 && growth < 87)
                            growth_group = 6;
                        if (growth >= 87 && growth < 92)
                            growth_group = 7;
                        if (growth >= 92)
                            growth_group = 8;

                        if (weight < 10.2)
                            weight_group = 1;
                        if (weight >= 10.2 && weight < 10.5)
                            weight_group = 2;
                        if (weight >= 10.5 && weight < 11)
                            weight_group = 3;
                        if (weight >= 11 && weight < 12)
                            weight_group = 4;
                        if (weight >= 12 && weight < 12.8)
                            weight_group = 5;
                        if (weight >= 12.8 && weight < 13.5)
                            weight_group = 6;
                        if (weight >= 13.5 && weight < 14)
                            weight_group = 7;
                        if (weight >= 14)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 16)
                    {
                        if (growth < 78)
                            growth_group = 1;
                        if (growth >= 78 && growth < 82)
                            growth_group = 2;
                        if (growth >= 82 && growth < 83)
                            growth_group = 3;
                        if (growth >= 83 && growth < 86)
                            growth_group = 4;
                        if (growth >= 86 && growth < 89)
                            growth_group = 5;
                        if (growth >= 89 && growth < 91)
                            growth_group = 6;
                        if (growth >= 91 && growth < 93)
                            growth_group = 7;
                        if (growth >= 93)
                            growth_group = 8;

                        if (weight < 10)
                            weight_group = 1;
                        if (weight >= 10 && weight < 10.8)
                            weight_group = 2;
                        if (weight >= 10.8 && weight < 11.5)
                            weight_group = 3;
                        if (weight >= 11.5 && weight < 12.1)
                            weight_group = 4;
                        if (weight >= 12.1 && weight < 13)
                            weight_group = 5;
                        if (weight >= 13 && weight < 14)
                            weight_group = 6;
                        if (weight >= 14 && weight < 15.3)
                            weight_group = 7;
                        if (weight >= 15.3)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 17)
                    {
                        if (growth < 81)
                            growth_group = 1;
                        if (growth >= 81 && growth < 84)
                            growth_group = 2;
                        if (growth >= 84 && growth < 87.6)
                            growth_group = 3;
                        if (growth >= 87.6 && growth < 89.3)
                            growth_group = 4;
                        if (growth >= 89.3 && growth < 90.1)
                            growth_group = 5;
                        if (growth >= 90.1 && growth < 92.3)
                            growth_group = 6;
                        if (growth >= 92.3 && growth < 96.5)
                            growth_group = 7;
                        if (growth >= 96.5)
                            growth_group = 8;

                        if (weight < 10.5)
                            weight_group = 1;
                        if (weight >= 10.5 && weight < 10.9)
                            weight_group = 2;
                        if (weight >= 10.9 && weight < 11.8)
                            weight_group = 3;
                        if (weight >= 11.8 && weight < 12.7)
                            weight_group = 4;
                        if (weight >= 12.7 && weight < 13.6)
                            weight_group = 5;
                        if (weight >= 13.6 && weight < 14.5)
                            weight_group = 6;
                        if (weight >= 14.5 && weight < 15.6)
                            weight_group = 7;
                        if (weight >= 15.6)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 18)
                    {
                        if (growth < 83)
                            growth_group = 1;
                        if (growth >= 83 && growth < 86)
                            growth_group = 2;
                        if (growth >= 86 && growth < 88)
                            growth_group = 3;
                        if (growth >= 88 && growth < 91)
                            growth_group = 4;
                        if (growth >= 91 && growth < 92)
                            growth_group = 5;
                        if (growth >= 92 && growth < 95)
                            growth_group = 6;
                        if (growth >= 95 && growth < 98)
                            growth_group = 7;
                        if (growth >= 98)
                            growth_group = 8;

                        if (weight < 10.5)
                            weight_group = 1;
                        if (weight >= 10.5 && weight < 11)
                            weight_group = 2;
                        if (weight >= 11 && weight < 12)
                            weight_group = 3;
                        if (weight >= 12 && weight < 13)
                            weight_group = 4;
                        if (weight >= 13 && weight < 14)
                            weight_group = 5;
                        if (weight >= 14 && weight < 15)
                            weight_group = 6;
                        if (weight >= 15 && weight < 15.8)
                            weight_group = 7;
                        if (weight >= 15.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 19)
                    {
                        if (growth < 84)
                            growth_group = 1;
                        if (growth >= 84 && growth < 87.1)
                            growth_group = 2;
                        if (growth >= 87.1 && growth < 89.9)
                            growth_group = 3;
                        if (growth >= 89.9 && growth < 92.9)
                            growth_group = 4;
                        if (growth >= 92.9 && growth < 94.1)
                            growth_group = 5;
                        if (growth >= 94.1 && growth < 97.8)
                            growth_group = 6;
                        if (growth >= 97.8 && growth < 99.2)
                            growth_group = 7;
                        if (growth >= 99.2)
                            growth_group = 8;

                        if (weight < 11.2)
                            weight_group = 1;
                        if (weight >= 11.2 && weight < 12.1)
                            weight_group = 2;
                        if (weight >= 12.1 && weight < 12.8)
                            weight_group = 3;
                        if (weight >= 12.8 && weight < 13.5)
                            weight_group = 4;
                        if (weight >= 13.5 && weight < 14.1)
                            weight_group = 5;
                        if (weight >= 14.1 && weight < 15.6)
                            weight_group = 6;
                        if (weight >= 15.6 && weight < 16.8)
                            weight_group = 7;
                        if (weight >= 16.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 20)
                    {
                        if (growth < 86)
                            growth_group = 1;
                        if (growth >= 86)
                            growth_group = 2;
                        if (growth >= 89)
                            growth_group = 3;
                        if (growth >= 92)
                            growth_group = 4;
                        if (growth >= 95)
                            growth_group = 5;
                        if (growth >= 98)
                            growth_group = 6;
                        if (growth >= 102.5)
                            growth_group = 7;
                        if (growth >= 107)
                            growth_group = 8;

                        if (weight < 11.3)
                            weight_group = 1;
                        if (weight >= 11.3)
                            weight_group = 2;
                        if (weight >= 12.1)
                            weight_group = 3;
                        if (weight >= 13)
                            weight_group = 4;
                        if (weight >= 14.15)
                            weight_group = 5;
                        if (weight >= 15.5)
                            weight_group = 6;
                        if (weight >= 16.9)
                            weight_group = 7;
                        if (weight >= 19.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 21)
                    {
                        if (growth < 91)
                            growth_group = 1;
                        if (growth >= 91)
                            growth_group = 2;
                        if (growth >= 94)
                            growth_group = 3;
                        if (growth >= 96)
                            growth_group = 4;
                        if (growth >= 99)
                            growth_group = 5;
                        if (growth >= 102)
                            growth_group = 6;
                        if (growth >= 104)
                            growth_group = 7;
                        if (growth >= 107)
                            growth_group = 8;

                        if (weight < 12.7)
                            weight_group = 1;
                        if (weight >= 12.7)
                            weight_group = 2;
                        if (weight >= 13.5)
                            weight_group = 3;
                        if (weight >= 14)
                            weight_group = 4;
                        if (weight >= 15.25)
                            weight_group = 5;
                        if (weight >= 16.5)
                            weight_group = 6;
                        if (weight >= 18)
                            weight_group = 7;
                        if (weight >= 19.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 22)
                    {
                        if (growth < 94)
                            growth_group = 1;
                        if (growth >= 94)
                            growth_group = 2;
                        if (growth >= 98)
                            growth_group = 3;
                        if (growth >= 100)
                            growth_group = 4;
                        if (growth >= 103)
                            growth_group = 5;
                        if (growth >= 106.75)
                            growth_group = 6;
                        if (growth >= 109.5)
                            growth_group = 7;
                        if (growth >= 112)
                            growth_group = 8;

                        if (weight < 12.5)
                            weight_group = 1;
                        if (weight >= 12.5)
                            weight_group = 2;
                        if (weight >= 14)
                            weight_group = 3;
                        if (weight >= 15.05)
                            weight_group = 4;
                        if (weight >= 16.5)
                            weight_group = 5;
                        if (weight >= 18)
                            weight_group = 6;
                        if (weight >= 19.5)
                            weight_group = 7;
                        if (weight >= 22)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 23)
                    {
                        if (growth < 97)
                            growth_group = 1;
                        if (growth >= 97)
                            growth_group = 2;
                        if (growth >= 100)
                            growth_group = 3;
                        if (growth >= 103)
                            growth_group = 4;
                        if (growth >= 106)
                            growth_group = 5;
                        if (growth >= 110)
                            growth_group = 6;
                        if (growth >= 112)
                            growth_group = 7;
                        if (growth >= 116)
                            growth_group = 8;

                        if (weight < 13.7)
                            weight_group = 1;
                        if (weight >= 13.7)
                            weight_group = 2;
                        if (weight >= 14.6)
                            weight_group = 3;
                        if (weight >= 16)
                            weight_group = 4;
                        if (weight >= 17.1)
                            weight_group = 5;
                        if (weight >= 18.6)
                            weight_group = 6;
                        if (weight >= 20.4)
                            weight_group = 7;
                        if (weight >= 22.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 24)
                    {
                        if (growth < 102)
                            growth_group = 1;
                        if (growth >= 102)
                            growth_group = 2;
                        if (growth >= 104)
                            growth_group = 3;
                        if (growth >= 107)
                            growth_group = 4;
                        if (growth >= 111)
                            growth_group = 5;
                        if (growth >= 114)
                            growth_group = 6;
                        if (growth >= 116)
                            growth_group = 7;
                        if (growth >= 119)
                            growth_group = 8;

                        if (weight < 14.5)
                            weight_group = 1;
                        if (weight >= 14.5)
                            weight_group = 2;
                        if (weight >= 15.4)
                            weight_group = 3;
                        if (weight >= 16.8)
                            weight_group = 4;
                        if (weight >= 18.5)
                            weight_group = 5;
                        if (weight >= 20)
                            weight_group = 6;
                        if (weight >= 21.5)
                            weight_group = 7;
                        if (weight >= 23.3)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 25)
                    {
                        if (growth < 104)
                            growth_group = 1;
                        if (growth >= 104)
                            growth_group = 2;
                        if (growth >= 108)
                            growth_group = 3;
                        if (growth >= 111)
                            growth_group = 4;
                        if (growth >= 113)
                            growth_group = 5;
                        if (growth >= 117)
                            growth_group = 6;
                        if (growth >= 121)
                            growth_group = 7;
                        if (growth >= 123)
                            growth_group = 8;

                        if (weight < 15.3)
                            weight_group = 1;
                        if (weight >= 15.3)
                            weight_group = 2;
                        if (weight >= 16.2)
                            weight_group = 3;
                        if (weight >= 17.7)
                            weight_group = 4;
                        if (weight >= 19.5)
                            weight_group = 5;
                        if (weight >= 21)
                            weight_group = 6;
                        if (weight >= 23.5)
                            weight_group = 7;
                        if (weight >= 27)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 26)
                    {
                        if (growth < 107)
                            growth_group = 1;
                        if (growth >= 107)
                            growth_group = 2;
                        if (growth >= 110)
                            growth_group = 3;
                        if (growth >= 113)
                            growth_group = 4;
                        if (growth >= 117)
                            growth_group = 5;
                        if (growth >= 120)
                            growth_group = 6;
                        if (growth >= 123)
                            growth_group = 7;
                        if (growth >= 126)
                            growth_group = 8;

                        if (weight < 16)
                            weight_group = 1;
                        if (weight >= 16)
                            weight_group = 2;
                        if (weight >= 17.2)
                            weight_group = 3;
                        if (weight >= 18.45)
                            weight_group = 4;
                        if (weight >= 20.3)
                            weight_group = 5;
                        if (weight >= 22.1)
                            weight_group = 6;
                        if (weight >= 24.3)
                            weight_group = 7;
                        if (weight >= 27)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 27)
                    {
                        if (growth < 110)
                            growth_group = 1;
                        if (growth >= 110)
                            growth_group = 2;
                        if (growth >= 114)
                            growth_group = 3;
                        if (growth >= 116)
                            growth_group = 4;
                        if (growth >= 120)
                            growth_group = 5;
                        if (growth >= 123)
                            growth_group = 6;
                        if (growth >= 126)
                            growth_group = 7;
                        if (growth >= 130)
                            growth_group = 8;

                        if (weight < 16.5)
                            weight_group = 1;
                        if (weight >= 16.5)
                            weight_group = 2;
                        if (weight >= 17.9)
                            weight_group = 3;
                        if (weight >= 19.5)
                            weight_group = 4;
                        if (weight >= 21.2)
                            weight_group = 5;
                        if (weight >= 23.7)
                            weight_group = 6;
                        if (weight >= 26.2)
                            weight_group = 7;
                        if (weight >= 29.7)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 28)
                    {
                        if (growth < 113.6)
                            growth_group = 1;
                        if (growth >= 113.6)
                            growth_group = 2;
                        if (growth >= 117.7)
                            growth_group = 3;
                        if (growth >= 120.8)
                            growth_group = 4;
                        if (growth >= 124.6)
                            growth_group = 5;
                        if (growth >= 127.9)
                            growth_group = 6;
                        if (growth >= 130.6)
                            growth_group = 7;
                        if (growth >= 132.9)
                            growth_group = 8;

                        if (weight < 17.3)
                            weight_group = 1;
                        if (weight >= 17.3)
                            weight_group = 2;
                        if (weight >= 19.23)
                            weight_group = 3;
                        if (weight >= 21.7)
                            weight_group = 4;
                        if (weight >= 24.2)
                            weight_group = 5;
                        if (weight >= 26.2)
                            weight_group = 6;
                        if (weight >= 29.58)
                            weight_group = 7;
                        if (weight >= 33.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 29)
                    {
                        if (growth < 119.3)
                            growth_group = 1;
                        if (growth >= 119.3)
                            growth_group = 2;
                        if (growth >= 121.7)
                            growth_group = 3;
                        if (growth >= 125.6)
                            growth_group = 4;
                        if (growth >= 129.6)
                            growth_group = 5;
                        if (growth >= 132.7)
                            growth_group = 6;
                        if (growth >= 135.7)
                            growth_group = 7;
                        if (growth >= 141.1)
                            growth_group = 8;

                        if (weight < 19.5)
                            weight_group = 1;
                        if (weight >= 19.5)
                            weight_group = 2;
                        if (weight >= 21.36)
                            weight_group = 3;
                        if (weight >= 23.4)
                            weight_group = 4;
                        if (weight >= 26.2)
                            weight_group = 5;
                        if (weight >= 29.7)
                            weight_group = 6;
                        if (weight >= 35.3)
                            weight_group = 7;
                        if (weight >= 46.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 30)
                    {
                        if (growth < 122.8)
                            growth_group = 1;
                        if (growth >= 122.8)
                            growth_group = 2;
                        if (growth >= 127.2)
                            growth_group = 3;
                        if (growth >= 129.9)
                            growth_group = 4;
                        if (growth >= 133.4)
                            growth_group = 5;
                        if (growth >= 137.7)
                            growth_group = 6;
                        if (growth >= 142.8)
                            growth_group = 7;
                        if (growth >= 146.7)
                            growth_group = 8;

                        if (weight < 21.9)
                            weight_group = 1;
                        if (weight >= 21.9)
                            weight_group = 2;
                        if (weight >= 24.08)
                            weight_group = 3;
                        if (weight >= 25.7)
                            weight_group = 4;
                        if (weight >= 28.7)
                            weight_group = 5;
                        if (weight >= 31.9)
                            weight_group = 6;
                        if (weight >= 38.98)
                            weight_group = 7;
                        if (weight >= 43.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 31)
                    {
                        if (growth < 129)
                            growth_group = 1;
                        if (growth >= 129)
                            growth_group = 2;
                        if (growth >= 132.2)
                            growth_group = 3;
                        if (growth >= 135.6)
                            growth_group = 4;
                        if (growth >= 140.2)
                            growth_group = 5;
                        if (growth >= 145.3)
                            growth_group = 6;
                        if (growth >= 148.7)
                            growth_group = 7;
                        if (growth >= 152.8)
                            growth_group = 8;

                        if (weight < 24.4)
                            weight_group = 1;
                        if (weight >= 24.4)
                            weight_group = 2;
                        if (weight >= 25.84)
                            weight_group = 3;
                        if (weight >= 28.9)
                            weight_group = 4;
                        if (weight >= 33.4)
                            weight_group = 5;
                        if (weight >= 38.2)
                            weight_group = 6;
                        if (weight >= 43.82)
                            weight_group = 7;
                        if (weight >= 53.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 32)
                    {
                        if (growth < 130.7)
                            growth_group = 1;
                        if (growth >= 130.7)
                            growth_group = 2;
                        if (growth >= 137.3)
                            growth_group = 3;
                        if (growth >= 141.2)
                            growth_group = 4;
                        if (growth >= 146.5)
                            growth_group = 5;
                        if (growth >= 151.3)
                            growth_group = 6;
                        if (growth >= 154.5)
                            growth_group = 7;
                        if (growth >= 157.8)
                            growth_group = 8;

                        if (weight < 23.3)
                            weight_group = 1;
                        if (weight >= 23.3)
                            weight_group = 2;
                        if (weight >= 27.7)
                            weight_group = 3;
                        if (weight >= 31.6)
                            weight_group = 4;
                        if (weight >= 36.4)
                            weight_group = 5;
                        if (weight >= 41.9)
                            weight_group = 6;
                        if (weight >= 49.75)
                            weight_group = 7;
                        if (weight >= 56.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 33)
                    {
                        if (growth < 140.9)
                            growth_group = 1;
                        if (growth >= 140.9)
                            growth_group = 2;
                        if (growth >= 144.3)
                            growth_group = 3;
                        if (growth >= 147.9)
                            growth_group = 4;
                        if (growth >= 151.7)
                            growth_group = 5;
                        if (growth >= 156.7)
                            growth_group = 6;
                        if (growth >= 161.8)
                            growth_group = 7;
                        if (growth >= 165.8)
                            growth_group = 8;

                        if (weight < 31.2)
                            weight_group = 1;
                        if (weight >= 31.2)
                            weight_group = 2;
                        if (weight >= 32.8)
                            weight_group = 3;
                        if (weight >= 36.3)
                            weight_group = 4;
                        if (weight >= 41.9)
                            weight_group = 5;
                        if (weight >= 50.5)
                            weight_group = 6;
                        if (weight >= 58.5)
                            weight_group = 7;
                        if (weight >= 77.1)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 34)
                    {
                        if (growth < 144.4)
                            growth_group = 1;
                        if (growth >= 144.4)
                            growth_group = 2;
                        if (growth >= 150.4)
                            growth_group = 3;
                        if (growth >= 154.2)
                            growth_group = 4;
                        if (growth >= 158)
                            growth_group = 5;
                        if (growth >= 163.3)
                            growth_group = 6;
                        if (growth >= 168.5)
                            growth_group = 7;
                        if (growth >= 172.5)
                            growth_group = 8;

                        if (weight < 32.7)
                            weight_group = 1;
                        if (weight >= 32.7)
                            weight_group = 2;
                        if (weight >= 37.5)
                            weight_group = 3;
                        if (weight >= 42.6)
                            weight_group = 4;
                        if (weight >= 48.4)
                            weight_group = 5;
                        if (weight >= 54)
                            weight_group = 6;
                        if (weight >= 62.1)
                            weight_group = 7;
                        if (weight >= 69.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 35)
                    {
                        if (growth < 151.3)
                            growth_group = 1;
                        if (growth >= 151.3)
                            growth_group = 2;
                        if (growth >= 154.4)
                            growth_group = 3;
                        if (growth >= 157.9)
                            growth_group = 4;
                        if (growth >= 161.9)
                            growth_group = 5;
                        if (growth >= 165.9)
                            growth_group = 6;
                        if (growth >= 168.4)
                            growth_group = 7;
                        if (growth >= 173.8)
                            growth_group = 8;

                        if (weight < 38.3)
                            weight_group = 1;
                        if (weight >= 38.3)
                            weight_group = 2;
                        if (weight >= 42.1)
                            weight_group = 3;
                        if (weight >= 46.4)
                            weight_group = 4;
                        if (weight >= 51.7)
                            weight_group = 5;
                        if (weight >= 56.9)
                            weight_group = 6;
                        if (weight >= 65.5)
                            weight_group = 7;
                        if (weight >= 75.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 36)
                    {
                        if (growth < 151.1)
                            growth_group = 1;
                        if (growth >= 151.1)
                            growth_group = 2;
                        if (growth >= 153.9)
                            growth_group = 3;
                        if (growth >= 157.8)
                            growth_group = 4;
                        if (growth >= 161.8)
                            growth_group = 5;
                        if (growth >= 166.9)
                            growth_group = 6;
                        if (growth >= 171.8)
                            growth_group = 7;
                        if (growth >= 175.5)
                            growth_group = 8;

                        if (weight < 39.2)
                            weight_group = 1;
                        if (weight >= 39.2)
                            weight_group = 2;
                        if (weight >= 43.9)
                            weight_group = 3;
                        if (weight >= 47.4)
                            weight_group = 4;
                        if (weight >= 53.4)
                            weight_group = 5;
                        if (weight >= 59.3)
                            weight_group = 6;
                        if (weight >= 67.1)
                            weight_group = 7;
                        if (weight >= 78.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 37)
                    {
                        if (growth < 152.9)
                            growth_group = 1;
                        if (growth >= 152.9)
                            growth_group = 2;
                        if (growth >= 156.9)
                            growth_group = 3;
                        if (growth >= 159.4)
                            growth_group = 4;
                        if (growth >= 163.1)
                            growth_group = 5;
                        if (growth >= 167.7)
                            growth_group = 6;
                        if (growth >= 170.9)
                            growth_group = 7;
                        if (growth >= 176.5)
                            growth_group = 8;

                        if (weight < 40.9)
                            weight_group = 1;
                        if (weight >= 40.9)
                            weight_group = 2;
                        if (weight >= 44.7)
                            weight_group = 3;
                        if (weight >= 49.8)
                            weight_group = 4;
                        if (weight >= 54)
                            weight_group = 5;
                        if (weight >= 59.6)
                            weight_group = 6;
                        if (weight >= 64.9)
                            weight_group = 7;
                        if (weight >= 75)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 38 || age_group == 39 || age_group == 40)
                    {
                        if (growth < 152.1)
                            growth_group = 1;
                        if (growth >= 152.1)
                            growth_group = 2;
                        if (growth >= 155.1)
                            growth_group = 3;
                        if (growth >= 160.3)
                            growth_group = 4;
                        if (growth >= 164)
                            growth_group = 5;
                        if (growth >= 168.3)
                            growth_group = 6;
                        if (growth >= 173.3)
                            growth_group = 7;
                        if (growth >= 177.4)
                            growth_group = 8;

                        if (weight < 40.4)
                            weight_group = 1;
                        if (weight >= 40.4)
                            weight_group = 2;
                        if (weight >= 45.2)
                            weight_group = 3;
                        if (weight >= 49.8)
                            weight_group = 4;
                        if (weight >= 55.6)
                            weight_group = 5;
                        if (weight >= 61.5)
                            weight_group = 6;
                        if (weight >= 67.9)
                            weight_group = 7;
                        if (weight >= 78.9)
                            weight_group = 8;
                    }
                    //разделение для глаз
                }
                else
                {
                    //разделение для глаз
                    if (age_group == 1)
                    {
                        if (growth < 50)
                            growth_group = 1;
                        if (growth >= 50 && growth < 52)
                            growth_group = 2;
                        if (growth >= 52 && growth < 53)
                            growth_group = 3;
                        if (growth >= 53 && growth < 55)
                            growth_group = 4;
                        if (growth >= 55 && growth < 56)
                            growth_group = 5;
                        if (growth >= 56 && growth < 57)
                            growth_group = 6;
                        if (growth >= 57 && growth < 58)
                            growth_group = 7;
                        if (growth >= 58)
                            growth_group = 8;

                        if (weight < 2.95)
                            weight_group = 1;
                        if (weight >= 2.95 && weight < 3.35)
                            weight_group = 2;
                        if (weight >= 3.35 && weight < 3.75)
                            weight_group = 3;
                        if (weight >= 3.75 && weight < 4.15)
                            weight_group = 4;
                        if (weight >= 4.15 && weight < 4.51)
                            weight_group = 5;
                        if (weight >= 4.51 && weight < 5)
                            weight_group = 6;
                        if (weight >= 5 && weight < 5.3)
                            weight_group = 7;
                        if (weight >= 5.3)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 2)
                    {
                        if (growth < 55)
                            growth_group = 1;
                        if (growth >= 55 && growth < 56)
                            growth_group = 2;
                        if (growth >= 56 && growth < 57)
                            growth_group = 3;
                        if (growth >= 57 && growth < 59)
                            growth_group = 4;
                        if (growth >= 59 && growth < 60)
                            growth_group = 5;
                        if (growth >= 60 && growth < 61)
                            growth_group = 6;
                        if (growth >= 61 && growth < 62)
                            growth_group = 7;
                        if (growth >= 62)
                            growth_group = 8;

                        if (weight < 4.2)
                            weight_group = 1;
                        if (weight >= 4.2 && weight < 4.6)
                            weight_group = 2;
                        if (weight >= 4.6 && weight < 5)
                            weight_group = 3;
                        if (weight >= 5 && weight < 5.4)
                            weight_group = 4;
                        if (weight >= 5.4 && weight < 5.9)
                            weight_group = 5;
                        if (weight >= 5.9 && weight < 6.35)
                            weight_group = 6;
                        if (weight >= 6.35 && weight < 6.7)
                            weight_group = 7;
                        if (weight >= 6.7)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 3)
                    {
                        if (growth < 56)
                            growth_group = 1;
                        if (growth >= 56 && growth < 58)
                            growth_group = 2;
                        if (growth >= 58 && growth < 60)
                            growth_group = 3;
                        if (growth >= 60 && growth < 62)
                            growth_group = 4;
                        if (growth >= 62 && growth < 64)
                            growth_group = 5;
                        if (growth >= 64 && growth < 65)
                            growth_group = 6;
                        if (growth >= 65 && growth < 66)
                            growth_group = 7;
                        if (growth >= 66)
                            growth_group = 8;

                        if (weight < 4.5)
                            weight_group = 1;
                        if (weight >= 4.5 && weight < 5.2)
                            weight_group = 2;
                        if (weight >= 5.2 && weight < 5.7)
                            weight_group = 3;
                        if (weight >= 5.7 && weight < 6.3)
                            weight_group = 4;
                        if (weight >= 6.3 && weight < 6.8)
                            weight_group = 5;
                        if (weight >= 6.8 && weight < 7.2)
                            weight_group = 6;
                        if (weight >= 7.2 && weight < 7.8)
                            weight_group = 7;
                        if (weight >= 7.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 4)
                    {
                        if (growth < 59)
                            growth_group = 1;
                        if (growth >= 59 && growth < 61)
                            growth_group = 2;
                        if (growth >= 61 && growth < 63)
                            growth_group = 3;
                        if (growth >= 63 && growth < 65)
                            growth_group = 4;
                        if (growth >= 65 && growth < 66)
                            growth_group = 5;
                        if (growth >= 66 && growth < 67)
                            growth_group = 6;
                        if (growth >= 67 && growth < 68)
                            growth_group = 7;
                        if (growth >= 68)
                            growth_group = 8;

                        if (weight < 5.8)
                            weight_group = 1;
                        if (weight >= 5.8 && weight < 6.1)
                            weight_group = 2;
                        if (weight >= 6.1 && weight < 6.6)
                            weight_group = 3;
                        if (weight >= 6.6 && weight < 7.2)
                            weight_group = 4;
                        if (weight >= 7.2 && weight < 7.8)
                            weight_group = 5;
                        if (weight >= 7.8 && weight < 8.3)
                            weight_group = 6;
                        if (weight >= 8.3 && weight < 8.7)
                            weight_group = 7;
                        if (weight >= 8.7)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 5)
                    {
                        if (growth < 61)
                            growth_group = 1;
                        if (growth >= 61 && growth < 63)
                            growth_group = 2;
                        if (growth >= 63 && growth < 65)
                            growth_group = 3;
                        if (growth >= 65 && growth < 67)
                            growth_group = 4;
                        if (growth >= 67 && growth < 68.5)
                            growth_group = 5;
                        if (growth >= 68.5 && growth < 70)
                            growth_group = 6;
                        if (growth >= 70 && growth < 71)
                            growth_group = 7;
                        if (growth >= 71)
                            growth_group = 8;

                        if (weight < 6.25)
                            weight_group = 1;
                        if (weight >= 6.25 && weight < 6.82)
                            weight_group = 2;
                        if (weight >= 6.82 && weight < 7.25)
                            weight_group = 3;
                        if (weight >= 7.25 && weight < 7.8)
                            weight_group = 4;
                        if (weight >= 7.8 && weight < 8.4)
                            weight_group = 5;
                        if (weight >= 8.4 && weight < 9)
                            weight_group = 6;
                        if (weight >= 9 && weight < 9.3)
                            weight_group = 7;
                        if (weight >= 9.3)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 6)
                    {
                        if (growth < 63)
                            growth_group = 1;
                        if (growth >= 63 && growth < 65)
                            growth_group = 2;
                        if (growth >= 65 && growth < 66)
                            growth_group = 3;
                        if (growth >= 66 && growth < 68)
                            growth_group = 4;
                        if (growth >= 68 && growth < 70)
                            growth_group = 5;
                        if (growth >= 70 && growth < 71)
                            growth_group = 6;
                        if (growth >= 71 && growth < 72)
                            growth_group = 7;
                        if (growth >= 72)
                            growth_group = 8;

                        if (weight < 6.7)
                            weight_group = 1;
                        if (weight >= 6.7 && weight < 7.2)
                            weight_group = 2;
                        if (weight >= 7.2 && weight < 7.65)
                            weight_group = 3;
                        if (weight >= 7.65 && weight < 8.3)
                            weight_group = 4;
                        if (weight >= 8.3 && weight < 9)
                            weight_group = 5;
                        if (weight >= 9 && weight < 9.5)
                            weight_group = 6;
                        if (weight >= 9.5 && weight < 9.8)
                            weight_group = 7;
                        if (weight >= 9.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 7)
                    {
                        if (growth < 65)
                            growth_group = 1;
                        if (growth >= 65 && growth < 67)
                            growth_group = 2;
                        if (growth >= 67 && growth < 69)
                            growth_group = 3;
                        if (growth >= 69 && growth < 70)
                            growth_group = 4;
                        if (growth >= 70 && growth < 72)
                            growth_group = 5;
                        if (growth >= 72 && growth < 73)
                            growth_group = 6;
                        if (growth >= 73 && growth < 74)
                            growth_group = 7;
                        if (growth >= 74)
                            growth_group = 8;

                        if (weight < 7.25)
                            weight_group = 1;
                        if (weight >= 7.25 && weight < 8)
                            weight_group = 2;
                        if (weight >= 8 && weight < 8.4)
                            weight_group = 3;
                        if (weight >= 8.4 && weight < 9)
                            weight_group = 4;
                        if (weight >= 9 && weight < 9.5)
                            weight_group = 5;
                        if (weight >= 9.5 && weight < 10.1)
                            weight_group = 6;
                        if (weight >= 10.1 && weight < 10.8)
                            weight_group = 7;
                        if (weight >= 10.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 8)
                    {
                        if (growth < 67)
                            growth_group = 1;
                        if (growth >= 67 && growth < 69)
                            growth_group = 2;
                        if (growth >= 69 && growth < 71)
                            growth_group = 3;
                        if (growth >= 71 && growth < 72)
                            growth_group = 4;
                        if (growth >= 72 && growth < 73)
                            growth_group = 5;
                        if (growth >= 73 && growth < 74)
                            growth_group = 6;
                        if (growth >= 74 && growth < 76)
                            growth_group = 7;
                        if (growth >= 76)
                            growth_group = 8;

                        if (weight < 7.6)
                            weight_group = 1;
                        if (weight >= 7.6 && weight < 8.36)
                            weight_group = 2;
                        if (weight >= 8.36 && weight < 8.8)
                            weight_group = 3;
                        if (weight >= 8.8 && weight < 9.5)
                            weight_group = 4;
                        if (weight >= 9.5 && weight < 10)
                            weight_group = 5;
                        if (weight >= 10 && weight < 10.6)
                            weight_group = 6;
                        if (weight >= 10.6 && weight < 11.35)
                            weight_group = 7;
                        if (weight >= 11.35)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 9)
                    {
                        if (growth < 68)
                            growth_group = 1;
                        if (growth >= 68 && growth < 69)
                            growth_group = 2;
                        if (growth >= 69 && growth < 71)
                            growth_group = 3;
                        if (growth >= 71 && growth < 73)
                            growth_group = 4;
                        if (growth >= 73 && growth < 74)
                            growth_group = 5;
                        if (growth >= 74 && growth < 76)
                            growth_group = 6;
                        if (growth >= 76 && growth < 78)
                            growth_group = 7;
                        if (growth >= 78)
                            growth_group = 8;

                        if (weight < 7.9)
                            weight_group = 1;
                        if (weight >= 7.9 && weight < 8.3)
                            weight_group = 2;
                        if (weight >= 8.3 && weight < 9)
                            weight_group = 3;
                        if (weight >= 9 && weight < 9.6)
                            weight_group = 4;
                        if (weight >= 9.6 && weight < 10.1)
                            weight_group = 5;
                        if (weight >= 10.1 && weight < 10.6)
                            weight_group = 6;
                        if (weight >= 10.6 && weight < 11.5)
                            weight_group = 7;
                        if (weight >= 11.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 10)
                    {
                        if (growth < 70)
                            growth_group = 1;
                        if (growth >= 70 && growth < 71)
                            growth_group = 2;
                        if (growth >= 71 && growth < 73)
                            growth_group = 3;
                        if (growth >= 73 && growth < 75)
                            growth_group = 4;
                        if (growth >= 75 && growth < 76)
                            growth_group = 5;
                        if (growth >= 76 && growth < 78)
                            growth_group = 6;
                        if (growth >= 78 && growth < 79)
                            growth_group = 7;
                        if (growth >= 79)
                            growth_group = 8;

                        if (weight < 8.5)
                            weight_group = 1;
                        if (weight >= 8.5 && weight < 9)
                            weight_group = 2;
                        if (weight >= 9 && weight < 9.6)
                            weight_group = 3;
                        if (weight >= 9.6 && weight < 10)
                            weight_group = 4;
                        if (weight >= 10 && weight < 10.76)
                            weight_group = 5;
                        if (weight >= 10.76 && weight < 11.2)
                            weight_group = 6;
                        if (weight >= 11.2 && weight < 12.4)
                            weight_group = 7;
                        if (weight >= 12.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 11)
                    {
                        if (growth < 70)
                            growth_group = 1;
                        if (growth >= 70 && growth < 72)
                            growth_group = 2;
                        if (growth >= 72 && growth < 75)
                            growth_group = 3;
                        if (growth >= 75 && growth < 76)
                            growth_group = 4;
                        if (growth >= 76 && growth < 78)
                            growth_group = 5;
                        if (growth >= 78 && growth < 79)
                            growth_group = 6;
                        if (growth >= 79 && growth < 80)
                            growth_group = 7;
                        if (growth >= 80)
                            growth_group = 8;

                        if (weight < 8.3)
                            weight_group = 1;
                        if (weight >= 8.3 && weight < 9.2)
                            weight_group = 2;
                        if (weight >= 9.2 && weight < 9.85)
                            weight_group = 3;
                        if (weight >= 9.85 && weight < 10.55)
                            weight_group = 4;
                        if (weight >= 10.55 && weight < 11.11)
                            weight_group = 5;
                        if (weight >= 11.11 && weight < 11.7)
                            weight_group = 6;
                        if (weight >= 11.7 && weight < 12.1)
                            weight_group = 7;
                        if (weight >= 12.1)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 12)
                    {
                        if (growth < 71)
                            growth_group = 1;
                        if (growth >= 71 && growth < 73)
                            growth_group = 2;
                        if (growth >= 73 && growth < 75)
                            growth_group = 3;
                        if (growth >= 75 && growth < 77)
                            growth_group = 4;
                        if (growth >= 77 && growth < 79)
                            growth_group = 5;
                        if (growth >= 79 && growth < 80)
                            growth_group = 6;
                        if (growth >= 80 && growth < 82)
                            growth_group = 7;
                        if (growth >= 82)
                            growth_group = 8;

                        if (weight < 8.8)
                            weight_group = 1;
                        if (weight >= 8.8 && weight < 9.4)
                            weight_group = 2;
                        if (weight >= 9.4 && weight < 10.2)
                            weight_group = 3;
                        if (weight >= 10.2 && weight < 10.9)
                            weight_group = 4;
                        if (weight >= 10.9 && weight < 11.5)
                            weight_group = 5;
                        if (weight >= 11.5 && weight < 12.2)
                            weight_group = 6;
                        if (weight >= 12.2 && weight < 13)
                            weight_group = 7;
                        if (weight >= 13)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    if (age_group == 13)
                    {
                        if (growth < 72)
                            growth_group = 1;
                        if (growth >= 72 && growth < 73)
                            growth_group = 2;
                        if (growth >= 73 && growth < 77)
                            growth_group = 3;
                        if (growth >= 77 && growth < 78)
                            growth_group = 4;
                        if (growth >= 78 && growth < 80)
                            growth_group = 5;
                        if (growth >= 80 && growth < 85)
                            growth_group = 6;
                        if (growth >= 85 && growth < 87)
                            growth_group = 7;
                        if (growth >= 87)
                            growth_group = 8;

                        if (weight < 9.3)
                            weight_group = 1;
                        if (weight >= 9.3 && weight < 9.7)
                            weight_group = 2;
                        if (weight >= 9.7 && weight < 10.2)
                            weight_group = 3;
                        if (weight >= 10.2 && weight < 10.8)
                            weight_group = 4;
                        if (weight >= 10.8 && weight < 11.2)
                            weight_group = 5;
                        if (weight >= 11.2 && weight < 12)
                            weight_group = 6;
                        if (weight >= 12 && weight < 12.8)
                            weight_group = 7;
                        if (weight >= 12.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 14)
                    {
                        if (growth < 76)
                            growth_group = 1;
                        if (growth >= 76 && growth < 77)
                            growth_group = 2;
                        if (growth >= 77 && growth < 80)
                            growth_group = 3;
                        if (growth >= 80 && growth < 81.5)
                            growth_group = 4;
                        if (growth >= 81.5 && growth < 85)
                            growth_group = 5;
                        if (growth >= 85 && growth < 87)
                            growth_group = 6;
                        if (growth >= 87 && growth < 89)
                            growth_group = 7;
                        if (growth >= 89)
                            growth_group = 8;

                        if (weight < 9.3)
                            weight_group = 1;
                        if (weight >= 9.3 && weight < 9.6)
                            weight_group = 2;
                        if (weight >= 9.6 && weight < 10.85)
                            weight_group = 3;
                        if (weight >= 10.85 && weight < 11.5)
                            weight_group = 4;
                        if (weight >= 11.5 && weight < 12.4)
                            weight_group = 5;
                        if (weight >= 12.4 && weight < 13)
                            weight_group = 6;
                        if (weight >= 13 && weight < 13.2)
                            weight_group = 7;
                        if (weight >= 13.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 15)
                    {
                        if (growth < 78)
                            growth_group = 1;
                        if (growth >= 78 && growth < 80)
                            growth_group = 2;
                        if (growth >= 80 && growth < 82)
                            growth_group = 3;
                        if (growth >= 82 && growth < 84.5)
                            growth_group = 4;
                        if (growth >= 84.5 && growth < 86)
                            growth_group = 5;
                        if (growth >= 86 && growth < 89)
                            growth_group = 6;
                        if (growth >= 89 && growth < 91)
                            growth_group = 7;
                        if (growth >= 91)
                            growth_group = 8;

                        if (weight < 9)
                            weight_group = 1;
                        if (weight >= 9 && weight < 11)
                            weight_group = 2;
                        if (weight >= 11 && weight < 11.5)
                            weight_group = 3;
                        if (weight >= 11.5 && weight < 12)
                            weight_group = 4;
                        if (weight >= 12 && weight < 13)
                            weight_group = 5;
                        if (weight >= 13 && weight < 14)
                            weight_group = 6;
                        if (weight >= 14 && weight < 14.5)
                            weight_group = 7;
                        if (weight >= 14.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 16)
                    {
                        if (growth < 80)
                            growth_group = 1;
                        if (growth >= 80 && growth < 82)
                            growth_group = 2;
                        if (growth >= 82 && growth < 83)
                            growth_group = 3;
                        if (growth >= 83 && growth < 86)
                            growth_group = 4;
                        if (growth >= 86 && growth < 90)
                            growth_group = 5;
                        if (growth >= 90 && growth < 93)
                            growth_group = 6;
                        if (growth >= 93 && growth < 96)
                            growth_group = 7;
                        if (growth >= 96)
                            growth_group = 8;

                        if (weight < 9.7)
                            weight_group = 1;
                        if (weight >= 9.7 && weight < 10.3)
                            weight_group = 2;
                        if (weight >= 10.3 && weight < 11.4)
                            weight_group = 3;
                        if (weight >= 11.4 && weight < 13)
                            weight_group = 4;
                        if (weight >= 13 && weight < 13.8)
                            weight_group = 5;
                        if (weight >= 13.8 && weight < 15)
                            weight_group = 6;
                        if (weight >= 15 && weight < 15.5)
                            weight_group = 7;
                        if (weight >= 15.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 17)
                    {
                        if (growth < 81)
                            growth_group = 1;
                        if (growth >= 81 && growth < 83)
                            growth_group = 2;
                        if (growth >= 83 && growth < 86)
                            growth_group = 3;
                        if (growth >= 86 && growth < 89)
                            growth_group = 4;
                        if (growth >= 89 && growth < 92)
                            growth_group = 5;
                        if (growth >= 92 && growth < 95)
                            growth_group = 6;
                        if (growth >= 95 && growth < 98)
                            growth_group = 7;
                        if (growth >= 98)
                            growth_group = 8;

                        if (weight < 10.5)
                            weight_group = 1;
                        if (weight >= 10.5 && weight < 10.7)
                            weight_group = 2;
                        if (weight >= 10.7 && weight < 12.3)
                            weight_group = 3;
                        if (weight >= 12.3 && weight < 13.5)
                            weight_group = 4;
                        if (weight >= 13.5 && weight < 14.1)
                            weight_group = 5;
                        if (weight >= 14.1 && weight < 15.6)
                            weight_group = 6;
                        if (weight >= 15.6 && weight < 16.1)
                            weight_group = 7;
                        if (weight >= 16.1)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 18)
                    {
                        if (growth < 83)
                            growth_group = 1;
                        if (growth >= 83 && growth < 86)
                            growth_group = 2;
                        if (growth >= 86 && growth < 89)
                            growth_group = 3;
                        if (growth >= 89 && growth < 92)
                            growth_group = 4;
                        if (growth >= 92 && growth < 94)
                            growth_group = 5;
                        if (growth >= 94 && growth < 97)
                            growth_group = 6;
                        if (growth >= 97 && growth < 100)
                            growth_group = 7;
                        if (growth >= 100)
                            growth_group = 8;

                        if (weight < 11)
                            weight_group = 1;
                        if (weight >= 11 && weight < 12)
                            weight_group = 2;
                        if (weight >= 12 && weight < 12.7)
                            weight_group = 3;
                        if (weight >= 12.7 && weight < 13.8)
                            weight_group = 4;
                        if (weight >= 13.8 && weight < 14.6)
                            weight_group = 5;
                        if (weight >= 14.6 && weight < 16)
                            weight_group = 6;
                        if (weight >= 16 && weight < 16.5)
                            weight_group = 7;
                        if (weight >= 16.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 19)
                    {
                        if (growth < 84)
                            growth_group = 1;
                        if (growth >= 84 && growth < 87)
                            growth_group = 2;
                        if (growth >= 87 && growth < 90)
                            growth_group = 3;
                        if (growth >= 90 && growth < 93)
                            growth_group = 4;
                        if (growth >= 93 && growth < 96)
                            growth_group = 5;
                        if (growth >= 96 && growth < 98)
                            growth_group = 6;
                        if (growth >= 98 && growth < 105)
                            growth_group = 7;
                        if (growth >= 105)
                            growth_group = 8;

                        if (weight < 11.6)
                            weight_group = 1;
                        if (weight >= 11.6 && weight < 12.5)
                            weight_group = 2;
                        if (weight >= 12.5 && weight < 13.1)
                            weight_group = 3;
                        if (weight >= 13.1 && weight < 14.2)
                            weight_group = 4;
                        if (weight >= 14.2 && weight < 15)
                            weight_group = 5;
                        if (weight >= 15 && weight < 16.1)
                            weight_group = 6;
                        if (weight >= 16.1 && weight < 17.6)
                            weight_group = 7;
                        if (weight >= 17.6)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 20)
                    {
                        if (growth < 88)
                            growth_group = 1;
                        if (growth >= 88 && growth < 90)
                            growth_group = 2;
                        if (growth >= 90 && growth < 93)
                            growth_group = 3;
                        if (growth >= 93 && growth < 96)
                            growth_group = 4;
                        if (growth >= 96 && growth < 99)
                            growth_group = 5;
                        if (growth >= 99 && growth < 101)
                            growth_group = 6;
                        if (growth >= 101 && growth < 104)
                            growth_group = 7;
                        if (growth >= 104)
                            growth_group = 8;

                        if (weight < 12)
                            weight_group = 1;
                        if (weight >= 12 && weight < 12.85)
                            weight_group = 2;
                        if (weight >= 12.85 && weight < 13.6)
                            weight_group = 3;
                        if (weight >= 13.6 && weight < 14.95)
                            weight_group = 4;
                        if (weight >= 14.95 && weight < 16)
                            weight_group = 5;
                        if (weight >= 16 && weight < 16.9)
                            weight_group = 6;
                        if (weight >= 16.9 && weight < 18)
                            weight_group = 7;
                        if (weight >= 18)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 21)
                    {
                        if (growth < 91)
                            growth_group = 1;
                        if (growth >= 91 && growth < 94)
                            growth_group = 2;
                        if (growth >= 94 && growth < 96.5)
                            growth_group = 3;
                        if (growth >= 96.5 && growth < 100)
                            growth_group = 4;
                        if (growth >= 100 && growth < 103)
                            growth_group = 5;
                        if (growth >= 103 && growth < 105)
                            growth_group = 6;
                        if (growth >= 105 && growth < 108)
                            growth_group = 7;
                        if (growth >=108 )
                            growth_group = 8;

                        if (weight < 12.7)
                            weight_group = 1;
                        if (weight >= 12.7 && weight < 12.7)
                            weight_group = 2;
                        if (weight >= 13.6 && weight < 13.6)
                            weight_group = 3;
                        if (weight >= 14.7 && weight < 14.7)
                            weight_group = 4;
                        if (weight >= 16 && weight < 17.3)
                            weight_group = 5;
                        if (weight >= 17.3 && weight < 18.8)
                            weight_group = 6;
                        if (weight >= 18.8 && weight < 20)
                            weight_group = 7;
                        if (weight >= 20)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 22)
                    {
                        if (growth < 94)
                            growth_group = 1;
                        if (growth >= 94 && growth < 97.5)
                            growth_group = 2;
                        if (growth >= 97.5 && growth < 100)
                            growth_group = 3;
                        if (growth >= 100 && growth < 103)
                            growth_group = 4;
                        if (growth >= 103 && growth < 106)
                            growth_group = 5;
                        if (growth >= 106 && growth < 108)
                            growth_group = 6;
                        if (growth >= 108 && growth < 111)
                            growth_group = 7;
                        if (growth >= 111)
                            growth_group = 8;

                        if (weight < 13.5)
                            weight_group = 1;
                        if (weight >= 13.5 && weight < 14)
                            weight_group = 2;
                        if (weight >= 14 && weight < 15)
                            weight_group = 3;
                        if (weight >= 15 && weight < 16.5)
                            weight_group = 4;
                        if (weight >= 16.5 && weight < 18)
                            weight_group = 5;
                        if (weight >= 18 && weight < 19.5)
                            weight_group = 6;
                        if (weight >= 19.5 && weight < 21)
                            weight_group = 7;
                        if (weight >= 21)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 23)
                    {
                        if (growth < 98)
                            growth_group = 1;
                        if (growth >= 98 && growth < 101)
                            growth_group = 2;
                        if (growth >= 101 && growth < 103.5)
                            growth_group = 3;
                        if (growth >= 103.5 && growth < 107)
                            growth_group = 4;
                        if (growth >= 107 && growth < 110)
                            growth_group = 5;
                        if (growth >= 110 && growth < 113)
                            growth_group = 6;
                        if (growth >= 113 && growth < 118)
                            growth_group = 7;
                        if (growth >= 118)
                            growth_group = 8;

                        if (weight < 14.2)
                            weight_group = 1;
                        if (weight >= 14.2 && weight < 15.2)
                            weight_group = 2;
                        if (weight >= 15.2 && weight < 16.3)
                            weight_group = 3;
                        if (weight >= 16.3 && weight < 17.8)
                            weight_group = 4;
                        if (weight >= 17.8 && weight < 19.5)
                            weight_group = 5;
                        if (weight >= 19.5 && weight < 21.1)
                            weight_group = 6;
                        if (weight >= 21.1 && weight < 25.2)
                            weight_group = 7;
                        if (weight >= 25.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 24)
                    {
                        if (growth < 102)
                            growth_group = 1;
                        if (growth >= 102 && growth < 104)
                            growth_group = 2;
                        if (growth >= 104 && growth < 107)
                            growth_group = 3;
                        if (growth >= 107 && growth < 110)
                            growth_group = 4;
                        if (growth >= 110 && growth < 113)
                            growth_group = 5;
                        if (growth >= 113 && growth < 115)
                            growth_group = 6;
                        if (growth >= 115 && growth < 118)
                            growth_group = 7;
                        if (growth >= 118)
                            growth_group = 8;

                        if (weight < 15)
                            weight_group = 1;
                        if (weight >= 15 && weight < 16)
                            weight_group = 2;
                        if (weight >= 16 && weight < 17)
                            weight_group = 3;
                        if (weight >= 17 && weight < 18.5)
                            weight_group = 4;
                        if (weight >= 18.5 && weight < 20.1)
                            weight_group = 5;
                        if (weight >= 20.1 && weight < 21.5)
                            weight_group = 6;
                        if (weight >= 21.5 && weight < 24)
                            weight_group = 7;
                        if (weight >= 24)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 25)
                    {
                        if (growth < 104)
                            growth_group = 1;
                        if (growth >= 104 && growth < 107)
                            growth_group = 2;
                        if (growth >= 107 && growth < 110)
                            growth_group = 3;
                        if (growth >= 110 && growth < 114)
                            growth_group = 4;
                        if (growth >= 114 && growth < 117)
                            growth_group = 5;
                        if (growth >= 117 && growth < 120)
                            growth_group = 6;
                        if (growth >= 120 && growth < 124)
                            growth_group = 7;
                        if (growth >= 124)
                            growth_group = 8;

                        if (weight < 15.5)
                            weight_group = 1;
                        if (weight >= 15.5 && weight < 16.8)
                            weight_group = 2;
                        if (weight >= 16.8 && weight < 18.2)
                            weight_group = 3;
                        if (weight >= 18.2 && weight < 19.5)
                            weight_group = 4;
                        if (weight >= 19.5 && weight < 21.2)
                            weight_group = 5;
                        if (weight >= 21.2 && weight < 23.1)
                            weight_group = 6;
                        if (weight >= 23.1 && weight < 26.5)
                            weight_group = 7;
                        if (weight >= 26.5)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 26)
                    {
                        if (growth < 108)
                            growth_group = 1;
                        if (growth >= 108 && growth < 110)
                            growth_group = 2;
                        if (growth >= 110 && growth < 113)
                            growth_group = 3;
                        if (growth >= 113 && growth < 117)
                            growth_group = 4;
                        if (growth >= 117 && growth < 121)
                            growth_group = 5;
                        if (growth >= 121 && growth < 124)
                            growth_group = 6;
                        if (growth >= 124 && growth < 127)
                            growth_group = 7;
                        if (growth >= 127)
                            growth_group = 8;

                        if (weight < 16.4)
                            weight_group = 1;
                        if (weight >= 16.4 && weight < 17.7)
                            weight_group = 2;
                        if (weight >= 17.7 && weight < 19)
                            weight_group = 3;
                        if (weight >= 19 && weight < 20.8)
                            weight_group = 4;
                        if (weight >= 20.8 && weight < 22.6)
                            weight_group = 5;
                        if (weight >= 22.6 && weight < 25)
                            weight_group = 6;
                        if (weight >= 25 && weight < 27.1)
                            weight_group = 7;
                        if (weight >= 27.1)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 27)
                    {
                        if (growth < 111)
                            growth_group = 1;
                        if (growth >= 111 && growth < 114)
                            growth_group = 2;
                        if (growth >= 114 && growth < 117)
                            growth_group = 3;
                        if (growth >= 117 && growth < 120)
                            growth_group = 4;
                        if (growth >= 120 && growth < 124)
                            growth_group = 5;
                        if (growth >= 124 && growth < 127)
                            growth_group = 6;
                        if (growth >= 127 && growth < 131)
                            growth_group = 7;
                        if (growth >= 131)
                            growth_group = 8;

                        if (weight < 17.2)
                            weight_group = 1;
                        if (weight >= 17.2 && weight < 18.6)
                            weight_group = 2;
                        if (weight >= 18.6 && weight < 20.2)
                            weight_group = 3;
                        if (weight >= 20.2 && weight < 22.4)
                            weight_group = 4;
                        if (weight >= 22.4 && weight < 24.3)
                            weight_group = 5;
                        if (weight >= 24.3 && weight < 27)
                            weight_group = 6;
                        if (weight >= 27 && weight < 30)
                            weight_group = 7;
                        if (weight >= 30)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 28)
                    {
                        if (growth < 115.3)
                            growth_group = 1;
                        if (growth >= 115.3 && growth < 118.3)
                            growth_group = 2;
                        if (growth >= 118.3 && growth < 121.96)
                            growth_group = 3;
                        if (growth >= 121.96 && growth < 125.8)
                            growth_group = 4;
                        if (growth >= 125.8 && growth < 128.7)
                            growth_group = 5;
                        if (growth >= 128.7 && growth < 132.25)
                            growth_group = 6;
                        if (growth >= 132.25 && growth < 134.5)
                            growth_group = 7;
                        if (growth >= 134.5)
                            growth_group = 8;

                        if (weight < 19)
                            weight_group = 1;
                        if (weight >= 19 && weight < 19.78)
                            weight_group = 2;
                        if (weight >= 19.78 && weight < 21.9)
                            weight_group = 3;
                        if (weight >= 21.9 && weight < 24.59)
                            weight_group = 4;
                        if (weight >= 24.59 && weight < 27.5)
                            weight_group = 5;
                        if (weight >= 27.5 && weight < 31.25)
                            weight_group = 6;
                        if (weight >= 31.25 && weight < 33.9)
                            weight_group = 7;
                        if (weight >= 33.9)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 29)
                    {
                        if (growth < 119.1)
                            growth_group = 1;
                        if (growth >= 119.1 && growth < 121.2)
                            growth_group = 2;
                        if (growth >= 121.2 && growth < 125.8)
                            growth_group = 3;
                        if (growth >= 125.8 && growth < 129.8)
                            growth_group = 4;
                        if (growth >= 129.8 && growth < 134.2)
                            growth_group = 5;
                        if (growth >= 134.2 && growth < 137.9)
                            growth_group = 6;
                        if (growth >= 137.9 && growth < 140.8)
                            growth_group = 7;
                        if (growth >= 140.8)
                            growth_group = 8;

                        if (weight < 21.73)
                            weight_group = 1;
                        if (weight >= 21.73 && weight < 22.9)
                            weight_group = 2;
                        if (weight >= 22.9 && weight < 24.3)
                            weight_group = 3;
                        if (weight >= 24.3 && weight < 26.8)
                            weight_group = 4;
                        if (weight >= 26.8 && weight < 29.9)
                            weight_group = 5;
                        if (weight >= 29.9 && weight < 35.4)
                            weight_group = 6;
                        if (weight >=35.4  && weight < 41.8)
                            weight_group = 7;
                        if (weight >= 41.8)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 30)
                    {
                        if (growth < 124.9)
                            growth_group = 1;
                        if (growth >= 124.9 && growth < 129.4)
                            growth_group = 2;
                        if (growth >= 129.4 && growth < 131.9)
                            growth_group = 3;
                        if (growth >= 131.9 && growth < 136.7)
                            growth_group = 4;
                        if (growth >= 136.7 && growth < 140.9)
                            growth_group = 5;
                        if (growth >= 140.9 && growth < 145.2)
                            growth_group = 6;
                        if (growth >= 145.2 && growth < 149)
                            growth_group = 7;
                        if (growth >= 149)
                            growth_group = 8;

                        if (weight < 22.9)
                            weight_group = 1;
                        if (weight >= 22.9 && weight < 24.78)
                            weight_group = 2;
                        if (weight >= 24.78 && weight < 27.9)
                            weight_group = 3;
                        if (weight >= 27.9 && weight < 30.75)
                            weight_group = 4;
                        if (weight >= 30.75 && weight <37.1 )
                            weight_group = 5;
                        if (weight >= 37.1 && weight < 42.32)
                            weight_group = 6;
                        if (weight >= 42.32 && weight < 52.2)
                            weight_group = 7;
                        if (weight >= 52.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 31)
                    {
                        if (growth < 130.9)
                            growth_group = 1;
                        if (growth >= 130.9 && growth < 132.9)
                            growth_group = 2;
                        if (growth >= 132.9 && growth < 136.8)
                            growth_group = 3;
                        if (growth >= 136.8 && growth < 140.2)
                            growth_group = 4;
                        if (growth >= 140.2 && growth < 144.6)
                            growth_group = 5;
                        if (growth >= 144.6 && growth < 147.7)
                            growth_group = 6;
                        if (growth >= 147.7 && growth < 150.7)
                            growth_group = 7;
                        if (growth >= 150.7)
                            growth_group = 8;

                        if (weight < 25.4)
                            weight_group = 1;
                        if (weight >= 25.4 && weight < 26.98)
                            weight_group = 2;
                        if (weight >= 26.98 && weight < 29.6)
                            weight_group = 3;
                        if (weight >= 29.6 && weight < 32.8)
                            weight_group = 4;
                        if (weight >= 32.8 && weight < 39.1)
                            weight_group = 5;
                        if (weight >= 39.1 && weight < 50.1)
                            weight_group = 6;
                        if (weight >= 50.1 && weight < 56.6)
                            weight_group = 7;
                        if (weight >= 56.6)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 32)
                    {
                        if (growth < 134)
                            growth_group = 1;
                        if (growth >= 134 && growth < 136.8)
                            growth_group = 2;
                        if (growth >= 136.8 && growth < 142.6)
                            growth_group = 3;
                        if (growth >= 142.6 && growth < 147)
                            growth_group = 4;
                        if (growth >= 147 && growth <150.3 )
                            growth_group = 5;
                        if (growth >= 150.3 && growth < 155.1)
                            growth_group = 6;
                        if (growth >= 155.1 && growth < 160.2)
                            growth_group = 7;
                        if (growth >= 160.2)
                            growth_group = 8;

                        if (weight < 27.3)
                            weight_group = 1;
                        if (weight >= 27.3 && weight < 29.33)
                            weight_group = 2;
                        if (weight >= 29.33 && weight < 33.3)
                            weight_group = 3;
                        if (weight >= 33.3 && weight < 38.4)
                            weight_group = 4;
                        if (weight >= 38.4 && weight < 45.5)
                            weight_group = 5;
                        if (weight >= 45.5 && weight < 53.7)
                            weight_group = 6;
                        if (weight >= 53.7 && weight < 63.4)
                            weight_group = 7;
                        if (weight >= 63.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 33)
                    {
                        if (growth < 137.6)
                            growth_group = 1;
                        if (growth >= 137.6 && growth < 142.4)
                            growth_group = 2;
                        if (growth >= 142.4 && growth < 146.5)
                            growth_group = 3;
                        if (growth >= 146.5 && growth < 151.5)
                            growth_group = 4;
                        if (growth >= 151.5 && growth < 156.3)
                            growth_group = 5;
                        if (growth >= 156.3 && growth < 161.4)
                            growth_group = 6;
                        if (growth >= 161.4 && growth < 164.4)
                            growth_group = 7;
                        if (growth >= 164.4)
                            growth_group = 8;

                        if (weight < 29.2)
                            weight_group = 1;
                        if (weight >= 29.2 && weight < 32.37)
                            weight_group = 2;
                        if (weight >= 32.37 && weight < 36.5)
                            weight_group = 3;
                        if (weight >= 36.5 && weight < 41.3)
                            weight_group = 4;
                        if (weight >= 41.3 && weight < 50.6)
                            weight_group = 5;
                        if (weight >= 50.6 && weight < 63.9)
                            weight_group = 6;
                        if (weight >= 63.9 && weight < 75.4)
                            weight_group = 7;
                        if (weight >= 75.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 34)
                    {
                        if (growth < 143.8)
                            growth_group = 1;
                        if (growth >= 143.8)
                            growth_group = 2;
                        if (growth >= 146.6)
                            growth_group = 3;
                        if (growth >= 151.9)
                            growth_group = 4;
                        if (growth >= 159.3)
                            growth_group = 5;
                        if (growth >= 164.6)
                            growth_group = 6;
                        if (growth >= 168.3)
                            growth_group = 7;
                        if (growth >= 172.2)
                            growth_group = 8;

                        if (weight < 31.9)
                            weight_group = 1;
                        if (weight >= 31.9)
                            weight_group = 2;
                        if (weight >= 35.34)
                            weight_group = 3;
                        if (weight >= 40.4)
                            weight_group = 4;
                        if (weight >= 46.9)
                            weight_group = 5;
                        if (weight >= 53.1)
                            weight_group = 6;
                        if (weight >= 62.15)
                            weight_group = 7;
                        if (weight >= 71.6)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 35)
                    {
                        if (growth < 151.1)
                            growth_group = 1;
                        if (growth >= 151.1)
                            growth_group = 2;
                        if (growth >= 154.8)
                            growth_group = 3;
                        if (growth >= 159.9)
                            growth_group = 4;
                        if (growth >= 166.4)
                            growth_group = 5;
                        if (growth >= 1782)
                            growth_group = 6;
                        if (growth >= 177.9)
                            growth_group = 7;
                        if (growth >= 182.7)
                            growth_group = 8;

                        if (weight < 35.4)
                            weight_group = 1;
                        if (weight >= 35.4)
                            weight_group = 2;
                        if (weight >= 41.15)
                            weight_group = 3;
                        if (weight >= 46.8)
                            weight_group = 4;
                        if (weight >= 54.2)
                            weight_group = 5;
                        if (weight >= 62.1)
                            weight_group = 6;
                        if (weight >= 74.7)
                            weight_group = 7;
                        if (weight >= 88.4)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 36)
                    {
                        if (growth < 156.9)
                            growth_group = 1;
                        if (growth >= 156.9)
                            growth_group = 2;
                        if (growth >= 163.8)
                            growth_group = 3;
                        if (growth >= 168.1)
                            growth_group = 4;
                        if (growth >= 171.8)
                            growth_group = 5;
                        if (growth >= 176.3)
                            growth_group = 6;
                        if (growth >= 180.5)
                            growth_group = 7;
                        if (growth >= 183.6)
                            growth_group = 8;

                        if (weight < 41.8)
                            weight_group = 1;
                        if (weight >= 41.8)
                            weight_group = 2;
                        if (weight >= 51)
                            weight_group = 3;
                        if (weight >= 53.7)
                            weight_group = 4;
                        if (weight >= 59.5)
                            weight_group = 5;
                        if (weight >= 65.9)
                            weight_group = 6;
                        if (weight >= 81)
                            weight_group = 7;
                        if (weight >= 96.2)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 37)
                    {
                        if (growth < 161.7)
                            growth_group = 1;
                        if (growth >= 161.7)
                            growth_group = 2;
                        if (growth >= 165.4)
                            growth_group = 3;
                        if (growth >= 169.7)
                            growth_group = 4;
                        if (growth >= 174.2)
                            growth_group = 5;
                        if (growth >= 178.8)
                            growth_group = 6;
                        if (growth >= 182.3)
                            growth_group = 7;
                        if (growth >= 186.6)
                            growth_group = 8;

                        if (weight < 47.4)
                            weight_group = 1;
                        if (weight >= 47.4)
                            weight_group = 2;
                        if (weight >= 51.48)
                            weight_group = 3;
                        if (weight >= 54.9)
                            weight_group = 4;
                        if (weight >= 60.2)
                            weight_group = 5;
                        if (weight >= 68.2)
                            weight_group = 6;
                        if (weight >= 74.9)
                            weight_group = 7;
                        if (weight >= 95)
                            weight_group = 8;
                    }
                    //разделение для глаз
                    //разделение для глаз
                    if (age_group == 38 || age_group == 39 || age_group == 40)
                    {
                        if (growth < 162.6)
                            growth_group = 1;
                        if (growth >= 162.6)
                            growth_group = 2;
                        if (growth >= 168)
                            growth_group = 3;
                        if (growth >= 171.2)
                            growth_group = 4;
                        if (growth >= 175.5)
                            growth_group = 5;
                        if (growth >= 179.8)
                            growth_group = 6;
                        if (growth >= 183.3)
                            growth_group = 7;
                        if (growth >= 187.9)
                            growth_group = 8;

                        if (weight < 46.3)
                            weight_group = 1;
                        if (weight >= 46.3)
                            weight_group = 2;
                        if (weight >= 53.8)
                            weight_group = 3;
                        if (weight >= 57.9)
                            weight_group = 4;
                        if (weight >= 65)
                            weight_group = 5;
                        if (weight >= 72.1)
                            weight_group = 6;
                        if (weight >= 79)
                            weight_group = 7;
                        if (weight >= 93.7)
                            weight_group = 8;
                    }
                    //разделение для глаз
                }
                //зубы
                tooth_param = "Вне возраста";
                if (sex)
                {
                    if (age_group >=24 && age_group <= 31)
                        tooth = Convert.ToInt16(textBox4.Text);
                    if (age_group == 24)
                    {
                        if (tooth == 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 1)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 25)
                    {
                        if (tooth >= 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 4)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 26)
                    {
                        if (tooth >= 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 7)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 27)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 2)
                            tooth_param = "Соотвествует";
                        if (tooth >= 11)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 28)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 8)
                            tooth_param = "Соотвествует";
                        if (tooth >= 13)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 29)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 10)
                            tooth_param = "Соотвествует";
                        if (tooth >= 15)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 30)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 12)
                            tooth_param = "Соотвествует";
                        if (tooth >= 17)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 31)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 12)
                            tooth_param = "Соотвествует";
                    }
                }
                else
                {
                    if (age_group >= 24 && age_group <= 33)
                        tooth = Convert.ToInt16(textBox4.Text);
                    if (age_group == 24)
                    {
                        if (tooth == 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 1)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 25)
                    {
                        if (tooth >= 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 3)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 26)
                    {
                        if (tooth >= 0)
                            tooth_param = "Соотвествует";
                        if (tooth >= 5)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 27)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 2)
                            tooth_param = "Соотвествует";
                        if (tooth >= 9)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 28)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 6)
                            tooth_param = "Соотвествует";
                        if (tooth >= 12)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 29)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 8)
                            tooth_param = "Соотвествует";
                        if (tooth >= 14)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 30)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 10)
                            tooth_param = "Соотвествует";
                        if (tooth >= 14)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 31)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 12)
                            tooth_param = "Соотвествует";
                        if (tooth >= 21)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 32)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 15)
                            tooth_param = "Соотвествует";
                        if (tooth >= 25)
                            tooth_param = "Опережает";
                    }
                    if (age_group == 33)
                    {
                        if (tooth >= 0)
                            tooth_param = "Отстает";
                        if (tooth >= 21)
                            tooth_param = "Соотвествует";
                    }
                }
                //половые признаки
                SumSex_param = "Вне возраста";
                SumSex = 0;
                if (sex)
                {
                    if (radioButton3.Checked)
                        MaF = 0;
                    if (radioButton4.Checked)
                        MaF = 1.2;
                    if (radioButton5.Checked)
                        MaF = 2.4;
                    if (radioButton6.Checked)
                        MaF = 3.6;
                    if (radioButton9.Checked)
                        PF = 0;
                    if (radioButton10.Checked)
                        PF = 0.3;
                    if (radioButton8.Checked)
                        PF = 0.6;
                    if (radioButton7.Checked)
                        PF = 0.9;
                    if (radioButton13.Checked)
                        AxF = 0;
                    if (radioButton14.Checked)
                        AxF = 0.4;
                    if (radioButton12.Checked)
                        AxF = 0.8;
                    if (radioButton11.Checked)
                        AxF = 1.2;
                    if (radioButton17.Checked)
                        MeF = 0;
                    if (radioButton18.Checked)
                        MeF = 2.1;
                    if (radioButton16.Checked)
                        MeF = 4.2;
                    if (radioButton15.Checked)
                        MeF = 6.3;

                    SumSex = MaF + PF + AxF + MeF;

                    if (age_group == 31)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 2.7)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 32)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 1.2)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 2.7)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 33)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 1.5)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 7)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 34)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 3)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 11.6)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 35)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 5)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 12)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 36)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 11.6)
                            SumSex_param = "Соотвествует";
                    }
                }
                else
                {

                    if (radioButton21.Checked)
                        AxM = 0;
                    if (radioButton22.Checked)
                        AxM = 1;
                    if (radioButton20.Checked)
                        AxM = 2;
                    if (radioButton19.Checked)
                        AxM = 3;
                    if (radioButton27.Checked)
                        AxM = 4;
                    if (radioButton25.Checked)
                        PM = 0;
                    if (radioButton26.Checked)
                        PM = 1.1;
                    if (radioButton24.Checked)
                        PM = 2.2;
                    if (radioButton23.Checked)
                        PM = 3.3;
                    if (radioButton29.Checked)
                        PM = 4.4;
                    if (radioButton28.Checked)
                        PM = 5.5;
                    if (radioButton34.Checked)
                        LM = 0;
                    if (radioButton35.Checked)
                        LM = 0.6;
                    if (radioButton33.Checked)
                        LM = 1.2;
                    if (radioButton31.Checked)
                        VM = 0;
                    if (radioButton32.Checked)
                        VM = 0.7;
                    if (radioButton30.Checked)
                        VM = 1.4;
                    if (radioButton40.Checked)
                        FM = 0;
                    if (radioButton41.Checked)
                        FM = 1.6;
                    if (radioButton39.Checked)
                        FM = 3.2;
                    if (radioButton38.Checked)
                        FM = 4.8;
                    if (radioButton37.Checked)
                        FM = 6.4;
                    if (radioButton36.Checked)
                        FM = 8;

                    SumSex = AxM + PM + LM + VM + FM;

                    if (age_group == 33)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 1.8)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 34)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 0.7)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 7.3)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 35)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 2.9)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 9.5)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 36)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 5.7)
                            SumSex_param = "Соотвествует";
                        if (SumSex >= 14.3)
                            SumSex_param = "Опережает";
                    }
                    if (age_group == 37)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 10)
                            SumSex_param = "Соотвествует";
                    }
                    if (age_group == 38)
                    {
                        if (SumSex >= 0)
                            SumSex_param = "Отстает";
                        if (SumSex >= 10.6)
                            SumSex_param = "Соотвествует";
                    }
                }
                //про мужской пол сюда else


                    //добавление параметров в таблицу
                    if (radioButton1.Checked)
                    command.Parameters.AddWithValue("Sex", radioButton1.Text);
                else
                    command.Parameters.AddWithValue("Sex", radioButton2.Text);
                command.Parameters.AddWithValue("IdN", textBox3.Text);
                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Age", textBox2.Text);
                command.Parameters.AddWithValue("Growth", textBox8.Text);
                command.Parameters.AddWithValue("Weight", textBox7.Text);
                command.Parameters.AddWithValue("gAge", age_group);
                command.Parameters.AddWithValue("gGrowth", growth_group);
                command.Parameters.AddWithValue("gWeight", weight_group);
                command.Parameters.AddWithValue("pTooth", tooth_param);
                command.Parameters.AddWithValue("SumSex", SumSex);
                command.Parameters.AddWithValue("pSumSex", SumSex_param);
                command.ExecuteNonQuery();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены";
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [TableKids]", sqlConnection);

            try
            {
                sqlReader =  command.ExecuteReader();
                listBox1.Items.Add("|  ID  | Номер |                      ФИО                      |       Пол       |  Дата рождения  | Рост | Вес | Возрастная группа | Оценка роста | Оценка веса | Норма зубов | Половое развитие");
                listBox1.Items.Add("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                while (sqlReader.Read())
                {
                    listBox1.Items.Add("|   " + Convert.ToString(sqlReader["Id"]) + "   |   " + Convert.ToString(sqlReader["IdN"]) + "    |  " + Convert.ToString(sqlReader["Name"]) + "  |   " + Convert.ToString(sqlReader["Sex"]) + "  |      " + Convert.ToString(sqlReader["Age"]) + "      |   " + Convert.ToString(sqlReader["Growth"]) + "   |   " + Convert.ToString(sqlReader["Weight"]) + "  |                " + Convert.ToString(sqlReader["gAge"]) + "                |           " + Convert.ToString(sqlReader["gGrowth"]) + "            |           " + Convert.ToString(sqlReader["gWeight"]) + "            | " + Convert.ToString(sqlReader["pTooth"]) + " | " + Convert.ToString(sqlReader["pSumSex"]) + "(" + Convert.ToString(sqlReader["SumSex"]) + ")");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }


        }

        private void создатьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [TableKids]", sqlConnection);
            try
            {
                sqlReader = command.ExecuteReader();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fn = saveFileDialog1.FileName;
                    StreamWriter sw = new StreamWriter(fn, false, System.Text.Encoding.GetEncoding(1251));
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("<title>Список детей</title>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<h1>Список детей</h1>");
                    sw.WriteLine("<table border = " + '"' + '1' + '"' + '>');
                    sw.WriteLine("<tr>");
                    sw.WriteLine("<td><h3>№</h3></td>");
                    sw.WriteLine("<td><h3>ФИО</h3></td>");
                    sw.WriteLine("<td><h3>Пол</h3></td>");
                    sw.WriteLine("<td><h3>Дата рождения</h3></td>");
                    sw.WriteLine("<td><h3>Рост</h3></td>");
                    sw.WriteLine("<td><h3>Вес</h3></td>");
                    sw.WriteLine("<td><h3>Возрастная группа</h3></td>");
                    sw.WriteLine("<td><h3>Оценка роста</h3></td>");
                    sw.WriteLine("<td><h3>Оценка веса</h3></td>");
                    sw.WriteLine("<td><h3>Норма зубов</h3></td>");
                    sw.WriteLine("<td><h3>Половое развитие</h3></td>");
                    sw.WriteLine("</tr>");
                    int i = 1;
                    while (sqlReader.Read())
                    {
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["IdN"]) + "</td>");
                        i++;
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["Name"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["Sex"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["Age"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["Growth"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["Weight"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["gAge"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["gGrowth"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["gWeight"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["pTooth"]) + "</td>");
                        sw.WriteLine("<td>" + Convert.ToString(sqlReader["pSumSex"]) + "(" + Convert.ToString(sqlReader["SumSex"]) + ")" + "</td>");
                        sw.WriteLine("</tr>");
                    }
                    sw.WriteLine("</table>");
                    sw.WriteLine("</body>");
                    sw.WriteLine("</html>");
                    sw.Close();
                    }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }


        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void spoiler1_Fold(object sender, Spoiler.SpoilerEventArgs e)
        {

        }
    }
}
