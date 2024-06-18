using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyVatTu.ReportForm
{
    class ConvertMoney
    {
        private static string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static string[] teens = { "mười", "mười một", "mười hai", "mười ba", "mười bốn", "mười lăm", "mười sáu", "mười bảy", "mười tám", "mười chín" };
        private static string[] tens = { "", "", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };
        private static string[] thousandsGroups = { "", "nghìn", "triệu", "tỷ" };

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "không";

            if (number < 0)
                return "âm " + NumberToWords(Math.Abs(number));

            string words = "";

            int thousandGroup = 0;

            while (number > 0)
            {
                int groupNumber = number % 1000;
                number /= 1000;

                if (groupNumber != 0)
                {
                    string groupWords = GroupToWords(groupNumber);
                    words = groupWords + " " + thousandsGroups[thousandGroup] + " " + words;
                }

                thousandGroup++;
            }

            return words.Trim();
        }

        private static string GroupToWords(int number)
        {
            string words = "";

            if (number / 100 > 0)
            {
                words += units[number / 100] + " trăm ";
                number %= 100;
            }

            if (number > 0)
            {
                if (number < 10)
                    words += units[number];
                else if (number < 20)
                    words += teens[number - 10];
                else
                {
                    words += tens[number / 10];
                    if (number % 10 > 0)
                        words += " " + units[number % 10];
                }
            }

            return words.Trim();
        }

        public static string ConvertToText(int number)
        {
            string words = NumberToWords(number);
            return words + " đồng";
        }
    }
}
