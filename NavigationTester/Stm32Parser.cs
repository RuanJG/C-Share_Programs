using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationTester
{
    public class Stm32Parser
    {
        private const byte PACKET_START = 0xAC;
        private const byte PACKET_END = 0xAD;
        private const byte PACKET_ESCAPE = 0xAE;
        private const byte PACKET_ESCAPE_MASK = 0x80;

        private byte[] m_cache;
        private int m_length;

        public delegate int encodeReceiveCallback(byte[] data, int start, int len);
        private encodeReceiveCallback receiveCb = null;

        public Stm32Parser(encodeReceiveCallback rhandler)
        {
            m_cache = new byte[4096];
            m_length = 0;
            receiveCb = rhandler;
        }
        public Stm32Parser()
        {
            m_cache = new byte[4096];
            m_length = 0;
            receiveCb = null;
        }

        private static int findByte(byte[] arr, int index, int len, byte needle)
        {
            int result = -1;
            for (int size = 0; size < len; ++size)
            {
                if (needle == arr[index + size])
                {
                    result = index + size;
                    break;
                }
            }
            return result;
        }

        private static byte crc8(byte[] data, int start, int size)
        {
            int i = 0;
            int j = 0;
            byte crc = 0;

            for (i = 0; i < size; ++i)
            {
                crc = (byte)(crc ^ data[start + i]);
                for (j = 0; j < 8; ++j)
                {
                    if ((crc & 0x01) != 0)
                    {
                        crc = (byte)((crc >> 1) ^ 0x8C);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        public void parse(byte[] data, int index, int len)
        {
            if (len > m_cache.Length || len < 1)
            {
                reset();
                return;
            }

            if (m_length + len > m_cache.Length)
            {
                reset();
            }

            int startIndex = 0;
            Array.Copy(data, index, m_cache, m_length, len);
            m_length += len;

            while (startIndex < m_length)
            {
                int start = findByte(m_cache, startIndex, m_length - startIndex, PACKET_START);
                if (start == -1)
                {
                    startIndex = m_length;
                    break;
                }

                int end = findByte(m_cache, startIndex, m_length - startIndex, PACKET_END);
                if (end == -1)
                {
                    break;
                }

                if (end - start + 1 >= 2)
                {
                    handleOnePacket(m_cache, start, end - start + 1);
                }
                startIndex = end + 1;
            }

            if (startIndex < 1)
            {
                return;
            }

            if (startIndex == m_length)
            {
                reset();
                return;
            }

            byte[] bak = m_cache;
            m_cache = new byte[bak.Length];

            Array.Copy(bak, startIndex, m_cache, 0, m_length - startIndex);
            m_length = m_length - startIndex;
        }

        private void handleOnePacket(byte[] data, int start, int len)
        {
            int cmdLen = 0;
            for (int index = 0; index < len; ++index)
            {
                byte byte1 = data[start + index];
                if ((PACKET_START == byte1) || (PACKET_END == byte1))
                {
                    continue;
                }

                if (PACKET_ESCAPE == byte1)
                {
                    ++index;

                    if (index >= len)
                    {
                        return;
                    }

                    byte1 = (byte)data[start + index];
                    byte1 = (byte)(byte1 ^ PACKET_ESCAPE_MASK);
                }

                data[start + cmdLen] = byte1;
                cmdLen += 1;
            }

            if (cmdLen < 2)
            {
                return;
            }

            if (crc8(data, start, cmdLen - 1) != data[start + cmdLen - 1])
            {
                //Form1.log("CRC8 Error\r\n");
                return;
            }

            parseCommand(data, start, cmdLen - 1);
        }

        
        private void parseCommand(byte[] data, int start, int len)
        {
            if (receiveCb != null)
                receiveCb(data, start, len);
        }

        public void reset()
        {
            m_length = 0;
        }

        public byte[] encodeDatas(byte[] cmd)
        {
            byte[] finalCmd = new byte[cmd.Length * 2 + 2];
            int dataIdx = 0;

            finalCmd[dataIdx++] = PACKET_START;

            for (int index = 0; index < cmd.Length; ++index)
            {
                byte byte1 = cmd[index];

                if (byte1 == PACKET_ESCAPE || byte1 == PACKET_END || byte1 == PACKET_ESCAPE)
                {
                    finalCmd[dataIdx++] = PACKET_ESCAPE;
                    byte1 = (byte)(byte1 ^ PACKET_ESCAPE_MASK);
                }

                finalCmd[dataIdx++] = byte1;
            }

            byte crc = crc8(cmd, 0, cmd.Length);
            if (crc == PACKET_ESCAPE || crc == PACKET_END || crc == PACKET_ESCAPE)
            {
                finalCmd[dataIdx++] = PACKET_ESCAPE;
                crc = (byte)(crc ^ PACKET_ESCAPE_MASK);
            }

            finalCmd[dataIdx++] = crc;
            finalCmd[dataIdx++] = PACKET_END;

            byte[] result = new byte[dataIdx];
            Array.Copy(finalCmd, 0, result, 0, dataIdx);
            return result;
        }
    }
}
