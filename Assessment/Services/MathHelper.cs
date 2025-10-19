
using Assessment.Exceptions;

namespace Assessment.Services {
    public class MathService : IMathService {
        public int FindSecondLargest(IEnumerable<int> distinctNums) {
            if (distinctNums == null || !distinctNums.Any()) {
                throw new InsufficientDataException("The collection cannot be null or empty.");
            }

            var distinctNumbers = distinctNums.Distinct();

            if (distinctNumbers.Count() < 2) {
                throw new InsufficientDataException("The collection must contain at least two distinct numbers.");
            }

            int first = int.MinValue;
            int second = int.MinValue;
            foreach (var num in distinctNumbers) {
                if (num > first) {
                    second = first;
                    first = num;
                } else if (num > second) {
                    second = num;
                }
            }


            return second;
        }
    }
}
