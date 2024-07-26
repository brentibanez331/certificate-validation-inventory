using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utilities
{
    public static class CertificateCodeGenerator
    {
        private static readonly Random random = new();

        public static string GenerateCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public static class CertificateFilePathNameGenerator
    {
        public static string GenerateUniqueFilename(string fileExtension)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var uniqueFilename = new string(Enumerable.Repeat(chars, 20)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());
            return uniqueFilename + fileExtension;
        }
    }

}