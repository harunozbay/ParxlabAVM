namespace ParxlabAVM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("parxlab.firma")]
    public partial class firma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public firma()
        {
            anatablo = new HashSet<anatablo>();
            parkyeri = new HashSet<parkyeri>();
        }

        [Required]
        [StringLength(45)]
        public string firmaadi { get; set; }

        public int ilid { get; set; }

        public int ilceid { get; set; }

        public int yetkiliid { get; set; }

        public int id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anatablo> anatablo { get; set; }

        public virtual il il { get; set; }

        public virtual ilce ilce { get; set; }

        public virtual yetkili yetkili { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parkyeri> parkyeri { get; set; }
    }
}
