using System;
using System.Collections.Generic;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models
{
    public partial class LensType
    {
        public LensType()
        {
            Eyeglasses = new HashSet<Eyeglass>();
        }

        public string LensTypeId { get; set; } = null!;
        public string LensTypeName { get; set; } = null!;
        public string LensTypeDescription { get; set; } = null!;
        public bool? IsPrescription { get; set; }

        public virtual ICollection<Eyeglass> Eyeglasses { get; set; }
    }
}
