//-----------------------------------------------------------------------
// <copyright file="JoueurSprite.cs" company="Collège La Cité">
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
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Classe représentant le sprite contrôlé par le joueur (le soldat).
    /// </summary>
    public class JoueurSprite : SpriteAnimation
    {
        /// <summary>
        /// Attribut statique (i.e. partagé par toutes les instances) constituant une 
        /// liste de palettes à exploiter selon la direction et l'état du joueur.
        /// </summary>
        private static List<Palette> palettes = new List<Palette>();

        /// <summary>
        /// Attribut indiquant la direction de déplacement courante.
        /// </summary>
        private Direction directionDeplacement;

        /// <summary>
        /// Attribut indiquant la direction de déplacement courante.
        /// </summary>
        private Etats etat;

        /// <summary>
        /// Vitesse de marche du joueur, avec valeur par défaut.
        /// </summary>
        private float vitesseMarche = 0.2f;

        /// <summary>
        /// Constructeur paramétré recevant la position du sprite.
        /// </summary>
        /// <param name="x">Position horizontale initiale du sprite.</param>
        /// <param name="y">Position verticale initiale du sprite.</param>
        public JoueurSprite(float x, float y)
            : base(x, y)
        {
            // Par défaut, le sprite est stationnaire et fait face au joueur.
            this.directionDeplacement = Direction.Sud;
            this.etat = Etats.Stationnaire;
        }

        /// <summary>
        /// Constructeur paramétré recevant la position du sprite. On invoque l'autre constructeur.
        /// </summary>
        /// <param name="position">Position initiale du sprite.</param>
        public JoueurSprite(Vector2 position)
            : this(position.X, position.Y)
        {
        }

        /// <summary>
        /// Enumération des directions potentielles de déplacement du joueur.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Vers le nord.
            /// </summary>
            Nord,

            /// <summary>
            /// Vers le nord est.
            /// </summary>
            NordEst,

            /// <summary>
            /// Vers l'est.
            /// </summary>
            Est,

            /// <summary>
            /// Vers le sud est.
            /// </summary>
            SudEst,

            /// <summary>
            /// Vers le sud.
            /// </summary>
            Sud,

            /// <summary>
            /// Vers le sud ouest.
            /// </summary>
            SudOuest,

            /// <summary>
            /// Vers l'ouest.
            /// </summary>
            Ouest,

            /// <summary>
            /// Vers le nord ouest.
            /// </summary>
            NordOuest
        }

        /// <summary>
        /// États disponibles du personnage.
        /// </summary>
        public enum Etats
        {
            /// <summary>
            /// Le sprite ne se déplace pas.
            /// </summary>
            Stationnaire,

            /// <summary>
            /// Le sprite se déplace en marchant.
            /// </summary>
            Marche,

            /// <summary>
            /// Le sprite se déplace en courant.
            /// </summary>
            Course
        }

        /// <summary>
        /// Propriété indiquant la direction de déplacement courante.
        /// </summary>
        /// <value>La direction du déplacement.</value>
        public Direction DirectionDeplacement
        {
            /// <summary>
            /// Accesseur par défaut.
            /// </summary>
            get
            {
                return this.directionDeplacement;
            }

            /// <summary>
            /// Mutateur par défaut.
            /// </summary>
            set
            {
                this.directionDeplacement = value;
            }
        }

        /// <summary>
        /// Propriété indiquant la direction de déplacement courante.
        /// </summary>
        /// <value>L'état du sprite.</value>
        public Etats Etat
        {
            /// <summary>
            /// Accesseur par défaut.
            /// </summary>
            get
            {
                return this.etat;
            }

            /// <summary>
            /// Mutateur par défaut.
            /// </summary>
            set
            {
                this.etat = value;
            }
        }

        /// <summary>
        /// Propriété gérant la vitesse de déplacement du sprite lorsqu'il marche.
        /// </summary>
        /// <value>Vitesse de déplacement du sprite lorsqu'il marche.</value>
        public float VitesseMarche
        {
            get { return this.vitesseMarche; }
            set { this.vitesseMarche = value; }
        }

        /// <summary>
        /// Surchargé afin de retourner la palette correspondant à la direction de 
        /// déplacement et l'état du joueur.
        /// </summary>
        /// <value>La palette à utiliser pour l'animation.</value>
        protected override Palette PaletteAnimation
        {
            /// <summary>
            /// Les palettes sont stockées dans la liste en groupes d'état (i.e.
            /// 8 palettes de direction pour chaque état).
            /// </summary>
            get
            {
                return palettes[((int)this.etat * 8) + (int)this.directionDeplacement];
            }
        }

        /// <summary>
        /// Charge les images associées au sprite du joueur.
        /// </summary>
        /// <param name="content">Gestionnaire de contenu (du répertoire Content).</param>
        /// <param name="graphics">Gestionnaire de l'affichage (écran).</param>
        public static void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            // Puisque les palettes sont répertoriées selon l'état, on procède ainsi,
            // chargeant les huit palettes directionnelles un état à la fois.
            foreach (Etats unetat in Enum.GetValues(typeof(Etats)))
            {
                // Déterminer le répertoire contenant les palettes selon l'état.
                string repertoire;
                switch (unetat)
                {
                    case Etats.Marche:
                        repertoire = @"Joueur\Marche\";
                        break;
                    case Etats.Course:
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

        /// <summary>
        /// Ajuste la position du sprite en fonction de l'input.
        /// </summary>
        /// <param name="gameTime">Temps de jeu.</param>
        /// <param name="graphics">Gestionnaire de l'affichage (écran).</param>
        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            // Calcul de la vitesse de marche du joueur (indépendante du matériel)
            float vitesse = gameTime.ElapsedGameTime.Milliseconds * this.vitesseMarche;

            // Pour éviter d'interroger le clavier trop souvent (soucis d'efficacité), on 
            // stocke son état.
            KeyboardState etatClavier = Keyboard.GetState();

            // Changer le sprite selon la direction.
            if (etatClavier.IsKeyDown(Keys.Up) && etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = Direction.NordEst;
            }
            else if (etatClavier.IsKeyDown(Keys.Up) && etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = Direction.NordOuest;
            }
            else if (etatClavier.IsKeyDown(Keys.Down) && etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = Direction.SudEst;
            }
            else if (etatClavier.IsKeyDown(Keys.Down) && etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = Direction.SudOuest;
            }
            else if (etatClavier.IsKeyDown(Keys.Up))
            {
                this.directionDeplacement = Direction.Nord;
            }
            else if (etatClavier.IsKeyDown(Keys.Right))
            {
                this.directionDeplacement = Direction.Est;
            }
            else if (etatClavier.IsKeyDown(Keys.Left))
            {
                this.directionDeplacement = Direction.Ouest;
            }
            else if (etatClavier.IsKeyDown(Keys.Down))
            {
                this.directionDeplacement = Direction.Sud;
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
                    this.etat = Etats.Course;
                    deltaX = (int)(deltaX * 1.75f);
                    deltaY = (int)(deltaY * 1.75f);
                }
                else
                {
                    this.etat = Etats.Marche;
                }

                // Mettre à jour la position du sprite.
                this.Position = new Vector2(this.Position.X + deltaX, this.Position.Y + deltaY);
            }
            else
            {
                this.Etat = Etats.Stationnaire;    // aucun mouvement: le joueur est stationnaire
            }

            // Ne pas oublier d'invoquer SpriteAnimation.Update() qui s'occupe de l'animation.
            base.Update(gameTime, graphics);
        }
    }
}
