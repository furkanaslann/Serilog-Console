using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using Serilog.Sinks.MSSqlServer;
using Microsoft.Data.SqlClient;

public class Program
{

    static void Main(string[] args)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        ILogger logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log.txt")
            .CreateLogger();

        //string sqlconnection = "Data Source=DESKTOP-FT3ECCO;Initial Catalog=FurkanSerilogDB;Integrated Security=True";
        logger.Information($"{DateTime.Now} System is connection database");

        // *Socket Programming Start*
        const int ServerPortNmb = 51465;
        logger.Information($"{DateTime.Now} Server port number is locked.");

        Console.WriteLine("Enter 'S' for server, 'C' for client");
        string input = Console.ReadLine();
        logger.Information($"{DateTime.Now} Choice has been made!");


        if (input.Equals("S"))
        {
            logger.Information($"{DateTime.Now} Server started to work!");
            Server();
        }
        else if (input.Equals("C"))
        {
            logger.Information($"{DateTime.Now} Client started to work!");
            Client();
        }
        else
        {
            Console.WriteLine("Unexpected input!");
            logger.Warning($"{DateTime.Now} Unexpected input!");
        }

        void Client()
        {
            // Create a Socket
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 0); 
            logger.Information($"{DateTime.Now} Ip address to identify");
            Socket clientsocket = new Socket(SocketType.Stream, ProtocolType.Tcp); 
            logger.Information($"{DateTime.Now} Socket to identify");
            clientsocket.Bind(clientEndPoint); 
            logger.Information($"{DateTime.Now} Connected Endpoint to Socket");

            // Create connection
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, ServerPortNmb);  
            logger.Information($"{DateTime.Now} Ip address to identify");
            clientsocket.Connect(serverEndPoint); 
            logger.Information($"{DateTime.Now} Connected Endpoint to Socket");

            // Send message
            string messageToSend = "Hi Robot! How are you?"; 
            logger.Information($"{DateTime.Now} Message to identify: {messageToSend}");
            byte[] byteToSend = Encoding.Default.GetBytes(messageToSend); 
            logger.Information($"{DateTime.Now} Convert message to byte");
            clientsocket.Send(byteToSend); 
            logger.Information($"{DateTime.Now} Byte message send from socket");
            Console.WriteLine($"Client send message: {messageToSend}");  

            // Display received message
            byte[] buffer = new byte[1024]; 
            logger.Information($"{DateTime.Now} buffer to identify");
            int nmberOfBytesReceived = clientsocket.Receive(buffer); 
            logger.Information($"{DateTime.Now} Pulled information from socket up to Array.");
            byte[] receivedBytes = new byte[nmberOfBytesReceived];
            logger.Information($"{DateTime.Now} Second buffer to identify");
            Array.Copy(buffer, receivedBytes, nmberOfBytesReceived); 
            logger.Information($"{DateTime.Now} Copy function is work");
            string receivedMessage = Encoding.Default.GetString(receivedBytes); 
            logger.Information($"{DateTime.Now} Convert message to string");
            Console.WriteLine($"Client received message: {receivedMessage}"); 
            logger.Information($"{DateTime.Now} Display received message");
            Console.ReadLine();
        }



        void Server()
        {
            // Create a Socket
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, ServerPortNmb);
            logger.Information($"{DateTime.Now} Ip address to identify");
            Socket welcomingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp); 
            logger.Information($"{DateTime.Now} Socket to identify");
            welcomingSocket.Bind(serverEndPoint); 
            logger.Information($"{DateTime.Now} Connected Endpoint to Socket");

            // Wait for connection
            welcomingSocket.Listen(ServerPortNmb); 
            logger.Information($"{DateTime.Now} Listen to port number");
            Socket connectionSocket = welcomingSocket.Accept(); 
            logger.Information($"{DateTime.Now} Accepted message coming from Client");

            // Display received message
            byte[] buffer = new byte[1024];
            logger.Information($"{DateTime.Now} buffer to identify");
            int nmberOfBytesReceived = connectionSocket.Receive(buffer);
            logger.Information($"{DateTime.Now} Pulled information from socket up to Array.");
            byte[] receivedBytes = new byte[nmberOfBytesReceived]; 
            logger.Information($"{DateTime.Now} Second buffer to identify");
            Array.Copy(buffer, receivedBytes, nmberOfBytesReceived); 
            logger.Information($"{DateTime.Now} Copy function is work");
            string receivedMessage = Encoding.Default.GetString(receivedBytes);
            logger.Information($"{DateTime.Now} Convert message to string");
            Console.WriteLine($"Server received message: {receivedMessage}");

            // Send received message to the client
            connectionSocket.Send(receivedBytes);
            logger.Information($"{DateTime.Now} Send received message to the client");
            Console.ReadLine();
        }
        // *Socket Programming Finish*

        logger.Information($"{DateTime.Now} User logged out of the system!");

    }
}
