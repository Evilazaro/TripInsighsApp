using System;

namespace poi.Models
{
    public class BaseDataObject
    {
        public string Id { get; set; }

        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}