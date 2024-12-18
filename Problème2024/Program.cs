using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Problème2024;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Veuillez choisir la langue du plateau, :\n 1 : Français\n 2 : Anglais");
        Dictionnaire dictionnaire;
        string langue = Console.ReadLine().ToLower().Trim();
        switch (langue)
        {
            case "1":
                dictionnaire = new Dictionnaire("français"); //: voir la création du dico au niveau du choix de la langue : comment renvoyer au bon fichier directement ?
                break;
            case "2":
                dictionnaire = new Dictionnaire("english");
                break;
            default:
                Console.WriteLine("La langue choisie est invalide. Par défaut, la langue du plateau sera le français.");
                dictionnaire = new Dictionnaire("français");
                break;
        }
        List<Joueur> Joueurs = new List<Joueur>();

        Alphabet alphabet = new Alphabet(langue);

        Console.WriteLine("Boogle !\n\nVeuillez entrer vos noms, puis appuyer sur entrée");

        string nom = Console.ReadLine();
        while (nom != null || Joueurs.Count < 2)
        {
            Joueurs.Add(new Joueur(nom, alphabet));
            nom = Console.ReadLine();
        }

        Console.WriteLine("Veuillez choisir la taille du plateau, entre 2 et 10 cases de côté");
        int taillePlateau = int.Parse(Console.ReadLine());

        Random r = new Random();
        Plateau plateau = new Plateau(taillePlateau * taillePlateau, r, alphabet);

        string mot = "";
        for (int i = 1; i <= Joueurs.Count; i++)
        {

            Console.WriteLine($"C'est au tour de {Joueurs[i - 1].Nom} de jouer ! Vous avez 1 min pour trouver un maximum de mots !\nVoici le plateau de jeu :\n ");
            Stopwatch timer = Stopwatch.StartNew();
            while (timer.Elapsed.Minutes < 1)
            {
                plateau.AfficherPlateauActif();
                mot = Console.ReadLine();

                if (mot != null && mot.Length >= 2 && !(Joueurs[i-1].Contain(mot)) && dictionnaire.RechDichoRecursif(mot) && plateau.Test_Plateau(mot))
                {
                    Joueurs[i - 1].MotsTrouves.Add(mot);
                    Console.WriteLine("Bravo ! Un mot de plus !");
                }
                else
                {
                    Console.WriteLine("Oups ! Mot invalide !");
                }
            }
            timer.Stop();
            Console.WriteLine($"Temps écoulé !\n{Joueurs[i-1].toString()}");
            plateau.ActualiserPlateauActif();
        }
        Joueur gagnant = Joueurs[0];
        for (int i = 1; i <= Joueurs.Count; i++)
        {
            Console.WriteLine($"{Joueurs[i - 1].Nom} a réussi à obtenir {Joueurs[i - 1].Score} points");
            gagnant = (Joueurs[i - 1].Score > gagnant.Score) ? Joueurs[i - 1] : gagnant;
        }
        Console.WriteLine($"Bravo au gagnant {gagnant.Nom} !");//: Créer un classement si y'a le temps
    }


}
