package sample;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import model.Song;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle;

public class Controller implements Initializable {

    @FXML
    private VBox songList;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        List<Song> songs = new ArrayList<>();
        songs.add(new Song());
        songs.add(new Song());
        songs.add(new Song());
        songs.add(new Song());
        songs.add(new Song());

        for (Song song : songs) {
            try {
                FXMLLoader loader = new FXMLLoader(getClass().getResource("song.fxml"));
                HBox songItem = loader.load();
                SongController songController = loader.getController();
                songController.setSong(song);
                songList.getChildren().add(songItem);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
