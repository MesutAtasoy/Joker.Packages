using System;

namespace Joker.Shared.Models
{
    public class AuthorizedMethodInfoInfo
    {
        public Guid Id { get; set; }
        public string ApiResourceName { get; set; }
        public string ControllerName { get; set; }
        public string Version { get; set; }
        public string ActionName { get; set; }
        public string HttpVerbName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
