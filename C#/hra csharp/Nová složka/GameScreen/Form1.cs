using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameScreen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeGameScreen();
        }

        private void InitializeGameScreen()
        {
            // Nastavení poměru stran 3:4
            int width = 600;  // Například šířka
            int height = (width * 4) / 3;  // Výška vypočítaná podle poměru stran 3:4

            this.ClientSize = new Size(width, height);
            this.Text = "Herní obrazovka 3:4";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;  // Zamezit maximalizaci okna
            this.Paint += new PaintEventHandler(Form1_Paint);  // Přidání Paint události
        }

        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            // Vypočítání šířky jednoho sloupce
            int columnWidth = this.ClientSize.Width / 3;

            // Vytvoření pera pro kreslení čar
            using (Pen pen = new Pen(Color.Black, 2))
            {
                // Kreslení svislých čar pro rozdělení na tři sloupce
                e.Graphics.DrawLine(pen, columnWidth, 0, columnWidth, this.ClientSize.Height);
                e.Graphics.DrawLine(pen, 2 * columnWidth, 0, 2 * columnWidth, this.ClientSize.Height);

                // --- Levý sloupec: Mřížka 6x4 čtverců ---
                int leftColumnX = 0;
                int squareWidth = columnWidth / 4;
                int squareHeight = this.ClientSize.Height / 6;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int x = leftColumnX + j * squareWidth;
                        int y = i * squareHeight;

                        // Prostor pro název nad čtvercem
                        int titleHeight = squareHeight / 2;
                        e.Graphics.DrawRectangle(pen, x, y, squareWidth, titleHeight);

                        // Samotný čtverec
                        int squareY = y + titleHeight;
                        e.Graphics.DrawRectangle(pen, x, squareY, squareWidth, squareHeight);
                    }
                }

                // --- Prostřední sloupec: Inventář ---
                int middleColumnX = columnWidth;

                // 1. Prostor pro zobrazování položek
                int itemsAreaHeight = (int)(this.ClientSize.Height * 0.75); // 75% výšky pro položky
                int itemSquareSize = columnWidth / 4;
                int rows = itemsAreaHeight / itemSquareSize;
                int cols = 4;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        int x = middleColumnX + j * itemSquareSize;
                        int y = i * itemSquareSize;

                        e.Graphics.DrawRectangle(pen, x, y, itemSquareSize, itemSquareSize);
                    }
                }

                // 2. Prostor pro aktuálně nasazené brnění
                int armorAreaY = itemsAreaHeight;
                int armorAreaHeight = this.ClientSize.Height - itemsAreaHeight; // 25% výšky pro brnění
                int armorAreaWidth = columnWidth;

                // Přidání kontrolních výpisů
                Console.WriteLine($"middleColumnX: {middleColumnX}, armorAreaY: {armorAreaY}, armorAreaWidth: {armorAreaWidth}, armorAreaHeight: {armorAreaHeight}");

                // Kontrola, zda jsou hodnoty správné
                if (middleColumnX >= 0 && armorAreaY >= 0 && armorAreaWidth > 0 && armorAreaHeight > 0)
                {
                    e.Graphics.DrawRectangle(pen, middleColumnX, armorAreaY, armorAreaWidth, armorAreaHeight);
                }
                else
                {
                    Console.WriteLine("Chyba: Neplatné hodnoty pro obdélník brnění.");
                }

                // --- Pravý sloupec: Zatím prázdný pro budoucí využití ---
                // Tady můžete přidat další obsah do pravého sloupce v budoucnosti
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}