using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Programming_Task
{
    /*Takes all csv files in the folder and import them to db SQL*/
    class WriteCsvToSql : SqlHandlerBase
    {
        private string csv_path;

        public WriteCsvToSql()
        {
            csv_path = ConfigurationManager.AppSettings.Get("CsvPath");
        }

        //Creates a DataTable object from csv file
        private DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return csvData;
        }

        //Imports all csv files as DataTables to SQL.
        public void ImportCSVFilesToSql()
        {
            SqlConnection con = CreateConnection();
            con.Open();

            var AllFiles = new DirectoryInfo(csv_path).GetFiles("*.CSV");

            foreach (var file in AllFiles)
                using (SqlBulkCopy s = new SqlBulkCopy(con))
                {
                    DataTable dt = GetDataTabletFromCSVFile(file.FullName);

                    s.DestinationTableName = tableName;
                    foreach (var column in dt.Columns)
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    s.WriteToServer(dt);

                }
        }

    }
}
