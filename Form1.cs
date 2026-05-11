using System;
using System.Drawing;
using System.Windows.Forms;

namespace RockPaperCardGame
{
    public partial class Form1 : Form
    {
        Label titleLabel = null!;

        Label playerText = null!;
        Label computerText = null!;

        Label playerCard = null!;
        Label computerCard = null!;

        Label deckLabel = null!;
        Label resultLabel = null!;
        Label scoreLabel = null!;

        Button rockBtn = null!;
        Button paperBtn = null!;
        Button scissorsBtn = null!;

        int score = 0;
        int deckCount = 10;

        Random rnd = new Random();

        string[] cards = { "🪨", "📄", "✂️" };

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        void SetupUI()
        {
            // FORM
            this.Text = "Rock Paper Scissors";
            this.Width = 1000;
            this.Height = 650;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 35);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TITLE
            titleLabel = new Label();
            titleLabel.Text = "ROCK PAPER SCISSORS";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 26, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(250, 30);

            // PLAYER TEXT
            playerText = new Label();
            playerText.Text = "YOU";
            playerText.ForeColor = Color.White;
            playerText.Font = new Font("Arial", 14, FontStyle.Bold);
            playerText.AutoSize = true;
            playerText.Location = new Point(180, 120);

            // COMPUTER TEXT
            computerText = new Label();
            computerText.Text = "COMPUTER";
            computerText.ForeColor = Color.White;
            computerText.Font = new Font("Arial", 14, FontStyle.Bold);
            computerText.AutoSize = true;
            computerText.Location = new Point(680, 120);

            // PLAYER CARD
            playerCard = new Label();
            playerCard.Text = "❓";
            playerCard.ForeColor = Color.White;
            playerCard.Font = new Font("Segoe UI Emoji", 70);
            playerCard.AutoSize = true;
            playerCard.Location = new Point(200, 180);

            // COMPUTER CARD
            computerCard = new Label();
            computerCard.Text = "❓";
            computerCard.ForeColor = Color.White;
            computerCard.Font = new Font("Segoe UI Emoji", 70);
            computerCard.AutoSize = true;
            computerCard.Location = new Point(720, 180);

            // ROCK BUTTON
            rockBtn = new Button();
            rockBtn.Text = "🪨 Rock";
            rockBtn.Size = new Size(140, 55);
            rockBtn.Location = new Point(120, 420);
            rockBtn.BackColor = Color.FromArgb(70, 130, 180);
            rockBtn.ForeColor = Color.White;
            rockBtn.FlatStyle = FlatStyle.Flat;
            rockBtn.Font = new Font("Arial", 11, FontStyle.Bold);
            rockBtn.Click += RockBtn_Click;

            // PAPER BUTTON
            paperBtn = new Button();
            paperBtn.Text = "📄 Paper";
            paperBtn.Size = new Size(140, 55);
            paperBtn.Location = new Point(300, 420);
            paperBtn.BackColor = Color.FromArgb(60, 179, 113);
            paperBtn.ForeColor = Color.White;
            paperBtn.FlatStyle = FlatStyle.Flat;
            paperBtn.Font = new Font("Arial", 11, FontStyle.Bold);
            paperBtn.Click += PaperBtn_Click;

            // SCISSORS BUTTON
            scissorsBtn = new Button();
            scissorsBtn.Text = "✂️ Scissors";
            scissorsBtn.Size = new Size(140, 55);
            scissorsBtn.Location = new Point(480, 420);
            scissorsBtn.BackColor = Color.FromArgb(220, 20, 60);
            scissorsBtn.ForeColor = Color.White;
            scissorsBtn.FlatStyle = FlatStyle.Flat;
            scissorsBtn.Font = new Font("Arial", 11, FontStyle.Bold);
            scissorsBtn.Click += ScissorsBtn_Click;

            // RESULT LABEL
            resultLabel = new Label();
            resultLabel.Text = "Choose your move!";
            resultLabel.ForeColor = Color.Gold;
            resultLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(320, 520);

            // SCORE LABEL
            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.ForeColor = Color.White;
            scoreLabel.Font = new Font("Arial", 13, FontStyle.Bold);
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(30, 30);

            // DECK LABEL
            deckLabel = new Label();
            deckLabel.Text = "🎴 Deck Cards: 10";
            deckLabel.ForeColor = Color.Cyan;
            deckLabel.Font = new Font("Arial", 13, FontStyle.Bold);
            deckLabel.AutoSize = true;
            deckLabel.Location = new Point(30, 70);

            // ADD CONTROLS
            this.Controls.Add(titleLabel);

            this.Controls.Add(playerText);
            this.Controls.Add(computerText);

            this.Controls.Add(playerCard);
            this.Controls.Add(computerCard);

            this.Controls.Add(rockBtn);
            this.Controls.Add(paperBtn);
            this.Controls.Add(scissorsBtn);

            this.Controls.Add(resultLabel);
            this.Controls.Add(scoreLabel);
            this.Controls.Add(deckLabel);
        }

        void PlayGame(string playerChoice)
        {
            // Deck kontrolü
            if (deckCount <= 0)
            {
                MessageBox.Show(
                    "No cards left in your deck!",
                    "Deck Empty"
                );
                return;
            }

        
            deckCount--;
            deckLabel.Text = "🎴 Deck Cards: " + deckCount;

            playerCard.Text = playerChoice;

            string computerChoice = cards[rnd.Next(cards.Length)];
            computerCard.Text = computerChoice;

           
            if (playerChoice == computerChoice)
            {
                resultLabel.Text = "DRAW!";
                resultLabel.ForeColor = Color.LightBlue;
            }

           
            else if (
                (playerChoice == "🪨" && computerChoice == "✂️") ||
                (playerChoice == "📄" && computerChoice == "🪨") ||
                (playerChoice == "✂️" && computerChoice == "📄")
            )
            {
                resultLabel.Text = "YOU WIN!";
                resultLabel.ForeColor = Color.Lime;

                score++;
                scoreLabel.Text = "Score: " + score;

                MessageBox.Show(
                    "YOU WIN! 🎉\nYou are being redirected to the question screen.",
                    "Victory"
                );
            }

         
            else
            {
                resultLabel.Text = "YOU LOSE!";
                resultLabel.ForeColor = Color.Red;
            }
        }

        void RockBtn_Click(object? sender, EventArgs e)
        {
            PlayGame("🪨");
        }

        void PaperBtn_Click(object? sender, EventArgs e)
        {
            PlayGame("📄");
        }

        void ScissorsBtn_Click(object? sender, EventArgs e)
        {
            PlayGame("✂️");
        }
    }
}