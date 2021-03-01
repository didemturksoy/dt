using System.Text;

namespace LinkConverterApi.Services
{
    public class ConvertToDeepLinkService :IConvertToDeepLinkService
    {
        public string ConvertToDeepLink(string link)
        {
           if(!link.Contains("https://www.trendyol.com"))
                return string.Empty;

            string baseLink = link.Substring(24);

            if (baseLink.StartsWith("/sr?"))
            {
                return ConvertDeepLinkForSearch(baseLink);
            }
            else if (baseLink.Contains("-p-"))
            {
                return ConvertToDeepLinkForPdp();
            }
            else
                return ConvertToDeepLinkForHome();
        }

        #region Deep Link Converter For Search

        public string ConvertDeepLinkForSearch(string link)
        {
            string searchKey = link.Substring(6);

            return CreateDeepLinkForSearch(searchKey);
        }

        public string CreateDeepLinkForSearch(string searchKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ty://?Page=Search&Query={0}", searchKey);

            return sb.ToString();
        }
        #endregion

        public string ConvertToDeepLinkForPdp()
        {
            return "ty://?Page=Product&ContentId=1925865";
        }

        public string ConvertToDeepLinkForHome()
        {
            return "ty://?Page=Home";
        }
    }
}
