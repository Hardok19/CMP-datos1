package org.example.prueba1;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import org.jetbrains.annotations.NotNull;

public class GUI extends Application{
    public void start(@NotNull Stage primaryStage) throws Exception {

        Parent root = FXMLLoader.load(getClass().getResource("hello-view.fxml"));
        primaryStage.setTitle("GUI SERVER");
        primaryStage.setScene(new Scene(root));
        primaryStage.show();
    }

    public static void main(String[] args) {
        // Start the JavaFX application on a separate thread
        Thread thread = new Thread(() -> launch(args));
        thread.start();
    }
}
