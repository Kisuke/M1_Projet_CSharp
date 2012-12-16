using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterim.Structure
{
    class Date
    {
        int jour;
        int mois;
        int annee;

        public Date(int jour_, int mois_, int annee_)
        {
            if (jour_ < 1 || jour_ > 31 || mois_ < 1 || mois_ > 12 || annee < 1970)
            {
                Console.WriteLine("Erreur");
            }
            else
            {
                jour = jour_;
                mois = mois_;
                annee = annee_;
            }
        }

        //Surcharge d'opérateurs
        public static bool operator >(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return false;
                    else return a.jour > b.jour ? true : false;
                }
                else return a.mois > b.mois ? true : false;
            }
            else return a.annee > b.annee ? true : false;
        }


        public static bool operator <(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return false;
                    else return a.jour < b.jour ? true : false;
                }
                else return a.mois < b.mois ? true : false;
            }
            else return a.annee < b.annee ? true : false;
        }


        public static bool operator >=(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return true;
                    else return a.jour > b.jour ? true : false;
                }
                else return a.mois > b.mois ? true : false;
            }
            else return a.annee > b.annee ? true : false;
        }

        public static bool operator <=(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return true;
                    else return a.jour < b.jour ? true : false;
                }
                else return a.mois < b.mois ? true : false;
            }
            else return a.annee < b.annee ? true : false;
        }

        public static bool operator ==(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public static bool operator !=(Date a, Date b)
        {
            if (a.annee == b.annee)
            {
                if (a.mois == b.mois)
                {
                    if (a.jour == b.jour) return false;
                    else return true;
                }
                else return true;
            }
            else return true;
        }
        
        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(Object d)
        {
            return false;
        }
    }

        
}
    //Fin des surcharges d'opérateur

