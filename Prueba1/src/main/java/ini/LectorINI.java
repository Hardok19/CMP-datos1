package ini;

import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

public class LectorINI {
    private static final Logger logger = LogManager.getLogger(LectorINI.class);
    private static final String CONFIG_FILE_PATH = "src/main/java/ini/configuracion.ini";
    private final Properties properties;

    public LectorINI() {
        properties = new Properties();
        try (FileInputStream fileInput = new FileInputStream(CONFIG_FILE_PATH)) {
            properties.load(fileInput);
        } catch (IOException e) {
            logger.error("Error al cargar el archivo de configuración: " + e.getMessage(), e);
        }
    }

    public String getProperty(String key) {
        return properties.getProperty(key);
    }

}