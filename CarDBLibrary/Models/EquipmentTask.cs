using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDBLibrary.Models
{
    // Задачи навыполнение загрузки
    public class EquipmentTask : BaseModel
    {
        public DateTime? Since { get; set; }
        public DateTime? NextSince { get; set; }
        public ResultEquipmentTaskStep Result { get; set; } = ResultEquipmentTaskStep.Start;
        [StringLength(50)]
        public string Cursor { get; set; }
        public enum ResultEquipmentTaskStep
        {
            Start,
            Success,
            Error
        }
    }
}
