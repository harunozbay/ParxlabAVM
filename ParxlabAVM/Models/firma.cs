namespace ParxlabAVM.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("firma")]
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

        [Required]
        [StringLength(128)]
        public string yetkilikullaniciid { get; set; }

        public int firmaid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<anatablo> anatablo { get; set; }

        [JsonIgnore]
        public virtual kullanici AspNetUsers { get; set; }

        [JsonIgnore]
        public virtual il il { get; set; }

        [JsonIgnore]
        public virtual ilce ilce { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<parkyeri> parkyeri { get; set; }
    }
}
