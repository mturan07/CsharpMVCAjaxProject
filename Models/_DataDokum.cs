using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace WebApp.Models
{
    public class _DataDokum : Kalitim
    {
        public void CSVOlustur(string dosyaadi, string kuladi, string sqltxt)
        {
            DosyalariSil(kuladi);
            DataTable dt = new DataTable();
            dt = VerileriGetir(sqltxt);

            //var result = new StringBuilder();
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    result.Append(dt.Columns[i].ColumnName);
            //    result.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    for (int i = 0; i < dt.Columns.Count; i++)
            //    {
            //        result.Append(row[i].ToString());
            //        result.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
            //    }
            //}

            //File.WriteAllText(@"C:\Csv\" + dosyaadi, result.ToString());

            string csv = DataTableToCSV(dt, ';');
            File.WriteAllText(@"C:\Csv\" + dosyaadi, csv);
        }

        public DataTable VerileriGetir(string sqltxt)
        {
            SqlConnection conn =
                new SqlConnection(Dbc.Database.Connection.ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(sqltxt, conn)
            {
                CommandTimeout = 600
            };

            SqlDataReader dr = cmd.ExecuteReader();

            var tb = new DataTable();
            tb.Load(dr);

            return tb;
        }

        public String DataTableToCSV(DataTable datatable, char seperator)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                sb.Append("GSM");
                //sb.Append(datatable.Columns[i]);
                if (i <= datatable.Columns.Count - 1)
                    sb.Append(seperator);
            }
            sb.AppendLine();
            foreach (DataRow dr in datatable.Rows)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    sb.Append(dr[i].ToString());

                    if (i <= datatable.Columns.Count - 1)
                        sb.Append(seperator);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void DosyalariSil(string aranan)
        {
            string[] Files = Directory.GetFiles(@"C:/Csv/");

            foreach (string file in Files)
            {
                if (file.ToUpper().Contains(aranan.ToUpper()))
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
}