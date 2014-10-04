using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SPMeta2.Definitions;
using SPMeta2.Samples.Provision.Base;
using SPMeta2.Samples.Provision.Definitions;
using SPMeta2.Syntax.Default;
using SPMeta2.Enumerations;
using SPMeta2.BuiltInDefinitions;

namespace SPMeta2.Samples.Provision
{
    [TestClass]
    public class ScenariosProvision : ProvisionTestBase
    {
        #region samples

        [TestMethod]
        [TestCategory("Scenarios provision")]
        public void Deploy_CRMWebHierarchy()
        {
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddWeb(SampleWebs.Archive)
                                     .AddWeb(SampleWebs.Blog)
                                     .AddWeb(SampleWebs.CIO, cioWeb =>
                                     {
                                         cioWeb
                                             .AddWeb(SampleWebs.Blog);
                                     })
                                     .AddWeb(SampleWebs.Departments, departmentsWeb =>
                                     {
                                         departmentsWeb
                                           .AddWeb(SampleWebs.HR)
                                           .AddWeb(SampleWebs.IT)
                                           .AddWeb(SampleWebs.Delivery)
                                           .AddWeb(SampleWebs.Sales)
                                           .AddWeb(SampleWebs.PR);
                                     })
                                     .AddWeb(SampleWebs.Projects)
                                     .AddWeb(SampleWebs.Wiki)
                                     .AddWeb(SampleWebs.FAQ);

                             });

            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Scenarios provision")]
        public void Deploy_CRMMetadata()
        {
            var model = SPMeta2Model
                           .NewSiteModel(site =>
                           {
                               site
                                   .WithFields(field =>
                                   {
                                       field
                                           .AddField(SampleFields.CRM.ClientComment)
                                           .AddField(SampleFields.CRM.ClientId)
                                           .AddField(SampleFields.CRM.ClientIsNonProfit)
                                           .AddField(SampleFields.CRM.ClientName)
                                           .AddField(SampleFields.CRM.Dept)
                                           .AddField(SampleFields.CRM.Loan)
                                           .AddField(SampleFields.CRM.Revenue)
                                           .AddField(SampleFields.CRM.SignLink);
                                   })
                                   .WithContentTypes(contentTypes =>
                                   {
                                       contentTypes
                                           .AddContentType(SampleContentTypes.CRM.CustomerDocument, customerDoc =>
                                           {
                                               customerDoc
                                                   .AddContentTypeFieldLink(SampleFields.CRM.ClientId)
                                                   .AddContentTypeFieldLink(SampleFields.CRM.ClientName)
                                                   .AddContentTypeFieldLink(SampleFields.CRM.ClientComment);
                                           })
                                           .AddContentType(SampleContentTypes.CRM.CustomerAnnualContract)
                                           .AddContentType(SampleContentTypes.CRM.CustomerContract)
                                           .AddContentType(SampleContentTypes.CRM.CustomerSignedContract, signedContract =>
                                           {
                                               signedContract
                                                   .AddContentTypeFieldLink(SampleFields.CRM.SignLink);
                                           })
                                           .AddContentType(SampleContentTypes.CRM.CustomerKPI, clientKpi =>
                                           {
                                               clientKpi
                                                   .AddContentTypeFieldLink(SampleFields.CRM.Dept)
                                                   .AddContentTypeFieldLink(SampleFields.CRM.Loan)
                                                   .AddContentTypeFieldLink(SampleFields.CRM.Revenue)
                                                   .AddContentTypeFieldLink(SampleFields.CRM.SignLink);
                                           });

                                   });
                           });

            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Scenarios provision")]
        public void Deploy_CRMCustomerSite()
        {
            var customerName = string.Format("Customer_{0}", Environment.TickCount);
            var newCustomerWeb = new WebDefinition
            {
                Title = customerName,
                Url = customerName,
                WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite
            };

            var model = SPMeta2Model
                            .NewWebModel(newCustomerWeb, web =>
                            {
                                web
                                    .AddList(SampleLists.CustomerSite.CustomerDocs)
                                    .AddList(SampleLists.CustomerSite.CustomerIssues)
                                    .AddList(SampleLists.CustomerSite.CustomerTasks)
                                    .AddList(SampleLists.CustomerSite.KPI)
                                    .AddList(BuiltInListDefinitions.SitePages, pages =>
                                    {
                                        pages
                                            .AddWebPartPage(SamplePages.KPI)
                                            .AddWebPartPage(SamplePages.MyTasks)
                                            .AddWikiPage(SamplePages.About)
                                            .AddWikiPage(SamplePages.FAQ);
                                    });
                            });

            DeployWebModel(model);
        }

        #endregion
    }
}
