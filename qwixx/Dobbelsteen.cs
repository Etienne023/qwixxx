using System;
using System.Drawing;
using System.Windows.Forms;

public class Dobbelsteen : Control
{
    public int waarde; // Current value of the dice
    private Brush kleur; // Color of the dice
    private Random random; // Random number generator

    // Event handler for when the dice is clicked
    public event EventHandler DiceClicked;

    // Constructor for Dobbelsteen class
    public Dobbelsteen(Brush kleur)
    {
        this.kleur = kleur;
        this.random = new Random();
        this.Click += Dobbelsteen_Click; // Attach click event handler
    }

    // Event handler for when the dice is clicked
    private void Dobbelsteen_Click(object sender, EventArgs e)
    {
        OnDiceClicked(EventArgs.Empty); // Raise DiceClicked event
    }

    // Method to raise the DiceClicked event
    public void OnDiceClicked(EventArgs e)
    {
        EventHandler handler = DiceClicked;
        handler?.Invoke(this, e);
    }

    // Method to set the value of the dice and trigger a repaint
    public void Rol(int value)
    {
        waarde = value;
        Invalidate(); // Request a repaint of the control
    }

    // Method to paint the dice control
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawDice(e); // Draw the dice
    }

    // Method to draw the dice
    private void DrawDice(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        int size = 95; // Size of the dice

        // Draw the outline of the dice
        g.DrawRectangle(Pens.Black, 10, 10, size, size);

        // Fill the dice with the selected color
        g.FillRectangle(kleur, 10, 10, size, size);

        // Draw dots based on the value of the dice
        int dotSize = size / 7; // Size of each dot
        int middle = size / 2; // Middle point of the dice

        switch (waarde)
        {
            case 1:
                g.FillEllipse(Brushes.Black, middle - dotSize / 2, middle - dotSize / 2, dotSize, dotSize);
                break;
            case 2:
                g.FillEllipse(Brushes.Black, 20, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, size - 20 - dotSize, dotSize, dotSize);
                break;
            case 3:
                g.FillEllipse(Brushes.Black, 20, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, middle - dotSize / 2, middle - dotSize / 2, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, size - 20 - dotSize, dotSize, dotSize);
                break;
            case 4:
                g.FillEllipse(Brushes.Black, 20, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, 20, size - 20 - dotSize, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, size - 20 - dotSize, dotSize, dotSize);
                break;
            case 5:
                g.FillEllipse(Brushes.Black, 20, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, 20, size - 20 - dotSize, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, size - 20 - dotSize, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, middle - dotSize / 2, middle - dotSize / 2, dotSize, dotSize);
                break;
            case 6:
                g.FillEllipse(Brushes.Black, 20, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, 20, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, 20, middle - dotSize / 2, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, middle - dotSize / 2, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, 20, size - 20 - dotSize, dotSize, dotSize);
                g.FillEllipse(Brushes.Black, size - 20 - dotSize, size - 20 - dotSize, dotSize, dotSize);
                break;
        }
    }

    // Method to get the value of the dice
    public int GetWaarde()
    {
        return waarde;
    }
}
