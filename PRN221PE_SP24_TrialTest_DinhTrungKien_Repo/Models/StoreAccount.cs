using System;
using System.Collections.Generic;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models
{
    public partial class StoreAccount
    {
        public int AccountId { get; set; }
        public string AccountPassword { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? EmailAddress { get; set; }
        public int? Role { get; set; }
    }
}
