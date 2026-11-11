// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomRecordE.cs" company="Company Name">
//   Company Name
// </copyright>
// <summary>
//   Class intelliSense of record
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Library.Auxiliary.Constants.Entities.Custom
{
    public partial class AccountRecordE : StandardAttributesE
    {
        #region Custom Attributes
        public const string LogicalName = "account";
        public const string Title = "name";
        public const string CreditLlimit = "creditlimit";
        public const string CreditHold = "creditonhold";
        public const string PaymentTerms = "paymenttermscode";

        public const string Description = "description";
        public const string UserEmail = "emailaddress1";
        public const string Longitude = "address1_longitude";
        public const string Latitude = "address1_latitude";
        public const string ExampleOptionSet = "industrycode";

        #endregion Custom Attributes
    }
    public partial class AccountRecordE
    {
        #region Local enumValues

        public enum CustomEnum
        {
            CustomValue1 = 0,
            CustomValue2 = 1
        }

        #endregion Local enumValues
    }
}
