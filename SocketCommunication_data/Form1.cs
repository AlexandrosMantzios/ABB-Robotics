using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SocketCommunication_data
{
    public partial class Form1 : Form
    {
        //const int PORT_NO = 6510;
        const int PORT_NO = 1025;
      // string SERVER_IP = "172.27.65.249";
        string CAMERA_IP = "172.27.65.208";
        const int PORT_CAM_NO = 50010;
        string SERVER_IP = "127.0.0.1";
        public string message;
        public string messagecam;
        public string recv;
      //  public string messagecam = string.Empty;
      
        public Form1()
        {
            InitializeComponent();
            //  message = "[55.37,26.850061546,169.038884343,0.000443534,-0.999999902,0,0,0,0,0,0]";
            // message = "[476.5,79.6,169.08,0.000313626,-0.707106712,-0.707106712,-0.000313626]";
            //message = messagecam;
            //   message = "[476.504,79.601040791,169.085678078]";
             // message = "[-55.37,26.85,169,0,0,0.999999902,-0.000443534]";
            //  message = "[-55.37,26.85,169]";
            //message = "[55.37,26.850061546,169.038884343]";
        }

    
        private void Button3_Click(object sender, EventArgs e)
        {

            {
                //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //IPAddress ipAdd = System.Net.IPAddress.Parse("172.27.65.208");
                //IPEndPoint remoteEP = new IPEndPoint(ipAdd, 50010);
                //socket.Connect(remoteEP);


                TcpClient client1 = new TcpClient(CAMERA_IP, PORT_CAM_NO);
                NetworkStream nwStream1 = client1.GetStream();



                /// OPTION 0   ////////
                // byte[] data = new Byte[1024];
                //// byte[] data = Encoding.ASCII.GetBytes(messagecam);
                //int iRx = socket.Receive(data);
                //char[] chars = new char[iRx];
                //System.Text.Decoder d = System.Text.Encoding.ASCII.GetDecoder();
                //int charLen = d.GetChars(data, 0, iRx, chars, 0);
                //System.String messagecam = new System.String(chars);

                //// string messagecam = String.Empty;

                ////Int32 dataLength; 

                ////{

                ////    {
                ////        dataLength = nwStream1.Read(data, 0, data.Length);
                ////        messagecam = System.Text.Encoding.ASCII.GetString(data, 0, dataLength);
                ////    messagecam = System.Text.RegularExpressions.Regex.Replace(messagecam, @"[^\u0000-\u007F]", string.Empty);
                //System.Threading.Thread.Sleep(5);
                //        Console.Write(" Received :" + messagecam);

                //string s2 = ",";
                //bool b = messagecam.Contains(s2);
                //Console.WriteLine(" {0} is in the string {1} :{2}", s2, messagecam, b);
                //if (b)
                //{
                //    int index = messagecam.IndexOf(s2);
                //    if (index >= 0)
                //        Console.WriteLine("{0} begins at character position {1}", s2, index + 1);
                //}

                //socket.Disconnect(false);
                //socket.Close();
                //    }




                //}










                /////////// OPTION 1 /////////////////////////////////////////////////////////////////////////
                // Encoding utf8 = Encoding.UTF8;
                // string messagecam = string.Empty;

                string messagecam = String.Empty;
                //byte[] data = ASCIIEncoding.ASCII.GetBytes(messagecam);
                byte [] data = new byte[client1.ReceiveBufferSize];
                // byte[] data = Encoding.ASCII.GetBytes(messagecam);
              nwStream1.Read(data, 0, data.Length);
               // data = new Byte[1024];
               // string messagecam = String.Empty;
                //{
                //    {
                int dataLength = nwStream1.Read(data, 0, data.Length);
                messagecam = System.Text.Encoding.ASCII.GetString(data, 0, dataLength);
             //   messagecam = System.Text.RegularExpressions.Regex.Replace(messagecam, @"[^\u0000-\u007F]", string.Empty);
                System.Threading.Thread.Sleep(5);
                Console.Write(" Received :" + messagecam);



               // message = string.Empty;
                 message = messagecam;



                // //// OPTION 2 ////////////////////////////////////////////////////////////////////////////////////////

                // byte[] buffer = new byte[1024];
              //  byte[] bytestoRead1 = new byte[client1.ReceiveBufferSize];
              //  int bytesRead1 = nwStream1.Read(bytestoRead1, 0, client1.ReceiveBufferSize);



              ////  messagecam = System.Text.RegularExpressions.Regex.Replace(messagecam, @"[^\u0000-\u007F]", string.Empty);

              //  Console.WriteLine("Received:" + Encoding.UTF8.GetString(bytestoRead1, 0, bytesRead1));
                client1.Close();

                TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                NetworkStream nwStream = client.GetStream();

                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);


                //---send to text---//
                Console.WriteLine("Receiving:" + message);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //---read back the text---//
                byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                client.Close();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////}
                ///

                ///////// OPTION 3 ////////////////////// 


                //  Console.WriteLine(" Message" + messagecam);



                ////if (textBox2.Text != null)
                ////{
                ////    button2.Enabled = true;
                ////    messagecam = textBox2.Text.ToString();
                ////    //SERVER_IP = "127.0.0.1";               
                ////}

                ////else
                ////{
                ////    button2.Enabled = true;
                ////    SERVER_IP = "127.0.0.1";
                ////}






            }

        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {


            // message = textBox1.Text.ToString();


            // double messagenum = Convert.ToDouble(message);
            // bool isIntString = message.All(char.IsDigit);
            //  if (isIntString==true)

            // if (messagenum > 0)


            ////{
            //TcpClient client = new TcpClient(CAMERA_IP, PORT_CAM_NO);

            //NetworkStream nwStream1 = client.GetStream();
            //string messagecam = string.Empty;
            //byte[] bytesToReceive = ASCIIEncoding.ASCII.GetBytes(messagecam);

            ////---send to text---//
            //Console.WriteLine("Receiving:" + messagecam);
            //nwStream1.Write(bytesToReceive, 0, bytesToReceive.Length);

            ////---read back the text---//
            //byte[] bytestoRead = new byte[client.ReceiveBufferSize];
            //int bytesRead = nwStream1.Read(bytestoRead, 0, client.ReceiveBufferSize);
            //Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
            //client.Close();
            ////}


            {
                TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);


                //---send to text---//
                Console.WriteLine("Receiving:" + message);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                //---read back the text---//
                byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                client.Close();

            }






        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        public void button2_Click(object sender, EventArgs e)
        {


            // button1.Enabled = false;





            //    //---send to text---//
            //    Console.WriteLine("Receiving:" + messagecam);
            //    nwStream1.Write(bytesToReceive, 0, bytesToReceive.Length);

            //    //---read back the text---//
            //    byte[] bytestoRead1 = new byte[client1.ReceiveBufferSize];
            //    int bytesRead1 = nwStream1.Read(bytestoRead1, 0, client1.ReceiveBufferSize);
            //    Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead1, 0, bytesRead1));
            //    client1.Close();
            //}

            //if (textBox2.Text != null)
            //{
            //    button2.Enabled = true;
            //    messagecam = textBox2.Text.ToString();
            //    //SERVER_IP = "127.0.0.1";               
            //}

            //else
            //{
            //    button2.Enabled = true;
            //    SERVER_IP = "127.0.0.1";
            //}
            // message = Data;
        }

        


        //private void CheckBox2_Click(object sender, EventArgs e)
        //{
            
        //    {
        //        if (this.checkBox2.Checked)
        //        {
        //            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

        //            NetworkStream nwStream = client.GetStream();

        //            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);


        //            //---send to text---//
        //            Console.WriteLine("Receiving:" + message);
        //            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

        //            //---read back the text---//
        //            byte[] bytestoRead = new byte[client.ReceiveBufferSize];
        //            int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
        //            Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
        //            client.Close();
        //        }
        //        else
        //        {
                   
        //        }
        //    }


        //}



        //private void CheckBox1_Click(object sender, EventArgs e)
        //{

        //    {
        //        if (this.checkBox1.Checked)
        //        {
        //            message = "[-55.37,26.85,169]";
        //        }
        //        else
        //        {
        //            message = "[476.504,79.601040791,169.085678078]";
        //        }
        //    }


        

        //}

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
 
                {
                    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                    NetworkStream nwStream = client.GetStream();

                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);

                   

                            //---send to text---//
                            Console.WriteLine("Receiving:" + message);
                            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                            //---read back the text---//
                            byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                            int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                            Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                            client.Close();

                  

            }
          //  }

           

        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            
                {
                    if (this.checkBox2.Checked)
                    {
                        //message = "[-400,-50,169.085678078,-180,0,-90]";
                        message = "[239,421,300,-180,0,180]";

                        TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                        NetworkStream nwStream = client.GetStream();

                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);



                        //---send to text---//
                        Console.WriteLine("Receiving:" + message);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                        //---read back the text---//
                        byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                        int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                        Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                        int bytesRead1 = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                        Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead1));
                        client.Close();


                        //message = "[-400,-50,169.085678078,-180,0,-90]";
                        message = "[239,421,300,-180,0,180]";

                    }
                    else
                    {
                        message = "[50,-59.601040791,169.085678078,-180,0,180]";
                        //message = "[50,-59.601040791,169.085678078,-180,0,-90]";

                        TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                        NetworkStream nwStream = client.GetStream();

                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);



                        //---send to text---//
                        Console.WriteLine("Receiving:" + message);
                        nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                        //---read back the text---//
                        byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                        int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                        Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                        client.Close();
                    }
                }
            
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (this.checkBox3.Checked)
                {
                    message = "[-55.37, 100.85, 300,-180,0,-90]";

                    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                    NetworkStream nwStream = client.GetStream();

                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);



                    //---send to text---//
                    Console.WriteLine("Receiving:" + message);
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    //---read back the text---//
                    byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                    Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                    client.Close();



                    message = "[-55.37, 100.85, 300,-180,0,-90]";
                }
                else
                {
                    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);

                    NetworkStream nwStream = client.GetStream();

                    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);



                    //---send to text---//
                    Console.WriteLine("Receiving:" + message);
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    //---read back the text---//
                    byte[] bytestoRead = new byte[client.ReceiveBufferSize];
                    int bytesRead = nwStream.Read(bytestoRead, 0, client.ReceiveBufferSize);
                    Console.WriteLine("Received:" + Encoding.ASCII.GetString(bytestoRead, 0, bytesRead));
                    client.Close();


                    message = "[-55.37,26.85,169.085678078,-180,0,-90]";
                }
            }
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

        

        //PLANES TO QUANTERNIONS
        //   public static double[] PlaneToQuaternion(Plane plane)
        //    {
        //        var q = Quaternion.Rotation(Plane.WorldXY, plane);
        //        return new double[] { plane.OriginX, plane.OriginY, plane.OriginZ, q.A, q.B, q.C, q.D };
        //    }

        //    Or you want to interpolate 3d rotations? That’s more tricky and theres no “best” way, but a usual way is using axis angle, this is the code I use:

        //public Plane CartesianLerp(Plane a, Plane b, double t, double min, double max)
        //    {
        //        t = (t - min) / (max - min);
        //        if (double.IsNaN(t)) t = 0;
        //        var newOrigin = a.Origin * (1 - t) + b.Origin * t;
        //        Quaternion q = Quaternion.Rotation(a, b);
        //        q.GetRotation(out var angle, out var axis);
        //        angle = (angle > PI) ? angle - 2 * PI : angle;
        //        a.Rotate(t * angle, axis, a.Origin);
        //        a.Origin = newOrigin;
        //        return a;
        //    }


    
}
