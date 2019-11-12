using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace MVCDemo.Models {
    public static class SuperDAL {
        private static string ReadOnlyConnectionString = "Server=localhost;Database=SuperHeroes;Trusted_Connection=True;";
        private static string EditOnlyConnectionString = "Server=localhost;Database=SuperHeroes;Trusted_Connection=True;";
        internal enum dbAction {
            Read,
            Edit
        }
        #region Database Object Strings
        #region Citizen
        internal const string db_Citizen_ID = "CitizenID";
        internal const string db_Citizen_FirstName = "FirstName";
        internal const string db_Citizen_LastName = "LastName";
        internal const string db_Citizen_DateOfBirth = "DateOfBirth";
        internal const string db_Citizen_EyeColor = "EyeColor";
        internal const string db_Citizen_HeightInInches = "HeightInInches";
        #endregion
        #region City
        internal const string db_City_ID= "CityID";
        internal const string db_City_Name= "Name";
        #endregion

        #region Costume
        internal const string db_Costume_ID= "CostumeID";
        internal const string db_Costume_ColorMain= "ColorMain";
        internal const string db_Costume_ColorSecondary= "ColorSecondary";
        internal const string db_Costume_ColorTertiary= "ColorTertiary";
        internal const string db_Costume_HasCape= "HasCape";
        internal const string db_Costume_HasMask = "HasMask";
        #endregion
        #region Hideout
        internal const string db_Hideout_ID = "HideoutID";
        internal const string db_Hideout_Name = "Name";
        internal const string db_Hideout_IsHeroBase = "IsHeroBase";
        #endregion
        #region PetType
        internal const string db_PetType_ID = "PetTypeID";
        internal const string db_PetType_Name = "Name";
        #endregion
        #region SuperHero
        internal const string db_SuperHero_ID = "SuperHeroID";
        internal const string db_SuperHero_FirstName = "FirstName";
        internal const string db_SuperHero_LastName = "LastName";
        internal const string db_SuperHero_DateOfBirth = "DateOfBirth";
        internal const string db_SuperHero_EyeColor = "EyeColor";
        internal const string db_SuperHero_HeightInInches = "HeightInInches";
        internal const string db_SuperHero_AlterEgo = "AlterEgoID";
        internal const string db_SuperHero_SideKick = "SideKickID";
        internal const string db_SuperHero_Costume = "CostumeID";
        #endregion
        #region SuperPet
        internal const string db_SuperPet_ID = "SuperPetID";
        internal const string db_SuperPet_Name = "Name";
        internal const string db_SuperPet_PetType = "PetTypeID";
        internal const string db_SuperPet_SuperHero = "SuperHeroID";
        #endregion
        #region Villian
        internal const string db_Villian_ID= "VillianID";
        internal const string db_Villian_FirstName= "FirstName";
        internal const string db_Villian_LastName= "LastName";
        internal const string db_Villian_DateOfBirth= "DateOfBirth";
        internal const string db_Villian_EyeColor= "EyeColor";
        internal const string db_Villian_HeightInInches= "HeightInInches";
        internal const string db_Villian_SideKick= "SideKickID";
        internal const string db_Villian_Costume = "CostumeID";
        #endregion
        #endregion
        #region Database Connections
        internal static void ConnectToDatabase(SqlCommand comm, dbAction action = dbAction.Read) {
            try {
                if (action == dbAction.Edit)
                    comm.Connection = new SqlConnection(EditOnlyConnectionString);
                else
                    comm.Connection = new SqlConnection(ReadOnlyConnectionString);

                comm.CommandType = System.Data.CommandType.StoredProcedure;
            } catch (Exception ex) { }
        }
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
        /// Gets the ChangeMeOut.Citizen correposponding with the given ID
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
        /// Gets the ChangeMeOut.Citizencorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.Citizen objects from the database.
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
                comm.Parameters.AddWithValue("@" + Citizen.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Citizen.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Citizen.db_HeightInInches, obj.HeightInInches);
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
                comm.Parameters.AddWithValue("@" + Citizen.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Citizen.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Citizen.db_HeightInInches, obj.HeightInInches);
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
        /// Gets the ChangeMeOut.Costume correposponding with the given ID
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
        /// Gets the ChangeMeOut.Costumecorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.Costume objects from the database.
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


        #region SuperHeroe
        /// <summary>
        /// Gets the ChangeMeOut.SuperHeroe correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperHeroe GetSuperHeroe(String idstring, Boolean retNewObject) {
            SuperHeroe retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new SuperHeroe();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetSuperHeroe(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the ChangeMeOut.SuperHeroecorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static SuperHeroe GetSuperHeroe(int id) {
            SqlCommand comm = new SqlCommand("sprocSuperHeroeGet");
            SuperHeroe retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new SuperHeroe(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all ChangeMeOut.SuperHeroe objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<SuperHeroe> GetSuperHeroes() {
            SqlCommand comm = new SqlCommand("sprocSuperHeroesGetAll");
            List<SuperHeroe> retList = new List<SuperHeroe>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new SuperHeroe(dr));
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retList;
        }




        /// <summary>
        /// Attempts to add a database entry corresponding to the given SuperHeroe
        /// </summary>
        /// <remarks></remarks>

        internal static int AddSuperHeroe(SuperHeroe obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperHeroeAdd");
            try {
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_HeightInInches, obj.HeightInInches);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_AlterEgo, obj.AlterEgo);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_Costume, obj.CostumeID);
                return AddObject(comm, "@" + SuperHeroe.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given SuperHeroe
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateSuperHeroe(SuperHeroe obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_SuperHeroeUpdate");
            try {
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_FirstName, obj.FirstName);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_LastName, obj.LastName);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_HeightInInches, obj.HeightInInches);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_AlterEgo, obj.AlterEgo);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_SideKick, obj.SideKick);
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_Costume, obj.CostumeID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the SuperHeroe
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveSuperHeroe(SuperHeroe obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + SuperHeroe.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region Villian
        /// <summary>
        /// Gets the ChangeMeOut.Villian correposponding with the given ID
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
        /// Gets the ChangeMeOut.Villiancorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.Villian objects from the database.
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
                comm.Parameters.AddWithValue("@" + Villian.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Villian.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Villian.db_HeightInInches, obj.HeightInInches);
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
                comm.Parameters.AddWithValue("@" + Villian.db_DateOfBirth, obj.DateOfBirth);
                comm.Parameters.AddWithValue("@" + Villian.db_EyeColor, obj.EyeColor);
                comm.Parameters.AddWithValue("@" + Villian.db_HeightInInches, obj.HeightInInches);
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
        /// Gets the ChangeMeOut.City correposponding with the given ID
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
        /// Gets the ChangeMeOut.Citycorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.City objects from the database.
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
        /// Gets the ChangeMeOut.Hideout correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Hideout GetHideout(String idstring, Boolean retNewObject) {
            Hideout retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new Hideout();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetHideout(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the ChangeMeOut.Hideoutcorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static Hideout GetHideout(int id) {
            SqlCommand comm = new SqlCommand("sprocHideoutGet");
            Hideout retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + Hideout.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new Hideout(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all ChangeMeOut.Hideout objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<Hideout> GetHideouts() {
            SqlCommand comm = new SqlCommand("sprocHideoutsGetAll");
            List<Hideout> retList = new List<Hideout>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new Hideout(dr));
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

        internal static int AddHideout(Hideout obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutAdd");
            try {
                comm.Parameters.AddWithValue("@" + Hideout.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Hideout.db_IsHeroBase, obj.IsHeroBase);
                return AddObject(comm, "@" + Hideout.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given Hideout
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateHideout(Hideout obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutUpdate");
            try {
                comm.Parameters.AddWithValue("@" + Hideout.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + Hideout.db_Name, obj.Name);
                comm.Parameters.AddWithValue("@" + Hideout.db_IsHeroBase, obj.IsHeroBase);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the Hideout
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveHideout(Hideout obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + Hideout.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region Universe
        /// <summary>
        /// Gets the ChangeMeOut.Universe correposponding with the given ID
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
        /// Gets the ChangeMeOut.Universecorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.Universe objects from the database.
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


        #region HideoutMember
        /// <summary>
        /// Gets the ChangeMeOut.HideoutMember correposponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static HideoutMember GetHideoutMember(String idstring, Boolean retNewObject) {
            HideoutMember retObject = null;
            int ID;
            if (int.TryParse(idstring, out ID)) {
                if (ID == -1 && retNewObject) {
                    retObject = new HideoutMember();
                    retObject.ID = -1;
                } else if (ID >= 0) {
                    retObject = GetHideoutMember(ID);
                }
            }
            return retObject;
        }


        /// <summary>
        /// Gets the ChangeMeOut.HideoutMembercorresponding with the given ID
        /// </summary>
        /// <remarks></remarks>

        public static HideoutMember GetHideoutMember(int id) {
            SqlCommand comm = new SqlCommand("sprocHideoutMemberGet");
            HideoutMember retObj = null;
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember.db_ID, id);
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retObj = new HideoutMember(dr);
                }
                comm.Connection.Close();
            } catch (Exception ex) {
                comm.Connection.Close();
            }
            return retObj;
        }


        /// <summary>
        /// Gets a list of all ChangeMeOut.HideoutMember objects from the database.
        /// </summary>
        /// <remarks></remarks>
        public static List<HideoutMember> GetHideoutMembers() {
            SqlCommand comm = new SqlCommand("sprocHideoutMembersGetAll");
            List<HideoutMember> retList = new List<HideoutMember>();
            try {
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader dr = GetDataReader(comm);
                while (dr.Read()) {
                    retList.Add(new HideoutMember(dr));
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

        internal static int AddHideoutMember(HideoutMember obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutMemberAdd");
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember.db_Hideout, obj.HideoutID);
                comm.Parameters.AddWithValue("@" + HideoutMember.db_Member, obj.Member);
                return AddObject(comm, "@" + HideoutMember.db_ID);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to the database entry corresponding to the given HideoutMember
        /// </summary>
        /// <remarks></remarks>

        internal static int UpdateHideoutMember(HideoutMember obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand("sproc_HideoutMemberUpdate");
            try {
                comm.Parameters.AddWithValue("@" + HideoutMember.db_ID, obj.ID);
                comm.Parameters.AddWithValue("@" + HideoutMember.db_Hideout, obj.HideoutID);
                comm.Parameters.AddWithValue("@" + HideoutMember.db_Member, obj.Member);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        /// <summary>
        /// Attempts to delete the database entry corresponding to the HideoutMember
        /// </summary>
        /// <remarks></remarks>
        internal static int RemoveHideoutMember(HideoutMember obj) {
            if (obj == null) return -1;
            SqlCommand comm = new SqlCommand();
            try {
                //comm.CommandText = //Insert Sproc Name Here;
                comm.Parameters.AddWithValue("@" + HideoutMember.db_ID, obj.ID);
                return UpdateObject(comm);
            } catch (Exception ex) {
            }
            return -1;
        }


        #endregion


        #region PetType
        /// <summary>
        /// Gets the ChangeMeOut.PetType correposponding with the given ID
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
        /// Gets the ChangeMeOut.PetTypecorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.PetType objects from the database.
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
        /// Gets the ChangeMeOut.SuperPet correposponding with the given ID
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
        /// Gets the ChangeMeOut.SuperPetcorresponding with the given ID
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
        /// Gets a list of all ChangeMeOut.SuperPet objects from the database.
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

        #region Fill Methods
        /// <summary>
        /// Fills Citizen from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(Citizen objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_Citizen_ID];
            objToFill.FirstName = (string)dr[db_Citizen_FirstName];
            objToFill.LastName = (string)dr[db_Citizen_LastName];
            objToFill.BirthDate = (DateTime)dr[db_Citizen_DateOfBirth];
            objToFill.EyeColor = (byte)dr[db_Citizen_EyeColor];
            objToFill.Height = (double)dr[db_Citizen_HeightInInches];
        }

        /// <summary>
        /// Fills City from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(City objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_City_ID];
            objToFill.Name = (string)dr[db_City_Name];
        }

        /// <summary>
        /// Fills Costume from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(Costume objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_Costume_ID];s
            objToFill.ColorMain = (int)dr[db_Costume_ColorMain];
            objToFill.ColorSecondary = (int)dr[db_Costume_ColorSecondary];
            objToFill.ColorTertiary = (int)dr[db_Costume_ColorTertiary];
            objToFill.HasCape = (bool)dr[db_Costume_HasCape];
            objToFill.HasMask = (bool)dr[db_Costume_HasMask];
        }

        /// <summary>
        /// Fills Costume from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(Hideout<object,object> objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_Hideout_ID];
            objToFill.Name = (string)dr[db_Hideout_Name];
            objToFill.IsHeroBase = (bool)dr[db_Hideout_IsHeroBase];
        }

        /// <summary>
        /// Fills PetType from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(PetType objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_PetType_ID];
            objToFill.Name = (string)dr[db_PetType_Name];
        }

        /// <summary>
        /// Fills SuperHero from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(SuperHero objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_SuperHero_ID];
            objToFill.FirstName = (string)dr[db_SuperHero_FirstName];
            objToFill.LastName = (string)dr[db_SuperHero_LastName];
            objToFill.DateOfBirth = (DateTime)dr[db_SuperHero_DateOfBirth];
            objToFill.EyeColor = (byte)dr[db_SuperHero_EyeColor];
            objToFill.HeightInInches = (double)dr[db_SuperHero_HeightInInches];
            objToFill.AlterEgo = (int)dr[db_SuperHero_AlterEgo];
            objToFill.SideKick = (int)dr[db_SuperHero_SideKick];
            objToFill.CostumeID = (int)dr[Costume.db_SuperHero_ID];
        }

        /// <summary>
        /// Fills SuperPet from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(SuperPet objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_SuperPet_ID];
            objToFill.Name = (string)dr[db_SuperPet_Name];
            objToFill.PetTypeID = (int)dr[PetType.db_SuperPet_ID];
            objToFill.SuperHeroID = (int)dr[db_SuperPet_SuperHero];
        }

        /// <summary>
        /// Fills Villian from a SqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        private static void Fill(Villian objToFill, SqlDataReader dr) {
            objToFill.ID = (int)dr[db_Villian_ID];
            objToFill.FirstName = (string)dr[db_Villian_FirstName];
            objToFill.LastName = (string)dr[db_Villian_LastName];
            objToFill.BirthDate = (DateTime)dr[db_Villian_DateOfBirth];
            objToFill.EyeColor = (byte)dr[db_Villian_EyeColor];
            objToFill.Height = (double)dr[db_Villian_HeightInInches];
            objToFill.SideKick = (int)dr[db_Villian_SideKick];
            objToFill.CostumeID = (int)dr[Costume.db_Villian_ID];
        }

        #endregion

    }
}
