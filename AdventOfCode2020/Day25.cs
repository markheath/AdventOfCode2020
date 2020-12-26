using System;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day25 : ISolver
    {
        public (string Part1, string Part2) ExpectedResult => ("6408263", "");

        public long Transform(long subject, int loopSize)
        {
            var value = 1L;
            for (var n = 0; n < loopSize; n++)
            {
                // Set the value to itself multiplied by the subject number.
                value *= subject;
                // Set the value to the remainder after dividing the value by 20201227.
                value %= 20201227;
            }
            return value;
        }

        public (int LoopSize, long PublicKey) Search(long subject, long pk1, long pk2)
        {
            var value = 1L;
            for (var loopSize = 1; true; loopSize++)
            {
                // Set the value to itself multiplied by the subject number.
                value *= subject;
                // Set the value to the remainder after dividing the value by 20201227.
                value %= 20201227;
                if (value == pk1) return (loopSize, pk1);
                if (value == pk2) return (loopSize, pk2);
            }
            throw new InvalidOperationException();
        }

        public int FindSecretLoopSize(long subjectNumber, long expectedPublicKey)
        {
            for (var n = 1; true; n++)
            {
                if (Transform(subjectNumber, n) == expectedPublicKey)
                    return n;

            }
            throw new InvalidOperationException();
        }


        public (string Part1, string Part2) Solve(string[] input)
        {
            var cardPublicKey = long.Parse(input[0]);
            var doorPublicKey = long.Parse(input[1]);
            var (loopSize, pk) = Search(7, doorPublicKey, cardPublicKey);
            long encryptionKey;
            if (pk == doorPublicKey)
                encryptionKey = Transform(cardPublicKey, loopSize);
            else
                encryptionKey = Transform(doorPublicKey, loopSize);
            return (encryptionKey.ToString(), "");
        }
    }
}
