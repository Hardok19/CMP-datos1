package org.example.server;

import java.io.File;
import org.example.prueba1.*;
import com.google.gson.JsonParser;
import com.google.gson.JsonObject;
import ini.LectorINI;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.example.prueba1.GUI;


public class Main {
    private static final Logger logger = LogManager.getLogger(Main.class);

    // Crear una lista circular doblemente enlazada
    static ListDoubleO playlist = new ListDoubleO();



    public static void main(String[] args) throws Exception {

        LectorINI INI = new LectorINI();
        String libraryPath = INI.getProperty("libraryPath");

        MP3MetadataExtractor data = new MP3MetadataExtractor();
        System.out.println(libraryPath);
        int i = 0;

        while (i < nFiles(libraryPath)){
            String ruta = libraryPath + "\\" + getFileToCrawl(libraryPath, i);
            data.extractor(ruta);
            playlist.addSong(new SongS(data.getTitle(), data.getArtist(), data.getAlbum(), data.getGenre(), data.getMp3FilePath()));


            i += 1;
        }


        GUI.main(args);



    }


    public static int nFiles(String directorio) {
        File carpeta = new File(directorio);
        File[] lista = carpeta.listFiles();
        int cuenta=0;

        for (int i = 0; i < lista.length; i++) {
            if (lista[i].isFile())
                cuenta++;
        }
        return cuenta;
    }
    public static String getFileToCrawl(String directory, int o){
        File dir = new File(directory);

        String[] children = dir.list();
        if (children == null) {
            return "";
        } else {
            return children[o];
        }

    }

    public static void sendvote(String vote){
        // Llamar al método para extraer los valores
        String command = extractValue(vote, "command");
        String id = extractValue(vote, "id");
        playlist.vote(command, id);

    }


    // Método para extraer el valor de una clave específica del JSON
    public static String extractValue(String jsonString, String key) {
        JsonParser parser = new JsonParser();
        JsonObject jsonObject = parser.parse(jsonString).getAsJsonObject();
        return jsonObject.get(key).getAsString();
    }
    public static String songsinP() {
        playlist.autoSort();
        return playlist.getSongNames();
    }
    public static void play(){
        Thread play = new Thread(() -> {
            playlist.playAllSongs();
        });
        play.start();
    }


}




