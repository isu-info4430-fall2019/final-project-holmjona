using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace MVCDemo {
    public static class SuperDAL {
        private static string ReadOnlyConnectionString = "Server=localhost;Database=SuperHeroes;Trusted_Connection=True;";
        private static string EditOnlyConnectionString = "Server=localhost;Database=SuperHeroes;Trusted_Connection=True;";
        internal enum dbAction {
            Read,
            Edit
        }
        #region Database Connections
        /// <summary>
        /// Connects to the database based on the connection string needed
        /// </summary>
        /// <param name="comm">Command we are connecting to the database</param>
        /// <param name="action">The action type that will affect the connections string.</param>
        internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read) {
            try {
                if (action == dbAction.Edit)
                    comm.Connection = new SqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new SqlConnection(ReadOnlyConnectionString);

                comm.CommandType = System.Data.CommandType.StoredProcedure;
            } catch (Exception ex) { }
        }
        /// <summary>
        /// Sets Connection and Executes a command to read data from the database
        /// </summary>
        /// <param name="comm">Command we will run.</param>
        /// <returns>Table result of the returning data requested.</returns>
        public static SqlDataReader GetDataReader(SqlCommand comm) {
            try {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Sets Connection and Executes a command to add a new object/ entity to the database.
        /// </summary>
        /// <param name="comm">Command that will be run</param>
        /// <param name="parameterName">parameter name that will contain the returning Identity value.</param>
        /// <returns>The new Identity created by the database for this object after adding it.</returns>
        internal static int AddObject(SqlCommand comm, string parameterName) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                SqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            } catch (Exception ex) {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }


        /// <summary>
        /// Sets Connection and Executes given comm on the database
        /// </summary>
        /// <param name="comm">SQLCommand to execute</param>
        /// <returns>number of rows affected; -1 on failure, positive value on success.</returns>
        /// <remarks>Must make sure to populate the command with sqltext and any parameters before passing to this function.
        ///       Edit Connection is set here.</remarks>
        internal static int UpdateObject(SqlCommand comm) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            } catch (Exception ex) {
                if (comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }
        #endregion
        #region Citizen
        /// <summary>
        /// Gets the MVCDemo.Citizen correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Citizen GetCitizen(String idstring, Boolean retNewObject) {
            Citizen retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Citizen();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetCitizen(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.Citizencorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Citizen GetCitizen(int id) {
            SqlCommand comm = new SqlCommand("sprocCitizenGet");
            Citizen retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Citizen.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Citizen(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.Citizen objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Citizen> GetCitizens() {
            SqlCommand comm = new SqlCommand("sprocCitizensGetAll");
            List<Citizen> retList = new List<Citizen>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Citizen(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Citizen
        /// </summary>
        /// <remarks></remarks>

        internal static int AddCitizen(Citizen obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CitizenAdd");
            try {
                comm.Parameters.AddWithValue("@" + Citizen.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Citizen.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Citizen.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + Citizen.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Citizen.db_HeightInInches, obj.Height);
                return AddObject(comm, "@" + Citizen.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Citizen
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateCitizen(Citizen obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CitizenUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Citizen.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Citizen.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Citizen.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Citizen.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + Citizen.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Citizen.db_HeightInInches, obj.Height);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Citizen
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveCitizen(Citizen obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + Citizen.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region Costume
        /// <summary>
        /// Gets the MVCDemo.Costume correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Costume GetCostume(String idstring, Boolean retNewObject) {
            Costume retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Costume();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetCostume(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.Costumecorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Costume GetCostume(int id) {
            SqlCommand comm = new SqlCommand("sprocCostumeGet");
            Costume retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Costume.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Costume(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.Costume objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Costume> GetCostumes() {
            SqlCommand comm = new SqlCommand("sprocCostumesGetAll");
            List<Costume> retList = new List<Costume>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Costume(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Costume
        /// </summary>
        /// <remarks></remarks>

        internal static int AddCostume(Costume obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CostumeAdd");
            try {
                comm.Parameters.AddWithValue("@" + Costume.db_ColorMain, obj.ColorMain);
                comm.Parameters.AddWithValue("@" + Costume.db_ColorSecondary, obj.ColorSecondary);
                comm.Parameters.AddWithValue("@" + Costume.db_ColorTertiary, obj.ColorTertiary);
                comm.Parameters.AddWithValue("@" + Costume.db_HasCape, obj.HasCape);
                comm.Parameters.AddWithValue("@" + Costume.db_HasMask, obj.HasMask);
                return AddObject(comm, "@" + Costume.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Costume
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateCostume(Costume obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CostumeUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Costume.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Costume.db_ColorMain, obj.ColorMain);
                comm.Parameters.AddWithValue("@" + Costume.db_ColorSecondary, obj.ColorSecondary);
                comm.Parameters.AddWithValue("@" + Costume.db_ColorTertiary, obj.ColorTertiary);
                comm.Parameters.AddWithValue("@" + Costume.db_HasCape, obj.HasCape);
                comm.Parameters.AddWithValue("@" + Costume.db_HasMask, obj.HasMask);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Costume
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveCostume(Costume obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + Costume.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region SuperHero
        /// <summary>
        /// Gets the MVCDemo.SuperHero correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperHero GetSuperHero(String idstring, Boolean retNewObject) {
            SuperHero retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new SuperHero();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetSuperHero(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.SuperHerocorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperHero GetSuperHero(int id) {
            SqlCommand comm = new SqlCommand("sprocSuperHeroGet");
            SuperHero retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + SuperHero.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new SuperHero(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.SuperHero objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<SuperHero> GetSuperHeroes() {
            SqlCommand comm = new SqlCommand("sprocSuperHeroesGetAll");
            List<SuperHero> retList = new List<SuperHero>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new SuperHero(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given SuperHero
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSuperHero(SuperHero obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperHeroAdd");
            try {
                comm.Parameters.AddWithValue("@" + SuperHero.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + SuperHero.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + SuperHero.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + SuperHero.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + SuperHero.db_HeightInInches, obj.Height);
                comm.Parameters.AddWithValue("@" + SuperHero.db_AlterEgo, obj.AlterEgo);
                comm.Parameters.AddWithValue("@" + SuperHero.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + SuperHero.db_Costume, obj.CostumeID);
                return AddObject(comm, "@" + SuperHero.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given SuperHero
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSuperHero(SuperHero obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperHeroUpdate");
            try {
                comm.Parameters.AddWithValue("@" + SuperHero.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + SuperHero.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + SuperHero.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + SuperHero.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + SuperHero.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + SuperHero.db_HeightInInches, obj.Height);
                comm.Parameters.AddWithValue("@" + SuperHero.db_AlterEgo, obj.AlterEgo);
                comm.Parameters.AddWithValue("@" + SuperHero.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + SuperHero.db_Costume, obj.CostumeID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the SuperHero
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSuperHero(SuperHero obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + SuperHero.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region Villian
        /// <summary>
        /// Gets the MVCDemo.Villian correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Villian GetVillian(String idstring, Boolean retNewObject) {
            Villian retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Villian();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetVillian(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.Villiancorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Villian GetVillian(int id) {
            SqlCommand comm = new SqlCommand("sprocVillianGet");
            Villian retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Villian.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Villian(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.Villian objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Villian> GetVillians() {
            SqlCommand comm = new SqlCommand("sprocVilliansGetAll");
            List<Villian> retList = new List<Villian>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Villian(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Villian
        /// </summary>
        /// <remarks></remarks>

        internal static int AddVillian(Villian obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_VillianAdd");
            try {
                comm.Parameters.AddWithValue("@" + Villian.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Villian.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Villian.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + Villian.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Villian.db_HeightInInches, obj.Height);
                comm.Parameters.AddWithValue("@" + Villian.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + Villian.db_Costume, obj.CostumeID);
                return AddObject(comm, "@" + Villian.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Villian
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateVillian(Villian obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_VillianUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Villian.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Villian.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + Villian.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + Villian.db_DateOfBirth, obj.BirthDate);
                comm.Parameters.AddWithValue("@" + Villian.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Villian.db_HeightInInches, obj.Height);
                comm.Parameters.AddWithValue("@" + Villian.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + Villian.db_Costume, obj.CostumeID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Villian
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveVillian(Villian obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + Villian.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region City
        /// <summary>
        /// Gets the MVCDemo.City correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static City GetCity(String idstring, Boolean retNewObject) {
            City retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new City();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetCity(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.Citycorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static City GetCity(int id) {
            SqlCommand comm = new SqlCommand("sprocCityGet");
            City retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + City.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new City(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.City objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<City> GetCities() {
            SqlCommand comm = new SqlCommand("sprocCitiesGetAll");
            List<City> retList = new List<City>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new City(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given City
        /// </summary>
        /// <remarks></remarks>

        internal static int AddCity(City obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CityAdd");
            try {
                comm.Parameters.AddWithValue("@" + City.db_Name, obj.Name);
                return AddObject(comm, "@" + City.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given City
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateCity(City obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_CityUpdate");
            try {
                comm.Parameters.AddWithValue("@" + City.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + City.db_Name, obj.Name);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the City
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveCity(City obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + City.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region Hideout
        /// <summary>
        /// Gets the MVCDemo.Hideout correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static dynamic GetHideout(Type T, String idstring, Boolean retNewObject) {
            Hideout<SuperHero> retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Hideout<SuperHero>();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetHideout(T, ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.Hideoutcorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static dynamic GetHideout(Type T, int id) {
            SqlCommand comm = new SqlCommand("sprocHideoutGet");
            dynamic retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    if (T == typeof(SuperHero))
                        retObj = new Hideout<SuperHero>(dr);
                    else if (T == typeof(Villian))
                        retObj = new Hideout<Villian>(dr);
                    else
                        retObj = new Hideout<object>(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.Hideout objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<dynamic> GetHideouts(Type T) {
            SqlCommand comm = new SqlCommand("sprocHideoutsGetAll");
            List<dynamic> retList = new List<dynamic>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    if (T == typeof(SuperHero))
                        retList.Add(new Hideout<SuperHero>(dr));
                    else if (T == typeof(Villian))
                        retList.Add(new Hideout<Villian>(dr));
                    else
                        retList.Add(new Hideout<object>(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given Hideout
        /// </summary>
        /// <remarks></remarks>

        internal static int AddHideout(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutAdd");
            try {
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_IsHeroBase, obj.IsHeroBase);
                return AddObject(comm, "@" + Hideout<object>.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Hideout
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateHideout(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Hideout<object>.db_IsHeroBase, obj.IsHeroBase);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Hideout
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveHideout(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        /*   #region Universe
           /// <summary>
           /// Gets the MVCDemo.Universe correposponding with the given ID
           /// </summary>
           /// <remarks></remarks>

           public static Universe GetUniverse(String idstring, Boolean retNewObject) {
               Universe retObject = null;
               int ID;
               if (int.TryParse(idstring, out ID)) {
                   if (ID == -1 && retNewObject) {
                       retObject = new Universe();
                       retObject.ID = -1;
                   } else if (ID >= 0) {
                       retObject = GetUniverse(ID);
                   }
               }
               return retObject;
           }


           /// <summary>
           /// Gets the MVCDemo.Universecorresponding with the given ID
           /// </summary>
           /// <remarks></remarks>

           public static Universe GetUniverse(int id) {
               SqlCommand comm = new SqlCommand("sprocUniverseGet");
               Universe retObj = null;
               try {
                   comm.Parameters.AddWithValue("@" + Universe.db_ID, id);
                   SqlDataReader dr = GetDataReader(comm);
                   while (dr.Read()) {
                       retObj = new Universe(dr);
                   }
                   comm.Connection.Close();
               } catch (Exception ex) {
                   comm.Connection.Close();
               }
               return retObj;
           }


           /// <summary>
           /// Gets a list of all MVCDemo.Universe objects from the database.
           /// </summary>
           /// <remarks></remarks>
           public static List<Universe> GetUniverses() {
               SqlCommand comm = new SqlCommand("sprocUniversesGetAll");
               List<Universe> retList = new List<Universe>();
               try {
                   comm.CommandType = System.Data.CommandType.StoredProcedure;
                   SqlDataReader dr = GetDataReader(comm);
                   while (dr.Read()) {
                       retList.Add(new Universe(dr));
                   }
                   comm.Connection.Close();
               } catch (Exception ex) {
                   comm.Connection.Close();
               }
               return retList;
           }




           /// <summary>
           /// Attempts to add a database entry corresponding to the given Universe
           /// </summary>
           /// <remarks></remarks>

           internal static int AddUniverse(Universe obj) {
               if (obj == null) return -1;
               SqlCommand comm = new SqlCommand("sproc_UniverseAdd");
               try {
                   comm.Parameters.AddWithValue("@" + Universe.db_Name, obj.Name);
                   return AddObject(comm, "@" + Universe.db_ID);
               } catch (Exception ex) {
               }
               return -1;
           }


           /// <summary>
           /// Attempts to the database entry corresponding to the given Universe
           /// </summary>
           /// <remarks></remarks>

           internal static int UpdateUniverse(Universe obj) {
               if (obj == null) return -1;
               SqlCommand comm = new SqlCommand("sproc_UniverseUpdate");
               try {
                   comm.Parameters.AddWithValue("@" + Universe.db_ID, obj.ID);
                   comm.Parameters.AddWithValue("@" + Universe.db_Name, obj.Name);
                   return UpdateObject(comm);
               } catch (Exception ex) {
               }
               return -1;
           }


           /// <summary>
           /// Attempts to delete the database entry corresponding to the Universe
           /// </summary>
           /// <remarks></remarks>
           internal static int RemoveUniverse(Universe obj) {
               if (obj == null) return -1;
               SqlCommand comm = new SqlCommand();
               try {
                   //comm.CommandText = //Insert Sproc Name Here;
                   comm.Parameters.AddWithValue("@" + Universe.db_ID, obj.ID);
                   return UpdateObject(comm);
               } catch (Exception ex) {
               }
               return -1;
           }


           #endregion
          */

        #region HideoutMember
        /// <summary>
        /// Gets the MVCDemo.HideoutMember correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static dynamic GetHideoutMember(Type T, String idstring, Boolean retNewObject) {
            dynamic retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    if (T == typeof(SuperHero))
                        retObject = new HideoutMember<SuperHero>();
                    else if (T == typeof(Villian))
                        retObject = new HideoutMember<Villian>();
                    else
                        retObject = new HideoutMember<object>();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetHideoutMember(T,ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.HideoutMembercorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static dynamic GetHideoutMember(Type T, int id) {
            SqlCommand comm = new SqlCommand("sprocHideoutMemberGet");
            dynamic retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    if (T == typeof(SuperHero))
                        retObj = new HideoutMember<SuperHero>(dr);
                    else if (T == typeof(Villian))
                        retObj = new HideoutMember<Villian>(dr);
                    else
                        retObj = new HideoutMember<object>(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.HideoutMember objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<dynamic> GetHideoutMembers(Type T) {
            SqlCommand comm = new SqlCommand("sprocHideoutMembersGetAll");
            List<dynamic> retList = new List<dynamic>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    if (T == typeof(SuperHero))
                        retList.Add(new HideoutMember<SuperHero>(dr));
                    else if (T == typeof(Villian))
                        retList.Add(new HideoutMember<Villian>(dr));
                    else
                        retList.Add(new HideoutMember<object>(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given HideoutMember
        /// </summary>
        /// <remarks></remarks>

        internal static int AddHideoutMember(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutMemberAdd");
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_Hideout, obj.HideoutID);
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_Member, obj.Member);
                return AddObject(comm, "@" + HideoutMember<SuperHero>.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given HideoutMember
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateHideoutMember(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutMemberUpdate");
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_Hideout, obj.HideoutID);
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_Member, obj.Member);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the HideoutMember
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveHideoutMember(dynamic obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + HideoutMember<object>.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion
 

        #region PetType
        /// <summary>
        /// Gets the MVCDemo.PetType correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static PetType GetPetType(String idstring, Boolean retNewObject) {
            PetType retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new PetType();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetPetType(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.PetTypecorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static PetType GetPetType(int id) {
            SqlCommand comm = new SqlCommand("sprocPetTypeGet");
            PetType retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + PetType.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new PetType(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.PetType objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<PetType> GetPetTypes() {
            SqlCommand comm = new SqlCommand("sprocPetTypesGetAll");
            List<PetType> retList = new List<PetType>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new PetType(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given PetType
        /// </summary>
        /// <remarks></remarks>

        internal static int AddPetType(PetType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_PetTypeAdd");
            try {
                comm.Parameters.AddWithValue("@" + PetType.db_Name, obj.Name);
                return AddObject(comm, "@" + PetType.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given PetType
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdatePetType(PetType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_PetTypeUpdate");
            try {
                comm.Parameters.AddWithValue("@" + PetType.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + PetType.db_Name, obj.Name);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the PetType
        /// </summary>
        /// <remarks></remarks>
        internal static int RemovePetType(PetType obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + PetType.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region SuperPet
        /// <summary>
        /// Gets the MVCDemo.SuperPet correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperPet GetSuperPet(String idstring, Boolean retNewObject) {
            SuperPet retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new SuperPet();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetSuperPet(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the MVCDemo.SuperPetcorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperPet GetSuperPet(int id) {
            SqlCommand comm = new SqlCommand("sprocSuperPetGet");
            SuperPet retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + SuperPet.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new SuperPet(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all MVCDemo.SuperPet objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<SuperPet> GetSuperPets() {
            SqlCommand comm = new SqlCommand("sprocSuperPetsGetAll");
            List<SuperPet> retList = new List<SuperPet>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new SuperPet(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets a list of all MVCDemo.SuperPet objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<SuperPet> GetSuperPets(SuperHero sup) {
            SqlCommand comm = new SqlCommand("sprocSuperPetsGetForSuperHero");
            List<SuperPet> retList = new List<SuperPet>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@" + SuperHero.db_ID, sup.ID);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new SuperPet(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given SuperPet
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSuperPet(SuperPet obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperPetAdd");
            try {
                comm.Parameters.AddWithValue("@" + SuperPet.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + SuperPet.db_PetType, obj.PetTypeID);
                comm.Parameters.AddWithValue("@" + SuperPet.db_SuperHero, obj.SuperHeroID);
                return AddObject(comm, "@" + SuperPet.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given SuperPet
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSuperPet(SuperPet obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperPetUpdate");
            try {
                comm.Parameters.AddWithValue("@" + SuperPet.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + SuperPet.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + SuperPet.db_PetType, obj.PetTypeID);
                comm.Parameters.AddWithValue("@" + SuperPet.db_SuperHero, obj.SuperHeroID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the SuperPet
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSuperPet(SuperPet obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + SuperPet.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion

        #region User
        public static List<Models.User> GetUsers() {
            return Models.Users.List;
        }
        public static Models.User GetUser(int id) {
            // Use fake data store
          return  Models.Users.GetByID(id);
        }
        public static Models.User GetUser(string uName,string pWord) {
            // Use fake data store
            Models.User usr = Models.Users.GetByUserName(uName);
            if (usr != null) {
                if (usr.Password == Models.Hasher.HashIt(pWord,usr.Salt)) {
                    // password match
                } else {
                    // no match
                    usr = null;
                }
            }
            return usr;
        }

        public static string GetCookie(Models.User usr) {
            return usr.Salt + usr.ID;
        }
            public static Models.User GetUserForCookie(string cookValue) {
            // Use fake data store
            Models.User usr = null;
            if (!string.IsNullOrEmpty(cookValue)) {
                // we have a cookie set
                int sePlace = cookValue.LastIndexOf("=") + 1;
                string saltCheck = cookValue.Substring(0, sePlace);
                string strID = cookValue.Substring(sePlace);
                int id;
                if (int.TryParse(strID, out id)) {
                    usr = Models.Users.GetByID(id);
                    if (usr.Salt == saltCheck) {
                        // still matches not modified 
                    } else {
                        // salt and or ID changed. Assuming hacking attempt.
                        usr = null;
                    }
                }
            }
            return usr;
        }
        #endregion

    }
}
