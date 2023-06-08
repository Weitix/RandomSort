using System;
using System.Collections.Generic;
using System.Threading;

public class RandomNumberGenerator
{

    public List<int> GenerateRandomNumber(int minLength, int maxLength)
    {

        var random = new Random();

        int length = random.Next(minLength, maxLength + 1);
        List<int> numbers = new List<int>();

        for (int i = 0; i < length; i++)
        {
            numbers.Add(random.Next(-100, 101));
        }

        return numbers;
    }

    public void SortNumbers(List<int> numbers, string sortAlgorithm)
    {
        Console.WriteLine("Выполняется сортировка: " + sortAlgorithm + "\n");

        Thread.Sleep(1500); 

        DateTime startTime = DateTime.Now;

        switch (sortAlgorithm)
        {
            case "Bubble":
                BubbleSort(numbers);
                break;
            case "Gnome":
                GnomeSort(numbers);
                break;
        }

        DateTime endTime = DateTime.Now;
        TimeSpan aTime = endTime - startTime;

        Console.WriteLine("Время выполнения сортировки: " + aTime.TotalMilliseconds + " мс \n");

    }

    private void BubbleSort(List<int> numbers)
    {
        int temp; 

        for(int i = 0; i < numbers.Count; i++)
        {
            for(int j = i+1; j < numbers.Count; j++)
            {
                if (numbers[j] < numbers[i])
                {
                    temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }
        }
    }

    private void GnomeSort(List<int> numbers)
    {
        int index = 0;
        int temp;

        while (index < numbers.Count)
        {
            if (index == 0 || numbers[index - 1] <= numbers[index])
            {
                index++;
            }
            else
            {
                temp = numbers[index];
                numbers[index] = numbers[index - 1];
                numbers[index - 1] = temp;
                index--;
            }
        }
    }
}