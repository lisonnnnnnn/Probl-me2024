using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problème2024
{
    public class Dictionnaire
    {
        private string langue;
        private List<string> mots;

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string fichierDico;
            if (langue == "english")
            {
                fichierDico = File.ReadAllText("MotsPossiblesEN");
            }
            else 
            {
                fichierDico = File.ReadAllText("MotsPossiblesFR");
            }
            List<string> mots = new List<string>();
            while (fichierDico != null || fichierDico.Length > 0)
            {
                int indexEspace = fichierDico.IndexOf(" ");
                if (indexEspace != -1)
                {
                    string motPotentiel = fichierDico.Substring(0, indexEspace).Trim();

                    if (motPotentiel != null && motPotentiel.Length >= 2)
                    {
                        mots.Add(motPotentiel.Trim());
                    }
                    fichierDico = fichierDico.Substring(indexEspace + 1);
                }
                else
                {
                    mots.Add(fichierDico.Trim());
                }
            }
            mots.Sort();
            SortedList<int, List<string>> dicoTrie = TrieNumeraire(mots);

        }
        public static SortedList<int, List<string>> TrieNumeraire(List<string> dico)
        {
            SortedList<int, List<string>> dicoTrie = new SortedList<int, List<string>>();
            foreach (string mot in dico)
            {
                int longueur = mot.Length;
                if (dicoTrie.ContainsKey(longueur))
                {
                    dicoTrie[longueur].Add(mot);
                }
                else
                {
                    dicoTrie[longueur] = new List<string> { mot };
                }
            }
            return dicoTrie;
        }
        public string toString()
        {
            return $"Ce dictionnaire est en {langue}, "; //: comment ça nombre de mots par longueur etle nombre de mots par symboleLettre
        }
        public bool RechDichoRecursif(string mot)
        {

        }
    }
}
