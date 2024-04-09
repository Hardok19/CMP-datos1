import com.google.gson.Gson;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.HashMap;



public class ListDoubleO {
    private Node tail; // Puntero al último nodo en la lista

    public int size = 0; // Llevara cuenta del tamaño

    // Método para agregar una canción al final de la lista
    public void addSong(Song song) {
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
            System.out.println("La lista está vacía.");
            return;
        }

        Node current = tail.next; // Comenzamos desde el nuevo head (antes tail.next)

        do {
            System.out.println(current.data.getSongName());
            current = current.next;
        } while (current != tail.next); // Continuamos hasta llegar al nuevo head (antes tail.next)
    }

    // Método para ordenar la lista en función de la diferencia entre likes y dislikes
    public void autoSort() {

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

}




