namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.subscription")]
    public partial class subscription
    {
        [Key]
        [StringLength(20)]
        public string S_Name { get; set; }

        public int S_Duration { get; set; }

        public int S_Amount { get; set; }
    }
}
