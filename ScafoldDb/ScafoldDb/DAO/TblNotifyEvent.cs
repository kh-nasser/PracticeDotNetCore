using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ScafoldDb.DAO
{
    [Table("tblNotifyEvent", Schema = "notify")]
    [Index("BatchId", Name = "IX_batchId")]
    [Index("FkTblNotifyEventTypeTblNotifyEvent", Name = "IX_tblNotifyEvent_FK_tblNotifyEventType_tblNotifyEvent")]
    public partial class TblNotifyEvent
    {
        [Key]
        public long Id { get; set; }
        [Column("FK_tblAccount_tblNotifyEvent")]
        public long FkTblAccountTblNotifyEvent { get; set; }
        public Guid BatchId { get; set; }
        [Column("FK_tblNotifyEventType_tblNotifyEvent")]
        public byte FkTblNotifyEventTypeTblNotifyEvent { get; set; }
        [Column("FK_tblAccount_tblNotifyEvent_Reciever")]
        public long FkTblAccountTblNotifyEventReciever { get; set; }
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "text")]
        public string? Description { get; set; }

        [ForeignKey("FkTblAccountTblNotifyEvent")]
        [InverseProperty("TblNotifyEventFkTblAccountTblNotifyEventNavigations")]
        public virtual TblAccount FkTblAccountTblNotifyEventNavigation { get; set; } = null!;
        [ForeignKey("FkTblAccountTblNotifyEventReciever")]
        [InverseProperty("TblNotifyEventFkTblAccountTblNotifyEventRecieverNavigations")]
        public virtual TblAccount FkTblAccountTblNotifyEventRecieverNavigation { get; set; } = null!;
        [ForeignKey("FkTblNotifyEventTypeTblNotifyEvent")]
        [InverseProperty("TblNotifyEvents")]
        public virtual TblNotifyEventType FkTblNotifyEventTypeTblNotifyEventNavigation { get; set; } = null!;
    }
}
