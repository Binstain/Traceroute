using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Traceroute
{
    class MyTraceroute
    {
        private Socket sendSocket;
        private Socket receiveSocket;

        public IPAddress ip;
        private const int port = 30320;
        private IPEndPoint ipEndPoint;

        private EndPoint endPoint;

        private const Byte type = 8;
        private const Byte code = 0;
        private const UInt16 checkSum = 0;
        private const UInt16 ID = 30320;
        private UInt16 SN;
        private UInt32 data = 12345;
        private Byte[] ICMP;

        private Byte[] receiveBuffer;

        private Byte ttl;

        public string hopIP;

        public bool hasReached = false;

        public MyTraceroute(string IPOrName) 
        {
            sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);           
            //          receiveSocket.ReceiveTimeout = 1000;
            receiveSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
            receiveSocket.Bind(new IPEndPoint(IPAddress.Any, 30320));
            endPoint = (EndPoint)new IPEndPoint(IPAddress.Any, 30320);

            IPAddress[] ips;
            try
            {
                ips = Dns.GetHostAddresses(IPOrName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ip = ips[0];
            ipEndPoint = new IPEndPoint(ip, port);

            ttl = 1;
        }

        public void InitToSend()
        { 
            SN = ttl;

            ICMP = new Byte[12];

            Byte[] bType = new Byte[] { type };
            Byte[] bCode = new Byte[] { code };
            Byte[] bCheckSum = BitConverter.GetBytes(checkSum);
            Byte[] bID = BitConverter.GetBytes(ID);
            Byte[] bSN = BitConverter.GetBytes(SN);
            Byte[] bData = BitConverter.GetBytes(data);

            Array.Copy(bType, 0, ICMP, 0, bType.Length);
            Array.Copy(bCode, 0, ICMP, 1, bCode.Length);
            Array.Copy(bCheckSum, 0, ICMP, 2, bCheckSum.Length);
            Array.Copy(bID, 0, ICMP, 4, bID.Length);
            Array.Copy(bSN, 0, ICMP, 6, bSN.Length);
            Array.Copy(bData, 0, ICMP, 8, bData.Length);

            UInt32 temp_CheckSum = 0;
            int size = ICMP.Length;
            int index = 0;
            while(size > 1)
            {
                temp_CheckSum += Convert.ToUInt32(BitConverter.ToUInt16(ICMP, index));
                index += 2;
                size -= 2;
            }
            if(size == 1)
            {
                temp_CheckSum += Convert.ToUInt32(ICMP[index]);
            }
            temp_CheckSum = (temp_CheckSum >> 16) + (temp_CheckSum & 0xffff);
            temp_CheckSum += (checkSum >> 16);

            bCheckSum = BitConverter.GetBytes((UInt16)(~temp_CheckSum));
            Array.Copy(bCheckSum, 0, ICMP, 2, bCheckSum.Length);

            sendSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);
            ttl++;
        }

        public string sendAndReceive()
        {
            int receiveSize;
            Byte receiveType;
            Byte receiveCode;
            UInt16 receiveID;
            UInt16 receiveSN;

            receiveBuffer = new Byte[1024];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                sendSocket.SendTo(ICMP, ipEndPoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            while(true)
            {
                try
                {
                    receiveSize = receiveSocket.ReceiveFrom(receiveBuffer, ref endPoint);
                }
                catch (SocketException ex)
                {
                    return "*";
                }

                //                if (receiveSize < 56)                 
                //                   continue;

                receiveType = receiveBuffer[20];
                receiveCode = receiveBuffer[21];
                receiveID = BitConverter.ToUInt16(receiveBuffer, 52);
                receiveSN = BitConverter.ToUInt16(receiveBuffer, 54);
                if (receiveType == 11 && receiveCode == 0 && receiveID == ID && receiveSN == SN ||
                    receiveType == 0 && receiveCode == 0)
                {
                    stopwatch.Stop();
                    break;
                }
                              
            }

            IPAddress temp = new IPAddress(BitConverter.ToUInt32(receiveBuffer, 12));
            hopIP = temp.ToString();
            long ms = stopwatch.ElapsedMilliseconds;
            return ms == 0 ? "<1ms" : $"{ms}ms";
        }
    }
}
