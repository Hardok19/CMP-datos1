/**
 * Accept connections from client sockets and convert the string to uppercase.
 * 
 * @author Hari krishna
 */

 import java.io.IOException;
 import java.io.PrintWriter;
 import java.net.ServerSocket;
 import java.net.Socket;
 import java.util.Scanner;
 
 public class CustomServer {
 
  private int port;
  private int backlog;
 
  public CustomServer(int port, int backlog) {
   this.port = port;
   this.backlog = backlog;
  }
 
  public void startServer() {
   try (ServerSocket serverSocket = new ServerSocket(port, backlog)) {
 
    System.out.println("Started Listening for clients");
    while (true) {
 
     // take input and output streams
     try (Socket client = serverSocket.accept();
       Scanner scanner = new Scanner(client.getInputStream());
       PrintWriter pw = new PrintWriter(client.getOutputStream(), true)) {
      String dataFromClient = scanner.nextLine();
 
      String response = getResponse(dataFromClient);
      pw.write(response);
 
     }catch(Exception e){
      e.printStackTrace();
     }
 
    }
   } catch (IOException e) {

    e.printStackTrace();
   }
  }
 
  private String getResponse(String dataFromClient) {
   return dataFromClient.toUpperCase();
  }
 
 }
