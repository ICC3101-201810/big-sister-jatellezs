using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace LabPOO
{
    class Program
    {
        
        public static List<Product> cart;
        public static List<Product> market;
        public static List<string> recipe;
        public delegate void BigSister(List<string> r, Product p);

        static void Main(string[] args)
        {
            cart = new List<Product>();
            market = new List<Product>();
            recipe = new List<string>();
            BigSister c1 = new BigSister(Check);
            SupplyStore();
            using (Stream stream = File.Open("data.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, cart);
            }
            using (StreamReader streamReader = new StreamReader("data.bin"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                object obj = binaryFormatter.Deserialize(streamReader.BaseStream);
            }
            while (true)
            {
                PrintHeader();
                Console.WriteLine("¿Que quieres hacer?\n");
                Console.WriteLine("\t1. Ver Receta");
                Console.WriteLine("\t2. Comprar");
                Console.WriteLine("\t3. Ver carrito");
                Console.WriteLine("\t4. Pagar");
                Console.WriteLine("\t5. Salir");
                while (true)
                {
                    String answer = Console.ReadLine();
                    if (answer == "1")
                    {
                        ShowRecipe();
                        break;
                    }
                    else if (answer == "2")
                    {
                        WalkAround();
                        c1(recipe, cart[cart.Count - 1]);
                        break;
                    }
                    else if (answer == "3")
                    {
                        PrintCart();
                        break;
                    }
                    else if (answer == "4")
                    {
                        Pay();
                        break;
                    }
                    else if (answer == "5")
                    {
                        using (Stream stream = File.Open("data.bin", FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            BinaryFormatter bin = new BinaryFormatter();
                            bin.Serialize(stream, cart);
                        }
                        Environment.Exit(1);
                    }
                }
            }
        }

        public static void Pay()
        {
            PrintHeader();
            int total = 0;
            for (int i = 0; i < cart.Count; i++)
            {
                total += cart[i].Price;
            }
            Console.WriteLine("El total de tu compra es: $" + total.ToString());
            Console.Write("Este programa se cerrará en ");
            for (int i = 5; i > 0; i--)
            {
                Console.Write(i.ToString() + " ");
                Thread.Sleep(1000);
            }
            cart.Clear();
        }

        public static void WalkAround()
        {
            PrintHeader();
            Console.WriteLine("¿Que deseas comprar?\n\n");
            for (int i = 0; i < market.Count(); i++)
            {
                PrintProduct(i, market[i]);
            }
            while (true)
            {
                try
                {
                    int answer = Convert.ToInt32(Console.ReadLine());
                    if (answer >= market.Count())
                    {
                        continue;
                    }

                    AddToCart(market[answer]);
                    break;
                }
                catch
                {
                    continue;
                }
            }           
        }

        public static void PrintCart()
        {
            PrintHeader();
            Console.WriteLine("Su carrito:\n\n");
            for (int i = 0; i < cart.Count(); i++)
            {
                PrintProduct(i, cart[i]);
            }
            Console.WriteLine("\n\nPresiona ENTER para volver al supermercado...");
            ConsoleKeyInfo response = Console.ReadKey(true);
            while (response.Key != ConsoleKey.Enter)
            {
                response = Console.ReadKey(true);
            }
        }

        public static void PrintProduct(int index, Product product)
        {
            Console.WriteLine(String.Format("{0}. {1}\n\tPrecio: ${2}\t{3}\tStock: {4}\n", index.ToString(), product.Name, product.Price, product.Unit, product.Stock));
        }

        public static void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine("\t\t LIDER\n");
        }

        public static bool AddToCart(Product product)
        {
            return product.Agregar(cart);
        }

        public static void SupplyStore()
        {
            market.Add(new Product("Leche Entera", 820, 89, "1L"));
            market.Add(new Product("Gomitas Flipy", 720, 12, "100g"));
            market.Add(new Product("Mantequilla", 850, 12, "125g"));
            market.Add(new Product("Crema para hemorroides", 4990, 7, "300cc"));
            market.Add(new Product("Pimienta", 430, 84, "15g"));
            market.Add(new Product("Vino Sauvignon Blanc Reserva Botella", 4150, 23, "750cc"));
            market.Add(new Product("Sal Lobos", 330, 150, "1kg"));
            market.Add(new Product("Cuaderno Mi Pequeño Pony", 1290, 50, "1un"));
            market.Add(new Product("Láminas de Lasaña", 1250, 85, "400g"));
            market.Add(new Product("Tomate", 1290, 200, "1kg"));
            market.Add(new Product("Harina", 890, 43, "1kg"));
            market.Add(new Product("Audifonos Samsung", 5990, 40, "1un"));
            market.Add(new Product("Pisco Alto del Carmen", 5990, 120, "1L"));
            market.Add(new Product("Carne Molida", 4390, 15, "500g"));
            market.Add(new Product("Aceite de Oliva", 1790, 77, "250g"));
            market.Add(new Product("Sal parrillera", 840, 50, "750g"));
            market.Add(new Product("Cable HDMI 1m", 3990, 25, "1un"));
            market.Add(new Product("Queso Rallado Parmesano", 499, 102, "40g"));
            market.Add(new Product("Vino Blanco Caja", 2790, 84, "2L"));
            market.Add(new Product("Malla de Cebollas", 1090, 91, "1kg"));
            market.Add(new Product("Tomates Pelados en lata", 700, 48, "540g"));
            market.Add(new Product("Queso Parmesano", 3790, 41, "200g"));
            market.Add(new Product("Bolsa de Zanahorias", 890, 74, "1un"));
            recipe.Add("Carne Molida");
            recipe.Add("Láminas de Lasaña");
            recipe.Add("Queso Rallado Parmesano");
            recipe.Add("Mantequilla");
            recipe.Add("Vino Blanco Caja");
            recipe.Add("Tomates Pelados en lata");
            recipe.Add("Bolsa de Zanahorias");
            recipe.Add("Malla de Cebollas");
            recipe.Add("Aceite de Oliva");
            recipe.Add("Sal Lobos");
            recipe.Add("Pimienta");
            recipe.Add("Harina");
            recipe.Add("Leche Entera");
        }

        public static void ShowRecipe()
        {
            Console.Clear();
            Console.WriteLine("\t\t===> Lasagne alla bolognese <===\n");
            Console.WriteLine("Ingredientes básicos:");
            Console.WriteLine("\t12 láminas de Lasaña");
            Console.WriteLine("\t70 gramos de parmesano rallado");
            Console.WriteLine("\tMantequilla\n");
            Console.WriteLine("Para el relleno:");
            Console.WriteLine("\t300 gramos de carne picada (queda más jugosa con mezcla de cerdo y ternera)");
            Console.WriteLine("\tMedio vaso de vino blanco");
            Console.WriteLine("\t250 gramos de tomate entero pelado de lata");
            Console.WriteLine("\t1 zanahoria");
            Console.WriteLine("\t1 cebolla");
            Console.WriteLine("\tAceite de oliva");
            Console.WriteLine("\tSal");
            Console.WriteLine("\tPimienta\n");
            Console.WriteLine("Para la bechamel:");
            Console.WriteLine("\t50 gramos de mantequilla");
            Console.WriteLine("\t50 gramos de harina");
            Console.WriteLine("\tMedio litro de leche");
            Console.WriteLine("\tSal");
            Console.WriteLine("\tPimienta\n\n");
            Console.WriteLine("Presiona ENTER para volver al supermercado...");
            ConsoleKeyInfo response = Console.ReadKey(true);
            while (response.Key != ConsoleKey.Enter)
            {
                response = Console.ReadKey(true);
            }
        }

        public static void Check(List<string> receta, Product pr)
        {
            foreach(string e in receta)
            {
                if(e == pr.Name)
                {
                    Console.WriteLine("Producto en la receta");
                }

                else
                {
                    cart.RemoveAt(cart.Count - 1);
                    Console.WriteLine("Producto no esta en la receta");
                    break;
                }
            }
        }
    }
}
