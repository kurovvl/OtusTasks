using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OtusTasks
{
    public static class SpaceFinder
    {
        public static IEnumerable<FileCharCnt> GetCharCountByFile(string directory, string pattern = null, char chr = ' ')
        {
            var FileNames = Directory.GetFiles(directory, pattern);
            //var result = new ConcurrentDictionary<string, int>();
            var TaskList = new List<Task<FileCharCnt>>();
            Parallel.ForEach(FileNames, fn =>
            {

                //lock(result)
                //{

                var task = Task<FileCharCnt>.Factory.StartNew(() =>
                {
                    var origTxt = File.ReadAllText(fn);
                    var replacedTxt = origTxt.Replace(chr.ToString(), "");

                    return new FileCharCnt() 
                    { 
                        Filename = fn, 
                        Count = origTxt.Length-replacedTxt.Length 
                    };
                });

                TaskList.Add(task);
            });

            Task.WaitAll(TaskList.ToArray());
            return TaskList.Select(s => s.Result);
        }

        public static ConcurrentDictionary<string, int> GetCharCountByFileWithoutTask(string directory, string pattern = null, char chr = ' ')
        {
            var FileNames = Directory.GetFiles(directory, pattern);
            var result = new ConcurrentDictionary<string, int>();
            
            foreach (var fn in FileNames)
            {
                var cnt = File.ReadAllText(fn).Count(c => c == chr);
                result.TryAdd(fn, cnt);
            }
            
            return result;
        }
    }

    public class FileCharCnt
    {
        public string Filename;
        public int Count;
    }
}
