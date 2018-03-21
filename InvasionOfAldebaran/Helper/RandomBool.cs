using System;

namespace InvasionOfAldebaran.Helper
{
    /// <summary>
    /// Offers a quick way to use random boolean values
    /// </summary>
    public static class RandomBool
    {
        private static readonly Random r = new Random();

        /// <summary>
        /// Returns a true or false value with a 50:50 chance for true
        /// </summary>
        public static bool Get()
        {
            var value = r.Next(0, 2);
            switch (value)
            {
                case 0:
                    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }
    }
}