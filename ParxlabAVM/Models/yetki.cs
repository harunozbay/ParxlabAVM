namespace ParxlabAVM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("yetki")]
    public partial class yetki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public yetki()
        {
            yetkili = new HashSet<yetkili>();
        }

        [Column("yetki")]
        [Required]
        [StringLength(45)]
        public string yetki1 { get; set; }

        public int yetkiid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yetkili> yetkili { get; set; }
    }
}
