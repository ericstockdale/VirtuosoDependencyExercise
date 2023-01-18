using System.Reflection;

namespace DependencyExercise.Tests
{
    public class DependencySequencerTests
    {
        [Test]
        public void GetDependentItemsListTest()
        {
            string expectedResultStart = "left sock";
            string expectedResultFinish = "overcoat";

            var input = GetTestDependentItemsInput();
            var sequencer = new DependencySequencer();


            var actualResults = sequencer.GetDependentItemsList(input);

            Assert.AreEqual(expectedResultStart, actualResults[0].Name);
            Assert.AreEqual(expectedResultFinish, actualResults[^1].Name);
        }

        [Test]
        public void GetDependentItemsOutputTest()
        {
            var expectedResult = GetExpectedDependentItemsOutput();

            var input = GetTestDependentItemsInput();
            var sequencer = new DependencySequencer();

            var actualResult = sequencer.GetDependentItemsOutput(sequencer.GetDependentItemsList(input));

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void WriteDependentItemsOutputTest()
        {
            var expectedResult = GetExpectedDependentItemsOutput();

            string filePath = Assembly.GetExecutingAssembly().Location + "DependencyOutput.txt";
            var input = GetTestDependentItemsInput();
            var sequencer = new DependencySequencer();

            sequencer.WriteDependentItemsOutput(sequencer.GetDependentItemsOutput(sequencer.GetDependentItemsList(input)), filePath);
            string actualResult = File.ReadAllText(filePath);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void WriteLargeDependentItemsOutputTest()
        {
            var input = GetLargeTestDependentItemsInput();
            var sequencer = new DependencySequencer();

            DateTime start = DateTime.Now;
            var actualResult = sequencer.GetDependentItemsOutput(sequencer.GetDependentItemsList(input));
            DateTime end = DateTime.Now;

            Assert.IsTrue(end.Subtract(start) < TimeSpan.FromSeconds(3));
        }

        private string[,] GetTestDependentItemsInput()
        {
            return new string[,]
            {
                //dependency    //item
                {"t-shirt",     "dress shirt"},
                {"dress shirt", "pants"},
                {"dress shirt", "suit jacket"},
                {"tie",         "suit jacket"},
                {"pants",       "suit jacket"},
                {"belt",        "suit jacket"},
                {"suit jacket", "overcoat"},
                {"dress shirt", "tie"},
                {"suit jacket", "sun glasses"},
                {"sun glasses", "overcoat"},
                {"left sock",   "pants"},
                {"pants",       "belt"},
                {"suit jacket", "left shoe"},
                {"suit jacket", "right shoe"},
                {"left shoe",   "overcoat"},
                {"right sock",  "pants"},
                {"right shoe",  "overcoat"},
                {"t-shirt",     "suit jacket"}
            };
        }

        private string[,] GetLargeTestDependentItemsInput()
        {
            int totalLength = 5000;
            string[,] input = new string[totalLength, 2];
            int index = 0;
            while (index < totalLength)
            {
                input[index, 0] = index.ToString();
                input[index, 1] = (index + 1).ToString();
                input[index + 1, 0] = index.ToString();
                input[index + 1, 1] = (index + 2).ToString();
                input[index + 2, 0] = index.ToString();
                input[index + 2, 1] = (index + 3).ToString();
                input[index + 3, 0] = index.ToString();
                input[index + 3, 1] = (index + 4).ToString();
                input[index + 4, 0] = index.ToString();
                input[index + 4, 1] = (index + 5).ToString();
                index += 5;

            }

            return input;
        }

        private string GetExpectedDependentItemsOutput()
        {
            return @"left sock, right sock, t-shirt
dress shirt
pants, tie
belt
suit jacket
left shoe, right shoe, sun glasses
overcoat";
        }
    }
}