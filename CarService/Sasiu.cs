//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace CarService
{
    using System;
    using System.Collections.Generic;
    
    [DataContract(IsReference = true)]
    public partial class Sasiu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sasiu()
        {
            this.Masini = new List<Auto>();
        }
    
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CodSasiu { get; set; }
        [DataMember]
        public string Denumire { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [DataMember]
        public virtual List<Auto> Masini { get; set; }
    }
}
