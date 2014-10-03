using System;
using System.Text;
using Microsoft.SharePoint.Client.WebParts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using SPMeta2.BuiltInDefinitions;
using SPMeta2.CSOM.Services;
using SPMeta2.Samples.Provision.Base;
using SPMeta2.Samples.Provision.Utils;
using SPMeta2.SSOM.Services;
using SPMeta2.Models;
using SPMeta2.Samples.Common;
using SPMeta2.Enumerations;
using SPMeta2.Syntax.Default;
using SPMeta2.Definitions;
using SPMeta2.Syntax.Default.Extensions;

namespace SPMeta2.Samples.Provision
{
    [TestClass]
    public class HelloWorldProvision : ProvisionTestBase
    {
        #region samples

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_NewSiteField()
        {
            // Step 1, define site field
            var customerNameField = new FieldDefinition
            {
                Id = new Guid("26470917-fbbf-413b-9eb3-537f74797e4e"),
                Title = "Customer Name",
                InternalName = "cstm_CustomerName",
                Description = "Name of the target customer.",
                Group = SampleConsts.DefaultMetadataGroup,
                FieldType = BuiltInFieldTypes.Text
            };

            var customerDescriptionField = new FieldDefinition
             {
                 Id = new Guid("26470917-fbbf-413b-9eb3-537f74797e4e"),
                 Title = "Customer Description",
                 InternalName = "cstm_CustomerDescription",
                 Description = "Description of the target customer.",
                 Group = SampleConsts.DefaultMetadataGroup,
                 FieldType = BuiltInFieldTypes.Note
             };

            // Step 2, define site model and artifact relationships - add field to the site 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                     .AddField(customerNameField)
                                     .AddField(customerDescriptionField);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
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

            var customerDocumentContentType = new ContentTypeDefinition
            {
                Id = new Guid("3faaa80e-0780-47d9-bc4e-ff767e11bd4a"),
                Name = "Important Customer",
                Description = "An important client to work with.",
                ParentContentTypeId = BuiltInContentTypeId.Item,
                Group = SampleConsts.DefaultMetadataGroup
            };

            // Step 2, define site model and artifact relationships - add content type to the site 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                     .AddContentType(customerContentType)
                                     .AddContentType(customerDocumentContentType);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
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

            var supportWeb = new WebDefinition
            {
                Title = "Support",
                Url = "support",
                Description = "Feedback and support site.",
                WebTemplate = BuiltInWebTemplates.Collaboration.TeamSite
            };

            // Step 2, define web model and artifact relationships - add web to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                    .AddWeb(customerWeb)
                                    .AddWeb(supportWeb);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
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

            var gettingStartedFeature = BuiltInWebFeatures.GettingStarted
                                                .Inherit(feature =>
                                                {
                                                    feature.Enable = false;
                                                });

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddFeature(mdsFeatrure)
                                     .AddFeature(gettingStartedFeature);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_ActivateOOTBSiteFeature()
        {
            // Step 1, define web feature
            //
            // BuiltInSiteFeatures defines OOTB features
            // Inherit() method creates a copy of the feature so that we can adjust properties as we wish
            // Inherit(action) allows to setup particular features properties - Enable/Disable/ForceEnable and the rest
            var workflows = BuiltInSiteFeatures.SharePoint2007Workflows
                                                .Inherit(feature =>
                                                {
                                                    feature.Enable = true;
                                                });

            var threeStateWorkflow = BuiltInSiteFeatures.ThreeStateWorkflow
                                               .Inherit(feature =>
                                               {
                                                   feature.Enable = true;
                                               });

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                   .AddSiteFeature(workflows)
                                   .AddSiteFeature(threeStateWorkflow);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
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

            var gettingStartedFeature = BuiltInWebFeatures.GettingStarted
                                               .Inherit(feature =>
                                               {
                                                   feature.Enable = true;
                                               });

            // Step 2, define web model and artifact relationships - add feature to the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddWebFeature(mdsFeatrure)
                                   .AddWebFeature(gettingStartedFeature);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
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
        [TestCategory("Hello world provision")]
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
        [TestCategory("Hello world provision")]
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
        [TestCategory("Hello world provision")]
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
        [TestCategory("Hello world provision")]
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
        [TestCategory("Hello world provision")]
        public void Deploy_ListsViews()
        {
            // Step 1, define lists
            var customerClaims = new ListDefinition
            {
                Title = "Customer claims",
                Url = "CustomerClaims",
                Description = "Stores customer related claims.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary
            };


            var lastTenClaims = new ListViewDefinition
            {
                Title = "Last 10 claims",
                IsDefault = false,
                RowLimit = 10,
                Query = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='FALSE' /></OrderBy>", BuiltInInternalFieldNames.Created),
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
                Title = "Last 10 edited claims",
                IsDefault = false,
                RowLimit = 10,
                Query = string.Format("<OrderBy><FieldRef Name='{0}' Ascending='FALSE' /></OrderBy>", BuiltInInternalFieldNames.Modified),
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
        [TestCategory("Hello world provision")]
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

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_SecurityRoles()
        {
            // Step 1, define security groups
            var customerSupport = new SecurityRoleDefinition
            {
                Name = "Customer support role",
                Description = "Customer support team."
            };

            var customerReviewers = new SecurityRoleDefinition
            {
                Name = "Customer reviewers role",
                Description = "Customer reviewers team."
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                   .AddSecurityRole(customerSupport)
                                   .AddSecurityRole(customerReviewers);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_WikiPage()
        {
            // Step 1, define security groups
            var aboutPage = new WikiPageDefinition
            {
                Title = "About",
                FileName = "about.aspx"
            };

            var contactPage = new WikiPageDefinition
            {
                Title = "Contact",
                FileName = "contact.aspx"
            };

            var faqPage = new WikiPageDefinition
            {
                Title = "FAQ",
                FileName = "faq.aspx"
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddList(BuiltInListDefinitions.SitePages, list =>
                                   {
                                       list
                                           .AddWikiPage(aboutPage)
                                           .AddWikiPage(contactPage)
                                           .AddWikiPage(faqPage);
                                   });
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_WebPartPages()
        {
            // Step 1, define security groups
            var sales = new WebPartPageDefinition
            {
                Title = "Sales Dashboard",
                FileName = "Sales-Dashboard.aspx",
                PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd1
            };

            var ratings = new WebPartPageDefinition
            {
                Title = "Ratings Dashboard",
                FileName = "Ratings-Dashboard.aspx",
                PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd2
            };

            var performance = new WebPartPageDefinition
            {
                Title = "Performance Dashboard",
                FileName = "Performance-Dashboard.aspx",
                PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd3
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddList(BuiltInListDefinitions.SitePages, list =>
                                   {
                                       list
                                           .AddWebPartPage(sales)
                                           .AddWebPartPage(ratings)
                                           .AddWebPartPage(performance);
                                   });
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_WebParts()
        {
            // Step 1, define security groups
            var gettingStarted = new SPMeta2.Definitions.WebPartDefinition
            {
                Title = "Getting started with site",
                Id = "spmGettingStarted",
                ZoneId = "Main",
                ZoneIndex = 100,
                WebpartXmlTemplate = ResourceReader.ReadFromResourceName("Templates.Webparts.Get started with your site.webpart")
            };

            var contentEditor = new SPMeta2.Definitions.WebPartDefinition
            {
                Title = "SPMeta2 Content Editor Webpart",
                Id = "spmContentEditorWebpart",
                ZoneId = "Main",
                ZoneIndex = 200,
                WebpartXmlTemplate = ResourceReader.ReadFromResourceName("Templates.Webparts.Content Editor.dwp")
            };

            var webpartPage = new WebPartPageDefinition
            {
                Title = "Getting started",
                FileName = "Getting-Started.aspx",
                PageLayoutTemplate = BuiltInWebPartPageTemplates.spstd1
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                   .AddList(BuiltInListDefinitions.SitePages, list =>
                                   {
                                       list
                                           .AddWebPartPage(webpartPage, page =>
                                           {
                                               page
                                                   .AddWebPart(gettingStarted)
                                                   .AddWebPart(contentEditor);
                                           });
                                   });
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_CustomUserActions()
        {
            // Step 1, define security groups
            var signinAsDifferentUser = new UserCustomActionDefinition
            {
                Title = "Signin as different user",
                Name = "Signin as different user",
                Group = BuiltInCustomActionLocationId.Microsoft.SharePoint.StandardMenu.Groups.SiteActions,
                Location = BuiltInCustomActionLocationId.Microsoft.SharePoint.StandardMenu.Location,
                Sequence = 2000,
                Url = "~site/_layouts/closeConnection.aspx?loginasanotheruser=true"
            };

            // watch out 'customer site sync handler' log message while checking Chrome/IE with F12
            var customerSiteSyncSettings = new UserCustomActionDefinition
            {
                Title = "Customer site sync handler",
                Name = "Customer site sync handler",
                Location = "ScriptLink",
                Sequence = 2010,
                ScriptBlock = "console.log('customer site sync handler');"
            };

            var jQueryFromCDN = new UserCustomActionDefinition
            {
                Title = "jquery",
                Name = "jquery",
                Location = "ScriptLink",
                Sequence = 3000,
                ScriptSrc = "~site/Style Library/libs/jquery/1.11.1/jquery.min.js"
            };

            var jQueryFromCDNInitHandler = new UserCustomActionDefinition
            {
                Title = "jquery-init-handler",
                Name = "jquery-init-handler",
                Location = "ScriptLink",
                Sequence = 3010,
                ScriptBlock = "jQuery(document).ready( function() {  console.log('hello from jQuery'); });"
            };


            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                   .AddUserCustomAction(signinAsDifferentUser)
                                   .AddUserCustomAction(customerSiteSyncSettings)
                                   .AddUserCustomAction(jQueryFromCDN)
                                   .AddUserCustomAction(jQueryFromCDNInitHandler);
                             });

            // Step 3, deploy model
            DeploySiteModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_WebProperty()
        {
            // Step 1, define security groups
            var siteType = new PropertyDefinition
            {
                Key = "_site_type",
                Value = "spmeta2 demo"
            };

            var tick = new PropertyDefinition
            {
                Key = "_site_tick",
                Value = Environment.TickCount,
                Overwrite = true
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            var model = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddDefinitionNode(siteType)
                                     .AddDefinitionNode(tick);
                             });

            // Step 3, deploy model
            DeployWebModel(model);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_ContentTypesToList()
        {
            // Step 1, define security groups
            var contractsList = new ListDefinition
            {
                Title = "Customer contracts",
                Url = "CustomerContracts",
                Description = "Stores customer related contracts.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary,
                ContentTypesEnabled = true
            };

            var standardContract = new ContentTypeDefinition
            {
                Id = new Guid("49fbbb62-f8cd-4372-94a0-756e55a8945e"),
                Name = "Standard Contract",
                ParentContentTypeId = BuiltInContentTypeId.Document,
                Group = SampleConsts.DefaultMetadataGroup
            };

            var legacyContract = new ContentTypeDefinition
            {
                Id = new Guid("ba049ddb-962a-4b8e-80a0-2bd10a6c4a88"),
                Name = "Legacy Contract",
                ParentContentTypeId = BuiltInContentTypeId.Document,
                Group = SampleConsts.DefaultMetadataGroup
            };

            // Step 2, define web model and artifact relationships - add security groups t the web 
            // Deploy site model first - content types to site
            var siteModel = SPMeta2Model
                             .NewSiteModel(site =>
                             {
                                 site
                                     .AddContentType(standardContract)
                                     .AddContentType(legacyContract);
                             });

            DeploySiteModel(siteModel);

            // deploy web model - list and add content type links to list
            var webModel = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddList(contractsList, list =>
                                     {
                                         list
                                             .AddContentTypeLink(standardContract)
                                             .AddContentTypeLink(legacyContract);
                                     });
                             });

            DeployWebModel(webModel);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_FoldersToLibrary()
        {
            // Step 1, define security groups
            var customerReports = new ListDefinition
            {
                Title = "Customer reports",
                Url = "CustomerReports",
                Description = "Stores customer related documents.",
                TemplateType = BuiltInListTemplateTypeId.DocumentLibrary
            };

            var Year2010 = new FolderDefinition { Name = "2010" };
            var Year2011 = new FolderDefinition { Name = "2011" };
            var Year2012 = new FolderDefinition { Name = "2012" };

            var Q1 = new FolderDefinition { Name = "Q1" };
            var Q2 = new FolderDefinition { Name = "Q2" };
            var Q3 = new FolderDefinition { Name = "Q3" };
            var Q4 = new FolderDefinition { Name = "Q4" };

            // deploy web model - list and add content type links to list
            var webModel = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddList(customerReports, list =>
                                     {
                                         list
                                             .AddFolder(Year2010, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             })
                                             .AddFolder(Year2011, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             })
                                             .AddFolder(Year2012, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             });
                                     });
                             });

            DeployWebModel(webModel);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_FoldersToList()
        {
            // Step 1, define security groups
            var customerIssues = new ListDefinition
            {
                Title = "Customer KPIs",
                Url = "CustomerKPI",
                Description = "Stores customer related KPIs.",
                TemplateType = BuiltInListTemplateTypeId.GenericList
            };

            var Year2010 = new FolderDefinition { Name = "2010" };
            var Year2011 = new FolderDefinition { Name = "2011" };
            var Year2012 = new FolderDefinition { Name = "2012" };

            var Q1 = new FolderDefinition { Name = "Q1" };
            var Q2 = new FolderDefinition { Name = "Q2" };
            var Q3 = new FolderDefinition { Name = "Q3" };
            var Q4 = new FolderDefinition { Name = "Q4" };

            // deploy web model - list and add content type links to list
            var webModel = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddList(customerIssues, list =>
                                     {
                                         list
                                             .AddFolder(Year2010, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             })
                                             .AddFolder(Year2011, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             })
                                             .AddFolder(Year2012, folder =>
                                             {
                                                 folder
                                                     .AddFolder(Q1)
                                                     .AddFolder(Q2)
                                                     .AddFolder(Q3)
                                                     .AddFolder(Q4);
                                             });
                                     });
                             });

            DeployWebModel(webModel);
        }

        [TestMethod]
        [TestCategory("Hello world provision")]
        public void Deploy_ModuleFiles()
        {
            // Step 1, define security groups
            var helloModuleFile = new ModuleFileDefinition
            {
                FileName = "hello-module.txt",
                Content = Encoding.UTF8.GetBytes("A hello world module file provision.")
            };

            var angularFile = new ModuleFileDefinition
            {
                FileName = "angular.min.js",
                Content = Encoding.UTF8.GetBytes(ResourceReader.ReadFromResourceName("Modules.js.angular.min.js"))
            };

            var jQueryFile = new ModuleFileDefinition
            {
                FileName = "jquery-1.11.1.min.js",
                Content = Encoding.UTF8.GetBytes(ResourceReader.ReadFromResourceName("Modules.js.jquery-1.11.1.min.js"))
            };

            var jsFolder = new FolderDefinition { Name = "spmeta2-custom-js" };

            // deploy web model - list and add content type links to list
            var webModel = SPMeta2Model
                             .NewWebModel(web =>
                             {
                                 web
                                     .AddList(BuiltInListDefinitions.StyleLibrary, list =>
                                     {
                                         list
                                             .AddModuleFile(helloModuleFile)
                                             .AddFolder(jsFolder, folder =>
                                             {
                                                 folder
                                                     .AddModuleFile(angularFile)
                                                     .AddModuleFile(jQueryFile);
                                             });
                                     });
                             });

            DeployWebModel(webModel);
        }

        #endregion

       
    }
}
