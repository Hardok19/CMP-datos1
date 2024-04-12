module org.example.prueba1 {
    requires javafx.controls;
    requires javafx.fxml;
    requires javafx.web;
    exports org.example.server;
    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires net.synedra.validatorfx;
    requires org.kordamp.ikonli.javafx;
    requires org.kordamp.bootstrapfx.core;
    requires eu.hansolo.tilesfx;
    requires com.almasb.fxgl.all;
    requires annotations;
    requires com.google.gson;
    requires org.apache.logging.log4j;
    requires javafx.media;
    requires org.apache.commons.io;
    requires jaudiotagger;
    requires java.desktop;
    requires jlayer;

    opens org.example.prueba1 to javafx.fxml;
    exports org.example.prueba1;
}