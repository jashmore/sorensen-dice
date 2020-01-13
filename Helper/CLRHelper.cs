using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

namespace SorensenDice.Helper
{
    public class CLRFunctions
    {
        [SqlFunction(
            DataAccess = DataAccessKind.None,
            FillRowMethodName = "FillRowMethod",
            IsDeterministic = true)
        ]        
        public static string HelloWorld()
        {
            return "Hello World";
        }

        [SqlFunction(
            DataAccess = DataAccessKind.None,
            FillRowMethodName = "FillRowMethod",
            IsDeterministic = true)
        ]
        public static decimal ComputeSorensenDiceIndex(string fingerprint1, string fingerprint2)
        {
            int[] fp1 = Array.ConvertAll(fingerprint1.Split(','), int.Parse);
            int[] fp2 = Array.ConvertAll(fingerprint2.Split(','), int.Parse);

            return SorensenDiceHelper.Intersection(fp1, fp2);
        }

        public static void FillRowMethod(object input, out SqlChars results)
        {
            results = new SqlChars(input.ToString());
        }
    }
}