using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapHero
{
    class LevelManager
    {
        Rectangle cameraRect;

        WorldState worldState = WorldState.InitialWorld;

        Monde monde;
        Joueur joueur;

        public LevelManager(GraphicsDevice gd)
        {
            cameraRect = new Rectangle(0, 0, gd.Viewport.Width, gd.Viewport.Height);
        }

        public void InitializeWorld(ContentManager content, GraphicsDeviceManager graphics)
        {
            if (monde != null) return;
            switch (worldState)
            {
                case WorldState.InitialWorld:
                    monde = new MondePrincipale();
                    MondePrincipale.LoadContent(content);

                    JoueurHumain.LoadContent(content, graphics);
                    joueur = new JoueurHumain(1150, 300);
                    joueur.BoundsRect = new Rectangle(0, 0, monde.Largeur, monde.Hauteur);
                    break;
                case WorldState.LevelOne:
                    monde = new NiveauUn();
                    //NiveauUn.LoadContent(content);
                    break;
                case WorldState.LevelTwo:
                    monde = new NiveauDeux();
                    //NiveauDeux.LoadContent(content);
                    break;
            }
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            joueur.Update(gameTime, graphics);
            cameraRect.X = (int)this.joueur.Position.X - (cameraRect.Width / 2);
            cameraRect.Y = (int)this.joueur.Position.Y - (cameraRect.Height / 2);
            RestreindreCameraAuMonde();
        }
        
        public void Draw(GraphicsDevice gd, SpriteBatch spriteBatch)
        {
            if (monde == null) return;
            monde.Draw(cameraRect, spriteBatch, new Color(75,75,50));
            if (joueur == null) return;
            joueur.Draw(cameraRect, spriteBatch, null);
        }

        private void RestreindreCameraAuMonde()
        {
            this.cameraRect.Offset(System.Math.Max(0, -this.cameraRect.Left), System.Math.Max(-this.cameraRect.Top, 0));
            this.cameraRect.Offset(System.Math.Min(0, this.monde.Largeur - this.cameraRect.Right), System.Math.Min(0, this.monde.Hauteur - this.cameraRect.Bottom));
        }
    }
}
