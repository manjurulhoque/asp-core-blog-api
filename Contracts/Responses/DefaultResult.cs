using System.Collections.Generic;

namespace blogapi.Contracts.Responses
{
    public class DefaultResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}