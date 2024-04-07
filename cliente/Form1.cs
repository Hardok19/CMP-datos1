using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using loggings;
using IniA;
using Sockets;
using votes;
using System.CodeDom;
using Json;


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
        public ClientSocket clientSock;
        private static readonly ILogger<Form1> _logger = Logger.CreateLogger<Form1>();
        private string estado = "Desconectado";
        private string cancionValue = "";
        private string artistaValue = "";
        private int upVotesValue = 0; 
        private int downVotesValue = 0; 


        public Form1()
        {
            
            InitializeComponent();
            InitializeCustomComponents();
            start(clientSock);

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
            

            // Declarar variables para cada columna
            string cancionValue = "";
            string artistaValue = "";
            int upVotesValue = 0; 
            int downVotesValue = 0; 

            // Suscribir al evento SelectedIndexChanged
            listViewSongs.SelectedIndexChanged += (sender, e) =>{
                // Verificar si se ha seleccionado una fila
                if (listViewSongs.SelectedItems.Count > 0)
                {
                    // Obtener el índice de la fila seleccionada
                    int selectedIndex = listViewSongs.SelectedIndices[0];

                    // Verificar el número de subelementos en la fila seleccionada
                    int numSubItems = listViewSongs.Items[selectedIndex].SubItems.Count;
                    _logger.LogInformation(numSubItems.ToString());

                    // Verificar si hay suficientes subelementos en la fila seleccionada
                    if (listViewSongs.Items[selectedIndex].SubItems.Count >= 4){
                        // Obtener los valores de cada columna en la fila seleccionada
                        string cancionValue = listViewSongs.Items[selectedIndex].SubItems[0].Text;
                        string artistaValue = listViewSongs.Items[selectedIndex].SubItems[1].Text;
                        int upVotesValue = int.Parse(listViewSongs.Items[selectedIndex].SubItems[2].Text);
                        int downVotesValue = int.Parse(listViewSongs.Items[selectedIndex].SubItems[3].Text);


                        this.cancionValue = cancionValue;
                        this.artistaValue = artistaValue;
                        this.upVotesValue = upVotesValue;
                        this.downVotesValue = downVotesValue;

                    }
                };
            };

            // Botones de votaci�n
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
                Text = estado,
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

        private void ButtonUpVote_Click(object sender, EventArgs e)
        {
            string cancionvalue;
            this.upVotesValue += 1;


            cancionvalue = this.cancionValue;
            Vote vote = new Vote();
            vote.Up(cancionvalue, clientSock, listViewSongs);
        }

        private void ButtonDownVote_Click(object sender, EventArgs e)
        {
            string cancionvalue;
            this.downVotesValue += 1;


            cancionvalue = this.cancionValue;
            Vote vote = new Vote();
            vote.Down(cancionvalue, clientSock, listViewSongs);
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

        async Task start(ClientSocket clientSock){
            try{
                string Address;
                int port;
                var config = new IniFile("config.ini"); //Variable de ruta archivo configuracion
                config.Write("Address", "localhost");
                config.Write("Port", "1235");
                Address = config.Read("Address");
                port = Convert.ToInt16(config.Read("Port"));

                

                bool connected = false;

                while (!connected){
                    try
                    {
                        clientSock = new ClientSocket(Address, port, 3000, 3000);
                        this.clientSock = clientSock;
                        connected = true;
                        labelConnectionStatus.Text = "Conectado";
                    }
                    catch (Exception)
                    {
                        labelConnectionStatus.Text = "Desconectado";
                        // Si no se pudo conectar, esperar un tiempo antes de intentar de nuevo
                        _logger.LogError("No se pudo conectar. Reintentando en 5 segundos...");
                        await Task.Delay(5000); // Esperar 5 segundos antes de reintentar
                    }
                }

                _logger.LogInformation("Conexión establecida correctamente");

                Queue<Song> savequeue = new Queue<Song>();



                // Crear un CancellationTokenSource para poder cancelar el polling si es necesario
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                // Iniciar el polling en un hilo aparte
                Task pollingTask = Poll.Request(this.clientSock, cancellationTokenSource.Token, listViewSongs, savequeue, labelArtist, labelCurrentSong);


                // Esperar a que el polling termine
                await pollingTask;
            }
            catch(Exception ex){
                _logger.LogError(ex, "Ocurrió un error no controlado: {ErrorMessage}", ex.Message);
            }
        }


    }
    
}