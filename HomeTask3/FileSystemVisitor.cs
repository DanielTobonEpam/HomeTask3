using HomeTask3;
using System;
using System.Collections.Generic;
using System.IO;

public class FileSystemVisitor
{
    public event EventHandler<string> Start;
    public event EventHandler<string> Finish;
    public event EventHandler<string> FileFound;
    public event EventHandler<string> DirectoryFound;
    public event EventHandler<string> FilteredFileFound;
    public event EventHandler<string> FilteredDirectoryFound;

    private readonly string rootPath;
    private readonly Func<string, bool> filter;
    private bool abortSearch;
    private bool excludeItems;

    public FileSystemVisitor(string rootPath, Func<string, bool> filter = null)
    {
        this.rootPath = rootPath;
        this.filter = filter ?? (path => true); // Default filter returns true for all items
    }

    public IEnumerable<string> GetItems()
    {
        Start?.Invoke(this, rootPath);
        return GetItems(rootPath);
    }

    public IEnumerable<string> GetItems(string folderPath)
    {
        if (abortSearch)
        {
            Finish?.Invoke(this, rootPath);
            yield break;
        }

        if (Directory.Exists(folderPath))
        {
            foreach (var file in Directory.GetFiles(folderPath))
            {
                FileFound?.Invoke(this, file);

                if (!excludeItems && filter(file))
                {
                    FilteredFileFound?.Invoke(this, file);
                    yield return file;
                }
            }

            foreach (var folder in Directory.GetDirectories(folderPath))
            {
                DirectoryFound?.Invoke(this, folder);

                if (!excludeItems && filter(folder))
                {
                    FilteredDirectoryFound?.Invoke(this, folder);
                    yield return folder;
                }

                foreach (var item in GetItems(folder))
                {
                    if (abortSearch)
                    {
                        Finish?.Invoke(this, rootPath);
                        yield break;
                    }

                    yield return item;
                }
            }
        }
    }

    public void AbortSearch()
    {
        abortSearch = true;
    }

    public void ExcludeItems()
    {
        excludeItems = true;
    }
}