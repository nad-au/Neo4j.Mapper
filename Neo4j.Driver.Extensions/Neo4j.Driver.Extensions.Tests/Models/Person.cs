using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neo4j.Driver.Extensions.Tests.Models
{
    public class Person
    {
        private int? age;
        private bool isDateOfBirthConfirmed;

        public Person()
        {
            LanguagesSpoken = new List<string>();
            EthnicCultureIdentities = new List<string>();
        }

        public long? UniqueId { get; set; }

        public string GivenName { get; set; }

        public string MiddleNames { get; set; }

        public string FamilyName { get; set; }

        public string PreferredName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateCertainty? DateOfBirthCertainty { get; set; }

        public string PlaceOfBirthTown { get; set; }

        public string PlaceOfBirthState { get; set; }

        public string PlaceOfBirthCountry { get; set; }

        public string BirthCertificateLocation { get; set; }

        public string TribalArea { get; set; }

        public string LanguageGroup { get; set; }

        public string LanguageComments { get; set; }

        public string ATSIIdentifyInformation { get; set; }

        public string CommunicationDetails { get; set; }

        public bool IntepreterRequired { get; set; }

        public string IntepreterComments { get; set; }

        public YesNoType? Religious { get; set; }

        public string ReligiousDietaryRestrictions { get; set; }

        public string Religion { get; set; }

        public string DisabilityComments { get; set; }

        public bool Deceased { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public bool DateOfDeathConfirmed { get; set; }

        public DateStyle? DateOfDeathStyle { get; set; }

        [JsonIgnore]
        public bool IsDateOfBirthConfirmed
        {
            get { return isDateOfBirthConfirmed; }
            set { isDateOfBirthConfirmed = value; }
        }

        public bool CalculatedIsDateOfBirthConfirmed
        {
            set { isDateOfBirthConfirmed = value; }
        }

        [JsonIgnore]
        public int? CurrentAge
        {
            get
            {
                return DateOfBirthCertainty == null && age == 0 ? null : age;
            }
            set
            {
                age = value;
            }
        }

        public int? CalculatedAge
        {
            set { age = value; }
        }

        [JsonIgnore]
        public IEnumerable<string> EthnicCultureIdentities { get; set; }

        public string EthnicityComments { get; set; }

        public string[] MigratedEthnicCultureIdentities { get; set; }

        [JsonIgnore]
        public IEnumerable<string> LanguagesSpoken { get; set; }

        public string[] MigratedLanguagesSpoken { get; set; }

        public string GetDateOfDeathInText()
        {
            if (!DateOfDeath.HasValue)
                return null;

            return DateOfDeathStyle == DateStyle.YearOnly
                ? DateOfDeath.Value.ToString("yyyy")
                : DateOfDeath.Value.ToString();
        }

        public string FullName()
        {
            return $"{GivenName} {FamilyName}".Trim();
        }
    }
}