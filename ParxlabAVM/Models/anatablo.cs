namespace ParxlabAVM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("parxlab.anatablo")]
    public partial class anatablo
    {
        public int cihazid { get; set; }

        public int parkid { get; set; }

        public int firmaid { get; set; }

        public DateTime? giriszamani { get; set; }

        public DateTime? cikiszamani { get; set; }

        [Required]
        [StringLength(45)]
        public string aracplakasi { get; set; }

        [Required]
        [StringLength(45)]
        public string kullaniciid { get; set; }

        public int anatabloid { get; set; }

        public virtual firma firma { get; set; }

        public virtual kullanici kullanici { get; set; }

        public virtual parkyeri parkyeri { get; set; }

        public virtual cihaz cihaz { get; set; }
    }
}
