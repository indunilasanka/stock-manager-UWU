using Login.Models;
using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Service
{
    public class StoreService
    {

        private readonly StoreRepository _repository;

        public StoreService()
        {
            _repository = new StoreRepository();
        }

        public GRNViewModel SearchGRN(GRNViewModel model)
        {
            GRNViewModel Details = new GRNViewModel();
            try
            {
                Details = _repository.SearchGRN(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public GRN GetGRN(int? GRNId)
        {
            GRN user = new GRN();
            try
            {
                user = _repository.GetGRN(GRNId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }
        public GRNItemList GetGRNItem(int? GIId)
        {
            GRNItemList user = new GRNItemList();
            try
            {
                user = _repository.GetGRNItem(GIId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public List<GRNItemList> GetGRNItemList(int? GRNId)
        {
            List<GRNItemList> user = new List<GRNItemList>();
            try
            {
                user = _repository.GetGRNItemList(GRNId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdateGRN(GRN user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateGRN(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int UpdateGRNItem(GRNItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateGRNItem(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddGRN(GRN user)
        {
            user.GRNCreatedDate = DateTime.Now;
            int id = 0;
            try
            {
                id = _repository.AddGRN(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddGRNItem(GRNItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.AddGRNItem(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteGRN(GRNId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AcceptGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                id = _repository.AcceptGRN(GRNId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AcceptRN(int? RNId)
        {
            int id = 0;
            try
            {
                id = _repository.AcceptRN(RNId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteGRNItem(int? GIId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteGRNItem(GIId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }




        public IssueViewModel SearchIssue(IssueViewModel model)
        {
            IssueViewModel Details = new IssueViewModel();
            try
            {
                Details = _repository.SearchIssue(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public Issue GetIssue(int? IId)
        {
            Issue user = new Issue();
            try
            {
                user = _repository.GetIssue(IId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }
        public IssueItem GetIssueItem(int? IItemId)
        {
            IssueItem user = new IssueItem();
            try
            {
                user = _repository.GetIssueItem(IItemId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public List<IssueItem> GetIssueItemList(int? IId)
        {
            List<IssueItem> user = new List<IssueItem>();
            try
            {
                user = _repository.GetIssueItemList(IId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdateIssue(Issue user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateIssue(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int UpdateIssueItem(IssueItem user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateIssueItem(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddIssue(Issue user)
        {
            user.ICreatedDate = DateTime.Now;
            int id = 0;
            try
            {
                id = _repository.AddIssue(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddIssueItem(IssueItem user)
        {
            int id = 0;
            try
            {
                id = _repository.AddIssueItem(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteIssue(int? IId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteIssue(IId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteIssueItem(int? IItemId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteIssueItem(IItemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public void UpdateQR(int? GRNId, int? GIId, string qr)
        {
            _repository.UpdateQR(GRNId, GIId, qr);
        }


        public RNViewModel SearchRN(RNViewModel model)
        {
            RNViewModel Details = new RNViewModel();
            try
            {
                Details = _repository.SearchRN(model);
            }

            catch (Exception ex)
            {
                throw;
            }

            return Details;
        }

        public RN GetRN(int? RNId)
        {
            RN user = new RN();
            try
            {
                user = _repository.GetRN(RNId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }
        public RNItemList GetRNItem(int? RIId)
        {
            RNItemList user = new RNItemList();
            try
            {
                user = _repository.GetRNItem(RIId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public List<RNItemList> GetRNItemList(int? RNId)
        {
            List<RNItemList> user = new List<RNItemList>();
            try
            {
                user = _repository.GetRNItemList(RNId);
            }

            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public int UpdateRN(RN user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateRN(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int UpdateRNItem(RNItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.UpdateRNItem(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddRN(RN user)
        {
            user.RNCreatedDate = DateTime.Now;
            int id = 0;
            try
            {
                id = _repository.AddRN(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int AddRNItem(RNItemList user)
        {
            int id = 0;
            try
            {
                id = _repository.AddRNItem(user);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteRN(int? RNId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteRN(RNId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }

        public int DeleteRNItem(int? RIId)
        {
            int id = 0;
            try
            {
                id = _repository.DeleteRNItem(RIId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
    }
}