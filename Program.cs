using System;
using System.IO;
namespace CommentEraser
{
    
    class CommentEraser {
        private static string Path;
        private static OutcomeOptions Options;
        private static FileStream FileStream;
        public CommentEraser(string pathToFile, OutcomeOptions options)
        {
            Path = pathToFile;
            Options = options;
            FileStream = File.Open(Path,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
        }
        public void Erase(string sequenceStart) {

            StreamReader reader = new StreamReader(FileStream);
            StreamWriter writer = new StreamWriter(File.Create(new FileInfo(Path).DirectoryName +"\\"+  new Random().Next()+".txt"));
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
        }
    }
    
    enum OutcomeOptions { 
        createNew = 0,
        updateCurrent
    }
    class Program
    {
        static void Main(string[] args)
        {
            CommentEraser ver = new CommentEraser(@"C:\Users\xbox0\Desktop\tests\test1.txt",OutcomeOptions.updateCurrent);
            ver.Erase("asd");
        }
    }
}
