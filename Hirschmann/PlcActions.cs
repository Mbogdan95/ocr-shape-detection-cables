using Sharp7;
using System;
using System.Threading;

namespace Hirschmann
{
    public class PlcActions
    {
        //S7Client client = new S7Client();

        bool stop = false;

        #region PLC variables


        #endregion

        public void ContinousReadWritePlc(out bool alarm1, out bool alarm2, out bool alarm3, out bool alarm4, out bool alarm5, out bool alarm6, out bool alarm7, out bool alarm8, out bool alarm9, out bool alarm10, out bool statusRun)
        {
            alarm1 = false;
            alarm2 = false;
            alarm3 = false;
            alarm4 = false;
            alarm5 = false;
            alarm6 = false;
            alarm7 = false;
            alarm8 = false;
            alarm9 = false;
            alarm10 = false;
            statusRun = false;

            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            while (!stop)
            {
                byte[] readBuffer = new byte[11];
                int result = client.DBRead(1, 33, 2, readBuffer);
                if (result != 0)
                {
                    Console.WriteLine("Error continous read: " + client.ErrorText(result));
                }

                alarm1 = S7.GetBitAt(readBuffer, 0, 0);
                alarm2 = S7.GetBitAt(readBuffer, 0, 1);
                alarm3 = S7.GetBitAt(readBuffer, 0, 2);
                alarm4 = S7.GetBitAt(readBuffer, 0, 3);
                alarm5 = S7.GetBitAt(readBuffer, 0, 4);
                alarm6 = S7.GetBitAt(readBuffer, 0, 5);
                alarm7 = S7.GetBitAt(readBuffer, 0, 6);
                alarm8 = S7.GetBitAt(readBuffer, 0, 7);
                alarm9 = S7.GetBitAt(readBuffer, 1, 0);
                alarm10 = S7.GetBitAt(readBuffer, 1, 1);
                statusRun = S7.GetBitAt(readBuffer, 1, 2);

                byte[] writeBuffer = new byte[1];
                int startIndex = 32;

                S7.SetBitAt(ref writeBuffer, 0, 0, true);

                result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + 2, 1, S7Consts.S7WLBit, writeBuffer);
                if (result != 0)
                {
                    Console.WriteLine("Error continous write: " + client.ErrorText(result));
                }
            }

            client.Disconnect();
        }

        public void WriteToPlc(int triggerOffset, int wasteOffset, int startWastingOffset, bool camera1, bool camera2, string machineName)
        {
            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] writeBuffer = new byte[20];
            int startIndex = 0;

            //Offset Trigger
            S7.SetDIntAt(writeBuffer, 0, triggerOffset);
            //Offset Rebut
            S7.SetDIntAt(writeBuffer, 4, wasteOffset);

            //Rejectii
            S7.SetDIntAt(writeBuffer, 8, startWastingOffset);

            //Camera
            if (camera1 && camera2)
            {
                S7.SetDIntAt(writeBuffer, 12, 3);
            }
            else if (!camera1 && !camera2)
            {
                S7.SetDIntAt(writeBuffer, 12, 0);
            }
            else if (camera1)
            {
                S7.SetDIntAt(writeBuffer, 12, 1);
            }
            else if (camera2)
            {
                S7.SetDIntAt(writeBuffer, 12, 2);
            }

            //Masina
            if (machineName == "Metzner")
            {
                S7.SetDIntAt(writeBuffer, 16, 1);
            }
            else if (machineName == "Komax")
            {
                S7.SetDIntAt(writeBuffer, 16, 2);
            }

            int result = client.DBWrite(1, startIndex, writeBuffer.Length, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            writeBuffer = new byte[1];
            startIndex = 32;

            S7.SetBitAt(ref writeBuffer, 0, 0, true);

            result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8, 1, S7Consts.S7WLBit, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            Thread.Sleep(200);

            ResetHkConfigLoad();

            client.Disconnect();
        }

        private void ResetHkConfigLoad()
        {
            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));

            }

            byte[] writeBuffer = new byte[1];
            int startIndex = 32;

            S7.SetBitAt(ref writeBuffer, 0, 0, false);

            int result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + 1, 1, S7Consts.S7WLBit, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            client.Disconnect();
        }

        public void ResetAlarms()
        {
            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] writeBuffer = new byte[1];
            int startIndex = 32;

            S7.SetBitAt(ref writeBuffer, 0, 1, true);

            int result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + 5, 1, S7Consts.S7WLBit, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            client.Disconnect();
        }

        public void WriteCableOk()
        {
            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] writeBuffer = new byte[1];
            int startIndex = 32;

            S7.SetBitAt(ref writeBuffer, 0, 1, true);

            int result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + 3, 1, S7Consts.S7WLBit, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            client.Disconnect();
        }

        public void WriteCableNok()
        {
            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] writeBuffer = new byte[1];
            int startIndex = 32;

            S7.SetBitAt(ref writeBuffer, 0, 1, true);

            int result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + 4, 1, S7Consts.S7WLBit, writeBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            client.Disconnect();
        }

        public void ReadInputs(out bool input1,
                out bool input2,
                out bool input3,
                out bool input4,
                out bool input5,
                out bool input6,
                out bool input7,
                out bool input8,
                out bool input9,
                out bool input10,
                out bool input11,
                out bool input12,
                out bool input13,
                out bool input14)
        {
            input1 = false;
            input2 = false;
            input3 = false;
            input4 = false;
            input5 = false;
            input6 = false;
            input7 = false;
            input8 = false;
            input9 = false;
            input10 = false;
            input11 = false;
            input12 = false;
            input13 = false;
            input14 = false;

            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] readBuffer = new byte[14];
            int result = client.DBRead(1, 35, 2, readBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            input1 = S7.GetBitAt(readBuffer, 0, 0);
            input2 = S7.GetBitAt(readBuffer, 0, 1);
            input3 = S7.GetBitAt(readBuffer, 0, 2);
            input4 = S7.GetBitAt(readBuffer, 0, 3);
            input5 = S7.GetBitAt(readBuffer, 0, 4);
            input6 = S7.GetBitAt(readBuffer, 0, 5);
            input7 = S7.GetBitAt(readBuffer, 0, 6);
            input8 = S7.GetBitAt(readBuffer, 0, 7);
            input9 = S7.GetBitAt(readBuffer, 1, 0);
            input10 = S7.GetBitAt(readBuffer, 1, 1);
            input11 = S7.GetBitAt(readBuffer, 1, 2);
            input12 = S7.GetBitAt(readBuffer, 1, 3);
            input13 = S7.GetBitAt(readBuffer, 1, 4);
            input14 = S7.GetBitAt(readBuffer, 1, 5);
        }

        public void ReadOutputs(out bool output1,
                out bool output2,
                out bool output3,
                out bool output4,
                out bool output5,
                out bool output6,
                out bool output7,
                out bool output8,
                out bool output9,
                out bool output10,
                out bool output11,
                out bool output12,
                out bool output13,
                out bool output14)
        {
            output1 = false;
            output2 = false;
            output3 = false;
            output4 = false;
            output5 = false;
            output6 = false;
            output7 = false;
            output8 = false;
            output9 = false;
            output10 = false;
            output11 = false;
            output12 = false;
            output13 = false;
            output14 = false;

            S7Client client = new S7Client();

            int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

            if (restultClient == 0)
            {
                Console.WriteLine("Connected to PLC");
            }
            else
            {
                Console.WriteLine(client.ErrorText(restultClient));
                return;
            }

            byte[] readBuffer = new byte[14];
            int result = client.DBRead(1, 36, 3, readBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error read outputs: " + client.ErrorText(result));
            }

            output1 = S7.GetBitAt(readBuffer, 0, 6);
            output2 = S7.GetBitAt(readBuffer, 0, 7);
            output3 = S7.GetBitAt(readBuffer, 1, 0);
            output4 = S7.GetBitAt(readBuffer, 1, 1);
            output5 = S7.GetBitAt(readBuffer, 1, 2);
            output6 = S7.GetBitAt(readBuffer, 1, 3);
            output7 = S7.GetBitAt(readBuffer, 1, 4);
            output8 = S7.GetBitAt(readBuffer, 1, 5);
            output9 = S7.GetBitAt(readBuffer, 1, 6);
            output10 = S7.GetBitAt(readBuffer, 1, 7);
            output11 = S7.GetBitAt(readBuffer, 2, 0);
            output12 = S7.GetBitAt(readBuffer, 2, 1);
            output13 = S7.GetBitAt(readBuffer, 2, 2);
            output14 = S7.GetBitAt(readBuffer, 2, 3);

            client.Disconnect();
        }

        public void WriteOutput(int startIndex, int offset, bool on)
        {
            try
            {
                S7Client client = new S7Client();

                int restultClient = client.ConnectTo("192.168.0.1", 0, 0);

                if (restultClient == 0)
                {
                    Console.WriteLine("Connected to PLC");
                }
                else
                {
                    Console.WriteLine(client.ErrorText(restultClient));
                    return;
                }

                byte[] writeBuffer = new byte[1];

                S7.SetBitAt(ref writeBuffer, 0, 1, on);

                int result = client.WriteArea(S7Consts.S7AreaDB, 1, startIndex * 8 + offset, 1, S7Consts.S7WLBit, writeBuffer);
                if (result != 0)
                {
                    Console.WriteLine("Error: " + client.ErrorText(result));
                }

                client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Write output: " + ex.Message);
            }
        }
    }
}
