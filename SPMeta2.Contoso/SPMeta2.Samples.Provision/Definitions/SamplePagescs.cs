using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.WebPartPages;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;

namespace SPMeta2.Samples.Provision.Definitions
{
    public static class SamplePages
    {
        #region properties

        public static WikiPageDefinition About = new WikiPageDefinition
        {
            Title = "About",
            FileName = "about.aspx"
        };

        public static WikiPageDefinition FAQ = new WikiPageDefinition
        {
            Title = "FAQ",
            FileName = "FAQ.aspx"
        };

        public static WebPartPageDefinition KPI = new WebPartPageDefinition
        {
            Title = "KPI",
            FileName = "KPI.aspx",
            PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd1
        };

        public static WebPartPageDefinition MyTasks = new WebPartPageDefinition
        {
            Title = "MyTasks",
            FileName = "MyTasks.aspx",
            PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd1
        };


        #endregion
    }
}
