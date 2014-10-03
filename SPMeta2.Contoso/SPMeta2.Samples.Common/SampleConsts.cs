using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMeta2.Samples.Common
{
    public static class SampleConsts
    {
        #region constructors

        static SampleConsts()
        {
            SSOM_SiteUrl = "http://sp2013dev:31415";
            CSOM_SiteUrl = "http://sp2013dev:31415";
        }

        #endregion

        #region properties

        public static string SSOM_SiteUrl { get; set; }
        public static string CSOM_SiteUrl { get; set; }

        #endregion
    }
}
