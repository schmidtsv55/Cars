using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public void SetActive() 
        {
            this.Status = CommonTypes.Status.Active;
        }
        public void SetDeactive()
        {
            this.Status = CommonTypes.Status.Deactive;
        }
        public void SetUpdating()
        {
            this.Status = CommonTypes.Status.Updating;
        }
        public bool IsActive 
        {
            get
            {
                return CommonTypes.Status.IsActive(this.Status);
            }
        }
        public bool IsDeactive
        {
            get
            {
                return CommonTypes.Status.IsDeactive(this.Status);
            }
        }
        public bool IsUpdating
        {
            get
            {
                return CommonTypes.Status.IsUpdating(this.Status);
            }
        }
    }
}
