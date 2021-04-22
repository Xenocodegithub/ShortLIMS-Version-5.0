using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LIMS_DEMO.Core.User;
using LIMS_DEMO.Core.Enquiry;
using LIMS_DEMO.Core.Inventory;
using LIMS_DEMO.Core.Configuration;
using LIMS_DEMO.Core.Lab;
using LIMS_DEMO.Core.Customer;
using LIMS_DEMO.Core.Collection;
using LIMS_DEMO.Core.Arrival;
using LIMS_DEMO.Core.ManageSampleCollection;
using LIMS_DEMO.Core.DropDowns;
using LIMS_DEMO.Core.FieldDetermination;
using LIMS_DEMO.Core.Repository;
using LIMS_DEMO.Core.WorkOrderCustomer;

namespace LIMS_DEMO.Core
{
    public static class CoreFactory
    {
        //UserLogin & LeftMenu
        public static User.UserEntity userEntity = null;
        public static AppSettings.MenuEntity menuEntity = null;
        public static List<AppSettings.MenuEntity> menuList = null;
        public static Enquiry.EnquiryEntity enquiryEntity = null;
        public static List<EnquiryEntity> enquiryList = null;
        public static EnquiryLogEntity enquiryLogEntity = null;
        public static List<EnquiryLogEntity> enquiryLogList = null;
        public static Enquiry.ContractEntity contractEntity = null;
        public static List<ContractEntity> contractList = null;
        public static EnquirySampleEntity enqurySampleEntity = null;
        public static List<EnquirySampleEntity> enquirySampleList = null;
        public static EnquiryParameterEntity enquiryParameterEntity = null;
        public static List<EnquiryParameterEntity> enquiryParameterList = null;
        public static CostingEntity costingEntity = null;
        public static List<CostingEntity> costingList = null;
        public static QuotationEntity quotationEntity = null;
        public static List<TermsAndConditionEntity> TermsList = null;
        public static List<QuotationTNCEntity> quotTermList = null;
        public static WorkOrderEntity workOrderEntity = null;
        public static List<SampleLocationEntity> sampleLocationList = null;
        public static Collection.SampleCollectionEntity samplecollectionEntity = null;
        public static List<SampleCollectionEntity> SamplecollectionList = null;
        public static List<QuantityPreservativeEntity> quantityPreservativeList = null;
        public static List<Core.Collection.SampleDeviceEntity> sampleDeviceList = null;
        public static List<Core.Collection.ProcedureEntity> procedureList = null;
        public static Collection.SurveyingTeamLeadEntity surveyingTeamEntity = null;
        public static List<SurveyingTeamLeadEntity> surveyingTeamList = null;
        public static Arrival.SampleArrivalEntity samplearrivalEntity = null;
        public static List<SampleArrivalEntity> samplearrivalList = null;
        public static List<FinalReportEntity> finalReportList = null;
        public static List<PlannerByDisciplineEntity> plannerbydisciplineList = null;
        public static ManageSampleCollection.ManageSampleEntity managesampleEntity = null;
        public static List<ManageSampleEntity> managesampleList = null;
        public static IList<DisposalEntity> DisposalList = null;
        public static WorkOrderCustomer.WorkOrderCustomerEntity WorkOrderCustomerEntity = null;
        public static List<WorkOrderCustomerEntity> WorkOrderCustomerList = null;
        public static List<WorkOrderSampleEntity> OrderSampleList = null;
        public static WorkOrderCustomer.WorkOrderSampleEntity OrderSampleEntity = null;
        public static WorkOderCostingEntity workcostingEntity = null;

        //FieldDetermination Entity
        public static AmbientAirMonitoringEntity ambientAirEntity = null;
        public static BuildingMaterialEntity buildingMaterialEntity = null;
        public static CoalCokeSolidFuelEntity coalCokeSolidFuelEntity = null;
        public static FoodAndAgriProductsEntity foodAndAgriProductsEntity = null;
        public static MicrobiologicalMonitoringOfAirEntity microbiologicalMonitoringEntity = null;
        public static NoiseLevelMonitoringEntity noiseLevelMonitoringEntity = null;
        public static SolidWasteSoilOilEntity solidWasteSoilOilEntity = null;
        public static StackEmissionMonitoringEntity stackEmissionMonitoringEntity = null;
        public static WasteWaterEntity wasteWaterEntity = null;
        public static List<WasteWaterEntity> wastewaterList = null;
        public static WorkplaceAndFugitiveEmissionEntity workplaceAndFugitiveEmissionEntity = null;

        //Inventory Entity
        public static InventoryTypeEntity inventoryTypeEntity = null;
        public static Core.Inventory.CategoryEntity catagoryEntity = null;
        public static Core.Inventory.EmployeeEntity employeeEntity = null;
        public static Core.Inventory.CapacityEntity capacityEntity = null;
        public static Core.Inventory.InventoryBasicDetailEntity inventoryBasicDetailEntity = null;
        public static Core.Inventory.BasicNumberEntity basicNumberEntity = null;
        public static Core.Inventory.InventoryCommercialDetailEntity inventoryCommercialDetailEntity = null;
        public static PurchaseEntity purchaseEntity = null;
        public static Core.Inventory.InventoryBasicItemDetailEntity inventoryBasicItemDetailEntity = null;
        public static Core.Inventory.InventoryMaintainanceAndCalibrationScheduleEntity inventoryMaintainanceAndCalibrationScheduleEntity = null;
        public static Core.Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity inventoryMaintainanceAndCalibrationScheduleDatesEntity = null;
        public static Core.Inventory.InventoryMaintainanceAndCalibrationEntity inventoryMaintainanceAndCalibrationEntity = null;
        public static Core.Inventory.InventoryMaintenanceEntity inventoryMaintenanceEntity = null;

        //Dashboard
        public static Core.Dashboard.DashboardEntity dashboardEntity = null;


        public static BreakDownEntity breakDownEntity = null;
        //List

        //List Inventory
        public static List<Inventory.InventoryItemEntity> InventoryItemList = null;
        public static List<Inventory.CategoryEntity> categoryList = null;
        public static List<Inventory.EmployeeEntity> EmployeeList = null;
        public static List<Inventory.CapacityEntity> CapacityList = null;
        public static List<Inventory.InventoryBasicDetailEntity> InventoryBasicDetailsList = null;
        public static List<Inventory.InventoryBasicItemDetailEntity> InventoryBasicItemDetailList = null;
        public static List<Inventory.InventoryCommercialDetailEntity> InventoryCommercialList = null;
        public static List<Inventory.InventoryCommercialFileDetailEntity> InventoryCommercialFileDetailList = null; 
        public static List<Inventory.InventoryMaintainanceAndCalibrationScheduleEntity> inventoryMaintainanceAndCalibrationScheduleList = null;
        public static List<Inventory.InventoryMaintainanceAndCalibrationScheduleDatesEntity> InventoryMaintainanceAndCalibrationScheduleDatesList = null;
        public static List<Inventory.InventoryMaintenanceEntity> InventoryMaintenanceList = null;
        public static List<InventoryMaintainanceAndCalibrationEntity> imclist = null;
        public static List<InventoryMaintainanceAndCalibrationScheduleDatesEntity> imcscheduledatelist = null;
      
        public static List<Configuration.DisciplineMasterEntity> disciplineList = null;

        //public static Inventory.InventoryItemMasterentity inventoryItemMasterentity = null;
        public static List<Core.Inventory.InventoryItemEntity> InvItemMentList = null;

        public static List<Configuration.UserMasterEntity> userMasterList = null;
        public static List<TestMethodMasterEntity> testmethodList = null;
        public static List<MenuMasterEntity> parentMenuList = null;
        public static List<Configuration.RoleMasterEntity> userRoleList = null;
        public static List<ParameterMasterEntity> parameterMasterList = null;
        public static List<Configuration.EmployeeMasterEntity> employeeMasterList = null;
        public static List<Configuration.UnitMasterEntity> unitMasterList = null;
        public static List<MenuMasterEntity> subMenuList = null;
        public static List<MenuMasterEntity> menuMappingist = null;
        public static List<ProductSubMatrixEntity> productGroup1List = null;
        public static List<ProductSubMatrixEntity> subGroup1List = null;
        public static List<ProductSubMatrixEntity> matrix1List = null;
        public static List<CustomerEntity> customerList = null;
        public static List<SPSMEntity> spsmList = null;

        public static List<Inventory.PurchaseEntity> purchaseRequestList = null;

        public static List<Dashboard.DashboardEntity> dashboardList = null;



        public static IList<BreakDownEntity> breakDownEntityList = null;
        public static IList<ItemSerialNumberEntity> ItemSerialNumber = null;
        //Interfaces: UserLogin & LeftMenu
        public static ILogin login = null;
        public static IMenu menu = null;
        public static IEnquiry enquiry = null;
        public static IContract contract = null;
        public static ISampleParameter sampleParameter = null;
        public static ICosting costing = null;
        public static IQuotation quotation = null;
        public static IWorkOrder workOrder = null;
        public static IWorkOrderCustomer workOrderCustomer = null;
        public static ISampleCollection samplecollection = null;
        public static ISurveyingTeamLead surveyingTeamLead = null;
        public static ISampleArrival samplearrival = null;
        public static IManageSample manageSample = null;
        public static IFieldDetermination fieldDetermination = null;
        public static IInventory inventory = null;
        public static IDropDown dropdowns = null;
        public static IPurchase Purchase = null; 
        public static IInventoryBreakDown inventoryBreakDown = null;

        public static IMenuMaster MenuMaster = null;
        public static ITestMethodMaster TestMethodMaster = null;
        public static IDiscipline discipline = null;
        public static IUserMaster userMaster = null;
        public static IRoleMaster roleMaster = null;
        public static IEmployeeMaster employeeMaster = null;
        public static IParameterMaster parameterMaster = null;
        public static IUnitMaster unitMaster = null;
        public static IFormulaBuilder formulaBuilder = null;
        public static IProductSubMatrix productSubMatrix = null;

        public static ICustomer customer = null;

        public static IPlan plan = null;
        public static ITester tester = null;
        public static IReviewer reviewer = null;
        public static IApprover approver = null;

        public static IDashboard dashboard = null;

        public static IInvoice invoice = null;

        //Configuration
        public static DisciplineMasterEntity disciplineMasterEntity = null;
        public static UserMasterEntity userMasterEntity = null;
        public static TestMethodMasterEntity testMethodMasterEntity = null;
        public static MenuMasterEntity menuMasterEntity = null;
        public static RoleMasterEntity roleMasterEntity = null;
        public static EmployeeMasterEntity employeeMasterEntity = null;
        public static ParameterMasterEntity parameterMasterEntity = null;
        public static UnitMasterEntity unitMasterEntity = null;
        public static ParameterMasterEntity parameterMasterEntity1 = null;

        //Customer
        public static CompanyTypeEntity companyTypeEntity = null;
        public static CustomerEntity customerEntity = null;
        public static CustomerGroupEntity customerGroupEntity = null;
        public static CustomerTypeEntity customerTypeEntity = null;

        public static MenuMasterEntity menuMasterEntity1 = null;
        public static ProductSubMatrixEntity productSubMatrixEntity = null;

        //Lab
        public static PlanEntity planEntity = null;
        public static ReviewerEntity reviewerEntity = null;
        public static ApproverEntity approverEntity = null;

        //Invoice
        public static Invoice.InvoiceEntity invoiceEntity = null;

    }
}