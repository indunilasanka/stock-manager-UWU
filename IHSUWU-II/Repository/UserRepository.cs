using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Login.Repository
{
    public class UserRepository : RepositoryBase<UserContext>
    {
        public UserRepository()
        {
        }

        public User CheckUserExits(User model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            User UserDetails = new User();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    UserDetails = db.Query<User>("exec usp_CheckUserExists " + "@UserName,@Password",
                            new { UserName = model.UserName, Password = model.Password }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return UserDetails;
        }

        public UserViewModel SearchUsers(UserViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            UserViewModel UserDetails = new UserViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    UserDetails.UserList = db.Query<User>("exec usp_SearchUsers " + "@FullName,@Email,@Division",
                            new { FullName = model.FullName, Email = model.Email, Division = model.Division }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return UserDetails;
        }

        public User GetUser(int? UserId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            User user = new User();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<User>("exec usp_GetUser " + "@UserId",
                            new { UserId = UserId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return user;
        }


        public int UpdateUser(User user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditUser " +
                             " @UserId,@FullName,@Email,@Designation,@Division,@UserName,@Password,@IsPasswordChanged; select @@ret; ",
                      new
                      {
                          @UserId = user.UserId,
                          @FullName = user.FullName,
                          @Email = user.Email,
                          @Designation = user.Designation,
                          @Division = user.Division,
                          @UserName = user.UserName,
                          @Password = user.Password,
                          @IsPasswordChanged = user.IsPasswordChanged
                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }

        public int AddUser(User user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddUser " + " @FullName,@Email,@Designation,@Division,@UserName,@Password; select @@ret;",
                        new
                        {
                            FullName = user.FullName,
                            Email = user.Email,
                            Designation = user.Designation,
                            Division = user.Division,
                            UserName = user.UserName,
                            Password = user.Password

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }

        public int DeleteUser(int? UserId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteUser" + " @UserId", new { UserId = UserId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public LocationViewModel SearchLocations(LocationViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            LocationViewModel LocationDetails = new LocationViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    LocationDetails.LocationList = db.Query<Location>("exec usp_SearchLocations " + "@LocationName,@LocationSymbol",
                            new { LocationName = model.LocationName, LocationSymbol = model.LocationSymbol }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return LocationDetails;
        }

        public Location GetLocation(int? LocationId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            Location location = new Location();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    location = db.Query<Location>("exec usp_GetLocation " + "@LocationId",
                            new { LocationId = LocationId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return location;
        }


        public int UpdateLocation(Location location)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditLocation " +
                             " @LocationId,@LocationName,@LocationSymbol; select @@ret; ",
                      new
                      {
                          @LocationId = location.LocationId,
                          @LocationName = location.LocationName,
                          @LocationSymbol = location.LocationSymbol

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }

        public int AddLocation(Location location)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddLocation " + " @LocationName,@LocationSymbol; select @@ret;",
                        new
                        {
                            LocationName = location.LocationName,
                            LocationSymbol = location.LocationSymbol

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }

        public int DeleteLocation(int? LocationId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteLocation" + " @LocationId", new { LocationId = LocationId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public SupplierViewModel SearchSuppliers(SupplierViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            SupplierViewModel SupplierDetails = new SupplierViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    SupplierDetails.SupplierList = db.Query<Supplier>("exec usp_SearchSuppliers " + "@SName,@SMobile,@SEmail",
                            new { SName = model.SName, SMobile = model.SMobile, SEmail = model.SEmail }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return SupplierDetails;
        }

        public Supplier GetSupplier(int? SId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            Supplier suppliers = new Supplier();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    suppliers = db.Query<Supplier>("exec usp_GetSupplier " + "@SId",
                            new { SId = SId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
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

        public int UpdateSupplier(Supplier supplier)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditSupplier " +
                             " @SId,@SName,@SAddress,@SMobile,@SEmail; select @@ret; ",
                      new
                      {
                          @SId = supplier.SId,
                          @SName = supplier.SName,
                          @SAddress = supplier.SAddress,
                          @SMobile = supplier.SMobile,
                          @SEmail = supplier.SEmail

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }

        public int AddSupplier(Supplier supplier)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddSupplier " + " @SName,@SAddress,@SMobile,@SEmail; select @@ret;",
                        new
                        {
                            @SName = supplier.SName,
                            @SAddress = supplier.SAddress,
                            @SMobile = supplier.SMobile,
                            @SEmail = supplier.SEmail
                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }

        public int DeleteSupplier(int? SId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteSupplier" + " @SId", new { SId = SId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public MainCatogoryViewModel SearchMainAssests(MainCatogoryViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            MainCatogoryViewModel assestDetails = new MainCatogoryViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assestDetails.MainAssestsList = db.Query<MainCatogory>("exec usp_SearchMainAssests " + "@MCName,@MCSymbol",
                            new { MCName = model.MCName, MCSymbol = model.MCSymbol }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assestDetails;
        }
        public MainCatogory GetMainAssest(int? MCId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            MainCatogory assest = new MainCatogory();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assest = db.Query<MainCatogory>("exec usp_GetMainAssest " + "@MCId",
                            new { MCId = MCId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assest;
        }
        public int UpdateMainAssest(MainCatogory assest)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditMainAssest " +
                             " @MCId,@MCName,@MCSymbol; select @@ret; ",
                      new
                      {
                          @MCId = assest.MCId,
                          @MCName = assest.MCName,
                          @MCSymbol = assest.MCSymbol

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }
        public int AddMainAssest(MainCatogory assest)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddMainAssest " + " @MCName,@MCSymbol; select @@ret;",
                        new
                        {
                            @MCName = assest.MCName,
                            @MCSymbol = assest.MCSymbol

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }
        public int DeleteMainAssest(int? MCId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteMainAssest " + " @MCId", new { MCId = MCId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public SubCatogoryViewModel SearchSubAssests(SubCatogoryViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            SubCatogoryViewModel assestDetails = new SubCatogoryViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assestDetails.SubAssestsList = db.Query<SubCatogory>("exec usp_SearchSubAssests " + "@MCId,@SCName,@SCSymbol",
                            new { MCId = model.MCId, SCName = model.SCName, SCSymbol = model.SCSymbol }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assestDetails;
        }
        public SubCatogory GetSubAssest(int? SCId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            SubCatogory assest = new SubCatogory();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assest = db.Query<SubCatogory>("exec usp_GetSubAssest " + "@SCId",
                            new { SCId = SCId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assest;
        }
        public int UpdateSubAssest(SubCatogory assest)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditSubAssest " +
                             " @SCId,@MCId,@SCName,@SCSymbol; select @@ret; ",
                      new
                      {
                          @SCId = assest.SCId,
                          @MCId = assest.MCId,
                          @SCName = assest.SCName,
                          @SCSymbol = assest.SCSymbol

                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }
        public int AddSubAssest(SubCatogory assest)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddSubAssest " + " @MCId,@SCName,@SCSymbol; select @@ret;",
                        new
                        {
                            @MCId = assest.MCId,
                            @SCName = assest.SCName,
                            @SCSymbol = assest.SCSymbol

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }
        public int DeleteSubAssest(int? SCId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteSubAssest " + " @SCId", new { SCId = SCId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public ProductViewModel SearchProducts(ProductViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            ProductViewModel assestDetails = new ProductViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assestDetails.ProductsList = db.Query<Product>("exec usp_SearchProducts " + "@MCId,@ProNo,@ProName",
                            new { MCId = model.MCId, ProNo = model.ProNo, ProName = model.ProName }).ToList();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assestDetails;
        }
        public Product GetProduct(int? PROId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            Product assest = new Product();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    assest = db.Query<Product>("exec usp_GetProduct " + "@PROId",
                            new { PROId = PROId }).FirstOrDefault();
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            return assest;
        }
        public int UpdateProduct(Product assest)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditProduct " +
                             " @PROId,@SCId,@MCId,@ProNo,@ProName; select @@ret; ",
                      new
                      {
                          @PROId = assest.PROId,
                          @SCId = assest.SCId,
                          @MCId = assest.MCId,
                          @ProNo = assest.ProNo,
                          @ProName = assest.ProName
                      });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;

        }
        public int AddProduct(Product assest)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddProduct " + " @SCId,@MCId,@ProNo,@ProName; select @@ret;",
                        new
                        {
                            @SCId = assest.SCId,
                            @MCId = assest.MCId,
                            @ProNo = assest.ProNo,
                            @ProName = assest.ProName

                        });

            }
            catch (Exception ex)
            {

            }

            finally
            {
                if (dbConn != null)
                { dbConn.Close(); }
            }
            return id;
        }
        public int DeleteProduct(int? PROId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteProduct " + " @PROId", new { PROId = PROId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


    }
}

