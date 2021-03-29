using CommonCode.Models;
using System.Collections.Generic;

namespace CommonCode.Data
{
    public interface ICompletionRepository
    {
        IEnumerable<Completion> Completions { get; set; }
    }
}
