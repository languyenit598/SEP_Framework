using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFrameWork.Databases
{
    public class SQLServerConnection : IConnector
    {
        public String databaseName { get; set; }
        public String dataSource { get; set; }
        public String username { get; set; }
        public String password { get; set; }


        public SQLServerConnection(String databaseName, String dataSource = @".\SQLEXPRESS", String username = null, String password = null)
        {
            this.databaseName = databaseName;
            this.dataSource = dataSource;
            this.username = username;
            this.password = password;
        }

        private SqlConnection GetDBConnection()
        {
            SqlConnection conn;
            string connetionString;
            if (this.username != null && this.password != null)
            {
                connetionString = @"Data Source=" + this.dataSource + ";Initial Catalog=" + this.databaseName + ";User ID=" + this.username + ";Password=" + this.password + "; Integrated Security = True";
            }
            else // username, pass đều null
            {
                connetionString = @"Data Source=" + this.dataSource + ";Initial Catalog=" + this.databaseName + "; Integrated Security = True";
            }
            try
            {
                conn = new SqlConnection(connetionString);
            }
            catch (Exception ex)
            {
                return null;
            }

            return conn;
        }

        public bool CreateData(string tableName, object[] data)
        {
            SqlConnection conn = null;
            SqlCommand cmd;

            String sqlQuery;
            //tạo list các fields để insert dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);
            List<string> listAutoIncrement = this.GetFieldsAutoIncrement(tableName);
            foreach (var e in listAutoIncrement)
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
            SqlConnection conn = null;
            SqlCommand cmd;
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
            SqlConnection conn = null;
            SqlCommand cmd;

            String sqlQuery;
            //tạo list các fields để upfate dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);

            // chuỗi để lưu index 
            StringBuilder paramsUpdateSET = new StringBuilder();
            //StringBuilder paramsUpdateWHERE = new StringBuilder();


            if (listFields.Count < 1)
            {
                return false;
            }

            for (int i = 0; i < listFields.Count; i++)
            {
                paramsUpdateSET.Append(listFields[i]).Append(" = ").Append("@paramset").Append(i);
                //paramsUpdateWHERE.Append(listFields[i]).Append(" = ").Append("@paramwhere").Append(i);              
                if (i < listFields.Count - 1)
                {
                    paramsUpdateSET.Append(", ");
                    //paramsUpdateWHERE.Append(" AND ");
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
                sqlQuery = "UPDATE " + tableName + " SET " + paramsUpdateSET + " WHERE " + listFields[0] + " = " + oldData[0];
                cmd.CommandText = sqlQuery;
                for (int i = 0; i < listFields.Count; i++)
                {
                    cmd.Parameters.AddWithValue("@paramset" + i, newData[i]);
                    //cmd.Parameters.AddWithValue("@paramwhere" + i, oldData[i]);                  
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
            SqlConnection conn = null;
            SqlCommand cmd;

            String sqlQuery;

            //tạo list các fields để upfate dữ liệu
            List<string> listFields = this.GetNameFieldsOfTable(tableName);

            //StringBuilder paramDelete = new StringBuilder();
            //for (int i = 0; i < listFields.Count; i++)
            //{
            //    paramDelete.Append(listFields[i]).Append(" = ").Append("@param").Append(i);
            //    if (i < listFields.Count - 1)
            //    {
            //        paramDelete.Append(" AND ");
            //    }
            //}
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "DELETE FROM " + tableName + " WHERE " + listFields[0] + " = " + data[0];
                cmd.CommandText = sqlQuery;

                for (int i = 0; i < data.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@param" + i, data[i]);
                }

                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                return false;

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
            SqlConnection conn = null;

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
            SqlConnection conn = null;
            SqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT column_name as 'Column Name' FROM information_schema.columns WHERE table_name =  @tableName";
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@tableName", tableName);

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
            SqlConnection conn = null;
            SqlCommand cmd;
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
            SqlConnection conn = null;
            SqlCommand cmd;
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
                            listResult.Add(reader.GetString(1));
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

        public List<string> GetPrimaryKeyOfTable(string tableName)
        {
            String result = null;
            SqlConnection conn = null;
            SqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            List<string> keys = new List<string>();
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT Col.Column_Name from INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col WHERE Col.Constraint_Name = Tab.Constraint_Name AND Col.Table_Name = Tab.Table_Name AND Constraint_Type = 'PRIMARY KEY' AND Col.Table_Name = '" + tableName + "'";
                cmd.CommandText = sqlQuery;
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
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

        public List<string> GetFieldsAutoIncrement(string tableName)
        {
            String result = null;
            SqlConnection conn = null;
            SqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            List<string> keys = new List<string>();
            try
            {
                conn = this.GetDBConnection();
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT name FROM sys.identity_columns " + "WHERE object_id = OBJECT_ID(@TableName)";
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@tableName", tableName);
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
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

        public List<string> GetNameDatabase()
        {
            String result = null;
            SqlConnection conn = null;
            SqlCommand cmd;
            DbDataReader reader;
            String sqlQuery;
            List<string> keys = new List<string>();
            try
            {
                //var connstring = @"Data Source=.\SQLEXPRESS; Integrated Security = True";
                var connstring = @"Data Source=" + this.dataSource + "; Integrated Security = True";
                //conn = this.GetDBConnection();
                conn = new SqlConnection(connstring);
                conn.Open();
                cmd = conn.CreateCommand();

                sqlQuery = "SELECT name from sys.databases WHERE name NOT IN('master', 'tempdb', 'model', 'msdb')"; // không dùng các db hệ thống
                cmd.CommandText = sqlQuery;
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = reader.GetString(0);
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
    }
}
