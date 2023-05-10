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
    public partial class Reg : Form
    {
        DataBase DataBase = new DataBase();

        public Reg()
        {
            StartPosition = FormStartPosition.CenterScreen;//открытие новой формы по центру экрана
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
            string Query = $"INSERT INTO users (login_user,password_user,is_admin) VALUES ('{loginUser}', '{passUser}',0)";

            SqlCommand command = new SqlCommand(Query, DataBase.GetConnection());

            DataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт создан", "Уведомление");
                Loginka log = new Loginka();
                this.Hide();
                log.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан, ошибка", "Уведомление");
            }
            DataBase.closeConnection();
        }

        private Boolean check()
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
        
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string Query = $"SELECT id_user,login_user,password_user, is_admin FROM users WHERE login_user = '{loginUser}' AND password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(Query, DataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой юзер уже существует, перерегистрируйся", "Уведомление");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }
    }
}
