using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dog.Models
{
    public class Users
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsersID { get; set; }

        //[Display(Name = "訂單ID")]
        //public int OrdersID { get; set; }
        //[JsonIgnore]
        //[ForeignKey("OrdersID")]
        //public virtual Orders Orders { get; set; }

        [Display(Name = "使用者編號")]
        [MaxLength(20)]
        public string Number { get; set; }

        [Display(Name = "使用者角色")]
        public Role Roles { get; set; }

        [Display(Name = "名稱")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Display(Name = "Line大頭照")]
        [MaxLength(255)]
        public string LinePicUrl { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "{0}必填")]//[Required]必須有值 錯誤訊息佔位符 {0}表示「必須填寫」
        public string Phone { get; set; }

        [Display(Name = "註冊日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}