using HomeTask3;

string rootFolder = @"C:\Users\Daniel_Tobon\Documents\NET Mentoring program\Third module";

var visitor = new FileSystemVisitor(rootFolder);

//Events
visitor.Start += (sender, path) => Console.WriteLine($"Started searching at: {path}");
visitor.Finish += (sender, path) => Console.WriteLine($"Finished searching at: {path}");
visitor.FileFound += (sender, file) => Console.WriteLine($"File found: {file}");
visitor.DirectoryFound += (sender, folder) => Console.WriteLine($"Directory found: {folder}");
visitor.FilteredFileFound += (sender, file) => Console.WriteLine($"Filtered file found: {file}");
visitor.FilteredDirectoryFound += (sender, folder) => Console.WriteLine($"Filtered directory found: {folder}");


foreach (var item in visitor.GetItems(rootFolder))
{
    Console.WriteLine(item);
}
