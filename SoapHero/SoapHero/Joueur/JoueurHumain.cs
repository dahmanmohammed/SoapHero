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

    public class JoueurHumain : Joueur
    {
        public JoueurHumain(float x, float y) : base(x, y) { }

        public static void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            // Puisque les palettes sont répertoriées selon l'état, on procède ainsi,
            // chargeant les huit palettes directionnelles un état à la fois.
            foreach (PlayerState unetat in Enum.GetValues(typeof(PlayerState)))
            {
                // Déterminer le répertoire contenant les palettes selon l'état.
                string repertoire;
                switch (unetat)
                {
                    case PlayerState.Marche:
                        repertoire = @"Joueur\Marche\";
                        break;
                    case PlayerState.Course:
                        repertoire = @"Joueur\Course\";
                        break;
                    default:
                        repertoire = @"Joueur\Stationnaire\";
                        break;
                }

                // Charger les différentes palettes du personnage selon les directions.
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "Nord"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "NordEst"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "Est"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "SudEst"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "Sud"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "SudOuest"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "Ouest"), 96, 96));
                palettes.Add(new Palette(content.Load<Texture2D>(repertoire + "NordOuest"), 96, 96));
            }
        }
    }
}
