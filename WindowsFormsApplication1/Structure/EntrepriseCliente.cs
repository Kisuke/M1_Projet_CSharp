using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterim.Structure
{
    class EntrepriseCliente
    {
        Contact contactEntreprise;
        int ChiffreAffaire;
        String sNomEntreprise;
        String sAdresse;
        String sNumeroSiret;

        //Getteur
        public Contact getContact { get { return contactEntreprise; } }
        public String getNom { get { return sNomEntreprise; } }
        public String getAdresse { get { return sAdresse; } }
        public String getSiret { get { return sNumeroSiret; } }
        public int getAffaire { get { return ChiffreAffaire; } }
        
        //Constructeur
        public EntrepriseCliente()
        {
            contactEntreprise = null;
            sNomEntreprise = null;
            sAdresse = null;
            sNumeroSiret = null;
        }

        public EntrepriseCliente(String nom, String adresse, String siret, Contact contact)
        {
            contactEntreprise = contact;
            sNomEntreprise = nom;
            sAdresse = adresse;
            sNumeroSiret = siret;
        }

        //Methodes
        

    }
}
