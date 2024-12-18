using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        List<Joueur> Joueurs = new List<Joueur>;

        Console.WriteLine("Boogle !\n\nVeuillez entrer vos noms, puis appuyer sur entrée");

        string nom = Console.ReadLine();
        while (nom != null || Joueurs.Count < 2)
        {
            Joueurs.Add(new Joueur(nom)));
            nom = Console.ReadLine();
        }

        Console.WriteLine("Veuillez choisir la taille du plateau, entre 2 et 10 cases de côté");
        int taillePlateau = int.Parse(Console.ReadLine());

        Console.WriteLine("Veuillez choisir la langue du plateau, :\n 1 : Français\n 2 : Anglais");
        switch (Console.ReadLine())
        {
            case "1":
                Dictionnaire dictionnaire = new Dictionnaire("français"); //: voir la création du dico au niveau du choix de la langue : comment renvoyer au bon fichier directement ?
                break;
            case "2":
                Dictionnaire dictionnaire = new Dictionnaire("english");
                break;
            default:
                Console.WriteLine("La langue choisie est invalide. Par défaut, la langue du plateau sera le français.");
                dictionnaire = new Dictionnaire("français");
                break;
        }

        Random r = new Random();
        Plateau plateau = new Plateau(taillePlateau * taillePlateau, r);

        string mot = "";
        for (int i = 1; i <= Joueurs.Count; i++)
        {

            Console.WriteLine($"C'est au tour de {joueur[i - 1].Nom} de jouer ! Vous avez 1 min pour trouver un maximum de mots !\nVoici le plateau de jeu :\n ");
            Stopwatch timer = Stopwatch.StartNew();
            while (timer.Elapsed.Minutes < 1)
            {
                AfficherPlateauActif(taillePlateau);
                mot = Console.ReadLine();

                if (mot != null && mot.Length >= 2 && !Contain(mot) && RechDichoRecursif(mot) && Test_Plateau(mot))
                {
                    joueur[i - 1].MotsTrouves += Add_Mot(mot);
                    Console.WriteLine("Bravo ! Un mot de plus !");
                }
                else
                {
                    Console.WriteLine("Oups ! Mot invalide !");
                }
            }
            timer.Stop();
            Console.WriteLine($"Temps écoulé !\n{joueur[i].toString()}");
            ActualiserTableauActif();
        }
        Joueur gagnant = Joueurs[0];
        for (int i = 1; i <= Joueurs.Count; i++)
        {
            Console.WriteLine($"{joueur[i - 1].Nom} a réussi à obtenir {joueur[i - 1].Score} points");
            gagnant = (joueur[i - 1].Score > gagnant.Score) ? joueur[i - 1] : gagnant;
        }
        Console.WriteLine($"Bravo au gagnant {gagnant.Nom} !");//: Créer un classement si y'a le temps
    }


}
