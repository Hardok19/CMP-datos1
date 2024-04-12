package org.example.server;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import java.io.File;
import org.jaudiotagger.audio.AudioFile;
import org.jaudiotagger.audio.AudioFileIO;
import org.jaudiotagger.tag.FieldKey;
import org.jaudiotagger.tag.Tag;


public class MP3MetadataExtractor {
    private static final Logger logger = LogManager.getLogger(MP3MetadataExtractor.class);
    private String title;
    private String artist;
    private String album;
    private String genre;
    private String mp3FilePath;


    public void extractor(String ruta) {
        try {
            // Ruta del archivo de música
            File file = new File(ruta);

            // Leer el archivo de audio
            AudioFile audioFile = AudioFileIO.read(file);

            // Obtener los metadatos
            Tag tag = audioFile.getTag();
            String title = tag.getFirst(FieldKey.TITLE);
            String artist = tag.getFirst(FieldKey.ARTIST);
            String album = tag.getFirst(FieldKey.ALBUM);
            String genre = tag.getFirst(FieldKey.GENRE);

            this.title = title;
            this.artist = artist;
            this.album = album;
            this.genre = genre;
            this.mp3FilePath = ruta;





        } catch (Exception e) {
            logger.error(e);
        }
    }



    public String getTitle() {
        return title;
    }

    public String getArtist() {
        return artist;
    }

    public String getAlbum() {
        return album;
    }

    public String getGenre() {
        return genre;
    }

    public String getMp3FilePath() {
        return mp3FilePath;
    }

    public String getMetadataAsString() {
        return "Nombre: " + title + "\n" +
                "Artista: " + artist + "\n" +
                "Álbum: " + album + "\n" +
                "Género: " + genre + "\n" +
                "Ruta del archivo MP3: " + mp3FilePath;
    }
}



