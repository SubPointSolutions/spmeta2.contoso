using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Samples.Common;

namespace SPMeta2.Samples.Provision.Definitions
{
    public class SampleFields
    {
        #region properties

        public static class CRM
        {
            public static FieldDefinition ClientId = new FieldDefinition
            {
                Id = new Guid("1d20b513-0095-4735-a68d-c5c972494afc"),
                Title = "Client ID",
                InternalName = "clnt_ClientId",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Text
            };

            public static FieldDefinition ClientName = new FieldDefinition
            {
                Id = new Guid("2a121dbf-ad68-4f2c-af49-f8671dfd4bf7"),
                Title = "Client Name",
                InternalName = "clnt_ClientName",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Text
            };

            public static FieldDefinition ClientComment = new FieldDefinition
            {
                Id = new Guid("0d122a96-24ba-4776-a68c-32cf32bb1150"),
                Title = "Client Name",
                InternalName = "clnt_ClientComment",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Note
            };

            public static FieldDefinition ClientIsNonProfit = new FieldDefinition
            {
                Id = new Guid("f8e98eee-842c-48a3-a3ad-9a204e809256"),
                Title = "Client Name",
                InternalName = "clnt_ClientIsNonProfit",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Boolean
            };

            public static FieldDefinition Dept = new FieldDefinition
            {
                Id = new Guid("c2a3f0fb-024c-43cd-8502-55ce866fb0ec"),
                Title = "Client Dept",
                InternalName = "clnt_Dept",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Currency
            };

            public static FieldDefinition Loan = new FieldDefinition
            {
                Id = new Guid("187ea759-a615-4638-9a0a-e9980327eed6"),
                Title = "Client Dept",
                InternalName = "clnt_Loan",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Currency
            };

            public static FieldDefinition Revenue = new FieldDefinition
            {
                Id = new Guid("306dc168-bdf1-479c-9c41-40c977634dbf"),
                Title = "Client Revenue",
                InternalName = "clnt_Revenue",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Currency
            };

            public static FieldDefinition SignLink = new FieldDefinition
            {
                Id = new Guid("d3526287-8657-4be5-a7de-7633441d0213"),
                Title = "Client Sign Link",
                InternalName = "clnt_SignLink",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.URL
            };
        }


        #endregion
    }
}
