package org.example.server;

import com.google.gson.Gson;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.HashMap;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import javazoom.jl.decoder.JavaLayerException;
import javazoom.jl.player.advanced.AdvancedPlayer;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.List;


public class ListDoubleO {
    private static final Logger logger = LogManager.getLogger(ListDoubleO.class);
    private Node tail; // Puntero al último nodo en la lista

    public int size = 0; // Llevara cuenta del tamaño
    private AdvancedPlayer player; // Reproductor de música
    private boolean isPlaying = false; // Indica si se está reproduciendo una canción

    private Node currentSong; // Canción actual

    private Thread playbackThread;




    // Método para agregar una canción al final de la lista
    public void addSong(SongS song) {
        Node newNode = new Node(song);
        size++;
        if (tail == null) {
            // Si la lista está vacía, el nuevo nodo es la cola y apunta a sí mismo en ambas direcciones
            tail = newNode;
            tail.next = tail;
            tail.prev = tail;
        } else {
            // Si la lista no está vacía, agregamos el nuevo nodo al final
            newNode.next = tail.next;
            newNode.prev = tail;
            tail.next = newNode;
            tail.next.prev = newNode;
            tail = newNode;
        }
    }

    // Método para imprimir los nombres de las canciones en la lista
    public void printSongs() {
        if (tail == null) {
            logger.info("La lista está vacía.");
            return;
        }

        Node current = tail.next; // Comenzamos desde el nuevo head (antes tail.next)

        do {
            logger.info(current.data.getSongName());
            current = current.next;
        } while (current != tail.next); // Continuamos hasta llegar al nuevo head (antes tail.next)
    }

    // Método para ordenar la lista en función de la diferencia entre likes y dislikes
    public void autoSort() {
        if (size < 2) {
            // No hay necesidad de ordenar si hay 0 o 1 elementos en la lista
            return;
        }
        for (int i = 0; i < size - 1; i++) {
            for (int j = 0; j < size - 1 - i; j++) {
                Node current = getNode(j);
                Node next = getNode(j + 1);

                int currentDifference = current.data.getLikes() - current.data.getDislikes();
                int nextDifference = next.data.getLikes() - next.data.getDislikes();

                if (currentDifference < nextDifference) {
                    // Intercambiar los datos de las canciones
                    SongS temp = current.data;
                    current.data = next.data;
                    next.data = temp;
                }
            }
        }
    }
    // Método para obtener un nodo específico en la lista
    public Node getNode(int index) {
        if (tail == null || index < 0) {
            return null;
        }

        Node current = tail.next;
        for (int i = 0; i < index; i++) {
            current = current.next;
            if (current == tail.next) {
                return null; // El índice está fuera de rango
            }
        }

        return current;
    }

    // Método para obtener los nombres de las canciones junto con sus artistas e ids en formato JSON
    public String getSongNames() {
        Gson gson = new Gson();
        List<Map<String, String>> songs = new ArrayList<>();

        if (tail == null) {
            return gson.toJson(songs); // Retorna un JSON representando una lista vacía si la lista está vacía
        }

        Node current = tail.next;
        do {
            Map<String, String> songMap = new HashMap<>();
            songMap.put("id", current.data.getid());
            songMap.put("songName", current.data.getSongName());
            songMap.put("artist", current.data.getArtist());
            songMap.put("likes", Integer.toString(current.data.getLikes()));
            songMap.put("dislikes", Integer.toString(current.data.getDislikes()));

            songs.add(songMap);
            current = current.next;
        } while (current != tail.next);

        return gson.toJson(songs);
    }
    public void vote(String command, String id) {
        int i = 0;
        if (tail == null) {
            return;
        }
        Node current = tail.next;

        while (i <= size){
            if(id.equals(current.data.getSongName())){
                if (command.startsWith("up")){
                    current.data.like(1);
                    System.out.println("los likes son de " + id + " son "+ current.data.getLikes());
                    break;
                }
                if (command.startsWith("down")){
                    current.data.dislike(1);
                    System.out.println("los dislikes de " + id + " son "+ current.data.getDislikes());
                    break;
                }


            }
            else{
                i += 1;
                current = current.next;
            }
        }

    }
    // Método para reproducir todas las canciones
    public void playAllSongs() {
        currentSong = tail.next;
        playNextSong();
    }
    // Método para reproducir una canción
    private void playSong(Node song) {
        try {
            FileInputStream fis = new FileInputStream(song.data.mp3FilePath());
            player = new AdvancedPlayer(fis);
            playbackThread = new Thread(() -> {
                try {
                    isPlaying = true;
                    player.play();
                    isPlaying = false;
                    playNextSong(); // Llama a playNextSong() cuando la canción actual haya terminado
                } catch (JavaLayerException e) {
                    logger.error(e);
                }
            });
            playbackThread.start();
        } catch (FileNotFoundException | JavaLayerException e) {
            logger.error(e);
        }
    }

    // Método para reproducir la siguiente canción
    private void playNextSong() {
        if (currentSong != null) {
            playSong(currentSong);
            currentSong = currentSong.next;


        }
    }
    // Método para saltar a la siguiente canción
    public void skipSong() {
        if (isPlaying) {
            playbackThread.interrupt(); // Interrumpe la reproducción actual
            isPlaying = false;
        } else {
            // Si no está reproduciendo, simplemente pasa a la siguiente canción
            playNextSong();
        }
    }
}




