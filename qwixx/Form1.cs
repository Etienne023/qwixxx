using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace qwixx
{
    public partial class Form1 : Form
    {
        // Declare class variables
        private Speelkaart speelkaart;
        private Dobbelsteen dobbelsteen1;
        private Dobbelsteen dobbelsteen2;
        private Dobbelsteen dobbelsteen3;
        private Dobbelsteen dobbelsteen4;
        private Dobbelsteen dobbelsteen5;
        private Dobbelsteen dobbelsteen6;
        private Random random;
        private List<Dobbelsteen> selectedDice = new List<Dobbelsteen>();
        private bool isUpdatingText = false;
        private int whitePoints = 0;

        // Constructor for Form1
        public Form1()
        {
            InitializeComponent();
            random = new Random();
            InitializeComponents(); // Initialize GUI components
            InitializeSpeelkaart(); // Initialize game board
            GameEnd(); // Check if game has ended
        }

        // Initialize GUI components
        private void InitializeComponents()
        {
            // Initialize dice controls
            dobbelsteen1 = InitializeDice(Brushes.White, 100);
            dobbelsteen2 = InitializeDice(Brushes.White, 210);
            dobbelsteen3 = InitializeDice(Brushes.Red, 320);
            dobbelsteen4 = InitializeDice(Brushes.Blue, 430);
            dobbelsteen5 = InitializeDice(Brushes.Yellow, 540);
            dobbelsteen6 = InitializeDice(Brushes.Green, 650);
        }

        // Initialize individual dice
        private Dobbelsteen InitializeDice(Brush brushColor, int yPos)
        {
            Dobbelsteen dice = new Dobbelsteen(brushColor);
            dice.Location = new Point(808, yPos);
            dice.Size = new Size(90, 90);
            dice.DiceClicked += Dobbelsteen_Clicked;
            this.Controls.Add(dice);
            return dice;
        }

        // Initialize game board
        private void InitializeSpeelkaart()
        {
            speelkaart = new Speelkaart();

            // Set parameters for buttons on game board
            const int buttonWidth = 60;
            const int buttonHeight = 60;
            const int startX = 20;
            const int startY = 30;
            const int paddingX = 5;
            const int paddingY = 5;

            // Create buttons for game board
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    Button btn = speelkaart.GetButton(row, col);
                    btn.Width = buttonWidth;
                    btn.Height = buttonHeight;
                    btn.Left = startX + (buttonWidth + paddingX) * col;
                    btn.Top = startY + (buttonHeight + paddingY) * row;
                    btn.Click += Button_Click;
                    btn.Enabled = false;
                    Controls.Add(btn);
                }
            }
        }

        // Handle roll button click event
        private void btnRol_Click(object sender, EventArgs e)
        {
            // Roll each dice and update game board
            dobbelsteen1.Rol(random.Next(1, 7));
            dobbelsteen2.Rol(random.Next(1, 7));
            dobbelsteen3.Rol(random.Next(1, 7));
            dobbelsteen4.Rol(random.Next(1, 7));
            dobbelsteen5.Rol(random.Next(1, 7));
            dobbelsteen6.Rol(random.Next(1, 7));
            speelkaart.SpeelKaartClick(dobbelsteen1.waarde + dobbelsteen2.waarde);
            GameEnd(); // Check if game has ended
        }

        // Handle individual dice click event
        private void Dobbelsteen_Clicked(object sender, EventArgs e)
        {
            Dobbelsteen clickedDice = sender as Dobbelsteen;

            // Handle dice selection
            if (clickedDice != null)
            {
                if (selectedDice.Contains(clickedDice))
                    selectedDice.Remove(clickedDice);
                else if (selectedDice.Count < 2)
                    selectedDice.Add(clickedDice);

                if (selectedDice.Count == 2)
                {
                    int totalValue = 0;
                    foreach (Dobbelsteen dice in selectedDice)
                        totalValue += dice.waarde;

                    speelkaart.SpeelKaartClick(totalValue);
                    selectedDice.Clear();
                }
            }
            else
            {
                selectedDice.Clear();
            }
        }

        // Handle game board button click event
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                clickedButton.Enabled = false;
                speelkaart.HandleButtonClick(clickedButton);
            }

            updateTotaal();
            UpdateLabels();
            GameEnd(); 
        }

        // Update point labels
        private void UpdateLabels()
        {
            int pointsRed = speelkaart.PointsRulesred(speelkaart.puntenRed);
            lblRed.Text = $"{pointsRed}";

            int pointsBlue = speelkaart.PointsRulesblue(speelkaart.puntenBlue);
            lblBlue.Text = $"{pointsBlue}";

            int pointsYellow = speelkaart.PointsRulesyellow(speelkaart.puntenYellow);
            lblYellow.Text = $"{pointsYellow}";

            int pointsGreen = speelkaart.PointsRulesgreen(speelkaart.puntenGreen);
            lblGreen.Text = $"{pointsGreen}";
        }

        // Update total points and white points
        private void updateTotaal()
        {
            if (!isUpdatingText)
            {
                isUpdatingText = true;
                int totalPoints = speelkaart.getPointsTotal();
                if (!msw1Btn.Enabled)
                    totalPoints -= 5;

                if (!msw2Btn.Enabled)
                    totalPoints -= 5;

                if (!msw3Btn.Enabled)
                    totalPoints -= 5;

                if (!msw4Btn.Enabled)
                    totalPoints -= 5;

                lblPoints.Text = $"{totalPoints}";
                lblWhite.Text = $"{whitePoints}";
                isUpdatingText = false;
            }
        }

        // Handle white point button click events
        private void msw1Btn_Click(object sender, EventArgs e)
        {
            msw1Btn.Enabled = false;
            whitePoints -= 5;
            updateTotaal();
            GameEnd();
        }

        private void msw2Btn_Click(object sender, EventArgs e)
        {
            msw2Btn.Enabled = false;
            whitePoints -= 5;
            updateTotaal();
            GameEnd();
        }

        private void msw3Btn_Click(object sender, EventArgs e)
        {
            msw3Btn.Enabled = false;
            whitePoints -= 5;
            updateTotaal();
            GameEnd();
        }

        private void msw4Btn_Click(object sender, EventArgs e)
        {
            msw4Btn.Enabled = false;
            whitePoints -= 5;
            updateTotaal();
            GameEnd();
        }

        // Check if game has ended
        private void GameEnd()
        {
            // If all white point buttons are disabled, end game
            if (!msw1Btn.Enabled && !msw2Btn.Enabled && !msw3Btn.Enabled && !msw4Btn.Enabled)
            {
                MessageBox.Show("bekijk de punten om te zien wie gewonnen heeft!");

                // Disable all buttons on the form
                foreach (Control control in Controls)
                {
                    if (control is Button btn)
                        btn.Enabled = false;
                }
            }

            // If end game button on game board is clicked, end game
            if (speelkaart.IsEndGameButtonClicked())
            {
                MessageBox.Show("bekijk de punten om te zien wie gewonnen heeft!");

                // Disable all buttons on the form
                foreach (Control control in Controls)
                {
                    if (control is Button btn)
                        btn.Enabled = false;
                }
            }
        }
    }
}
