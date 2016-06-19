
//------------------------------------------------------------------------------------------------- 
// <copyright file="ControllerTest.cs" company="Allors bvba">
// Copyright 2002-2009 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Platform is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// <summary>Defines the ControllerTest type.</summary>
//-------------------------------------------------------------------------------------------------

namespace Allors
{
    using Allors.Domain;

    public class Population
    {
        private IDatabaseSession session;

        public MetricDefinition MetricDefinition66RollingPPM;
        public MetricDefinition MetricDefinition65AlluminumEfficiency;
        public MetricDefinition MetricDefinition206Ebitda;
        public MetricDefinition MetricDefinition260HRDaysToFill;

        public ReportingUnit Guarulhos;
        public ReportingUnit JohannesburgIT;
        public ReportingUnit GuarulhosPassCar;
        public ReportingUnit GuarulhosTruck;

        public MetricVariableDefinition Mvd165MachiningWheelsMonthly;
        public MetricVariableDefinition Mvd234WeightCastedWheels;
        public MetricVariableDefinition Mvd235ChipsProducedMachining;
        public MetricVariableDefinition Mvd236AluminumConsumedMelting;
        public MetricVariableDefinition MetricVariableDefinition322Ebitda;
        public MetricVariableDefinition MetricVariableDefinition391Ebitda2;
        public MetricVariableDefinition Mvd377WithBaseline384;

        public BusinessUnit Gwg;
        public BusinessUnit Corporate;

        public BusinessUnit IT;
        public BusinessUnit HR;

        public MetricBaselineDefinition BaselineDefinition384;

        public Person Person1;
        public Person Person2;

        public Population(IDatabaseSession session)
        {
            this.session = session;

            new Setup(session).Apply();

            this.Person1 = new PersonBuilder(this.session)
                .WithUserName("person1@inxin.com")
                .WithFirstName("person1 first name")
                .WithLastName("person1 last name")
                .WithUserEmail("person1@inxin.com")
                .WithUserEmailConfirmed(true)
                .WithUserPasswordHash("ADhrQUXHNLCelscP3NfZxpld7jRMOHkfxd8WRb9HtvgqS4vHKRzQCiIlZuj3qZ98kQ==")
                .Build();

            this.Person2 = new PersonBuilder(this.session)
                .WithUserName("Person2@inxin.com")
                .WithFirstName("person2 first name")
                .WithLastName("person2 last name")
                .WithUserEmail("mathijs.verwer@inxin.com")
                .WithUserEmailConfirmed(true)
                .WithUserPasswordHash("ADhrQUXHNLCelscP3NfZxpld7jRMOHkfxd8WRb9HtvgqS4vHKRzQCiIlZuj3qZ98kQ==")
                .Build();

            new UserGroups(this.session).Administrators.AddMember(this.Person1);

            this.session.Derive(true);
            this.session.Commit();
        }

        public void GivenDefaults()
        {
            this.GivenBusinessUnits();
            this.GivenBaselines();
            this.GivenMetricVariableDefintions();
            this.GivenReportingUnits();
            this.GivenMetricDefinition66RollingPPM();
            this.GivenMetricDefinition65AlluminumEfficiency();
            this.GivenMetricDefinition206Ebitda();
            this.GivenMetricDefinition260HRDaysToFill();
            this.session.Derive(true);
        }

        private void GivenMetricDefinition65AlluminumEfficiency()
        {
            MetricDefinition65AlluminumEfficiency =
                new MetricDefinitionBuilder(this.session).WithExternalPrimaryKey(65)
                    .WithName("Aluminum Efficiency")
                    .WithShortName("")
                    .WithDescription("Aluminum Efficiency")
                    .WithMetricPeriod(MetricPeriod.Monthly)
                    .WithMetricCategory(MetricCategory.LowestCost)
                    .WithCalculation("([234]/[235])-[236]")
                    .WithMetricType(MetricType.KeyProcessMetric)
                    .WithMetricValueBy(MetricValueBy.Additive)
                    .Build();
        }

        private void GivenMetricDefinition66RollingPPM()
        {
            MetricDefinition66RollingPPM =
                new MetricDefinitionBuilder(this.session).WithExternalPrimaryKey(66)
                    .WithName("3 month rolling PPM, Total")
                    .WithShortName("PPM")
                    .WithDescription("3 months ppm including Near Misses and defects found by T&WA")
                    .WithMetricPeriod(MetricPeriod.Monthly)
                    .WithMetricCategory(MetricCategory.LowestCost)
                    .WithCalculation("[165] + [377] - [384]")
                    .WithMetricType(MetricType.BreakThroughMetric)
                    .WithMetricValueBy(MetricValueBy.Additive)
                    .Build();
        }

        private void GivenMetricDefinition206Ebitda()
        {
            MetricDefinition206Ebitda =
                new MetricDefinitionBuilder(this.session).WithExternalPrimaryKey(206)
                    .WithName("FA EBITDA ABS MTD")
                    .WithShortName("PPM")
                    .WithDescription("Forcast Accuracy from Hyperion")
                    .WithMetricPeriod(MetricPeriod.Monthly)
                    .WithMetricCategory(MetricCategory.LowestCost)
                    .WithCalculation("[322]")
                    .WithMetricType(MetricType.BreakThroughMetric)
                    .WithMetricValueBy(MetricValueBy.Additive)
                    .Build();
        }

        private void GivenMetricDefinition260HRDaysToFill()
        {
            MetricDefinition260HRDaysToFill =
                new MetricDefinitionBuilder(this.session).WithExternalPrimaryKey(260)
                    .WithName("HR - Days to Fill")
                    .WithShortName("PPM")
                    .WithDescription("Average number of days required to complete the recruitment and selection process to fill a vacant position.  DTF calculated by eReq system and manually entered here.")
                    .WithMetricPeriod(MetricPeriod.Monthly)
                    .WithMetricCategory(MetricCategory.LowestCost)
                    .WithCalculation("[391]")
                    .WithMetricType(MetricType.BreakThroughMetric)
                    .WithMetricValueBy(MetricValueBy.Additive)
                    .Build();
        }

        private void GivenReportingUnits()
        {
            var brazil = new Countries(this.session).CountryByIsoCode["br"];
            var southAfrica = new Countries(this.session).CountryByIsoCode["sa"];

            var gwg = new BusinessUnits(this.session).GWG;
            var git = new BusinessUnits(this.session).GIT;

            this.Guarulhos =
                new ReportingUnitBuilder(this.session).WithExternalPrimaryKey(31)
                    .WithAbbreviation("GRU")
                    .WithName("Guarulhos")
                    .WithCountry(brazil)
                    .WithBusinessUnit(gwg)
                    .WithPerson(this.Person1)
                    .Build();
            this.JohannesburgIT =
                new ReportingUnitBuilder(this.session).WithExternalPrimaryKey(64)
                    .WithAbbreviation("JOH")
                    .WithName("Johannesburg -IT")
                    .WithCountry(southAfrica)
                    .WithBusinessUnit(git)
                    .WithPerson(this.Person1)
                    .Build();

            //Child reporting units.
            this.GuarulhosPassCar =
                new ReportingUnitBuilder(this.session).WithExternalPrimaryKey(54)
                    .WithAbbreviation("GRP")
                    .WithName("Guarulhos Pass Car")
                    .WithCountry(brazil)
                    .WithParent(this.Guarulhos)
                    .WithBusinessUnit(gwg)
                    .WithPerson(this.Person1)
                    .Build();
            this.GuarulhosTruck =
                new ReportingUnitBuilder(this.session).WithExternalPrimaryKey(55)
                    .WithAbbreviation("GRT")
                    .WithName("Guarulhos Truck")
                    .WithCountry(brazil)
                    .WithParent(this.Guarulhos)
                    .WithBusinessUnit(gwg)
                    .WithPerson(this.Person1)
                    .Build();
        }

        private void GivenMetricVariableDefintions()
        {
            var manual = new MetricVariableEntryTypes(this.session).Manual;
            var interfaced = new MetricVariableEntryTypes(this.session).Interfaced;
            var unknown = new MetricVariableEntryTypes(this.session).Unknown;

            Mvd165MachiningWheelsMonthly = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(165)
                .WithName("machining wheels monthly")
                .WithDescription("wheels machining monthly")
                .WithNumber(165)
                .WithMetricVariableEntryType(manual)
                .Build();
         
            Mvd234WeightCastedWheels = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(234)
                .WithName("Weight of Casted Wheels (kg)")
                .WithDescription("Weight of Casted Wheels (kg)")
                .WithNumber(234)
                .WithMetricVariableEntryType(manual)
                .Build();
            Mvd235ChipsProducedMachining = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(235)
                .WithName("Chips produced in machining Mvd235")
                .WithDescription("Chips produced in machining Mvd235 description.")
                .WithNumber(235)
                .WithMetricVariableEntryType(manual)
                .Build();

            //Interfaced variable.
            Mvd236AluminumConsumedMelting = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(236)
                .WithName("Aluminum consumed in 1st melting (kg)")
                .WithDescription("Aluminum consumed in 1st melting (kg)")
                .WithNumber(236)
                .WithMetricVariableEntryType(interfaced)
                .Build();
            
            MetricVariableDefinition322Ebitda = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(322)
                .WithName("FA EBITDA ABS  MTD v")
                .WithDescription("Forecast accuracy variable loaded from Hyperion")
                .WithNumber(322)
                .WithMetricVariableEntryType(interfaced)
                .Build();

            //Manual variable.
            MetricVariableDefinition391Ebitda2 = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(391)
                .WithName("FA EBITDA ABS  MTD v")
                .WithDescription("Forecast accuracy variable loaded from Hyperion")
                .WithNumber(391)
                .WithMetricVariableEntryType(manual)
                .Build();
            //With baseline
            Mvd377WithBaseline384 = new MetricVariableDefinitionBuilder(this.session).WithExternalPrimaryKey(235)
                .WithName("MetricVariableDefinition with a baseline")
                .WithDescription("MetricVariableDefinition with a baseline description.")
                .WithNumber(377)
                .WithMetricVariableEntryType(manual)
                .WithMetricBaselineDefinition(BaselineDefinition384)
                .Build();
        
        }

        private void GivenBaselines()
        {
            BaselineDefinition384 = new MetricBaselineDefinitionBuilder(this.session)
                .WithExternalPrimaryKey(384)
                .WithNumber(384)
                .Build();
            this.session.Derive(true);
        }

        private void GivenBusinessUnits()
        {
            Gwg = new BusinessUnitBuilder(this.session).WithExternalPrimaryKey(12)
                .WithAbbreviation("GWG" + "")
                .WithName("Global")
                .Build();
            Corporate = new BusinessUnitBuilder(this.session).WithExternalPrimaryKey(9)
                .WithAbbreviation("CORP")
                .WithName("Corporate")
                .Build();
            IT = new BusinessUnitBuilder(this.session).WithExternalPrimaryKey(18)
                .WithAbbreviation("GIT" + "")
                .WithName("Global IT")
                .Build();
            HR = new BusinessUnitBuilder(this.session).WithExternalPrimaryKey(19)
                .WithAbbreviation("GHR")
                .WithName("Global HR")
                .Build();
        }
    }
}