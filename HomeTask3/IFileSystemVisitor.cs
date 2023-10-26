using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask3
{
    public interface IFileSystemVisitor
    {
        event EventHandler<string> Start;
        event EventHandler<string> Finish;
        event EventHandler<string> FileFound;
        event EventHandler<string> DirectoryFound;
        event EventHandler<string> FilteredFileFound;
        event EventHandler<string> FilteredDirectoryFound;

        string rootPath { get; }
        Func<string, bool> Filter { get; }
        IEnumerable<string> GetItems();
        IEnumerable<string> GetItems(string folderPath);
        void AbortSearch();
        void ExcludeItems();
    }
}
