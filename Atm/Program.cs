using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm
{
    class Program
    {
        static void Main(string[] args)
        {
            User[] users = new User[5];
            users[0]=new User("Majid","Gurbanli",new Card("1111222233334444","2000","655","27/12/2022",107));
            users[1] = new User("Farid", "Imanzada", new Card("5555666677778888", "2001", "656", "31/12/2022", 95));
            users[2] = new User("Faxri", "Nuriyev", new Card("4444666622228888", "2002", "657", "01/12/2022", 106));
            users[3] = new User("Cavid", "Guliyev", new Card("1234123412341234", "2003", "658", "01/10/2022", 9));
            users[4] = new User("Murad", "Ibrahimxanli", new Card("114411441144", "2004", "659", "01/10/2023", 100000));
            Console.WriteLine("Zehmet olmasa pin kodunuzu daxil edin");
             int.TryParse(Console.ReadLine(), out int passInt);
            OpenMenu(passInt, users);
           
           
                





        }
        static int ReturnValidUser(int passInt,User[] users)
        {
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].CheckUserCode(passInt) == true)
                {
                    return i;
                }

            }
            return -1;
        }
        static void OpenMenu(int passInt, User[] users)
        {
            int userId = ReturnValidUser(passInt, users);
            if (userId == -1)
            {
                Console.WriteLine("Parolu Yanlish Daxil etdiniz");
                Console.WriteLine("Zehmet olmasa pin kodunuzu daxil edin");
                int.TryParse(Console.ReadLine(), out int passSecondInt);
                OpenMenu(passSecondInt, users);
            }
            else
            {
                Console.WriteLine($"{users[userId].Name}   {users[userId].Surname} Xosh gelmisiniz ");
                Console.WriteLine("Zehmet olmasa Secin");
                Console.WriteLine("Balans ucun 1 Nagd Pul Ucun 2");
                int.TryParse(Console.ReadLine(), out int activity);
                if (activity == 1)
                {

                    Console.WriteLine($"Balansiniz { users[userId].returnBalance() } ");

                }
                else if(activity == 2)
                {
                    Console.WriteLine("1. 10 Azn");
                    Console.WriteLine("2. 20 Azn");
                    Console.WriteLine("3. 50 Azn");
                    Console.WriteLine("4. 100 Azn");
                    Console.WriteLine("5. diger mebleg");
                    int.TryParse(Console.ReadLine(), out int moneyCheck);
                    switch (moneyCheck)
                    {
                        case 1:
                            BalansValidChecker(users[userId], 10,users,passInt);
                            

                            break;
                        case 2:
                            BalansValidChecker(users[userId], 20, users, passInt);

                            break;
                        case 3:
                            BalansValidChecker(users[userId], 50, users, passInt);
                            break;
                        case 4:
                            BalansValidChecker(users[userId], 100, users, passInt);
                            break;
                        case 5:
                            Console.WriteLine("Diger meblegi daxil edin");
                            int.TryParse(Console.ReadLine(), out int money);
                            BalansValidChecker(users[userId], money, users, passInt);


                            break;
                    }


                }
            }
        }
        static void BalansValidChecker(User myUser, decimal money,User []myUsers, int pass)
        {
            if (myUser.decreaseBalance(money) == -1)
            {
                Console.WriteLine("Balansinizda bu qeder mebleg yoxdu");
                Console.WriteLine($" Balansinizda olan mebleg: {myUser.returnBalance()}");
            }
            else
            {
                Console.WriteLine($"Balansinizdan {money} azn cixildi");
                Console.WriteLine($"Balansinizda qalan mebleg {myUser.returnBalance()}");
                OpenMenu(pass, myUsers);

            }
        }
    }
    class Card
    {
        private string panStr;
        private string pinStr;
        private string cvcStr;
        private string dateStr;
        public decimal BalanceDec {  get; set; }

        public Card(string panStr, string pinStr, string cvcStr, string dateStr, decimal balanceDec)
        {
            this.panStr = panStr;
            this.pinStr = pinStr;
            this.cvcStr = cvcStr;
            this.dateStr = dateStr;
            this.BalanceDec = balanceDec;
        }
        public bool isSameCode(int pinInt)
        {
            int localPinInt = int.Parse(pinStr);
            if (localPinInt == pinInt)
            {
                return true;
            }

            return false;
        }

    }
    class User
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }

        private Card creditCard;
        

        public User(string name, string surname, Card creditCard)
        {
            this.Name = name;
            this.Surname = surname;
            this.creditCard = creditCard;
        }
        public bool CheckUserCode(int pinInt)
        {
            if (creditCard.isSameCode(pinInt) == true)
            {
                return true;
            }



            return false;
        }
        public decimal returnBalance()
        {
            return creditCard.BalanceDec;
        }
        public decimal decreaseBalance(decimal decreasedAmount)
        {
            if (decreasedAmount <= creditCard.BalanceDec)
            {

                creditCard.BalanceDec = creditCard.BalanceDec - decreasedAmount;
                return creditCard.BalanceDec;
            }
            return -1;
        }
    }
}
