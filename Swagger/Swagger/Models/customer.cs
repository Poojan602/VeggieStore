namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.customer")]
    public partial class customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string C_Name { get; set; }

        [Required]
        [StringLength(25)]
        public string C_Email { get; set; }

        [Required]
        [StringLength(15)]
        public string C_Phone { get; set; }

        [Required]
        [StringLength(45)]
        public string C_Address { get; set; }

        [Required]
        [StringLength(45)]
        public string C_Area { get; set; }

        public int C_Zipcode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Reg_Date { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [Required]
        [StringLength(25)]
        public string C_Pass { get; set; }
    }
}
