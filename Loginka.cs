﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace TestReload
{
    public partial class Loginka : Form
    {

        DataBase DataBase = new DataBase();

        public Loginka()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;//открытие новой формы по центру экрана
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
       
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string Query = $"SELECT id_user,login_user,password_user, is_admin FROM users WHERE login_user = '{loginUser}' AND password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(Query, DataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);


            if (table.Rows.Count == 1)
            {
                var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));
                MessageBox.Show($"Вы авторизовались, как: {loginUser}", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Uchet_Tovarov tovar = new Uchet_Tovarov(user);
                this.Hide();
                tovar.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show($"Такой аккаунт не зарегестрирован", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Loginka_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox1.MaxLength = 50;
            textBox2.MaxLength = 50;
        }
    }
}
