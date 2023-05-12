namespace Boek.Core.Constants;

public class District
{
    public int Code { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string NameWithType { get; set; }
    public string NameWithProvince { get; set; }
    public int ParentCode { get; set; }
}

public class DistrictsList
{
    public static readonly District[] DISTRICTS = new[]
    {
        new District()
        {
            Code = 100,
            Name = "Điện Biên",
            Slug = "dien-bien",
            NameWithType = "Huyện Điện Biên",
            NameWithProvince = "Huyện Điện Biên, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 101,
            Name = "Điện Biên Đông",
            Slug = "dien-bien-dong",
            NameWithType = "Huyện Điện Biên Đông",
            NameWithProvince = "Huyện Điện Biên Đông, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 102,
            Name = "Mường Ảng",
            Slug = "muong-ang",
            NameWithType = "Huyện Mường Ảng",
            NameWithProvince = "Huyện Mường Ảng, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 103,
            Name = "Nậm Pồ",
            Slug = "nam-po",
            NameWithType = "Huyện Nậm Pồ",
            NameWithProvince = "Huyện Nậm Pồ, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 105,
            Name = "Lai Châu",
            Slug = "lai-chau",
            NameWithType = "Thành phố Lai Châu",
            NameWithProvince = "Thành phố Lai Châu, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 106,
            Name = "Tam Đường",
            Slug = "tam-duong",
            NameWithType = "Huyện Tam Đường",
            NameWithProvince = "Huyện Tam Đường, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 107,
            Name = "Mường Tè",
            Slug = "muong-te",
            NameWithType = "Huyện Mường Tè",
            NameWithProvince = "Huyện Mường Tè, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 108,
            Name = "Sìn Hồ",
            Slug = "sin-ho",
            NameWithType = "Huyện Sìn Hồ",
            NameWithProvince = "Huyện Sìn Hồ, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 109,
            Name = "Phong Thổ",
            Slug = "phong-tho",
            NameWithType = "Huyện Phong Thổ",
            NameWithProvince = "Huyện Phong Thổ, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 110,
            Name = "Than Uyên",
            Slug = "than-uyen",
            NameWithType = "Huyện Than Uyên",
            NameWithProvince = "Huyện Than Uyên, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 111,
            Name = "Tân Uyên",
            Slug = "tan-uyen",
            NameWithType = "Huyện Tân Uyên",
            NameWithProvince = "Huyện Tân Uyên, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 112,
            Name = "Nậm Nhùn",
            Slug = "nam-nhun",
            NameWithType = "Huyện Nậm Nhùn",
            NameWithProvince = "Huyện Nậm Nhùn, Tỉnh Lai Châu",
            ParentCode = 12,
        },
        new District()
        {
            Code = 116,
            Name = "Sơn La",
            Slug = "son-la",
            NameWithType = "Thành phố Sơn La",
            NameWithProvince = "Thành phố Sơn La, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 118,
            Name = "Quỳnh Nhai",
            Slug = "quynh-nhai",
            NameWithType = "Huyện Quỳnh Nhai",
            NameWithProvince = "Huyện Quỳnh Nhai, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 119,
            Name = "Thuận Châu",
            Slug = "thuan-chau",
            NameWithType = "Huyện Thuận Châu",
            NameWithProvince = "Huyện Thuận Châu, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 120,
            Name = "Mường La",
            Slug = "muong-la",
            NameWithType = "Huyện Mường La",
            NameWithProvince = "Huyện Mường La, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 121,
            Name = "Bắc Yên",
            Slug = "bac-yen",
            NameWithType = "Huyện Bắc Yên",
            NameWithProvince = "Huyện Bắc Yên, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 122,
            Name = "Phù Yên",
            Slug = "phu-yen",
            NameWithType = "Huyện Phù Yên",
            NameWithProvince = "Huyện Phù Yên, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 123,
            Name = "Mộc Châu",
            Slug = "moc-chau",
            NameWithType = "Huyện Mộc Châu",
            NameWithProvince = "Huyện Mộc Châu, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 124,
            Name = "Yên Châu",
            Slug = "yen-chau",
            NameWithType = "Huyện Yên Châu",
            NameWithProvince = "Huyện Yên Châu, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 125,
            Name = "Mai Sơn",
            Slug = "mai-son",
            NameWithType = "Huyện Mai Sơn",
            NameWithProvince = "Huyện Mai Sơn, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 126,
            Name = "Sông Mã",
            Slug = "song-ma",
            NameWithType = "Huyện Sông Mã",
            NameWithProvince = "Huyện Sông Mã, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 127,
            Name = "Sốp Cộp",
            Slug = "sop-cop",
            NameWithType = "Huyện Sốp Cộp",
            NameWithProvince = "Huyện Sốp Cộp, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 128,
            Name = "Vân Hồ",
            Slug = "van-ho",
            NameWithType = "Huyện Vân Hồ",
            NameWithProvince = "Huyện Vân Hồ, Tỉnh Sơn La",
            ParentCode = 14,
        },
        new District()
        {
            Code = 132,
            Name = "Yên Bái",
            Slug = "yen-bai",
            NameWithType = "Thành phố Yên Bái",
            NameWithProvince = "Thành phố Yên Bái, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 133,
            Name = "Nghĩa Lộ",
            Slug = "nghia-lo",
            NameWithType = "Thị xã Nghĩa Lộ",
            NameWithProvince = "Thị xã Nghĩa Lộ, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 135,
            Name = "Lục Yên",
            Slug = "luc-yen",
            NameWithType = "Huyện Lục Yên",
            NameWithProvince = "Huyện Lục Yên, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 136,
            Name = "Văn Yên",
            Slug = "van-yen",
            NameWithType = "Huyện Văn Yên",
            NameWithProvince = "Huyện Văn Yên, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 137,
            Name = "Mù Căng Chải",
            Slug = "mu-cang-chai",
            NameWithType = "Huyện Mù Căng Chải",
            NameWithProvince = "Huyện Mù Căng Chải, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 138,
            Name = "Trấn Yên",
            Slug = "tran-yen",
            NameWithType = "Huyện Trấn Yên",
            NameWithProvince = "Huyện Trấn Yên, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 139,
            Name = "Trạm Tấu",
            Slug = "tram-tau",
            NameWithType = "Huyện Trạm Tấu",
            NameWithProvince = "Huyện Trạm Tấu, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 140,
            Name = "Văn Chấn",
            Slug = "van-chan",
            NameWithType = "Huyện Văn Chấn",
            NameWithProvince = "Huyện Văn Chấn, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 141,
            Name = "Yên Bình",
            Slug = "yen-binh",
            NameWithType = "Huyện Yên Bình",
            NameWithProvince = "Huyện Yên Bình, Tỉnh Yên Bái",
            ParentCode = 15,
        },
        new District()
        {
            Code = 148,
            Name = "Hòa Bình",
            Slug = "hoa-binh",
            NameWithType = "Thành phố Hòa Bình",
            NameWithProvince = "Thành phố Hòa Bình, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 150,
            Name = "Đà Bắc",
            Slug = "da-bac",
            NameWithType = "Huyện Đà Bắc",
            NameWithProvince = "Huyện Đà Bắc, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 152,
            Name = "Lương Sơn",
            Slug = "luong-son",
            NameWithType = "Huyện Lương Sơn",
            NameWithProvince = "Huyện Lương Sơn, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 153,
            Name = "Kim Bôi",
            Slug = "kim-boi",
            NameWithType = "Huyện Kim Bôi",
            NameWithProvince = "Huyện Kim Bôi, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 154,
            Name = "Cao Phong",
            Slug = "cao-phong",
            NameWithType = "Huyện Cao Phong",
            NameWithProvince = "Huyện Cao Phong, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 155,
            Name = "Tân Lạc",
            Slug = "tan-lac",
            NameWithType = "Huyện Tân Lạc",
            NameWithProvince = "Huyện Tân Lạc, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 156,
            Name = "Mai Châu",
            Slug = "mai-chau",
            NameWithType = "Huyện Mai Châu",
            NameWithProvince = "Huyện Mai Châu, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 157,
            Name = "Lạc Sơn",
            Slug = "lac-son",
            NameWithType = "Huyện Lạc Sơn",
            NameWithProvince = "Huyện Lạc Sơn, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 158,
            Name = "Yên Thủy",
            Slug = "yen-thuy",
            NameWithType = "Huyện Yên Thủy",
            NameWithProvince = "Huyện Yên Thủy, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 159,
            Name = "Lạc Thủy",
            Slug = "lac-thuy",
            NameWithType = "Huyện Lạc Thủy",
            NameWithProvince = "Huyện Lạc Thủy, Tỉnh Hoà Bình",
            ParentCode = 17,
        },
        new District()
        {
            Code = 164,
            Name = "Thái Nguyên",
            Slug = "thai-nguyen",
            NameWithType = "Thành phố Thái Nguyên",
            NameWithProvince = "Thành phố Thái Nguyên, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 165,
            Name = "Sông Công",
            Slug = "song-cong",
            NameWithType = "Thành phố Sông Công",
            NameWithProvince = "Thành phố Sông Công, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 167,
            Name = "Định Hóa",
            Slug = "dinh-hoa",
            NameWithType = "Huyện Định Hóa",
            NameWithProvince = "Huyện Định Hóa, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 168,
            Name = "Phú Lương",
            Slug = "phu-luong",
            NameWithType = "Huyện Phú Lương",
            NameWithProvince = "Huyện Phú Lương, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 169,
            Name = "Đồng Hỷ",
            Slug = "dong-hy",
            NameWithType = "Huyện Đồng Hỷ",
            NameWithProvince = "Huyện Đồng Hỷ, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 170,
            Name = "Võ Nhai",
            Slug = "vo-nhai",
            NameWithType = "Huyện Võ Nhai",
            NameWithProvince = "Huyện Võ Nhai, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 171,
            Name = "Đại Từ",
            Slug = "dai-tu",
            NameWithType = "Huyện Đại Từ",
            NameWithProvince = "Huyện Đại Từ, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 172,
            Name = "Phổ Yên",
            Slug = "pho-yen",
            NameWithType = "Thành phố Phổ Yên",
            NameWithProvince = "Thành phố Phổ Yên, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 173,
            Name = "Phú Bình",
            Slug = "phu-binh",
            NameWithType = "Huyện Phú Bình",
            NameWithProvince = "Huyện Phú Bình, Tỉnh Thái Nguyên",
            ParentCode = 19,
        },
        new District()
        {
            Code = 178,
            Name = "Lạng Sơn",
            Slug = "lang-son",
            NameWithType = "Thành phố Lạng Sơn",
            NameWithProvince = "Thành phố Lạng Sơn, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 180,
            Name = "Tràng Định",
            Slug = "trang-dinh",
            NameWithType = "Huyện Tràng Định",
            NameWithProvince = "Huyện Tràng Định, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 181,
            Name = "Bình Gia",
            Slug = "binh-gia",
            NameWithType = "Huyện Bình Gia",
            NameWithProvince = "Huyện Bình Gia, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 182,
            Name = "Văn Lãng",
            Slug = "van-lang",
            NameWithType = "Huyện Văn Lãng",
            NameWithProvince = "Huyện Văn Lãng, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 183,
            Name = "Cao Lộc",
            Slug = "cao-loc",
            NameWithType = "Huyện Cao Lộc",
            NameWithProvince = "Huyện Cao Lộc, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 184,
            Name = "Văn Quan",
            Slug = "van-quan",
            NameWithType = "Huyện Văn Quan",
            NameWithProvince = "Huyện Văn Quan, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 185,
            Name = "Bắc Sơn",
            Slug = "bac-son",
            NameWithType = "Huyện Bắc Sơn",
            NameWithProvince = "Huyện Bắc Sơn, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 186,
            Name = "Hữu Lũng",
            Slug = "huu-lung",
            NameWithType = "Huyện Hữu Lũng",
            NameWithProvince = "Huyện Hữu Lũng, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 187,
            Name = "Chi Lăng",
            Slug = "chi-lang",
            NameWithType = "Huyện Chi Lăng",
            NameWithProvince = "Huyện Chi Lăng, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 188,
            Name = "Lộc Bình",
            Slug = "loc-binh",
            NameWithType = "Huyện Lộc Bình",
            NameWithProvince = "Huyện Lộc Bình, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 189,
            Name = "Đình Lập",
            Slug = "dinh-lap",
            NameWithType = "Huyện Đình Lập",
            NameWithProvince = "Huyện Đình Lập, Tỉnh Lạng Sơn",
            ParentCode = 20,
        },
        new District()
        {
            Code = 193,
            Name = "Hạ Long",
            Slug = "ha-long",
            NameWithType = "Thành phố Hạ Long",
            NameWithProvince = "Thành phố Hạ Long, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 194,
            Name = "Móng Cái",
            Slug = "mong-cai",
            NameWithType = "Thành phố Móng Cái",
            NameWithProvince = "Thành phố Móng Cái, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 195,
            Name = "Cẩm Phả",
            Slug = "cam-pha",
            NameWithType = "Thành phố Cẩm Phả",
            NameWithProvince = "Thành phố Cẩm Phả, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 196,
            Name = "Uông Bí",
            Slug = "uong-bi",
            NameWithType = "Thành phố Uông Bí",
            NameWithProvince = "Thành phố Uông Bí, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 198,
            Name = "Bình Liêu",
            Slug = "binh-lieu",
            NameWithType = "Huyện Bình Liêu",
            NameWithProvince = "Huyện Bình Liêu, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 199,
            Name = "Tiên Yên",
            Slug = "tien-yen",
            NameWithType = "Huyện Tiên Yên",
            NameWithProvince = "Huyện Tiên Yên, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 200,
            Name = "Đầm Hà",
            Slug = "dam-ha",
            NameWithType = "Huyện Đầm Hà",
            NameWithProvince = "Huyện Đầm Hà, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 201,
            Name = "Hải Hà",
            Slug = "hai-ha",
            NameWithType = "Huyện Hải Hà",
            NameWithProvince = "Huyện Hải Hà, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 202,
            Name = "Ba Chẽ",
            Slug = "ba-che",
            NameWithType = "Huyện Ba Chẽ",
            NameWithProvince = "Huyện Ba Chẽ, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 203,
            Name = "Vân Đồn",
            Slug = "van-don",
            NameWithType = "Huyện Vân Đồn",
            NameWithProvince = "Huyện Vân Đồn, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 205,
            Name = "Đông Triều",
            Slug = "dong-trieu",
            NameWithType = "Thị xã Đông Triều",
            NameWithProvince = "Thị xã Đông Triều, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 206,
            Name = "Quảng Yên",
            Slug = "quang-yen",
            NameWithType = "Thị xã Quảng Yên",
            NameWithProvince = "Thị xã Quảng Yên, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 207,
            Name = "Cô Tô",
            Slug = "co-to",
            NameWithType = "Huyện Cô Tô",
            NameWithProvince = "Huyện Cô Tô, Tỉnh Quảng Ninh",
            ParentCode = 22,
        },
        new District()
        {
            Code = 213,
            Name = "Bắc Giang",
            Slug = "bac-giang",
            NameWithType = "Thành phố Bắc Giang",
            NameWithProvince = "Thành phố Bắc Giang, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 215,
            Name = "Yên Thế",
            Slug = "yen-the",
            NameWithType = "Huyện Yên Thế",
            NameWithProvince = "Huyện Yên Thế, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 216,
            Name = "Tân Yên",
            Slug = "tan-yen",
            NameWithType = "Huyện Tân Yên",
            NameWithProvince = "Huyện Tân Yên, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 217,
            Name = "Lạng Giang",
            Slug = "lang-giang",
            NameWithType = "Huyện Lạng Giang",
            NameWithProvince = "Huyện Lạng Giang, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 218,
            Name = "Lục Nam",
            Slug = "luc-nam",
            NameWithType = "Huyện Lục Nam",
            NameWithProvince = "Huyện Lục Nam, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 219,
            Name = "Lục Ngạn",
            Slug = "luc-ngan",
            NameWithType = "Huyện Lục Ngạn",
            NameWithProvince = "Huyện Lục Ngạn, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 220,
            Name = "Sơn Động",
            Slug = "son-dong",
            NameWithType = "Huyện Sơn Động",
            NameWithProvince = "Huyện Sơn Động, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 221,
            Name = "Yên Dũng",
            Slug = "yen-dung",
            NameWithType = "Huyện Yên Dũng",
            NameWithProvince = "Huyện Yên Dũng, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 222,
            Name = "Việt Yên",
            Slug = "viet-yen",
            NameWithType = "Huyện Việt Yên",
            NameWithProvince = "Huyện Việt Yên, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 223,
            Name = "Hiệp Hòa",
            Slug = "hiep-hoa",
            NameWithType = "Huyện Hiệp Hòa",
            NameWithProvince = "Huyện Hiệp Hòa, Tỉnh Bắc Giang",
            ParentCode = 24,
        },
        new District()
        {
            Code = 227,
            Name = "Việt Trì",
            Slug = "viet-tri",
            NameWithType = "Thành phố Việt Trì",
            NameWithProvince = "Thành phố Việt Trì, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 228,
            Name = "Phú Thọ",
            Slug = "phu-tho",
            NameWithType = "Thị xã Phú Thọ",
            NameWithProvince = "Thị xã Phú Thọ, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 230,
            Name = "Đoan Hùng",
            Slug = "doan-hung",
            NameWithType = "Huyện Đoan Hùng",
            NameWithProvince = "Huyện Đoan Hùng, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 231,
            Name = "Hạ Hoà",
            Slug = "ha-hoa",
            NameWithType = "Huyện Hạ Hoà",
            NameWithProvince = "Huyện Hạ Hoà, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 232,
            Name = "Thanh Ba",
            Slug = "thanh-ba",
            NameWithType = "Huyện Thanh Ba",
            NameWithProvince = "Huyện Thanh Ba, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 233,
            Name = "Phù Ninh",
            Slug = "phu-ninh",
            NameWithType = "Huyện Phù Ninh",
            NameWithProvince = "Huyện Phù Ninh, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 234,
            Name = "Yên Lập",
            Slug = "yen-lap",
            NameWithType = "Huyện Yên Lập",
            NameWithProvince = "Huyện Yên Lập, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 235,
            Name = "Cẩm Khê",
            Slug = "cam-khe",
            NameWithType = "Huyện Cẩm Khê",
            NameWithProvince = "Huyện Cẩm Khê, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 236,
            Name = "Tam Nông",
            Slug = "tam-nong",
            NameWithType = "Huyện Tam Nông",
            NameWithProvince = "Huyện Tam Nông, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 237,
            Name = "Lâm Thao",
            Slug = "lam-thao",
            NameWithType = "Huyện Lâm Thao",
            NameWithProvince = "Huyện Lâm Thao, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 238,
            Name = "Thanh Sơn",
            Slug = "thanh-son",
            NameWithType = "Huyện Thanh Sơn",
            NameWithProvince = "Huyện Thanh Sơn, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 239,
            Name = "Thanh Thuỷ",
            Slug = "thanh-thuy",
            NameWithType = "Huyện Thanh Thuỷ",
            NameWithProvince = "Huyện Thanh Thuỷ, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 240,
            Name = "Tân Sơn",
            Slug = "tan-son",
            NameWithType = "Huyện Tân Sơn",
            NameWithProvince = "Huyện Tân Sơn, Tỉnh Phú Thọ",
            ParentCode = 25,
        },
        new District()
        {
            Code = 243,
            Name = "Vĩnh Yên",
            Slug = "vinh-yen",
            NameWithType = "Thành phố Vĩnh Yên",
            NameWithProvince = "Thành phố Vĩnh Yên, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 244,
            Name = "Phúc Yên",
            Slug = "phuc-yen",
            NameWithType = "Thành phố Phúc Yên",
            NameWithProvince = "Thành phố Phúc Yên, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 246,
            Name = "Lập Thạch",
            Slug = "lap-thach",
            NameWithType = "Huyện Lập Thạch",
            NameWithProvince = "Huyện Lập Thạch, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 247,
            Name = "Tam Dương",
            Slug = "tam-duong",
            NameWithType = "Huyện Tam Dương",
            NameWithProvince = "Huyện Tam Dương, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 248,
            Name = "Tam Đảo",
            Slug = "tam-dao",
            NameWithType = "Huyện Tam Đảo",
            NameWithProvince = "Huyện Tam Đảo, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 249,
            Name = "Bình Xuyên",
            Slug = "binh-xuyen",
            NameWithType = "Huyện Bình Xuyên",
            NameWithProvince = "Huyện Bình Xuyên, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 250,
            Name = "Mê Linh",
            Slug = "me-linh",
            NameWithType = "Huyện Mê Linh",
            NameWithProvince = "Huyện Mê Linh, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 251,
            Name = "Yên Lạc",
            Slug = "yen-lac",
            NameWithType = "Huyện Yên Lạc",
            NameWithProvince = "Huyện Yên Lạc, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 252,
            Name = "Vĩnh Tường",
            Slug = "vinh-tuong",
            NameWithType = "Huyện Vĩnh Tường",
            NameWithProvince = "Huyện Vĩnh Tường, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 253,
            Name = "Sông Lô",
            Slug = "song-lo",
            NameWithType = "Huyện Sông Lô",
            NameWithProvince = "Huyện Sông Lô, Tỉnh Vĩnh Phúc",
            ParentCode = 26,
        },
        new District()
        {
            Code = 256,
            Name = "Bắc Ninh",
            Slug = "bac-ninh",
            NameWithType = "Thành phố Bắc Ninh",
            NameWithProvince = "Thành phố Bắc Ninh, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 258,
            Name = "Yên Phong",
            Slug = "yen-phong",
            NameWithType = "Huyện Yên Phong",
            NameWithProvince = "Huyện Yên Phong, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 259,
            Name = "Quế Võ",
            Slug = "que-vo",
            NameWithType = "Huyện Quế Võ",
            NameWithProvince = "Huyện Quế Võ, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 260,
            Name = "Tiên Du",
            Slug = "tien-du",
            NameWithType = "Huyện Tiên Du",
            NameWithProvince = "Huyện Tiên Du, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 261,
            Name = "Từ Sơn",
            Slug = "tu-son",
            NameWithType = "Thành phố Từ Sơn",
            NameWithProvince = "Thành phố Từ Sơn, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 262,
            Name = "Thuận Thành",
            Slug = "thuan-thanh",
            NameWithType = "Huyện Thuận Thành",
            NameWithProvince = "Huyện Thuận Thành, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 263,
            Name = "Gia Bình",
            Slug = "gia-binh",
            NameWithType = "Huyện Gia Bình",
            NameWithProvince = "Huyện Gia Bình, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 264,
            Name = "Lương Tài",
            Slug = "luong-tai",
            NameWithType = "Huyện Lương Tài",
            NameWithProvince = "Huyện Lương Tài, Tỉnh Bắc Ninh",
            ParentCode = 27,
        },
        new District()
        {
            Code = 268,
            Name = "Hà Đông",
            Slug = "ha-dong",
            NameWithType = "Quận Hà Đông",
            NameWithProvince = "Quận Hà Đông, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 269,
            Name = "Sơn Tây",
            Slug = "son-tay",
            NameWithType = "Thị xã Sơn Tây",
            NameWithProvince = "Thị xã Sơn Tây, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 271,
            Name = "Ba Vì",
            Slug = "ba-vi",
            NameWithType = "Huyện Ba Vì",
            NameWithProvince = "Huyện Ba Vì, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 272,
            Name = "Phúc Thọ",
            Slug = "phuc-tho",
            NameWithType = "Huyện Phúc Thọ",
            NameWithProvince = "Huyện Phúc Thọ, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 273,
            Name = "Đan Phượng",
            Slug = "dan-phuong",
            NameWithType = "Huyện Đan Phượng",
            NameWithProvince = "Huyện Đan Phượng, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 274,
            Name = "Hoài Đức",
            Slug = "hoai-duc",
            NameWithType = "Huyện Hoài Đức",
            NameWithProvince = "Huyện Hoài Đức, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 275,
            Name = "Quốc Oai",
            Slug = "quoc-oai",
            NameWithType = "Huyện Quốc Oai",
            NameWithProvince = "Huyện Quốc Oai, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 276,
            Name = "Thạch Thất",
            Slug = "thach-that",
            NameWithType = "Huyện Thạch Thất",
            NameWithProvince = "Huyện Thạch Thất, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 277,
            Name = "Chương Mỹ",
            Slug = "chuong-my",
            NameWithType = "Huyện Chương Mỹ",
            NameWithProvince = "Huyện Chương Mỹ, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 278,
            Name = "Thanh Oai",
            Slug = "thanh-oai",
            NameWithType = "Huyện Thanh Oai",
            NameWithProvince = "Huyện Thanh Oai, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 279,
            Name = "Thường Tín",
            Slug = "thuong-tin",
            NameWithType = "Huyện Thường Tín",
            NameWithProvince = "Huyện Thường Tín, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 280,
            Name = "Phú Xuyên",
            Slug = "phu-xuyen",
            NameWithType = "Huyện Phú Xuyên",
            NameWithProvince = "Huyện Phú Xuyên, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 281,
            Name = "Ứng Hòa",
            Slug = "ung-hoa",
            NameWithType = "Huyện Ứng Hòa",
            NameWithProvince = "Huyện Ứng Hòa, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 282,
            Name = "Mỹ Đức",
            Slug = "my-duc",
            NameWithType = "Huyện Mỹ Đức",
            NameWithProvince = "Huyện Mỹ Đức, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 288,
            Name = "Hải Dương",
            Slug = "hai-duong",
            NameWithType = "Thành phố Hải Dương",
            NameWithProvince = "Thành phố Hải Dương, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 290,
            Name = "Chí Linh",
            Slug = "chi-linh",
            NameWithType = "Thành phố Chí Linh",
            NameWithProvince = "Thành phố Chí Linh, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 291,
            Name = "Nam Sách",
            Slug = "nam-sach",
            NameWithType = "Huyện Nam Sách",
            NameWithProvince = "Huyện Nam Sách, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 292,
            Name = "Kinh Môn",
            Slug = "kinh-mon",
            NameWithType = "Thị xã Kinh Môn",
            NameWithProvince = "Thị xã Kinh Môn, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 293,
            Name = "Kim Thành",
            Slug = "kim-thanh",
            NameWithType = "Huyện Kim Thành",
            NameWithProvince = "Huyện Kim Thành, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 294,
            Name = "Thanh Hà",
            Slug = "thanh-ha",
            NameWithType = "Huyện Thanh Hà",
            NameWithProvince = "Huyện Thanh Hà, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 295,
            Name = "Cẩm Giàng",
            Slug = "cam-giang",
            NameWithType = "Huyện Cẩm Giàng",
            NameWithProvince = "Huyện Cẩm Giàng, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 296,
            Name = "Bình Giang",
            Slug = "binh-giang",
            NameWithType = "Huyện Bình Giang",
            NameWithProvince = "Huyện Bình Giang, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 297,
            Name = "Gia Lộc",
            Slug = "gia-loc",
            NameWithType = "Huyện Gia Lộc",
            NameWithProvince = "Huyện Gia Lộc, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 298,
            Name = "Tứ Kỳ",
            Slug = "tu-ky",
            NameWithType = "Huyện Tứ Kỳ",
            NameWithProvince = "Huyện Tứ Kỳ, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 299,
            Name = "Ninh Giang",
            Slug = "ninh-giang",
            NameWithType = "Huyện Ninh Giang",
            NameWithProvince = "Huyện Ninh Giang, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 300,
            Name = "Thanh Miện",
            Slug = "thanh-mien",
            NameWithType = "Huyện Thanh Miện",
            NameWithProvince = "Huyện Thanh Miện, Tỉnh Hải Dương",
            ParentCode = 30,
        },
        new District()
        {
            Code = 303,
            Name = "Hồng Bàng",
            Slug = "hong-bang",
            NameWithType = "Quận Hồng Bàng",
            NameWithProvince = "Quận Hồng Bàng, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 304,
            Name = "Ngô Quyền",
            Slug = "ngo-quyen",
            NameWithType = "Quận Ngô Quyền",
            NameWithProvince = "Quận Ngô Quyền, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 305,
            Name = "Lê Chân",
            Slug = "le-chan",
            NameWithType = "Quận Lê Chân",
            NameWithProvince = "Quận Lê Chân, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 306,
            Name = "Hải An",
            Slug = "hai-an",
            NameWithType = "Quận Hải An",
            NameWithProvince = "Quận Hải An, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 307,
            Name = "Kiến An",
            Slug = "kien-an",
            NameWithType = "Quận Kiến An",
            NameWithProvince = "Quận Kiến An, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 308,
            Name = "Đồ Sơn",
            Slug = "do-son",
            NameWithType = "Quận Đồ Sơn",
            NameWithProvince = "Quận Đồ Sơn, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 309,
            Name = "Dương Kinh",
            Slug = "duong-kinh",
            NameWithType = "Quận Dương Kinh",
            NameWithProvince = "Quận Dương Kinh, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 311,
            Name = "Thuỷ Nguyên",
            Slug = "thuy-nguyen",
            NameWithType = "Huyện Thuỷ Nguyên",
            NameWithProvince = "Huyện Thuỷ Nguyên, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 312,
            Name = "An Dương",
            Slug = "an-duong",
            NameWithType = "Huyện An Dương",
            NameWithProvince = "Huyện An Dương, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 313,
            Name = "An Lão",
            Slug = "an-lao",
            NameWithType = "Huyện An Lão",
            NameWithProvince = "Huyện An Lão, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 314,
            Name = "Kiến Thuỵ",
            Slug = "kien-thuy",
            NameWithType = "Huyện Kiến Thuỵ",
            NameWithProvince = "Huyện Kiến Thuỵ, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 315,
            Name = "Tiên Lãng",
            Slug = "tien-lang",
            NameWithType = "Huyện Tiên Lãng",
            NameWithProvince = "Huyện Tiên Lãng, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 316,
            Name = "Vĩnh Bảo",
            Slug = "vinh-bao",
            NameWithType = "Huyện Vĩnh Bảo",
            NameWithProvince = "Huyện Vĩnh Bảo, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 317,
            Name = "Cát Hải",
            Slug = "cat-hai",
            NameWithType = "Huyện Cát Hải",
            NameWithProvince = "Huyện Cát Hải, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 318,
            Name = "Bạch Long Vĩ",
            Slug = "bach-long-vi",
            NameWithType = "Huyện Bạch Long Vĩ",
            NameWithProvince = "Huyện Bạch Long Vĩ, Thành phố Hải Phòng",
            ParentCode = 31,
        },
        new District()
        {
            Code = 323,
            Name = "Hưng Yên",
            Slug = "hung-yen",
            NameWithType = "Thành phố Hưng Yên",
            NameWithProvince = "Thành phố Hưng Yên, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 325,
            Name = "Văn Lâm",
            Slug = "van-lam",
            NameWithType = "Huyện Văn Lâm",
            NameWithProvince = "Huyện Văn Lâm, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 326,
            Name = "Văn Giang",
            Slug = "van-giang",
            NameWithType = "Huyện Văn Giang",
            NameWithProvince = "Huyện Văn Giang, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 327,
            Name = "Yên Mỹ",
            Slug = "yen-my",
            NameWithType = "Huyện Yên Mỹ",
            NameWithProvince = "Huyện Yên Mỹ, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 328,
            Name = "Mỹ Hào",
            Slug = "my-hao",
            NameWithType = "Thị xã Mỹ Hào",
            NameWithProvince = "Thị xã Mỹ Hào, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 329,
            Name = "Ân Thi",
            Slug = "an-thi",
            NameWithType = "Huyện Ân Thi",
            NameWithProvince = "Huyện Ân Thi, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 330,
            Name = "Khoái Châu",
            Slug = "khoai-chau",
            NameWithType = "Huyện Khoái Châu",
            NameWithProvince = "Huyện Khoái Châu, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 331,
            Name = "Kim Động",
            Slug = "kim-dong",
            NameWithType = "Huyện Kim Động",
            NameWithProvince = "Huyện Kim Động, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 332,
            Name = "Tiên Lữ",
            Slug = "tien-lu",
            NameWithType = "Huyện Tiên Lữ",
            NameWithProvince = "Huyện Tiên Lữ, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 333,
            Name = "Phù Cừ",
            Slug = "phu-cu",
            NameWithType = "Huyện Phù Cừ",
            NameWithProvince = "Huyện Phù Cừ, Tỉnh Hưng Yên",
            ParentCode = 33,
        },
        new District()
        {
            Code = 336,
            Name = "Thái Bình",
            Slug = "thai-binh",
            NameWithType = "Thành phố Thái Bình",
            NameWithProvince = "Thành phố Thái Bình, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 338,
            Name = "Quỳnh Phụ",
            Slug = "quynh-phu",
            NameWithType = "Huyện Quỳnh Phụ",
            NameWithProvince = "Huyện Quỳnh Phụ, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 339,
            Name = "Hưng Hà",
            Slug = "hung-ha",
            NameWithType = "Huyện Hưng Hà",
            NameWithProvince = "Huyện Hưng Hà, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 340,
            Name = "Đông Hưng",
            Slug = "dong-hung",
            NameWithType = "Huyện Đông Hưng",
            NameWithProvince = "Huyện Đông Hưng, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 341,
            Name = "Thái Thụy",
            Slug = "thai-thuy",
            NameWithType = "Huyện Thái Thụy",
            NameWithProvince = "Huyện Thái Thụy, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 342,
            Name = "Tiền Hải",
            Slug = "tien-hai",
            NameWithType = "Huyện Tiền Hải",
            NameWithProvince = "Huyện Tiền Hải, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 343,
            Name = "Kiến Xương",
            Slug = "kien-xuong",
            NameWithType = "Huyện Kiến Xương",
            NameWithProvince = "Huyện Kiến Xương, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 344,
            Name = "Vũ Thư",
            Slug = "vu-thu",
            NameWithType = "Huyện Vũ Thư",
            NameWithProvince = "Huyện Vũ Thư, Tỉnh Thái Bình",
            ParentCode = 34,
        },
        new District()
        {
            Code = 347,
            Name = "Phủ Lý",
            Slug = "phu-ly",
            NameWithType = "Thành phố Phủ Lý",
            NameWithProvince = "Thành phố Phủ Lý, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 349,
            Name = "Duy Tiên",
            Slug = "duy-tien",
            NameWithType = "Thị xã Duy Tiên",
            NameWithProvince = "Thị xã Duy Tiên, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 350,
            Name = "Kim Bảng",
            Slug = "kim-bang",
            NameWithType = "Huyện Kim Bảng",
            NameWithProvince = "Huyện Kim Bảng, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 351,
            Name = "Thanh Liêm",
            Slug = "thanh-liem",
            NameWithType = "Huyện Thanh Liêm",
            NameWithProvince = "Huyện Thanh Liêm, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 352,
            Name = "Bình Lục",
            Slug = "binh-luc",
            NameWithType = "Huyện Bình Lục",
            NameWithProvince = "Huyện Bình Lục, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 353,
            Name = "Lý Nhân",
            Slug = "ly-nhan",
            NameWithType = "Huyện Lý Nhân",
            NameWithProvince = "Huyện Lý Nhân, Tỉnh Hà Nam",
            ParentCode = 35,
        },
        new District()
        {
            Code = 356,
            Name = "Nam Định",
            Slug = "nam-dinh",
            NameWithType = "Thành phố Nam Định",
            NameWithProvince = "Thành phố Nam Định, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 358,
            Name = "Mỹ Lộc",
            Slug = "my-loc",
            NameWithType = "Huyện Mỹ Lộc",
            NameWithProvince = "Huyện Mỹ Lộc, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 359,
            Name = "Vụ Bản",
            Slug = "vu-ban",
            NameWithType = "Huyện Vụ Bản",
            NameWithProvince = "Huyện Vụ Bản, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 360,
            Name = "Ý Yên",
            Slug = "y-yen",
            NameWithType = "Huyện Ý Yên",
            NameWithProvince = "Huyện Ý Yên, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 361,
            Name = "Nghĩa Hưng",
            Slug = "nghia-hung",
            NameWithType = "Huyện Nghĩa Hưng",
            NameWithProvince = "Huyện Nghĩa Hưng, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 362,
            Name = "Nam Trực",
            Slug = "nam-truc",
            NameWithType = "Huyện Nam Trực",
            NameWithProvince = "Huyện Nam Trực, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 363,
            Name = "Trực Ninh",
            Slug = "truc-ninh",
            NameWithType = "Huyện Trực Ninh",
            NameWithProvince = "Huyện Trực Ninh, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 364,
            Name = "Xuân Trường",
            Slug = "xuan-truong",
            NameWithType = "Huyện Xuân Trường",
            NameWithProvince = "Huyện Xuân Trường, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 365,
            Name = "Giao Thủy",
            Slug = "giao-thuy",
            NameWithType = "Huyện Giao Thủy",
            NameWithProvince = "Huyện Giao Thủy, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 366,
            Name = "Hải Hậu",
            Slug = "hai-hau",
            NameWithType = "Huyện Hải Hậu",
            NameWithProvince = "Huyện Hải Hậu, Tỉnh Nam Định",
            ParentCode = 36,
        },
        new District()
        {
            Code = 369,
            Name = "Ninh Bình",
            Slug = "ninh-binh",
            NameWithType = "Thành phố Ninh Bình",
            NameWithProvince = "Thành phố Ninh Bình, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 370,
            Name = "Tam Điệp",
            Slug = "tam-diep",
            NameWithType = "Thành phố Tam Điệp",
            NameWithProvince = "Thành phố Tam Điệp, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 372,
            Name = "Nho Quan",
            Slug = "nho-quan",
            NameWithType = "Huyện Nho Quan",
            NameWithProvince = "Huyện Nho Quan, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 373,
            Name = "Gia Viễn",
            Slug = "gia-vien",
            NameWithType = "Huyện Gia Viễn",
            NameWithProvince = "Huyện Gia Viễn, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 374,
            Name = "Hoa Lư",
            Slug = "hoa-lu",
            NameWithType = "Huyện Hoa Lư",
            NameWithProvince = "Huyện Hoa Lư, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 375,
            Name = "Yên Khánh",
            Slug = "yen-khanh",
            NameWithType = "Huyện Yên Khánh",
            NameWithProvince = "Huyện Yên Khánh, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 376,
            Name = "Kim Sơn",
            Slug = "kim-son",
            NameWithType = "Huyện Kim Sơn",
            NameWithProvince = "Huyện Kim Sơn, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 377,
            Name = "Yên Mô",
            Slug = "yen-mo",
            NameWithType = "Huyện Yên Mô",
            NameWithProvince = "Huyện Yên Mô, Tỉnh Ninh Bình",
            ParentCode = 37,
        },
        new District()
        {
            Code = 380,
            Name = "Thanh Hóa",
            Slug = "thanh-hoa",
            NameWithType = "Thành phố Thanh Hóa",
            NameWithProvince = "Thành phố Thanh Hóa, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 381,
            Name = "Bỉm Sơn",
            Slug = "bim-son",
            NameWithType = "Thị xã Bỉm Sơn",
            NameWithProvince = "Thị xã Bỉm Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 382,
            Name = "Sầm Sơn",
            Slug = "sam-son",
            NameWithType = "Thành phố Sầm Sơn",
            NameWithProvince = "Thành phố Sầm Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 384,
            Name = "Mường Lát",
            Slug = "muong-lat",
            NameWithType = "Huyện Mường Lát",
            NameWithProvince = "Huyện Mường Lát, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 385,
            Name = "Quan Hóa",
            Slug = "quan-hoa",
            NameWithType = "Huyện Quan Hóa",
            NameWithProvince = "Huyện Quan Hóa, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 386,
            Name = "Bá Thước",
            Slug = "ba-thuoc",
            NameWithType = "Huyện Bá Thước",
            NameWithProvince = "Huyện Bá Thước, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 387,
            Name = "Quan Sơn",
            Slug = "quan-son",
            NameWithType = "Huyện Quan Sơn",
            NameWithProvince = "Huyện Quan Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 388,
            Name = "Lang Chánh",
            Slug = "lang-chanh",
            NameWithType = "Huyện Lang Chánh",
            NameWithProvince = "Huyện Lang Chánh, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 389,
            Name = "Ngọc Lặc",
            Slug = "ngoc-lac",
            NameWithType = "Huyện Ngọc Lặc",
            NameWithProvince = "Huyện Ngọc Lặc, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 390,
            Name = "Cẩm Thủy",
            Slug = "cam-thuy",
            NameWithType = "Huyện Cẩm Thủy",
            NameWithProvince = "Huyện Cẩm Thủy, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 391,
            Name = "Thạch Thành",
            Slug = "thach-thanh",
            NameWithType = "Huyện Thạch Thành",
            NameWithProvince = "Huyện Thạch Thành, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 392,
            Name = "Hà Trung",
            Slug = "ha-trung",
            NameWithType = "Huyện Hà Trung",
            NameWithProvince = "Huyện Hà Trung, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 393,
            Name = "Vĩnh Lộc",
            Slug = "vinh-loc",
            NameWithType = "Huyện Vĩnh Lộc",
            NameWithProvince = "Huyện Vĩnh Lộc, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 394,
            Name = "Yên Định",
            Slug = "yen-dinh",
            NameWithType = "Huyện Yên Định",
            NameWithProvince = "Huyện Yên Định, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 395,
            Name = "Thọ Xuân",
            Slug = "tho-xuan",
            NameWithType = "Huyện Thọ Xuân",
            NameWithProvince = "Huyện Thọ Xuân, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 396,
            Name = "Thường Xuân",
            Slug = "thuong-xuan",
            NameWithType = "Huyện Thường Xuân",
            NameWithProvince = "Huyện Thường Xuân, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 397,
            Name = "Triệu Sơn",
            Slug = "trieu-son",
            NameWithType = "Huyện Triệu Sơn",
            NameWithProvince = "Huyện Triệu Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 398,
            Name = "Thiệu Hóa",
            Slug = "thieu-hoa",
            NameWithType = "Huyện Thiệu Hóa",
            NameWithProvince = "Huyện Thiệu Hóa, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 399,
            Name = "Hoằng Hóa",
            Slug = "hoang-hoa",
            NameWithType = "Huyện Hoằng Hóa",
            NameWithProvince = "Huyện Hoằng Hóa, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 400,
            Name = "Hậu Lộc",
            Slug = "hau-loc",
            NameWithType = "Huyện Hậu Lộc",
            NameWithProvince = "Huyện Hậu Lộc, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 401,
            Name = "Nga Sơn",
            Slug = "nga-son",
            NameWithType = "Huyện Nga Sơn",
            NameWithProvince = "Huyện Nga Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 402,
            Name = "Như Xuân",
            Slug = "nhu-xuan",
            NameWithType = "Huyện Như Xuân",
            NameWithProvince = "Huyện Như Xuân, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 403,
            Name = "Như Thanh",
            Slug = "nhu-thanh",
            NameWithType = "Huyện Như Thanh",
            NameWithProvince = "Huyện Như Thanh, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 404,
            Name = "Nông Cống",
            Slug = "nong-cong",
            NameWithType = "Huyện Nông Cống",
            NameWithProvince = "Huyện Nông Cống, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 405,
            Name = "Đông Sơn",
            Slug = "dong-son",
            NameWithType = "Huyện Đông Sơn",
            NameWithProvince = "Huyện Đông Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 406,
            Name = "Quảng Xương",
            Slug = "quang-xuong",
            NameWithType = "Huyện Quảng Xương",
            NameWithProvince = "Huyện Quảng Xương, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 407,
            Name = "Nghi Sơn",
            Slug = "nghi-son",
            NameWithType = "Thị xã Nghi Sơn",
            NameWithProvince = "Thị xã Nghi Sơn, Tỉnh Thanh Hóa",
            ParentCode = 38,
        },
        new District()
        {
            Code = 412,
            Name = "Vinh",
            Slug = "vinh",
            NameWithType = "Thành phố Vinh",
            NameWithProvince = "Thành phố Vinh, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 413,
            Name = "Cửa Lò",
            Slug = "cua-lo",
            NameWithType = "Thị xã Cửa Lò",
            NameWithProvince = "Thị xã Cửa Lò, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 414,
            Name = "Thái Hoà",
            Slug = "thai-hoa",
            NameWithType = "Thị xã Thái Hoà",
            NameWithProvince = "Thị xã Thái Hoà, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 415,
            Name = "Quế Phong",
            Slug = "que-phong",
            NameWithType = "Huyện Quế Phong",
            NameWithProvince = "Huyện Quế Phong, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 416,
            Name = "Quỳ Châu",
            Slug = "quy-chau",
            NameWithType = "Huyện Quỳ Châu",
            NameWithProvince = "Huyện Quỳ Châu, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 417,
            Name = "Kỳ Sơn",
            Slug = "ky-son",
            NameWithType = "Huyện Kỳ Sơn",
            NameWithProvince = "Huyện Kỳ Sơn, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 418,
            Name = "Tương Dương",
            Slug = "tuong-duong",
            NameWithType = "Huyện Tương Dương",
            NameWithProvince = "Huyện Tương Dương, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 419,
            Name = "Nghĩa Đàn",
            Slug = "nghia-dan",
            NameWithType = "Huyện Nghĩa Đàn",
            NameWithProvince = "Huyện Nghĩa Đàn, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 420,
            Name = "Quỳ Hợp",
            Slug = "quy-hop",
            NameWithType = "Huyện Quỳ Hợp",
            NameWithProvince = "Huyện Quỳ Hợp, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 421,
            Name = "Quỳnh Lưu",
            Slug = "quynh-luu",
            NameWithType = "Huyện Quỳnh Lưu",
            NameWithProvince = "Huyện Quỳnh Lưu, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 422,
            Name = "Con Cuông",
            Slug = "con-cuong",
            NameWithType = "Huyện Con Cuông",
            NameWithProvince = "Huyện Con Cuông, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 423,
            Name = "Tân Kỳ",
            Slug = "tan-ky",
            NameWithType = "Huyện Tân Kỳ",
            NameWithProvince = "Huyện Tân Kỳ, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 424,
            Name = "Anh Sơn",
            Slug = "anh-son",
            NameWithType = "Huyện Anh Sơn",
            NameWithProvince = "Huyện Anh Sơn, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 425,
            Name = "Diễn Châu",
            Slug = "dien-chau",
            NameWithType = "Huyện Diễn Châu",
            NameWithProvince = "Huyện Diễn Châu, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 426,
            Name = "Yên Thành",
            Slug = "yen-thanh",
            NameWithType = "Huyện Yên Thành",
            NameWithProvince = "Huyện Yên Thành, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 427,
            Name = "Đô Lương",
            Slug = "do-luong",
            NameWithType = "Huyện Đô Lương",
            NameWithProvince = "Huyện Đô Lương, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 428,
            Name = "Thanh Chương",
            Slug = "thanh-chuong",
            NameWithType = "Huyện Thanh Chương",
            NameWithProvince = "Huyện Thanh Chương, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 429,
            Name = "Nghi Lộc",
            Slug = "nghi-loc",
            NameWithType = "Huyện Nghi Lộc",
            NameWithProvince = "Huyện Nghi Lộc, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 430,
            Name = "Nam Đàn",
            Slug = "nam-dan",
            NameWithType = "Huyện Nam Đàn",
            NameWithProvince = "Huyện Nam Đàn, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 431,
            Name = "Hưng Nguyên",
            Slug = "hung-nguyen",
            NameWithType = "Huyện Hưng Nguyên",
            NameWithProvince = "Huyện Hưng Nguyên, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 432,
            Name = "Hoàng Mai",
            Slug = "hoang-mai",
            NameWithType = "Thị xã Hoàng Mai",
            NameWithProvince = "Thị xã Hoàng Mai, Tỉnh Nghệ An",
            ParentCode = 40,
        },
        new District()
        {
            Code = 436,
            Name = "Hà Tĩnh",
            Slug = "ha-tinh",
            NameWithType = "Thành phố Hà Tĩnh",
            NameWithProvince = "Thành phố Hà Tĩnh, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 437,
            Name = "Hồng Lĩnh",
            Slug = "hong-linh",
            NameWithType = "Thị xã Hồng Lĩnh",
            NameWithProvince = "Thị xã Hồng Lĩnh, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 439,
            Name = "Hương Sơn",
            Slug = "huong-son",
            NameWithType = "Huyện Hương Sơn",
            NameWithProvince = "Huyện Hương Sơn, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 440,
            Name = "Đức Thọ",
            Slug = "duc-tho",
            NameWithType = "Huyện Đức Thọ",
            NameWithProvince = "Huyện Đức Thọ, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 441,
            Name = "Vũ Quang",
            Slug = "vu-quang",
            NameWithType = "Huyện Vũ Quang",
            NameWithProvince = "Huyện Vũ Quang, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 442,
            Name = "Nghi Xuân",
            Slug = "nghi-xuan",
            NameWithType = "Huyện Nghi Xuân",
            NameWithProvince = "Huyện Nghi Xuân, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 443,
            Name = "Can Lộc",
            Slug = "can-loc",
            NameWithType = "Huyện Can Lộc",
            NameWithProvince = "Huyện Can Lộc, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 444,
            Name = "Hương Khê",
            Slug = "huong-khe",
            NameWithType = "Huyện Hương Khê",
            NameWithProvince = "Huyện Hương Khê, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 445,
            Name = "Thạch Hà",
            Slug = "thach-ha",
            NameWithType = "Huyện Thạch Hà",
            NameWithProvince = "Huyện Thạch Hà, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 446,
            Name = "Cẩm Xuyên",
            Slug = "cam-xuyen",
            NameWithType = "Huyện Cẩm Xuyên",
            NameWithProvince = "Huyện Cẩm Xuyên, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 447,
            Name = "Kỳ Anh",
            Slug = "ky-anh",
            NameWithType = "Huyện Kỳ Anh",
            NameWithProvince = "Huyện Kỳ Anh, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 448,
            Name = "Lộc Hà",
            Slug = "loc-ha",
            NameWithType = "Huyện Lộc Hà",
            NameWithProvince = "Huyện Lộc Hà, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 449,
            Name = "Kỳ Anh",
            Slug = "ky-anh",
            NameWithType = "Thị xã Kỳ Anh",
            NameWithProvince = "Thị xã Kỳ Anh, Tỉnh Hà Tĩnh",
            ParentCode = 42,
        },
        new District()
        {
            Code = 450,
            Name = "Đồng Hới",
            Slug = "dong-hoi",
            NameWithType = "Thành Phố Đồng Hới",
            NameWithProvince = "Thành Phố Đồng Hới, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 452,
            Name = "Minh Hóa",
            Slug = "minh-hoa",
            NameWithType = "Huyện Minh Hóa",
            NameWithProvince = "Huyện Minh Hóa, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 453,
            Name = "Tuyên Hóa",
            Slug = "tuyen-hoa",
            NameWithType = "Huyện Tuyên Hóa",
            NameWithProvince = "Huyện Tuyên Hóa, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 454,
            Name = "Quảng Trạch",
            Slug = "quang-trach",
            NameWithType = "Huyện Quảng Trạch",
            NameWithProvince = "Huyện Quảng Trạch, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 455,
            Name = "Bố Trạch",
            Slug = "bo-trach",
            NameWithType = "Huyện Bố Trạch",
            NameWithProvince = "Huyện Bố Trạch, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 456,
            Name = "Quảng Ninh",
            Slug = "quang-ninh",
            NameWithType = "Huyện Quảng Ninh",
            NameWithProvince = "Huyện Quảng Ninh, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 457,
            Name = "Lệ Thủy",
            Slug = "le-thuy",
            NameWithType = "Huyện Lệ Thủy",
            NameWithProvince = "Huyện Lệ Thủy, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 458,
            Name = "Ba Đồn",
            Slug = "ba-don",
            NameWithType = "Thị xã Ba Đồn",
            NameWithProvince = "Thị xã Ba Đồn, Tỉnh Quảng Bình",
            ParentCode = 44,
        },
        new District()
        {
            Code = 461,
            Name = "Đông Hà",
            Slug = "dong-ha",
            NameWithType = "Thành phố Đông Hà",
            NameWithProvince = "Thành phố Đông Hà, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 462,
            Name = "Quảng Trị",
            Slug = "quang-tri",
            NameWithType = "Thị xã Quảng Trị",
            NameWithProvince = "Thị xã Quảng Trị, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 464,
            Name = "Vĩnh Linh",
            Slug = "vinh-linh",
            NameWithType = "Huyện Vĩnh Linh",
            NameWithProvince = "Huyện Vĩnh Linh, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 465,
            Name = "Hướng Hóa",
            Slug = "huong-hoa",
            NameWithType = "Huyện Hướng Hóa",
            NameWithProvince = "Huyện Hướng Hóa, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 466,
            Name = "Gio Linh",
            Slug = "gio-linh",
            NameWithType = "Huyện Gio Linh",
            NameWithProvince = "Huyện Gio Linh, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 467,
            Name = "Đa Krông",
            Slug = "da-krong",
            NameWithType = "Huyện Đa Krông",
            NameWithProvince = "Huyện Đa Krông, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 468,
            Name = "Cam Lộ",
            Slug = "cam-lo",
            NameWithType = "Huyện Cam Lộ",
            NameWithProvince = "Huyện Cam Lộ, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 469,
            Name = "Triệu Phong",
            Slug = "trieu-phong",
            NameWithType = "Huyện Triệu Phong",
            NameWithProvince = "Huyện Triệu Phong, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 470,
            Name = "Hải Lăng",
            Slug = "hai-lang",
            NameWithType = "Huyện Hải Lăng",
            NameWithProvince = "Huyện Hải Lăng, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 471,
            Name = "Cồn Cỏ",
            Slug = "con-co",
            NameWithType = "Huyện Cồn Cỏ",
            NameWithProvince = "Huyện Cồn Cỏ, Tỉnh Quảng Trị",
            ParentCode = 45,
        },
        new District()
        {
            Code = 474,
            Name = "Huế",
            Slug = "hue",
            NameWithType = "Thành phố Huế",
            NameWithProvince = "Thành phố Huế, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 476,
            Name = "Phong Điền",
            Slug = "phong-dien",
            NameWithType = "Huyện Phong Điền",
            NameWithProvince = "Huyện Phong Điền, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 477,
            Name = "Quảng Điền",
            Slug = "quang-dien",
            NameWithType = "Huyện Quảng Điền",
            NameWithProvince = "Huyện Quảng Điền, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 478,
            Name = "Phú Vang",
            Slug = "phu-vang",
            NameWithType = "Huyện Phú Vang",
            NameWithProvince = "Huyện Phú Vang, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 479,
            Name = "Hương Thủy",
            Slug = "huong-thuy",
            NameWithType = "Thị xã Hương Thủy",
            NameWithProvince = "Thị xã Hương Thủy, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 480,
            Name = "Hương Trà",
            Slug = "huong-tra",
            NameWithType = "Thị xã Hương Trà",
            NameWithProvince = "Thị xã Hương Trà, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 481,
            Name = "A Lưới",
            Slug = "a-luoi",
            NameWithType = "Huyện A Lưới",
            NameWithProvince = "Huyện A Lưới, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 482,
            Name = "Phú Lộc",
            Slug = "phu-loc",
            NameWithType = "Huyện Phú Lộc",
            NameWithProvince = "Huyện Phú Lộc, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 483,
            Name = "Nam Đông",
            Slug = "nam-dong",
            NameWithType = "Huyện Nam Đông",
            NameWithProvince = "Huyện Nam Đông, Tỉnh Thừa Thiên Huế",
            ParentCode = 46,
        },
        new District()
        {
            Code = 490,
            Name = "Liên Chiểu",
            Slug = "lien-chieu",
            NameWithType = "Quận Liên Chiểu",
            NameWithProvince = "Quận Liên Chiểu, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 491,
            Name = "Thanh Khê",
            Slug = "thanh-khe",
            NameWithType = "Quận Thanh Khê",
            NameWithProvince = "Quận Thanh Khê, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 492,
            Name = "Hải Châu",
            Slug = "hai-chau",
            NameWithType = "Quận Hải Châu",
            NameWithProvince = "Quận Hải Châu, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 493,
            Name = "Sơn Trà",
            Slug = "son-tra",
            NameWithType = "Quận Sơn Trà",
            NameWithProvince = "Quận Sơn Trà, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 494,
            Name = "Ngũ Hành Sơn",
            Slug = "ngu-hanh-son",
            NameWithType = "Quận Ngũ Hành Sơn",
            NameWithProvince = "Quận Ngũ Hành Sơn, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 495,
            Name = "Cẩm Lệ",
            Slug = "cam-le",
            NameWithType = "Quận Cẩm Lệ",
            NameWithProvince = "Quận Cẩm Lệ, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 497,
            Name = "Hòa Vang",
            Slug = "hoa-vang",
            NameWithType = "Huyện Hòa Vang",
            NameWithProvince = "Huyện Hòa Vang, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 498,
            Name = "Hoàng Sa",
            Slug = "hoang-sa",
            NameWithType = "Huyện Hoàng Sa",
            NameWithProvince = "Huyện Hoàng Sa, Thành phố Đà Nẵng",
            ParentCode = 48,
        },
        new District()
        {
            Code = 502,
            Name = "Tam Kỳ",
            Slug = "tam-ky",
            NameWithType = "Thành phố Tam Kỳ",
            NameWithProvince = "Thành phố Tam Kỳ, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 503,
            Name = "Hội An",
            Slug = "hoi-an",
            NameWithType = "Thành phố Hội An",
            NameWithProvince = "Thành phố Hội An, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 504,
            Name = "Tây Giang",
            Slug = "tay-giang",
            NameWithType = "Huyện Tây Giang",
            NameWithProvince = "Huyện Tây Giang, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 505,
            Name = "Đông Giang",
            Slug = "dong-giang",
            NameWithType = "Huyện Đông Giang",
            NameWithProvince = "Huyện Đông Giang, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 506,
            Name = "Đại Lộc",
            Slug = "dai-loc",
            NameWithType = "Huyện Đại Lộc",
            NameWithProvince = "Huyện Đại Lộc, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 507,
            Name = "Điện Bàn",
            Slug = "dien-ban",
            NameWithType = "Thị xã Điện Bàn",
            NameWithProvince = "Thị xã Điện Bàn, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 508,
            Name = "Duy Xuyên",
            Slug = "duy-xuyen",
            NameWithType = "Huyện Duy Xuyên",
            NameWithProvince = "Huyện Duy Xuyên, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 509,
            Name = "Quế Sơn",
            Slug = "que-son",
            NameWithType = "Huyện Quế Sơn",
            NameWithProvince = "Huyện Quế Sơn, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 510,
            Name = "Nam Giang",
            Slug = "nam-giang",
            NameWithType = "Huyện Nam Giang",
            NameWithProvince = "Huyện Nam Giang, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 511,
            Name = "Phước Sơn",
            Slug = "phuoc-son",
            NameWithType = "Huyện Phước Sơn",
            NameWithProvince = "Huyện Phước Sơn, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 512,
            Name = "Hiệp Đức",
            Slug = "hiep-duc",
            NameWithType = "Huyện Hiệp Đức",
            NameWithProvince = "Huyện Hiệp Đức, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 513,
            Name = "Thăng Bình",
            Slug = "thang-binh",
            NameWithType = "Huyện Thăng Bình",
            NameWithProvince = "Huyện Thăng Bình, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 514,
            Name = "Tiên Phước",
            Slug = "tien-phuoc",
            NameWithType = "Huyện Tiên Phước",
            NameWithProvince = "Huyện Tiên Phước, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 515,
            Name = "Bắc Trà My",
            Slug = "bac-tra-my",
            NameWithType = "Huyện Bắc Trà My",
            NameWithProvince = "Huyện Bắc Trà My, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 516,
            Name = "Nam Trà My",
            Slug = "nam-tra-my",
            NameWithType = "Huyện Nam Trà My",
            NameWithProvince = "Huyện Nam Trà My, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 517,
            Name = "Núi Thành",
            Slug = "nui-thanh",
            NameWithType = "Huyện Núi Thành",
            NameWithProvince = "Huyện Núi Thành, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 518,
            Name = "Phú Ninh",
            Slug = "phu-ninh",
            NameWithType = "Huyện Phú Ninh",
            NameWithProvince = "Huyện Phú Ninh, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 519,
            Name = "Nông Sơn",
            Slug = "nong-son",
            NameWithType = "Huyện Nông Sơn",
            NameWithProvince = "Huyện Nông Sơn, Tỉnh Quảng Nam",
            ParentCode = 49,
        },
        new District()
        {
            Code = 522,
            Name = "Quảng Ngãi",
            Slug = "quang-ngai",
            NameWithType = "Thành phố Quảng Ngãi",
            NameWithProvince = "Thành phố Quảng Ngãi, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 524,
            Name = "Bình Sơn",
            Slug = "binh-son",
            NameWithType = "Huyện Bình Sơn",
            NameWithProvince = "Huyện Bình Sơn, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 525,
            Name = "Trà Bồng",
            Slug = "tra-bong",
            NameWithType = "Huyện Trà Bồng",
            NameWithProvince = "Huyện Trà Bồng, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 527,
            Name = "Sơn Tịnh",
            Slug = "son-tinh",
            NameWithType = "Huyện Sơn Tịnh",
            NameWithProvince = "Huyện Sơn Tịnh, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 528,
            Name = "Tư Nghĩa",
            Slug = "tu-nghia",
            NameWithType = "Huyện Tư Nghĩa",
            NameWithProvince = "Huyện Tư Nghĩa, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 529,
            Name = "Sơn Hà",
            Slug = "son-ha",
            NameWithType = "Huyện Sơn Hà",
            NameWithProvince = "Huyện Sơn Hà, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 530,
            Name = "Sơn Tây",
            Slug = "son-tay",
            NameWithType = "Huyện Sơn Tây",
            NameWithProvince = "Huyện Sơn Tây, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 531,
            Name = "Minh Long",
            Slug = "minh-long",
            NameWithType = "Huyện Minh Long",
            NameWithProvince = "Huyện Minh Long, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 532,
            Name = "Nghĩa Hành",
            Slug = "nghia-hanh",
            NameWithType = "Huyện Nghĩa Hành",
            NameWithProvince = "Huyện Nghĩa Hành, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 533,
            Name = "Mộ Đức",
            Slug = "mo-duc",
            NameWithType = "Huyện Mộ Đức",
            NameWithProvince = "Huyện Mộ Đức, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 534,
            Name = "Đức Phổ",
            Slug = "duc-pho",
            NameWithType = "Thị xã Đức Phổ",
            NameWithProvince = "Thị xã Đức Phổ, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 535,
            Name = "Ba Tơ",
            Slug = "ba-to",
            NameWithType = "Huyện Ba Tơ",
            NameWithProvince = "Huyện Ba Tơ, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 536,
            Name = "Lý Sơn",
            Slug = "ly-son",
            NameWithType = "Huyện Lý Sơn",
            NameWithProvince = "Huyện Lý Sơn, Tỉnh Quảng Ngãi",
            ParentCode = 51,
        },
        new District()
        {
            Code = 540,
            Name = "Quy Nhơn",
            Slug = "quy-nhon",
            NameWithType = "Thành phố Quy Nhơn",
            NameWithProvince = "Thành phố Quy Nhơn, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 542,
            Name = "An Lão",
            Slug = "an-lao",
            NameWithType = "Huyện An Lão",
            NameWithProvince = "Huyện An Lão, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 543,
            Name = "Hoài Nhơn",
            Slug = "hoai-nhon",
            NameWithType = "Thị xã Hoài Nhơn",
            NameWithProvince = "Thị xã Hoài Nhơn, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 544,
            Name = "Hoài Ân",
            Slug = "hoai-an",
            NameWithType = "Huyện Hoài Ân",
            NameWithProvince = "Huyện Hoài Ân, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 545,
            Name = "Phù Mỹ",
            Slug = "phu-my",
            NameWithType = "Huyện Phù Mỹ",
            NameWithProvince = "Huyện Phù Mỹ, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 546,
            Name = "Vĩnh Thạnh",
            Slug = "vinh-thanh",
            NameWithType = "Huyện Vĩnh Thạnh",
            NameWithProvince = "Huyện Vĩnh Thạnh, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 547,
            Name = "Tây Sơn",
            Slug = "tay-son",
            NameWithType = "Huyện Tây Sơn",
            NameWithProvince = "Huyện Tây Sơn, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 548,
            Name = "Phù Cát",
            Slug = "phu-cat",
            NameWithType = "Huyện Phù Cát",
            NameWithProvince = "Huyện Phù Cát, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 549,
            Name = "An Nhơn",
            Slug = "an-nhon",
            NameWithType = "Thị xã An Nhơn",
            NameWithProvince = "Thị xã An Nhơn, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 550,
            Name = "Tuy Phước",
            Slug = "tuy-phuoc",
            NameWithType = "Huyện Tuy Phước",
            NameWithProvince = "Huyện Tuy Phước, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 551,
            Name = "Vân Canh",
            Slug = "van-canh",
            NameWithType = "Huyện Vân Canh",
            NameWithProvince = "Huyện Vân Canh, Tỉnh Bình Định",
            ParentCode = 52,
        },
        new District()
        {
            Code = 555,
            Name = "Tuy Hoà",
            Slug = "tuy-hoa",
            NameWithType = "Thành phố Tuy Hoà",
            NameWithProvince = "Thành phố Tuy Hoà, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 557,
            Name = "Sông Cầu",
            Slug = "song-cau",
            NameWithType = "Thị xã Sông Cầu",
            NameWithProvince = "Thị xã Sông Cầu, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 558,
            Name = "Đồng Xuân",
            Slug = "dong-xuan",
            NameWithType = "Huyện Đồng Xuân",
            NameWithProvince = "Huyện Đồng Xuân, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 559,
            Name = "Tuy An",
            Slug = "tuy-an",
            NameWithType = "Huyện Tuy An",
            NameWithProvince = "Huyện Tuy An, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 560,
            Name = "Sơn Hòa",
            Slug = "son-hoa",
            NameWithType = "Huyện Sơn Hòa",
            NameWithProvince = "Huyện Sơn Hòa, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 561,
            Name = "Sông Hinh",
            Slug = "song-hinh",
            NameWithType = "Huyện Sông Hinh",
            NameWithProvince = "Huyện Sông Hinh, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 562,
            Name = "Tây Hoà",
            Slug = "tay-hoa",
            NameWithType = "Huyện Tây Hoà",
            NameWithProvince = "Huyện Tây Hoà, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 563,
            Name = "Phú Hoà",
            Slug = "phu-hoa",
            NameWithType = "Huyện Phú Hoà",
            NameWithProvince = "Huyện Phú Hoà, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 564,
            Name = "Đông Hòa",
            Slug = "dong-hoa",
            NameWithType = "Thị xã Đông Hòa",
            NameWithProvince = "Thị xã Đông Hòa, Tỉnh Phú Yên",
            ParentCode = 54,
        },
        new District()
        {
            Code = 568,
            Name = "Nha Trang",
            Slug = "nha-trang",
            NameWithType = "Thành phố Nha Trang",
            NameWithProvince = "Thành phố Nha Trang, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 569,
            Name = "Cam Ranh",
            Slug = "cam-ranh",
            NameWithType = "Thành phố Cam Ranh",
            NameWithProvince = "Thành phố Cam Ranh, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 570,
            Name = "Cam Lâm",
            Slug = "cam-lam",
            NameWithType = "Huyện Cam Lâm",
            NameWithProvince = "Huyện Cam Lâm, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 571,
            Name = "Vạn Ninh",
            Slug = "van-ninh",
            NameWithType = "Huyện Vạn Ninh",
            NameWithProvince = "Huyện Vạn Ninh, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 572,
            Name = "Ninh Hòa",
            Slug = "ninh-hoa",
            NameWithType = "Thị xã Ninh Hòa",
            NameWithProvince = "Thị xã Ninh Hòa, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 573,
            Name = "Khánh Vĩnh",
            Slug = "khanh-vinh",
            NameWithType = "Huyện Khánh Vĩnh",
            NameWithProvince = "Huyện Khánh Vĩnh, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 574,
            Name = "Diên Khánh",
            Slug = "dien-khanh",
            NameWithType = "Huyện Diên Khánh",
            NameWithProvince = "Huyện Diên Khánh, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 575,
            Name = "Khánh Sơn",
            Slug = "khanh-son",
            NameWithType = "Huyện Khánh Sơn",
            NameWithProvince = "Huyện Khánh Sơn, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 576,
            Name = "Trường Sa",
            Slug = "truong-sa",
            NameWithType = "Huyện Trường Sa",
            NameWithProvince = "Huyện Trường Sa, Tỉnh Khánh Hòa",
            ParentCode = 56,
        },
        new District()
        {
            Code = 582,
            Name = "Phan Rang-Tháp Chàm",
            Slug = "phan-rang-thap-cham",
            NameWithType = "Thành phố Phan Rang-Tháp Chàm",
            NameWithProvince = "Thành phố Phan Rang-Tháp Chàm, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 584,
            Name = "Bác Ái",
            Slug = "bac-ai",
            NameWithType = "Huyện Bác Ái",
            NameWithProvince = "Huyện Bác Ái, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 585,
            Name = "Ninh Sơn",
            Slug = "ninh-son",
            NameWithType = "Huyện Ninh Sơn",
            NameWithProvince = "Huyện Ninh Sơn, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 586,
            Name = "Ninh Hải",
            Slug = "ninh-hai",
            NameWithType = "Huyện Ninh Hải",
            NameWithProvince = "Huyện Ninh Hải, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 587,
            Name = "Ninh Phước",
            Slug = "ninh-phuoc",
            NameWithType = "Huyện Ninh Phước",
            NameWithProvince = "Huyện Ninh Phước, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 588,
            Name = "Thuận Bắc",
            Slug = "thuan-bac",
            NameWithType = "Huyện Thuận Bắc",
            NameWithProvince = "Huyện Thuận Bắc, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 589,
            Name = "Thuận Nam",
            Slug = "thuan-nam",
            NameWithType = "Huyện Thuận Nam",
            NameWithProvince = "Huyện Thuận Nam, Tỉnh Ninh Thuận",
            ParentCode = 58,
        },
        new District()
        {
            Code = 593,
            Name = "Phan Thiết",
            Slug = "phan-thiet",
            NameWithType = "Thành phố Phan Thiết",
            NameWithProvince = "Thành phố Phan Thiết, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 594,
            Name = "La Gi",
            Slug = "la-gi",
            NameWithType = "Thị xã La Gi",
            NameWithProvince = "Thị xã La Gi, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 595,
            Name = "Tuy Phong",
            Slug = "tuy-phong",
            NameWithType = "Huyện Tuy Phong",
            NameWithProvince = "Huyện Tuy Phong, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 596,
            Name = "Bắc Bình",
            Slug = "bac-binh",
            NameWithType = "Huyện Bắc Bình",
            NameWithProvince = "Huyện Bắc Bình, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 597,
            Name = "Hàm Thuận Bắc",
            Slug = "ham-thuan-bac",
            NameWithType = "Huyện Hàm Thuận Bắc",
            NameWithProvince = "Huyện Hàm Thuận Bắc, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 598,
            Name = "Hàm Thuận Nam",
            Slug = "ham-thuan-nam",
            NameWithType = "Huyện Hàm Thuận Nam",
            NameWithProvince = "Huyện Hàm Thuận Nam, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 599,
            Name = "Tánh Linh",
            Slug = "tanh-linh",
            NameWithType = "Huyện Tánh Linh",
            NameWithProvince = "Huyện Tánh Linh, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 600,
            Name = "Đức Linh",
            Slug = "duc-linh",
            NameWithType = "Huyện Đức Linh",
            NameWithProvince = "Huyện Đức Linh, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 601,
            Name = "Hàm Tân",
            Slug = "ham-tan",
            NameWithType = "Huyện Hàm Tân",
            NameWithProvince = "Huyện Hàm Tân, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 602,
            Name = "Phú Quí",
            Slug = "phu-qui",
            NameWithType = "Huyện Phú Quí",
            NameWithProvince = "Huyện Phú Quí, Tỉnh Bình Thuận",
            ParentCode = 60,
        },
        new District()
        {
            Code = 608,
            Name = "Kon Tum",
            Slug = "kon-tum",
            NameWithType = "Thành phố Kon Tum",
            NameWithProvince = "Thành phố Kon Tum, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 610,
            Name = "Đắk Glei",
            Slug = "dak-glei",
            NameWithType = "Huyện Đắk Glei",
            NameWithProvince = "Huyện Đắk Glei, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 611,
            Name = "Ngọc Hồi",
            Slug = "ngoc-hoi",
            NameWithType = "Huyện Ngọc Hồi",
            NameWithProvince = "Huyện Ngọc Hồi, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 612,
            Name = "Đắk Tô",
            Slug = "dak-to",
            NameWithType = "Huyện Đắk Tô",
            NameWithProvince = "Huyện Đắk Tô, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 613,
            Name = "Kon Plông",
            Slug = "kon-plong",
            NameWithType = "Huyện Kon Plông",
            NameWithProvince = "Huyện Kon Plông, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 614,
            Name = "Kon Rẫy",
            Slug = "kon-ray",
            NameWithType = "Huyện Kon Rẫy",
            NameWithProvince = "Huyện Kon Rẫy, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 615,
            Name = "Đắk Hà",
            Slug = "dak-ha",
            NameWithType = "Huyện Đắk Hà",
            NameWithProvince = "Huyện Đắk Hà, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 616,
            Name = "Sa Thầy",
            Slug = "sa-thay",
            NameWithType = "Huyện Sa Thầy",
            NameWithProvince = "Huyện Sa Thầy, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 617,
            Name = "Tu Mơ Rông",
            Slug = "tu-mo-rong",
            NameWithType = "Huyện Tu Mơ Rông",
            NameWithProvince = "Huyện Tu Mơ Rông, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 618,
            Name = "Ia H' Drai",
            Slug = "ia-h-drai",
            NameWithType = "Huyện Ia H' Drai",
            NameWithProvince = "Huyện Ia H' Drai, Tỉnh Kon Tum",
            ParentCode = 62,
        },
        new District()
        {
            Code = 622,
            Name = "Pleiku",
            Slug = "pleiku",
            NameWithType = "Thành phố Pleiku",
            NameWithProvince = "Thành phố Pleiku, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 623,
            Name = "An Khê",
            Slug = "an-khe",
            NameWithType = "Thị xã An Khê",
            NameWithProvince = "Thị xã An Khê, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 624,
            Name = "Ayun Pa",
            Slug = "ayun-pa",
            NameWithType = "Thị xã Ayun Pa",
            NameWithProvince = "Thị xã Ayun Pa, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 625,
            Name = "KBang",
            Slug = "kbang",
            NameWithType = "Huyện KBang",
            NameWithProvince = "Huyện KBang, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 626,
            Name = "Đăk Đoa",
            Slug = "dak-doa",
            NameWithType = "Huyện Đăk Đoa",
            NameWithProvince = "Huyện Đăk Đoa, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 627,
            Name = "Chư Păh",
            Slug = "chu-pah",
            NameWithType = "Huyện Chư Păh",
            NameWithProvince = "Huyện Chư Păh, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 628,
            Name = "Ia Grai",
            Slug = "ia-grai",
            NameWithType = "Huyện Ia Grai",
            NameWithProvince = "Huyện Ia Grai, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 629,
            Name = "Mang Yang",
            Slug = "mang-yang",
            NameWithType = "Huyện Mang Yang",
            NameWithProvince = "Huyện Mang Yang, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 630,
            Name = "Kông Chro",
            Slug = "kong-chro",
            NameWithType = "Huyện Kông Chro",
            NameWithProvince = "Huyện Kông Chro, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 631,
            Name = "Đức Cơ",
            Slug = "duc-co",
            NameWithType = "Huyện Đức Cơ",
            NameWithProvince = "Huyện Đức Cơ, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 632,
            Name = "Chư Prông",
            Slug = "chu-prong",
            NameWithType = "Huyện Chư Prông",
            NameWithProvince = "Huyện Chư Prông, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 633,
            Name = "Chư Sê",
            Slug = "chu-se",
            NameWithType = "Huyện Chư Sê",
            NameWithProvince = "Huyện Chư Sê, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 634,
            Name = "Đăk Pơ",
            Slug = "dak-po",
            NameWithType = "Huyện Đăk Pơ",
            NameWithProvince = "Huyện Đăk Pơ, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 635,
            Name = "Ia Pa",
            Slug = "ia-pa",
            NameWithType = "Huyện Ia Pa",
            NameWithProvince = "Huyện Ia Pa, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 637,
            Name = "Krông Pa",
            Slug = "krong-pa",
            NameWithType = "Huyện Krông Pa",
            NameWithProvince = "Huyện Krông Pa, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 638,
            Name = "Phú Thiện",
            Slug = "phu-thien",
            NameWithType = "Huyện Phú Thiện",
            NameWithProvince = "Huyện Phú Thiện, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 639,
            Name = "Chư Pưh",
            Slug = "chu-puh",
            NameWithType = "Huyện Chư Pưh",
            NameWithProvince = "Huyện Chư Pưh, Tỉnh Gia Lai",
            ParentCode = 64,
        },
        new District()
        {
            Code = 643,
            Name = "Buôn Ma Thuột",
            Slug = "buon-ma-thuot",
            NameWithType = "Thành phố Buôn Ma Thuột",
            NameWithProvince = "Thành phố Buôn Ma Thuột, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 644,
            Name = "Buôn Hồ",
            Slug = "buon-ho",
            NameWithType = "Thị xã Buôn Hồ",
            NameWithProvince = "Thị xã Buôn Hồ, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 645,
            Name = "Ea H'leo",
            Slug = "ea-h-leo",
            NameWithType = "Huyện Ea H'leo",
            NameWithProvince = "Huyện Ea H'leo, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 646,
            Name = "Ea Súp",
            Slug = "ea-sup",
            NameWithType = "Huyện Ea Súp",
            NameWithProvince = "Huyện Ea Súp, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 647,
            Name = "Buôn Đôn",
            Slug = "buon-don",
            NameWithType = "Huyện Buôn Đôn",
            NameWithProvince = "Huyện Buôn Đôn, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 648,
            Name = "Cư M'gar",
            Slug = "cu-m-gar",
            NameWithType = "Huyện Cư M'gar",
            NameWithProvince = "Huyện Cư M'gar, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 649,
            Name = "Krông Búk",
            Slug = "krong-buk",
            NameWithType = "Huyện Krông Búk",
            NameWithProvince = "Huyện Krông Búk, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 650,
            Name = "Krông Năng",
            Slug = "krong-nang",
            NameWithType = "Huyện Krông Năng",
            NameWithProvince = "Huyện Krông Năng, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 651,
            Name = "Ea Kar",
            Slug = "ea-kar",
            NameWithType = "Huyện Ea Kar",
            NameWithProvince = "Huyện Ea Kar, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 652,
            Name = "M'Đrắk",
            Slug = "m-drak",
            NameWithType = "Huyện M'Đrắk",
            NameWithProvince = "Huyện M'Đrắk, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 653,
            Name = "Krông Bông",
            Slug = "krong-bong",
            NameWithType = "Huyện Krông Bông",
            NameWithProvince = "Huyện Krông Bông, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 654,
            Name = "Krông Pắc",
            Slug = "krong-pac",
            NameWithType = "Huyện Krông Pắc",
            NameWithProvince = "Huyện Krông Pắc, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 655,
            Name = "Krông A Na",
            Slug = "krong-a-na",
            NameWithType = "Huyện Krông A Na",
            NameWithProvince = "Huyện Krông A Na, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 656,
            Name = "Lắk",
            Slug = "lak",
            NameWithType = "Huyện Lắk",
            NameWithProvince = "Huyện Lắk, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 657,
            Name = "Cư Kuin",
            Slug = "cu-kuin",
            NameWithType = "Huyện Cư Kuin",
            NameWithProvince = "Huyện Cư Kuin, Tỉnh Đắk Lắk",
            ParentCode = 66,
        },
        new District()
        {
            Code = 660,
            Name = "Gia Nghĩa",
            Slug = "gia-nghia",
            NameWithType = "Thành phố Gia Nghĩa",
            NameWithProvince = "Thành phố Gia Nghĩa, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 661,
            Name = "Đăk Glong",
            Slug = "dak-glong",
            NameWithType = "Huyện Đăk Glong",
            NameWithProvince = "Huyện Đăk Glong, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 662,
            Name = "Cư Jút",
            Slug = "cu-jut",
            NameWithType = "Huyện Cư Jút",
            NameWithProvince = "Huyện Cư Jút, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 663,
            Name = "Đắk Mil",
            Slug = "dak-mil",
            NameWithType = "Huyện Đắk Mil",
            NameWithProvince = "Huyện Đắk Mil, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 664,
            Name = "Krông Nô",
            Slug = "krong-no",
            NameWithType = "Huyện Krông Nô",
            NameWithProvince = "Huyện Krông Nô, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 665,
            Name = "Đắk Song",
            Slug = "dak-song",
            NameWithType = "Huyện Đắk Song",
            NameWithProvince = "Huyện Đắk Song, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 666,
            Name = "Đắk R'Lấp",
            Slug = "dak-r-lap",
            NameWithType = "Huyện Đắk R'Lấp",
            NameWithProvince = "Huyện Đắk R'Lấp, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 667,
            Name = "Tuy Đức",
            Slug = "tuy-duc",
            NameWithType = "Huyện Tuy Đức",
            NameWithProvince = "Huyện Tuy Đức, Tỉnh Đắk Nông",
            ParentCode = 67,
        },
        new District()
        {
            Code = 672,
            Name = "Đà Lạt",
            Slug = "da-lat",
            NameWithType = "Thành phố Đà Lạt",
            NameWithProvince = "Thành phố Đà Lạt, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 673,
            Name = "Bảo Lộc",
            Slug = "bao-loc",
            NameWithType = "Thành phố Bảo Lộc",
            NameWithProvince = "Thành phố Bảo Lộc, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 674,
            Name = "Đam Rông",
            Slug = "dam-rong",
            NameWithType = "Huyện Đam Rông",
            NameWithProvince = "Huyện Đam Rông, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 675,
            Name = "Lạc Dương",
            Slug = "lac-duong",
            NameWithType = "Huyện Lạc Dương",
            NameWithProvince = "Huyện Lạc Dương, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 676,
            Name = "Lâm Hà",
            Slug = "lam-ha",
            NameWithType = "Huyện Lâm Hà",
            NameWithProvince = "Huyện Lâm Hà, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 677,
            Name = "Đơn Dương",
            Slug = "don-duong",
            NameWithType = "Huyện Đơn Dương",
            NameWithProvince = "Huyện Đơn Dương, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 678,
            Name = "Đức Trọng",
            Slug = "duc-trong",
            NameWithType = "Huyện Đức Trọng",
            NameWithProvince = "Huyện Đức Trọng, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 679,
            Name = "Di Linh",
            Slug = "di-linh",
            NameWithType = "Huyện Di Linh",
            NameWithProvince = "Huyện Di Linh, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 680,
            Name = "Bảo Lâm",
            Slug = "bao-lam",
            NameWithType = "Huyện Bảo Lâm",
            NameWithProvince = "Huyện Bảo Lâm, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 681,
            Name = "Đạ Huoai",
            Slug = "da-huoai",
            NameWithType = "Huyện Đạ Huoai",
            NameWithProvince = "Huyện Đạ Huoai, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 682,
            Name = "Đạ Tẻh",
            Slug = "da-teh",
            NameWithType = "Huyện Đạ Tẻh",
            NameWithProvince = "Huyện Đạ Tẻh, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 683,
            Name = "Cát Tiên",
            Slug = "cat-tien",
            NameWithType = "Huyện Cát Tiên",
            NameWithProvince = "Huyện Cát Tiên, Tỉnh Lâm Đồng",
            ParentCode = 68,
        },
        new District()
        {
            Code = 688,
            Name = "Phước Long",
            Slug = "phuoc-long",
            NameWithType = "Thị xã Phước Long",
            NameWithProvince = "Thị xã Phước Long, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 689,
            Name = "Đồng Xoài",
            Slug = "dong-xoai",
            NameWithType = "Thành phố Đồng Xoài",
            NameWithProvince = "Thành phố Đồng Xoài, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 690,
            Name = "Bình Long",
            Slug = "binh-long",
            NameWithType = "Thị xã Bình Long",
            NameWithProvince = "Thị xã Bình Long, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 691,
            Name = "Bù Gia Mập",
            Slug = "bu-gia-map",
            NameWithType = "Huyện Bù Gia Mập",
            NameWithProvince = "Huyện Bù Gia Mập, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 692,
            Name = "Lộc Ninh",
            Slug = "loc-ninh",
            NameWithType = "Huyện Lộc Ninh",
            NameWithProvince = "Huyện Lộc Ninh, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 693,
            Name = "Bù Đốp",
            Slug = "bu-dop",
            NameWithType = "Huyện Bù Đốp",
            NameWithProvince = "Huyện Bù Đốp, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 694,
            Name = "Hớn Quản",
            Slug = "hon-quan",
            NameWithType = "Huyện Hớn Quản",
            NameWithProvince = "Huyện Hớn Quản, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 695,
            Name = "Đồng Phú",
            Slug = "dong-phu",
            NameWithType = "Huyện Đồng Phú",
            NameWithProvince = "Huyện Đồng Phú, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 696,
            Name = "Bù Đăng",
            Slug = "bu-dang",
            NameWithType = "Huyện Bù Đăng",
            NameWithProvince = "Huyện Bù Đăng, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 697,
            Name = "Chơn Thành",
            Slug = "chon-thanh",
            NameWithType = "Thị xã Chơn Thành",
            NameWithProvince = "Thị xã Chơn Thành, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 698,
            Name = "Phú Riềng",
            Slug = "phu-rieng",
            NameWithType = "Huyện Phú Riềng",
            NameWithProvince = "Huyện Phú Riềng, Tỉnh Bình Phước",
            ParentCode = 70,
        },
        new District()
        {
            Code = 703,
            Name = "Tây Ninh",
            Slug = "tay-ninh",
            NameWithType = "Thành phố Tây Ninh",
            NameWithProvince = "Thành phố Tây Ninh, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 705,
            Name = "Tân Biên",
            Slug = "tan-bien",
            NameWithType = "Huyện Tân Biên",
            NameWithProvince = "Huyện Tân Biên, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 706,
            Name = "Tân Châu",
            Slug = "tan-chau",
            NameWithType = "Huyện Tân Châu",
            NameWithProvince = "Huyện Tân Châu, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 707,
            Name = "Dương Minh Châu",
            Slug = "duong-minh-chau",
            NameWithType = "Huyện Dương Minh Châu",
            NameWithProvince = "Huyện Dương Minh Châu, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 708,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 709,
            Name = "Hòa Thành",
            Slug = "hoa-thanh",
            NameWithType = "Thị xã Hòa Thành",
            NameWithProvince = "Thị xã Hòa Thành, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 710,
            Name = "Gò Dầu",
            Slug = "go-dau",
            NameWithType = "Huyện Gò Dầu",
            NameWithProvince = "Huyện Gò Dầu, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 711,
            Name = "Bến Cầu",
            Slug = "ben-cau",
            NameWithType = "Huyện Bến Cầu",
            NameWithProvince = "Huyện Bến Cầu, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 712,
            Name = "Trảng Bàng",
            Slug = "trang-bang",
            NameWithType = "Thị xã Trảng Bàng",
            NameWithProvince = "Thị xã Trảng Bàng, Tỉnh Tây Ninh",
            ParentCode = 72,
        },
        new District()
        {
            Code = 718,
            Name = "Thủ Dầu Một",
            Slug = "thu-dau-mot",
            NameWithType = "Thành phố Thủ Dầu Một",
            NameWithProvince = "Thành phố Thủ Dầu Một, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 719,
            Name = "Bàu Bàng",
            Slug = "bau-bang",
            NameWithType = "Huyện Bàu Bàng",
            NameWithProvince = "Huyện Bàu Bàng, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 720,
            Name = "Dầu Tiếng",
            Slug = "dau-tieng",
            NameWithType = "Huyện Dầu Tiếng",
            NameWithProvince = "Huyện Dầu Tiếng, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 721,
            Name = "Bến Cát",
            Slug = "ben-cat",
            NameWithType = "Thị xã Bến Cát",
            NameWithProvince = "Thị xã Bến Cát, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 722,
            Name = "Phú Giáo",
            Slug = "phu-giao",
            NameWithType = "Huyện Phú Giáo",
            NameWithProvince = "Huyện Phú Giáo, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 723,
            Name = "Tân Uyên",
            Slug = "tan-uyen",
            NameWithType = "Thị xã Tân Uyên",
            NameWithProvince = "Thị xã Tân Uyên, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 724,
            Name = "Dĩ An",
            Slug = "di-an",
            NameWithType = "Thành phố Dĩ An",
            NameWithProvince = "Thành phố Dĩ An, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 725,
            Name = "Thuận An",
            Slug = "thuan-an",
            NameWithType = "Thành phố Thuận An",
            NameWithProvince = "Thành phố Thuận An, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 726,
            Name = "Bắc Tân Uyên",
            Slug = "bac-tan-uyen",
            NameWithType = "Huyện Bắc Tân Uyên",
            NameWithProvince = "Huyện Bắc Tân Uyên, Tỉnh Bình Dương",
            ParentCode = 74,
        },
        new District()
        {
            Code = 731,
            Name = "Biên Hòa",
            Slug = "bien-hoa",
            NameWithType = "Thành phố Biên Hòa",
            NameWithProvince = "Thành phố Biên Hòa, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 732,
            Name = "Long Khánh",
            Slug = "long-khanh",
            NameWithType = "Thành phố Long Khánh",
            NameWithProvince = "Thành phố Long Khánh, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 734,
            Name = "Tân Phú",
            Slug = "tan-phu",
            NameWithType = "Huyện Tân Phú",
            NameWithProvince = "Huyện Tân Phú, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 735,
            Name = "Vĩnh Cửu",
            Slug = "vinh-cuu",
            NameWithType = "Huyện Vĩnh Cửu",
            NameWithProvince = "Huyện Vĩnh Cửu, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 736,
            Name = "Định Quán",
            Slug = "dinh-quan",
            NameWithType = "Huyện Định Quán",
            NameWithProvince = "Huyện Định Quán, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 737,
            Name = "Trảng Bom",
            Slug = "trang-bom",
            NameWithType = "Huyện Trảng Bom",
            NameWithProvince = "Huyện Trảng Bom, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 738,
            Name = "Thống Nhất",
            Slug = "thong-nhat",
            NameWithType = "Huyện Thống Nhất",
            NameWithProvince = "Huyện Thống Nhất, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 739,
            Name = "Cẩm Mỹ",
            Slug = "cam-my",
            NameWithType = "Huyện Cẩm Mỹ",
            NameWithProvince = "Huyện Cẩm Mỹ, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 740,
            Name = "Long Thành",
            Slug = "long-thanh",
            NameWithType = "Huyện Long Thành",
            NameWithProvince = "Huyện Long Thành, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 741,
            Name = "Xuân Lộc",
            Slug = "xuan-loc",
            NameWithType = "Huyện Xuân Lộc",
            NameWithProvince = "Huyện Xuân Lộc, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 742,
            Name = "Nhơn Trạch",
            Slug = "nhon-trach",
            NameWithType = "Huyện Nhơn Trạch",
            NameWithProvince = "Huyện Nhơn Trạch, Tỉnh Đồng Nai",
            ParentCode = 75,
        },
        new District()
        {
            Code = 747,
            Name = "Vũng Tàu",
            Slug = "vung-tau",
            NameWithType = "Thành phố Vũng Tàu",
            NameWithProvince = "Thành phố Vũng Tàu, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 748,
            Name = "Bà Rịa",
            Slug = "ba-ria",
            NameWithType = "Thành phố Bà Rịa",
            NameWithProvince = "Thành phố Bà Rịa, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 750,
            Name = "Châu Đức",
            Slug = "chau-duc",
            NameWithType = "Huyện Châu Đức",
            NameWithProvince = "Huyện Châu Đức, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 751,
            Name = "Xuyên Mộc",
            Slug = "xuyen-moc",
            NameWithType = "Huyện Xuyên Mộc",
            NameWithProvince = "Huyện Xuyên Mộc, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 752,
            Name = "Long Điền",
            Slug = "long-dien",
            NameWithType = "Huyện Long Điền",
            NameWithProvince = "Huyện Long Điền, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 753,
            Name = "Đất Đỏ",
            Slug = "dat-do",
            NameWithType = "Huyện Đất Đỏ",
            NameWithProvince = "Huyện Đất Đỏ, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 754,
            Name = "Phú Mỹ",
            Slug = "phu-my",
            NameWithType = "Thị xã Phú Mỹ",
            NameWithProvince = "Thị xã Phú Mỹ, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 755,
            Name = "Côn Đảo",
            Slug = "con-dao",
            NameWithType = "Huyện Côn Đảo",
            NameWithProvince = "Huyện Côn Đảo, Tỉnh Bà Rịa - Vũng Tàu",
            ParentCode = 77,
        },
        new District()
        {
            Code = 760,
            Name = "1",
            Slug = "1",
            NameWithType = "Quận 1",
            NameWithProvince = "Quận 1, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 761,
            Name = "12",
            Slug = "12",
            NameWithType = "Quận 12",
            NameWithProvince = "Quận 12, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 764,
            Name = "Gò Vấp",
            Slug = "go-vap",
            NameWithType = "Quận Gò Vấp",
            NameWithProvince = "Quận Gò Vấp, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 765,
            Name = "Bình Thạnh",
            Slug = "binh-thanh",
            NameWithType = "Quận Bình Thạnh",
            NameWithProvince = "Quận Bình Thạnh, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 766,
            Name = "Tân Bình",
            Slug = "tan-binh",
            NameWithType = "Quận Tân Bình",
            NameWithProvince = "Quận Tân Bình, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 767,
            Name = "Tân Phú",
            Slug = "tan-phu",
            NameWithType = "Quận Tân Phú",
            NameWithProvince = "Quận Tân Phú, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 768,
            Name = "Phú Nhuận",
            Slug = "phu-nhuan",
            NameWithType = "Quận Phú Nhuận",
            NameWithProvince = "Quận Phú Nhuận, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 769,
            Name = "Thủ Đức",
            Slug = "thu-duc",
            NameWithType = "Thành phố Thủ Đức",
            NameWithProvince = "Thành phố Thủ Đức, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 770,
            Name = "3",
            Slug = "3",
            NameWithType = "Quận 3",
            NameWithProvince = "Quận 3, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 771,
            Name = "10",
            Slug = "10",
            NameWithType = "Quận 10",
            NameWithProvince = "Quận 10, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 772,
            Name = "11",
            Slug = "11",
            NameWithType = "Quận 11",
            NameWithProvince = "Quận 11, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 773,
            Name = "4",
            Slug = "4",
            NameWithType = "Quận 4",
            NameWithProvince = "Quận 4, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 774,
            Name = "5",
            Slug = "5",
            NameWithType = "Quận 5",
            NameWithProvince = "Quận 5, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 775,
            Name = "6",
            Slug = "6",
            NameWithType = "Quận 6",
            NameWithProvince = "Quận 6, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 776,
            Name = "8",
            Slug = "8",
            NameWithType = "Quận 8",
            NameWithProvince = "Quận 8, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 777,
            Name = "Bình Tân",
            Slug = "binh-tan",
            NameWithType = "Quận Bình Tân",
            NameWithProvince = "Quận Bình Tân, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 778,
            Name = "7",
            Slug = "7",
            NameWithType = "Quận 7",
            NameWithProvince = "Quận 7, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 783,
            Name = "Củ Chi",
            Slug = "cu-chi",
            NameWithType = "Huyện Củ Chi",
            NameWithProvince = "Huyện Củ Chi, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 784,
            Name = "Hóc Môn",
            Slug = "hoc-mon",
            NameWithType = "Huyện Hóc Môn",
            NameWithProvince = "Huyện Hóc Môn, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 785,
            Name = "Bình Chánh",
            Slug = "binh-chanh",
            NameWithType = "Huyện Bình Chánh",
            NameWithProvince = "Huyện Bình Chánh, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 786,
            Name = "Nhà Bè",
            Slug = "nha-be",
            NameWithType = "Huyện Nhà Bè",
            NameWithProvince = "Huyện Nhà Bè, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 787,
            Name = "Cần Giờ",
            Slug = "can-gio",
            NameWithType = "Huyện Cần Giờ",
            NameWithProvince = "Huyện Cần Giờ, Thành phố Hồ Chí Minh",
            ParentCode = 79,
        },
        new District()
        {
            Code = 794,
            Name = "Tân An",
            Slug = "tan-an",
            NameWithType = "Thành phố Tân An",
            NameWithProvince = "Thành phố Tân An, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 795,
            Name = "Kiến Tường",
            Slug = "kien-tuong",
            NameWithType = "Thị xã Kiến Tường",
            NameWithProvince = "Thị xã Kiến Tường, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 796,
            Name = "Tân Hưng",
            Slug = "tan-hung",
            NameWithType = "Huyện Tân Hưng",
            NameWithProvince = "Huyện Tân Hưng, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 797,
            Name = "Vĩnh Hưng",
            Slug = "vinh-hung",
            NameWithType = "Huyện Vĩnh Hưng",
            NameWithProvince = "Huyện Vĩnh Hưng, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 798,
            Name = "Mộc Hóa",
            Slug = "moc-hoa",
            NameWithType = "Huyện Mộc Hóa",
            NameWithProvince = "Huyện Mộc Hóa, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 799,
            Name = "Tân Thạnh",
            Slug = "tan-thanh",
            NameWithType = "Huyện Tân Thạnh",
            NameWithProvince = "Huyện Tân Thạnh, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 800,
            Name = "Thạnh Hóa",
            Slug = "thanh-hoa",
            NameWithType = "Huyện Thạnh Hóa",
            NameWithProvince = "Huyện Thạnh Hóa, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 801,
            Name = "Đức Huệ",
            Slug = "duc-hue",
            NameWithType = "Huyện Đức Huệ",
            NameWithProvince = "Huyện Đức Huệ, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 802,
            Name = "Đức Hòa",
            Slug = "duc-hoa",
            NameWithType = "Huyện Đức Hòa",
            NameWithProvince = "Huyện Đức Hòa, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 803,
            Name = "Bến Lức",
            Slug = "ben-luc",
            NameWithType = "Huyện Bến Lức",
            NameWithProvince = "Huyện Bến Lức, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 804,
            Name = "Thủ Thừa",
            Slug = "thu-thua",
            NameWithType = "Huyện Thủ Thừa",
            NameWithProvince = "Huyện Thủ Thừa, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 805,
            Name = "Tân Trụ",
            Slug = "tan-tru",
            NameWithType = "Huyện Tân Trụ",
            NameWithProvince = "Huyện Tân Trụ, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 806,
            Name = "Cần Đước",
            Slug = "can-duoc",
            NameWithType = "Huyện Cần Đước",
            NameWithProvince = "Huyện Cần Đước, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 807,
            Name = "Cần Giuộc",
            Slug = "can-giuoc",
            NameWithType = "Huyện Cần Giuộc",
            NameWithProvince = "Huyện Cần Giuộc, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 808,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Long An",
            ParentCode = 80,
        },
        new District()
        {
            Code = 815,
            Name = "Mỹ Tho",
            Slug = "my-tho",
            NameWithType = "Thành phố Mỹ Tho",
            NameWithProvince = "Thành phố Mỹ Tho, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 816,
            Name = "Gò Công",
            Slug = "go-cong",
            NameWithType = "Thị xã Gò Công",
            NameWithProvince = "Thị xã Gò Công, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 817,
            Name = "Cai Lậy",
            Slug = "cai-lay",
            NameWithType = "Thị xã Cai Lậy",
            NameWithProvince = "Thị xã Cai Lậy, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 818,
            Name = "Tân Phước",
            Slug = "tan-phuoc",
            NameWithType = "Huyện Tân Phước",
            NameWithProvince = "Huyện Tân Phước, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 819,
            Name = "Cái Bè",
            Slug = "cai-be",
            NameWithType = "Huyện Cái Bè",
            NameWithProvince = "Huyện Cái Bè, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 820,
            Name = "Cai Lậy",
            Slug = "cai-lay",
            NameWithType = "Huyện Cai Lậy",
            NameWithProvince = "Huyện Cai Lậy, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 821,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 822,
            Name = "Chợ Gạo",
            Slug = "cho-gao",
            NameWithType = "Huyện Chợ Gạo",
            NameWithProvince = "Huyện Chợ Gạo, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 823,
            Name = "Gò Công Tây",
            Slug = "go-cong-tay",
            NameWithType = "Huyện Gò Công Tây",
            NameWithProvince = "Huyện Gò Công Tây, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 824,
            Name = "Gò Công Đông",
            Slug = "go-cong-dong",
            NameWithType = "Huyện Gò Công Đông",
            NameWithProvince = "Huyện Gò Công Đông, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 825,
            Name = "Tân Phú Đông",
            Slug = "tan-phu-dong",
            NameWithType = "Huyện Tân Phú Đông",
            NameWithProvince = "Huyện Tân Phú Đông, Tỉnh Tiền Giang",
            ParentCode = 82,
        },
        new District()
        {
            Code = 829,
            Name = "Bến Tre",
            Slug = "ben-tre",
            NameWithType = "Thành phố Bến Tre",
            NameWithProvince = "Thành phố Bến Tre, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 831,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 832,
            Name = "Chợ Lách",
            Slug = "cho-lach",
            NameWithType = "Huyện Chợ Lách",
            NameWithProvince = "Huyện Chợ Lách, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 833,
            Name = "Mỏ Cày Nam",
            Slug = "mo-cay-nam",
            NameWithType = "Huyện Mỏ Cày Nam",
            NameWithProvince = "Huyện Mỏ Cày Nam, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 834,
            Name = "Giồng Trôm",
            Slug = "giong-trom",
            NameWithType = "Huyện Giồng Trôm",
            NameWithProvince = "Huyện Giồng Trôm, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 835,
            Name = "Bình Đại",
            Slug = "binh-dai",
            NameWithType = "Huyện Bình Đại",
            NameWithProvince = "Huyện Bình Đại, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 836,
            Name = "Ba Tri",
            Slug = "ba-tri",
            NameWithType = "Huyện Ba Tri",
            NameWithProvince = "Huyện Ba Tri, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 837,
            Name = "Thạnh Phú",
            Slug = "thanh-phu",
            NameWithType = "Huyện Thạnh Phú",
            NameWithProvince = "Huyện Thạnh Phú, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 838,
            Name = "Mỏ Cày Bắc",
            Slug = "mo-cay-bac",
            NameWithType = "Huyện Mỏ Cày Bắc",
            NameWithProvince = "Huyện Mỏ Cày Bắc, Tỉnh Bến Tre",
            ParentCode = 83,
        },
        new District()
        {
            Code = 842,
            Name = "Trà Vinh",
            Slug = "tra-vinh",
            NameWithType = "Thành phố Trà Vinh",
            NameWithProvince = "Thành phố Trà Vinh, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 844,
            Name = "Càng Long",
            Slug = "cang-long",
            NameWithType = "Huyện Càng Long",
            NameWithProvince = "Huyện Càng Long, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 845,
            Name = "Cầu Kè",
            Slug = "cau-ke",
            NameWithType = "Huyện Cầu Kè",
            NameWithProvince = "Huyện Cầu Kè, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 846,
            Name = "Tiểu Cần",
            Slug = "tieu-can",
            NameWithType = "Huyện Tiểu Cần",
            NameWithProvince = "Huyện Tiểu Cần, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 847,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 848,
            Name = "Cầu Ngang",
            Slug = "cau-ngang",
            NameWithType = "Huyện Cầu Ngang",
            NameWithProvince = "Huyện Cầu Ngang, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 849,
            Name = "Trà Cú",
            Slug = "tra-cu",
            NameWithType = "Huyện Trà Cú",
            NameWithProvince = "Huyện Trà Cú, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 850,
            Name = "Duyên Hải",
            Slug = "duyen-hai",
            NameWithType = "Huyện Duyên Hải",
            NameWithProvince = "Huyện Duyên Hải, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 851,
            Name = "Duyên Hải",
            Slug = "duyen-hai",
            NameWithType = "Thị xã Duyên Hải",
            NameWithProvince = "Thị xã Duyên Hải, Tỉnh Trà Vinh",
            ParentCode = 84,
        },
        new District()
        {
            Code = 855,
            Name = "Vĩnh Long",
            Slug = "vinh-long",
            NameWithType = "Thành phố Vĩnh Long",
            NameWithProvince = "Thành phố Vĩnh Long, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 857,
            Name = "Long Hồ",
            Slug = "long-ho",
            NameWithType = "Huyện Long Hồ",
            NameWithProvince = "Huyện Long Hồ, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 858,
            Name = "Mang Thít",
            Slug = "mang-thit",
            NameWithType = "Huyện Mang Thít",
            NameWithProvince = "Huyện Mang Thít, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 859,
            Name = "Vũng Liêm",
            Slug = "vung-liem",
            NameWithType = "Huyện  Vũng Liêm",
            NameWithProvince = "Huyện  Vũng Liêm, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 860,
            Name = "Tam Bình",
            Slug = "tam-binh",
            NameWithType = "Huyện Tam Bình",
            NameWithProvince = "Huyện Tam Bình, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 861,
            Name = "Bình Minh",
            Slug = "binh-minh",
            NameWithType = "Thị xã Bình Minh",
            NameWithProvince = "Thị xã Bình Minh, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 862,
            Name = "Trà Ôn",
            Slug = "tra-on",
            NameWithType = "Huyện Trà Ôn",
            NameWithProvince = "Huyện Trà Ôn, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 863,
            Name = "Bình Tân",
            Slug = "binh-tan",
            NameWithType = "Huyện Bình Tân",
            NameWithProvince = "Huyện Bình Tân, Tỉnh Vĩnh Long",
            ParentCode = 86,
        },
        new District()
        {
            Code = 866,
            Name = "Cao Lãnh",
            Slug = "cao-lanh",
            NameWithType = "Thành phố Cao Lãnh",
            NameWithProvince = "Thành phố Cao Lãnh, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 867,
            Name = "Sa Đéc",
            Slug = "sa-dec",
            NameWithType = "Thành phố Sa Đéc",
            NameWithProvince = "Thành phố Sa Đéc, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 868,
            Name = "Hồng Ngự",
            Slug = "hong-ngu",
            NameWithType = "Thành phố Hồng Ngự",
            NameWithProvince = "Thành phố Hồng Ngự, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 869,
            Name = "Tân Hồng",
            Slug = "tan-hong",
            NameWithType = "Huyện Tân Hồng",
            NameWithProvince = "Huyện Tân Hồng, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 870,
            Name = "Hồng Ngự",
            Slug = "hong-ngu",
            NameWithType = "Huyện Hồng Ngự",
            NameWithProvince = "Huyện Hồng Ngự, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 871,
            Name = "Tam Nông",
            Slug = "tam-nong",
            NameWithType = "Huyện Tam Nông",
            NameWithProvince = "Huyện Tam Nông, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 872,
            Name = "Tháp Mười",
            Slug = "thap-muoi",
            NameWithType = "Huyện Tháp Mười",
            NameWithProvince = "Huyện Tháp Mười, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 873,
            Name = "Cao Lãnh",
            Slug = "cao-lanh",
            NameWithType = "Huyện Cao Lãnh",
            NameWithProvince = "Huyện Cao Lãnh, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 874,
            Name = "Thanh Bình",
            Slug = "thanh-binh",
            NameWithType = "Huyện Thanh Bình",
            NameWithProvince = "Huyện Thanh Bình, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 875,
            Name = "Lấp Vò",
            Slug = "lap-vo",
            NameWithType = "Huyện Lấp Vò",
            NameWithProvince = "Huyện Lấp Vò, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 876,
            Name = "Lai Vung",
            Slug = "lai-vung",
            NameWithType = "Huyện Lai Vung",
            NameWithProvince = "Huyện Lai Vung, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 877,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Đồng Tháp",
            ParentCode = 87,
        },
        new District()
        {
            Code = 883,
            Name = "Long Xuyên",
            Slug = "long-xuyen",
            NameWithType = "Thành phố Long Xuyên",
            NameWithProvince = "Thành phố Long Xuyên, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 884,
            Name = "Châu Đốc",
            Slug = "chau-doc",
            NameWithType = "Thành phố Châu Đốc",
            NameWithProvince = "Thành phố Châu Đốc, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 886,
            Name = "An Phú",
            Slug = "an-phu",
            NameWithType = "Huyện An Phú",
            NameWithProvince = "Huyện An Phú, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 887,
            Name = "Tân Châu",
            Slug = "tan-chau",
            NameWithType = "Thị xã Tân Châu",
            NameWithProvince = "Thị xã Tân Châu, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 888,
            Name = "Phú Tân",
            Slug = "phu-tan",
            NameWithType = "Huyện Phú Tân",
            NameWithProvince = "Huyện Phú Tân, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 889,
            Name = "Châu Phú",
            Slug = "chau-phu",
            NameWithType = "Huyện Châu Phú",
            NameWithProvince = "Huyện Châu Phú, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 890,
            Name = "Tịnh Biên",
            Slug = "tinh-bien",
            NameWithType = "Huyện Tịnh Biên",
            NameWithProvince = "Huyện Tịnh Biên, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 891,
            Name = "Tri Tôn",
            Slug = "tri-ton",
            NameWithType = "Huyện Tri Tôn",
            NameWithProvince = "Huyện Tri Tôn, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 892,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 893,
            Name = "Chợ Mới",
            Slug = "cho-moi",
            NameWithType = "Huyện Chợ Mới",
            NameWithProvince = "Huyện Chợ Mới, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 894,
            Name = "Thoại Sơn",
            Slug = "thoai-son",
            NameWithType = "Huyện Thoại Sơn",
            NameWithProvince = "Huyện Thoại Sơn, Tỉnh An Giang",
            ParentCode = 89,
        },
        new District()
        {
            Code = 899,
            Name = "Rạch Giá",
            Slug = "rach-gia",
            NameWithType = "Thành phố Rạch Giá",
            NameWithProvince = "Thành phố Rạch Giá, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 900,
            Name = "Hà Tiên",
            Slug = "ha-tien",
            NameWithType = "Thành phố Hà Tiên",
            NameWithProvince = "Thành phố Hà Tiên, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 902,
            Name = "Kiên Lương",
            Slug = "kien-luong",
            NameWithType = "Huyện Kiên Lương",
            NameWithProvince = "Huyện Kiên Lương, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 903,
            Name = "Hòn Đất",
            Slug = "hon-dat",
            NameWithType = "Huyện Hòn Đất",
            NameWithProvince = "Huyện Hòn Đất, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 904,
            Name = "Tân Hiệp",
            Slug = "tan-hiep",
            NameWithType = "Huyện Tân Hiệp",
            NameWithProvince = "Huyện Tân Hiệp, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 905,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 906,
            Name = "Giồng Riềng",
            Slug = "giong-rieng",
            NameWithType = "Huyện Giồng Riềng",
            NameWithProvince = "Huyện Giồng Riềng, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 907,
            Name = "Gò Quao",
            Slug = "go-quao",
            NameWithType = "Huyện Gò Quao",
            NameWithProvince = "Huyện Gò Quao, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 908,
            Name = "An Biên",
            Slug = "an-bien",
            NameWithType = "Huyện An Biên",
            NameWithProvince = "Huyện An Biên, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 909,
            Name = "An Minh",
            Slug = "an-minh",
            NameWithType = "Huyện An Minh",
            NameWithProvince = "Huyện An Minh, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 910,
            Name = "Vĩnh Thuận",
            Slug = "vinh-thuan",
            NameWithType = "Huyện Vĩnh Thuận",
            NameWithProvince = "Huyện Vĩnh Thuận, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 911,
            Name = "Phú Quốc",
            Slug = "phu-quoc",
            NameWithType = "Thành phố Phú Quốc",
            NameWithProvince = "Thành phố Phú Quốc, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 912,
            Name = "Kiên Hải",
            Slug = "kien-hai",
            NameWithType = "Huyện Kiên Hải",
            NameWithProvince = "Huyện Kiên Hải, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 913,
            Name = "U Minh Thượng",
            Slug = "u-minh-thuong",
            NameWithType = "Huyện U Minh Thượng",
            NameWithProvince = "Huyện U Minh Thượng, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 914,
            Name = "Giang Thành",
            Slug = "giang-thanh",
            NameWithType = "Huyện Giang Thành",
            NameWithProvince = "Huyện Giang Thành, Tỉnh Kiên Giang",
            ParentCode = 91,
        },
        new District()
        {
            Code = 916,
            Name = "Ninh Kiều",
            Slug = "ninh-kieu",
            NameWithType = "Quận Ninh Kiều",
            NameWithProvince = "Quận Ninh Kiều, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 917,
            Name = "Ô Môn",
            Slug = "o-mon",
            NameWithType = "Quận Ô Môn",
            NameWithProvince = "Quận Ô Môn, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 918,
            Name = "Bình Thuỷ",
            Slug = "binh-thuy",
            NameWithType = "Quận Bình Thuỷ",
            NameWithProvince = "Quận Bình Thuỷ, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 919,
            Name = "Cái Răng",
            Slug = "cai-rang",
            NameWithType = "Quận Cái Răng",
            NameWithProvince = "Quận Cái Răng, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 923,
            Name = "Thốt Nốt",
            Slug = "thot-not",
            NameWithType = "Quận Thốt Nốt",
            NameWithProvince = "Quận Thốt Nốt, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 924,
            Name = "Vĩnh Thạnh",
            Slug = "vinh-thanh",
            NameWithType = "Huyện Vĩnh Thạnh",
            NameWithProvince = "Huyện Vĩnh Thạnh, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 925,
            Name = "Cờ Đỏ",
            Slug = "co-do",
            NameWithType = "Huyện Cờ Đỏ",
            NameWithProvince = "Huyện Cờ Đỏ, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 926,
            Name = "Phong Điền",
            Slug = "phong-dien",
            NameWithType = "Huyện Phong Điền",
            NameWithProvince = "Huyện Phong Điền, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 927,
            Name = "Thới Lai",
            Slug = "thoi-lai",
            NameWithType = "Huyện Thới Lai",
            NameWithProvince = "Huyện Thới Lai, Thành phố Cần Thơ",
            ParentCode = 92,
        },
        new District()
        {
            Code = 930,
            Name = "Vị Thanh",
            Slug = "vi-thanh",
            NameWithType = "Thành phố Vị Thanh",
            NameWithProvince = "Thành phố Vị Thanh, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 931,
            Name = "Ngã Bảy",
            Slug = "nga-bay",
            NameWithType = "Thành phố Ngã Bảy",
            NameWithProvince = "Thành phố Ngã Bảy, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 932,
            Name = "Châu Thành A",
            Slug = "chau-thanh-a",
            NameWithType = "Huyện Châu Thành A",
            NameWithProvince = "Huyện Châu Thành A, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 933,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 934,
            Name = "Phụng Hiệp",
            Slug = "phung-hiep",
            NameWithType = "Huyện Phụng Hiệp",
            NameWithProvince = "Huyện Phụng Hiệp, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 935,
            Name = "Vị Thuỷ",
            Slug = "vi-thuy",
            NameWithType = "Huyện Vị Thuỷ",
            NameWithProvince = "Huyện Vị Thuỷ, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 936,
            Name = "Long Mỹ",
            Slug = "long-my",
            NameWithType = "Huyện Long Mỹ",
            NameWithProvince = "Huyện Long Mỹ, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 937,
            Name = "Long Mỹ",
            Slug = "long-my",
            NameWithType = "Thị xã Long Mỹ",
            NameWithProvince = "Thị xã Long Mỹ, Tỉnh Hậu Giang",
            ParentCode = 93,
        },
        new District()
        {
            Code = 941,
            Name = "Sóc Trăng",
            Slug = "soc-trang",
            NameWithType = "Thành phố Sóc Trăng",
            NameWithProvince = "Thành phố Sóc Trăng, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 942,
            Name = "Châu Thành",
            Slug = "chau-thanh",
            NameWithType = "Huyện Châu Thành",
            NameWithProvince = "Huyện Châu Thành, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 943,
            Name = "Kế Sách",
            Slug = "ke-sach",
            NameWithType = "Huyện Kế Sách",
            NameWithProvince = "Huyện Kế Sách, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 944,
            Name = "Mỹ Tú",
            Slug = "my-tu",
            NameWithType = "Huyện Mỹ Tú",
            NameWithProvince = "Huyện Mỹ Tú, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 945,
            Name = "Cù Lao Dung",
            Slug = "cu-lao-dung",
            NameWithType = "Huyện Cù Lao Dung",
            NameWithProvince = "Huyện Cù Lao Dung, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 946,
            Name = "Long Phú",
            Slug = "long-phu",
            NameWithType = "Huyện Long Phú",
            NameWithProvince = "Huyện Long Phú, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 947,
            Name = "Mỹ Xuyên",
            Slug = "my-xuyen",
            NameWithType = "Huyện Mỹ Xuyên",
            NameWithProvince = "Huyện Mỹ Xuyên, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 948,
            Name = "Ngã Năm",
            Slug = "nga-nam",
            NameWithType = "Thị xã Ngã Năm",
            NameWithProvince = "Thị xã Ngã Năm, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 949,
            Name = "Thạnh Trị",
            Slug = "thanh-tri",
            NameWithType = "Huyện Thạnh Trị",
            NameWithProvince = "Huyện Thạnh Trị, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 950,
            Name = "Vĩnh Châu",
            Slug = "vinh-chau",
            NameWithType = "Thị xã Vĩnh Châu",
            NameWithProvince = "Thị xã Vĩnh Châu, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 951,
            Name = "Trần Đề",
            Slug = "tran-de",
            NameWithType = "Huyện Trần Đề",
            NameWithProvince = "Huyện Trần Đề, Tỉnh Sóc Trăng",
            ParentCode = 94,
        },
        new District()
        {
            Code = 954,
            Name = "Bạc Liêu",
            Slug = "bac-lieu",
            NameWithType = "Thành phố Bạc Liêu",
            NameWithProvince = "Thành phố Bạc Liêu, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 956,
            Name = "Hồng Dân",
            Slug = "hong-dan",
            NameWithType = "Huyện Hồng Dân",
            NameWithProvince = "Huyện Hồng Dân, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 957,
            Name = "Phước Long",
            Slug = "phuoc-long",
            NameWithType = "Huyện Phước Long",
            NameWithProvince = "Huyện Phước Long, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 958,
            Name = "Vĩnh Lợi",
            Slug = "vinh-loi",
            NameWithType = "Huyện Vĩnh Lợi",
            NameWithProvince = "Huyện Vĩnh Lợi, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 959,
            Name = "Giá Rai",
            Slug = "gia-rai",
            NameWithType = "Thị xã Giá Rai",
            NameWithProvince = "Thị xã Giá Rai, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 960,
            Name = "Đông Hải",
            Slug = "dong-hai",
            NameWithType = "Huyện Đông Hải",
            NameWithProvince = "Huyện Đông Hải, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 961,
            Name = "Hoà Bình",
            Slug = "hoa-binh",
            NameWithType = "Huyện Hoà Bình",
            NameWithProvince = "Huyện Hoà Bình, Tỉnh Bạc Liêu",
            ParentCode = 95,
        },
        new District()
        {
            Code = 964,
            Name = "Cà Mau",
            Slug = "ca-mau",
            NameWithType = "Thành phố Cà Mau",
            NameWithProvince = "Thành phố Cà Mau, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 966,
            Name = "U Minh",
            Slug = "u-minh",
            NameWithType = "Huyện U Minh",
            NameWithProvince = "Huyện U Minh, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 967,
            Name = "Thới Bình",
            Slug = "thoi-binh",
            NameWithType = "Huyện Thới Bình",
            NameWithProvince = "Huyện Thới Bình, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 968,
            Name = "Trần Văn Thời",
            Slug = "tran-van-thoi",
            NameWithType = "Huyện Trần Văn Thời",
            NameWithProvince = "Huyện Trần Văn Thời, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 969,
            Name = "Cái Nước",
            Slug = "cai-nuoc",
            NameWithType = "Huyện Cái Nước",
            NameWithProvince = "Huyện Cái Nước, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 970,
            Name = "Đầm Dơi",
            Slug = "dam-doi",
            NameWithType = "Huyện Đầm Dơi",
            NameWithProvince = "Huyện Đầm Dơi, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 971,
            Name = "Năm Căn",
            Slug = "nam-can",
            NameWithType = "Huyện Năm Căn",
            NameWithProvince = "Huyện Năm Căn, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 972,
            Name = "Phú Tân",
            Slug = "phu-tan",
            NameWithType = "Huyện Phú Tân",
            NameWithProvince = "Huyện Phú Tân, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 973,
            Name = "Ngọc Hiển",
            Slug = "ngoc-hien",
            NameWithType = "Huyện Ngọc Hiển",
            NameWithProvince = "Huyện Ngọc Hiển, Tỉnh Cà Mau",
            ParentCode = 96,
        },
        new District()
        {
            Code = 1,
            Name = "Ba Đình",
            Slug = "ba-dinh",
            NameWithType = "Quận Ba Đình",
            NameWithProvince = "Quận Ba Đình, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 2,
            Name = "Hoàn Kiếm",
            Slug = "hoan-kiem",
            NameWithType = "Quận Hoàn Kiếm",
            NameWithProvince = "Quận Hoàn Kiếm, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 3,
            Name = "Tây Hồ",
            Slug = "tay-ho",
            NameWithType = "Quận Tây Hồ",
            NameWithProvince = "Quận Tây Hồ, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 4,
            Name = "Long Biên",
            Slug = "long-bien",
            NameWithType = "Quận Long Biên",
            NameWithProvince = "Quận Long Biên, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 5,
            Name = "Cầu Giấy",
            Slug = "cau-giay",
            NameWithType = "Quận Cầu Giấy",
            NameWithProvince = "Quận Cầu Giấy, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 6,
            Name = "Đống Đa",
            Slug = "dong-da",
            NameWithType = "Quận Đống Đa",
            NameWithProvince = "Quận Đống Đa, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 7,
            Name = "Hai Bà Trưng",
            Slug = "hai-ba-trung",
            NameWithType = "Quận Hai Bà Trưng",
            NameWithProvince = "Quận Hai Bà Trưng, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 8,
            Name = "Hoàng Mai",
            Slug = "hoang-mai",
            NameWithType = "Quận Hoàng Mai",
            NameWithProvince = "Quận Hoàng Mai, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 9,
            Name = "Thanh Xuân",
            Slug = "thanh-xuan",
            NameWithType = "Quận Thanh Xuân",
            NameWithProvince = "Quận Thanh Xuân, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 16,
            Name = "Sóc Sơn",
            Slug = "soc-son",
            NameWithType = "Huyện Sóc Sơn",
            NameWithProvince = "Huyện Sóc Sơn, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 17,
            Name = "Đông Anh",
            Slug = "dong-anh",
            NameWithType = "Huyện Đông Anh",
            NameWithProvince = "Huyện Đông Anh, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 18,
            Name = "Gia Lâm",
            Slug = "gia-lam",
            NameWithType = "Huyện Gia Lâm",
            NameWithProvince = "Huyện Gia Lâm, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 19,
            Name = "Nam Từ Liêm",
            Slug = "nam-tu-liem",
            NameWithType = "Quận Nam Từ Liêm",
            NameWithProvince = "Quận Nam Từ Liêm, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 20,
            Name = "Thanh Trì",
            Slug = "thanh-tri",
            NameWithType = "Huyện Thanh Trì",
            NameWithProvince = "Huyện Thanh Trì, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 21,
            Name = "Bắc Từ Liêm",
            Slug = "bac-tu-liem",
            NameWithType = "Quận Bắc Từ Liêm",
            NameWithProvince = "Quận Bắc Từ Liêm, Thành phố Hà Nội",
            ParentCode = 1,
        },
        new District()
        {
            Code = 24,
            Name = "Hà Giang",
            Slug = "ha-giang",
            NameWithType = "Thành phố Hà Giang",
            NameWithProvince = "Thành phố Hà Giang, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 26,
            Name = "Đồng Văn",
            Slug = "dong-van",
            NameWithType = "Huyện Đồng Văn",
            NameWithProvince = "Huyện Đồng Văn, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 27,
            Name = "Mèo Vạc",
            Slug = "meo-vac",
            NameWithType = "Huyện Mèo Vạc",
            NameWithProvince = "Huyện Mèo Vạc, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 28,
            Name = "Yên Minh",
            Slug = "yen-minh",
            NameWithType = "Huyện Yên Minh",
            NameWithProvince = "Huyện Yên Minh, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 29,
            Name = "Quản Bạ",
            Slug = "quan-ba",
            NameWithType = "Huyện Quản Bạ",
            NameWithProvince = "Huyện Quản Bạ, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 30,
            Name = "Vị Xuyên",
            Slug = "vi-xuyen",
            NameWithType = "Huyện Vị Xuyên",
            NameWithProvince = "Huyện Vị Xuyên, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 31,
            Name = "Bắc Mê",
            Slug = "bac-me",
            NameWithType = "Huyện Bắc Mê",
            NameWithProvince = "Huyện Bắc Mê, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 32,
            Name = "Hoàng Su Phì",
            Slug = "hoang-su-phi",
            NameWithType = "Huyện Hoàng Su Phì",
            NameWithProvince = "Huyện Hoàng Su Phì, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 33,
            Name = "Xín Mần",
            Slug = "xin-man",
            NameWithType = "Huyện Xín Mần",
            NameWithProvince = "Huyện Xín Mần, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 34,
            Name = "Bắc Quang",
            Slug = "bac-quang",
            NameWithType = "Huyện Bắc Quang",
            NameWithProvince = "Huyện Bắc Quang, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 35,
            Name = "Quang Bình",
            Slug = "quang-binh",
            NameWithType = "Huyện Quang Bình",
            NameWithProvince = "Huyện Quang Bình, Tỉnh Hà Giang",
            ParentCode = 2,
        },
        new District()
        {
            Code = 40,
            Name = "Cao Bằng",
            Slug = "cao-bang",
            NameWithType = "Thành phố Cao Bằng",
            NameWithProvince = "Thành phố Cao Bằng, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 42,
            Name = "Bảo Lâm",
            Slug = "bao-lam",
            NameWithType = "Huyện Bảo Lâm",
            NameWithProvince = "Huyện Bảo Lâm, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 43,
            Name = "Bảo Lạc",
            Slug = "bao-lac",
            NameWithType = "Huyện Bảo Lạc",
            NameWithProvince = "Huyện Bảo Lạc, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 45,
            Name = "Hà Quảng",
            Slug = "ha-quang",
            NameWithType = "Huyện Hà Quảng",
            NameWithProvince = "Huyện Hà Quảng, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 47,
            Name = "Trùng Khánh",
            Slug = "trung-khanh",
            NameWithType = "Huyện Trùng Khánh",
            NameWithProvince = "Huyện Trùng Khánh, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 48,
            Name = "Hạ Lang",
            Slug = "ha-lang",
            NameWithType = "Huyện Hạ Lang",
            NameWithProvince = "Huyện Hạ Lang, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 49,
            Name = "Quảng Hòa",
            Slug = "quang-hoa",
            NameWithType = "Huyện Quảng Hòa",
            NameWithProvince = "Huyện Quảng Hòa, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 51,
            Name = "Hoà An",
            Slug = "hoa-an",
            NameWithType = "Huyện Hoà An",
            NameWithProvince = "Huyện Hoà An, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 52,
            Name = "Nguyên Bình",
            Slug = "nguyen-binh",
            NameWithType = "Huyện Nguyên Bình",
            NameWithProvince = "Huyện Nguyên Bình, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 53,
            Name = "Thạch An",
            Slug = "thach-an",
            NameWithType = "Huyện Thạch An",
            NameWithProvince = "Huyện Thạch An, Tỉnh Cao Bằng",
            ParentCode = 4,
        },
        new District()
        {
            Code = 58,
            Name = "Bắc Kạn",
            Slug = "bac-kan",
            NameWithType = "Thành Phố Bắc Kạn",
            NameWithProvince = "Thành Phố Bắc Kạn, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 60,
            Name = "Pác Nặm",
            Slug = "pac-nam",
            NameWithType = "Huyện Pác Nặm",
            NameWithProvince = "Huyện Pác Nặm, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 61,
            Name = "Ba Bể",
            Slug = "ba-be",
            NameWithType = "Huyện Ba Bể",
            NameWithProvince = "Huyện Ba Bể, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 62,
            Name = "Ngân Sơn",
            Slug = "ngan-son",
            NameWithType = "Huyện Ngân Sơn",
            NameWithProvince = "Huyện Ngân Sơn, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 63,
            Name = "Bạch Thông",
            Slug = "bach-thong",
            NameWithType = "Huyện Bạch Thông",
            NameWithProvince = "Huyện Bạch Thông, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 64,
            Name = "Chợ Đồn",
            Slug = "cho-don",
            NameWithType = "Huyện Chợ Đồn",
            NameWithProvince = "Huyện Chợ Đồn, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 65,
            Name = "Chợ Mới",
            Slug = "cho-moi",
            NameWithType = "Huyện Chợ Mới",
            NameWithProvince = "Huyện Chợ Mới, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 66,
            Name = "Na Rì",
            Slug = "na-ri",
            NameWithType = "Huyện Na Rì",
            NameWithProvince = "Huyện Na Rì, Tỉnh Bắc Kạn",
            ParentCode = 6,
        },
        new District()
        {
            Code = 70,
            Name = "Tuyên Quang",
            Slug = "tuyen-quang",
            NameWithType = "Thành phố Tuyên Quang",
            NameWithProvince = "Thành phố Tuyên Quang, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 71,
            Name = "Lâm Bình",
            Slug = "lam-binh",
            NameWithType = "Huyện Lâm Bình",
            NameWithProvince = "Huyện Lâm Bình, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 72,
            Name = "Na Hang",
            Slug = "na-hang",
            NameWithType = "Huyện Na Hang",
            NameWithProvince = "Huyện Na Hang, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 73,
            Name = "Chiêm Hóa",
            Slug = "chiem-hoa",
            NameWithType = "Huyện Chiêm Hóa",
            NameWithProvince = "Huyện Chiêm Hóa, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 74,
            Name = "Hàm Yên",
            Slug = "ham-yen",
            NameWithType = "Huyện Hàm Yên",
            NameWithProvince = "Huyện Hàm Yên, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 75,
            Name = "Yên Sơn",
            Slug = "yen-son",
            NameWithType = "Huyện Yên Sơn",
            NameWithProvince = "Huyện Yên Sơn, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 76,
            Name = "Sơn Dương",
            Slug = "son-duong",
            NameWithType = "Huyện Sơn Dương",
            NameWithProvince = "Huyện Sơn Dương, Tỉnh Tuyên Quang",
            ParentCode = 8,
        },
        new District()
        {
            Code = 80,
            Name = "Lào Cai",
            Slug = "lao-cai",
            NameWithType = "Thành phố Lào Cai",
            NameWithProvince = "Thành phố Lào Cai, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 82,
            Name = "Bát Xát",
            Slug = "bat-xat",
            NameWithType = "Huyện Bát Xát",
            NameWithProvince = "Huyện Bát Xát, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 83,
            Name = "Mường Khương",
            Slug = "muong-khuong",
            NameWithType = "Huyện Mường Khương",
            NameWithProvince = "Huyện Mường Khương, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 84,
            Name = "Si Ma Cai",
            Slug = "si-ma-cai",
            NameWithType = "Huyện Si Ma Cai",
            NameWithProvince = "Huyện Si Ma Cai, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 85,
            Name = "Bắc Hà",
            Slug = "bac-ha",
            NameWithType = "Huyện Bắc Hà",
            NameWithProvince = "Huyện Bắc Hà, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 86,
            Name = "Bảo Thắng",
            Slug = "bao-thang",
            NameWithType = "Huyện Bảo Thắng",
            NameWithProvince = "Huyện Bảo Thắng, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 87,
            Name = "Bảo Yên",
            Slug = "bao-yen",
            NameWithType = "Huyện Bảo Yên",
            NameWithProvince = "Huyện Bảo Yên, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 88,
            Name = "Sa Pa",
            Slug = "sa-pa",
            NameWithType = "Thị xã Sa Pa",
            NameWithProvince = "Thị xã Sa Pa, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 89,
            Name = "Văn Bàn",
            Slug = "van-ban",
            NameWithType = "Huyện Văn Bàn",
            NameWithProvince = "Huyện Văn Bàn, Tỉnh Lào Cai",
            ParentCode = 10,
        },
        new District()
        {
            Code = 94,
            Name = "Điện Biên Phủ",
            Slug = "dien-bien-phu",
            NameWithType = "Thành phố Điện Biên Phủ",
            NameWithProvince = "Thành phố Điện Biên Phủ, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 95,
            Name = "Mường Lay",
            Slug = "muong-lay",
            NameWithType = "Thị xã Mường Lay",
            NameWithProvince = "Thị xã Mường Lay, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 96,
            Name = "Mường Nhé",
            Slug = "muong-nhe",
            NameWithType = "Huyện Mường Nhé",
            NameWithProvince = "Huyện Mường Nhé, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 97,
            Name = "Mường Chà",
            Slug = "muong-cha",
            NameWithType = "Huyện Mường Chà",
            NameWithProvince = "Huyện Mường Chà, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 98,
            Name = "Tủa Chùa",
            Slug = "tua-chua",
            NameWithType = "Huyện Tủa Chùa",
            NameWithProvince = "Huyện Tủa Chùa, Tỉnh Điện Biên",
            ParentCode = 11,
        },
        new District()
        {
            Code = 99,
            Name = "Tuần Giáo",
            Slug = "tuan-giao",
            NameWithType = "Huyện Tuần Giáo",
            NameWithProvince = "Huyện Tuần Giáo, Tỉnh Điện Biên",
            ParentCode = 11,
        },
    };
}