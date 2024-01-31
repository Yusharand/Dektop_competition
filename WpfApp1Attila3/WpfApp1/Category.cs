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
    using System.Linq;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Combattants = new HashSet<Combattant>();
            this.Competition_Detail = new HashSet<Competition_Detail>();
            this.Poules = new HashSet<Poule>();
        }
    
        public int ID_Categorie { get; set; }
        public string Nom_Categorie { get; set; }
        private Competition_JJBEntities context = new Competition_JJBEntities();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Combattant> Combattants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competition_Detail> Competition_Detail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Poule> Poules { get; set; }

        public void CreerPoules(int nombrePoules)
        {
            Poules = new List<Poule>();

            // Assurez-vous que la liste de combattants n'est pas null et contient des éléments
            if (Combattants != null && Combattants.Any())
            {
                int combattantsParPoule = Combattants.Count / nombrePoules;

                for (int i = 0; i < nombrePoules; i++)
                {
                    Poule poule = new Poule
                    {
                        Combattants = new List<Combattant>()
                    };

                    foreach (Combattant combattant in Combattants.Skip(i * combattantsParPoule).Take(combattantsParPoule))
                    {
                        // Attribuer l'ID de la poule au combattant
                        combattant.ID_Poule = poule.ID_Poule;
                        poule.Combattants.Add(combattant);
                    }

                    Poules.Add(poule);
                }
                context.SaveChanges();
            }
        }
    }
}