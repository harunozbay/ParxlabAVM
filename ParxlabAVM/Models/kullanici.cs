namespace ParxlabAVM.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kullanici")]
    public partial class kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kullanici()
        {
            anatablo = new HashSet<anatablo>();
        }

        [StringLength(45)]
        public string kullaniciid { get; set; }

        [Required]
        [StringLength(45)]
        public string sifre { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anatablo> anatablo { get; set; }
    }
}
