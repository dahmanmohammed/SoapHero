using Microsoft.Xna.Framework;
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
        Monde monde;

        public void Update(WorldState worldState)
        {
            if (monde != null) return;
            switch (worldState)
            {
                case WorldState.InitialWorld:
                    monde = new MondePrincipale();
                    break;
                case WorldState.LevelOne:
                    monde = new NiveauUn();
                    break;
                case WorldState.LevelTwo:
                    monde = new NiveauDeux();
                    break;
            }
        }

        //public void Draw(int worldState, Rectangle cameraRect, SpriteBatch spriteBatch)
        //{
        //    monde.Draw(cameraRect, spriteBatch);
        //}

        //TEMP JUST TO TEST THE PLAYER
        
        public void Draw(WorldState worldState, GraphicsDevice gd)
        {
            switch (worldState)
            {
                case WorldState.InitialWorld:
                    gd.Clear(Color.Red);
                    break;
                case WorldState.LevelOne:
                    gd.Clear(Color.Green);
                    break;
                case WorldState.LevelTwo:
                    gd.Clear(Color.Blue);
                    break;
            }

        }
    }
}
