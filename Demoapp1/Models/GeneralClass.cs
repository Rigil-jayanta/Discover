using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Demoapp1.Models
{
    
        public class Budget_Custom_Report1
        {
            public long BudgetId { get; set; }
            public string SU { get; set; }
            public string ProgramName { get; set; }
            public string ZoomCode_Fixed { get; set; }
            public string ZoomCode_Critical { get; set; }

            public  Double? FY15_Requirement_Pay {get;set;}
            public  Double? FY15_Q1_RequirementNonPayORB_Fixed { get; set; }
            public  Double? FY15_Q1_Requirement_NonPayORB_Critical {get;set;}
            public  Double? FY15_Q1_Requirement_NonPayORB_Total {get;set;}
            public  Double? FY15_Q1_Requirement_TOTAL_Q1 {get;set;}
   
            public  Double? FY15_ROY_Allowances_Pay {get;set;}
            public  Double? FY15_ROY_Allowances_NonPayORB_Fixed {get;set;}
            public  Double? FY15_ROY_Allowances_NonPayORB_Critical {get;set;}
            public  Double? FY15_ROY_Allowances_NonPayORB_Total {get;set;}
            public  Double? FY15_ROY_Allowances_TOTAL_ROY {get;set;}
   
            public  Double? FY15_Allowances_Pay {get;set;}
            public  Double? FY15_Allowances_NonPayORB_Fixed {get;set;}
            public  Double? FY15_Allowances_NonPayORB_Critical {get;set;}
            public  Double? FY15_Allowances_NonPayORB_Total {get;set;}
            public  Double? FY15_Allowances_TOTAL_FY {get;set;}
   
            public  Double? FY16_Allowances_NonPayORB_Fixed {get;set;}
            public  Double? FY16_Allowances_NonPayORB_Critical {get;set;}
            public  Double? FY16_Allowances_NonPayORB_Total {get;set;}
            public  Double? FY16_Allowances_ORB_83M_Cut {get;set;}
   
            public  Double? Diff_btw_FY15_FY16_NonPayORB_Fixed {get;set;}
            public  Double? Diff_btw_FY15_FY16_NonPayORB_Critical {get;set;}
            public  Double? Diff_btw_FY15_FY16_NonPayORB_Total {get;set;}
   
            public  String Comments {get;set;}
            public  String ORB_Deliberation_Mission_Critical  {get;set;}
            public  Double? Percent_Change {get;set;}
            public  Double? Suggested_Adjusments_before_DL {get;set;}
            public  Double? Suggested_Adjusments_before_DL_keep_it_to_FY15 {get;set;}
   
            public  Double? FY17_Requests_NonPayORB_Fixed {get;set;}
            public  Double? FY17_Requests_NonPayORB_Critical {get;set;}
            public  Double? FY17_Requests_NonPayORB_Total {get;set;}
            public  Double? FY17_Requests_Diff {get;set;}
            public  Double? FY17_Requests_Diff_pc {get;set;}
   
            public  Double? FY16_DL_Quartiles_and_Scores_Value {get;set;}
            public  Double? FY16_DL_Quartiles_and_Scores_Quartile {get;set;}
            public  Double? FY16_DL_Quartiles_and_Scores_FY16_MC_Qtl_Allocation {get;set;}
            public  Double? FY16_MC_Qtl_Cut {get;set;}

        }

        [MetadataType(typeof(Budget))]
        public partial class Budget1
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            ICollection<BudgetItem> BudgetItems { get; set; }
        }

        [MetadataType(typeof(BudgetItem))]
        public partial class BudgetItem1
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Code { get; set; }
        }

}