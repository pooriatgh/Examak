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
    
    public partial class Tbl_Answer
    {
        public System.Guid PK_AnswerId { get; set; }
        public string UserAnswerText { get; set; }
        public string UserAnswerFileUrl { get; set; }
        public System.Guid FK_UserId { get; set; }
        public string TeacherAnswerText { get; set; }
        public string TeacherAnswerFile { get; set; }
        public System.DateTime AnswerDatetime { get; set; }
        public Nullable<System.DateTime> TeacherReplayDatetime { get; set; }
        public System.Guid FK_QuestionId { get; set; }
    
        public virtual Tbl_Question Tbl_Question { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
    }
}
