using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core;
using LIMS_DEMO.DAL;
using LIMS_DEMO.BAL;
using LIMS_DEMO.BAL.Configuration;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core.Repository;

namespace LIMS_DEMO.BAL
{
    public static class BALFactory
    {
        //Login & LeftMenu
        public static Login.LoginBAL loginBAL = null;
        public static AppSettings.MenuBAL menuBAL = null;
        public static Enquiry.EnquiryBAL enquiryBAL = null;
        public static Enquiry.ContractBAL contractBAL = null;
        public static Enquiry.SampleParameterBAL sampleParameterBAL = null;
        public static Enquiry.CostingBAL costingBAL = null;
        public static Enquiry.QuotationBAL quotationBAL = null;
        public static Enquiry.WorkOrderBAL workOrderBAL = null;
        public static WorkOrderCustomer.WorkOrderCustomerBAL workordercustomerBAL = null;
        public static Collection.SampleCollectionBAL samplecollectionBAL = null;
        public static Collection.SurveyingTeamLeadBAL surveyingTeamLeadBAL = null;
        public static Arrival.SampleArrivalBAL samplearrivalBAL = null;
        public static ManageSampleCollection.ManageSampleCollectionBAL manageSampleBAL = null;

        //----------------------------Field Determination---------------------------------------------//
        public static FieldDetermination.AmbientAirMonitoringBAL ambientAirMonitoringBAL = null;
        public static FieldDetermination.BuildingMaterialBAL buildingMaterialBAL = null;
        public static FieldDetermination.CoalCokeSolidFuelsBAL coalCokeSolidFuelsBAL = null;
        public static FieldDetermination.FoodAndAgriProductsBAL foodAndAgriProductsBAL = null;
        public static FieldDetermination.MicrobiologicalMonitoringOfAirBAL microbiologicalMonitoringOfAirBAL = null;
        public static FieldDetermination.NoiseLevelMonitoringBAL noiseLevelMonitoringBAL = null;
        public static FieldDetermination.SolidWasteSoilOilBAL solidWasteSoilOilBAL = null;
        public static FieldDetermination.StackEmissionMonitoringBAL stackEmissionMonitoringBAL = null;
        public static FieldDetermination.WasteWaterBAL wasteWaterBAL = null;
        public static FieldDetermination.WorkplaceAndFugitiveEmissionBAL workplaceAndFugitiveEmissionBAL = null;

        //inventory
        public static Inventory.NewInventoryBAL NewInventoryBAL = null;
        public static Inventory.PurchaseBAL PurchaseBAL = null;
        public static Inventory.InventoryBreakDownBAL InventoryBreakDownBAL = null;
        //dropdown
        public static DropDown.DropDownBal dropdownsBAL = null;

        //Configuration
        public static Configuration.UserMasterBAL userMasterBAL = null;
        public static Configuration.RoleMasterBAL roleMasterBAL = null;
        public static Configuration.EmployeeMasterBAL employeeMasterBAL = null;
        public static Configuration.DisciplineMasterBAL disciplineMasterBAL = null;
        public static Configuration.TestMethodMasterBAL testMethodMasterBAL = null;
        public static Configuration.MenuMasterBAL menuMasterBAL = null;
        public static Configuration.ParameterMasterBAL parameterMasterBAL = null;
        public static Configuration.UnitMasterBAL unitMasterBAL = null;
        public static Configuration.FormulaBuilderBAL formulaBuilderBAL = null;
        public static Configuration.ProductSubMatrixBAL productSubMatrixBAL = null;
      
        //Customer
        public static Customer.CustomerBAL customerBAL = null;

        //Lab
        public static Lab.PlanBAL planBAL = null;
        public static Lab.TesterBAL testerBAL = null;
        public static Lab.ReviewerBAL reviewerBAL = null;
        public static Lab.ApproverBAL approverBAL = null;

        //Dashboard
        public static Dashboard.DashboardBAL dashboardBAL = null;

        //Invoice
        public static Invoice.InvoiceBAL invoiceBAL = null;
    }
}