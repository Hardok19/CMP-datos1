using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_CLIENTE
{
    public partial class Form1 : Form
    {
        private Label labelCurrentSong;
        private Label labelArtist;
        private Button buttonUpVote;
        private Button buttonDownVote;
        private ListView listViewSongs;
        private Label labelConnectionStatus;
        private System.Windows.Forms.Timer pollingTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            ConfigurePollingTimer();
        }

        private void InitializeCustomComponents()
        {
            
            labelCurrentSong = new Label
            {
                Text = "Canción Actual: [Nombre de la Canción]",
                Dock = DockStyle.Top,
                ForeColor = Color.White,
                Height = 25,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0)
            };

            
            labelArtist = new Label
            {
                Text = "Artista: [Nombre del Artista]",
                Dock = DockStyle.Top,
                ForeColor = Color.White,
                Height = 25,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 0, 0)
            };

            
            listViewSongs = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            listViewSongs.Columns.Add("Canción", -2, HorizontalAlignment.Left);
            listViewSongs.Columns.Add("Artista", -2, HorizontalAlignment.Left);
            listViewSongs.Columns.Add("Up-votes", -2, HorizontalAlignment.Left);
            listViewSongs.Columns.Add("Down-votes", -2, HorizontalAlignment.Left);

            // Botones de votación
            buttonUpVote = new Button
            {
                Text = "Up-vote",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            buttonUpVote.Click += ButtonUpVote_Click;

            buttonDownVote = new Button
            {
                Text = "Down-vote",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            buttonDownVote.Click += ButtonDownVote_Click;

            
            labelConnectionStatus = new Label
            {
                Text = "Desconectado",
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            
            Controls.Add(listViewSongs); 
            Controls.Add(labelCurrentSong); 
            Controls.Add(labelArtist);
            Controls.Add(buttonUpVote); 
            Controls.Add(buttonDownVote);
            Controls.Add(labelConnectionStatus); 

            ApplyStyles();
        }

        private void ConfigurePollingTimer()
        {
            pollingTimer = new System.Windows.Forms.Timer
            {
                Interval = 5000 
            };
            pollingTimer.Tick += PollingTimer_Tick;
            pollingTimer.Start();
        }

        private void PollingTimer_Tick(object sender, EventArgs e)
        {
            // Lógica de polling al servidor
        }

        private void ButtonUpVote_Click(object sender, EventArgs e)
        {
            // Lógica para enviar up-vote al servidor
        }

        private void ButtonDownVote_Click(object sender, EventArgs e)
        {
            // Lógica para enviar down-vote al servidor
        }

        private void ApplyStyles()
        {
            Color darkGreenColor = ColorTranslator.FromHtml("#1DB954");

            // Estilos aplicados
            labelCurrentSong.BackColor = darkGreenColor;
            labelCurrentSong.ForeColor = Color.White;
            labelArtist.BackColor = darkGreenColor;
            labelArtist.ForeColor = Color.White;
            buttonUpVote.BackColor = darkGreenColor;
            buttonUpVote.ForeColor = Color.White;
            buttonDownVote.BackColor = darkGreenColor;
            buttonDownVote.ForeColor = Color.White;
            listViewSongs.BackColor = ColorTranslator.FromHtml("#121212");
            listViewSongs.ForeColor = Color.White;
            labelConnectionStatus.ForeColor = darkGreenColor;
            labelConnectionStatus.BackColor = ColorTranslator.FromHtml("#181818");

            this.BackColor = ColorTranslator.FromHtml("#181818");
        }
    }
}