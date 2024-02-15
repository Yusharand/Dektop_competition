//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Combattant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Combattant()
        {
            this.Combats = new HashSet<Combat>();
            this.Combats1 = new HashSet<Combat>();
            this.Competition_Detail = new HashSet<Competition_Detail>();
        }
    
        public int ID_Combattant { get; set; }
        public string Club_Combattant { get; set; }
        public string Nom_Combattant { get; set; }
        public string Prenom_Combattant { get; set; }
        public string Genre_Combattant { get; set; }
        public System.DateTime Date_Naiss { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public double Poids { get; set; }
        public Nullable<int> ID_Categorie { get; set; }
        public Nullable<int> Pointspoules { get; set; }
        public Nullable<int> ID_Poule { get; set; }
        public string Index_Poule { get; set; }
        public Nullable<int> Points_Marque { get; set; }
        public Nullable<int> Points_Concede { get; set; }
        public Nullable<int> Avantage_Marque { get; set; }
        public Nullable<int> Avantage_Concede { get; set; }
        public Nullable<int> Penalite_Marque { get; set; }
        public Nullable<int> Penalite_Concede { get; set; }
        public Nullable<int> Sub_Marque { get; set; }
        public Nullable<int> Sub_Concede { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Combat> Combats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Combat> Combats1 { get; set; }
        public virtual Poule Poule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competition_Detail> Competition_Detail { get; set; }
    }
}
