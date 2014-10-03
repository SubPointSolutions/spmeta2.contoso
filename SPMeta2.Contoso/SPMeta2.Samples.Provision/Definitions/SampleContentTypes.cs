using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Samples.Common;
using SPMeta2.Syntax.Default;

namespace SPMeta2.Samples.Provision.Definitions
{
    public class SampleContentTypes
    {
        #region properties

        public static class CRM
        {
            public static ContentTypeDefinition CustomerDocument = new ContentTypeDefinition
            {
                Id = new Guid("ccf280ad-5e90-43a0-ba7e-278a62a13e76"),
                Name = "Customer Document",
                ParentContentTypeId = BuiltInContentTypeId.Document,
                Group = SampleConsts.DefaultMetadataGroup
            };

            public static ContentTypeDefinition CustomerContract = new ContentTypeDefinition
            {
                Id = new Guid("a3dba0e0-ac48-4428-88c8-6ca121824172"),
                Name = "Customer Contract",
                ParentContentTypeId = CustomerDocument.GetContentTypeId(),
                Group = SampleConsts.DefaultMetadataGroup
            };

            public static ContentTypeDefinition CustomerSignedContract = new ContentTypeDefinition
            {
                Id = new Guid("afa4e8f3-ae78-4a58-a61d-54c19b71ed53"),
                Name = "Customer Signed Contract",
                ParentContentTypeId = CustomerDocument.GetContentTypeId(),
                Group = SampleConsts.DefaultMetadataGroup
            };

            public static ContentTypeDefinition CustomerAnnualContract = new ContentTypeDefinition
            {
                Id = new Guid("bc81515f-c2d9-4596-b892-f85db65ca875"),
                Name = "Customer Annual Contract",
                ParentContentTypeId = CustomerDocument.GetContentTypeId(),
                Group = SampleConsts.DefaultMetadataGroup
            };

            public static ContentTypeDefinition CustomerKPI = new ContentTypeDefinition
            {
                Id = new Guid("2062f158-4fbe-473b-8938-454b0d29a257"),
                Name = "Customer KPI",
                ParentContentTypeId = BuiltInContentTypeId.Item,
                Group = SampleConsts.DefaultMetadataGroup
            };

        }


        #endregion
    }
}
