using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;

namespace SPMeta2.Samples.Provision.Definitions
{
    public static class SampleLists
    {
        public static class CustomerSite
        {
            public static ListDefinition CustomerDocs = new ListDefinition
            {
                Title = "Customer Documents",
                Url = "CustomerDocs",
                Description = "Stores customer related documents.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary
            };

            public static ListDefinition CustomerTasks = new ListDefinition
            {
                Title = "Customer Tasks",
                Url = "CustomerTasks",
                TemplateType = BuiltInListTemplateTypeId.TasksWithTimelineAndHierarchy
            };

            public static ListDefinition CustomerIssues = new ListDefinition
            {
                Title = "Customer Issues",
                Url = "CustomerIssues",
                TemplateType = BuiltInListTemplateTypeId.IssueTracking
            };

            public static ListDefinition KPI = new ListDefinition
            {
                Title = "KPI History",
                Url = "KPI",
                TemplateType = BuiltInListTemplateTypeId.GenericList
            };
        }
    }
}
