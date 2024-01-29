using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCampus.Pages
{
    public class SubjectCampusModel : PageModel

    {
        public List<InfoSubjects> InfoSubjectList = new List<InfoSubjects>();
        public int TotalPages { get; set; }
        public void OnGet(string searchString, int pageNumber = 1)
        {
            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mycampus;Integrated Security=True;Encrypt=False";

                int pageSize = 5;
                int skip = (pageNumber - 1) * pageSize; 

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    
                    using (SqlCommand countCommand = new SqlCommand($"SELECT COUNT(*) FROM matakuliah WHERE Nama_MK LIKE '%{searchString}%'", con))
                    {
                        int totalData = (int)countCommand.ExecuteScalar();
                        TotalPages = (totalData + pageSize - 1) / pageSize; 
                    }

                    string sql = $"SELECT * FROM matakuliah WHERE Nama_MK LIKE '%{searchString}%' ORDER BY Kode_MK OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InfoSubjects subjectInfo = new InfoSubjects();
                                subjectInfo.Kode_MK = reader.GetString(0);
                                subjectInfo.Nama_MK = reader.GetString(1);
                                subjectInfo.Sks = "" + reader.GetInt32(2);

                                InfoSubjectList.Add(subjectInfo);
                            }
                        }
                    }
                    foreach (var item in InfoSubjectList)
                    {
                        Console.WriteLine($"Kode_MK: {item.Kode_MK}, Nama_MK: {item.Nama_MK}, Sks: {item.Sks}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
        }
    }

    public class InfoSubjects
    {
        public string Kode_MK;
        public string Nama_MK;
        public String Sks;
    }
}
