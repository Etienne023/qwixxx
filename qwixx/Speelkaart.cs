using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qwixx
{
    internal class Speelkaart
    {
        private Button[,] buttons;
        public int puntenRed = 0;
        public int puntenBlue = 0;
        public int puntenYellow = 0;
        public int puntenGreen = 0;
        public int totalPoints = 0;

        // Constructor for Speelkaart class
        public Speelkaart()
        {
            // Initialize the buttons array and set up the colors and values
            Color[] colors = { Color.Red, Color.Yellow, Color.Green, Color.Blue };
            buttons = new Button[4, 11];

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    buttons[row, col] = new Button();
                    buttons[row, col].BackColor = colors[row];
                    if (col < 11)
                    {
                        buttons[row, col].Text = (row < 2 ? col + 2 : 12 - col).ToString();
                    }
                }
            }
        }

        // Handle the click event on the game board
        public void SpeelKaartClick(int totalWaarde)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    Button btn = buttons[row, col];

                    // Parse the button text to get its value
                    int buttonValue;
                    if (int.TryParse(btn.Text, out buttonValue))
                    {
                        // Check if the button value matches the totalWaarde
                        if (buttonValue == totalWaarde)
                        {
                            btn.Enabled = true;
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderSize = 3; // Set border size
                            btn.FlatAppearance.BorderColor = Color.Black; // Set border color
                        }
                        else
                        {
                            btn.Enabled = false;
                            // Reset border for disabled buttons
                            btn.FlatStyle = FlatStyle.Standard; // Reset border style
                            btn.FlatAppearance.BorderSize = 0; // Reset border size
                        }
                    }
                }
            }
        }

        // Handle the click event on a button in the game board
        public void HandleButtonClick(Button clickedButton)
        {
            Color buttonColor = clickedButton.BackColor;

            if (buttonColor == Color.Red)
                puntenRed++;
            else if (buttonColor == Color.Blue)
                puntenBlue++;
            else if (buttonColor == Color.Yellow)
                puntenYellow++;
            else if (buttonColor == Color.Green)
                puntenGreen++;

            int row = -1, col = -1;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (buttons[i, j] == clickedButton)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1) break;
            }

            // Disable the clicked button and display a different cross
            clickedButton.Enabled = false;
            clickedButton.Text = "✖";
            clickedButton.Font = new Font(clickedButton.Font.FontFamily, clickedButton.Font.Size * 2);
            clickedButton.ForeColor = Color.Black;

            // Disable buttons in the same row up to the clicked button
            for (int j = 0; j < col; j++)
            {
                Button leftButton = buttons[row, j];
                leftButton.Enabled = false;
                if (leftButton.Text != "✖")
                {
                    leftButton.Text = "✖";
                    leftButton.Font = new Font(leftButton.Font.FontFamily, leftButton.Font.Size);
                    leftButton.ForeColor = Color.Black;
                }
            }

            // Disable buttons in other rows
            for (int i = 0; i < 4; i++)
            {
                if (i != row)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        Button otherButton = buttons[i, j];
                        otherButton.Enabled = false;
                    }
                }
            }
        }

        // Calculate points based on the rules for the red category
        public int PointsRulesred(int puntenred)
        {
            if (puntenred <= 1)
            {
                return puntenred;
            }
            else
            {
                int totalPointsred = 1;
                for (int i = 2; i <= puntenred; i++)
                {
                    totalPointsred += i;
                }
                return totalPointsred;
            }
        }

        // Calculate points based on the rules for the blue category
        public int PointsRulesblue(int puntenblue)
        {
            if (puntenblue <= 1)
            {
                return puntenblue;
            }
            else
            {
                int totalPointsblue = 1;
                for (int i = 2; i <= puntenblue; i++)
                {
                    totalPointsblue += i;
                }
                return totalPointsblue;
            }
        }

        // Calculate points based on the rules for the yellow category
        public int PointsRulesyellow(int puntenyellow)
        {
            if (puntenyellow <= 1)
            {
                return puntenyellow;
            }
            else
            {
                int totalPointsyellow = 1;
                for (int i = 2; i <= puntenyellow; i++)
                {
                    totalPointsyellow += i;
                }
                return totalPointsyellow;
            }
        }

        // Calculate points based on the rules for the green category
        public int PointsRulesgreen(int puntengreen)
        {
            if (puntengreen <= 1)
            {
                return puntengreen;
            }
            else
            {
                int totalPointsgreen = 1;
                for (int i = 2; i <= puntengreen; i++)
                {
                    totalPointsgreen += i;
                }
                return totalPointsgreen;
            }
        }

        // Check if the end game button is clicked
        public bool IsEndGameButtonClicked()
        {
            int count = 0;
            if (buttons[0, 10].Text == "✖")
                count++;
            if (buttons[1, 10].Text == "✖")
                count++;
            if (buttons[2, 10].Text == "✖")
                count++;
            if (buttons[3, 10].Text == "✖")
                count++;

            return count >= 2;
        }

        // Calculate the total points
        public int getPointsTotal()
        {
            int totalPoints = PointsRulesred(puntenRed) + PointsRulesblue(puntenBlue) + PointsRulesyellow(puntenYellow) + PointsRulesgreen(puntenGreen);
            return totalPoints;
        }

        // Get a button from the game board
        public Button GetButton(int row, int col)
        {
            return buttons[row, col];
        }
    }
}
