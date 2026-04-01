using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketInventoryManagementSystem
{
    class Item
    {
        public int itemId;
        public string itemName;
        public double price;

        public Item()
        {
            itemId = 0;
            itemName = string.Empty;
            price = 0;
        }
        public Item(int itemId, string itemName, double price)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.price = price;
        }
    }

    class Inventory { 
        public Dictionary<Item, int> items;
        public Inventory()
        {
            items = new Dictionary<Item, int>();
        }

        public void addItem()
        {
            Console.WriteLine("Enter Item ID : ");
            int itemId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Item Name : ");
            string itemName = Console.ReadLine();
            Console.WriteLine("Enter Item Price : ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Item Quantity : ");
            int qty = Convert.ToInt32(Console.ReadLine());

            Item newItem = new Item(itemId,itemName,price);
            items.Add(newItem, qty);
        }

        public Item getItembyId(int itemId)
        {
            var res = items.Where(item => item.Key.itemId == itemId);
            foreach (var item in res)
            {
                return item.Key;
            }
            return null;
        }
        public void displayInventory()
        {
            Console.WriteLine($"{"\n-------------Inventory Details--------------"}\n");
            Console.WriteLine($"{"Item ID",-10} {"Item",-10} {"Price",-10} {"Quantity",-10}");
            foreach (KeyValuePair<Item, int> pair in items)
            {
                Console.WriteLine($"{pair.Key.itemId,-10} {pair.Key.itemName,-10} {"Rs."+pair.Key.price,-10} {pair.Value,-10}");
            }
        }
    }

    class Bill
    {
        Dictionary<Item, int> cart;
        double totalAmount;
        public Bill()
        {
            cart = new Dictionary<Item, int>();
        }

        public void addToCart(int itemId,int qty,Inventory inventory)
        {
           
            Item item = inventory.getItembyId(itemId);
            if (item != null)
            {
                cart.Add(item, qty);
                inventory.items[item] -= qty;
            }
            else
            {
                Console.WriteLine("No items were found on the given ID!");
            }
        }

        public void generateBill()
        {
            Console.WriteLine($"{"\n\t------------BILL-------------"}\n");
            Console.WriteLine($"{"Item ID",-10} {"Item",-10} {"Price",-10} {"Quantity",-10} {"Total Price",-10}");
            foreach (KeyValuePair<Item, int> pair in cart)
            {
                Console.WriteLine($"{pair.Key.itemId,-10} {pair.Key.itemName,-10} {pair.Key.price,-10} {cart[pair.Key],-10} {(pair.Key.price * cart[pair.Key]),-10}");
                totalAmount += pair.Key.price * pair.Value;
            }

            Console.WriteLine($"Total Amount Payable : {totalAmount,-40}\n");


        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            Bill bill = new Bill();
            bool isUserExited = false;
            Console.WriteLine("-----Supermark Inventory Management System------");
            while (!isUserExited)
            {
                Console.WriteLine("\nChoose an option : ");
                Console.WriteLine("1.Add Items to Inventory");
                Console.WriteLine("2.Purchase");
                Console.WriteLine("3.Display Inventory");
                Console.WriteLine("4.Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice) {
                    case 1:
                        inventory.addItem();
                        break;
                    case 2:
                        purchase(bill,inventory);
                        break;
                    case 3:
                        inventory.displayInventory();
                        break;
                    case 4:
                        isUserExited = true;
                        break;
                }
            }

        }

        public static void purchase(Bill bill,Inventory inventory)
        {
            bool isUserExited = false;
            
            while (!isUserExited)
            {
                Console.WriteLine("\nChoose an option : ");
                Console.WriteLine("1.Add Item to cart");
                Console.WriteLine("2.Generate Bill");
                Console.WriteLine("3.Exit from Purchase");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the Item ID : ");
                        int itemID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the quantity : ");
                        int qty = Convert.ToInt32(Console.ReadLine());
                        bill.addToCart(itemID, qty, inventory);
                        break;
                    case 2:
                        bill.generateBill();
                        isUserExited=true;
                        break;
                    case 3:
                        isUserExited =true;
                        break;
                }
            }
        }
    }
}
