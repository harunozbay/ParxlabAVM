namespace ParxlabAVM.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("anatablo")]
    public partial class anatablo
    {
        public int firmaid { get; set; }

        public int parkid { get; set; }

        public int cihazid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? giriszamani { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? cikiszamani { get; set; }

        [StringLength(45)]
        public string aracplakasi { get; set; }

        [StringLength(128)]
        public string kullaniciid { get; set; }

        public int anatabloid { get; set; }

        [JsonIgnore]
        public virtual kullanici kullanici { get; set; }

        [JsonIgnore]
        public virtual firma firma { get; set; }

        [JsonIgnore]
        public virtual parkyeri parkyeri { get; set; }

        [JsonIgnore]
        public virtual cihaz cihaz { get; set; }
    }
}
