public class ListDoubleO {
    private Node tail; // Puntero al último nodo en la lista

    private int size = 0; // Llevara cuenta del tamaño

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

}
