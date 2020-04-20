using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FactoryDAL
{
    public class UsersDAL
    {
        private readonly string _connectionString;
        private SqlConnection _sqlConnection = null;

        public UsersDAL() : this(@"Data Source = DESKTOP-5D0552Q; Integrated Security=True; Initial Catalog = Factory")
        {

        }
        public UsersDAL(string connectionString) => _connectionString = connectionString;

        #region Open/Close Connnection
        private void OpenConnection()
        {
            _sqlConnection = new SqlConnection { ConnectionString = _connectionString };
            _sqlConnection.Open();
        }
        private void CloseConnection()
        {
            if (_sqlConnection.State != ConnectionState.Closed)
                _sqlConnection?.Close();
        }
        #endregion


        /// <summary>
        /// Проверка на существование пользователя в БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsUserExists(string login)
        {
            bool exists = false;

            OpenConnection();

            string sql = $"SELECT * FROM Users WHERE Login = '{login}'";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    SqlDataReader dataReader = command.ExecuteReader();
                    exists = (bool)dataReader.Read();

                    while (dataReader.Read())
                    {
                        MessageBox.Show((string)dataReader["Login"]);
                    }

                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
            }

            return exists;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool LogInUser(string login, string password)
        {
            bool exists = false;

            OpenConnection();

            string sql = $"SELECT * FROM Users WHERE Login = '{login}' AND Password = '{password}'";

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    SqlDataReader dataReader = command.ExecuteReader();
                    exists = (bool)dataReader.Read();


                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return exists;
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="surname"></param>
        /// <param name="userName"></param>
        /// <param name="middleName"></param>
        public void AddUser(string login, string password, string role, 
                            string surname, string userName, string middleName, string photoWay)
        {
            OpenConnection();

            string sql = $"INSERT INTO Users(Login,      Password,      Role,     Surname,    UserName,     MiddleName)" +
                         $"           VALUES('{login}', '{password}', '{role}', '{surname}', '{userName}', '{middleName}')";

            string insertPhoto = $"INSERT INTO Users(Picture) VALUES(@img) WHERE Login='{login}'";
            SqlParameter param = new SqlParameter("@img", SqlDbType.VarBinary);
            

            try
            {
                using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
                {
                    command.ExecuteNonQuery();

                    CloseConnection();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }







    }
}
