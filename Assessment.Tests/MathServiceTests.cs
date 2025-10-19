using FluentAssertions;
using Assessment.Services;
using Assessment.Exceptions;

namespace Assessment.Tests {
    // The [Fact] attribute denotes a single test without data.
    // The [Theory] attribute allows running the same test logic with multiple data sets.
    public class MathServiceTests {
        private readonly MathService _sut;

        public MathServiceTests() {
            _sut = new MathService();
        }


        [Theory(DisplayName = "Should correctly find the second largest integer in various lists")]
        [InlineData(new int[] { 10, 5, 20, 15, 20 }, 15)]
        [InlineData(new int[] { 5, 5, 5, 10, 10 }, 5)]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new int[] { 5, 4, 3, 2, 1 }, 4)]
        [InlineData(new int[] { 100, 100, 50, 50 }, 50)]
        public void FindSecondLargest_ShouldReturnCorrectValue_ForStandardInputs(int[] input, int expected) {
            int actual = _sut.FindSecondLargest(input);

            actual.Should().Be(expected, "because the method should find the second largest distinct number.");
        }

        [Fact(DisplayName = "Should handle zero and negative numbers correctly")]
        public void FindSecondLargest_ShouldHandleZeroAndNegativeNumbers() {
            var input = new List<int> { -10, -5, -20, 0, -1 };

            int actual = _sut.FindSecondLargest(input);

            actual.Should().Be(-1);
        }

        [Fact(DisplayName = "Should correctly handle int.MinValue as the largest number")]
        public void FindSecondLargest_ShouldHandleMinValueAsLargest() {
            var input = new List<int> { int.MinValue, int.MinValue + 1 };

            int actual = _sut.FindSecondLargest(input);

            actual.Should().Be(int.MinValue);
        }

        [Fact(DisplayName = "Should correctly handle int.MaxValue and int.MinValue")]
        public void FindSecondLargest_ShouldHandleMaxAndMinValue() {
            var input = new List<int> { int.MaxValue, int.MinValue };

            int actual = _sut.FindSecondLargest(input);

            actual.Should().Be(int.MinValue);
        }

        // --- Exception/Validation Tests ---

        [Fact(DisplayName = "Should throw exception when input is null")]
        public void FindSecondLargest_ShouldThrowException_WhenInputIsNull() {
            IEnumerable<int> input = null;

            Action act = () => _sut.FindSecondLargest(input);

            act.Should().Throw<InsufficientDataException>()
                .WithMessage("The collection cannot be null or empty.");
        }

        [Fact(DisplayName = "Should throw exception when input is empty")]
        public void FindSecondLargest_ShouldThrowException_WhenInputIsEmpty() {
            var input = Enumerable.Empty<int>();

            Action act = () => _sut.FindSecondLargest(input);

            act.Should().Throw<InsufficientDataException>()
                .WithMessage("The collection cannot be null or empty.");
        }

        [Fact(DisplayName = "Should throw exception when array has only one distinct number")]
        public void FindSecondLargest_ShouldThrowException_WhenOnlyOneDistinctNumberExists() {
            var input = new List<int> { 5, 5, 5 };

            Action act = () => _sut.FindSecondLargest(input);

            act.Should().Throw<InsufficientDataException>()
                .WithMessage("The collection must contain at least two distinct numbers.");
        }

        [Fact(DisplayName = "Should throw exception when array has only two distinct numbers but one is maxvalue")]
        public void FindSecondLargest_ShouldThrowException_WhenArrayHasOnlyOneTotalNumber() {
            var input = new List<int> { 42 };

            Action act = () => _sut.FindSecondLargest(input);

            act.Should().Throw<InsufficientDataException>()
                .WithMessage("The collection must contain at least two distinct numbers.");
        }
    }
}