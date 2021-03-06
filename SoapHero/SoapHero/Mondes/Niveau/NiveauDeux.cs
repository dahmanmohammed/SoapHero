﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapHero
{
    class NiveauDeux : MondeDeTuiles
    {
        /// <summary>
        /// Palette de tuiles constituant le monde.
        /// </summary>
        private static Palette paletteDeTuiles = null;

        /// <summary>
        /// Mappe monde : chaque valeur du tableau correspond à l'index d'une tuile dans le monde.
        /// </summary>
        private static int[,] mappeMonde = 
        {

        };

        /// <summary>
        /// Palette de tuiles à utiliser pour dessiner le monde.
        /// </summary>
        /// <value>Palette de tuiles.</value>
        protected override Palette PaletteDeTuiles    // palette des tuiles constituant le monde
        {
            /// <summary>
            /// Accesseur retournant la palette de tuiles.
            /// </summary>
            get
            {
                return paletteDeTuiles;
            }
        }

        /// <summary>
        /// Matrice de numéros de tuiles à utiliser pour dessiner le monde.
        /// </summary>
        /// <value>Matrice de numéros de tuiles.</value>
        protected override int[,] MappeMonde          // tableau d'index des tuiles du monde
        {
            /// <summary>
            /// Accesseur retournant la matrice de numéros de tuiles.
            /// </summary>
            get
            {
                return mappeMonde;
            }
        }
    }
}
