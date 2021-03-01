using LinkConverterApi.Repository;
using LinkConverterApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LinkConverterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkConverterController : ControllerBase
    {
        private readonly IConvertToDeepLinkService _deepLinkService;
        private readonly IConvertToWebUrlService _webUrlService;
        private ILinkRepository _linkRepository;
        public LinkConverterController(IConvertToDeepLinkService deepLinkService, IConvertToWebUrlService webUrlService, ILinkRepository linkRepository)
        {
            _deepLinkService = deepLinkService;
            _webUrlService = webUrlService;
            _linkRepository = linkRepository;
        }


        [Route("api/ConvertToDeepLink")]
        [HttpGet]
        public ActionResult ConvertToDeepLink(string link)
        {
            //Türkçe karakter sorununa bakılacak.
            try
            {
                var result = _deepLinkService.ConvertToDeepLink(link);

                SaveLinksToDb(link, result, false);

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("api/ConvertToWebUrl")]
        [HttpGet]
        public ActionResult ConvertToWebUrl(string link)
        {
            //türkçe karakter sorununa bakılacak.
            try
            {
                var result = _webUrlService.ConvertToWebUrl(link);

                SaveLinksToDb(link, result, true);
                
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SaveLinksToDb(string oldLink, string newLink, bool isMobile)
        {
            try
            {              
                _linkRepository.SaveLinks(new Model.Link
                {
                    OldLink = oldLink,
                    NewLink = newLink,
                    IsMobile = isMobile,
                    CreatedDate = DateTime.UtcNow
                });

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
