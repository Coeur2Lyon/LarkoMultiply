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
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class FenetreJeu : Window
    {
        private Jeu jeu;
        private TextBlock txtResultat1;
        private TextBlock txtResultat2;
        private TextBlock txtResultat3;

        public TextBlock TxtResultat1 { get => txtResultat1; set => txtResultat1 = value; }
        public TextBlock TxtResultat2 { get => txtResultat2; set => txtResultat2 = value; }
        public TextBlock TxtResultat3 { get => txtResultat3; set => txtResultat3 = value; }

        public FenetreJeu()
        {
            InitializeComponent();
            jeu = new Jeu();
            this.KeyDown += MainWindow_KeyDown;
            Dessiner();
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Right) || e.Key.Equals(Key.Left) || e.Key.Equals(Key.Down) || e.Key.Equals(Key.Up))
            {
                jeu.ToucheAppuyee(e.Key);
                Redessiner();
            }
        }

        private void Dessiner()
        {
            DessinerCarte();
            DessinerPersonnage();
            DessinerCasesResultats();
            Redessiner();

        }



        private void DessinerCarte() // Pour dessiner la carte, il faut parcourir la grille de mon jeu et qu'en fonction, je place la bonne image au bon endroit.
        {

            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
                {
                    Rectangle r = new Rectangle(); //Création de chaque rectangle r (dans lesquels on placera les images)
                    r.Width = 50; //largeur du rectangle r
                    r.Height = 50; // hauteur du rectangle r
                    r.Margin = new Thickness(colonne * 50, ligne * 50, 0, 0); // margin du rectangle r en fonction dans le parcours de la liste (relatif au nombre de colonnes et lignes parcourues) 

                    switch (jeu.Case(ligne, colonne))
                    {
                        case Jeu.Etat.Vide:
                            r.Fill = new ImageBrush(new BitmapImage(new Uri("images/sol.png", UriKind.Relative)));
                            break;

                        case Jeu.Etat.Mur:
                            r.Fill = new ImageBrush(new BitmapImage(new Uri("images/mur.png", UriKind.Relative)));
                            break;
                    }
                    cnv_plateau.Children.Add(r);
                }
            }
        }

        private void Redessiner()
        {
            //On commence efface tout ce qui est sur le canevas Deplacement (donc Caisses et personnage); on aurait pu utiliser .remove au lieu de Clear pour effacer un ou des éléments au cas par cas.
            cnv_deplacements.Children.Clear();
            DessinerPersonnage();
            CheckerCase();

        }
        private void DessinerPersonnage()
        {
            Rectangle r = new Rectangle();
            r.Width = 50; //largeur du rectangle r
            r.Height = 50; // hauteur du rectangle r
            r.Margin = new Thickness(jeu.Personnage.y * 50, jeu.Personnage.x * 50, 0, 0);
            switch (jeu.Etatpersonnage.etat)
            {
                case 1://Bas
                    r.Fill = new ImageBrush(new BitmapImage(new Uri("images/larko_haut.png", UriKind.Relative)));
                    break;

                case 2://Haut
                    r.Fill = new ImageBrush(new BitmapImage(new Uri("images/larko_haut.png", UriKind.Relative)));
                    break;

                case 3://Droite
                    r.Fill = new ImageBrush(new BitmapImage(new Uri("images/larko_droite.png", UriKind.Relative)));
                    break;

                case 4://Gauche
                    r.Fill = new ImageBrush(new BitmapImage(new Uri("images/larko_gauche.png", UriKind.Relative)));
                    break;
            }

            cnv_deplacements.Children.Add(r);

        }

        private void DessinerCasesResultats()
        {
            cnv_resultats.Children.Clear();
            TxtResultat1 = new TextBlock();
            // ICI il y aura le CODE pour stocker un aléatoire int à la place de ?? en convertissant en str
            Random aleatoire = new Random();
            int multipl1a = aleatoire.Next(1, 10);
            int multipl1b = aleatoire.Next(1, 10);//Génère un entier compris entre 0 et 9
            int resultat1a = multipl1a * multipl1b;
            string resultat1 = resultat1a.ToString();
            TxtResultat1.FontFamily = new FontFamily("Showcard Gothic");
            TxtResultat1.Text = resultat1;
            txtChiffre1.Text = multipl1a.ToString();
            txtChiffre2.Text = multipl1b.ToString();

            //txtResutat1.Inlines.Add("");

            TxtResultat1.Margin = new Thickness(jeu.CaseResultat1.y * 50, jeu.CaseResultat1.x * 50, 0, 0);
            TxtResultat1.Height = 50;
            TxtResultat1.FontSize = 40;
            TxtResultat1.Foreground = Brushes.Blue;
            TxtResultat1.Width = 50;
            TxtResultat1.TextAlignment = TextAlignment.Center;
            TxtResultat1.Visibility = Visibility.Visible;
            cnv_resultats.Children.Add(txtResultat1);




            int multipl2a = aleatoire.Next(1, 10);
            int multipl2b = aleatoire.Next(1, 10);//Génère un entier compris entre 0 et 9
            int resultat2a = multipl2a * multipl2b;
            while (resultat2a == resultat1a)
            {
                multipl2a = aleatoire.Next(1, 10);
                multipl2b = aleatoire.Next(1, 10);//Génère un entier compris entre 0 et 9
                resultat2a = multipl2a * multipl2b;
            }
            string resultat2 = resultat2a.ToString();

            TxtResultat2 = new TextBlock();
            TxtResultat2.Text = resultat2;
            //txtResutat2.Inlines.Add(resultat2);
            TxtResultat2.Margin = new Thickness(jeu.CaseResultat2.y * 50, jeu.CaseResultat2.x * 50, 0, 0);
            TxtResultat2.Height = 50;
            TxtResultat2.FontSize = 40;
            TxtResultat2.FontFamily = new FontFamily("Showcard Gothic");
            TxtResultat2.Foreground = Brushes.Blue;
            TxtResultat2.Width = 50;
            TxtResultat2.TextAlignment = TextAlignment.Center;
            TxtResultat2.Visibility = Visibility.Visible;
            cnv_resultats.Children.Add(TxtResultat2);

            TxtResultat3 = new TextBlock();
            int multipl3a = aleatoire.Next(1, 10);
            int multipl3b = aleatoire.Next(1, 10);//Génère un entier compris entre 0 et 9
            int resultat3a = multipl3a * multipl3b;
            while (resultat3a == resultat1a || resultat3a == resultat2a)
            {
                multipl3a = aleatoire.Next(1, 10);
                multipl3b = aleatoire.Next(1, 10);//Génère un entier compris entre 0 et 9
                resultat3a = multipl3a * multipl3b;
            }
            string resultat3 = resultat3a.ToString();
            TxtResultat3.Text = resultat3;
            TxtResultat3.Margin = new Thickness(jeu.CaseResultat3.y * 50, jeu.CaseResultat3.x * 50, 0, 0);
            TxtResultat3.Height = 50;
            TxtResultat3.FontSize = 40;
            TxtResultat3.FontFamily = new FontFamily("Showcard Gothic");
            TxtResultat3.Foreground = Brushes.Blue;
            TxtResultat3.Width = 50;
            TxtResultat3.TextAlignment = TextAlignment.Center;
            TxtResultat3.Visibility = Visibility.Visible;
            cnv_resultats.Children.Add(TxtResultat3);


        }


        private void CheckerCase()
        {
            bool test_superpose1 = (jeu.Personnage.x == jeu.CaseResultat1.x) && (jeu.Personnage.y == jeu.CaseResultat1.y);
            bool test_superpose2 = (jeu.Personnage.x == jeu.CaseResultat2.x) && (jeu.Personnage.y == jeu.CaseResultat2.y);
            bool test_superpose3 = (jeu.Personnage.x == jeu.CaseResultat3.x) && (jeu.Personnage.y == jeu.CaseResultat3.y);

            if (test_superpose1)
            {
                //int result1= int.Parse(txtChiffre1.Text) * int.Parse(txtChiffre2.Text);
                //string resutstring1 = result1.ToString();
                txtResultat.Text = TxtResultat1.Text; //Voir cahier 28-01-2019
                txtResultat.Foreground = Brushes.BlanchedAlmond;
                TxtResultat1.Background = Brushes.SpringGreen;
                txtResultat.Foreground = Brushes.SpringGreen;
                txtVraiFaux.Text = "VRAI";
                txtVraiFaux.Background = Brushes.SpringGreen;
                txtVraiFaux.Foreground = Brushes.BlanchedAlmond;
            }


            else if (test_superpose2)
            {
                txtResultat.Text = TxtResultat2.Text; //Voir cahier 28-01-2019
                txtResultat.Foreground = Brushes.Red;
                TxtResultat2.Background = Brushes.Red;
                TxtResultat2.Foreground = Brushes.BlanchedAlmond;
                txtVraiFaux.Text = "FAUX";
                txtVraiFaux.Background = Brushes.Red;
                txtVraiFaux.Foreground = Brushes.BlanchedAlmond;
                //MessageErreurCase();

            }
            else if (test_superpose3)
            {
                txtResultat.Text = TxtResultat3.Text;
                txtResultat.Foreground = Brushes.Red;
                txtVraiFaux.Text = "FAUX";
                TxtResultat3.Background = Brushes.Red;
                TxtResultat3.Foreground = Brushes.BlanchedAlmond;
                txtVraiFaux.Background = Brushes.Red;
                txtVraiFaux.Foreground = Brushes.BlanchedAlmond;
                //MessageErreurCase();

            }
            else
            {
                txtResultat.Text = "??";
                txtVraiFaux.Text = "Vide";
                txtVraiFaux.Background = Brushes.LightBlue;
                txtVraiFaux.Foreground = Brushes.Black;
                txtResultat.Foreground = Brushes.LightBlue;
            }
        }

        /*private void MessageErreurCase()
        {
            Random aleatoire = new Random();
            int messageNum1 = aleatoire.Next(1, 5);

            switch (messageNum1)
            {
                case 1:
                    MessageBox.Show("AÏE! Non, c'est le mauvais résultat!");
                    break;

                case 2:
                    MessageBox.Show("Hé non, ce n'est pas ça, une petite révision s'impose.");
                    break;

                case 3:
                    MessageBox.Show("Tu dois confondre Larkissa car ce n'est pas le bon résultat");
                    break;
                case 4:
                    MessageBox.Show("C'est pas la bonne case, essaye-en une autre :) ");
                    break;
            }

        }*/

        private void Bouton_AutreCalcul_Click(object sender, RoutedEventArgs e)
        {
            jeu.Restart();
            DessinerCasesResultats();
            Redessiner();
        }
    }
}








