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
            SSOM.SiteUrl = "http://sp2013dev:31415";
            CSOM.SiteUrl = "http://sp2013dev:31415";
        }

        #endregion

        #region properties

        public static class SSOM
        {
            public static string SiteUrl { get; set; }
        }

        public static class CSOM
        {
            public static string SiteUrl { get; set; }
        }

        #endregion
    }
}
