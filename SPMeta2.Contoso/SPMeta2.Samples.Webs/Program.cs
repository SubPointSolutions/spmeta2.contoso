using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Samples.Webs
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1, define web
            var customerField = new WebDefinition
            {
                Id = new Guid("26470917-fbbf-413b-9eb3-537f74797e4e"),
                Title = "Customer Name",
                InternalName = "cstm_CustomerName",
                Description = "Name of the target customer.",
                Group = "Hello SPMeta2",
                FieldType = BuiltInFieldTypes.Text
            };

            // Step 2, define site model and artifact relationships - add field to the site 
            var siteModel = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site.AddField(customerField);
                             });

            // Step 3, deploy model via CSOM
            using (var clientContext = new ClientContext(SampleConsts.CSOM_SiteUrl))
            {
                var csomProvisionService = new CSOMProvisionService();
                csomProvisionService.DeployModel(SPMeta2.CSOM.ModelHosts.SiteModelHost.FromClientContext(clientContext), siteModel);
            }

            // Step 4, deploy model via SSOM
            using (var site = new SPSite(SampleConsts.SSOM_SiteUrl))
            {
                var csomProvisionService = new SSOMProvisionService();
                csomProvisionService.DeployModel(SPMeta2.SSOM.ModelHosts.SiteModelHost.FromSite(site), siteModel);
            }
        }
    }
}
