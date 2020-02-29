using System;
using System.IO;

namespace MyDirectoryWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** The Amazing File Watcher App *******\n");

            // Установить путь к каталогу, за которым нужно наблюдать
            FileSystemWatcher watcher = new FileSystemWatcher();
            try
            {
                watcher.Path = "MyFolder";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            // Указать цели наблюдения
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Следить только за текстовыми файлами
            watcher.Filter = "*.txt";

            // Добавить обработчики событий
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Начать наблюдение за каталогом
            watcher.EnableRaisingEvents = true;

            // Ожидать команды пользователя на завершение программы
            Console.WriteLine("Press 'Esc' to quit app");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            // Показать что сделано, если файл изменен, создан или удален
            Console.WriteLine("File: {0} {1}!", e.FullPath, e.ChangeType);
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            // Показать, что файл был переименован
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        }
    }
}
