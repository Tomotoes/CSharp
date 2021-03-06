﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();
        private void Form1_Load(object sender, EventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
            con.Open();
            if(con.State==ConnectionState.Open )
            {
                this.Text = "open";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //第一种无参构造函数，为Command对象的三个属性分别赋值
            //字符串的拼接

            string str = "insert into StudentInfo values('" + txtName.Text + "','" + txtSex.Text + "'," + txtAge.Text + ",'" + dateTimePicker1.Value + "','" + txtPro.Text + "','" + txtAddres.Text + "','" + txtTel.Text + "','" + txtColle.Text + "','"+txtClass.Text+"')";
            SqlCommand com = new SqlCommand();
            com.CommandText = str;
            com.Connection = con;
            com.CommandType = CommandType.Text;  //要执行的是SQL语句 ,默认值
            //com.CommandType = CommandType.StoredProcedure;   //要执行的是存储过程

            com.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "update studentinfo set SAge=" + txtAge.Text + ",ssex='" + txtSex.Text + "' where sname='" + txtName.Text + "'";
            SqlCommand com = new SqlCommand(str);
            com.Connection = con;
            com.CommandType = CommandType.Text;  //要执行的是SQL语句 ,默认值
            com.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "delete from studentinfo where sname='"+txtName.Text+"'";
            SqlCommand com = new SqlCommand(str, con);
            int i = com.ExecuteNonQuery();
            MessageBox.Show(i.ToString ());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //参数化sql语句
            string str = "insert into studentinfo values(@name,@sex,@age,@birthday,@major,@address,@tel,@colledge,@class)";
            SqlCommand com = new SqlCommand(str, con);
            ////为参数进行赋值 第一种方式
            //com.Parameters.AddWithValue("@name",txtName.Text);
            //com.Parameters.AddWithValue("@sex", txtSex.Text);
            //com.Parameters.AddWithValue("@age", txtAge.Text);
            //com.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.ToString());
            //com.Parameters.AddWithValue("@major", txtPro.Text);
            //com.Parameters.AddWithValue("@address", txtAddres.Text );
            //com.Parameters.AddWithValue("@tel", txtTel.Text);
            //com.Parameters.AddWithValue("@colledge", txtColle.Text);
            //com.Parameters.AddWithValue("@class", txtClass.Text);

            //为参数进行赋值 第二种方式
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@name",txtName.Text),
                new SqlParameter("@sex",txtSex.Text),
                new SqlParameter("@age",txtAge.Text),
                new SqlParameter("@birthday",dateTimePicker1.Value.ToString()),
                new SqlParameter("@major",txtPro.Text),
                new SqlParameter("@address",txtAddres.Text),
                new SqlParameter("@tel",txtTel.Text),
                new SqlParameter("@colledge",txtColle.Text),
                new SqlParameter("@class",txtClass.Text)
            };

            com.Parameters.AddRange(param);

            com.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "update studentinfo set SSex=@SSex,SAge=@SAge,SBirthDate=@SBirthDate,SMajor=@SMajor,SAddress=@SAddress,STel=@STel,SCollege=@SCollege,SClass=@SClass where SName=@SName";
            SqlCommand com = new SqlCommand(str, con);

            //为参数进行赋值
            com.Parameters.AddWithValue("@SName", txtName.Text);
            com.Parameters.AddWithValue("@SSex", txtSex.Text);
            com.Parameters.AddWithValue("@SAge", txtAge.Text);
            com.Parameters.AddWithValue("@SBirthDate", dateTimePicker1.Value.ToString());
            com.Parameters.AddWithValue("@SMajor", txtPro.Text);
            com.Parameters.AddWithValue("@SAddress", txtAddres.Text);
            com.Parameters.AddWithValue("@STel", txtTel.Text);
            com.Parameters.AddWithValue("@SCollege", txtColle.Text);
            com.Parameters.AddWithValue("@SClass", txtClass.Text);

            com.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = "delete from studentinfo where SName=@SName";
            SqlCommand com = new SqlCommand(str, con);
            com.Parameters.AddWithValue("@SName", txtName.Text);
            com.ExecuteNonQuery();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("insertdata",con);
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter ("@CNo",txtCNo.Text),
                new SqlParameter ("@CName",txtCName.Text),
                new SqlParameter ("@CNote",txtCNote.Text),
                new SqlParameter ("@CCredit",txtCCredit.Text)
            };
            com.Parameters.AddRange(param);

            com.ExecuteNonQuery();
        }
    }
}
