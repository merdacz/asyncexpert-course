using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskCompletionSourceExercises.Core
{
    public class AsyncTools
    {
        public static Task<string> RunProgramAsync(string path, string args = "")
        {
            var tcs = new TaskCompletionSource<string>();
            var process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = new ProcessStartInfo(path, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            process.Exited += (sender, eventArgs) =>
            {
                var senderProcess = sender as Process;
                if (senderProcess?.ExitCode != 0)
                {
                    var error = senderProcess?.StandardError.ReadToEnd();
                    tcs.SetException(new Exception(error));
                }
                else
                {
                    var output = senderProcess?.StandardOutput.ReadToEnd();
                    tcs.SetResult(output);
                }
                
                senderProcess?.Dispose();
            };
            process.Start();

            return tcs.Task;
        }
    }
}
