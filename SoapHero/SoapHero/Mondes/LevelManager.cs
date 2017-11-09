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

        public void Update(int worldState)
        {
            if (monde != null) return;
            switch (worldState)
            {
                case 0: //WorldState.InitialWorld
                    monde = new MondePrincipale();
                    break;
                case 1: //WorldState.LevelOne
                    monde = new NiveauUn();
                    break;
                case 2: //WorldState.LevelTwo
                    monde = new NiveauDeux();
                    break;
            }
        }

        //public void Draw(int worldState, Rectangle cameraRect, SpriteBatch spriteBatch)
        //{
        //    monde.Draw(cameraRect, spriteBatch);
        //}

        //TEMP JUST TO TEST THE PLAYER
        
        public void Draw(int worldState, GraphicsDevice gd)
        {
            switch (worldState)
            {
                case 0: //WorldState.InitialWorld
                    gd.Clear(Color.Red);
                    break;
                case 1: //WorldState.LevelOne
                    gd.Clear(Color.Green);
                    break;
                case 2: //WorldState.LevelTwo
                    gd.Clear(Color.Blue);
                    break;
            }
        }
    }
}
