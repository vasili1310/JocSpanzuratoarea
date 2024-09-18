using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JocEducativ
{
    public partial class Form3 : Form
    {
        private Form2 previousForm;

        private Button[] lettersButtons;

        private List<string> words;
        private List<char> alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToList();

        private int wrongLetters;
        private string correctWord;
        private string guessedWord;
        private int attempts = 6;
        private int attemptsLeft;
        private int points;
        private int currentIndex = 0;
        private string[] imagePaths = { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };

        private string filePath = "C:\\Users\\huzea\\Downloads\\csarp\\csarp\\Cuvinte.txt";
        private string imagePath = "C:\\Users\\huzea\\Downloads\\csarp\\csarp\\StadiiFloare";

        /*
        public Form3(Form2 previousForm)
        {
            InitializeComponent();

            words = ReadWordsFromFile(filePath);
            NewGame();

            this.previousForm = previousForm;
        }
        */

        private void NewGame()
        {
            wrongLetters = 0;
            guessedLettersLabel.Text = "";
            Random random = new Random();
            int randomIndex = random.Next(words.Count);
            correctWord = words[randomIndex].ToUpper();
            guessedWord = new string('_', correctWord.Length);

            underscoreLabel.Text = "";
            for (int i = 0; i < correctWord.Length; i++)
            {
                underscoreLabel.Text += "_ ";
            }

            UpdateImage();
            UpdatePoints();

            lettersButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24 };

            Shuffle(alphabet);

            for (int i = 0; i < alphabet.Count && i < lettersButtons.Length; i++)
            {
                lettersButtons[i].Text = alphabet[i].ToString();
                lettersButtons[i].Visible = true;
            }
        }

        private void CheckLetter(char letter)
        {
            bool found = false;

            for (int i = 0; i < correctWord.Length; ++i)
            {
                if (correctWord[i] == letter)
                {
                    guessedWord = guessedWord.Substring(0, i) + letter + guessedWord.Substring(i + 1);
                    found = true;
                }
            }
            if (!found)
            {
                wrongLetters++;
            }

            UpdateWordDisplay();
            UpdateImage();
            UpdatePoints();

            if (guessedWord == correctWord)
            {
                MessageBox.Show("Felicitari! Ai gasit cuvantul: " + correctWord);
                NewGame();
            }
            else if (wrongLetters == 5)
            {
                points = 0;
                punctajLabel.Text = points.ToString();
                MessageBox.Show("Ai pierdut! Cuvantul corect a fost: " + correctWord);
                NewGame();
            }
        }

        private void UpdateWordDisplay()
        {
            guessedLettersLabel.Text = guessedWord;
        }

        private void UpdateImage()
        {
            try
            {
                hangmanPictureBox.Image = Image.FromFile(imagePath + "\\" + (6 - wrongLetters) + ".png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex);
            }

        }

        private void UpdatePoints()
        {
            points = 100 - 4 * wrongLetters;
            punctajLabel.Text = points.ToString();
        }

        private List<string> ReadWordsFromFile (string filePath)
        {
            List<string> words = new List<string>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                words.AddRange(lines);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }

            return words;
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void letterVerifyButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            char letter = button.Text[0];
            CheckLetter(letter);
            button.Hide();
        }

        /*
        private void exitHangmanButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            previousForm.Show();
        }
        */
    }
}
