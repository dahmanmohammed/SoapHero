//-----------------------------------------------------------------------
// <copyright file="MondeDeTuiles.cs" company="Collège La Cité">
//     Copyright (c) Marco Lavoie, 2010-2016. Tous droits réservés.
// </copyright>
//
// <summary>
// L'utilisation de ce matériel pédagogique (présentations, code source et 
// autres) avec ou sans modifications, est permise en autant que les conditions
// suivantes soient respectées:
//
// 1. La diffusion du matériel doit se limiter à un intranet dont l'accès est
//    imité aux étudiants inscrits à un cours exploitant le dit matériel. IL
//    EST STRICTEMENT INTERDIT DE DIFFUSER CE MATÉRIEL LIBREMENT SUR 
//    INTERNET.
// 2. La redistribution des présentations contenues dans le matériel 
//    pédagogique est autorisée uniquement en format Acrobat PDF et sous
//    restrictions stipulées à la condition #1. Le code source contenu dans
//    le matériel pédagogique peut cependant être redistribué sous sa forme 
//    originale, en autant que la condition #1 soit également respectée.
// 3. Le matériel diffusé doit contenir intégralement la mention de droits
//    d'auteurs ci-dessus, la notice présente ainsi que la décharge ci-dessous.
// 
// CE MATÉRIEL PÉDAGOGIQUE EST DISTRIBUÉ "TEL QUEL" PAR L'AUTEUR, SANS AUCUNE 
// GARANTIE EXPLICITE OU IMPLICITE. L'AUTEUR NE PEUT EN AUCUNE CIRCONSTANCE
// ÊTRE TENU RESPONSABLE DE DOMMAGES DIRECTS, INDIRECTS, CIRCONSTENTIELS OU
// EXEMPLAIRES. TOUTE VIOLATION DE DROITS D'AUTEUR OCCASIONNÉ PAR L'UTILISATION
// DE CE MATÉRIEL PÉDAGOGIQUE EST PRIS EN CHARGE PAR L'UTILISATEUR DU DIT 
// MATÉRIEL.
// 
// En utilisant ce matériel pédagogique, vous acceptez implicitement les
// conditions et la décharge exprimés ci-dessus.
// </summary>
//-----------------------------------------------------------------------

namespace SoapHero
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Classe abstraite représentant un monde constitué d'un tableau en deux dimensions de tuiles
    /// de tailles uniformes. Ces tuiles sont extraites d'une palette (instance de Palette).
    /// </summary>
    public abstract class MondeDeTuiles : Monde
    {
        /// <summary>
        /// Propriété retournant la largeur du monde en pixels.
        /// </summary>
        /// <value>Largeur du monde en pixels.</value>
        public override int Largeur
        {
            /// <summary>
            /// Accesseur calculant la largeur du monde en pixels.
            /// </summary>
            get
            {
                return this.MappeMonde.GetLength(1) * this.PaletteDeTuiles.LargeurTuile;
            }
        }

        /// <summary>
        /// Propriété retournant la hauteur du monde en pixels.
        /// </summary>
        /// <value>Hauteur du monde en pixels.</value>
        public override int Hauteur
        {
            /// <summary>
            /// Accesseur calculant la largeur du monde en pixels.
            /// </summary>
            get
            {
                return this.MappeMonde.GetLength(0) * this.PaletteDeTuiles.HauteurTuile;
            }
        }

        /// <summary>
        /// Propriété retournant la palette de tuiles à utiliser pour dessiner le monde.
        /// </summary>
        /// <value>Palette de tuile à utiliser pour dessiner le monde.</value>
        protected abstract Palette PaletteDeTuiles     // palette des tuiles constituant le monde
        {
            /// <summary>
            /// Accesseur à surcharger.
            /// </summary>
            get;
        }

        /// <summary>
        /// Propriété retournant la matrice de numéros de tuiles pour dessiner le monde.
        /// </summary>
        /// <value>Matrice de munéros de tuiles à utiliser pour dessiner le monde.</value>
        protected abstract int[,] MappeMonde           // tableau d'index des tuiles du monde
        {
            /// <summary>
            /// Accesseur à surcharger.
            /// </summary>
            get;
        }

        /// <summary>
        /// Affiche à l'écran la partie de la mappe monde visible par la camera fournie.
        /// </summary>
        /// <param name="cameraRect">Rectangle de caméra.</param>
        /// <param name="spriteBatch">Gestionnaire de mise en tampon d'affichage.</param>
        public override void Draw(Rectangle cameraRect, SpriteBatch spriteBatch, Color? color)
        {
            // Initialiser le rectangle de destination aux dimensions d'une tuile
            Rectangle destRect = new Rectangle(0, 0, this.PaletteDeTuiles.LargeurTuile, this.PaletteDeTuiles.HauteurTuile);

            // Afficher une rangée à la fois
            for (int row = 0; row < this.MappeMonde.GetLength(0); row++)
            {
                for (int col = 0; col < this.MappeMonde.GetLength(1); col++)
                {
                    // Calculer la position de la tuile à l'écran
                    destRect.X = col * this.PaletteDeTuiles.LargeurTuile;
                    destRect.Y = row * this.PaletteDeTuiles.HauteurTuile;

                    // Afficher la tuile si elle est visible
                    if (destRect.Intersects(cameraRect))
                    {
                        destRect.Offset(-cameraRect.X, -cameraRect.Y); // Décaler la destination en fonction de la caméra
                        this.PaletteDeTuiles.Draw(this.MappeMonde[row, col], destRect, spriteBatch, color);
                    }
                }
            }
        }
    }
}
