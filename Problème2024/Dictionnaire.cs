using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème2024
{
    public class Dictionnaire
    {
        private string langue;
        private SortedList<int,List<string>> dicoTrie;

        public Dictionnaire(string langue)
        {
            this.langue = langue;
            string fichierDico;

            try
            {
                if (langue.ToLower() == "english")
                    fichierDico = File.ReadAllText("MotsPossiblesEN.txt");
                else
                    fichierDico = File.ReadAllText("MotsPossiblesFR.txt");
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Le fichier dictionnaire est introuvable.");
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
                    mots.Add(fichierDico);
                }
            }
            mots.Sort();
            this.dicoTrie = TrieNumeraire(mots);
        }
        public static SortedList<int, List<string>> TrieNumeraire(List<string> dico)
        {
            SortedList<int, List<string>> dicoTrie = new SortedList<int, List<string>>();
            foreach (string mot in dico)
            {
                int longueur = mot.Length;
                if (!dicoTrie.ContainsKey(longueur))
                {
                    dicoTrie.Add(longueur, new List<string>());
                }
                dicoTrie[longueur].Add(mot);
            }
            return dicoTrie;
        }
        public string toString()
        {
            return $"Ce dictionnaire est en {langue}, "; //: comment ça nombre de mots par longueur et le nombre de mots par symboleLettre
        }
        public bool RechDichoRecursifBrutal(string mot, int indice = 0)
        {
            if(!dicoTrie.ContainsKey(mot.Length) || indice >= dicoTrie[mot.Length].Count)
            {
                return false;
            }
            else if (dicoTrie[mot.Length][indice] != null && dicoTrie[mot.Length][indice] == mot)
            {
                return true;
            }
            else
            {
                return RechDichoRecursifBrutal(mot, indice++);
            }
        }
        /// <summary>
        /// Cette fonction permet de vérifier si un mot est dans le dictionnaire en utilisant le principe de diviser pour régner
        /// Si le mot est d'une longueur qui n'existe pas dans le dico, ou si l'indice de début a dépassé celui de la fin, ce qu'il 
        /// se passe après n'avoir pas trouvé le mot dans un intervalle de taille 1, alors le mot n'y est pas 
        /// sinon, si le mot est dans la première moitié, on déplace les indices pour ne vérifier qu'à un intervalle de taille1, et alors
        /// on vérifie si le mot qu'il reste est celui qu'on cherchait, ou tomber sur le mot qu'on cherche en coupant la liste en deux, ou 
        /// bien si on ne le trouve pas alors debut va dépasser fin.
        /// </summary>
        /// <param name="mot">Mot à tester</param>
        /// <param name="debut">Indice de début de l'intervalle de la liste à vérifier. Initialisé à -1 pour le fonctionnement de l'algo</param>
        /// <param name="fin">Indice de début de l'intervalle de la liste à vérifier. Initialisé à -1 pour le fonctionnement de l'algo</param>
        /// En effet, si on les assigne directemnet on risque une erreur si la longueur du mot n'existe pas dans le dictionnaire
        /// <returns>On analyse à chaque fois la moitié de la liste dans laquelle se trouve le mot, jusqu'à tomber sur les cas décris ci-dessus</returns>
        public bool RechDichoRecursifDiviser(string mot, int debut=-1, int fin=-1)
        {
            
            if (debut > fin || !dicoTrie.ContainsKey(mot.Length))
            {
                return false;
            }
            if (debut == -1 && fin == -1)
            {
                debut = 0;
                fin = dicoTrie[mot.Length].Count - 1;
            }
            int milieu = (fin+debut) / 2;
            string motMilieu = dicoTrie[mot.Length][milieu].ToLower().Trim();
            if (mot.CompareTo(motMilieu)<0)
            {
                return RechDichoRecursifDiviser(mot, debut, milieu - 1);
            }
            else
            {
                if(mot.CompareTo(motMilieu)>0) 
                {
                    return RechDichoRecursifDiviser(mot, milieu + 1, fin);
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
