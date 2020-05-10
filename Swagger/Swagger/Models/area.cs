namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.area")]
    public partial class area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Area_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Area_Name { get; set; }

        public int Shipping_Charge { get; set; }

        public int Min_Amount { get; set; }
    }
}
