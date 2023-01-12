/*using PublisherApp.Domain;

namespace PublisherApp
{
    private static List<UnvalidatedProducts> ReadListOfProductsByCategory()
    {
        List<UnvalidatedProducts> listOfProducts = new();
        // query SQL : select * from Products_Table WHERE Category = {Category}
        return listOfProducts;
    }
    public class Publisher
    {
        //var listOfGrades = ReadListOfGrades().ToArray();
        int userOption;

        // verificare daca cosul este gol

        // este gol , se roaga clientul sa aleaga categorie
        Console.WriteLine("Alegeti una din urmatoarele cateogrii.");
        Console.WriteLine("1. Tricouri\n2. Hanorace\n3. Pantaloni\n4. Camasi\n5. Geci");
        userOption = Convert.ToInt32(Console.ReadLine());

        switch (userOption) {
            case 1:
                // string category = "Tricouri";
                // in api string selectString = $"SELECT * FROM ProductsTable WHERE `Category` = {category}";
                // request to API => result
                // Console.WriteLine(result);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                Console.WriteLine("Categorie gresita.");
                break;
        }

            // nu este gol => afisam meniul de alegeri
        Console.WriteLine("1. Adaugare produs in cos\n2. Modificare cos\n3. Goleste cosul");
                userOption = Convert.ToInt32(Console.ReadLine());
            // dupa ce clientul s-a decis : preluam comanda
            Console.WriteLine("Comanda a fost preluata cu succes!");
            switch (userOption)
            {
                case 1:
                    // string category = "Tricouri";
                    // in api string selectString = $"SELECT * FROM ProductsTable WHERE `Category` = {category}";
                    // request to API => result
                    // Console.WriteLine(result);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Categorie gresita.");
                    break;
            }
        }
    }
}
*/