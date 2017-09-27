using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class serv
{
    public static void Main()
    {
        run_server("127.0.0.1", 8888);
        Console.ReadLine();
    }
    static void run_server(string ipaddr, int port)
    {
        try
        {
            IPAddress ipAd = IPAddress.Parse(ipaddr);

            /* On initialise l'écoute sur l'ip et le port en argument*/
            TcpListener myList = new TcpListener(ipAd, port);

            /* On va écouter*/
            int count = 0;
            while (true)
            {
                count += 1;
                myList.Start();

                Console.WriteLine("Server info : {0}", myList.LocalEndpoint);
                Console.WriteLine("Waiting for connection {0}...", count);
                // Add check if not client gtfo
                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from {0}", s.RemoteEndPoint);

                byte[] b = new byte[100];
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));

                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("Sent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur : {0}", e.StackTrace);
        }
        Console.ReadLine();
    }
}
