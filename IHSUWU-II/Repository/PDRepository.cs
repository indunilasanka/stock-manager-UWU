using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Login.Repository
{
    public class PDRepository : RepositoryBase<UserContext>
    {
        public PDRepository()
        {
        }

        public POViewModel SearchPO(POViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            POViewModel Details = new POViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.POList = db.Query<PO>("exec usp_SearchPO " + "@SId,@POTenderNo,@POFileNo",
                            new { SId = model.SId, POTenderNo = model.POTenderNo, POFileNo = model.POFileNo }).ToList();
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


        public SerialViewModel SearchSerial(SerialViewModel model)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            SerialViewModel Details = new SerialViewModel();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    Details.SerialList = db.Query<Serial>("exec usp_SearchSerial ",
                            new { }).ToList();
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


        public PO GetPO(int? POId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            PO user = new PO();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<PO>("exec usp_GetPO " + "@POId",
                            new { POId = POId }).FirstOrDefault();
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

        public POItemList GetPOItem(int? PIID)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            POItemList user = new POItemList();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<POItemList>("exec usp_GetPOItem " + "@PIID",
                            new { PIID = PIID }).FirstOrDefault();
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

        public int UpdatePO(PO user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditPO " +
                             " @POId,@SId,@POVat,@PODueDate,@POTenderNo,@POFileNo,@POStatus,@POQuatationDate; select @@ret; ",
                      new
                      {
                          @POId = user.POId,
                          @SId = user.SId,
                          @POVat = user.POVat,
                          @PODueDate = user.PODueDate,
                          @POTenderNo = user.POTenderNo,
                          @POFileNo = user.POFileNo,
                          @POStatus = user.POStatus,
                          @POQuatationDate = user.POQuatationDate

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

        public int UpdatePOItem(POItemList user)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            int id = 0;



            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec usp_EditPOItem " +
                             " @PIId,@POId,@PROId,@PIDescription,@PIQuantity,@PIUnitPrice; select @@ret; ",
                      new
                      {
                          @PIId = user.PIId,
                          @POId = user.POId,
                          @PROId = user.PROId,
                          @PIDescription = user.PIDescription,
                          @PIQuantity = user.PIQuantity,
                          @PIUnitPrice = user.PIUnitPrice

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

        public int AddPO(PO user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddPO " + " @POCreatedDate,@SId,@POVat,@PODueDate,@POTenderNo,@POFileNo,@POStatus,@POQuatationDate; select @@ret;",
                        new
                        {
                            POCreatedDate = user.POCreatedDate,
                            SId = user.SId,
                            POVat = user.POVat,
                            PODueDate = user.PODueDate,
                            POTenderNo = user.POTenderNo,
                            POFileNo = user.POFileNo,
                            POStatus = user.POStatus,
                            POQuatationDate = user.POQuatationDate

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
        public int AddPOItem(POItemList user)
        {
            int id = 0;
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;

            try
            {
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;
                id = db.ExecuteScalar<int>("declare @@ret INT; exec @@ret = usp_AddPOItem " + " @POId,@PROId,@PIDescription,@PIQuantity,@PIUnitPrice; select @@ret;",
                        new
                        {
                            @POId = user.POId,
                            @PROId = user.PROId,
                            @PIDescription = user.PIDescription,
                            @PIQuantity = user.PIQuantity,
                            @PIUnitPrice = user.PIUnitPrice
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

        public int DeletePO(int? POId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeletePO" + " @POId", new { POId = POId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public int DeleteProduct(int? PIId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_DeletePOItem" + " @PIId", new { PIId = PIId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public List<POItemList> GetPOItemList(int? POId)
        {
            PetaPoco.Database db = null;
            var dbConn = DataContext.Database.Connection;
            List<POItemList> user = new List<POItemList>();

            try
            {
                using (DataContext)
                {
                    dbConn.Open();
                    db = new PetaPoco.Database(dbConn);
                    db.EnableAutoSelect = false;
                    user = db.Query<POItemList>("exec usp_GetPOItemList " + "@POId",
                            new { POId = POId }).ToList();
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

        public int AcceptPO(int? POId)
        {
            int id = 0;
            try
            {
                PetaPoco.Database db = null;
                var dbConn = DataContext.Database.Connection;
                dbConn.Open();
                db = new PetaPoco.Database(dbConn);
                db.EnableAutoSelect = false;

                id = db.Query<int>("exec usp_AcceptPO" + " @POId", new { POId = POId }).SingleOrDefault();
                dbConn.Close();
            }
            catch (Exception ex)
            {

            }
            return id;
        }


    }
}