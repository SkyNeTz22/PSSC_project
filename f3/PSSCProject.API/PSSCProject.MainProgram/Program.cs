using System.Net.Http.Headers;
using PSSCProject.Data.Models;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
using LanguageExt;
using Newtonsoft.Json;
using System.Collections.Generic;
using PSSCProject.Domain.Repositories;

namespace MainProgram
{
    internal class Program
    {

        public static void ShowCartItems(List<string> cartItems, int cartValue)
        {
            int numberOfItem = 0;
            Console.WriteLine("Lista de produse din cos :");
            foreach (var item in cartItems)
            {
                Console.WriteLine($"{numberOfItem}. {item}");
                numberOfItem++;
            }
            Console.WriteLine($"Valoarea totala a cosului este de {cartValue} RON.");
        }
        public static string GetCategory()
        {
            Console.WriteLine("Va rugam, selectati categoria.");
            Console.WriteLine("1. Tricouri;\n2. Geci;\n3. Blugi.");
            string option = Console.ReadLine();
            int result = 0;
            try
            {
                result = Int32.Parse(option);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Optiunea aleasa, {option}, nu este o optiune valida. Pentru a iesi, apasati 0.");
            }

            while (true)
            {
                switch (result)
                {
                    case 0:
                        break;
                    case 1:
                        return "Tricouri";
                    case 2:
                        return "Geci";
                    case 3:
                        return "Blugi";
                    default:
                        Console.WriteLine("Categoria aleasa este gresita. Incercati din nou. Pentru a iesi, apasati 0.");
                        string resultString = Console.ReadLine();
                        if (resultString != null)
                            result = Int32.Parse(resultString);
                        break;
                }
            }
            return "Invalid";
        }

        public static (List<string>, int) AddProducts(List<string> productNames, int cartValue)
        {
            string URL = "https://localhost:7116/category";
            string categoryName = GetCategory();
            string urlParameters = "?category=" + categoryName;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("*/*"));
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string products = response.Content.ReadAsStringAsync().Result;
                dynamic productsJson = JsonConvert.DeserializeObject(products);
                int productMarker = 0;
                foreach (var indexProduct in productsJson)
                {
                    Console.WriteLine($"{productMarker}. Produsul {indexProduct["name"]} este in stoc. Numarul curent de articole este {indexProduct["stoc"]}, iar pretul este {indexProduct["price"]} de RON.");
                    productMarker++;
                }
                string productNumberString = "";
                int productNumberInt = 0;
                Console.WriteLine("Introduceti numarul produsului pe care doriti sa il adaugati.");

                bool stopWhile = false;
                while (!stopWhile)
                {
                    productNumberString = Console.ReadLine();
                    if (productNumberString != null)
                        productNumberInt = Int32.Parse(productNumberString);
                    if (productsJson[productNumberInt]["name"] != "")
                    {
                        Console.WriteLine($"Produsul {productsJson[productNumberInt]["name"]} a fost adaugat cu succes.");
                        string productName = productsJson[productNumberInt]["name"];
                        productNames.Add(productName);
                        int pret = productsJson[productNumberInt]["price"];
                        cartValue += pret;
                    }
                    else
                        stopWhile = true;
                    string listOfProductsInCart = String.Join(", ", productNames);
                    Console.WriteLine($"Produsele curente : {listOfProductsInCart}");
                    Console.WriteLine($"Valoarea totala a cosului in momentul de fata este de {cartValue} RON.");
                    Console.WriteLine("Doriti sa adaugati alte produse?\n1. Da\n2. Nu");
                    productNumberString = Console.ReadLine();
                    if (productNumberString != null)
                        productNumberInt = Int32.Parse(productNumberString);
                    if (productNumberInt == 2)
                    {
                        Console.WriteLine($"Produsele curente : {listOfProductsInCart}");
                        Console.WriteLine($"Valoarea totala a cosului in momentul de fata este de {cartValue} RON.");
                        stopWhile = true;
                    }
                    else if (productNumberInt == 1)
                        Console.WriteLine("Introduceti numarul produsului pe care doriti sa il adaugati.");
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            client.Dispose();

            return (productNames, cartValue);

        }

        public static (List<string>, int) EditCart(List<string> cartItems, int cartValue)
        {
            string urlProductName = "https://localhost:7116/productName";
            string urlParameters = "?productName=";
            bool stopWhile = false;
            while (!stopWhile)
            {
                int numberOfEditedProduct = -1;
                int productNumber = 0;
                Console.WriteLine("Lista de produse este urmatoarea: ");
                foreach (var item in cartItems)
                {
                    Console.WriteLine($"{productNumber}. {item}");
                    productNumber++;
                }
                Console.WriteLine($"Valoarea este de {cartValue} RON.");
                Console.WriteLine("Va rugam selectati produsul pe care doriti sa il editati.");
                string option = Console.ReadLine();
                int result = -1;
                if (option != null)
                {
                    try
                    {
                        result = Int32.Parse(option);
                        numberOfEditedProduct = result;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Optiunea aleasa, {option}, nu este o optiune valida. Pentru a iesi, apasati 0.");
                    }
                    Console.WriteLine("1. Doriti sa adaugati acelasi produs?\n2. Doriti sa stergeti produsul?");
                    result = -1;
                    option = "";
                    option = Console.ReadLine();
                    if (option != null)
                        result = Int32.Parse(option);

                    if (result == 1)
                    {
                        urlParameters = "?productName=";
                        urlParameters += cartItems[numberOfEditedProduct].Replace(" ", "%20");
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(urlProductName);
                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("*/*"));
                        HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string products = response.Content.ReadAsStringAsync().Result;
                            dynamic productsJson = JsonConvert.DeserializeObject(products);
                            productNumber = 0;
                            foreach (var item in cartItems.ToList())
                            {
                                if (numberOfEditedProduct == productNumber)
                                {
                                    cartItems.Add(item);
                                    foreach (var indexProduct in productsJson)
                                    {
                                        int price = indexProduct["price"];
                                        cartValue += price;
                                    }
                                }
                                productNumber++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        }
                        client.Dispose();
                        ShowCartItems(cartItems, cartValue);
                    }
                    else if (result == 2)
                    {
                        urlParameters = "?productName=";
                        urlParameters += cartItems[numberOfEditedProduct].Replace(" ", "%20");
                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri(urlProductName);
                        client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("*/*"));
                        HttpResponseMessage response = client.GetAsync(urlParameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string products = response.Content.ReadAsStringAsync().Result;
                            dynamic productsJson = JsonConvert.DeserializeObject(products);
                            foreach (var indexProduct in productsJson)
                            {
                                int price = indexProduct["price"];
                                cartValue -= price;
                            }
                            Console.WriteLine(cartValue);
                            cartItems.RemoveAt(numberOfEditedProduct);
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        }
                        client.Dispose();
                        ShowCartItems(cartItems, cartValue);
                    }
                }

                else
                    Console.WriteLine("Optiune invalida.");

                Console.WriteLine("Doriti sa continuati modificarea?\n1. Da\n2. Nu");
                result = -1;
                option = "";
                option = Console.ReadLine();
                if (option != null)
                    result = Int32.Parse(option);

                if (result == 2)
                {
                    ShowCartItems(cartItems, cartValue);
                    break;
                }
            }

            return (cartItems, cartValue);

        }
        public static int Main(string[] args)
        {
            int cartValue = 0;
            bool emptyCart = true;
            List<string> cartItems = new List<string>();
            bool stopWhile = false;

            while (!stopWhile)
            {
                int result = 0;
                string option = "1";
                if (emptyCart == false)
                {
                    Console.WriteLine("Cosul are produse in interior.");
                    Console.WriteLine("Alegeti una din urmatoarele operatiuni:\n1. Adaugati produse;\n2. Modificati produsele curente;\n3. Anulati comanda\n4. Efectuati checkout.");
                    option = Console.ReadLine();
                }
                else
                {
                    result = 1;
                }

                try
                {
                    result = Int32.Parse(option);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Optiunea aleasa, {option}, nu este o optiune valida. Pentru a iesi, apasati 0.");
                }

                switch (result)
                {
                    case 0:
                        stopWhile = true;
                        break;
                    case 1:
                        (cartItems, cartValue) = AddProducts(cartItems, cartValue);
                        emptyCart = false;
                        break;
                    case 2:
                        (cartItems, cartValue) = EditCart(cartItems, cartValue);
                        break;
                    case 3:
                        emptyCart = true;
                        cartItems = new List<string>();
                        cartValue = 0;
                        Console.WriteLine("Comanda a fost anulata cu succes. Revenim la adaugarea produselor.");
                        break;
                    case 4:
                        Console.WriteLine("Urmatoarele produse se afla in cos:");
                        foreach (var item in cartItems)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine($"Valoarea cosului este egala cu {cartValue} RON. Va multumim pentru Checkout!");
                        return 0;
                    default:
                        Console.WriteLine("Optiunea aleasa este gresita. Incercati din nou. Pentru a iesi, apasati 0.");
                        string resultString = Console.ReadLine();
                        if (resultString != null)
                            result = Int32.Parse(resultString);
                        break;
                }
            }
            return 0;
        }
    }
}