using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService()
        {
            _repository = new UserRepository();
        }
        //user for login
        public User CheckUserExits(User model)
        {
            User UserDetails = new User();
            try
            {
                UserDetails = _repository.CheckUserExits(model);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                //network issue
                UserDetails.UserId = -1;
                return UserDetails;
            }

            catch (Exception ex)
            {
                throw;
            }

            return UserDetails;
        }


        public UserViewModel SearchUsers(UserViewModel model)
        {
            UserViewModel UserDetails = new UserViewModel();
            try
            {
                UserDetails = _repository.SearchUsers(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return UserDetails;
        }

        public User GetUser(int? UserId)
        {
            User user = new User();
            try
            {
                user = _repository.GetUser(UserId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdateUser(User user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddUser(User user)
        {
            int id = 0;
            try
            {
                id = _repository.AddUser(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteUser(int? UserId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteUser(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public LocationViewModel SearchLocations(LocationViewModel model)
        {
            LocationViewModel LocationDetails = new LocationViewModel();
            try
            {
                LocationDetails = _repository.SearchLocations(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return LocationDetails;
        }

        public Location GetLocation(int? LocationId)
        {
            Location location = new Location();
            try
            {
                location = _repository.GetLocation(LocationId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return location;
        }

        public int UpdateLocation(Location location)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateLocation(location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddLocation(Location location)
        {
            int id = 0;
            try
            {
                id = _repository.AddLocation(location);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteLocation(int? LocationId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteLocation(LocationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public SupplierViewModel SearchSuppliers(SupplierViewModel model)
        {
            SupplierViewModel SupplierDetails = new SupplierViewModel();
            try
            {
                SupplierDetails = _repository.SearchSuppliers(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return SupplierDetails;
        }

        public Supplier GetSupplier(int? SId)
        {
            Supplier supplier = new Supplier();
            try
            {
                supplier = _repository.GetSupplier(SId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return supplier;
        }

        public int UpdateSupplier(Supplier supplier)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateSupplier(supplier);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddSupplier(Supplier supplier)
        {
            int id = 0;
            try
            {
                id = _repository.AddSupplier(supplier);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteSupplier(int? SId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteSupplier(SId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public MainCatogoryViewModel SearchMainAssests(MainCatogoryViewModel model)
        {
            MainCatogoryViewModel assestDetails = new MainCatogoryViewModel();
            try
            {
                assestDetails = _repository.SearchMainAssests(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assestDetails;
        }

        public MainCatogory GetMainAssest(int? MCId)
        {
            MainCatogory assest = new MainCatogory();
            try
            {
                assest = _repository.GetMainAssest(MCId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assest;
        }

        public int UpdateMainAssest(MainCatogory assest)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateMainAssest(assest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddMainAssest(MainCatogory assest)
        {
            int id = 0;
            try
            {
                id = _repository.AddMainAssest(assest);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteMainAssest(int? MCId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteMainAssest(MCId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public SubCatogoryViewModel SearchSubAssests(SubCatogoryViewModel model)
        {
            SubCatogoryViewModel assestDetails = new SubCatogoryViewModel();
            try
            {
                assestDetails = _repository.SearchSubAssests(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assestDetails;
        }

        public SubCatogory GetSubAssest(int? SCId)
        {
            SubCatogory assest = new SubCatogory();
            try
            {
                assest = _repository.GetSubAssest(SCId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assest;
        }

        public int UpdateSubAssest(SubCatogory assest)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateSubAssest(assest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddSubAssest(SubCatogory assest)
        {
            int id = 0;
            try
            {
                id = _repository.AddSubAssest(assest);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteSubAssest(int? SCId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteSubAssest(SCId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public ProductViewModel SearchProducts(ProductViewModel model)
        {
            ProductViewModel assestDetails = new ProductViewModel();
            try
            {
                assestDetails = _repository.SearchProducts(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assestDetails;
        }

        public Product GetProduct(int? PROId)
        {
            Product assest = new Product();
            try
            {
                assest = _repository.GetProduct(PROId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return assest;
        }

        public int UpdateProduct(Product assest)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateProduct(assest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddProduct(Product assest)
        {
            int id = 0;
            try
            {
                id = _repository.AddProduct(assest);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteProduct(int? PROId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteProduct(PROId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

    }
}