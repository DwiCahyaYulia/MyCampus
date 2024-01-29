using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCampus.Pages
{
    public class NewSubjectModel : PageModel
    {
        public InfoSubjects subjectInfo= new InfoSubjects();
        public String errorMessage = "";
        public String succedMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            subjectInfo.Kode_MK = Request.Form["Kode_MK"];
            subjectInfo.Nama_MK = Request.Form["Nama_MK"];
            subjectInfo.Sks = Request.Form["Sks"];

            if (subjectInfo.Nama_MK.Length == 0 || subjectInfo.Sks.Length == 0 || subjectInfo.Kode_MK.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mycampus;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO matakuliah" +
                        "(Kode_MK, Nama_MK, Sks) VALUES" +
                        "(@Kode_MK, @Nama_MK, @Sks)";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@Kode_MK", subjectInfo.Kode_MK);
                        command.Parameters.AddWithValue("@Nama_MK", subjectInfo.Nama_MK);
                        command.Parameters.AddWithValue("@Sks", subjectInfo.Sks);

                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            subjectInfo.Nama_MK = ""; subjectInfo.Sks = "";
            succedMessage = "New Subject Added";

            Response.Redirect("/SubjectCampus");

        }
    }
}
