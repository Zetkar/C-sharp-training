using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalcKata
{
    public class StringCalc
    {

        public int Add(string numbers)
        {
            List<string> delimiters = new List<string> {",","\n"};
            int[] numbersArray;

            if (String.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            numbers = CheckAndExtractStringForCustomDelitimers(numbers, delimiters);
            numbersArray = SplitNumbersByDelimiters(numbers, delimiters.ToArray());

            return SumExcludingMoreThanThousandAndNegatives(numbersArray);         
        }

        private Func<string, string[], int[]> SplitNumbersByDelimiters = (string numbers, string[] delimiters) =>
                numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(number => int.Parse(number)).ToArray(); 

        private string CheckAndExtractStringForCustomDelitimers(string numbers, List<string> delimiters)
        {
            string delimiterPart;

            if (numbers.StartsWith("//"))
            {
                int indexForSplitting = numbers.IndexOf("\n");               
                delimiterPart = numbers.Substring(2, indexForSplitting -2);
                numbers = numbers.Substring(indexForSplitting + 1);

                if (delimiterPart.StartsWith("["))
                {
                    if (delimiterPart.Contains("]["))
                    {
                        delimiterPart = delimiterPart.Substring(1, delimiterPart.Length - 2);
                        delimiterPart.Split("][")
                            .Select(delimiter => delimiter)
                            .ToList()
                            .ForEach(delimiter => delimiters.Add(delimiter));
                    }
                    else
                    {
                        delimiters.Add(delimiterPart.Substring(1, delimiterPart.Length - 2));
                    }
                } 
                else
                {
                    delimiters.Add(delimiterPart);
                }
            }

            return numbers;
        }

        private int SumExcludingMoreThanThousandAndNegatives(int[] array)
        {
            List<int> negativeNumbers = new List<int>();
            int sum = 0;

            foreach(int n in array)
            {
                if (n < 0)
                    negativeNumbers.Add(n);
                if (n <= 1000)
                {
                    sum += n;
                }
            }

            if (negativeNumbers.Count > 0)
            {
                throw new ArithmeticException("Negative numbers found: "
                    + string.Join(",", negativeNumbers));
            }

            return sum;
        }
    }   
}
