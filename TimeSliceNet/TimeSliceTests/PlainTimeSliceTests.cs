using System;
using NUnit.Framework;
using TimeSlice;

namespace TimeSliceTests
{
    /// <summary>
    /// Tests <see cref="PlainTimeSlice"/>
    /// </summary>
    public class PlainTimeSliceTests
    {
        /// <summary>
        /// Tests that the duration of plain time slices is calculated as expected.
        /// </summary>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <param name="expectedDurationSeconds"></param>
        [Test]
        [TestCase("2021-07-01T00:00:00Z", " ", null)]
        [TestCase("2021-07-01T00:00:00Z", "2021-07-01T00:01:00Z", 60)]
        public void TestDuration(string startString, string endString, int? expectedDurationSeconds)
        {
            var start = DateTimeOffset.Parse(startString);
            DateTimeOffset? end = string.IsNullOrWhiteSpace(endString) ? null : DateTimeOffset.Parse(endString);
            var pts = new PlainTimeSlice
            {
                Start = start,
                End = end
            };
            var actualDuration = pts.Duration?.TotalSeconds;
            Assert.AreEqual(expectedDurationSeconds, actualDuration);
        }

        [Test]
        [TestCase("2021-07-01T00:00:00Z", " ", "Open time slice [2021-07-01T00:00:00.0000000+00:00 to infinity")]
        [TestCase("2021-07-01T00:00:00Z", "2021-07-01T00:01:00Z", "Time slice [2021-07-01T00:00:00.0000000+00:00 - 2021-07-01T00:01:00.0000000+00:00)")]
        public void TestToString(string startString, string endString, string expectedStringRepresentation)
        {
            var start = DateTimeOffset.Parse(startString);
            DateTimeOffset? end = string.IsNullOrWhiteSpace(endString) ? null : DateTimeOffset.Parse(endString);
            var pts = new PlainTimeSlice
            {
                Start = start,
                End = end
            };
            var actualString = pts.ToString();
            Assert.AreEqual(expectedStringRepresentation, actualString);
        }
    }
}