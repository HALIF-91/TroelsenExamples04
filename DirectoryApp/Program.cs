using System;
using System.IO;

namespace DirectoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********** Fun with DitectoryInfo **********\n");
            ShowWindowsDirectoryInfo();
            DisplayImageFiles();
            ModifyAppDirectory();
            FunWithDirectoryType();
            Console.ReadLine();
        }
        static void ShowWindowsDirectoryInfo()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            Console.WriteLine("****** Directory Info **********");
            Console.WriteLine("Full Name: {0}", dir.FullName);
            Console.WriteLine("Name: {0}", dir.Name);
            Console.WriteLine("Parent: {0}", dir.Parent);
            Console.WriteLine("Craetion: {0}", dir.CreationTime);
            Console.WriteLine("Attributes: {0}", dir.Attributes);
            Console.WriteLine("Root: {0}", dir.Root);
            Console.WriteLine("**********************************\n");
        }
        static void DisplayImageFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\CONTENT\Oboi_full_HD_1920_1080_[torrents.ru]");

            // Получить все файлы с расширением *.jpg заглянув во все подкаталоги
            FileInfo[] imageFiles = dir.GetFiles("*.jpg", SearchOption.AllDirectories);

            // Сколько файлов найдено?
            Console.WriteLine("Found {0} *.jpg files\n", imageFiles.Length);

            // Вывести информацию о каждом файле
            foreach (FileInfo f in imageFiles)
            {
                Console.WriteLine("********************************");
                Console.WriteLine("File name: {0}", f.Name);
                Console.WriteLine("File size: {0}", f.Length);
                Console.WriteLine("Creation: {0}", f.CreationTime);
                Console.WriteLine("Attributes: {0}", f.Attributes);
                Console.WriteLine("********************************\n");
            }
        }
        static void ModifyAppDirectory()
        {
            //DirectoryInfo dir = new DirectoryInfo(@"C:\");

            // Привязаться к текущему рабочему катологу
            DirectoryInfo dir = new DirectoryInfo(".");

            // Создать \MyFolder в начальном каталоге
            dir.CreateSubdirectory("MyFolder");

            // Получить возвращенный объект DirectoryInfo
            DirectoryInfo myDataFolder = dir.CreateSubdirectory(@"MyFolder2\Data");

            // Выводить путь к ...\MyFolder2\Data
            Console.WriteLine("New Folder is: {0}", myDataFolder);
        }
        static void FunWithDirectoryType()
        {
            // Вывести список всех дисковых устройств текущего компьютера
            string[] drives = Directory.GetLogicalDrives();
            Console.WriteLine("Here are your drives:");
            foreach (string s in drives)
            {
                Console.WriteLine("--> {0} ", s);
            }

            // Удалить то что было ранее создано
            Console.WriteLine("Press Enter to delete directories");
            Console.ReadLine();

            try
            {
                Directory.Delete("MyFolder");

                // Второй парамер указывает, нужно ли удалять подкаталоги
                Directory.Delete("MyFolder2", true);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
