using System.Collections.Generic;

namespace CommonCode.Models
{
    public interface IUploader
    {
        void InputCompletions(IEnumerable<Completion> completions);

        void LoginToWebsite();
    }
}
