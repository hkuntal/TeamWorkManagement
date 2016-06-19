using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DisplayEncodedStringFromMetadata();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        private static void DisplayEncodedStringFromMetadata()
        {
            byte[] fileData = System.IO.File.ReadAllBytes(@"encodingIssue.txt");

            var contentString = Encoding.UTF8.GetString(fileData, 613, 19);

            Console.WriteLine("image pixel data string: " + contentString);

            Console.WriteLine("CheckSpacingAttrValidAndSquare: " + CheckSpacingAttrValidAndSquare(contentString));

            Console.ReadLine();
        }

        private static bool CheckSpacingAttrValidAndSquare(string spacingAttr)
        {
            // We need to check imager pixel spacing for the following modalities - XA, XRF, DX, MG, CR
            if (!string.IsNullOrEmpty(spacingAttr))
            {
                var spacing = spacingAttr.Split('\\');

                // NOTE: The pixel spacing is sometimes "-1".
                if (spacing.Length >= 2)
                {
                    Console.WriteLine("Split successfully");
                    var horizontalSpacing = spacing[0];
                    var verticalSpacing = spacing[1];
                    double x, y;
                    if (double.TryParse(horizontalSpacing, out x) && double.TryParse(verticalSpacing, out y))
                    {
                        return x > 0 && y > 0 && x.Equals(y);
                    }
                }
            }
            return false;
        }
    }
}
