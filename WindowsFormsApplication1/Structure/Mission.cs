using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterim.Structure
{
    class Mission
    {
        DateTime dateDateDebut;
        DateTime dateDateFin;
        String sTitre;
        EmployeInterim emploEmployeAssigne;
        EntrepriseCliente entreEntrepriseAssigne;


        //Getteurs :
        public String getTitre { get { return sTitre; } }
        public DateTime getDebut { get { return dateDateDebut; } }
        public DateTime getFin { get { return dateDateFin; } }
        public EmployeInterim getEmploye { get { return emploEmployeAssigne; } }
        public EntrepriseCliente getEntreprise { get { return entreEntrepriseAssigne; } }
        

        public Mission(String titre, DateTime debut, DateTime fin)
        {
            sTitre = titre;
            dateDateDebut = debut;
            dateDateFin = fin;
            emploEmployeAssigne = null;
        }

    }
}
