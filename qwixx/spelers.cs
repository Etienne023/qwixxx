using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qwixx
{
    internal class Speler
    {
        public string Naam { get; set; }
        public Button SpelerButton { get; set; }
        // Add more properties as needed

        public Speler(string naam, Button spelerButton)
        {
            Naam = naam;
            SpelerButton = spelerButton;
            // Initialize other properties as needed
        }

        // Add methods specific to each player as needed
    }
}
