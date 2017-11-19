namespace SoapHero
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public abstract class Joueur : SpriteAnimation
    {
        protected static List<Palette> palettes = new List<Palette>();

        private PlayerDirection directionDeplacement = PlayerDirection.Sud;
        private PlayerState etat = PlayerState.Stationnaire;

        private float vitesse = 0.2f;

        public Joueur(float x, float y) : base(x, y) { }

        protected override Palette PaletteAnimation
        {
            get
            {
                return palettes[((int)this.etat * 8) + (int)this.directionDeplacement];
            }
        }

        protected abstract void LoadContent(ContentManager content, GraphicsDeviceManager graphics);

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            // Calcul de la vitesse de marche du joueur (indépendante du matériel)
            float vitesse = gameTime.ElapsedGameTime.Milliseconds * this.vitesse;

            // Pour éviter d'interroger le clavier trop souvent (soucis d'efficacité), on 
            // stocke son état.
            KeyboardState etatClavier = Keyboard.GetState();

            // Changer le sprite selon la direction.
            if (etatClavier.IsKeyDown(Keys.Up) && etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = PlayerDirection.NordEst;
            }
            else if (etatClavier.IsKeyDown(Keys.Up) && etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = PlayerDirection.NordOuest;
            }
            else if (etatClavier.IsKeyDown(Keys.Down) && etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = PlayerDirection.SudEst;
            }
            else if (etatClavier.IsKeyDown(Keys.Down) && etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = PlayerDirection.SudOuest;
            }
            else if (etatClavier.IsKeyDown(Keys.Up))
            {
                this.directionDeplacement = PlayerDirection.Nord;
            }
            else if (etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = PlayerDirection.Est;
            }
            else if (etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = PlayerDirection.Ouest;
            }
            else if (etatClavier.IsKeyDown(Keys.Down))
            {
                this.directionDeplacement = PlayerDirection.Sud;
            }

            // Déplacer le sprite selon la direction indiquée. Notez que deux directions
            // opposées s'annulent. On commence par calculer le déplacement qui sera
            // appliqué au sprite.
            int deltaX = 0, deltaY = 0;
            if (etatClavier.IsKeyDown(Keys.Left) && !etatClavier.IsKeyDown(Keys.Right))
            {
                deltaX = (int)-vitesse;
            }

            if (etatClavier.IsKeyDown(Keys.Right) && !etatClavier.IsKeyDown(Keys.Left))
            {
                deltaX = (int)vitesse;
            }

            if (etatClavier.IsKeyDown(Keys.Up) && !etatClavier.IsKeyDown(Keys.Down))
            {
                deltaY = (int)-vitesse;
            }

            if (etatClavier.IsKeyDown(Keys.Down) && !etatClavier.IsKeyDown(Keys.Up))
            {
                deltaY = (int)vitesse;
            }

            // Modifier la position et l'état du sprite en conséquence.
            if (deltaX != 0 || deltaY != 0)
            {
                // Il y a mouvement. Est-ce que le joueur court?
                if (etatClavier.IsKeyDown(Keys.C))
                {
                    this.etat = PlayerState.Course;
                    deltaX = (int)(deltaX * 1.75f);
                    deltaY = (int)(deltaY * 1.75f);
                }
                else
                {
                    this.etat = PlayerState.Marche;
                }

                // Mettre à jour la position du sprite.
                this.Position = new Vector2(this.Position.X + deltaX, this.Position.Y + deltaY);
            }
            else
            {
                this.etat = PlayerState.Stationnaire;    // aucun mouvement: le joueur est stationnaire
            }

            base.Update(gameTime, graphics);
        }
    }
}
