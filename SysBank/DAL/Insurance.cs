//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysBank.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Insurance
    {
        public int Id { get; set; }
        public int Document_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Insurance_Type_Id { get; set; }
        public string Details { get; set; }
        public System.DateTime Start_Date { get; set; }
        public System.DateTime End_Date { get; set; }
        public double Cost { get; set; }
    
        public virtual Application_Document Application_Document { get; set; }
        public virtual Insurance_Type Insurance_Type { get; set; }
    }
}
