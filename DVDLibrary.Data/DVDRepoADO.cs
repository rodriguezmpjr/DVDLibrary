using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLibrary.Models;

namespace DVDLibrary.Data
{
    public class DVDRepoADO : IDVDLibraryRepo
    {
        public DVD AddDvd(DVD dvdToAdd)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "AddADvd";
                cmd.Parameters.AddWithValue("@Title", dvdToAdd.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", dvdToAdd.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", dvdToAdd.Director);
                cmd.Parameters.AddWithValue("@Rating", dvdToAdd.Rating);
                cmd.Parameters.AddWithValue("@Notes", dvdToAdd.Notes);
                cmd.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
                cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                conn.Open();
                cmd.ExecuteNonQuery();
                dvdToAdd.DVDId = (int)cmd.Parameters["@RETURN_VALUE"].Value;
            }
            return dvdToAdd;
        }
        public DVD DeleteDvd(int dvdId)
        {
            DVD deleted = GetDvdById(dvdId);
            using (SqlConnection conn = new SqlConnection())
            {                 
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;                

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [DVD] WHERE DVDId = @toDelete";
                cmd.Parameters.AddWithValue("@toDelete", dvdId);
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            return deleted;
        }

        public DVD EditDvd(DVD current, DVD updated)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "EditADvd";
                cmd.Parameters.AddWithValue("@OriginalId", current.DVDId);
                cmd.Parameters.AddWithValue("@Title", updated.Title);
                cmd.Parameters.AddWithValue("@ReleaseYear", updated.ReleaseYear);
                cmd.Parameters.AddWithValue("@Director", updated.Director);
                cmd.Parameters.AddWithValue("@Rating", updated.Rating);
                cmd.Parameters.AddWithValue("@Notes", updated.Notes);
                cmd.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int));
                cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
                conn.Open();
                cmd.ExecuteNonQuery();
                updated.DVDId = (int)cmd.Parameters["@RETURN_VALUE"].Value;
            }
            return updated;
        }

        public List<DVD> GetAllDvds()
        {
            List<DVD> result = new List<DVD>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select DVD.DVDId, DVD.Title, DVD.ReleaseYear, d.Name as Director, r.Name as Rating," +
                                  " DVD.Notes from DVD inner join Director d on DVD.DirectorId = d.DirectorId" +
                                  " inner join Rating r on DVD.RatingId = r.RatingId";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD();
                        currentRow.DVDId = (int)dr["DVDId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = (int)dr["ReleaseYear"];
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        result.Add(currentRow);
                    }
                }
            }
            return result;
        }

        public DVD GetDvdById(int dvdId)
        {
            DVD result = new DVD();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select DVD.DVDId, DVD.Title, DVD.ReleaseYear, d.Name as Director, r.Name as Rating," +
                                  " DVD.Notes from DVD inner join Director d on DVD.DirectorId = d.DirectorId" +
                                  " inner join Rating r on DVD.RatingId = r.RatingId where DVDId = " + dvdId;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVD currentRow = new DVD();
                        currentRow.DVDId = (int)dr["DVDId"];
                        currentRow.Title = dr["Title"].ToString();
                        currentRow.ReleaseYear = (int)dr["ReleaseYear"];
                        currentRow.Director = dr["Director"].ToString();
                        currentRow.Rating = dr["Rating"].ToString();
                        currentRow.Notes = dr["Notes"].ToString();

                        result = currentRow;
                    }
                }
            }
            return result;
        }
    }
}
