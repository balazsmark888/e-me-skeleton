using System;
using System.ComponentModel.DataAnnotations;

namespace e_me.Shared.DTOs.User
{
    [Serializable]
    public class UserDetailDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public Guid UserId { get; set; }

        public string BirthDay => BirthDate?.ToString("dd");

        public string BirthMonth => BirthDate?.ToString("MM");

        public string BirthYear => BirthDate?.ToString("yyyy");

        public string BirthCity { get; set; }

        public string BirthCounty { get; set; }

        public string BirthCountry { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string HomeStreet { get; set; }

        public string HomeCity { get; set; }

        public string HomeCounty { get; set; }

        public string HomeCountry { get; set; }

        public string HomeStreetNumber { get; set; }

        public string HomeEntrance { get; set; }

        public string HomeApartmentNumber { get; set; }

        public string HomeBlockNumber { get; set; }

        public string HomePostalCode { get; set; }

        [StringLength(13, MinimumLength = 13)]
        public string PersonalNumericCode { get; set; }
    }
}
