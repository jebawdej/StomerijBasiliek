//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StomerijBasiliek
{
    using System;
    using System.Collections.Generic;
    
    public partial class Werkorder
    {
        public System.Guid Id { get; set; }
        public System.Guid KlantID { get; set; }
        public string Bon { get; set; }
        public int Werkzaamheden { get; set; }
        public int Voorkeur { get; set; }
        public int Status { get; set; }
        public Nullable<decimal> Prijs { get; set; }
        public string Commentaar { get; set; }
        public Nullable<System.DateTime> DatumStart { get; set; }
        public Nullable<System.DateTime> DatumLaatstAangepast { get; set; }
        public Nullable<System.DateTime> DatumTijdGereed { get; set; }
    
        public virtual Klant Klant { get; set; }
    }
}
