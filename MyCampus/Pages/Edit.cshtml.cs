using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyCampus.Pages
{
    public class EditModel : PageModel
    {
        public InfoSubjects subjectInfo = new InfoSubjects();
        public String errorMessage = "";
        public String succedMessage = "";
        public void OnGet()
        {
            String id = Request.Query["Kode_MK"];

            try
            {
                string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mycampus;Integrated Security=True;Encrypt=False";

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    string sql = "SELECT * FROM matakuliah WHERE Kode_MK=@Kode_MK";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@Kode_MK", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                subjectInfo.Kode_MK = reader.GetString(0);
                                subjectInfo.Nama_MK = reader.GetString(1);
                                subjectInfo.Sks = "" + reader.GetInt32(2);

                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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
                    String sql = "UPDATE matakuliah " +
                                "SET Nama_MK=@Nama_MK, Sks=@Sks " +
                                "WHERE Kode_MK=@Kode_MK";


                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {                       
                        command.Parameters.AddWithValue("@Nama_MK", subjectInfo.Nama_MK);
                        command.Parameters.AddWithValue("@Sks", subjectInfo.Sks);
                        command.Parameters.AddWithValue("@Kode_MK", subjectInfo.Kode_MK);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/SubjectCampus");

        }
    }
}
