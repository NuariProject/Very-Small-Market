using System;
using System.Collections.Generic;

namespace User_Management_Service.Context
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string TokenValue { get; set; } = null!;
        public DateTime ExpiredTime { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
