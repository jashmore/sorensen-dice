using System;

namespace SorensenDice.Helper
{
    public static class SorensenDiceHelper
    {
        /// <summary>
        /// Computes the string fingerprint
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static int[] ComputeFingerPrint(string inputString)
        {
            if (inputString == null)
            {
                return null;
            }

            inputString = Normalise(inputString);
            char[] inputCharArray = inputString.ToCharArray();

            int len = inputCharArray.Length;
            int[] fp = new int[len];

            for (int i = 0; i < len; i++)
            {
                fp[i] = inputCharArray[i] << 8;
                if (i != 0)
                {
                    fp[i - 1] |= inputCharArray[i];
                }
            }

            Array.Sort(fp);
            return fp;
        }


        public static double ComputeSorensenDiceIndex(int[] fp1, int[] fp2)
        {
            // The “double” forces floating point division
            // otherwise it uses integer division and the result here is always 0 or 1
            double score = Intersection(fp1, fp2);
            return (2 * score) / (fp2.Length + fp1.Length);
        }

        private static string Normalise(string s)
        {
            s = s.Replace("'", "");
            s = s.Replace("-", " ");
            s = s.Replace("  ", " ");
            return s.ToLower();
        }

        /// <summary>
        /// Counts the Intersection of two fingerprints
        /// </summary>
        /// <param name="fp1"></param>
        /// <param name="fp2"></param>
        /// <returns>Returns an integer value representing ... </returns>
        public static int Intersection(int[] fp1, int[] fp2)
        {
            int matches = 0;
            int i = 0;
            int j = 0;
            while (i < fp1.Length && j < fp2.Length)
            {
                if (fp1[i] == fp2[j])
                {
                    matches++;
                    i++;
                    j++;
                }
                else if (fp1[i] < fp2[j])
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }

            return matches;
        }
    }
}