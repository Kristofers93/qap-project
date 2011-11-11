using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    public class Parameters : Structures.IParameters
    {
        public int insects { get; set; }
        public int iterations { get; set; }
        public int reportEveryIterations { get; set; }
        public int sites { get; set; }
        public int beesRecruitedForBestSites { get; set; }
        //TODO Dopisać pozostałe parametry charakterystyczne dla algorytmu

        public Parameters()
        {
            //Tu można zmieniać i dopisać domyślne parametry dla danego algorytmu
            insects = 1000;
            iterations = 1000;
            reportEveryIterations = 10;
            sites = 100;
            beesRecruitedForBestSites = 500;
        }
    }
}
