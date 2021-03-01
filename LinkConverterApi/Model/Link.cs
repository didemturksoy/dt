using System;

namespace LinkConverterApi.Model
{
    public class Link
    {
        public int LinkId { get; set; }

        public string OldLink { get; set; }

        public string NewLink { get; set; }

        public bool IsMobile { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
