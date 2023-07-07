using System;
using System.Collections.Generic;

namespace Storage.Api.Models
{
    public partial class StorageInfo
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
