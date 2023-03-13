using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public class Password
    {
        private char[] NumbersCharacters = "0123456789".ToCharArray();
        private char[] LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private char[] UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private string SpecialCharacters = "!@#$%^&*()";

        private int length;
        private bool Lowercase { get; set; }    
        private bool Uppercase { get; set; }    
        private bool Special { get; set; }   
        private bool Numbers { get; set; }   
        
        private List<char> passwordString;
        private string password;

        private Random random;

        public Password()
        {
            passwordString = new List<char>();
            random = new Random();
        }

        public void Start()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Welcome to the B E S T P A S S W O R D M A N A G E R !");
            Console.WriteLine("*************************************");
            Console.WriteLine("");
        }

        

        private bool ChooseGenerator(string question)
        {
            bool repeat = true;
            bool answer = true;
            Console.WriteLine("*************************************");
            Console.WriteLine(question);
            do
            {
                Console.WriteLine("For the Yes/No option, please use the 'Y' or 'N' key.");
                string key = Console.ReadLine().ToUpper();

                switch (key)
                {
                    case "Y":
                        answer = true;
                        repeat = false;
                        break;

                    case "N":
                        answer = false;
                        repeat = false;
                        break;

                    default:
                        Console.WriteLine("Invalid election. Please use the 'Y' or 'N' key.");
                        break;
                }
            } while (repeat);

           
            

            return answer;
        }



        public void ReadInputs()
        {
            Lowercase = ChooseGenerator("Would you want to add lower case characters?");
            Uppercase = ChooseGenerator("Would you want to add upper case characters?");
            Special = ChooseGenerator("Would you want to add special characters?");
            Numbers = ChooseGenerator("Would you want to add numbers?");
            Console.WriteLine("*************************************");
            Console.WriteLine("How long do you want to keep your password length?");
            length = Convert.ToInt16(Console.ReadLine());

        }

        public void CreatePassword()
        {
            if (Lowercase)
            {
                foreach (char c in LowercaseCharacters)
                {
                    passwordString.Add(c);
                }
            }
            if (Uppercase)
            {
                foreach (char c in UppercaseCharacters)
                {
                    passwordString.Add(c);
                }
            }
            if (Special)
            {
                foreach (char c in SpecialCharacters)
                {
                    passwordString.Add(c);
                }
            }
            if (Numbers)
            {
                foreach (char c in NumbersCharacters)
                {
                    passwordString.Add(c);
                }
            }

            if(!(!Lowercase && !Uppercase && !Special && !Numbers))
            {
                for (int i = 0; i < length; i++)
                {
                    var randomIndex = random.Next(passwordString.Count);

                    password = password + passwordString[randomIndex];
                }

                Console.WriteLine("*************************************");
                Console.WriteLine(password);
                Console.WriteLine("*************************************");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("*************************************");
                Console.WriteLine("The password could not be created :(");
                Console.WriteLine("*************************************");
                Console.WriteLine("");
            }
        }

    }
}
