using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NightEdgeFramework.Core
{
    public class FileAgent
    {
        private static FileAgent fileAgent;

        // 用于临时缓存的文件夹目录
        private string sendBufferDirectory;
        private string recvBufferDirectory;

        #region Initialize

        private FileAgent()
        {

        }

        public static FileAgent GetFileAgent()
        {
            if (fileAgent == null)
            {
                fileAgent = new FileAgent();
            }

            return fileAgent;
        }

        #endregion


        #region Packup for transfer

        public void PackFiles(string targetName,string targetPath,string tempFilePath,int per_size)
        {
            NefxFilePacker packer = new NefxFilePacker { TempFilePath = tempFilePath, TargetDirectory = targetPath, UnitSize = per_size ,TargetName = targetName};
            packer.PackUp();
        }

        public void UnpackFiles(string targetName,string targetPath, string tempFilePath, int per_size)
        {
            NefxFilePacker packer = new NefxFilePacker { TempFilePath = tempFilePath, TargetDirectory = targetPath, UnitSize = per_size,TargetName=targetName };
            packer.Unpack();
        }

        #endregion

        public void ReadFile(Uri uri, ref string content)
        {
            string path = uri.ToString();
            using (FileStream fs = new FileStream(path,FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[1024];
                fs.Read(buffer, 0, 1024);
                content = Encoding.UTF8.GetString(buffer);
            }
        }

        public void WriteFile(string path,string content)
        {
            string filePath = path + @"\temp.txt";
            if (!File.Exists(filePath))
            {
                File.Create(path + @"\temp.txt");
                File.SetAttributes(filePath,
                    (new FileInfo(filePath)).Attributes | FileAttributes.Normal);
            }

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using(StreamWriter sr = new StreamWriter(fs))
                {
                    sr.WriteLine(content);
                }
                
            }
        }

    }

    public class NefxFilePacker
    {
        public string TempFilePath { get; set; }
        public string TargetDirectory { get; set; }
        public int UnitSize { get; set; }
        public string TargetName { get; set; }
        public string TargetPath { get { return this.TargetDirectory + @"\" + TargetName; } }

        // 将目标文件打包
        public void PackUp()
        {
            if (!Directory.Exists(this.TempFilePath))
                Directory.CreateDirectory(this.TempFilePath);

            using(var fsRead = new FileStream(this.TargetDirectory+ @"\" + TargetName, FileMode.Open))
            {
                long targetLength = fsRead.Length;
                int count = 0;
                //int start = 0;

                Action action = () => { 
                    using (var fsWrite = new FileStream(TempFilePath+@"\"+count.ToString()+".nefstmp", FileMode.Create))
                    {
                        byte[] buffer = new byte[this.UnitSize];
                        int len = fsRead.Read(buffer, 0, this.UnitSize);

                        fsWrite.Write(buffer, 0, len);

                        // Console.WriteLine(Encoding.ASCII.GetString(buffer) + len.ToString());
                    }
                    targetLength -= this.UnitSize;
                };

                while (targetLength > 0)
                {
                    action.Invoke();
                    count++;
                }
                    
            }
        }

        public void Unpack()
        {
            try
            {
                if (!Directory.Exists(TargetDirectory))
                    Directory.CreateDirectory(TargetDirectory);

                // File.Create 方法会创建一个文件流 FileStream 对象，在调用 Dispose 方法前目标文件会一直被占用
                // 这里使用 using 语句释放 FileStream 对象
                if (!File.Exists(this.TargetPath))
                    using(File.Create(this.TargetPath))

                        System.Console.WriteLine("创建目录成功！");

                using (var fsWrite = new FileStream(this.TargetPath, FileMode.Create))
                {
                    System.Console.WriteLine("打开文件成功！");
                    string[] paths = Directory.GetFiles(this.TempFilePath);
                    int start = 0;
                    int count = 0;

                    foreach (var item in paths)
                    {
                        using (var fsReader = new FileStream(item, FileMode.Open))
                        {
                            fsWrite.Position = start;
                            byte[] buffer = new byte[this.UnitSize];
                            int len = fsReader.Read(buffer, 0, this.UnitSize);
                            System.Console.WriteLine($"done{count}");
                            fsWrite.Write(buffer, 0, len);
                            start += UnitSize;
                            count++;
                        }                        
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        // 在完成传输后将临时文件释放空间
        public void DisposeTempFiles()
        {

        }
    }
}
