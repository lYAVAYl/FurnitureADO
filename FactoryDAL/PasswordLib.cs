using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDAL
{
    public class PasswordLib
    {
        public static bool CheckPassword(string password)
        {
            bool allCorrect = false;

            if (password.Length > 5 && password.Length < 19)
            {
                string specSymbols = "*&{}|+";
                bool oneSpecSymb = false;
                bool oneNum = false;
                bool oneBig = false;
                bool repeatSymbols = false;

                char c;

                for (int i = 0; i < password.Length; i++)
                {
                    c = password[i];
                    if(!oneNum) oneNum = char.IsDigit(c);
                    if(!oneBig) oneBig = c == char.ToUpper(c);
                    if(!oneSpecSymb) oneSpecSymb = specSymbols.Contains(c);

                    if (i<password.Length-2 && !repeatSymbols ) repeatSymbols = c == password[i + 1] && c == password[i + 2];
                }

                allCorrect = oneNum && oneBig && oneSpecSymb && !repeatSymbols;

            }

            return allCorrect;
        }


    }
}
