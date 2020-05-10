namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.dealer area")]
    public partial class dealer_area
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Area { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int D_ID { get; set; }

        public virtual dealer dealer { get; set; }
    }
}
