//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exam.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Ticket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Ticket()
        {
            this.Tbl_Ticket1 = new HashSet<Tbl_Ticket>();
        }
    
        public System.Guid PK_TicketId { get; set; }
        public Nullable<System.Guid> FK_TicketParent { get; set; }
        public string TicketText { get; set; }
        public string TicketSubject { get; set; }
        public bool IsAnswer { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public Nullable<System.Guid> FK_UserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Ticket> Tbl_Ticket1 { get; set; }
        public virtual Tbl_Ticket Tbl_Ticket2 { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
    }
}
