using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    public static class DAL {
        static String connectionString = "Server=localhost;Database=SuperHeroes;Trusted_Connection=True;";

        private static List<SuperHero> _sups = new List<SuperHero>();
        public static List<SuperHero> SuperHeroesGet() {
            List<SuperHero> retList = new List<SuperHero>();
            SqlConnection conn = null;

            try {
                conn = new SqlConnection();
                conn.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "sprocSuperHeroesGetAll";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read()) {
                    SuperHero sup = new SuperHero();
                    sup.ID  = (int)dr["SuperHeroID"];
                    sup.FirstName  = dr["FirstName"].ToString();
                    sup.LastName  = (string)dr["LastName"];
                    sup.BirthDate  = (DateTime)dr["DateOfBirth"];
                    //sup.EyeColor  = (Person.Color)dr["EyeColor"];
                    //sup.Height  = (double)dr["HeightInInches"];
                    //sup.AlterEgo  = dr["AlterEgoID"];
                    //sup.  = dr["SideKickID"];
                    //sup.  = dr["CostumeID"];
                    retList.Add(sup);
                }

            } catch (Exception ex) {

            } finally {
                if (conn != null) conn.Close();
            }
            return retList;
        }

        public static SuperHero SuperHeroesGet(int id) {
            SuperHero retObject = null; 
            SqlConnection conn = null;

            try {
                conn = new SqlConnection();
                conn.ConnectionString = connectionString;

                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "sprocSuperHeroGet";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SuperHeroID", id);

                conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read()) {
                    retObject = new SuperHero();
                    retObject.ID = (int)dr["SuperHeroID"];
                    retObject.FirstName = dr["FirstName"].ToString();
                    retObject.LastName = (string)dr["LastName"];
                    retObject.BirthDate = (DateTime)dr["DateOfBirth"];
                }

            } catch (Exception ex) {
                retObject = null;
            } finally {
                if (conn != null) conn.Close();
            }
            return retObject;
        }

    }
}
