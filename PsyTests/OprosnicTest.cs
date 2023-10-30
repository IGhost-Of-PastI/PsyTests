using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PsyTests
{
    struct Keys
    {
        public string Question;
        public string LinkedShkala;
    }
    internal class OprosnicTest
    {
        public OprosnicTest(string name, string opisanie, string pathToImage, string algorith, List<string> shakali, List<Keys> listOfKeys)
        {
            Name = name;
            Opisanie = opisanie;
            PathToImg = pathToImage;
            Algorithm = algorith;
            Shakli = shakali;
            ListOfKeys = listOfKeys;
        }
        public string Name { get; set; }
        public string Opisanie { get; set; }
        public string PathToImg { get; set; }
        public string Algorithm { get; set; }

        List<string> Shakli;
        List<Keys> ListOfKeys;


        

    }
}
