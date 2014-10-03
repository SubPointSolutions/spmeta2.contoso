using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using SPMeta2.CSOM.Services;
using SPMeta2.SSOM.Services;
using SPMeta2.Models;
using SPMeta2.Samples.Common;
using SPMeta2.Enumerations;
using SPMeta2.Syntax.Default;
using SPMeta2.Definitions;

namespace SPMeta2.Samples.Provision
{
    [TestClass]
    public class BaseProvision
    {
        #region samples

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_NewSiteField()
        {
            // Step 1, define site field
            var customerField = new FieldDefinition
            {
                Id = new Guid("26470917-fbbf-413b-9eb3-537f74797e4e"),
                Title = "Customer Name",
                InternalName = "cstm_CustomerName",
                Description = "Name of the target customer.",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Text
            };

            // Step 2, define site model and artifact relationships - add field to the site 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site.AddField(customerField);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_NewContentType()
        {
            // Step 1, define content type
            var customerContentType = new ContentTypeDefinition
            {
                Id = new Guid("26470917-fbbf-413b-9eb3-537f74797e4e"),
                Name = "Important Customer",
                Description = "An important client to work with.",
                ParentContentTypeId = BuiltInContentTypeId.Item,
                Group = SampleConsts.DefaultMetadataGroup
            };

            // Step 2, define site model and artifact relationships - add content type to the site 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site.AddContentType(customerContentType);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_NewWeb()
        {
            // Step 1, define web
            var customerWeb = new WebDefinition
            {
                Title = "Customers",
                Url = "customers",
                Description = "Web site to store customer information.",
                WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite

            };

            // Step 2, define web model and artifact relationships - add web to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web.AddWeb(customerWeb);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_DeactivateOOTBWebFeature()
        {
            // Step 1, define web feature
            //
            // BuiltInWebFeatures defines OOTB features
            // Inherit() method creates a copy of the feature so that we can adjust properties as we wish
            // Inherit(action) allows to setup particular features properties - Enable/Disable/ForceEnable and the rest
            var mdsFeatrure = BuiltInWebFeatures.MinimalDownloadStrategy
                                                .Inherit(feature =>
                                                {
                                                    feature.Enable = false;
                                                });

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web.AddFeature(mdsFeatrure);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_ActivateOOTBWebFeature()
        {
            // Step 1, define web feature
            //
            // BuiltInWebFeatures defines OOTB features
            // Inherit() method creates a copy of the feature so that we can adjust properties as we wish
            // Inherit(action) allows to setup particular features properties - Enable/Disable/ForceEnable and the rest
            var mdsFeatrure = BuiltInWebFeatures.MinimalDownloadStrategy
                                                .Inherit(feature =>
                                                {
                                                    feature.Enable = true;
                                                });

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web.AddFeature(mdsFeatrure);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_DeactivateCustomFeature()
        {
            // Step 1, define web feature
            //
            // For custom features we need to define Id, Scope and Enable/ForceActivate properties
            // Title is not used, but it is nice to have readable code
            var mdsFeatrure = new FeatureDefinition
            {
                Title = "Minimal Download Strategy",
                Id = new Guid("{87294c72-f260-42f3-a41b-981a2ffce37a}"),
                Scope = SPMeta2.Definitions.FeatureDefinitionScope.Web,
                Enable = false
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web.AddFeature(mdsFeatrure);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_ActivateCustomFeature()
        {
            // Step 1, define web feature
            //
            // For custom features we need to define Id, Scope and Enable/ForceActivate properties
            // Title is not used, but it is nice to have readable code
            var mdsFeatrure = new FeatureDefinition
            {
                Title = "Minimal Download Strategy",
                Id = new Guid("{87294c72-f260-42f3-a41b-981a2ffce37a}"),
                Scope = SPMeta2.Definitions.FeatureDefinitionScope.Web,
                Enable = true
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web.AddFeature(mdsFeatrure);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_TopNavigation()
        {
            // Step 1, define top navigation nodes
            var home = new TopNavigationNodeDefinition
            {
                Title = "Home",
                Url = "/",
                IsExternal = false
            };

            var google = new TopNavigationNodeDefinition
            {
                Title = "Google",
                Url = "http://google.com",
                IsExternal = true
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddTopNavigationNode(home)
                                   .AddTopNavigationNode(google);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_QuickNavigation()
        {
            // Step 1, define top navigation nodes
            var home = new QuickLaunchNavigationNodeDefinition
            {
                Title = "Home link",
                Url = "/",
                IsExternal = false
            };

            var google = new QuickLaunchNavigationNodeDefinition
            {
                Title = "Google link",
                Url = "http://google.com",
                IsExternal = true
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddQuickLaunchNavigationNode(home)
                                   .AddQuickLaunchNavigationNode(google);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_Lists()
        {
            // Step 1, define lists
            var customerDocuments = new ListDefinition
            {
                Title = "Customer documents",
                Url = "CustomerDocs",
                Description = "Stores customer related documents.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary
            };

            var customerTasks = new ListDefinition
            {
                Title = "Customer tasks",
                Url = "CustomerTasks",
                Description = "Stores customer related tasks.",
                TemplateType = BuiltInListTemplateTypeId.TasksWithTimelineAndHierarchy
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddList(customerDocuments)
                                   .AddList(customerTasks);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_ListsViews()
        {
            // Step 1, define lists
            var customerClaims = new ListDefinition
            {
                Title = "Customer claims",
                Url = "CustomerCLaims",
                Description = "Stores customer related claims.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary
            };


            var lastTenClaims = new ListViewDefinition
            {
                Title = "Last 10 Claims",
                IsDefault = false,
                RowLimit = 10,
                Query = "",
                Fields = new System.Collections.ObjectModel.Collection<string>
                {
                    BuiltInInternalFieldNames.ID,
                    BuiltInInternalFieldNames.File_x0020_Type,
                    BuiltInInternalFieldNames.FileLeafRef,
                    BuiltInInternalFieldNames.Created,
                    BuiltInInternalFieldNames.Modified,
                    BuiltInInternalFieldNames.Author,
                }
            };

            var lastTenEditedClaims = new ListViewDefinition
            {
                Title = "Last 10 Claims",
                IsDefault = false,
                RowLimit = 10,
                Query = "",
                Fields = new System.Collections.ObjectModel.Collection<string>
                {
                    BuiltInInternalFieldNames.ID,
                    BuiltInInternalFieldNames.File_x0020_Type,
                    BuiltInInternalFieldNames.FileLeafRef,
                    BuiltInInternalFieldNames.Created,
                    BuiltInInternalFieldNames.Modified,
                    BuiltInInternalFieldNames.Author,
                }
            };

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddList(customerClaims, list =>
                                   {
                                       list
                                           .AddView(lastTenClaims)
                                           .AddView(lastTenEditedClaims);
                                   });
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Base provision")]
        public void Deploy_SecurityGroups()
        {
            // Step 1, define security groups
            var customerSupport = new SecurityGroupDefinition
            {
                Name = "Customer support",
                Description = "Customer support team."
            };

            var customerReviewers = new SecurityGroupDefinition
            {
                Name = "Customer reviewers",
                Description = "Customer reviewers team."
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                   .AddSecurityGroup(customerSupport)
                                   .AddSecurityGroup(customerReviewers);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        #endregion

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
    }
}
