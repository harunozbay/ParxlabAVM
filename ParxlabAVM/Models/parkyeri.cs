namespace ParxlabAVM.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("parkyeri")]
    public partial class parkyeri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parkyeri()
        {
            anatablo = new HashSet<anatablo>();
            cihaz = new HashSet<cihaz>();
        }

        [Required]
        [StringLength(45)]
        public string parkadi { get; set; }

        public int firmaid { get; set; }

        [Key]
        public int parkid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<anatablo> anatablo { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cihaz> cihaz { get; set; }

        [JsonIgnore]
        public virtual firma firma { get; set; }
    }
}
