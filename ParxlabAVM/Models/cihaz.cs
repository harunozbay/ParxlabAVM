namespace ParxlabAVM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cihaz")]
    public partial class cihaz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cihaz()
        {
            anatablo = new HashSet<anatablo>();
        }

        public short cihazdurumu { get; set; }

        public double? enlem { get; set; }

        public double? boylam { get; set; }

        public int parkid { get; set; }

        public int cihazid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<anatablo> anatablo { get; set; }

        public virtual parkyeri parkyeri { get; set; }
    }
}
