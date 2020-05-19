namespace Utils
{
    public class CharUtils
    {
        /**
         * This method receives a digit represented as a character and returns whether the digit is even or not.
         */
        public static bool IsEvenDigit(char i_Digit)
        {
            return char.IsDigit(i_Digit) && i_Digit % 2 == 0;
        }
    }
}