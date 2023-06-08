using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    private static HttpClient httpClient = new HttpClient();
    private static RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();

    private static string apiUrl = ConfigurationManager.AppSettings["ApiUrl"];

    public static async Task Main(string[] args)
    {
        #region [ Генерация последовательности ]

        List<int> numbers = randomNumberGenerator.GenerateRandomNumber(20, 100);
        Console.WriteLine("Сгенерированная последовательность:\n");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(string.Join(" ", numbers) + "\n");
        Console.ResetColor();
        #endregion

        #region [ Сортировка последовательности ]

        string sortingAlgorithm = GetRandomSortAlgoritm();
        randomNumberGenerator.SortNumbers(numbers, sortingAlgorithm);
        Console.WriteLine("Отсортированная последовательность:\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(string.Join(" ", numbers) + "\n");
        Console.ResetColor();

        #endregion

        #region [ Отправка  данных ]

        Console.WriteLine("Отправить данные? (Y/N)\n" + 
            $"( Ваш Url: {(apiUrl != null ? apiUrl : "*В файле appsettings.json отсутствует значение ApiUrl")} )");

        bool isValidInput = false;

        do
        {
            string input = Console.ReadLine()?.Trim()?.ToUpper();
            
            switch (input)
            {
                case "Y":
                    await SendDataToApi(numbers);
                    isValidInput = true;
                    break;
                case "N":
                    Console.WriteLine("Отправка отменена\n");
                    isValidInput = true;
                    break;
                case "S":
                    Console.WriteLine("Завершение программы");
                    return; 
                default:
                    Console.WriteLine("\nНекорректный ввод. Повторите попытку\n что бы закончить (S)\n");
                    break;
            }
        } while (!isValidInput);

        #endregion
    }

    private static string GetRandomSortAlgoritm()
    {
        string[] sortingAlgorithms = { "Bubble", "Gnome" };
        int index = new Random().Next(sortingAlgorithms.Length);
        return sortingAlgorithms[index];
    }

    private static async Task SendDataToApi(List<int> numbers)
    {
        try
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(numbers), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, jsonContent);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Данные успешно отправлены");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке: {ex.Message}");
        }
    }
}