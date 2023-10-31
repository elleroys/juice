using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Modul
{
    public class Tank
    {
        private int capacity;
        private int currentAmount;

        public int Capacity
        {
            get => capacity;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Tank capacity must be a positive value.");
                }
                capacity = value;
            }
        }

        public int CurrentAmount
        {
            get => currentAmount;
            set
            {
                //if (value < 0)
                //{
                //    throw new ArgumentException("Current amount cannot be negative.");
                //}з
                //if (value > Capacity)
                //{
                //    throw new ArgumentException("Current amount cannot exceed the tank's capacity.");
                //}
                currentAmount = value;
            }
        }

        public string Id { get; set; }
        public JuiceType Juice { get; set; }

        public int FreeAmount => Capacity - CurrentAmount;

        public bool IsFree => CurrentAmount == 0;
        public bool IsFull => CurrentAmount >= (CriticalPercentage * Capacity) / 100;

        public static int CriticalPercentage { get; set; } = 90;
        public static int TankCount { get; set; } = 0;

        public Tank(string id, int capacity)
        {
            Id = id;
            Capacity = capacity;
            CurrentAmount = 0;
            Juice = JuiceType.Unknown;
            TankCount++;
        }

        public Tank(string id, int capacity, int currentAmount, JuiceType juice)
        {
            Id = id;
            Capacity = capacity;
            CurrentAmount = currentAmount; // Встановлюємо значення через властивість, щоб викликалася перевірка
            Juice = currentAmount > 0 ? juice : JuiceType.Unknown;
            TankCount++;
        }

        public bool AddJuice(int amount, JuiceType juiceType)
        {
            if (amount < 0 || (juiceType != Juice && !IsFree))
            {
                return false;
            }

            int newAmount = CurrentAmount + amount;

            if (newAmount > Capacity)
            {
                return false;
            }

            CurrentAmount = newAmount;
            Juice = juiceType;
            return true;
        }

        public void MakeFree()
        {
            CurrentAmount = 0;
            Juice = JuiceType.Unknown;
        }

        public string GetInfo()
        {
            return $"Id: {Id}, Capacity: {Capacity}, Current amount: {CurrentAmount}, Juice type: {Juice}";
        }

        public static bool ChangeCriticalPercentage(int newPercentage)
        {
            if (newPercentage >= 0 && newPercentage <= 100)
            {
                CriticalPercentage = newPercentage;
                return true;
            }
            return false;
        }
    }
}