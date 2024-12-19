using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème2024
{
    public class Plateau
    {
        private De[] setDeDes;
        private char[,] plateauActif;
        private int taillePlateau;
        private Random r;

        public Plateau(int taillePlateau, Random r, Alphabet alphabet)
        {
            this.taillePlateau = taillePlateau;
            this.r = r;
            this.setDeDes = new De[taillePlateau * taillePlateau];
            for (int i = 0; i < setDeDes.Length; i++)
            {
                this.setDeDes[i] = new De(r, alphabet);
            }

            this.plateauActif = new char[taillePlateau, taillePlateau];
            int indice = 0;
            for (int i = 0; i < taillePlateau; i++)
            {
                for (int j = 0; j < taillePlateau; j++)
                {
                    plateauActif[i, j] = setDeDes[indice].FaceVisible;
                    indice++;
                }
            }
        }
        public De[] SetDeDes
        {
            get { return setDeDes; }
        }
        public char[,] PlateauActif
        {
            get { return plateauActif; }
            set { plateauActif = value; }
        }
        public void ActualiserPlateauActif()
        {
            int indice = 0;
            for (int i = 0; i < taillePlateau; i++)
            {
                for (int j = 0; j < taillePlateau; j++)
                {
                    SetDeDes[indice].Lance(r);
                    plateauActif[i, j] = setDeDes[indice].FaceVisible;
                    indice++;
                }
            }
        }
        public void AfficherPlateauActif()
        {
            string frontiere = "";
            for (int i=0 ; i < taillePlateau; i++)
            {
                frontiere += "+----";
            }
            frontiere += "+";
            Console.WriteLine(frontiere);
            for (int j = 0; j < taillePlateau; j++)
            {
                for (int k = 0; k < taillePlateau; k++)
                {
                    Console.Write("| "+plateauActif[j, k] + " ");
                }
                Console.WriteLine("|");
                Console.WriteLine(frontiere);
            }
            Console.WriteLine(frontiere );
        }
        /// <summary>
        /// La méthode Test_Plateau permet de vérifier si le mot entré par le joueur existe sur le plateau de jeu.
        /// Pour cela on fonctionne en récursif. La première étape de fonctionnement est de collecter tous les indices, sous la forme de Positions, qui peuvent 
        /// mener à un chemin qui forme le mot. 
        /// Pour cela deux cas se distinguent, soit on est au début du mot et il faut alors collecter toutes les positions
        /// de la première symboleLettre dans le plateau de jeu, soit on ne se trouve pas à la première symboleLettre du mot la position posActuelle est alors non nulle, et 
        /// on vérifie toutes les positions adjacentes à celle à laquelle on se trouve. On élimine l'indice où on se trouve actuellement, puis on vérifie si 
        /// les indices sont dans les limites du plateau. Si ces conditions sont respectées, et que la symboleLettre se trouvant à cette position est celle recherchée
        /// et que nous ne sommes pas en train de repasser sur une position déjà traversée, stockée dans plateauBinaire, on stocke la position dans la liste
        /// des positions.
        /// Première apparition d'une condition d'arrêt ici. On est entré dans la boucle du cas où l'indice en train d'être testé est valide et contient la
        /// symboleLettre recherchée. Si le mot est de taille 1, alors on a trouvé la dernière symboleLettre donc on retourne true. Sinon, on continue l'exécution du code.
        /// Une fois la liste des positions qui peuvent mener à un chemin qui contient le mot établie, on va tester les chemins qui en découlent.
        /// </summary>
        /// <param name="mot">Contient le mot à vérifier. A chaque itération, on enlève la première symboleLettre si on l'a trouvée</param>
        /// <param name="posActuelle">Contient la position à laquelle on se trouve sur le plateau</param>
        /// <param name="plateauBinaire">Contient le chemin déjà parcouru sur le plateau 0 si la case est libre, 1 si on y est déjà passé</param>
        /// <returns>Si la liste est vide, on a trouvé aucune position valide qui contient la symboleLettre recherchée, donc le mot n'est pas dans le tableau et puisque la
        /// liste est vide, on n'entre pas dans la boucle foreach et on retourne cheminExiste tel qu'il a été initialisé, soit false. 
        /// Sinon, pour chaque position relevée, on marque le passage sur cette position dans tableauBinaire, et au booléen cheminExiste on associe cheminExiste ou
        /// le retour de la méthode elle même avec comme argument le mot sans le premier caractère, la position sur laquelle on travaille, le plateau binaire marqué
        /// du chemin. Enfin on remet la case marquée du plateauBinaire à 0 pour que le prochain chemin potentiel ne soit pas impacté.
        /// </returns>
        public bool Test_Plateau(string mot, Position posActuelle = null, int[,] plateauBinaire = null)
        {
            bool cheminExiste = false;
            char premiereLettre = mot[0];
            List<Position> posPossible=new List<Position>();
            if (plateauBinaire == null)
            {
                plateauBinaire=new int[taillePlateau, taillePlateau];
            }
            if (posActuelle==null)
            {
                for (int i = 0; i < taillePlateau; i++)
                {
                    for (int j = 0; j < taillePlateau; j++)
                    {
                        if (plateauActif[i, j] == premiereLettre)
                        {
                            posPossible.Add(new Position(i, j));
                        }
                    }
                }
            }
            else
            {
                for (int i = posActuelle.X - 1; i <= posActuelle.X + 1; i++)
                {
                    for (int j = posActuelle.Y - 1; j <= posActuelle.Y + 1; j++)
                    {
                        if ((i != posActuelle.X || j != posActuelle.Y) && i >= 0 && j >= 0 && i < taillePlateau && j < taillePlateau && plateauBinaire[i, j] == 0 && premiereLettre == plateauActif[i, j])
                        {
                            if (mot.Length > 1)
                            {
                                posPossible.Add(new Position(i, j));
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            foreach (Position pos in posPossible)
            {
                plateauBinaire[pos.X, pos.Y] = 1;
                cheminExiste = cheminExiste || Test_Plateau(mot.Substring(1), pos, plateauBinaire);
                plateauBinaire[pos.X, pos.Y] = 0;
            }
            return cheminExiste;
        }
    }
    public class Position
    {
        private int x;
        private int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
    }
}
