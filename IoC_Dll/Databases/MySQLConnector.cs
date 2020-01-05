
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SEPFrameWork.Databases
{
    public class MySQLConnector : IConnector
    {

        private String database;
        private String host = "localhost";
        private int port = 3306;
        private String username = "quochoi142";
        private String password = "quochoi142";


        public MySQLConnector(String username, String password, String host, string database)
        {
            this.username = username;
            this.password = password;
            this.host = host;
            this.database = database;
        }

        private MySqlConnection GetDBConnection()
        {

            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);



            return conn;
        }

        public bool CreateData(string tableName, object[] data)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;

            String sqlQuery;
            //tạo list các fields để insert dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);
            List<string> listAutoIncrement = this.GetFieldsAutoIncrement(tableName);
            foreach(var e in listAutoIncrement)
            {
                if (listFields.Contains(e))
                {
                    listFields.Remove(e);
                }
            }
            // sau tên table sẽ là các trường dữ liệu để chèn vào
            StringBuilder fieldsString = new StringBuilder();

            // chuỗi này để lưu index các param (ví dụ param1) dùng để truyền dữ liệu từ Object data
            StringBuilder indexParams = new StringBuilder();

            if (listFields.Count < 1)
            {
                return false;
            }
            fieldsString.Append(" (");
            indexParams.Append(" (");
            for (int i = 0; i < listFields.Count; i++)
            {
                fieldsString.Append(listFields[i]);
                indexParams.Append("@param").Append(i);
                if (i < listFields.Count - 1)
                {
                    fieldsString.Append(", ");
                    indexParams.Append(", ");
                }
            }
            fieldsString.Append(")");
            indexParams.Append(")");


            try
            {
                if (listFields.Count != data.Length)
                {
                    return false;
                }

                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                //param1, param2, param3

                sqlQuery = "INSERT INTO " + tableName + fieldsString + " VALUES " + indexParams;
                cmd.CommandText = sqlQuery;

                for (int i = 0; i < data.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, data[i]);
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Dictionary<String, String>> ReadData(string tableName)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;

            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();
                sqlQuery = "SELECT * FROM " + tableName;
                cmd.CommandText = sqlQuery;

                List<string> listKeys = this.GetNameFieldsOfTable(tableName);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, string> record = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                record.Add(listKeys[i], reader.GetValue(i).ToString());
                            }
                            result.Add(record);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }
        public bool UpdateData(string tableName, object[] oldData, object[] newData)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;

            String sqlQuery;
            //tạo list các fields để upfate dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);

            // chuỗi để lưu index 
            StringBuilder paramsUpdateSET = new StringBuilder();
            StringBuilder paramsUpdateWHERE = new StringBuilder();


            if (listFields.Count < 1)
            {
                return false;
            }

            for (int i = 0; i < listFields.Count; i++)
            {
                paramsUpdateSET.Append(listFields[i]).Append(" = ").Append("@paramset").Append(i);
                paramsUpdateWHERE.Append(listFields[i]).Append(" = ").Append("@paramwhere").Append(i);
                if (i < listFields.Count - 1)
                {
                    paramsUpdateSET.Append(", ");
                    paramsUpdateWHERE.Append(" AND ");
                }
            }

            try
            {
                if (listFields.Count != newData.Length)
                {
                    return false;
                }
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();
                sqlQuery = "UPDATE " + tableName + " SET " + paramsUpdateSET + " WHERE " + paramsUpdateWHERE;
                cmd.CommandText = sqlQuery;

                for (int i = 0; i < listFields.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@paramset" + i, newData[i]);
                    cmd.Parameters.AddWithValue("@paramwhere" + i, oldData[i]);
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool DeleteData(string tableName, object[] data)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;

            String sqlQuery;

            //tạo list các fields để upfate dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);


            StringBuilder paramDelete = new StringBuilder();

            for (int i = 0; i < listFields.Count; i++)
            {
                paramDelete.Append(listFields[i]).Append(" = ").Append("@param").Append(i);
                if (i < listFields.Count - 1)
                {
                    paramDelete.Append(" AND ");
                }
            }
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "DELETE FROM " + tableName + " WHERE " + paramDelete;
                cmd.CommandText = sqlQuery;

                for (int i = 0; i < data.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, data[i]);
                }

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<String> GetNameTables()
        {
            List<String> listNameTableOfDatabase = new List<String>();
            MySqlConnection conn = null;

            try
            {
                conn = this.GetDBConnection();
                conn.Open();

                DataTable dataTable = conn.GetSchema("Tables");
                foreach (DataRow row in dataTable.Rows)
                {
                    listNameTableOfDatabase.Add(row[2].ToString());
                }
                return listNameTableOfDatabase;

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<String> GetNameFieldsOfTable(string tableName)
        {
            List<String> nameFieldsOfTable = new List<String>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT `COLUMN_NAME` FROM `INFORMATION_SCHEMA`.`COLUMNS` WHERE `TABLE_SCHEMA`='"+database+"' AND `TABLE_NAME`='"+tableName+"' ";
                // sqlQuery = "SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name =  @tableName";
                cmd.CommandText = sqlQuery;
                //cmd.Parameters.AddWithValue("@database", this.database);
                //cmd.Parameters.AddWithValue("@tableName", tableName);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            nameFieldsOfTable.Add(reader.GetString(0));
                        }
                    }
                }
                return nameFieldsOfTable;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Type GetTypeofFields(string tableName, string field)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            String typeOfField = null;
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT data_type as 'DATA_TYPE' FROM information_schema.columns WHERE table_name =  @tableName AND column_name = @field";
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@tableName", tableName);
                cmd.Parameters.AddWithValue("@field", field);


                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            typeOfField = reader.GetString(0); // 0 là cột đầu tiên
                        }
                    }
                }
                return GetType(typeOfField);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private Type GetType(string typeOfField)
        {
            switch (typeOfField)
            {
                case "char":
                case "varchar":
                case "text":
                case "nvarchar":
                case "ntext":
                case "decimal":
                case "numeric":
                    return typeof(String);
                case "bit":
                    return typeof(Boolean);
                case "image":
                    return typeof(Byte[]);
                case "datetime":
                    return typeof(DateTime);
                case "int":
                case "Int32":
                    return typeof(Int32);
                case "float":
                case "double":
                    return typeof(Double);
                default:
                    return typeof(Nullable);
            }
        }

        public List<string> GetNameFieldsNotNullOfTable(string tableName)
        {
            List<String> listResult = new List<String>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT TABLE_CATALOG AS TABLE_NAME, COLUMN_NAME, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND IS_NULLABLE = 'NO'";
                cmd.CommandText = sqlQuery;
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            listResult.Add(reader.GetString(0));
                        }
                    }
                }
                return listResult;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<String> GetPrimaryKeyOfTable(String tableName)
        {
            String result = null;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            List<string> keys = new List<string>();
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SHOW KEYS FROM " + tableName + " WHERE Key_name = 'PRIMARY'";
                //sqlQuery = "SELECT Col.Column_Name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col WHERE Col.Constraint_Name = Tab.Constraint_Name AND Col.Table_Name = Tab.Table_Name AND Constraint_Type = 'PRIMARY KEY' AND Col.Table_Name = '" + tableName + "'";
                cmd.CommandText = sqlQuery;
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(4);
                            keys.Add(result);
                        }
                    }
                }
                return keys;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<String> GetFieldsAutoIncrement(String tableName)
        {
            MySqlConnection conn = null;

            try
            {
                List<String> keys = new List<string>();
                MySqlCommand cmd = new MySqlCommand();
                conn = GetDBConnection();
                conn.Open();
                cmd.Connection = conn;
                String sql = "show columns from " + tableName + " where extra like '%auto_increment%'";
                cmd.CommandText = sql;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            keys.Add(reader.GetString(0));
                        }

                    }
                }
                conn.Close();
                return keys;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                if (conn != null)
                    conn.Close();
                return null;
            }

        }
        public List<String> GetNameDatabase()
        {
            //String myConnectionString = "SERVER=localhost;UID='quochoi142';" + "PASSWORD='quochoi142';";
            //String myConnectionString = "SERVER=sql2.freemysqlhosting.net;UID='sql2317605';" + "PASSWORD='uC4!kS3%';"; // hòa test
            String myConnectionString = "SERVER=" + this.host + ";UID=" + this.username + ";PASSWORD=" + this.password + ";";
            List<String> Dbs = new List<string>();
            MySqlConnection conn = null;
            try
            {

                conn = new MySqlConnection(myConnectionString);
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SHOW DATABASES;";
                MySqlDataReader Reader;
                conn.Open();
                Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < Reader.FieldCount; i++)
                        row += Reader.GetValue(i).ToString();
                    Dbs.Add(row);


                }
                conn.Close();
                return Dbs;
            }
            catch (Exception ex)
            {
                if (conn != null)
                {
                    conn.Close();
                }
                Console.WriteLine(ex.Message);
                return null;

            }




        }
    }
}