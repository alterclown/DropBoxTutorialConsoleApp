using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace DropTutorial
{
    class Program
    {
        static string token = "1k1IBVL9-bAAAAAAAAAAC6ic_5NHzK2Lh8CO7QdquYH55V1l01yH86aaZFP5kwHY";
        
        static void Main(string[] args)
        {
            var task = Task.Run((Func<Task>)Program.Run);
            task.Wait();


        }
        //static async Task Run()
        //{
        //    using (var dbx = new DropboxClient(token))
        //    {
        //        var id = await dbx.Users.GetCurrentAccountAsync();
        //        Console.WriteLine($"Name: {id.Name.DisplayName}\n Email: {id.Email}\n Country:{id.Country}");
        //        Console.ReadKey();
        //    }
        //}


        //static async Task Run()
        //{
        //    using (var dbx = new DropboxClient(token))
        //    {
        //        var list = await dbx.Files.ListFolderAsync(string.Empty);

        //        // show folders then files
        //        foreach (var item in list.Entries.Where(i => i.IsFolder))
        //        {
        //            Console.WriteLine("D  {0}/", item.Name);
        //        }

        //        foreach (var item in list.Entries.Where(i => i.IsFile))
        //        {
        //            Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
        //        }
        //    }
        //    Console.ReadKey();
        //}



        //static async Task Run()
        //{
        //    using (var dbx = new DropboxClient(token))
        //    {
        //        string folder = "";
        //        string file = "Account Type.xlsx";
        //        using (var response = await dbx.Files.DownloadAsync(folder+"/"+file)) {
        //            var s = response.GetContentAsByteArrayAsync();
        //            s.Wait();
        //            var d = s.Result;
        //            File.WriteAllBytes(file, d);
        //        }

        //    }
        //}


        static async Task Run()
        {
            using (var dbx = new DropboxClient(token))
            {
                string file = @"C:\Users\hoxro12\Desktop\Test.Docx";
                string folder = "/home/Public";
                string filename = "Test.Doc";
                string url = "";
                using (var mem = new MemoryStream(File.ReadAllBytes(file)))
                {
                    var updated = dbx.Files.UploadAsync(folder + "/" + file, WriteMode.Overwrite.Instance, body: mem);
                    updated.Wait();
                    var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + file);
                    tx.Wait();
                    url = tx.Result.Url;
                }
                Console.WriteLine(url);

            }
            Console.ReadKey();
        }
    }
}
