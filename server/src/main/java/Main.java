import java.util.Scanner;
import com.google.gson.JsonParser;
import com.google.gson.JsonObject;
public class Main {

    // Crear una lista circular doblemente enlazada
    static ListDoubleO playlist = new ListDoubleO();
    static Scanner scanner = new Scanner(System.in);

    public static void main(String[] args) {

        // Crear y agregar 5 canciones a la lista
        playlist.addSong(new Song("Canción 1", "Artist 1", "1"));
        playlist.addSong(new Song("Canción 2", "Artist 2", "2"));
        playlist.addSong(new Song("Canción 3", "Artist 3", "3"));
        playlist.addSong(new Song("Canción 4", "Artist 4", "4"));
        playlist.addSong(new Song("Canción 5", "Artist 5", "5"));

        // Imprimir la lista de canciones antes de agregar likes
        System.out.println("Lista de canciones antes de agregar likes:");
        playlist.printSongs();
        System.out.println();

        // Agregar likes a cada canción según las especificaciones
        playlist.getNode(0).data.like(5); // Primera canción tiene 5 likes
        playlist.getNode(1).data.like(1); // Segunda canción tiene 1 like
        playlist.getNode(2).data.like(3); // Tercera canción tiene 3 likes
        playlist.getNode(3).data.like(5); // Cuarta canción tiene 5 likes
        playlist.getNode(4).data.like(2); // Quinta canción tiene 2 likes

        playlist.autoSort(); // Ordenar inicialmente por la diferencia de likes y dislikes

        // Imprimir la lista de canciones después de agregar likes
        System.out.println("Lista de canciones después de agregar likes:");
        playlist.printSongs();


        CustomServer.main(args, playlist);

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
    public static String hola() {
        mensajero mens = new mensajero();
        return mens.holas(playlist);
    }

}




