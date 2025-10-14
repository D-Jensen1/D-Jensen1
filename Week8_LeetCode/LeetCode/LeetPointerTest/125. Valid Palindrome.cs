using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeetPointerTest;

public class _125
{
    public bool IsPalindrome(string s)
    {
        int pointerLeft = 0;
        int pointerRight = s.Length - 1;

        while (pointerLeft < pointerRight)
        {
            if (Char.ToLower(s[pointerLeft]) != Char.ToLower(s[pointerRight]))
            {
                if (!Char.IsLetterOrDigit(s[pointerLeft]))
                {
                    pointerLeft++;
                    continue;
                }

                else if (!Char.IsLetterOrDigit(s[pointerRight]))
                {
                    pointerRight--;
                    continue;
                }
                else
                    return false;
            }

            pointerLeft++;
            pointerRight--;
        }
        return true;
    }

    [TestClass]
    public class _125Test
    {
        private _125 _solution = new _125();

        [TestMethod]
        public void IsPalindrome_BasicPalindrome_ReturnsTrue()
        {
            // Arrange
            string s = "A man a plan a canal Panama";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_NotPalindrome_ReturnsFalse()
        {
            // Arrange
            string s = "race a car";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_EmptyString_ReturnsTrue()
        {
            // Arrange
            string s = "";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_SingleCharacter_ReturnsTrue()
        {
            // Arrange
            string s = "a";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_OnlySpaces_ReturnsTrue()
        {
            // Arrange
            string s = " ";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_OnlyNonAlphanumeric_ReturnsTrue()
        {
            // Arrange
            string s = ".,!@#$%^&*()";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_NumbersOnly_ReturnsTrue()
        {
            // Arrange
            string s = "12321";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_NumbersOnly_ReturnsFalse()
        {
            // Arrange
            string s = "12345";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_MixedCaseLetters_ReturnsTrue()
        {
            // Arrange
            string s = "Aa";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_TwoCharacters_Different_ReturnsFalse()
        {
            // Arrange
            string s = "ab";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_NumbersAndLetters_ReturnsTrue()
        {
            // Arrange
            string s = "1a1";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_NumbersAndLetters_ReturnsFalse()
        {
            // Arrange
            string s = "1a2";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_WithPunctuationAndSpaces_ReturnsTrue()
        {
            // Arrange
            string s = "Was it a car or a cat I saw?";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_LeadingAndTrailingSpaces_ReturnsTrue()
        {
            // Arrange
            string s = "   aba   ";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_MultipleSpacesBetween_ReturnsTrue()
        {
            // Arrange
            string s = "a   b   a";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_LongPalindrome_ReturnsTrue()
        {
            // Arrange
            string s = "Madam, I'm Adam";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_LongNonPalindrome_ReturnsFalse()
        {
            // Arrange
            string s = "This is not a palindrome";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_AllUppercase_ReturnsTrue()
        {
            // Arrange
            string s = "RACECAR";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_AllLowercase_ReturnsTrue()
        {
            // Arrange
            string s = "racecar";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_LeetCodeExample1_ReturnsTrue()
        {
            // Arrange - Example from LeetCode
            string s = "A man, a plan, a canal: Panama";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_LeetCodeExample2_ReturnsFalse()
        {
            // Arrange - Example from LeetCode
            string s = "race a car";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPalindrome_LeetCodeExample3_ReturnsTrue()
        {
            // Arrange - Example from LeetCode (edge case)
            string s = " ";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_AlphanumericAtEnds_ReturnsTrue()
        {
            // Arrange
            string s = "!@#a$%^&*()b)(a!@#";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPalindrome_VeryLongPalindrome_ReturnsTrue()
        {
            // Arrange
            string s = "abcdefghijklmnopqrstuvwxyz.,!@#$%^&*()zyxwvutsrqponmlkjihgfedcba";

            // Act
            bool result = _solution.IsPalindrome(s);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
