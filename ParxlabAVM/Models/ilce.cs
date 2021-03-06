namespace ParxlabAVM.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ilce")]
    public partial class ilce
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ilce()
        {
            firma = new HashSet<firma>();
        }

        [Required]
        [StringLength(45)]
        public string ilceadi { get; set; }

        public int ilid { get; set; }

        public int ilceid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<firma> firma { get; set; }

        [JsonIgnore]
        public virtual il il { get; set; }
    }
}
