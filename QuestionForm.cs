using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RockPaperCardGame
{
    public partial class QuestionForm : Form
    {
        Label questionLabel = null!;
        RadioButton optionA = null!;
        RadioButton optionB = null!;
        RadioButton optionC = null!;
        RadioButton optionD = null!;
        Button submitBtn = null!;
        Label resultLabel = null!;

        List<Question> questions = new List<Question>();
        Question currentQuestion = null!;
        Random rnd = new Random();

        public QuestionForm()
        {
            LoadQuestions();
            SetupUI();
            ShowRandomQuestion();
        }

        void SetupUI()
        {
            this.Text = "Trivia Question";
            this.Width = 800;
            this.Height = 550;
            this.BackColor = Color.FromArgb(20, 20, 35);

            questionLabel = new Label();
            questionLabel.ForeColor = Color.White;
            questionLabel.Font = new Font("Arial", 18, FontStyle.Bold);
            questionLabel.Size = new Size(700, 100);
            questionLabel.Location = new Point(40, 40);

            optionA = new RadioButton();
            optionB = new RadioButton();
            optionC = new RadioButton();
            optionD = new RadioButton();

            foreach (var opt in new[] { optionA, optionB, optionC, optionD })
            {
                opt.ForeColor = Color.White;
                opt.Font = new Font("Arial", 14);
                opt.BackColor = Color.FromArgb(35, 35, 55);
                opt.FlatStyle = FlatStyle.Flat;
                opt.UseVisualStyleBackColor = false;
            }

            optionA.Location = new Point(70, 170);
            optionA.Size = new Size(300, 60);

            optionB.Location = new Point(400, 170);
            optionB.Size = new Size(300, 60);

            optionC.Location = new Point(70, 260);
            optionC.Size = new Size(300, 60);

            optionD.Location = new Point(400, 260);
            optionD.Size = new Size(300, 60);

            submitBtn = new Button();
            submitBtn.Text = "Submit";
            submitBtn.Size = new Size(180, 55);
            submitBtn.Location = new Point(290, 430);
            submitBtn.BackColor = Color.MediumPurple;
            submitBtn.ForeColor = Color.White;
            submitBtn.FlatStyle = FlatStyle.Flat;
            submitBtn.Font = new Font("Arial", 13, FontStyle.Bold);
            submitBtn.Click += SubmitBtn_Click;

            resultLabel = new Label();
            resultLabel.ForeColor = Color.White;
            resultLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(320, 20);

            this.Controls.Add(questionLabel);
            this.Controls.Add(optionA);
            this.Controls.Add(optionB);
            this.Controls.Add(optionC);
            this.Controls.Add(optionD);
            this.Controls.Add(submitBtn);
            this.Controls.Add(resultLabel);
        }

        void LoadQuestions()
        {
            string[] lines = File.ReadAllLines("questions.csv");

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');

                questions.Add(new Question
                {
                    QuestionText = data[0],
                    A = data[1],
                    B = data[2],
                    C = data[3],
                    D = data[4],
                    Correct = data[5]
                });
            }
        }

        void ShowRandomQuestion()
        {
            currentQuestion = questions[rnd.Next(questions.Count)];

            questionLabel.Text = currentQuestion.QuestionText;
            optionA.Text = "A) " + currentQuestion.A;
            optionB.Text = "B) " + currentQuestion.B;
            optionC.Text = "C) " + currentQuestion.C;
            optionD.Text = "D) " + currentQuestion.D;

            optionA.Checked = optionB.Checked =
            optionC.Checked = optionD.Checked = false;
        }

        void SubmitBtn_Click(object? sender, EventArgs e)
        {
            string selected = "";
            RadioButton selectedBtn = null!;

            if (optionA.Checked) { selected = "A"; selectedBtn = optionA; }
            else if (optionB.Checked) { selected = "B"; selectedBtn = optionB; }
            else if (optionC.Checked) { selected = "C"; selectedBtn = optionC; }
            else if (optionD.Checked) { selected = "D"; selectedBtn = optionD; }

            if (selected == "")
            {
                MessageBox.Show("Please select an answer!");
                return;
            }

            bool isCorrect = selected == currentQuestion.Correct;

            optionA.BackColor = Color.FromArgb(35, 35, 55);
            optionB.BackColor = Color.FromArgb(35, 35, 55);
            optionC.BackColor = Color.FromArgb(35, 35, 55);
            optionD.BackColor = Color.FromArgb(35, 35, 55);

            RadioButton correctBtn =
                currentQuestion.Correct == "A" ? optionA :
                currentQuestion.Correct == "B" ? optionB :
                currentQuestion.Correct == "C" ? optionC :
                optionD;

            if (isCorrect)
            {
                selectedBtn.BackColor = Color.Green;
                resultLabel.Text = "Correct 🎉";
                resultLabel.ForeColor = Color.Lime;
            }
            else
            {
                selectedBtn.BackColor = Color.Red;
                correctBtn.BackColor = Color.Green;

                resultLabel.Text = "Wrong ❌";
                resultLabel.ForeColor = Color.Red;
            }

            this.Refresh();
            System.Threading.Thread.Sleep(500);

            this.Tag = isCorrect;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}