package org.example.server;
import javafx.application.Application;
import javafx.scene.media.Media;
import javafx.scene.media.MediaPlayer;
import javafx.stage.Stage;
import java.io.File;

public class Audioplayer extends Application {


    public static void start(Stage primaryStage, String path) {
        Media sound = new Media(new File(path + ".mp3").toURI().toString());
        MediaPlayer player = new MediaPlayer(sound);
        player.play();
    }

    @Override
    public void start(Stage primaryStage) throws Exception {

    }
}
