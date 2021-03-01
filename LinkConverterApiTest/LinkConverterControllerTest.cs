using LinkConverterApi.Controllers;
using LinkConverterApi.Model;
using LinkConverterApi.Repository;
using LinkConverterApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LinkConverterApiTest
{
    public class LinkConverterControllerTest
    {
        LinkConverterController _controller;
        Mock<IConvertToWebUrlService> _webUrlService;
        Mock<IConvertToDeepLinkService> _deepLinkService;
        Mock<ILinkRepository> _linkRepository;
        public LinkConverterControllerTest()
        {
            _webUrlService = new Mock<IConvertToWebUrlService>();
            _deepLinkService = new Mock<IConvertToDeepLinkService>();
            _linkRepository = new Mock<ILinkRepository>();
            _controller = new LinkConverterController(_deepLinkService.Object, _webUrlService.Object,_linkRepository.Object);
        }

        [Theory, InlineData("https://www.trendyol.com/casio/saatp1925865?boutiqueId=439892&merchantId=105064", "ty://?Page=Product&ContentId=1925865&CampaignId=439892&MerchantId=105064")]
        [InlineData("https://www.trendyol.com/sr?q=elbise", "ty://?Page=Search&Query=elbise")]
        [InlineData("https://www.trendyol.com/Hesabim/Favoriler", "ty://?Page=Home")]
        public void ConvertToDeepLink_Should_Return_As_Expected(string link, string expected)
        {
            _deepLinkService.Setup(p => p.ConvertToDeepLink(link)).Returns(expected);

          
            var result = _controller.ConvertToDeepLink(link) as ObjectResult;

            
            Assert.Equal(expected, result.Value);

        }

        [Theory, InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892", "https://www.trendyol.com/brand/namei-p-1925865?merchantId=105064")]
        [InlineData("ty://?Page=Search&Query=%C3%BCt%C3%BC", "https://www.trendyol.com/sr?q=%C3%BCt%C3%BC")]
        [InlineData("ty://?Page=Orders", "https://www.trendyol.com")]
        public void ConvertToWebUrl_Should_Return_As_Expected(string link, string expected)
        {
            _webUrlService.Setup(p => p.ConvertToWebUrl(link)).Returns(expected);


            var result = _controller.ConvertToWebUrl(link) as ObjectResult;


            Assert.Equal(expected, result.Value);

        }


        [Theory, InlineData("ty://?Page=Product&ContentId=1925865&CampaignId=439892", "https://www.trendyol.com/brand/namei-p-1925865?merchantId=105064", false)]
        [InlineData("ty://?Page=Search&Query=%C3%BCt%C3%BC", "https://www.trendyol.com/sr?q=%C3%BCt%C3%BC", false)]
        [InlineData("https://www.trendyol.com/Hesabim/Favoriler", "ty://?Page=Home", true)]
        public void SaveLinkToDb_Should_Success(string oldLink, string newLink, bool isMobile)
        {
            Link linkEntity = new Link
            {
                OldLink = oldLink,
                NewLink = newLink,
                IsMobile = isMobile
            };

            var result = _controller.SaveLinksToDb(oldLink, newLink, isMobile) as ObjectResult;

            Assert.IsType<OkResult>(result);
        }
    }
}
