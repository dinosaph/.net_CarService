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
    public partial class Imagine
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titlu { get; set; }
        [DataMember]
        public string Descriere { get; set; }
        [DataMember]
        public System.DateTime Data { get; set; }
        [DataMember]
        public byte[] Foto { get; set; }
    
        [DataMember]
        public virtual DetaliuComanda DetaliuComanda { get; set; }
    }
}
