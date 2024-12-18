﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème2024
{
    public class Alphabet
    {
        private Lettre[] lettres;
        private List<char> possibilites;
        private string langue;

        public Alphabet(string langue)
        {
            string[] fichier;
            if (langue == "français")
            {
                fichier= File.ReadAllLines("Lettres.txt");
            }
            else
            {
                fichier = File.ReadAllLines("Lettres.txt");
            }
            for (int i = 0; i < fichier.Length; i++)
            {
                fichier[i] = fichier[i].Trim();

            }

            this.lettres = new Lettre[26];
            for (int i = 0; i < fichier.Length; i++)
            {
                int indiceLastvirgule = fichier[i].LastIndexOf(";");
                char LettreActuelle = Convert.ToChar(fichier[i].Substring(0, 1));
                int valeur = Convert.ToInt32(fichier[i].Substring(2, indiceLastvirgule - 3)); //:cf fonctionnement Substring 
                int occurenceMax = Convert.ToInt32(fichier[i].Substring(indiceLastvirgule + 1));
                lettres[i] = new Lettre(LettreActuelle, valeur, occurenceMax);
            }
            this.possibilites = new List<char>();
            foreach (Lettre l in lettres)
            {
                for (int i = 0; i < l.OccurenceMax; i++)
                {
                    possibilites.Add(l.SymboleLettre);
                }
            }
        }
        public Lettre[] Lettres
        {
            get { return this.lettres; }
        }
        public List<char> Possibilites
        {
            get { return this.possibilites; }
            set { this.possibilites = value;}
        }
    }
    public class Lettre
    {
        private char symboleLettre;
        private int valeur;
        private int occurenceMax;

        public Lettre(char lettre, int valeur, int occurenceMax)
        {
            this.symboleLettre = lettre;
            this.valeur = valeur;
            this.occurenceMax = occurenceMax;
        }
        public char SymboleLettre
        {
            get { return this.symboleLettre; }
        }
        public int Valeur
        {
            get { return valeur; }
        }

        public int OccurenceMax
        {
            get { return occurenceMax; }
        }
    }
}
