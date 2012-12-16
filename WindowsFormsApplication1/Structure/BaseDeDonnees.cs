using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjetInterim.Structure
{
    class BaseDeDonnees
    {
        List<Mission> listMissions;
        List<EntrepriseCliente> listEntreprises;
        List<EmployeInterim> listEmployes;
        List<String> listCompetencesInterim;
        XDocument xmlBDD;

        public BaseDeDonnees()
        {
             xmlBDD = new XDocument(
                 new XElement("Racine",
                     new XElement("ListeClients",
                         new XElement("Client", 
                             new XAttribute("ID",0),
                             new XAttribute("Nom", "entreprisename"),
                             new XAttribute("Adresse", "entrepriseAdd"),
                             new XAttribute("Siret", "numerosiret"),
                             new XAttribute("ChiffreAffaire", 250000),
                             new XElement("Contact",
                                 new XAttribute("Nom", "Vincent"),
                                 new XAttribute("Prenom","francky"),
                                 new XAttribute("Adresse","AdresseFrack"),
                                 new XAttribute("NumeroTelephone", "0554474849")
                                    )
                                )
                             ),
                     new XElement("ListEmployes",
                        new XElement("Employe",
                            new XAttribute("ID",0),
                            new XAttribute("Nom", "Emploinom"),
                            new XAttribute("Prenom", "Emploprenom"),
                            new XAttribute("Adresse", "AdresseEmpl"),
                            new XAttribute("NumeroTelephone", "0654474849"),
                            new XElement("Tarif",
                                new XAttribute("Type", "Fixe"), 150),
                            new XElement("Liste_Competences")
                         )
                     ),
                    new XElement("ListMissions",
                        new XElement("Mission",
                            new XAttribute("ID",0),
                            new XAttribute("IDinterim",0),
                            new XAttribute("IDClient",0),
                            new XAttribute("DateDebut", 09-12-2012),
                            new XAttribute("DateFin", 11-12-2012),
                            new XElement("CompetencesNecessaires","null")
                       )
                   )         
               )
           );
           xmlBDD.Save(@"doc_kisu.xml");
        }
        
        public void main()
        {
            BaseDeDonnees test = new BaseDeDonnees();
        }

       /********************************************************************************
        *  PARTIE AJOUT XML                                                            *
        ********************************************************************************
        *                                                                              *
        * Fonctions d'ajout directement dans le fichier XML                            *
        ********************************************************************************/

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Ajout XML d'Employes Interimaires******************************************************************************************************************
       
        public void XAddInterim(EmployeInterim nouveau)
        {
            XElement repere = xmlBDD.Root.Elements("ListEmployes").Last();

            XElement requete = new XElement("Employe",
                            new XAttribute("ID", repere != null? Convert.ToInt32(repere.Attribute("id").Value) + 1 : 0),
                            new XAttribute("Nom", nouveau.getNom),
                            new XAttribute("Prenom", nouveau.getPrenom),
                            new XAttribute("Adresse", nouveau.getAddresse),
                            new XAttribute("NumeroTelephone", nouveau.getTel),
                            new XElement("Tarif",
                                new XAttribute("Type", nouveau.getTarifVariation ? "Variable" : "Fixe"), nouveau.getTarif),
                            new XElement("Liste_Competences")
                         );

      
            foreach (String e in nouveau.getCompetences)
            {
                XElement competence = new XElement("Competence", e);
                requete.Element("Liste_Competences").Add(competence);
            }
            repere.AddAfterSelf(requete);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ajout XML d'Entreprises Clientes********************************************************************************************************************

        public void XAddClient(EntrepriseCliente nouveau)
        {
            XElement repere = xmlBDD.Root.Elements("ListClients").Last();

            XElement requete = new XElement("Client",
                            new XAttribute("ID", repere != null ? Convert.ToInt32(repere.Attribute("id").Value) + 1 : 0),
                            new XAttribute("Nom", nouveau.getNom),
                            new XAttribute("Adresse", nouveau.getAdresse),
                            new XAttribute("Siret", nouveau.getSiret),
                            new XAttribute("ChiffreAffaire", nouveau.getAffaire),
                            new XElement("Contact",
                                new XAttribute("Nom", nouveau.getContact.getNom),
                                new XAttribute("Prenom",nouveau.getContact.getPrenom),
                                new XAttribute("Adresse",nouveau.getContact.getAddresse),
                                new XAttribute("NumeroTelephone", nouveau.getContact.getTel)
                            )
                         );

            repere.AddAfterSelf(requete);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ajout XML de Missions ******************************************************************************************************************************

        public int XGetEmployeID(EmployeInterim sujet) //Recherche de l'ID de l'employé alloué
        {
            IEnumerable<XElement> search = from d in xmlBDD.Root.Element("ListEmployes").Descendants() where d.Attribute("Nom").Value == sujet.getNom
                                           && d.Attribute("Prenom").Value == sujet.getPrenom && d.Attribute("Adresse").Value == sujet.getAddresse
                                           select d;
            return Convert.ToInt32(search.First().Attribute("ID").Value);
        }

        public int XGetEntrepriseID(EntrepriseCliente sujet) //Recherche de l'ID de l'entreprise allouée
        {
            IEnumerable<XElement> search = from d in xmlBDD.Root.Element("ListClients").Descendants()
                                           where d.Attribute("Siret").Value == sujet.getSiret
                                           select d;
            return Convert.ToInt32(search.First().Attribute("ID").Value);
        }

        public void XAddMission(Mission nouveau) //Ajout de la mission
        {
            XElement repere = xmlBDD.Root.Elements("ListMissions").Last();

            XElement requete = new XElement("Mission",
                            new XAttribute("ID", repere != null? Convert.ToInt32(repere.Attribute("id").Value) + 1 : 0),
                            new XAttribute("IDinterim", XGetEmployeID(nouveau.getEmploye)),
                            new XAttribute("IDClient", XGetEntrepriseID(nouveau.getEntreprise)),
                            new XAttribute("DateDebut", nouveau.getDebut.ToString("dd-MM-yyyy")),
                            new XAttribute("DateFin", nouveau.getFin.ToString("dd-MM-yyyy")),
                            nouveau.getTitre
                         );
            repere.AddAfterSelf(requete);
        }



       /********************************************************************************
        *  PARTIE EDITIONS XML                                                         *
        ********************************************************************************
        *                                                                              *
        * Fonctions d'éditions dans le fichier XML                                     *
        ********************************************************************************/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Edition XML d'Employes Interimaires******************************************************************************************************************


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Edition XML d'Entreprises Clientes*******************************************************************************************************************


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Edition XML de Missions******************************************************************************************************************************



      /********************************************************************************
       *  PARTIE RECHERCHE XML                                                        *
       ********************************************************************************
       *                                                                              *
       * Fonctions de Recherches dans le fichier XML                                  *
       ********************************************************************************/

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Recherche XML d'Employes Interimaires****************************************************************************************************************
           
            //--Recherche d'Employés par Identité--//
        public IEnumerable<XElement> XSearchEmploye_ParIdentite(String prenom, String nom, String adresse) //Les paramètres peuvent être nuls
        {
            
            IEnumerable<XElement> search = from d in xmlBDD.Root.Element("ListEmployes").Descendants() 
                                           where (d.Attribute("Nom").Value.Contains(nom) && nom != null) ||
                                           (d.Attribute("Prenom").Value.Contains(prenom) && prenom != null)||
                                           (d.Attribute("Adresse").Value.Contains(adresse) && adresse != null) 
                                           select d;
            return search;
        }

            //--Recherche d'Employés par Compétences--//
        public IEnumerable<XElement> XSearchEmploye_ParCompetences(List<String> ListComp, bool full) //Full => true : l'employé doit posséder toutes les compétences, false : il doit en posséder au moins une
        {

            IEnumerable<XElement> search;

            search = xmlBDD.Root.Element("ListEmployes").Descendants();
                
            foreach(XElement e in search )
            {
                bool conserve = full?true:false; //Recherche par élimination ou par sélection
                IEnumerable<XElement> search2 = from d in e.Element("Liste_Competences").Descendants() select d;
                foreach(XElement z in search2)
                {
                    if(full) //Si on veut vérifier qu'il possède absolument tous les attributs
                    {
                        if(!ListComp.Contains(z.Value)) conserve = false; //Si il ne possède pas un attribut, on le conserve pas
                    }
                    else //Si on veut vérifier qu'il en possède au moins un
                    {
                        if(ListComp.Contains(z.Value)) conserve = true; //Si il en possède un, on le conserve
                    }
                }
            }

            return search;
        }
            
            //--Recherche d'Employés par Compétences--//
        public IEnumerable<XElement> XSearchEmploye_ParTarif(int Tarif_b, int Tarif_h) //Recherche entre deux bornes
        {
            IEnumerable<XElement> search;

            if (Tarif_b != null && Tarif_h == null)
            {
                search = from d in xmlBDD.Root.Element("ListEmployes").Descendants()
                         where (Convert.ToInt32(d.Attribute("Tarif").Value) >= Tarif_b)
                         select d;
            }
            else if (Tarif_h != null && Tarif_b == null)
            {
                search = from d in xmlBDD.Root.Element("ListEmployes").Descendants()
                         where (Convert.ToInt32(d.Attribute("Tarif").Value) <= Tarif_h)
                         select d;
            }
            else
            {
                search = from d in xmlBDD.Root.Element("ListEmployes").Descendants()
                                               where (Convert.ToInt32(d.Attribute("Tarif").Value) >= Tarif_b) &&
                                               (Convert.ToInt32(d.Attribute("Tarif").Value) <= Tarif_h)
                                               select d;
            }   

            return search;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Recherche XML d'Entreprises Clientes*****************************************************************************************************************

            //--Recherche d'Entreprises par Nom et Adresse--//

        public IEnumerable<XElement> XSearchClient_ParIdentite(String Nom, String Adresse)
        {
            IEnumerable<XElement> search = from d in  xmlBDD.Root.Element("ListClients").Descendants()
                                               where d.Attribute("Nom").Value.Contains(Nom) &&
                                               d.Attribute("Adresse").Value.Contains(Adresse)
                                               select d;
            return search;
        }
            //--Recherche d'Entreprises par Siret--// 
        public IEnumerable<XElement> XSearchClient_ParSiret(String Siret)
        {
            IEnumerable<XElement> search = from d in xmlBDD.Root.Element("ListClients").Descendants()
                                           where d.Attribute("Siret").Value.Equals(Siret)
                                           select d;
            return search;
        }
            //--Recherche d'Entreprises par Chiffre d'affaire--// 

        public IEnumerable<XElement> XSearchClient_ParChiffreAffaire(int chiffre_b, int chiffre_h)
        {
            IEnumerable<XElement> search;
            if (chiffre_b != null && chiffre_h == null)
            {
                search = from d in xmlBDD.Root.Element("ListClients").Descendants()
                         where (Convert.ToInt32(d.Attribute("ChiffreAffaire").Value) >= chiffre_b)
                         select d;
            }
            else if (chiffre_h != null && chiffre_b == null)
            {
                search = from d in xmlBDD.Root.Element("ListClients").Descendants()
                         where (Convert.ToInt32(d.Attribute("ChiffreAffaire").Value) <= chiffre_h)
                         select d;
            }
            else
            {
                search = from d in xmlBDD.Root.Element("ListClients").Descendants()
                         where (Convert.ToInt32(d.Attribute("ChiffreAffaire").Value) >= chiffre_b) &&
                         (Convert.ToInt32(d.Attribute("ChiffreAffaire").Value) <= chiffre_h)
                         select d;
            }   
            return search;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  Recherche XML de Missions****************************************************************************************************************************

            //--Recherche de Missions par Dates--// 
        public IEnumerable<XElement> XSearchMission_ParDateAbsolue(DateTime date_d, DateTime date_f) //Mission intégralement déroulée durant période
        {
            IEnumerable<XElement> search;
            if (date_d != null && date_f == null) //Pas de date de fin renseignée
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateDebut").Value, "dd-MM-yyyy", null) >= date_d)
                         select d;
            }
            else if (date_d == null && date_f != null) //Pas de date de début renseignée
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateFin").Value, "dd-MM-yyyy", null) <= date_f)
                         select d;
            }
            else //Tout champs renseigné
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateDebut").Value, "dd-MM-yyyy", null) >= date_d) &&
                         (DateTime.ParseExact(d.Attribute("DateFin").Value, "dd-MM-yyyy", null) <= date_f)
                         select d;
            }
            return search;
        }

        public IEnumerable<XElement> XSearchMission_ParDateRelative(DateTime date_d, DateTime date_f) //Mission active durant période ou date ponctuelle
        {
            IEnumerable<XElement> search;
            if (date_d != null && date_f == null) //Pas de date de fin renseignée
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateDebut").Value, "dd-MM-yyyy", null) <= date_d) &&
                         (DateTime.ParseExact(d.Attribute("DateFin").Value, "dd-MM-yyyy", null) >= date_d)
                         select d;
            }
            else if (date_d == null && date_f != null) //Pas de date de début renseignée
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateDebut").Value, "dd-MM-yyyy", null) <= date_f) &&
                         (DateTime.ParseExact(d.Attribute("DateFin").Value, "dd-MM-yyyy", null) >= date_f)
                         select d;
            }
            else //Tout champs renseigné
            {
                search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                         where (DateTime.ParseExact(d.Attribute("DateDebut").Value, "dd-MM-yyyy", null) <= date_d) &&
                         (DateTime.ParseExact(d.Attribute("DateFin").Value, "dd-MM-yyyy", null) >= date_f)
                         select d;
            }
            return search;
        }

            //--Recherche de Missions par Entreprise assignée--//
        public IEnumerable<XElement> XSearchMission_ParIDEntreprise(int id)
        {
            IEnumerable<XElement> search;
            search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                     where (d.Attribute("IDClient").Value == Convert.ToString(id))
                     select d;
            return search;
        }
            //--Recherche de Missions par Employé assigné--//
        public IEnumerable<XElement> XSearchMission_ParIDEntreprise(int id)
        {
            IEnumerable<XElement> search;
            search = from d in xmlBDD.Root.Element("ListMissions").Descendants()
                     where (d.Attribute("IDInterim").Value == Convert.ToString(id))
                     select d;
            return search;
        }
            //--Recherche de Missions par Compétences--//
        public IEnumerable<XElement> XSearchMissions_ParCompetences(List<String> ListComp, bool full) //Full => true : la mission doit posséder toutes les compétences, false : elle doit en posséder au moins une
        {

            IEnumerable<XElement> search;

            search = xmlBDD.Root.Element("ListMissions").Descendants();

            foreach (XElement e in search)
            {
                bool conserve = full ? true : false; //Recherche par élimination ou par sélection
                IEnumerable<XElement> search2 = from d in e.Element("CompetencesNecessaires").Descendants() select d;
                foreach (XElement z in search2)
                {
                    if (full) //Si on veut vérifier qu'il possède absolument tous les attributs
                    {
                        if (!ListComp.Contains(z.Value)) conserve = false; //Si il ne possède pas un attribut, on le conserve pas
                    }
                    else //Si on veut vérifier qu'il en possède au moins un
                    {
                        if (ListComp.Contains(z.Value)) conserve = true; //Si il en possède un, on le conserve
                    }
                }
            }

            return search;
        }




        /********************************************************************************
         *  PARTIE RECHERCHE                                                            *
         ********************************************************************************
         *                                                                              *
         * Pour la recherche, certains attributs peuvent être nuls                      *
         ********************************************************************************/

        // Recherche de Missions ************************************************************************************************************************
        
        /*
        public List<Mission> SearchMission_ParTitre(String titre)
        {
               List<Mission> temp = new List<Mission>();
            
               foreach(Mission m in listMissions)
               {
                     if(m.getTitre.Contains(titre)) temp.Add(m);
               }
               return temp;
        }
        
        public List<Mission> SearchMission_ParDateAbs(Date date_d, Date date_f) //Recherche par date Absolue ( si la mission a commencé et/ou finie entre les deux bornes )
        {
            List<Mission> temp = new List<Mission>();
            
                foreach(Mission m in listMissions)
                {
                       if(date_d != null && date_f == null) //Date de fin non spécifiée // Missions postérieures à une date 
                        {
                            if(date_d <= m.getDebut) temp.Add(m);
                        }
                        else if(date_d == null && date_f != null) //Date de début non spécifiée // Missions antérieures à une date
                        {
                            if(date_f >= m.getFin) temp.Add(m);
                        }
                        else //Date entre deux bornes // Missions commencée et terminée
                       {
                            if(date_d <= m.getDebut && date_f >= m.getFin) temp.Add(m);
                       }
                }
            return temp;
        }

        public List<Mission> SearchMission_ParDateRel(Date date_d, Date date_f) //Recherche par date Relative ( si la mission était active à ce moment là )
        {
            List<Mission> temp = new List<Mission>();
            foreach(Mission m in listMissions)
                {
                       if(date_d != null && date_f == null)//Cas une seule variable( Mission en cours à ce moment? oui/non )
                       {
                           if(date_d > m.getDebut && date_d < m.getFin ) temp.Add(m);
                       }
                       else
                       {
                           if(  !(m.getFin < date_d && m.getDebut < date_d) && !(m.getFin > date_f && m.getDebut > date_f)) temp.Add(m);
                       }
                }
            return temp;

        }

        public List<Mission> SearchMission_Exacte(Date date_d, Date date_f) //Recherche par date Exacte
        {
            List<Mission> temp = new List<Mission>();
            foreach(Mission m in listMissions)
                {
                       if(date_d == m.getDebut && date_f == m.getFin) temp.Add(m);
                }
            return temp;

        }

        public List<Mission> SearchMission(String nom, Date date_d, Date date_f, String type_search) //Chacun des attributs peut être nul
        {
            List<Mission> temp = new List<Mission>();
            
            ////////////////////A COMPLETER///////////////////////
            return temp;
        }

        // Fin des fonctions recherche de Mission //////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Recherche de Personnes //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<VPersonne> SearchPersonne(String nom, String prenom, List<VPersonne> ListBDD)
        {
            List<VPersonne> temp = new List<VPersonne>();

            foreach (VPersonne e in ListBDD)
            {
                if (nom == null && prenom != null) //Prenom non renseigné
                {
                    if (e.getPrenom.Contains(prenom)) temp.Add(e);
                }
                else if (prenom == null && nom != null) //Nom non renseigné
                {
                    if (e.getNom.Contains(nom)) temp.Add(e);
                }
                else //Cas générique
                {
                    if (e.getPrenom.Contains(prenom) && e.getNom.Contains(nom)) temp.Add(e);
                }
                
            }
            return temp;
        }

        public List<EmployeInterim> SearchEmploye_ParIdentite(String nom, String prenom)
        {
            List<VPersonne> listOfPersonne = listEmployes.ConvertAll(x => (VPersonne)x);
            return SearchPersonne(nom,prenom,listOfPersonne).ConvertAll(x => (EmployeInterim)x);
        }

        public List<Contact> SearchContact_ParIdentite(String nom, String prenom)
        {
           // List<IPersonne> listOfPersonne = listContact.ConvertAll(x => (IPersonne)x);
            return null; //SearchPersonne(nom, prenom, listOfPersonne).ConvertAll(x => (Contact)x);
        }

        // Fin des fonctions recherche de Personnes /////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Recherche d'Employes //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  

        public void AddMission(String titre, Date datedebut, Date datefin)
        {
            Mission temp = new Mission(titre, datedebut, datefin);
            listMissions.Add(temp);
        }

        public void AddEntreprise(String titre, Date datedebut, Date datefin)
        {
            Mission temp = new Mission(titre, datedebut, datefin);
            listMissions.Add(temp);
        }
          */
    }
}
