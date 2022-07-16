using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScafoldDb.DAO
{
    [Table("tblNotifyEventType", Schema = "notify")]
    public partial class TblNotifyEventType
    {
        public TblNotifyEventType()
        {
            TblNotifyEvents = new HashSet<TblNotifyEvent>();
        }

        [Key]
        public byte Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(100)]
        public string Description { get; set; } = null!;

        [InverseProperty("FkTblNotifyEventTypeTblNotifyEventNavigation")]
        public virtual ICollection<TblNotifyEvent> TblNotifyEvents { get; set; }
    }
}
