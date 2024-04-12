package org.example.prueba1;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.scene.layout.BorderPane;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;
import javafx.scene.control.Label;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.example.server.CustomServer;
import org.example.server.Main;


public class Controller implements Initializable {
    private static final Logger logger = LogManager.getLogger(Controller.class);
    @FXML
    private ImageView searchImageView;

    @FXML
    private ImageView userImageView;

    @FXML
    private ImageView loveImageView;

    @FXML
    private ImageView shuffleImageView;

    @FXML
    private ImageView skipToStartImageView;

    @FXML
    private ImageView playImageView;

    @FXML
    private ImageView endImageView;

    @FXML
    private ImageView repeatImageView;

    @FXML
    private VBox songList;
    @FXML
    private BorderPane borderPane;
    @FXML
    private VBox vBox1;
    @FXML
    private HBox hBox1;
    @FXML
    private HBox hBox3;
    @FXML
    private Button communityPlaylistButton;
    @FXML
    private  Button invisibleButton;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        // Cargar las imágenes desde el código
        searchImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/icsearch.png")));
        userImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/user.png")));
        loveImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_love.png")));
        shuffleImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_shuffle.png")));
        skipToStartImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_skip_to_start.png")));
        playImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_play.png")));
        endImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_end.png")));
        repeatImageView.setImage(new Image(getClass().getResourceAsStream("/org/example/prueba1/img/ic_repeat.png")));
        // Cargar la hoja de estilo CSS desde el código
        borderPane.getStylesheets().add(getClass().getResource("/org/example/prueba1/css/style.css").toExternalForm());
        vBox1.getStylesheets().add(getClass().getResource("/org/example/prueba1/css/style.css").toExternalForm());
        hBox1.getStylesheets().add(getClass().getResource("/org/example/prueba1/css/style.css").toExternalForm());
        communityPlaylistButton.getStylesheets().add(getClass().getResource("/org/example/prueba1/css/style.css").toExternalForm());
        hBox3.getStylesheets().add(getClass().getResource("/org/example/prueba1/css/style.css").toExternalForm());


        communityPlaylistButton.setOnAction(event -> handleButtonAction(event));
        invisibleButton.setOnAction(event -> play(event));




        SONG[] songs = new SONG[0];
        for (SONG song : songs) {
            try {
                FXMLLoader loader = new FXMLLoader(getClass().getResource("/org/example/prueba1/song.fxml"));
                HBox songItem = loader.load();
                SongController songController = loader.getController();
                songController.setSong(song);

                // Crear un VBox artificialmente
                VBox songList = new VBox();

                // Acceder al VBox dentro del HBox
                HBox hbox = new HBox();
                hbox.getChildren().add(songList);

                // Crear un VBox artificialmente y agregarlo como hijo del HBox
                VBox songListVBox = new VBox();
                hbox.getChildren().add(songListVBox);

                // Crear un HBox artificialmente
                HBox newSongItem = new HBox();
                // Agregar elementos al HBox (por ejemplo, etiquetas)
                Label titleLabel = new Label("Cancion 1");
                Label artistLabel = new Label("Artista 2");
                newSongItem.getChildren().addAll(titleLabel, artistLabel);

                // Agregar el elemento al VBox dentro del HBox
                songListVBox.getChildren().add(songItem);
            } catch (IOException e) {
                logger.error(e);
            }
        }

    }

    private void handleButtonAction(javafx.event.ActionEvent event) {
        Thread thread = new Thread(() -> {
            String[] args;
            args = new String[0]; // Initialize args
            CustomServer.main(args);

        });
        thread.start();
    }

    private void play(javafx.event.ActionEvent event){
        Thread thread = new Thread(() -> {
            Main.play();

        });
        thread.start();

    }

}
