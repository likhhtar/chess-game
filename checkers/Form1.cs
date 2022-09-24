using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace checkers
{
    public partial class Form1 : Form
    {

        const int mapSize = 8;
        const int cellSize = 50;

        int currentPlayer;

        Button previousButton;

        bool isMoving;

        int[,] map = new int[mapSize, mapSize];

        Image whiteFigure;
        Image blackFigure;

        public Form1()
        {
            InitializeComponent();

            whiteFigure = new Bitmap(new Bitmap(@"C:\Users\User\source\repos\checkers\images\w.png"), new Size(cellSize - 10, cellSize - 10));
            blackFigure = new Bitmap(new Bitmap(@"C:\Users\User\source\repos\checkers\images\b.png"), new Size(cellSize - 10, cellSize - 10));

            this.Text = "Checkers";

            Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void Init()
        {
            currentPlayer = 1;
            isMoving = false;
            previousButton = null;

            map = new int[mapSize, mapSize]{
                { 0,1,0,1,0,1,0,1},
                { 1,0,1,0,1,0,1,0},
                { 0,1,0,1,0,1,0,1},
                { 0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0},
                { 2,0,2,0,2,0,2,0},
                { 0,2,0,2,0,2,0,2},
                { 2,0,2,0,2,0,2,0}
            };
            CreateMap();
        }

        public void CreateMap()
        {
            this.Width = (mapSize + 1) * cellSize;
            this.Height = (mapSize + 1) * cellSize;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Click += new EventHandler(PressOnFigure);
                    if (map[i, j] == 1)
                        button.Image = whiteFigure;
                    else if(map[i, j] == 2)
                        button.Image = blackFigure;
                    if((i % 2 != 0 && j % 2 == 0) || (i % 2 == 0 && j % 2 != 0))
                    {
                        button.BackColor = Color.DarkGray;
                    }
                    this.Controls.Add(button);
                }
            }
        }

        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;
        }

        public Color GetPreviousFigureColor()
        {
            int previousButtonLocationX = previousButton.Location.X / cellSize;
            int previousButtonLocationY = previousButton.Location.Y / cellSize;
            if ((previousButtonLocationX % 2 != 0 && previousButtonLocationY % 2 == 0) || 
                (previousButtonLocationX % 2 == 0 && previousButtonLocationY % 2 != 0))
                {
                    return Color.DarkGray;
                }
            return Color.White;
        }
        public void PressOnFigure(object sender, EventArgs e)
        {
            if (previousButton != null)
                previousButton.BackColor = GetPreviousFigureColor();

            Button pressedButton = sender as Button;

            if (map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] != 0 &&
                map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == currentPlayer)
                {
                    pressedButton.BackColor = Color.Red;
                    isMoving = true;
                }
            else
            {
                if(isMoving)
                {
                    int forSwap = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                    map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[previousButton.Location.Y / cellSize, previousButton.Location.X / cellSize];
                    map[previousButton.Location.Y / cellSize, previousButton.Location.X / cellSize] = forSwap;
                    pressedButton.Image = previousButton.Image;
                    previousButton.Image = null;
                    isMoving = false;
                    SwitchPlayer();
                }
            }

            previousButton = pressedButton;
        }

}
}