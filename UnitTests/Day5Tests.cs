using AdventOfCode2020;
using NUnit.Framework;

namespace UnitTests
{
    public class Day5Tests
    {
        [TestCase("FBFBBFFRLR", 357)]
        [TestCase("BFFFBBFRRR", 567)]
        [TestCase("FFFBBBFRRR", 119)]
        [TestCase("BBFFBBFRLL", 820)]
        public void CanParseSeatId(string seat, int seatId)
        {
            Assert.AreEqual(seatId, Day5.SeatId(seat));
        }
    }

}