using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetInterim
{
    class EmployeInterim : VPersonne
    {
        int iTarifjournalier;
        bool bTarifFixe;
        List<String> listeCompetences;

        //getteurs
        public int getTarif { get { return this.iTarifjournalier; } }
        public bool getTarifVariation { get { return this.bTarifFixe; } }
        public List<String> getCompetences { get { return this.listeCompetences; } }
    }
}
