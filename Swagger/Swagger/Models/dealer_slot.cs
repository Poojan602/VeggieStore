namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.dealer slots")]
    public partial class dealer_slot
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dealer_ID { get; set; }

        [Key]
        [Column("Before/After", Order = 1)]
        [StringLength(45)]
        public string Before_After { get; set; }

        [Key]
        [Column(Order = 2)]
        public TimeSpan Time { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(45)]
        public string TimeSlot { get; set; }

        public virtual dealer dealer { get; set; }
    }
}
