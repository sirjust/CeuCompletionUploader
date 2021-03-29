using AutomateIdahoUploads.Dependency;
using CommonCode.Data;
using CommonCode.Helpers;
using CommonCode.Models;
using Ninject;
using System;

namespace AutomateIdahoUploads
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new DependencyContainer());
            var completionRepository = kernel.Get<CompletionRepository>();

            // take user input and convert to a string list
            Console.WriteLine("Please input completion data, then press Enter twice: ");
            var convertedList = DataHelper.ConvertDataToStringList();

            // convert string list to completion list which can be used by the upload task
            completionRepository.Completions = DataHelper.ListToCompletionList(convertedList);

            // send sanitized data to uploader, iterate and upload each entry
            kernel.Get<IUploader>().InputCompletions(completionRepository.Completions);

            // the log file is located in the bin/debug folder, it is called log.txt
            Console.WriteLine("Your uploads are complete. Please check the log file for any errors.");
            Console.Read();
        }
    }
}
