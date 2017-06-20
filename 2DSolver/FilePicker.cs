using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DSolver
{
    public class FilePicker
    {
        public string BasePath { get; private set; }
        public int Selected { get; private set; }

        public FilePicker()
        {
            this.Selected = 0;
        }

        public FilePicker WithBasePath(string path)
        {
            this.BasePath = path;
            return this;
        }

        public List<TxtFile> GetFiles()
        {
            List<TxtFile> files = new List<TxtFile>();
            string[] filePaths = Directory.GetFiles(this.BasePath, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (string path in filePaths)
            {
                files.Add(new TxtFile().WithPath(path));
            }
            return files;
        }

        public TxtFile PickAFile()
        {
            List<TxtFile> files = this.GetFiles();
            this.Selected = new OptionSelector()
                .WithOptions(files.Select(x => x.Name).ToArray())
                .WithExplanation("Choose a file")
                .PickAnOption();
            files[this.Selected].validateFile();
            return files[this.Selected];
        }
    }
}

