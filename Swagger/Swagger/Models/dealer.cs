namespace Swagger.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test.dealer")]
    public partial class dealer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dealer()
        {
            dealer_area = new HashSet<dealer_area>();
            dealer_slots = new HashSet<dealer_slot>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int D_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string D_Name { get; set; }

        [Required]
        [StringLength(45)]
        public string D_Address { get; set; }

        [Required]
        [StringLength(15)]
        public string D_Area { get; set; }

        [Required]
        [StringLength(10)]
        public string D_Zipcode { get; set; }

        [Required]
        [StringLength(15)]
        public string D_Phno { get; set; }

        [Required]
        [StringLength(25)]
        public string D_Email { get; set; }

        [Required]
        [StringLength(25)]
        public string D_Pass { get; set; }

        [Required]
        [StringLength(15)]
        public string D_Subscription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dealer_area> dealer_area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dealer_slot> dealer_slots { get; set; }
    }
}
