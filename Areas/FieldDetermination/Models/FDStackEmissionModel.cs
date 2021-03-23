using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LIMS_DEMO.Areas.FieldDetermination.Models
{
    public class FDStackEmissionModel
    {
        public long FDStack_ParameterDataId { get; set; }

        [Display(Name = "Test Results")]
        public string TestResults { get; set; }
        public Nullable<bool> IsNABLAccredited { get; set; }
        public Nullable<bool> InField { get; set; }
        [Display(Name = "Parameter")]
        public string ParameterName { get; set; }
        public int ParameterMasterId { get; set; }

        [Display(Name = "TestMethod")]
        public string TestMethodName { get; set; }
        public int TestMethodId { get; set; }
        public string CurrentStatus { get; set; }// StatusMaster tbls
        public long FDStackEmission_GaseousDataId { get; set; }
        public long FDStackEmission_IsoKineticId { get; set; }
        public long FDStackEmissionId { get; set; }
        public Nullable<long> EnquiryId { get; set; }
        public int SampleCollectionId { get; set; }
        public int EnquirySampleID { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string LoadAtMonitoring { get; set; }
        public int SrNo { get; set; }
        public int Vf { get; set; }
        public int Va { get; set; }
        public int Pf { get; set; }
        public int Pi { get; set; }
        public int Pstd { get; set; }
        public int Tf { get; set; }
        public int Ti { get; set; }
        public int Tstd { get; set; }
        public string InstrumentId { get; set; }
        public string Parameters { get; set; }
        public string StackAttachedTo { get; set; }
        public Nullable<int> StackArea { get; set; }
        public string StackIdentity { get; set; }
        public string StackHeight { get; set; }
        public Nullable<int> StackDiameter { get; set; }
        public Nullable<int> HeightOfPorthole { get; set; }
        public Nullable<bool> Is8DAnd2D { get; set; }
        public Nullable<bool> Samplingportandplatformexists { get; set; }
        public Nullable<bool> Airpollutioncontrolequipmentexists { get; set; }
        public string MaterialOfConstruction { get; set; }
        public string StackShapeTop { get; set; }
        public string Fuelused_Type { get; set; }
        public string Fuelused_Consumption { get; set; }
        public Nullable<decimal> Bwo { get; set; }
        public Nullable<decimal> StackTempC { get; set; }
        public Nullable<decimal> AmbientTempC { get; set; }
        public Nullable<decimal> C { get; set; }

        public Nullable<decimal> BarometricPressure { get; set; }
        public Nullable<decimal> FlueGasComposition_CO2 { get; set; }
        public Nullable<decimal> FlueGasComposition_O2 { get; set; }
        public Nullable<decimal> FlueGasComposition_CO { get; set; }
        public Nullable<decimal> FlueGasComposition_N2 { get; set; }
        public Nullable<decimal> MoistureContent { get; set; }
        public Nullable<decimal> DurationOfBoiler { get; set; }
        public Nullable<decimal> AmbientTemperature { get; set; }
        public Nullable<decimal> StackTemperature { get; set; }
        public Nullable<decimal> DryGasMeterReading_Initial1 { get; set; }
        public Nullable<decimal> DryGasMeterReading_Final1 { get; set; }
        public Nullable<decimal> DryGasMeterReading_Total1 { get; set; }
        public Nullable<decimal> VelocityofStackGas { get; set; }
        public Nullable<decimal> PitotTubefactor { get; set; }
        public Nullable<decimal> StaticPressure { get; set; }
        public Nullable<decimal> AbsoluteStackPressure { get; set; }
        public Nullable<decimal> FlueGasQuantity { get; set; }
        public Nullable<decimal> NozzleConstantArea { get; set; }
        public Nullable<double> ThimbleNO { get; set; }
        public Nullable<decimal> TotalGasPassed { get; set; }
        public Nullable<decimal> SamplingGasTemp_Initial { get; set; }
        public Nullable<decimal> SamplingGasTemp_Final { get; set; }
        public Nullable<decimal> SamplingGasTemp_Average { get; set; }
        public Nullable<decimal> Vsc { get; set; }
        public String Vs { get; set; }
        public Nullable<decimal> TraversePointdistance { get; set; }
        public Nullable<decimal> DifferentialPressure { get; set; }
        public Nullable<int> Velocity_V { get; set; }
        public long ParameterId { get; set; }
        public Nullable<decimal> FlowRate { get; set; }
        public string SamplingTime { get; set; }
        public Nullable<decimal> GasTemp { get; set; }
        public Nullable<decimal> SamplingFlowRate { get; set; }
        public Nullable<decimal> SamplingDuration { get; set; }
        public Nullable<decimal> VacuumPv_Initial { get; set; }
        public Nullable<decimal> VacuumPv_Final { get; set; }
        public Nullable<decimal> VacuumPv_Average { get; set; }
        public Nullable<decimal> DryGasMeterReading_Initial { get; set; }
        public Nullable<decimal> DryGasMeterReading_Final { get; set; }
        public Nullable<decimal> DryGasMeterReading_Total { get; set; }
        public Nullable<int> BottleNo { get; set; }
        public string AbsorbingSolutionUsed_solution { get; set; }
        public Nullable<decimal> AbsorbingSolutionUsed_Conc { get; set; }
        public Nullable<decimal> AbsorbingSolutionUsed_Vol { get; set; }
        public string PreservationDone { get; set; }
        public bool IsActive { get; set; }
        public int EnteredBy { get; set; }
        public System.DateTime EnteredDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public List<FDStackEmissionModel> GridModel { get; set; }
        public List<FDStackEmissionModel> GridParaModel { get; set; }
    }


}