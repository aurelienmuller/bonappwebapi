//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BonAppAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userfavorite
    {
        public int userfavorite_id { get; set; }
        public int userid_fav { get; set; }
        public string recipeid_fav { get; set; }
    
        public virtual recipe recipe { get; set; }
        public virtual user user { get; set; }
    }
}
