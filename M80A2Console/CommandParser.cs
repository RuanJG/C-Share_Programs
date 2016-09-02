using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M80A2Console
{
    public class CommandParser
    {
        private byte[] m_cache = new byte[2048];
        private int m_length = 0;

        public byte[] packResponse(byte[] data)
        {
            byte[] result = new byte[data.Length + 2];
            result[0] = (byte)((data.Length >> 8) & 0xFF);
            result[1] = (byte)((data.Length) & 0xFF);
            Array.Copy(data, 0, result, 2, data.Length);
            return result;
        }

        public List<byte[]> parse(byte[] data, int start, int size)
        {

            List<byte[]> commands = new List<byte[]>();
            if (size > m_cache.Length || size < 1)
            {
                m_length = 0;
                return commands;
            }

            if (m_length + size > m_cache.Length)
            {
                m_length = 0;
            }

            int startIndex = 0;
            Array.Copy(data, start, m_cache, m_length, size);
            m_length += size;

            while (startIndex < m_length)
            {
                int remain = m_length - startIndex;
                if (remain < 2)
                {
                    break;
                }

                byte byte1 = m_cache[startIndex + 0];
                byte byte2 = m_cache[startIndex + 1];
                int length = ((byte1 << 8) & 0xFF00) + (byte2 & 0x00FF);

                if (length < 1)
                {
                    startIndex += 2;
                    continue;
                }

                if (remain - 2 < length)
                {
                    break;
                }

                byte[] cmd = new byte[length];
                Array.Copy(m_cache, startIndex + 2, cmd, 0, length);

                startIndex += (2 + length);
                commands.Add(cmd);
            }

            if (startIndex < 1)
            {
                return commands;
            }

            if (startIndex == m_length)
            {
                m_length = 0;
                return commands;
            }

            byte[] bak = m_cache;
            m_cache = new byte[bak.Length];
            Array.Copy(bak, startIndex, m_cache, 0, m_length - startIndex);

            m_length = m_length - startIndex;
            return commands;
        }
    }
}
