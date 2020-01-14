using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

namespace SorensenDice.Helper
{
    public class CLRFunctions
    {
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            IsDeterministic = true)
        ]
        [return: SqlFacet(Precision = 8, Scale = 6)]
        public static decimal ComputeSorensenDiceIndex(string fingerprint1, string fingerprint2)
        {
            int[] fp1 = Array.ConvertAll(fingerprint1.Split(','), int.Parse);
            int[] fp2 = Array.ConvertAll(fingerprint2.Split(','), int.Parse);

            return SorensenDiceHelper.ComputeSorensenDiceIndex(fp1, fp2);
        }

        [SqlFunction(
            DataAccess = DataAccessKind.None,
            FillRowMethodName = "FillRowMethod",
            IsDeterministic = true)
        ]
        public static string GetSorensenDiceFingerprint(string input)
        {
            int[] fingerprint = SorensenDiceHelper.ComputeFingerPrint(input);

            return fingerprint != null &&  fingerprint.Any() ? string.Join(",", fingerprint) : null;
        }

        public static void FillRowMethod(object input, out SqlChars results)
        {
            results = new SqlChars(input.ToString());
        }
    }
}