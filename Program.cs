using System;
using System.IO;
using System.Threading;
namespace CommentEraser
{

    class CommentEraser {
        private static string Path;
        private static OutcomeOptions Options;
        private static FileStream FileStream;
        public CommentEraser(string pathToFile, OutcomeOptions options)
        {
           // SayHello();
            Path = pathToFile;
            Options = options;
            FileStream = File.Open(Path,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
        }
       /* private void SayHello() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
  _____                        __    ____ 
 / ______  __ _  __ _ ___ ___ / /_  / _________ _______ ____
/ /__/ _ \/  ' \/  ' / -_/ _ / __/ / _// __/ _ `(_-/ -_/ __/
\___/\___/_/_/_/_/_/_\__/_//_\__/ /___/_/  \_,_/___\__/_/");
            Thread.Sleep(80);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
  _____                        __    ____ 
 / ______  __ _  __ _ ___ ___ / /_  / _________ _______ ____
/ /__/ _ \/  ' \/  ' / -_/ _ / __/ / _// __/ _ `(_-/ -_/ __/
\___/\___/_/_/_/_/_/_\__/_//_\__/ /___/_/  \_,_/___\__/_/");
            Thread.Sleep(80);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
  _____                        __    ____ 
 / ______  __ _  __ _ ___ ___ / /_  / _________ _______ ____
/ /__/ _ \/  ' \/  ' / -_/ _ / __/ / _// __/ _ `(_-/ -_/ __/
\___/\___/_/_/_/_/_/_\__/_//_\__/ /___/_/  \_,_/___\__/_/");
            Console.ResetColor();
        }
       */
        public void Erase(string sequenceStart) {

            StreamReader reader = new StreamReader(FileStream);
            string newFileName = new FileInfo(Path).DirectoryName + "\\" + new Random().Next() + ".txt";
            StreamWriter writer = new StreamWriter(File.Create(newFileName));
            string currentString = reader.ReadLine();
            while (currentString!=null) {
                if (currentString.IndexOf(sequenceStart)==0) { }
                else if (currentString.Contains(sequenceStart))
                {
                    writer.WriteLine(currentString.Substring(0, currentString.IndexOf(sequenceStart)));
                }
                else{
                    writer.WriteLine(currentString);
                }
                currentString = reader.ReadLine();
            }

            reader.Close();
            writer.Close();
            FileStream.Close();
            writeDown(newFileName);
        }
        private void writeDown(string newFileName) {

            if (Options == OutcomeOptions.updateCurrent)
            {
                File.Delete(Path);
                File.Move(newFileName, System.IO.Path.GetFullPath(Path));
            }
            else if (Options == OutcomeOptions.createNew)
            {
                File.Move(newFileName, System.IO.Path.GetDirectoryName(Path) + System.IO.Path.DirectorySeparatorChar + new Random().Next() + System.IO.Path.GetExtension(Path));
            }
        }
        public void Erase(string startingSequence,string endingSequence) {

            StreamReader reader = new StreamReader(FileStream);
            string newFileName = new FileInfo(Path).DirectoryName + "\\" + new Random().Next() + ".txt";
            StreamWriter writer = new StreamWriter(File.Create(newFileName));
            string entireText = reader.ReadToEnd();
            if (entireText.Contains(startingSequence) && entireText.Contains(endingSequence))
            {
                while (entireText.Contains(startingSequence))
                {

                    entireText = entireText.Substring(0, entireText.IndexOf(startingSequence)) +
                        entireText.Substring(entireText.IndexOf(endingSequence) + endingSequence.Length);
                }
                writer.WriteLine(entireText);
            }
            
            else {
                Console.WriteLine("The text doesn't contain specified starting or ending sequence");

                reader.Close();
                writer.Close();
                FileStream.Close();
                File.Delete(newFileName);
                return;
            }

            reader.Close();
            writer.Close();
            FileStream.Close();
            writeDown(newFileName);
        }
    }
    enum OutcomeOptions { /*add clipboard option*/
        createNew = 0,
        updateCurrent
    }
    class Program
    {
        static void Main(string[] args)
        {
            CommentEraser ver = new CommentEraser(@"C:\Users\xbox0\Desktop\tests\test2.txt", OutcomeOptions.createNew);
            ver.Erase("hasnot","hasnot");
        }
    }
}
