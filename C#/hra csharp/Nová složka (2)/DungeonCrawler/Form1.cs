using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonCrawler
{
    public partial class MainForm : Form
    {
        private Game game;

        public MainForm()
        {
            InitializeComponent();
            game = new Game();
            game.GameOver += Game_GameOver;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            game.Start();
            UpdateUI();
        }
        private void UpdateUI()
        {
            roomNameLabel.Text = game.CurrentRoom.Name;
            roomDescriptionLabel.Text = game.CurrentRoom.Description;

            enemiesListBox.Items.Clear();
            foreach (var enemy in game.CurrentRoom.Enemies)
            {
                enemiesListBox.Items.Add(enemy.Name);
            }

            itemsListBox.Items.Clear();
            foreach (var item in game.CurrentRoom.Items)
            {
                itemsListBox.Items.Add(item.Name);
            }
        }

        private void HandleCommand(string command)
        {
            game.HandleCommand(command);
            UpdateUI();
        }

        private void commandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = commandTextBox.Text.ToLower();
                HandleCommand(command);
                commandTextBox.Text = "";
            }
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            MessageBox.Show("Konec hry.", "Dungeon Crawler", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
