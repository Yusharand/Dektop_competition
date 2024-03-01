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
    
    public partial class Combat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Combat()
        {
            this.Competition_Detail = new HashSet<Competition_Detail>();
        }
    
        public int ID_Combat { get; set; }
        public string Nom_Combat { get; set; }
        public Nullable<int> Points_Combattant1 { get; set; }
        public Nullable<int> Points_Combattant2 { get; set; }
        public Nullable<int> Avantages_Combattant1 { get; set; }
        public Nullable<int> Avantages_Combattant2 { get; set; }
        public Nullable<int> Penalites_Combattant1 { get; set; }
        public Nullable<int> Penalites_Combattant2 { get; set; }
        public string Duree_combat { get; set; }
        public Nullable<System.DateTime> Date_Heure { get; set; }
        public Nullable<int> ID_Combattant1 { get; set; }
        public Nullable<int> ID_Combattant2 { get; set; }
        public Nullable<int> ID_Categorie { get; set; }
        public Nullable<int> ID_Poule { get; set; }
        public string Tour_Match { get; set; }
        public Nullable<int> ID_Competition { get; set; }
        public Nullable<int> Sub_Combattant2 { get; set; }
        public Nullable<int> Sub_Combattant1 { get; set; }
        public string Victoire_Combattant1 { get; set; }
        public string Victoire_Combattant2 { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Competition Competition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competition_Detail> Competition_Detail { get; set; }
        public virtual Poule Poule { get; set; }
    }
}
