using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPFrameWork.Databases
{
    public interface IConnector
    {
        //lấy tất cả dữ liệu của bảng để lưu vào 1 list
        List<Dictionary<String, String>> ReadData(String tableName);


        //lấy tên của các bảng trong database
        List<String> GetNameTables();

        //thêm mới 1 record vào 1 bảng trong database
        bool CreateData(String tableName, Object[] data);

        //cập nhật lại 1 record trong 1 bảng của database   
        bool UpdateData(String tableName, Object[] data, Object[] newData);

        //xóa 1 record trong 1 bảng của databse
        bool DeleteData(String tableName, Object[] Data);

        //trả về tên của Collumn của bảng dữ liệu cùng với kiểu dữ liệu của nó
        List<String> GetNameFieldsOfTable(String tableName);

        //trả về kiểu dữ liệu của Fields trong bảng dữ liệu
        Type GetTypeofFields(String tableName, String field);

        //trả về List các Fields có thể để dữ liệu null trong bảng
        List<String> GetNameFieldsNotNullOfTable(String tableName);

        //trả về tên của Fields là khóa chính của bảng
        List<String> GetPrimaryKeyOfTable(String tableName);

        //Get a list of fields are auto increment
        List<String> GetFieldsAutoIncrement(String tableName);

        //trả về tên các database 
        List<string> GetNameDatabase();
    }
}
