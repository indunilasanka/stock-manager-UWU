using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class PDService
    {

        private readonly PDRepository _repository;

        public PDService()
        {
            _repository = new PDRepository();
        }

        public POViewModel SearchPO(POViewModel model)
        {
            POViewModel Details = new POViewModel();
            try
            {
                Details = _repository.SearchPO(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public SerialViewModel SearchSerial(SerialViewModel model)
        {
            SerialViewModel Details = new SerialViewModel();
            try
            {
                Details = _repository.SearchSerial(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public PO GetPO(int? POId)
        {
            PO user = new PO();
            try
            {
                user = _repository.GetPO(POId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }
        public POItemList GetPOItem(int? PIId)
        {
            POItemList user = new POItemList();
            try
            {
                user = _repository.GetPOItem(PIId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public List<POItemList> GetPOItemList(int? POId)
        {
            List<POItemList> user = new List<POItemList>();
            try
            {
                user = _repository.GetPOItemList(POId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdatePO(PO user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdatePO(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int UpdatePOItem(POItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdatePOItem(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddPO(PO user)
        {
            user.POCreatedDate = DateTime.Now;
            int id = 0;
            try
            {
                id = _repository.AddPO(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddPOItem(POItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.AddPOItem(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeletePO(int? POId)
        {
            int id = 0;
            try
            {
                id = _repository.DeletePO(POId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AcceptPO(int? POId)
        {
            int id = 0;
            try
            {
                id = _repository.AcceptPO(POId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteProduct(int? PIId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteProduct(PIId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
    }
}