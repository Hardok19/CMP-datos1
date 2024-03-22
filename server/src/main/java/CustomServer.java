import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class CustomServer {
    public static void main(String[] args) {
        final int PORT = 1235;
        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            System.out.println("Server listening on port " + PORT);
            
            while (true) {
                Socket clientSocket = serverSocket.accept();
                System.out.println("Client connected: " + clientSocket.getInetAddress().getHostAddress());
                
                // Crea un nuevo hilo para manejar la conexión con el cliente
                Thread thread = new Thread(new ClientHandler(clientSocket));
                thread.start();
                Main.showMenu();
            }
        }
        catch (IOException e) {
            e.printStackTrace();
        }
    }
    
    private static class ClientHandler implements Runnable {
        private final Socket clientSocket;
        
        public ClientHandler(Socket clientSocket) {
            this.clientSocket = clientSocket;
        }
        
        @Override
        public void run() {
            try (BufferedReader reader = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()))) {
                try (PrintWriter writer = new PrintWriter(clientSocket.getOutputStream(), true)) {
                    String inputLine;
                    while ((inputLine = reader.readLine()) != null) {
                        System.out.println("Received from client: " + inputLine);
                        if (inputLine.equals("GetPlaylistUpdates")) {
                            writer.println(Main.hola());
                            System.out.println("Received from client1234: " + inputLine);
                        }
                        if (inputLine.equals("up.vote")) {
                            System.out.println("Received from client: " + inputLine);
                            writer.println("Received: " + inputLine); // Envía una respuesta al cliente


                        }

                    }
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
