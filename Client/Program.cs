using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
public class MarvellousClient
{
    public static string GetLocalIPAddress()
    {
        Console.WriteLine("Harshal Web : Host name - {0}", Dns.GetHostName());

        var Marvelloushost = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in Marvelloushost.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Harshal Web :No network adapters with an IPv4 address in the system!");
    }

    public static void Main(string[] args)
    {
        TcpClient tcpclnt = null;
        Stream stm = null;
        Int32 MarvellousPort = 0;
        string MarvellousIP = null;

        try
        {
            MarvellousIP = GetLocalIPAddress();
            MarvellousPort = 21000;

            tcpclnt = new TcpClient();

            Console.WriteLine("Harshal Web : Connecting with server ...");

            tcpclnt.Connect(MarvellousIP, MarvellousPort);

            Console.WriteLine("Harshal Web : Connection Successful");

            Console.WriteLine("Harshal Web : Enter the message for server ...");

            String str = Console.ReadLine();

            Console.WriteLine("Harshal Web : Getting stream for data trasmission ...");
          
            stm = tcpclnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();

            byte[] ba = asen.GetBytes(str);

            Console.WriteLine("Harshal Web : Sending data ...");

            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];

            int k = stm.Read(bb, 0, 100);


            Console.WriteLine("Harshal Web : Message received from server ...");

            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(bb[i]));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Harshal Web Exception : " + e.StackTrace);
        }
        finally
        {
            Console.WriteLine("Harshal Web : Deallocating all resources ...");
            if (tcpclnt != null)
            {
                tcpclnt.Close();
            }
            if (stm != null)
            {
                stm.Close();
            }
        }
    }
}
