using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème2024
{
    public class Joueur
    {
        private string nom;
        private int score;
        private List<string> motsTrouves;
        private Alphabet alphabet;

        public Joueur(string nom, Alphabet alphabet)
        {
            this.nom = nom;
            this.score = 0;
            this.motsTrouves = new List<string>();
            this.alphabet = alphabet;
        }
        public string Nom
        {
            get { return this.nom; }
        }
        public int Score
        {
            get { return this.score; }
        }
        public List<string> MotsTrouves
        {
            get { return motsTrouves; }
        }
        public bool Contain(string mot)
        {
            bool contient = false;
            int indice = 0;
            while (!contient && indice < motsTrouves.Count)
            {
                contient = (mot == motsTrouves[indice]);
                indice++;
            }
            return contient;
        }
        public void Add_Mot(string mot)
        {
            motsTrouves.Add(mot);
            foreach (char l in mot)
            {
                int emplacement = (int)char.ToUpper(l) - 'A';
                score += alphabet.Lettres[emplacement].Valeur;
            }

        }
        public string toString()
        {
            return $"Le joueur {nom} a trouvé {motsTrouves.Count} et a un score de {score} les mots trouvés sont : {string.Join(" ", motsTrouves)}";
        }
    }
}
