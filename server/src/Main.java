
public class Main {
 public static void main(String[] args) {
  // Crear una lista circular doblemente enlazada
  ListDoubleO playlist = new ListDoubleO();

  // Crear y agregar 5 canciones a la lista
  playlist.addSong(new Song("Canción 1"));
  playlist.addSong(new Song("Canción 2"));
  playlist.addSong(new Song("Canción 3"));
  playlist.addSong(new Song("Canción 4"));
  playlist.addSong(new Song("Canción 5"));

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
 }
}

