using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScafoldDb.DAO
{
    [Table("tblAccount")]
    public partial class TblAccount
    {
        public TblAccount()
        {
            TblNotifyEventFkTblAccountTblNotifyEventNavigations = new HashSet<TblNotifyEvent>();
            TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations = new HashSet<TblNotifyEvent>();
        }

        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        [InverseProperty("FkTblAccountTblNotifyEventNavigation")]
        public virtual ICollection<TblNotifyEvent> TblNotifyEventFkTblAccountTblNotifyEventNavigations { get; set; }
        [InverseProperty("FkTblAccountTblNotifyEventRecieverNavigation")]
        public virtual ICollection<TblNotifyEvent> TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations { get; set; }
    }
}
