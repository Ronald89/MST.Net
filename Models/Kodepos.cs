//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MST.Net.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(KodeposMetaData))]
    public partial class Kodepos
    {
        public int Id { get; set; }
        public Nullable<int> kode { get; set; }
        public string kelurahan { get; set; }
        public string kecamatan { get; set; }
        public Nullable<bool> type_kab { get; set; }
        public string kabupaten { get; set; }
        public string provinsi { get; set; }
    }
}
