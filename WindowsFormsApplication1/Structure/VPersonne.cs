using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterim
{
    public abstract class VPersonne
    {
        String sPrenom;
        String sNom;
        String sAdresse;
        int iNumeroTelephone;
        int iAge;

        //Getteurs :
        public String getNom { get { return sNom; } }
        public String getPrenom { get { return sPrenom; } }
        public String getAddresse { get { return sAdresse; } }
        public int getTel { get { return iNumeroTelephone; } }
        public int getAge { get { return iAge; } }
    }
}
