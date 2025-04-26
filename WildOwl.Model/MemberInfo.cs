using System;

namespace WildOwls.Model
{
    public class MemberInfo
    {
        public string GroupId { get; set; }

        public string MemberId { get; set; }

        public string FamilyId { get; set; }

        public string MemberName { get; set; }
        public string WifeName { get; set; }
        public string AdditionalMember { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? MarriageDate { get; set; }
        public string CoupleId  { get; set; }
        public int IsAdditional{ get; set; }
        public int AmountPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Address { get; set; }

        public int IsEdit { get; set; }
    }
}
