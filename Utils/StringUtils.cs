using System;

namespace Utils
{
    public class StringUtils
    {
        // Constants declaration.
        public const int k_MinPhoneNumberLength = 9;
        public const int k_MaxPhoneNumberLength = 10;

        /**
         * This method receives a binary number as a string and converts the number into a decimal number and returns it.
         */
        public static int ConvertBinaryToDecimal(string i_BinaryNumber)
        {
            int convertedDecimalNumber = 0;

            for (int i = 0; i < i_BinaryNumber.Length; i++)
            {
                int power = (int)Math.Pow(2, i_BinaryNumber.Length - 1 - i); // Calculating the power for the i digit.
                int digit = (int)char.GetNumericValue(i_BinaryNumber[i]); // Getting the real digit value (0 or 1).

                convertedDecimalNumber += digit * power;
            }

            return convertedDecimalNumber;
        }

        /**
         * This method receives a string and returns whether the string is a Palindrome.
         */
        public static bool IsPalindrome(string i_UserInput)
        {
            bool isValid = true;

            for (int i = 0; i < i_UserInput.Length / 2 && isValid; i++)
            {
                if (i_UserInput[i] != i_UserInput[i_UserInput.Length - 1 - i])
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        /**
         * This method receives a string and returns the maximum character in the string.
         */
        public static char GetMaxChar(string i_Str)
        {
            char maxChar = i_Str[0];

            for (int i = 1; i < i_Str.Length; i++)
            {
                if (i_Str[i] > maxChar)
                {
                    maxChar = i_Str[i];
                }
            }

            return maxChar;
        }

        /**
         * This method receives a string and returns the minimum character in the string.
         */
        public static char GetMinChar(string i_Str)
        {
            char minChar = i_Str[0];

            for (int i = 1; i < i_Str.Length; i++)
            {
                if (i_Str[i] < minChar)
                {
                    minChar = i_Str[i];
                }
            }

            return minChar;
        }

        /**
         * This method receives a string and returns whether the string is a binary number or not.
         */
        public static bool IsBinaryNumber(string i_Str)
        {
            bool isValid = true;

            // Checking that every character in the string is either 0 or 1.
            for (int i = 0; i < i_Str.Length && isValid; i++)
            {
                if (i_Str[i] != '0' && i_Str[i] != '1')
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        /**
         * This method receives a string and returns whether the string represents a number.
         */
        public static bool IsInteger(string i_Str)
        {
            int parsed;
            bool isValid = int.TryParse(i_Str, out parsed);

            return isValid;
        }

        /**
         * This method receives a string and returns whether the string contains only letters and digits or not.
         */
        public static bool IsOnlyLettersAndDigitsString(string i_Str)
        {
            bool hasOnlyLettersAndDigits = true;

            for (int i = 0; i < i_Str.Length; i++)
            {
                if (!char.IsLetterOrDigit(i_Str[i]))
                {
                    hasOnlyLettersAndDigits = false;
                    break;
                }
            }

            return hasOnlyLettersAndDigits;
        }

        /**
         * This method receives a string and returns whether the string contains only letters or not.
         */
        public static bool IsLettersOnlyString(string i_Str)
        {
            bool isLettersOnly = true;

            for (int i = 0; i < i_Str.Length; i++)
            {
                if (!char.IsLetter(i_Str[i]))
                {
                    isLettersOnly = false;
                    break;
                }
            }

            return isLettersOnly;
        }

        /**
         * This method receives a string, a max length parameter and accept digits parameter.
         * The method then returns whether the string doesn't exceed the maximum length allowed, and it's a valid name (or a nickname if the accepts digits is turned on).
         * A valid name must not contain spaces.
         */
        public static bool IsNameValid(string i_Name, int i_MaxLength, bool i_AcceptDigits = false, bool i_AcceptSpecialCharacters = false)
        {
            bool isNameValid = true;

            if (i_Name.Length > i_MaxLength || string.IsNullOrEmpty(i_Name))
            {
                isNameValid = false;
            }
            else if (i_Name.Contains(" "))
            {
                isNameValid = false;
            }
            else if (!i_AcceptSpecialCharacters)
            {
                if (i_AcceptDigits && !IsOnlyLettersAndDigitsString(i_Name))
                {
                    isNameValid = false;
                }
                else if (!i_AcceptDigits && !IsLettersOnlyString(i_Name))
                {
                    isNameValid = false;
                }
            }

            return isNameValid;
        }

        /**
         * This method receives a name and returns the name with posession format.
         * If the name ends with 's' it will add "'" at the end of the name and otherwise it will add "'s" at the end of the name.
         */
        public static string GetFormattedNamePossession(string i_Name)
        {
            bool nameEndsWithS = char.ToLower(i_Name[i_Name.Length - 1]) == 's';
            string formattedName;

            if (nameEndsWithS)
            {
                formattedName = string.Format("{0}'", i_Name);
            }
            else
            {
                formattedName = string.Format("{0}'s", i_Name);
            }

            return formattedName;
        }

        /**
         * This method receives a phone number string and returns whether the phone number is valid or not.
         */
        public static bool IsValidPhoneNumber(string i_PhoneNumber)
        {
            bool inRange = i_PhoneNumber.Length >= k_MinPhoneNumberLength && i_PhoneNumber.Length <= k_MaxPhoneNumberLength;

            return IsInteger(i_PhoneNumber) && inRange;
        }
    }
}