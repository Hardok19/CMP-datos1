package org.example.prueba1;
import javafx.fxml.FXML;
import javafx.scene.control.Label;
import org.example.server.SongS;


public class SongController {

    @FXML
    private Label titleLabel;

    @FXML
    private Label artistLabel;

    private SONG song;

    public void setSong(SONG song) {
        this.song = song;
        // Actualiza los elementos visuales del controlador con los datos de la canción
        titleLabel.setText(song.getName());
        artistLabel.setText(song.getArtist());
    }
}

