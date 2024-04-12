package org.example.server;

import java.util.UUID;

/**
 * La clase sample.Song representa una canción con un nombre, la cantidad de "me gusta" y la cantidad de "no me gusta".
 */
public class SongS {

    // Atributos de la clase
    private final String songName; // Nombre de la canción
    private final String artist;
    private final UUID id;
    private int likes;       // Cantidad de "me gusta"
    private int dislikes;    // Cantidad de "no me gusta"
    private final String album;
    private final String genero;
    public final String mp3FilePath; // Campo para la ruta del archivo MP3


    /**
     * Constructor de la clase sample.Song. Inicializa una nueva canción con el nombre proporcionado.
     *
     * @param songName El nombre de la canción.
     */
    public SongS(String songName, String artist, String album, String genero, String mp3FilePath) {
        this.songName = songName;
        this.artist = artist;
        this.id = UUID.randomUUID(); // Genera un nuevo GUID único
        this.likes = 0;
        this.dislikes = 0;
        this.album = album;
        this.genero = genero;
        this.mp3FilePath = mp3FilePath;
    }

    /**
     * Obtiene el nombre de la canción.
     *
     * @return El nombre de la canción.
     */
    public String getSongName() {
        return songName;
    }
    public String getArtist(){
        return artist;
    }
    public String getid(){
        return id.toString();
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
    public String getalbum(){
        return album;
    }
    public String getGenero(){
        return genero;
    }

    public String getfpath(){
        return mp3FilePath;
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

    public String mp3FilePath() {
        return this.mp3FilePath;
    }
}
