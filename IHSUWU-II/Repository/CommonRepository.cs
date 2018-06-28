using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Login.Models;

namespace Login.Repository
{
    public class CommonRepository : RepositoryBase<UserContext>
    {
        public CommonRepository()
        {
        }

        public List<Divisions> GetAllDivisions()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Divisions> divisions = new List<Divisions>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    divisions = db.Query<Divisions>("exec usp_GetAllDivisions").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return divisions;
        }

        public List<Location> AllLocations()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Location> divisions = new List<Location>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    divisions = db.Query<Location>("exec usp_GetAllLocations").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return divisions;
        }

        public List<Supplier> GetAllSuppliers()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Supplier> suppliers = new List<Supplier>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    suppliers = db.Query<Supplier>("exec usp_GetAllSuppliers").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return suppliers;
        }

        public List<Product> AllProducts()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Product> suppliers = new List<Product>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    suppliers = db.Query<Product>("exec usp_GetAllProducts").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return suppliers;
        }

        public List<PO> AllPOs()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<PO> suppliers = new List<PO>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    suppliers = db.Query<PO>("exec usp_GetAllPOs").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return suppliers;
        }

        public List<GRN> AllGRNs()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<GRN> suppliers = new List<GRN>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    suppliers = db.Query<GRN>("exec usp_GetAllGRNs").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return suppliers;
        }

        public List<MainCatogory> AllMainAssests()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<MainCatogory> mainassests = new List<MainCatogory>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    mainassests = db.Query<MainCatogory>("exec usp_GetAllMainAssests").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return mainassests;
        }

        public List<Requests> AllRequests()
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Requests> mainassests = new List<Requests>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    mainassests = db.Query<Requests>("exec usp_GetAllRequests").ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return mainassests;
        }

        public List<Designations> GetAllDesignations(int? Division)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Designations> designation = new List<Designations>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    designation = db.Query<Designations>("usp_GetDesignationFromDivision  " + Division).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return designation;
        }

        public List<RequestItem> AllRequestItems(int? Division)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<RequestItem> designation = new List<RequestItem>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    designation = db.Query<RequestItem>("usp_RequestItemFromRequest  " + Division).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return designation;
        }


        public List<Product> AllProducts(int? Division)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<Product> designation = new List<Product>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    designation = db.Query<Product>("usp_GetDesignationFromDivision  " + Division).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return designation;
        }

        public List<POItemList> AllPOItem(int? Division)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<POItemList> designation = new List<POItemList>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    designation = db.Query<POItemList>("usp_GetItemsOfPO  " + Division).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return designation;
        }

        public List<GRNItemList> AllGRNItem(int? GRNId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<GRNItemList> designation = new List<GRNItemList>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    designation = db.Query<GRNItemList>("usp_GetItemsOfGRN  " + GRNId).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return designation;
        }

        public List<SubCatogory> GetSubCatogory(int? MC)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<SubCatogory> SubCatogoryList = new List<SubCatogory>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    SubCatogoryList = db.Query<SubCatogory>("usp_GetSubCatogoryFromMain  " + MC).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return SubCatogoryList;
        }

    }
}