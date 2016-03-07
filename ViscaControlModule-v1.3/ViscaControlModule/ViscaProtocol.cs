using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ViscaControlModule
{
    class ViscaProtocol
    {


        public byte[] SetAdress()
        {
            byte[] buffer = new byte[4];

            buffer[0] = (0x88);
            buffer[1] = (0x30);
            buffer[2] = (0x01);
            buffer[3] = (0xff);
            return buffer;
        }

        private byte[] TiltUp(byte adress, byte value)
        {
            byte[] buffer = new byte[9];

            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x01);
            buffer[4] = (value);
            buffer[5] = (value);
            buffer[6] = (0x03);
            buffer[7] = (0x01);
            buffer[8] = (0xff);
            return buffer;
        }

        private byte[] PanLeft(byte adress, byte value)
        {
            byte[] buffer = new byte[9];
            //string[] cmd = "8x 01 06 01 0a 0a 01 03 ff".Split(' ');

            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x01);
            buffer[4] = (value);
            buffer[5] = (value);

            buffer[6] = (0x01);
            buffer[7] = (0x03);
            buffer[8] = (0xff);

            return buffer;
        }

        private byte[] PanRight(byte adress, byte value)
        {
            byte[] buffer = new byte[9];
            //string[] cmd = "8x 01 06 01 0a 0a 02 03 ff".Split(' ');

            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x01);
            buffer[4] = (value);
            buffer[5] = (value);

            buffer[6] = (0x02);
            buffer[7] = (0x03);
            buffer[8] = (0xff);

            return buffer;
        }

        private byte[] TiltDown(byte adress, byte value)
        {
            byte[] buffer = new byte[9];
            //string[] cmd = "8x 01 06 01 0a 0a 01 03 ff".Split(' ');

            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x01);
            buffer[4] = (value);
            buffer[5] = (value);
            buffer[6] = (0x03);
            buffer[7] = (0x02);
            buffer[8] = (0xff);

            return buffer;
        }

       

        public byte[] Stop(byte adress, byte value)
        {
            byte[] buffer = new byte[9];
            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x01);
            buffer[4] = (value);
            buffer[5] = (value);
            buffer[6] = (0x03);
            buffer[7] = (0x03);
            buffer[8] = (0xff);
            return buffer;
        }

        public byte[] ZoomStop(byte adress)
        {
            byte[] buffer = new byte[6];
            buffer[0] = adress;
            buffer[1] = 0x01;
            buffer[2] = 0x04;
            buffer[3] = 0x07;
            buffer[4] = 0x00;
            buffer[5] = 0xff; 

            return buffer;

        }

        
        ///
        public void ControlPanRight(Stream stream, byte Speed)
        {

            byte[] buffer = PanRight(0x81, Speed);
            stream.Write(buffer, 0, buffer.Length);

            //System.Threading.Thread.Sleep(Speed);
            System.Threading.Thread.Sleep(300);

            buffer = Stop(0x81, 0x10);
            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);

        }


        public void ControlPanLeft(Stream stream, byte Speed)
        {

            byte[] buffer = PanLeft(0x81, Speed);
            stream.Write(buffer, 0, buffer.Length);

            //System.Threading.Thread.Sleep(Speed);
            System.Threading.Thread.Sleep(300);

            buffer = Stop(0x81, 0x10);
            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);
        }

        public void ControlTiltUp(Stream stream, byte Speed)
        {

            byte[] buffer = TiltUp(0x81, Speed);
            stream.Write(buffer, 0, buffer.Length);

            //System.Threading.Thread.Sleep(Speed);
            System.Threading.Thread.Sleep(300);

            buffer = Stop(0x81, 0x10);
            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);
        }

        public void ControlTiltDown(Stream stream, byte Speed)
        {

            byte[] buffer = TiltDown(0x81, Speed);
            stream.Write(buffer, 0, buffer.Length);

            //System.Threading.Thread.Sleep(Speed);
            System.Threading.Thread.Sleep(300);

            buffer = Stop(0x81, 0x10);
            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);
        }


        public byte[] PresetHome(byte adress)
        {
            byte[] buffer = new byte[5];
            //string[] cmd = "8x 01 06 01 0a 0a 01 03 ff".Split(' ');

            buffer[0] = (adress);
            buffer[1] = (0x01);
            buffer[2] = (0x06);
            buffer[3] = (0x04);
            buffer[4] = (0xff);
            return buffer;
        }


        public void ControlPreset1(Stream stream, byte adress)
        {

            // relative Position..
            byte[] buffer = new byte[15];
            buffer[0] = adress;
            buffer[1] = 0x01;
            buffer[2] = 0x06;
            buffer[3] = 0x03;
            buffer[4] = 0x15;
            buffer[5] = 0x15;
            buffer[6] = 0x0E; // E1E5 ~ 1E1B ( -170 ~ +170 )
            buffer[7] = 0x01; //
            buffer[8] = 0x0E; //
            buffer[9] = 0x05; //
            buffer[10] = 0x00;
            buffer[11] = 0x00;
            buffer[12] = 0x00;
            buffer[13] = 0x00;
            buffer[14] = 0xFF;
            
            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);
                        
        }

        public void ControlPreset5(Stream stream, byte adress)
        {

            byte[] buffer = new byte[15];
            buffer[0] = adress;
            buffer[1] = 0x01;
            buffer[2] = 0x06;
            buffer[3] = 0x03;
            buffer[4] = 0x15;
            buffer[5] = 0x15;
            buffer[6] = 0x01; // E1E5 ~ 1E1B ( -170 ~ +170 )
            buffer[7] = 0x0E; //
            buffer[8] = 0x01; //
            buffer[9] = 0x0B; //
            buffer[10] = 0x00;
            buffer[11] = 0x00;
            buffer[12] = 0x00;
            buffer[13] = 0x00;
            buffer[14] = 0xFF;

            stream.Write(buffer, 0, buffer.Length);
            System.Threading.Thread.Sleep(10);
        }

    }
}
