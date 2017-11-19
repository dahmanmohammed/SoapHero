//-----------------------------------------------------------------------
// <copyright file="Palette.cs" company="Collège La Cité">
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
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Classe représentant une palette de tuiles constituant les éléments d'un monde.
    /// </summary>
    public class Palette
    {
        /// <summary>
        /// Palette des tuiles.
        /// </summary>
        private Texture2D tuiles;

        /// <summary>
        /// Largeur d'une tuile en pixels.
        /// </summary>
        private int largeurTuile;

        /// <summary>
        /// Hauteur d'une tuile en pixels.
        /// </summary>
        private int hauteurTuile;

        /// <summary>
        /// Constructeur paramétré initialisant la palette.
        /// </summary>
        /// <param name="tuiles">Texture de la palette.</param>
        /// <param name="largeurTuile">Largeur uniforme d'une tuile dans la palette, en pixels.</param>
        /// <param name="hauteurTuile">Hauteur uniforme d'une tuile dans la palette, en pixels.</param>
        public Palette(Texture2D tuiles, int largeurTuile, int hauteurTuile)
        {
            this.tuiles = tuiles;

            this.largeurTuile = largeurTuile;
            this.hauteurTuile = hauteurTuile;
        }

        /// <summary>
        /// Propriété gérant la texture contenant la palette de tuiles.
        /// </summary>
        /// <value>Texture contenant la palette de tuiles.</value>
        public Texture2D Tuiles     // accesseur pour tuiles
        {
            /// <summary>
            /// Retourne la texture de l'ensemble de la palette.
            /// </summary>
            get { return this.tuiles; }
        }

        /// <summary>
        /// Propriété retournant la largeur d'une tuile en pixels.
        /// </summary>
        /// <value>Largeur d'une tuile en pixels.</value>
        public int LargeurTuile     // accesseur pour largeurTuile
        {
            /// <summary>
            /// Retourne la largeur d'une tuile en pixels.
            /// </summary>
            get { return this.largeurTuile; }
        }

        /// <summary>
        /// Propriété retournant la hauteur d'une tuile en pixels.
        /// </summary>
        /// <value>Hauteur d'une tuile en pixels.</value>
        public int HauteurTuile     // accesseur pour hauteurTuile
        {
            /// <summary>
            /// Retourne la hauteur d'une tuile en pixels.
            /// </summary>
            get { return this.hauteurTuile; }
        }

        /// <summary>
        /// Propriété retournant le nombre de tuiles dans la palette, calculée selon la largeur et 
        /// hauteur de chacune.
        /// </summary>
        /// <value>Nombre de tuiles dans la palette.</value>
        public int NombreDeTuiles   // nombre de tuiles dans la palette
        {
            /// <summary>
            /// Calcule le nombre de tuiles dans la palette.
            /// </summary>
            get
            {
                int tuilesParRangee = this.tuiles.Width / this.LargeurTuile;   // nombre de tuiles dans une rangée de la palette
                int tuilesParColonne = this.tuiles.Height / this.HauteurTuile; // nombre de tuiles dans une colonne de la palette

                return tuilesParRangee * tuilesParColonne;
            }
        }

        /// <summary>
        /// Retourne un Rectangle aux coordonnées de la tuile indiquée en argument.
        /// </summary>
        /// <param name="tuileIdx">Numéro de la tuile à localiser dans la palette.</param>
        /// <returns>Rectangle des pixels dans la palette constituant la tuile indiquée.</returns>
        public Rectangle SourceRect(int tuileIdx)
        {
            int tuilesParRangee = this.tuiles.Width / this.LargeurTuile; // nombre de tuiles dans une rangée de la palette

            int paletteRow = tuileIdx / tuilesParRangee;            // rangée de la tuile visée
            int paletteCol = tuileIdx % tuilesParRangee;            // colonne de la tuile visée

            // Créer un Rectangle aux coordonnées et dimensions de la tuile dans la palette.
            Rectangle sourceRect = new Rectangle(paletteCol * this.LargeurTuile, paletteRow * this.HauteurTuile, this.LargeurTuile, this.HauteurTuile);

            return sourceRect;
        }

        /// <summary>
        /// Affiche à l'écran la tuile indiquée dans le rectangle destinataire fourni.
        /// </summary>
        /// <param name="tuileIdx">Numéro de la tuile de palette à afficher.</param>
        /// <param name="destRect">Coordonnées de la position d'affichage de la tuile à l'écran.</param>
        /// <param name="spriteBatch">Gestionnaire de mise en tampon d'affichage.</param>
        public void Draw(int tuileIdx, Rectangle destRect, SpriteBatch spriteBatch, Color? color)
        {
            spriteBatch.Draw(this.tuiles, destinationRectangle: destRect, sourceRectangle: this.SourceRect(tuileIdx), color: color);   // afficher la tuile
        }
    }
}
