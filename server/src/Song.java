/**
 * La clase Song representa una canción con un nombre, la cantidad de "me gusta" y la cantidad de "no me gusta".
 */
public class Song {

    // Atributos de la clase
    private String songName; // Nombre de la canción
    private int likes;       // Cantidad de "me gusta"
    private int dislikes;    // Cantidad de "no me gusta"

    /**
     * Constructor de la clase Song. Inicializa una nueva canción con el nombre proporcionado.
     *
     * @param songName El nombre de la canción.
     */
    public Song(String songName) {
        this.songName = songName;
        this.likes = 0;
        this.dislikes = 0;
    }

    /**
     * Obtiene el nombre de la canción.
     *
     * @return El nombre de la canción.
     */
    public String getSongName() {
        return songName;
    }

    /**
     * Obtiene la cantidad de "me gusta" de la canción.
     *
     * @return La cantidad de "me gusta".
     */
    public int getLikes() {
        return likes;
    }

    /**
     * Obtiene la cantidad de "no me gusta" de la canción.
     *
     * @return La cantidad de "no me gusta".
     */
    public int getDislikes() {
        return dislikes;
    }

    /**
     * Incrementa la cantidad de "me gusta" de la canción.
     *
     * @param numLikes La cantidad de "me gusta" a agregar.
     */
    public void like(int numLikes) {
        likes += numLikes;
    }

    /**
     * Incrementa la cantidad de "no me gusta" de la canción.
     *
     * @param numDislikes La cantidad de "no me gusta" a agregar.
     */
    public void dislike(int numDislikes) {
        dislikes += numDislikes;
    }
}
