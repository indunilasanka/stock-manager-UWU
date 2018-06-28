using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class CommonService
    {
        private readonly CommonRepository _repository;

        public CommonService()
        {
            _repository = new CommonRepository();
        }

        public List<Divisions> GetAllDivisions()
        {
            List<Divisions> divisions = new List<Divisions>();
            try
            {
                divisions = _repository.GetAllDivisions();
            }

            catch (Exception ex)
            {
                throw;
            }

            return divisions;
        }

        public List<Location> AllLocations()
        {
            List<Location> divisions = new List<Location>();
            try
            {
                divisions = _repository.AllLocations();
            }

            catch (Exception ex)
            {
                throw;
            }

            return divisions;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            try
            {
                suppliers = _repository.GetAllSuppliers();
            }

            catch (Exception ex)
            {
                throw;
            }

            return suppliers;
        }

        public List<Product> AllProducts()
        {
            List<Product> suppliers = new List<Product>();
            try
            {
                suppliers = _repository.AllProducts();
            }

            catch (Exception ex)
            {
                throw;
            }

            return suppliers;
        }

        public List<PO> AllPOs()
        {
            List<PO> suppliers = new List<PO>();
            try
            {
                suppliers = _repository.AllPOs();
            }

            catch (Exception ex)
            {
                throw;
            }

            return suppliers;
        }

        public List<GRN> AllGRNs()
        {
            List<GRN> suppliers = new List<GRN>();
            try
            {
                suppliers = _repository.AllGRNs();
            }

            catch (Exception ex)
            {
                throw;
            }

            return suppliers;
        }

        public List<MainCatogory> AllMainAssests()
        {
            List<MainCatogory> mainassests = new List<MainCatogory>();
            try
            {
                mainassests = _repository.AllMainAssests();
            }

            catch (Exception ex)
            {
                throw;
            }

            return mainassests;
        }

        public List<Requests> AllRequests()
        {
            List<Requests> mainassests = new List<Requests>();
            try
            {
                mainassests = _repository.AllRequests();
            }

            catch (Exception ex)
            {
                throw;
            }

            return mainassests;
        }


        public List<Designations> GetAllDesignations(int? Division)
        {
            List<Designations> designation = new List<Designations>();
            try
            {
                designation = _repository.GetAllDesignations(Division);
            }

            catch (Exception ex)
            {
                throw;
            }

            return designation;
        }

        public List<RequestItem> AllRequestItems(int? Division)
        {
            List<RequestItem> designation = new List<RequestItem>();
            try
            {
                designation = _repository.AllRequestItems(Division);
            }

            catch (Exception ex)
            {
                throw;
            }

            return designation;
        }

        public List<Product> AllProducts(int? Division)
        {
            List<Product> designation = new List<Product>();
            try
            {
                designation = _repository.AllProducts(Division);
            }

            catch (Exception ex)
            {
                throw;
            }

            return designation;
        }

        public List<POItemList> AllPOItem(int? Division)
        {
            List<POItemList> designation = new List<POItemList>();
            try
            {
                designation = _repository.AllPOItem(Division);
            }

            catch (Exception ex)
            {
                throw;
            }

            return designation;
        }

        public List<GRNItemList> AllGRNItem(int? GRNId)
        {
            List<GRNItemList> designation = new List<GRNItemList>();
            try
            {
                designation = _repository.AllGRNItem(GRNId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return designation;
        }

        public List<SubCatogory> GetSubCatogory(int? MC)
        {
            List<SubCatogory> SubCatogoryList = new List<SubCatogory>();
            try
            {
                SubCatogoryList = _repository.GetSubCatogory(MC);
            }

            catch (Exception ex)
            {
                throw;
            }

            return SubCatogoryList;
        }

    }
}