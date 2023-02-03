
using ExcelDataReader;
using System.Data;
using User.ApplicationCore.Entities;
using User.Infrastructure.Data;

namespace WebAPI.DBGenerator
{
    public static class DataStore
    {
        public static void ImportData(string filePath, IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            provider = scope.ServiceProvider;

            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;

            if (Path.GetExtension(filePath).ToUpper() == ".XLS")
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);//*.xls
            }
            else
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);//*.xlsx
            }

            //DataSet
            DataSet ds = excelReader.AsDataSet();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                var tableName = ds.Tables[i].TableName;
                var dt = ds.Tables[i];
                switch (tableName)
                {
                    case "T_USER": provider.WriteUser(dt); break;
                    //case "T_COURSE": provider.WriteCourse(dt); break;
                    //case "T_USER_COURSE": provider.WriteUserCourse(dt); break;

                }
            }
        }

        private static void WriteUser(this IServiceProvider provider, DataTable dt)
        {
            var context = provider.GetRequiredService<UserContext>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new User.ApplicationCore.Entities.User();
                //item.id = Convert.ToInt32(dt.Rows[i][1].ToString());
                item.name = dt.Rows[i][1].ToString();
                item.password = dt.Rows[i][2].ToString();
                item.age = Convert.ToInt32(dt.Rows[i][3].ToString());
                item.gender = dt.Rows[i][4].ToString();
                item.race = dt.Rows[i][5].ToString();
                context.Add(item);
            }
            context.SaveChanges();
        }

        private static string NULLConvert(this object obj) => obj.ToString() == "NULL" ? null : obj.ToString();

    }
}
