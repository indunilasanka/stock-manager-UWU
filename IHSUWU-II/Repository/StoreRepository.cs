using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Login.Repository
{
    public class StoreRepository : RepositoryBase<UserContext>
    {
        public StoreRepository()
        {
        }

        public GRNViewModel SearchGRN(GRNViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            GRNViewModel Details = new GRNViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.GRNList = db.Query<GRN>("exec usp_SearchGRN " + "@SId",
                            new { SId = model.SId }).ToList();
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
            return Details;
        }

        public GRN GetGRN(int? GRNId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            GRN user = new GRN();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<GRN>("exec usp_GetGRN " + "@GRNId",
                            new { GRNId = GRNId }).FirstOrDefault();
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

        public GRNItemList GetGRNItem(int? GIId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            GRNItemList user = new GRNItemList();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<GRNItemList>("exec usp_GetGRNItem " + "@GIId",
                            new { GIId = GIId }).FirstOrDefault();
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

        public int UpdateGRN(GRN user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditGRN " +
                             " @GRNId,@SId; select @@ret; ",
                      new
                      {
                          @GRNId = user.GRNId,
                          @SId = user.SId,

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

        public int UpdateGRNItem(GRNItemList user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditGRNItem " +
                             " @GIId,@POId,@GRNId,@PIId,@GIDescription,@GIReceivedQuantity,@GIUnitPrice; select @@ret; ",
                      new
                      {
                          @GIId = user.GIId,
                          @POId = user.POId,
                          @GRNId = user.GRNId,
                          @PIId = user.PIId,
                          @GIDescription = user.GIDescription,
                          @GIReceivedQuantity = user.GIReceivedQuantity,
                          @GIUnitPrice = user.GIUnitPrice

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

        public int AddGRN(GRN user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddGRN " + " @GRNCreatedDate,@SId; select @@ret;",
                        new
                        {
                            @GRNCreatedDate = user.GRNCreatedDate,
                            @SId = user.SId,

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
        public int AddGRNItem(GRNItemList user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddGRNItem " + " @POId,@GRNId,@PIId,@GIDescription,@GIReceivedQuantity,@GIUnitPrice; select @@ret;",
                        new
                        {
                            @POId = user.POId,
                            @GRNId = user.GRNId,
                            @PIId = user.PIId,
                            @GIDescription = user.GIDescription,
                            @GIReceivedQuantity = user.GIReceivedQuantity,
                            @GIUnitPrice = user.GIUnitPrice
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

        public int DeleteGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteGRN" + " @GRNId", new { GRNId = GRNId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int UpdateQR(int? GRNId, int? GIId, string qr)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_UpdateQR" + " @GRNId,@GIId,@qr", new { GRNId = GRNId, GIId = GIId, qr = qr }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public int DeleteGRNItem(int? GIId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteGRNItem" + " @GIId", new { GIId = GIId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public List<GRNItemList> GetGRNItemList(int? GRNId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<GRNItemList> user = new List<GRNItemList>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<GRNItemList>("exec usp_GetGRNItemList " + "@GRNId",
                            new { GRNId = GRNId }).ToList();
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

        public int AcceptGRN(int? GRNId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_AcceptGRN" + " @GRNId", new { GRNId = GRNId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int AcceptRN(int? RNId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_AcceptRN" + " @RNId", new { RNId = RNId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


        public IssueViewModel SearchIssue(IssueViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            IssueViewModel Details = new IssueViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.IssueList = db.Query<Issue>("exec usp_SearchIssue " + "@LocationId",
                            new { LocationId = model.LocationId }).ToList();
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
            return Details;
        }

        public Issue GetIssue(int? IId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            Issue user = new Issue();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<Issue>("exec usp_GetIssue " + "@IId",
                            new { IId = IId }).FirstOrDefault();
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

        public IssueItem GetIssueItem(int? IItemId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            IssueItem user = new IssueItem();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<IssueItem>("exec usp_GetIssueItem " + "@IItemId",
                            new { IItemId = IItemId }).FirstOrDefault();
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

        public int UpdateIssue(Issue user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditIssue " +
                             " @IId,@LocationId,@IStatus; select @@ret; ",
                      new
                      {
                          @IId = user.IId,
                          @LocationId = user.LocationId,
                          @IStatus = user.IStatus

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

        public int UpdateIssueItem(IssueItem user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditIssueItem " +
                             " @IItemId,@IId,@RequestId,@RequestItemId,@PROId,@IssuedQuantity,@ItemUnitPrice; select @@ret; ",
                      new
                      {
                          @IItemId = user.IItemId,
                          @IId = user.IId,
                          @RequestId = user.RequestId,
                          @RequestItemId = user.RequestItemId,
                          @PROId = user.PROId,
                          @IssuedQuantity = user.IssuedQuantity,
                          @ItemUnitPrice = user.ItemUnitPrice

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

        public int AddIssue(Issue user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddIssue " + " @LocationId,@ICreatedDate,@IStatus; select @@ret;",
                        new
                        {
                            @LocationId = user.LocationId,
                            @ICreatedDate = user.ICreatedDate,
                            @IStatus = user.IStatus

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

        public int AddIssueItem(IssueItem user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddIssueItem " + " @IId,@RequestId,@RequestItemId,@PROId,@IssuedQuantity,@ItemUnitPrice; select @@ret;",
                        new
                        {
                            @IId = user.IId,
                            @RequestId = user.RequestId,
                            @RequestItemId = user.RequestItemId,
                            @PROId = user.PROId,
                            @IssuedQuantity = user.IssuedQuantity,
                            @ItemUnitPrice = user.ItemUnitPrice
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

        public int DeleteIssue(int? IId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteIssue" + " @IId", new { IId = IId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int DeleteIssueItem(int? IItemId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteIssueItem" + " @IItemId", new { IItemId = IItemId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }
        public List<IssueItem> GetIssueItemList(int? IId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<IssueItem> user = new List<IssueItem>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<IssueItem>("exec usp_GetIssueItemList " + "@IId",
                            new { IId = IId }).ToList();
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



        public RNViewModel SearchRN(RNViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            RNViewModel Details = new RNViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.RNList = db.Query<RN>("exec usp_SearchRN " + "@SId",
                            new { SId = model.SId }).ToList();
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
            return Details;
        }

        public RN GetRN(int? RNId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            RN user = new RN();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<RN>("exec usp_GetRN " + "@RNId",
                            new { RNId = RNId }).FirstOrDefault();
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

        public RNItemList GetRNItem(int? RIId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            RNItemList user = new RNItemList();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<RNItemList>("exec usp_GetRNItem " + "@RIId",
                            new { RIId = RIId }).FirstOrDefault();
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

        public int UpdateRN(RN user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditRN " +
                             " @RNId,@SId,@RNStatus; select @@ret; ",
                      new
                      {
                          @RNId = user.RNId,
                          @SId = user.SId,
                          @RNStatus = user.RNStatus
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

        public int UpdateRNItem(RNItemList user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditRNItem " +
                             " @RIId,@GRNId,@RNId,@GIId,@RIQuantity,@RIDescription,@RIUnitPrice; select @@ret; ",
                      new
                      {
                          @RIId = user.RIId,
                          @GRNId = user.GRNId,
                          @RNId = user.RNId,
                          @GIId = user.GIId,
                          @RIQuantity = user.RIQuantity,
                          @RIDescription = user.RIDescription,
                          @RIUnitPrice = user.RIUnitPrice
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

        public int AddRN(RN user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddRN " + " @SId,@RNStatus,@RNCreatedDate; select @@ret;",
                        new
                        {
                            @SId = user.SId,
                            @RNStatus = user.RNStatus,
                            @RNCreatedDate = user.RNCreatedDate
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
        public int AddRNItem(RNItemList user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddRNItem " + " @GRNId,@RNId,@GIId,@RIQuantity,@RIDescription,@RIUnitPrice; select @@ret;",
                        new
                        {
                            @GRNId = user.GRNId,
                            @RNId = user.RNId,
                            @GIId = user.GIId,
                            @RIQuantity = user.RIQuantity,
                            @RIDescription = user.RIDescription,
                            @RIUnitPrice = user.RIUnitPrice
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

        public int DeleteRN(int? RNId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteRN" + " @RNId", new { RNId = RNId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int DeleteRNItem(int? RIId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeleteRNItem" + " @RIId", new { RIId = RIId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public List<RNItemList> GetRNItemList(int? RNId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<RNItemList> user = new List<RNItemList>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<RNItemList>("exec usp_GetRNItemList " + "@RNId",
                            new { RNId = RNId }).ToList();
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

    }
}