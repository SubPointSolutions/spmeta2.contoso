using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using SPMeta2.CSOM.Services;
using SPMeta2.Models;
using SPMeta2.Samples.Common;
using SPMeta2.SSOM.Services;

namespace SPMeta2.Samples.Provision.Base
{
    public class ProvisionTestBase
    {
        #region methods

        #region utils

        protected void DeploySiteModel(ModelNode model)
        {
            DeploySiteModelAsCSOM(model);
            DeploySiteModelAsSSOM(model);
        }

        protected void DeployWebModel(ModelNode model)
        {
            DeployWebModelAsCSOM(model);
            DeployWebModelAsSSOM(model);
        }

        protected void DeploySiteModelAsCSOM(ModelNode model)
        {
            using (var clientContext = new ClientContext(SampleConsts.CSOM_SiteUrl))
            {
                var csomProvisionService = new CSOMProvisionService();
                csomProvisionService.DeployModel(SPMeta2.CSOM.ModelHosts.SiteModelHost.FromClientContext(clientContext), model);
            }
        }

        protected void DeploySiteModelAsSSOM(ModelNode model)
        {
            using (var site = new SPSite(SampleConsts.SSOM_SiteUrl))
            {
                var ssomProvisionService = new SSOMProvisionService();
                ssomProvisionService.DeployModel(SPMeta2.SSOM.ModelHosts.SiteModelHost.FromSite(site), model);
            }
        }

        protected void DeployWebModelAsCSOM(ModelNode model)
        {
            using (var clientContext = new ClientContext(SampleConsts.CSOM_SiteUrl))
            {
                var csomProvisionService = new CSOMProvisionService();
                csomProvisionService.DeployModel(SPMeta2.CSOM.ModelHosts.WebModelHost.FromClientContext(clientContext), model);
            }
        }

        protected void DeployWebModelAsSSOM(ModelNode model)
        {
            using (var site = new SPSite(SampleConsts.SSOM_SiteUrl))
            {
                using (var web = site.OpenWeb())
                {
                    var ssomProvisionService = new SSOMProvisionService();
                    ssomProvisionService.DeployModel(SPMeta2.SSOM.ModelHosts.WebModelHost.FromWeb(web), model);
                }
            }
        }

        #endregion

        #endregion
    }
}
