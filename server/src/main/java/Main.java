import java.util.Scanner;
public class Main {

    // Crear una lista circular doblemente enlazada
    static ListDoubleO playlist = new ListDoubleO();
    static Scanner scanner = new Scanner(System.in);

    public static void main(String[] args) {

        

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

        System.out.println(info());
        String hello = hola();
        System.out.println(hello);
        CustomServer.main(args);
        showMenu();

    }


    public static void showMenu() {
        System.out.println("Por favor, selecciona una opción:");
        System.out.println("1. Agregar canción");
        System.out.println("2. Quitar canción");
        System.out.println("3. Salir");

        int opcion = scanner.nextInt();

        switch (opcion) {
            case 1:
                // Agregar una canción
                System.out.print("Ingrese el nombre de la canción a agregar: ");
                scanner.nextLine(); // Consumir el salto de línea pendiente
                String nombreCancion = scanner.nextLine();
                playlist.addSong(new Song(nombreCancion));
                System.out.println("Canción agregada exitosamente.");
                break;
            case 2:
                // Quitar una canción
                System.out.print("Ingrese el índice de la canción a quitar (empezando desde 1): ");
                int indiceCancion = scanner.nextInt();
                if (indiceCancion >= 1 && indiceCancion <= playlist.size) {
                    playlist.getNode(indiceCancion - 1).prev.next = playlist.getNode(indiceCancion - 1).next;
                    playlist.getNode(indiceCancion - 1).next.prev = playlist.getNode(indiceCancion - 1).prev;
                    playlist.getNode(indiceCancion - 1).next = null;
                    playlist.getNode(indiceCancion - 1).prev = null;
                    System.out.println("Canción eliminada exitosamente.");
                } else {
                    System.out.println("Índice inválido. Inténtelo de nuevo.");
                }
                break;
            case 3:
                // Salir del programa
                System.out.println("¡Hasta luego!");
                scanner.close();
                System.exit(0);
            default:
                System.out.println("Opción inválida. Por favor, seleccione una opción válida.");
        }
    }
    public static String info(){ 
        return playlist.getNode(0).data.getSongName();
    }



    public static String hola(){
        return playlist.getSongNames();
        }


}



