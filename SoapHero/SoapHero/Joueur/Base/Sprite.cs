//-----------------------------------------------------------------------
// <copyright file="Sprite.cs" company="Collège La Cité">
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

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Classe abstraite de base des sprites du jeu.
    /// </summary>
    public abstract class Sprite
    {
        /// <summary>
        /// Position du sprite en 2D.
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Rectangle confinant les mouvements du sprite.
        /// </summary>
        private Rectangle boundsRect;

        /// <summary>
        /// Constructeur paramétré recevant la position du sprite.
        /// </summary>
        /// <param name="x">Position horizontale initiale du sprite.</param>
        /// <param name="y">Position verticale initiale du sprite.</param>
        public Sprite(float x, float y)
        {
            this.Position = new Vector2(x, y);
        }

        /// <summary>
        /// Constructeur paramétré recevant la position du sprite. On invoque l'autre constructeur.
        /// </summary>
        /// <param name="position">Position initiale du sprite.</param>
        public Sprite(Vector2 position)
            : this(position.X, position.Y)
        {
        }

        /// <summary>
        /// Propriété abstraite pour manipuler la texture du sprite. Doit être
        /// surchargée dans les classes dérivées afin de manipuler une Texture2D.
        /// </summary>
        /// <value>Texture représentant le sprite.</value>
        public abstract Texture2D Texture
        {
            get;
        }

        /// <summary>
        /// Propriété retournant la largeur du sprite en pixels et à
        /// surchargér dans les classes dérivées afin de manipuler une Texture2D.
        /// </summary>
        /// <value>Largeur de la texture du sprite en pixels.</value>
        public virtual int Width
        {
            get { return this.Texture.Width; }
        }

        /// <summary>
        /// Propriété retournant la hauteur du sprite en pixels et à
        /// surchargér dans les classes dérivées afin de manipuler une Texture2D.
        /// </summary>
        /// <value>Hauteur de la texture du sprite en pixels.</value>
        public virtual int Height
        {
            get { return this.Texture.Height; }
        }

        /// <summary>
        /// Propriété retournant la position du sprite dans le monde.
        /// </summary>
        /// <value>Position du sprite.</value>
        public Vector2 Position             // accesseur pour position
        {
            get
            {
                return this.position;
            }

            // Le setter s'assure que la nouvelle position n'excède pas les bornes de mouvements
            // si elles sont fournies.
            set
            {
                this.position = value;

                // Limiter le mouvement si un boundsRect est fourni.
                this.ClampPositionToBoundsRect();
            }
        }

        /// <summary>
        /// Propriété retournant les limites de déplacement du sprite dans le monde.
        /// </summary>
        /// <value>Rectangle bornant les déplacement du sprite, en coordonnées du monde.</value>
        public Rectangle BoundsRect          // accesseur pour boundsRect
        {
            get
            {
                return this.boundsRect;
            }

            // Le setter s'assurer que la position courante est confinée au nouvelles bornes.
            set
            {
                this.boundsRect = value;
                this.Position = this.position;         // exploiter le setter de position 
            }
        }

        /// <summary>
        /// Fonction membre abstraite (doit être surchargée) mettant à jour le sprite.
        /// </summary>
        /// <param name="gameTime">Temps de jeu.</param>
        /// <param name="graphics">Gestionnaire de l'affichage (écran).</param>
        public abstract void Update(GameTime gameTime, GraphicsDeviceManager graphics);

        /// <summary>
        /// Fonction membre à surcharger pour dessiner le sprite. Par défaut la this.texture est
        /// affichée à sa position corrigée selon la caméra donnée (si une est fournie).
        /// </summary>
        /// <param name="cameraRect">Rectangle de caméra.</param>
        /// <param name="spriteBatch">Gestionnaire de mise en tampon d'affichage.</param>
        public virtual void Draw(Rectangle cameraRect, SpriteBatch spriteBatch, Color? color)
        {
            // Calculer les coordonnées du sprite dans le monde.
            Rectangle destRect = new Rectangle(0, 0, this.Width, this.Height);
            destRect.X = (int)this.Position.X - (this.Width / 2);
            destRect.Y = (int)this.Position.Y - (this.Height / 2);

            // On s'assure que le sprite est visible dans la caméra.
            if (!destRect.Intersects(cameraRect))
            {
                return;
            }

            // Décaler la destination en fonction de la caméra avant de dessiner.
            destRect.Offset(-cameraRect.X, -cameraRect.Y);

            // Afficher le sprite.
            spriteBatch.Draw(this.Texture, destinationRectangle: destRect, color: color);
        }

        /// <summary>
        /// Fonction restreignant position à l'intérieur des limites fournies par boundsRect si
        /// de telles limites sont fournies.
        /// </summary>
        protected virtual void ClampPositionToBoundsRect()
        {
            // Limiter le mouvement si un boundsRect est fourni.
            if (!this.boundsRect.IsEmpty)
            {
                // On divise la taille du sprite par 2 car _position indique le centre du sprite.
                this.position.X = MathHelper.Clamp(this.position.X, this.boundsRect.Left + (this.Width / 2), this.boundsRect.Right - (this.Width / 2));
                this.position.Y = MathHelper.Clamp(this.position.Y, this.boundsRect.Top + (this.Height / 2), this.boundsRect.Bottom - (this.Height / 2));
            }
        }
    }
}
