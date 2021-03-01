using LinkConverterApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LinkConverterApi.Repository
{
    public class LinkRepository : ILinkRepository
    {
        protected LinkDbContext LinkContext { get; set; }
        private DbSet<Link> linkEntity;
        public LinkRepository(LinkDbContext linkContext)
        {
            this.LinkContext = linkContext;
            linkEntity = linkContext.Set<Link>();
        }
        public void SaveLinks(Link link)
        {
            LinkContext.Entry(link).State = EntityState.Added;
            LinkContext.SaveChanges();
        }

    }
}
