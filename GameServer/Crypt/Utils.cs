﻿using System;

namespace Crypt
{
    public class Utils
    {
        protected static Random Random = new Random((int) DateTime.Now.Ticks);

        public static byte[] XorKey(byte[] key1, byte[] key2)
        {
            byte[] result = new byte[Math.Min(key1.Length, key2.Length)];

            for (int i = 0; i < result.Length; i++)
                result[i] = (byte) (key1[i] ^ key2[i]);

            return result;
        }

        public static byte[] ShiftKey(byte[] src, int n, bool direction = true)
        {
            byte[] result = new byte[src.Length];

            for (int i = 0; i < src.Length; i++)
                if (direction)
                    result[(i + n)%src.Length] = src[i];
                else
                    result[i] = src[(i + n)%src.Length];

            return result;
        }

        public static byte[] Random128Key()
        {
            byte[] result = new byte[128];
            Random.NextBytes(result);
            return result;
        }
    }
}