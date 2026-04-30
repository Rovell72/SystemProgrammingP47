using System;
using System.Collections.Generic;
using System.Text;

namespace SystemProgrammingP47
{
    internal class Exam
    {
        const String SearchDirName = "files";

        public void Run()
        {
            Console.WriteLine("Екзамен: текстовий пошук у файлах");
            String searchPath = Path.Combine(Directory.GetCurrentDirectory(), SearchDirName);
            String[] fileNames = Directory.GetFiles(searchPath);
            Console.WriteLine($"Для пошуку доступно {fileNames.Length} файлів:");

            foreach (String fileName in fileNames)
            {
                Console.WriteLine(Path.GetFileName(fileName));
            }
            // ! - null-checker, аналог if (fragment == null) throw new NullReferenceException();
            Console.WriteLine("------------------------------");
            Console.Write("Введіть фрагмент для пошуку: ");
            String fragment = Console.ReadLine();
            Thread[] works = new Thread[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
            {
                int index = i;
                works[i] = new Thread(() => SearchInFile(fileNames[i], fragment));
                works[i].Start();
            }

            Console.WriteLine("Run finish. Waiting for works to finish");
            foreach (Thread t in works)
            {
                t.Join();
            }
            Console.WriteLine("all works are finished");

        }
        private void SearchInFile(String filename, String fragment)
        {
            String shortName = Path.GetFileName(filename);
            Console.WriteLine($"{shortName} start");
            String fileContent = File.ReadAllText(filename);
            List<int> positions = new List<int>();
            int index = -1;
            while (true)
            {
                index = fileContent.IndexOf(fragment, index + 1);
                if (index >= 0)
                {
                    positions.Add(index);
                }
                else
                {
                    break;
                }
            }

            if (positions.Count > 0)
            {
                String ending = positions.Count == 1 ? "і" : "ях";
                Console.WriteLine($"{shortName}: знайдено у позиці{ending}: {string.Join(", ", positions)}");
            }
            else
            {
                Console.WriteLine($"{Path.GetFileName(filename)}: не знайдено");
            }
        }


        //Console.WriteLine(fragment.Length);
    }
}