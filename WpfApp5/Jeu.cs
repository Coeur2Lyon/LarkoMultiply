using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LarkoMultiply
{
    public class Jeu
    {
        public enum Etat
        {
            Vide,
            Mur,
            /*Resultat1,
            Resultat2,
            Resultat3,
            //Resultat4,*/

        }

        private Position personnage;
        public Position Personnage { get { return personnage; } }

        private Perso etatpersonnage;
        public Perso Etatpersonnage { get { return etatpersonnage; } }

        private Position caseResultat1;
        public Position CaseResultat1 { get { return caseResultat1; } }

        private Position caseResultat2;
        public Position CaseResultat2 { get { return caseResultat2; } }

        private Position caseResultat3;
        public Position CaseResultat3 { get { return caseResultat3; } }

        private Etat[,] grille;


        //Le tableau suivant est la maquette de la plateforme 2D, les 1 seront des murs et les 0 seront une case vide,
        int[,] grilleTbl = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 2, 0, 0, 0, 1 }, { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

        public Jeu()
        {
            grille = new Etat[10, 10];
            InitCarte();
        }

        public void ToucheAppuyee(Key key)
        {
            Position newPos = new Position(personnage.x, personnage.y);//On positionne en tant que newPos(future position) le perso où il est à sa drnière position (avant appui touche)
            Perso newEtat = new Perso(etatpersonnage.etat);
            CalculNewEtat(newEtat, key);
            CalculNewPos(newPos, key);
            // On vérifie que la case d'arrivée ne soit pas un mur (en quel cas il faut bloque le perso)
            if (CaseOK(newPos, key)) //On crée une methode qui a pour but de vérifier si le déplacement est conforme (pas de mur ou caisse collée à mur a  l'arrivée) en fonction de la nouvelle position et de la dernière touche utilisée.
            {
                personnage = newPos;
                //nbDeplacements++;
            }
            etatpersonnage = newEtat;
        }

        public static void CalculNewEtat(Perso newEtat, Key key)
        {
            switch (key)
            {
                case Key.Down: // ATTENTION! Comme le X est en haut de l'écran, quand on descend le x augmente
                    newEtat.etat = 1;//Perso qui regarde en bas
                    break;
                case Key.Up:
                    newEtat.etat = 2;//Perso qui regarde en haut
                    break;
                case Key.Right:
                    newEtat.etat = 3;//Perso qui regarde à droite
                    break;
                case Key.Left:
                    newEtat.etat = 4;//Perso qui regarde à gauche
                    break;
            }
        }

        public static void CalculNewPos(Position newPos, Key key)
        {
            switch (key)
            {
                case Key.Down: // ATTENTION! Comme le X est en haut de l'écran, quand on descend le x augmente
                    newPos.x++;
                    break;
                case Key.Up:
                    newPos.x--;
                    break;
                case Key.Right:
                    newPos.y++;
                    break;
                case Key.Left:
                    newPos.y--;
                    break;
            }
        }

        private void InitCarte()
        {
            // créer une liste vide de caisses

            // Pour chaque case, initialise la bonne valeur.
            //Ajoute les caisses si besoin.
            //Détermine la position de départ du personnage.

            //On parcours notre liste grilleTxt pour savoir quoi mettre dans les cases:
            for (int ligne = 0; ligne < 10; ligne++) // on commence par les lignes.
            {
                for (int colonne = 0; colonne < 10; colonne++) //Ensuite colonnes de 0 à 9 également
                { // on lira notre grilleTxt et en fonction de ça on fera un Switch pour appliquer l'Etat de la case
                    switch (grilleTbl[ligne, colonne])
                    {
                        case 0://Vide
                            grille[ligne, colonne] = Etat.Vide;
                            break;

                        case 1://Mur
                            grille[ligne, colonne] = Etat.Mur;
                            break;

                        case 2://Vide avec perso dessus
                            personnage = new Position(ligne, colonne);
                            etatpersonnage = new Perso(1);
                            grille[ligne, colonne] = Etat.Vide;
                            break;
                            /* case 11: // Case résultat1
                                 caseResultat1 = new Position(ligne, colonne);
                                 grille[ligne, colonne] = Etat.Vide;
                                 break;
                                 case 22://Case résultat2
                                     grille[ligne, colonne] = Etat.Resultat2;
                                     break;
                                 case 33: //Case résultat3
                                     grille[ligne, colonne] = Etat.Resultat3;
                                     break;
                                 case 44: //Case résultat4
                                     grille[ligne, colonne] = Etat.Resultat4;
                                     break;

                                  case 'O':
                                     grille[ligne, colonne] = Etat.Cible;
                                     break;

                                  case 'C':
                                     caisses.Add(new Position(ligne, colonne));
                                     grille[ligne, colonne] = Etat.Vide;
                                     break;
                                         */
                    }
                    Random aleatoire = new Random();
                    int l1 = aleatoire.Next(1, 9); //Génère un entier compris entre 1 et 8 (ligne du résultat 1)
                    while (l1 == 7) //le perso est ligne 7; donc la case resultat ne doit pas lui être superposée
                    {
                        l1 = aleatoire.Next(1, 9);
                    }
                    int c1 = aleatoire.Next(1, 9);
                    while (c1 == 5)
                    {
                        c1 = aleatoire.Next(1, 9);
                    }

                    int l2 = aleatoire.Next(1, 9); //Génère un entier compris entre 1 et 8
                    while (l2 == 7 || l2 == l1)
                    {
                        l2 = aleatoire.Next(1, 9);
                    }
                    int c2 = aleatoire.Next(1, 9);
                    while (c2 == 5 || c2 == c1)
                    {
                        c2 = aleatoire.Next(1, 9);
                    }
                    int l3 = aleatoire.Next(1, 9); //Génère un entier compris entre 1 et 8
                    while (l3 == 7 || l3 == l1 || l3 == l2)
                    {
                        l3 = aleatoire.Next(1, 9);
                    }
                    int c3 = aleatoire.Next(1, 9);
                    while (c3 == 7 || c3 == c1 || c3 == c2)
                    {
                        c3 = aleatoire.Next(1, 9);
                    }
                    caseResultat1 = new Position(l1, c1);
                    grille[l1, c1] = Etat.Vide;

                    caseResultat2 = new Position(l2, c2);
                    grille[l2, c2] = Etat.Vide;

                    caseResultat3 = new Position(l3, c3);
                    grille[l3, c3] = Etat.Vide;


                }
            }

        }

        public Etat Case(int ligne, int colonne)
        {
            return grille[ligne, colonne];
        }

        private bool CaseOK(Position newPos, Key key)
        {
            //Présence d'un mur
            if (grille[newPos.x, newPos.y] == Etat.Mur)
            {
                return false;
            }

            return true; // par défaut (pas d'obstacle)
        }
        public void Restart()
        {
            InitCarte(); // pour obtenir les positions intiales du perso et des caisse / pas besoin de redessiner la carte.
        }
    }
}


