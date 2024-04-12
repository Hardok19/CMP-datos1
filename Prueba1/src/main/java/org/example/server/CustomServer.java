package org.example.server;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;

import ini.LectorINI;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import static org.example.server.Main.extractValue;


public class CustomServer {
    private static final Logger logger = LogManager.getLogger(CustomServer.class);
    public static void main(String[] args) {
        final int PORT = 1235;
        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            logger.info("Server listening on port " + PORT);




            while (true) {
                Socket clientSocket = serverSocket.accept();
                logger.info("Client connected: " + clientSocket.getInetAddress().getHostAddress());

                // Crea un nuevo hilo para manejar la conexión con el cliente
                Thread clientThread  = new Thread(new ClientHandler(clientSocket));
                clientThread.start();

            }
        }
        catch (IOException e) {
            logger.error("Error al iniciar el server " + e);
        }
    }
    
    private static class ClientHandler implements Runnable {
        private final Socket clientSocket;
        private static final Logger logger = LogManager.getLogger(ClientHandler.class);
        
        public ClientHandler(Socket clientSocket) {
            this.clientSocket = clientSocket;
        }
        
        @Override
        public void run() {
            try (BufferedReader reader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()))) {
                try (PrintWriter writer = new PrintWriter(clientSocket.getOutputStream(), true)) {
                    String inputLine;
                    while ((inputLine = reader.readLine()) != null) {
                        String fin = extractValue(inputLine, "command");
                        logger.info("Received from client: " + inputLine);
                        if (fin.equals("GetPlaylistUpdates")) {
                            writer.println(Main.songsinP());
                        }
                        else{
                            Main.sendvote(inputLine);
                            writer.println("Received: " + inputLine); // Envía una respuesta al cliente

                        }

                    }
                }
            } catch (IOException e) {
                logger.error("ERROR" + e);
            }
        }
    }
}
