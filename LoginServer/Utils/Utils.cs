using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

    class Converter
    {
        //Converts from Hex to Ascii
        public static string HexStringToAscii(string input)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= input.Length - 2; i += 2)
            {
                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(input.Substring(i, 2),
                    System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
        public static byte[] ExtractResource(String filename)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            foreach (string reso in a.GetManifestResourceNames())
            {
                Console.WriteLine(reso);
            }
            using (Stream resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.ReadWrite);
                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);
                // close file stream
                _FileStream.Close();
                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            // error occured, return false
            return false;
        }
    }

    /// <summary>
    /// Provides a variety of tools for working with bytes.
    /// </summary>
    public static class ByteUtilities
    {
        private static byte[] _data = new byte[] { };

        public static byte[] AppendBytes(byte[] bytes)
        {
            int i = _data.Length;
            Array.Resize(ref _data, i + bytes.Length);
            bytes.CopyTo(_data, i);

            return _data;
        }
        public static byte[] AppendBytesValue(byte[] bytes)
        {
            int i = _data.Length;
            Array.Resize(ref _data, i + bytes.Length + 4);

            byte[] lengthBytes = BitConverter.GetBytes(bytes.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);

            lengthBytes.CopyTo(_data, i);
            bytes.CopyTo(_data, i + 4);

            return _data;
        }

        public static byte[] AppendHex(String hex)
        {
            return AppendBytes(HexToBytes(hex));
        }

        public static byte[] AppendHexValue(String hex)
        {
            return AppendBytesValue(HexToBytes(hex));
        }

        public static byte[] AppendInt(int value)
        {
            byte[] lengthBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);

            return AppendBytes(lengthBytes);
        }
        public static byte[] AppendIntValue(int value)
        {
            byte[] lengthBytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);

            return AppendBytesValue(lengthBytes);
        }

        public static byte[] AppendString(String value)
        {
            return AppendBytes(Encoding.UTF8.GetBytes(value));
        }

        public static byte[] AppendStringValue(String value)
        {
            return AppendBytesValue(Encoding.UTF8.GetBytes(value));
        }

        public static byte[] Build()
        {
            byte[] result = new byte[4 + _data.Length];

            byte[] lengthBytes = BitConverter.GetBytes(_data.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthBytes);

            lengthBytes.CopyTo(result, 0);
            _data.CopyTo(result, 4);

            return result;
        }


        private static readonly string[] Baths;
        static ByteUtilities()
        {
            Baths = new string[256];
            for (int i = 0; i < 256; i++)
                Baths[i] = String.Format("{0:X2}", i);
        }

        /// <summary>
        /// Trims a given array of 8-bit signed integers.
        /// </summary>
        /// <param name="bytes">The array giving bytes to trim.</param>
        /// <param name="offset">Start of trimming.</param>
        /// <param name="length">How much to trim from offset.</param>
        /// <returns>Returns a net array containing a solid byte array with no nulls blanks.</returns>
        public static byte[] Trim(this byte[] bytes, int offset, int length)
        {
            byte[] trimmedBytes = new byte[length - offset];
            for (int i = 0, j = offset; j < length; i++, j++)
            {
                trimmedBytes[i] = bytes[j];
            }
            return trimmedBytes;
        }
        public static string ToHex(this byte[] array)
        {
            StringBuilder builder = new StringBuilder(array.Length * 2);

            for (int i = 0; i < array.Length; i++)
                builder.Append(Baths[array[i]]);

            return builder.ToString();
        }
        public static string FormatHex(this byte[] data)
        {
            StringBuilder builder = new StringBuilder(data.Length * 4);

            int count = 0;
            int pass = 1;
            foreach (byte b in data)
            {
                if (count == 0)
                    builder.AppendFormat("{0,-6}\t", "[" + (pass - 1) * 16 + "]");

                count++;
                builder.Append(b.ToString("X2"));
                if (count == 4 || count == 8 || count == 12)
                    builder.Append(" ");
                if (count == 16)
                {
                    builder.Append("\t");
                    for (int i = (pass * count) - 16; i < (pass * count); i++)
                    {
                        char c = (char)data[i];
                        if (c > 0x1f && c < 0x80)
                            builder.Append(c);
                        else
                            builder.Append(".");
                    }
                    builder.Append("\r\n");
                    count = 0;
                    pass++;
                }
            }

            return builder.ToString();
        }
        public static byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string BytesToHex(byte[] bytes)
        {
            return (bytes != null) ? BitConverter.ToString(bytes).Replace("-", "") : "";
        }

        public static byte[] HexSringToBytes(this String hexString)
        {
            try
            {
                byte[] result = new byte[hexString.Length / 2];

                for (int index = 0; index < result.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    result[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid hex string: {0}", hexString);
                throw;
            }
        }
        public static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }
        public static string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }

    }

    //Random sh1t
    public static class RandomUtilities
    {
        private static readonly Random Randomizer = new Random((int)DateTime.Now.Ticks);
        public static Random Random()
        {
            return Randomizer;
        }
        public static int DateTimeToInt(DateTime theDate)
        {
            return (int)(theDate.Date - new DateTime(1900, 1, 1)).TotalDays + 2;
        }
        public static DateTime IntToDateTime(int intDate)
        {
            return new DateTime(1900, 1, 1).AddDays(intDate - 2);
        }
        public static bool IsLuck(byte chance)
        {
            if (chance >= 100)
                return true;

            if (chance <= 0)
                return false;

            return new Random().Next(0, 100) <= chance;
        }

        //Gets current DateTime (t_time format)
        public static Int32 t_time()
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            UInt32 time_t = Convert.ToUInt32((DateTime.Now - startTime).TotalSeconds);
            return (int)time_t;
        }

        private static readonly DateTime StaticDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long GetCurrentMilliseconds()
        {
            return (long)(DateTime.UtcNow - StaticDate).TotalMilliseconds;
        }
        public static int GetRoundedUtc()
        {
            return (int)Math.Round((double)(GetCurrentMilliseconds() / 1000));
        }

    }

    /// <summary>
    /// Provides multiple ways of working with strings.
    /// </summary>
    public static class StringUtilities
    {
        #region Fields
        /// <summary>
        /// Valid characters able to be sent to runescape game.
        /// </summary>
        private static char[] validChars = {
		    '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 
		    'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 
		    't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', 
		    '3', '4', '5', '6', '7', '8', '9'
	    };
        #endregion Fields

        #region Methods
        /// <summary>
        /// Converts a given 64-bit integer to a string.
        /// </summary>
        /// <param name="value">The value of the integer.</param>
        /// <returns>Returns a string.</returns>
        public static string LongToString(this long value)
        {
            if (value <= 0L || value >= 0x5B5B57F8A98A5DD1L)
                return null;
            if (value % 37L == 0L)
                return null;

            int i = 0;
            char[] ac = new char[12];
            while (value != 0L)
            {
                long l1 = value;
                value /= 37L;
                ac[11 - i++] = validChars[(int)(l1 - value * 37L)];
            }
            return new string(ac, 12 - i, i);
        }

        /// <summary>
        /// Converts a given string to a 64-bit integer.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>Returns a 64-bit integer.</returns>
        public static long StringToLong(this string s)
        {
            long l = 0L;
            for (int i = 0; i < s.Length && i < 12; i++)
            {
                char c = s[i];
                l *= 37L;

                if (c >= 'A' && c <= 'Z')
                    l += (1 + c) - 65;
                else if (c >= 'a' && c <= 'z')
                    l += (1 + c) - 97;
                else if (c >= '0' && c <= '9')
                    l += (27 + c) - 48;
            }
            while (l % 37L == 0L && l != 0L) l /= 37L;
            return l;
        }

        /// <summary>
        /// Formats a given name for display.
        /// </summary>
        /// <param name="name">The name to format.</param>
        /// <returns>Returns a formatted string.</returns>
        public static string FormatForDisplay(string name)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name).Replace('_', ' ');
        }

        /// <summary>
        /// Formats a given name for protocol.
        /// </summary>
        /// <param name="name">The name to format.</param>
        /// <returns>Returns a formatted string.</returns>
        public static string FormatForProtocol(string name)
        {
            return name.Replace(' ', '_').ToLower();
        }
        #endregion Methods
    }
}
