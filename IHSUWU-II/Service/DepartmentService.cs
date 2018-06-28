using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class DepartmentService
    {

        private readonly DepartmentRepository _repository;

        public DepartmentService()
        {
            _repository = new DepartmentRepository();
        }

        public RequestViewModel SearchRequest(RequestViewModel model)
        {
            RequestViewModel Details = new RequestViewModel();
            try
            {
                Details = _repository.SearchRequest(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public Requests GetRequest(int? RequestId)
        {
            Requests user = new Requests();
            try
            {
                user = _repository.GetRequest(RequestId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }
        public RequestItem GetRequestItem(int? RequestItemId)
        {
            RequestItem user = new RequestItem();
            try
            {
                user = _repository.GetRequestItem(RequestItemId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public List<RequestItem> GetRequestItemList(int? RequestId)
        {
            List<RequestItem> user = new List<RequestItem>();
            try
            {
                user = _repository.GetRequestItemList(RequestId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdateRequest(Requests user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateRequest(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int UpdateRequestItem(RequestItem user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateRequestItem(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddRequest(Requests user)
        {
            int id = 0;
            try
            {
                id = _repository.AddRequest(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddRequestItem(RequestItem user)
        {
            int id = 0;
            try
            {
                id = _repository.AddRequestItem(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteRequest(int? RequestId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteRequest(RequestId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteRequestItem(int? RequestItemId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteRequestItem(RequestItemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
    }
}