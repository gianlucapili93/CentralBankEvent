using System;

namespace CentralBank
{
    internal class Program
    {
        //Quando il nome della banca centrale viene modifica viene notificato alle banche ad essa ereditate
        public delegate void Feedback(object source, NotificationArgs e);
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            CryptoWallet wallet = new CryptoWallet();
            Feedback feedback = new Feedback(Notification);
            bank.Notify += feedback;
            wallet.Notify += feedback;

            Console.WriteLine("Cambia il nome della banca centrale: ");
            bank.CentralBankName = Console.ReadLine();
        }

        public class NotificationArgs : EventArgs
        {
            public string Message;
            public NotificationArgs(string message)
            {
                this.Message = message;
            }
        }
        public static void Notification(object source, NotificationArgs e)
        {
            Console.WriteLine(e.Message);
        }

        public abstract class CentralBank
        {
            public virtual string CentralBankName { get; set; } = "Banca Centrale";
            public virtual event Feedback Notify;
        }
        public class Bank : CentralBank
        {
            public string BankName { get; set; } = "Banca";
            public override event Feedback Notify;
            public override string CentralBankName 
            { 
                get { return base.CentralBankName; } 
                set 
                { 
                    if(base.CentralBankName != value)
                    {
                        if(Notify != null)
                        {
                            NotificationArgs msg = new ($"{this.BankName}: Il nome della banca è cambiato da {this.CentralBankName} in {value}");
                            Notify(this, msg);
                            base.CentralBankName = value;
                        }
                    }
                } 
            }
        }
        public class CryptoWallet : CentralBank
        {
            public string WalletName { get; set; } = "Portafoglio crypto";
            public override event Feedback Notify;
            public override string CentralBankName 
            { 
                get => base.CentralBankName;
                set 
                {
                    if (base.CentralBankName != value)
                    {
                        if (Notify != null)
                        {
                            NotificationArgs msg = new ($"{this.WalletName}: Il nome della banca è cambiato da {this.CentralBankName} in {value}");
                            Notify(this, msg);
                            base.CentralBankName = value;
                        }
                    }
                } 
            }
        }
    }
}
