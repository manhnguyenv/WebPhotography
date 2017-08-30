namespace WebSavingPicture.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        public int PictureId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Picture name")]
        public string PictureName { get; set; }

        [StringLength(100)]
        [Display(Name ="Description")]
        [DataType(DataType.MultilineText)]
        public string PictureDescription { get; set; }
            
        public string PictureType { get; set; }      
        public byte[] PictureBin { get; set; }

        [Column(TypeName = "date")]
        public DateTime PictureCreationTime { get; set; }
    }
}
