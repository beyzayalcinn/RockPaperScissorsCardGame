using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

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

    SetupUI();

}

        void SetupUI()
        {
            // FORM
            this.Text = "Rock Paper Scissors";
            this.Width = 1100;
            this.Height = 720;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(20, 20, 35);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TITLE
            titleLabel = new Label();
            titleLabel.Text = "ROCK PAPER SCISSORS";
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Arial", 28, FontStyle.Bold);
            titleLabel.AutoSize = true;

            // PLAYER TEXT
            playerText = new Label();
            playerText.Text = "YOU";
            playerText.ForeColor = Color.White;
            playerText.Font = new Font("Arial", 18, FontStyle.Bold);
            playerText.AutoSize = true;

            // COMPUTER TEXT
            computerText = new Label();
            computerText.Text = "COMPUTER";
            computerText.ForeColor = Color.White;
            computerText.Font = new Font("Arial", 18, FontStyle.Bold);
            computerText.AutoSize = true;

            // PLAYER CARD
            playerCard = new Label();
            playerCard.Text = "❓";
            playerCard.ForeColor = Color.White;
            playerCard.BackColor = Color.FromArgb(35, 35, 55);
            playerCard.Font = new Font("Segoe UI Emoji", 90);
            playerCard.AutoSize = false;
            playerCard.Size = new Size(180, 180);
            playerCard.TextAlign = ContentAlignment.MiddleCenter;

            // COMPUTER CARD
            computerCard = new Label();
            computerCard.Text = "❓";
            computerCard.ForeColor = Color.White;
            computerCard.BackColor = Color.FromArgb(35, 35, 55);
            computerCard.Font = new Font("Segoe UI Emoji", 90);
            computerCard.AutoSize = false;
            computerCard.Size = new Size(180, 180);
            computerCard.TextAlign = ContentAlignment.MiddleCenter;

            // ROCK BUTTON
            rockBtn = new Button();
            rockBtn.Text = "🪨 Rock";
            rockBtn.Size = new Size(150, 60);
            rockBtn.Location = new Point(200, 470);
            rockBtn.BackColor = Color.FromArgb(70, 130, 180);
            rockBtn.ForeColor = Color.White;
            rockBtn.FlatStyle = FlatStyle.Flat;
            rockBtn.FlatAppearance.BorderSize = 0;
            rockBtn.Font = new Font("Arial", 12, FontStyle.Bold);
            rockBtn.Click += RockBtn_Click;

            // PAPER BUTTON
            paperBtn = new Button();
            paperBtn.Text = "📄 Paper";
            paperBtn.Size = new Size(150, 60);
            paperBtn.Location = new Point(430, 470);
            paperBtn.BackColor = Color.FromArgb(60, 179, 113);
            paperBtn.ForeColor = Color.White;
            paperBtn.FlatStyle = FlatStyle.Flat;
            paperBtn.FlatAppearance.BorderSize = 0;
            paperBtn.Font = new Font("Arial", 12, FontStyle.Bold);
            paperBtn.Click += PaperBtn_Click;

            // SCISSORS BUTTON
            scissorsBtn = new Button();
            scissorsBtn.Text = "✂️ Scissors";
            scissorsBtn.Size = new Size(150, 60);
            scissorsBtn.Location = new Point(660, 470);
            scissorsBtn.BackColor = Color.FromArgb(220, 20, 60);
            scissorsBtn.ForeColor = Color.White;
            scissorsBtn.FlatStyle = FlatStyle.Flat;
            scissorsBtn.FlatAppearance.BorderSize = 0;
            scissorsBtn.Font = new Font("Arial", 12, FontStyle.Bold);
            scissorsBtn.Click += ScissorsBtn_Click;

            // RESULT LABEL
            resultLabel = new Label();
            resultLabel.Text = "Choose your move!";
            resultLabel.ForeColor = Color.Gold;
          resultLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            resultLabel.AutoSize = true;

            // SCORE LABEL
            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.ForeColor = Color.White;
            scoreLabel.Font = new Font("Arial", 13, FontStyle.Bold);
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(30, 20);

            // DECK LABEL
            deckLabel = new Label();
            deckLabel.Text = "🎴 Deck Cards: 10";
            deckLabel.ForeColor = Color.Cyan;
            deckLabel.Font = new Font("Arial", 13, FontStyle.Bold);
            deckLabel.AutoSize = true;
            deckLabel.Location = new Point(30, 55);

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

            // TITLE CENTER
            titleLabel.Location = new Point(
                (this.ClientSize.Width - titleLabel.Width) / 2,
                25
            );

            // PLAYER AREA
            playerCard.Location = new Point(220, 170);

            playerText.Location = new Point(
                playerCard.Left +
                (playerCard.Width - playerText.Width) / 2,
                120
            );

            // COMPUTER AREA
            computerCard.Location = new Point(700, 170);

            computerText.Location = new Point(
                computerCard.Left +
                (computerCard.Width - computerText.Width) / 2,
                120
            );

            // RESULT CENTER
            resultLabel.Location = new Point(
                (this.ClientSize.Width - resultLabel.Width) / 2,
                590
            );
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

            // Deck azalt
            deckCount--;

            deckLabel.Text =
                "🎴 Deck Cards: " + deckCount;

            // Player card
            playerCard.Text = playerChoice;

            // Computer card
            string computerChoice =
                cards[rnd.Next(cards.Length)];

            computerCard.Text = computerChoice;

            // DRAW
            if (playerChoice == computerChoice)
            {
                resultLabel.Text = "DRAW!";
                resultLabel.ForeColor = Color.LightBlue;
            }

// WIN
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

    QuestionForm qf = new QuestionForm();
    qf.ShowDialog();
}

            // LOSE
            else
            {
                resultLabel.Text = "YOU LOSE!";
                resultLabel.ForeColor = Color.Red;
            }

            // RESULT LABEL CENTER
            resultLabel.Left =
                (this.ClientSize.Width - resultLabel.Width) / 2;
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