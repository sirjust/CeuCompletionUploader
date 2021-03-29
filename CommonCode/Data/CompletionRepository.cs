using CommonCode.Models;
using System.Collections.Generic;

namespace CommonCode.Data
{
    public class CompletionRepository : ICompletionRepository
    {
        public IEnumerable<Completion> Completions { get; set; }
    }
}
