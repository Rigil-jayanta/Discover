using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demoapp1.Models;
using System.Data.Linq;

namespace Demoapp1.Controllers
{
    public class testController : Controller
    {
        private TempEntities db = new TempEntities();

        private iViewsDBDataContext dbiViews = new iViewsDBDataContext();

        //
        // GET: /test/

        public ActionResult Index()
        {
            return View(db.AttributesMetaDatas.ToList());
        }

        public ActionResult Test1()
        {
            var tmp = dbiViews.Budgets.Where(m=>m.Id==2585).ToList();
            return null;
        }

        public ActionResult Test()
        {
            //IOrderedQueryable<vw_Budget_Report> data = dbiViews.vw_Budget_Reports.Where(i => i.Year == "2015").OrderBy(m => m.BudgetId).ThenBy(m => m.BFTId).ThenBy(m => m.ExpenseTypeId).ThenBy(m => m.MetaDataId);
            IOrderedQueryable<vw_Budget_Report> data = dbiViews.vw_Budget_Reports.OrderBy(m=>m.BIRID).ThenBy(m => m.BudgetId).ThenBy(m=>m.Year).ThenBy(m => m.BFTId).ThenBy(m => m.ExpenseTypeId).ThenBy(m => m.MetaDataId);

            List<Budget_Custom_Report1> rptlines = new List<Budget_Custom_Report1>();

            //int budgetItemsCount_FY15 = dbiViews.vw_Budget_Reports.Where(d=>d.Year=="2015").Select(m => m.BudgetId).Distinct().Count();
            int budgetItemsCount_FY15 = dbiViews.vw_Budget_Reports.Select(m => m.BIRID).Distinct().Count();

            int budgetItemsCount_FY15_select = data.Count();

            long birId;
            double factor;

            int j = 0;

           
            List<vw_Budget_Report> ll = data.ToList();


            for (int i = 0; i < budgetItemsCount_FY15;i++ )
            {
                birId = ll[j].BIRID;
                Budget_Custom_Report1 rptline = new Budget_Custom_Report1();
                rptline.FY15_Requirement_Pay = 0;
                rptline.FY15_Allowances_Pay = 0;
                rptline.FY15_ROY_Allowances_Pay = 0;
                rptline.FY16_Allowances_NonPayORB_Total = 0;

                while (true)
                {
                    if (birId ==ll[j].BIRID)
                    {
                        rptline.BudgetId = ll[j].BudgetId;
                        rptline.SU = ll[j].SU;
                        rptline.ProgramName = ll[j].ProgramName;

                        rptline.ZoomCode_Fixed = ll[j].ExpenseTypeId == 1 && ll[j].MetaDataId == 7 ? ll[j].OtherDataValue : rptline.ZoomCode_Fixed == " " ? "" : rptline.ZoomCode_Fixed;
                        rptline.ZoomCode_Critical = ll[j].ExpenseTypeId == 2 && ll[j].MetaDataId == 7 ? ll[j].OtherDataValue : rptline.ZoomCode_Critical==" "? "":rptline.ZoomCode_Critical;

                        if (ll[j].Year=="2015")
                        {
                            rptline.FY15_Requirement_Pay = ll[j].BFTId == 2 && ll[j].ExpenseTypeId == 3 ? ll[j].Amount : rptline.FY15_Requirement_Pay == 0 ? 0 : rptline.FY15_Requirement_Pay;
                            rptline.FY15_Q1_RequirementNonPayORB_Fixed = ll[j].BFTId == 2 && ll[j].ExpenseTypeId == 1 ? ll[j].Amount : rptline.FY15_Q1_RequirementNonPayORB_Fixed == 0 ? 0 : rptline.FY15_Q1_RequirementNonPayORB_Fixed;
                            rptline.FY15_Q1_Requirement_NonPayORB_Critical = ll[j].BFTId == 2 && ll[j].ExpenseTypeId == 2 ? ll[j].Amount : rptline.FY15_Q1_Requirement_NonPayORB_Critical == 0 ? 0 : rptline.FY15_Q1_Requirement_NonPayORB_Critical;
                            rptline.FY15_Q1_Requirement_NonPayORB_Total = rptline.FY15_Q1_RequirementNonPayORB_Fixed + rptline.FY15_Q1_Requirement_NonPayORB_Critical;
                            rptline.FY15_Q1_Requirement_TOTAL_Q1 = rptline.FY15_Requirement_Pay + rptline.FY15_Q1_Requirement_NonPayORB_Total;
                           
                            rptline.FY15_Allowances_Pay = ll[j].BFTId == 9 && ll[j].ExpenseTypeId == 3 ? ll[j].Amount : rptline.FY15_Allowances_Pay == 0 ? 0 : rptline.FY15_Allowances_Pay;
                            rptline.FY15_Allowances_NonPayORB_Fixed = ll[j].BFTId == 9 && ll[j].ExpenseTypeId == 1 ? ll[j].Amount : rptline.FY15_Allowances_NonPayORB_Fixed == 0 ? 0 : rptline.FY15_Allowances_NonPayORB_Fixed;
                            rptline.FY15_Allowances_NonPayORB_Critical = ll[j].BFTId == 9 && ll[j].ExpenseTypeId == 2 ? ll[j].Amount : rptline.FY15_Allowances_NonPayORB_Critical == 0 ? 0 : rptline.FY15_Allowances_NonPayORB_Critical;
                            rptline.FY15_Allowances_NonPayORB_Total = rptline.FY15_Allowances_NonPayORB_Fixed + rptline.FY15_Allowances_NonPayORB_Critical;
                            rptline.FY15_Allowances_TOTAL_FY = rptline.FY15_Allowances_Pay + rptline.FY15_Allowances_NonPayORB_Total;
                            
                            rptline.FY15_ROY_Allowances_Pay = ll[j].BFTId == 10 && ll[j].ExpenseTypeId == 3 ? ll[j].Amount : rptline.FY15_ROY_Allowances_Pay == 0 ? 0 : rptline.FY15_ROY_Allowances_Pay;
                            rptline.FY15_ROY_Allowances_NonPayORB_Fixed = ll[j].BFTId == 10 && ll[j].ExpenseTypeId == 1 ? ll[j].Amount : rptline.FY15_ROY_Allowances_NonPayORB_Fixed == 0 ? 0 : rptline.FY15_ROY_Allowances_NonPayORB_Fixed;
                            rptline.FY15_ROY_Allowances_NonPayORB_Critical = ll[j].BFTId == 10 && ll[j].ExpenseTypeId == 2 ? ll[j].Amount : rptline.FY15_ROY_Allowances_NonPayORB_Critical == 0 ? 0 : rptline.FY15_ROY_Allowances_NonPayORB_Critical;
                            rptline.FY15_ROY_Allowances_NonPayORB_Total = rptline.FY15_ROY_Allowances_NonPayORB_Fixed + rptline.FY15_ROY_Allowances_NonPayORB_Critical;
                            rptline.FY15_ROY_Allowances_TOTAL_ROY = rptline.FY15_ROY_Allowances_Pay + rptline.FY15_ROY_Allowances_NonPayORB_Total;
                        }

                        if (ll[j].Year == "2016")
                        {
                            rptline.FY16_Allowances_NonPayORB_Fixed = ll[j].BFTId == 9 && ll[j].ExpenseTypeId == 1 ? ll[j].Amount : rptline.FY16_Allowances_NonPayORB_Fixed == 0 ? 0 : rptline.FY16_Allowances_NonPayORB_Fixed;
                            rptline.FY16_Allowances_NonPayORB_Critical = ll[j].BFTId == 9 && ll[j].ExpenseTypeId == 2 ? ll[j].Amount : rptline.FY16_Allowances_NonPayORB_Critical == 0 ? 0 : rptline.FY16_Allowances_NonPayORB_Critical;
                            rptline.FY16_Allowances_NonPayORB_Total = rptline.FY16_Allowances_NonPayORB_Fixed + rptline.FY16_Allowances_NonPayORB_Critical;
                            rptline.FY16_Allowances_ORB_83M_Cut = 0;
                        }

                        if (ll[j].Year == "2017")
                        {
                            rptline.FY17_Requests_NonPayORB_Fixed = 0;
                            rptline.FY17_Requests_NonPayORB_Critical = 0;
                            rptline.FY17_Requests_NonPayORB_Total = ll[j].ExpenseTypeId == 4 ? ll[j].Amount : rptline.FY17_Requests_NonPayORB_Total == 0 ? 0 : rptline.FY17_Requests_NonPayORB_Total;
                            rptline.FY17_Requests_Diff = rptline.FY17_Requests_NonPayORB_Total - rptline.FY16_Allowances_NonPayORB_Total;
                            rptline.FY17_Requests_Diff_pc = (rptline.FY17_Requests_Diff / rptline.FY16_Allowances_NonPayORB_Total) * 100;
                        }

                        rptline.Diff_btw_FY15_FY16_NonPayORB_Fixed = rptline.Diff_btw_FY15_FY16_NonPayORB_Fixed == 0 ? (rptline.FY16_Allowances_NonPayORB_Fixed - rptline.FY15_Allowances_NonPayORB_Fixed) : rptline.Diff_btw_FY15_FY16_NonPayORB_Fixed;
                        rptline.Diff_btw_FY15_FY16_NonPayORB_Critical = rptline.Diff_btw_FY15_FY16_NonPayORB_Critical == 0 ? (rptline.FY16_Allowances_NonPayORB_Critical - rptline.FY15_Allowances_NonPayORB_Critical) : rptline.Diff_btw_FY15_FY16_NonPayORB_Critical;
                        rptline.Diff_btw_FY15_FY16_NonPayORB_Total = rptline.Diff_btw_FY15_FY16_NonPayORB_Total == 0 ? (rptline.FY16_Allowances_NonPayORB_Total - rptline.FY15_Allowances_NonPayORB_Total) : rptline.Diff_btw_FY15_FY16_NonPayORB_Total;

                        rptline.Percent_Change = 0;
                        rptline.Percent_Change = rptline.Percent_Change == 0 ? (rptline.FY16_Allowances_NonPayORB_Total / rptline.FY15_Allowances_NonPayORB_Total) - 1 : rptline.Percent_Change;
                        rptline.Percent_Change = rptline.Percent_Change * 100;

                        rptline.Comments = ll[j].MetaDataId == 1 ? ll[j].OtherDataValue : rptline.Comments == " " ? "" : rptline.Comments;
                        rptline.ORB_Deliberation_Mission_Critical = ll[j].MetaDataId == 2 ? ll[j].OtherDataValue : rptline.ORB_Deliberation_Mission_Critical == " " ? "" : rptline.ORB_Deliberation_Mission_Critical;
                        rptline.Suggested_Adjusments_before_DL = ll[j].MetaDataId == 3 ? Convert.ToDouble(ll[j].OtherDataValue) : rptline.Suggested_Adjusments_before_DL == 0 ? 0 : rptline.Suggested_Adjusments_before_DL;
                        rptline.Suggested_Adjusments_before_DL_keep_it_to_FY15 = ll[j].MetaDataId == 4 ? Convert.ToDouble(ll[j].OtherDataValue) : rptline.Suggested_Adjusments_before_DL_keep_it_to_FY15 == 0 ? 0 : rptline.Suggested_Adjusments_before_DL_keep_it_to_FY15;
                        rptline.FY16_DL_Quartiles_and_Scores_Value = ll[j].MetaDataId == 5 ? Convert.ToDouble(ll[j].OtherDataValue) : rptline.FY16_DL_Quartiles_and_Scores_Value == 0 ? 0 : rptline.FY16_DL_Quartiles_and_Scores_Value;
                        rptline.FY16_DL_Quartiles_and_Scores_Quartile = ll[j].MetaDataId == 6 ? Convert.ToDouble(ll[j].OtherDataValue) : rptline.FY16_DL_Quartiles_and_Scores_Quartile == 0 ? 0 : rptline.FY16_DL_Quartiles_and_Scores_Quartile;

                        factor = 0;
                        if (rptline.FY16_DL_Quartiles_and_Scores_Quartile == 1) { factor = 0.95; }
                        if (rptline.FY16_DL_Quartiles_and_Scores_Quartile == 2) { factor = 0.90; }
                        if (rptline.FY16_DL_Quartiles_and_Scores_Quartile == 3) { factor = 0.85; }
                        if (rptline.FY16_DL_Quartiles_and_Scores_Quartile == 4) { factor = 0.80; }

                        rptline.FY16_DL_Quartiles_and_Scores_FY16_MC_Qtl_Allocation = rptline.FY16_Allowances_NonPayORB_Critical * factor;
                        rptline.FY16_MC_Qtl_Cut = rptline.FY16_Allowances_NonPayORB_Critical - rptline.FY16_DL_Quartiles_and_Scores_FY16_MC_Qtl_Allocation;
                    
                    }else
                    {
                        break;
                    }

                    j = j+1;
                    if (j==budgetItemsCount_FY15_select)
                    {
                        break;
                    }

                }
                rptlines.Add(rptline);

            }

            return View(rptlines);
        }

        //
        // GET: /test/Details/5

        public ActionResult Details(int id = 0)
        {
            AttributesMetaData attributesmetadata = db.AttributesMetaDatas.Find(id);
            if (attributesmetadata == null)
            {
                return HttpNotFound();
            }
            return View(attributesmetadata);
        }

        //
        // GET: /test/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /test/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttributesMetaData attributesmetadata)
        {
            if (ModelState.IsValid)
            {
                db.AttributesMetaDatas.Add(attributesmetadata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attributesmetadata);
        }

        //
        // GET: /test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AttributesMetaData attributesmetadata = db.AttributesMetaDatas.Find(id);
            if (attributesmetadata == null)
            {
                return HttpNotFound();
            }
            return View(attributesmetadata);
        }

        //
        // POST: /test/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AttributesMetaData attributesmetadata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributesmetadata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attributesmetadata);
        }

        //
        // GET: /test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AttributesMetaData attributesmetadata = db.AttributesMetaDatas.Find(id);
            if (attributesmetadata == null)
            {
                return HttpNotFound();
            }
            return View(attributesmetadata);
        }

        //
        // POST: /test/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributesMetaData attributesmetadata = db.AttributesMetaDatas.Find(id);
            db.AttributesMetaDatas.Remove(attributesmetadata);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}