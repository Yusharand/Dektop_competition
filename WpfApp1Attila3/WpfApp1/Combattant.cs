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
        public int ID_Combattant { get; set; }
        public Nullable<int> ID_Competition { get; set; }
        public Nullable<int> ID_Club { get; set; }
        public string Nom_Club { get; set; }
        public Nullable<int> Position_poule { get; set; }
        public Nullable<int> Victoire_demi { get; set; }
        public Nullable<int> Victoire_aller { get; set; }
        public Nullable<int> Victoire_retour { get; set; }
        public string Victoire_finale { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Club Club { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual Poule Poule { get; set; }
    }
}
