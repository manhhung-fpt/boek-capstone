import range from "../libs/functions/range";
import { MobileBookProductViewModel } from "../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";
import { StaffCampaignMobilesViewModel } from "../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
export const t = range(1, 20);

export const mockBooks: MobileBookProductViewModel[] =
    [
        {
            "id": "aedacc7b-13eb-4fd3-9d39-4c3b3f6e8ea0",
            "campaignId": 4,
            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
            "title": "Test_Combo_1",
            "description": "Test_Combo_1_Desc_a",
            "saleQuantity": 6,
            "salePrice": 99000,
            "type": 2,
            "typeName": "Sách combo",
            "format": 1,
            "formatName": "Sách giấy",
            "status": 3,
            "statusName": "Ngừng bán do hội sách kết thúc",
            "bookProductItems": [
                {
                    "id": 1,
                    "parentBookProductId": "aedacc7b-13eb-4fd3-9d39-4c3b3f6e8ea0",
                    "bookId": 11,
                    "format": 1,
                    "displayIndex": 1,
                    "withPdf": false,
                    "withAudio": false,
                    "book": {
                        "id": 11,
                        "code": "TB001_Test",
                        "genreId": 48,
                        "publisherId": 2,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "Book1_Test",
                        "translator": "Book1_Translator_Test",
                        "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                        "coverPrice": 50000,
                        "description": "Book1_Description_Test",
                        "language": "VN",
                        "size": "Book1_Size_Test",
                        "releasedYear": 2021,
                        "page": 200,
                        "isSeries": false,
                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": false,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 2,
                            "code": "NXBKD",
                            "name": "NXB Kim Đồng",
                            "email": "cskh_online@nxbkimdong.com.vn",
                            "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                            "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                            "phone": "01900571595"
                        },
                        "bookAuthors": [
                            {
                                "id": 22,
                                "bookId": 11,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            },
                            {
                                "id": 24,
                                "bookId": 11,
                                "authorId": 3,
                                "author": {
                                    "id": 3,
                                    "name": "Carl Gustav",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                },
                {
                    "id": 3,
                    "parentBookProductId": "aedacc7b-13eb-4fd3-9d39-4c3b3f6e8ea0",
                    "bookId": 15,
                    "format": 1,
                    "displayIndex": 2,
                    "withPdf": false,


                    "withAudio": false,


                    "book": {
                        "id": 15,
                        "code": "TB004_Test",
                        "genreId": 48,
                        "publisherId": 2,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "Book4_Test",
                        "translator": "Book_Translator_Test",
                        "imageUrl": "https://salt.tikicdn.com/cache/w1200/ts/product/88/3b/22/5911d66164e96a2d8b3c77bcee059983.jpg",
                        "coverPrice": 18000,
                        "description": "Book4_Description_Test",
                        "language": "VN",
                        "size": "Book4_Size_Test",
                        "releasedYear": 2022,
                        "page": 200,
                        "isSeries": false,




                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": false,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 2,
                            "code": "NXBKD",
                            "name": "NXB Kim Đồng",
                            "email": "cskh_online@nxbkimdong.com.vn",
                            "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                            "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                            "phone": "01900571595"
                        },
                        "bookAuthors": [
                            {
                                "id": 27,
                                "bookId": 15,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            },
                            {
                                "id": 28,
                                "bookId": 15,
                                "authorId": 2,
                                "author": {
                                    "id": 2,
                                    "name": "Sven Carlsson, Jonas",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                }
            ],

        },
        {
            "id": "b5c763a8-2821-4359-9dfc-550d09c7a1ff",
            "bookId": 11,
            "campaignId": 4,
            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
            "title": "Book1_Test",
            "description": "Book1_Description_Test",
            "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
            "saleQuantity": 5,
            "discount": 0,
            "salePrice": 50000,
            "type": 1,
            "typeName": "Sách lẻ",
            "format": 1,
            "formatName": "Sách giấy",
            "withPdf": false,


            "withAudio": false,


            "status": 3,
            "statusName": "Ngừng bán do hội sách kết thúc",
            "book": {
                "id": 11,
                "code": "TB001_Test",
                "genreId": 48,
                "publisherId": 2,
                "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                "isbn10": "",
                "isbn13": "",
                "name": "Book1_Test",
                "translator": "Book1_Translator_Test",
                "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                "coverPrice": 50000,
                "description": "Book1_Description_Test",
                "language": "VN",
                "size": "Book1_Size_Test",
                "releasedYear": 2021,
                "page": 200,
                "isSeries": false,




                "status": 1,
                "statusName": "Phát hành",
                "fullPdfAndAudio": false,
                "onlyPdf": false,
                "onlyAudio": false,
                "genre": {
                    "id": 48,
                    "parentId": 1,
                    "name": "Tiểu thuyết",
                    "displayIndex": 8,
                    "status": true,
                    "statusName": "Hoạt động"
                },
                "issuer": {
                    "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                    "user": {
                        "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "code": "I307304132",
                        "name": "BIZBOOK",
                        "email": "ngcphungnguyn@gmail.com",
                        "address": "",
                        "phone": "0123456789",
                        "roleName": "Issuer",
                        "role": 2,
                        "status": true,
                        "statusName": "Hoạt động",
                        "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                    }
                },
                "publisher": {
                    "id": 2,
                    "code": "NXBKD",
                    "name": "NXB Kim Đồng",
                    "email": "cskh_online@nxbkimdong.com.vn",
                    "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                    "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                    "phone": "01900571595"
                },
                "bookAuthors": [
                    {
                        "id": 22,
                        "bookId": 11,
                        "authorId": 1,
                        "author": {
                            "id": 1,
                            "name": "Nguyễn Nhật Ánh",


                        }
                    },
                    {
                        "id": 24,
                        "bookId": 11,
                        "authorId": 3,
                        "author": {
                            "id": 3,
                            "name": "Carl Gustav",


                        }
                    }
                ],
                "bookItems": []
            },
            "bookProductItems": []
        },
        {
            "id": "16ba2832-e89f-40b7-8897-576a7262ffdc",
            "campaignId": 4,
            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
            "title": "Test_Combo_2_a",
            "description": "Test_Combo_2_Desc_a",
            "imageUrl": "https://cf.shopee.vn/file/sg-11134201-22100-1gnjdg9okxiv61",
            "saleQuantity": 5,

            "salePrice": 88000,
            "type": 2,
            "typeName": "Sách combo",
            "format": 1,
            "formatName": "Sách giấy",
            "status": 3,
            "statusName": "Ngừng bán do hội sách kết thúc",

            "bookProductItems": [
                {
                    "id": 33,
                    "parentBookProductId": "16ba2832-e89f-40b7-8897-576a7262ffdc",
                    "bookId": 20,
                    "format": 1,
                    "displayIndex": 2,
                    "withPdf": false,


                    "withAudio": false,


                    "book": {
                        "id": 20,
                        "code": "TB005_Test",
                        "genreId": 48,
                        "publisherId": 4,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "TB005_Test_Name",
                        "translator": "TB005_Test_Translator",
                        "imageUrl": "https://dictionary.cambridge.org/vi/images/thumb/book_noun_004_0453.jpg?version=5.0.286",
                        "coverPrice": 50000,
                        "description": "TB005_Test_Desc",
                        "language": "VN",
                        "size": "TB005_Test_Size",
                        "releasedYear": 2022,
                        "page": 100,
                        "isSeries": false,
                        "pdfExtraPrice": 10000,
                        "pdfTrialUrl": "https://dictionary.cambridge.org/vi/images/thumb/book_noun_004_0453.jpg?version=5.0.286",


                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": true,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 4,
                            "code": "NXBTre",
                            "name": "NXB Trẻ",
                            "email": "hopthubandoc@nxbtre.com.vn ",
                            "address": "161B Lý Chính Thắng, phường Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh",
                            "imageUrl": "https://www.nxbtre.com.vn/css/skin/logo.png",
                            "phone": "02839317849"
                        },
                        "bookAuthors": [
                            {
                                "id": 29,
                                "bookId": 20,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                },
                {
                    "id": 34,
                    "parentBookProductId": "16ba2832-e89f-40b7-8897-576a7262ffdc",
                    "bookId": 11,
                    "format": 1,
                    "displayIndex": 1,
                    "withPdf": false,


                    "withAudio": false,


                    "book": {
                        "id": 11,
                        "code": "TB001_Test",
                        "genreId": 48,
                        "publisherId": 2,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "Book1_Test",
                        "translator": "Book1_Translator_Test",
                        "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                        "coverPrice": 50000,
                        "description": "Book1_Description_Test",
                        "language": "VN",
                        "size": "Book1_Size_Test",
                        "releasedYear": 2021,
                        "page": 200,
                        "isSeries": false,




                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": false,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 2,
                            "code": "NXBKD",
                            "name": "NXB Kim Đồng",
                            "email": "cskh_online@nxbkimdong.com.vn",
                            "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                            "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                            "phone": "01900571595"
                        },
                        "bookAuthors": [
                            {
                                "id": 22,
                                "bookId": 11,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            },
                            {
                                "id": 24,
                                "bookId": 11,
                                "authorId": 3,
                                "author": {
                                    "id": 3,
                                    "name": "Carl Gustav",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                }
            ],
        },
        {
            "id": "faed02f0-7595-43b0-96da-8fb10d938794",
            "bookId": 19,
            "campaignId": 4,
            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
            "title": "Book3_Test",
            "description": "Book3_Description_Test",
            "imageUrl": "https://gamek.mediacdn.vn/133514250583805952/2020/5/18/photo-1-15897700009731242974248.jpg",
            "saleQuantity": 5,
            "discount": 5,
            "salePrice": 0,
            "type": 3,
            "typeName": "Sách series",
            "format": 1,
            "formatName": "Sách giấy",
            "status": 3,
            "statusName": "Ngừng bán do hội sách kết thúc",
            "book": {
                "id": 19,
                "code": "TB003_Test",
                "genreId": 48,
                "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                "isbn10": "",
                "isbn13": "",
                "name": "Book3_Test",
                "imageUrl": "https://gamek.mediacdn.vn/133514250583805952/2020/5/18/photo-1-15897700009731242974248.jpg",
                "coverPrice": 88000,
                "description": "Book3_Description_Test",
                "releasedYear": 2022,
                "isSeries": true,
                "status": 1,
                "statusName": "Phát hành",
                "fullPdfAndAudio": false,
                "onlyPdf": false,
                "onlyAudio": false,
                "genre": {
                    "id": 48,
                    "parentId": 1,
                    "name": "Tiểu thuyết",
                    "displayIndex": 8,
                    "status": true,
                    "statusName": "Hoạt động"
                },
                "issuer": {
                    "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                    "user": {
                        "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "code": "I307304132",
                        "name": "BIZBOOK",
                        "email": "ngcphungnguyn@gmail.com",
                        "address": "",
                        "phone": "0123456789",
                        "roleName": "Issuer",
                        "role": 2,
                        "status": true,
                        "statusName": "Hoạt động",
                        "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                    }
                },
                "bookAuthors": [],
                "bookItems": [
                    {
                        "id": 1,
                        "parentBookId": 19,
                        "bookId": 11,
                        "displayIndex": 1,
                        "book": {
                            "id": 11,
                            "code": "TB001_Test",
                            "genreId": 48,
                            "publisherId": 2,
                            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                            "isbn10": "",
                            "isbn13": "",
                            "name": "Book1_Test",
                            "translator": "Book1_Translator_Test",
                            "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                            "coverPrice": 50000,
                            "description": "Book1_Description_Test",
                            "language": "VN",
                            "size": "Book1_Size_Test",
                            "releasedYear": 2021,
                            "page": 200,
                            "isSeries": false,




                            "status": 1,
                            "statusName": "Phát hành",
                            "fullPdfAndAudio": false,
                            "onlyPdf": false,
                            "onlyAudio": false
                        }
                    },
                    {
                        "id": 3,
                        "parentBookId": 19,
                        "bookId": 15,
                        "displayIndex": 2,
                        "book": {
                            "id": 15,
                            "code": "TB004_Test",
                            "genreId": 48,
                            "publisherId": 2,
                            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                            "isbn10": "",
                            "isbn13": "",
                            "name": "Book4_Test",
                            "translator": "Book_Translator_Test",
                            "imageUrl": "https://salt.tikicdn.com/cache/w1200/ts/product/88/3b/22/5911d66164e96a2d8b3c77bcee059983.jpg",
                            "coverPrice": 18000,
                            "description": "Book4_Description_Test",
                            "language": "VN",
                            "size": "Book4_Size_Test",
                            "releasedYear": 2022,
                            "page": 200,
                            "isSeries": false,




                            "status": 1,
                            "statusName": "Phát hành",
                            "fullPdfAndAudio": false,
                            "onlyPdf": false,
                            "onlyAudio": false
                        }
                    }
                ]
            },
            "bookProductItems": [
                {
                    "id": 30,
                    "parentBookProductId": "faed02f0-7595-43b0-96da-8fb10d938794",
                    "bookId": 11,
                    "format": 1,
                    "displayIndex": 1,
                    "withPdf": false,


                    "withAudio": false,


                    "book": {
                        "id": 11,
                        "code": "TB001_Test",
                        "genreId": 48,
                        "publisherId": 2,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "Book1_Test",
                        "translator": "Book1_Translator_Test",
                        "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                        "coverPrice": 50000,
                        "description": "Book1_Description_Test",
                        "language": "VN",
                        "size": "Book1_Size_Test",
                        "releasedYear": 2021,
                        "page": 200,
                        "isSeries": false,




                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": false,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 2,
                            "code": "NXBKD",
                            "name": "NXB Kim Đồng",
                            "email": "cskh_online@nxbkimdong.com.vn",
                            "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                            "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                            "phone": "01900571595"
                        },
                        "bookAuthors": [
                            {
                                "id": 22,
                                "bookId": 11,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            },
                            {
                                "id": 24,
                                "bookId": 11,
                                "authorId": 3,
                                "author": {
                                    "id": 3,
                                    "name": "Carl Gustav",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                },
                {
                    "id": 31,
                    "parentBookProductId": "faed02f0-7595-43b0-96da-8fb10d938794",
                    "bookId": 15,
                    "format": 1,
                    "displayIndex": 2,
                    "withPdf": false,


                    "withAudio": false,


                    "book": {
                        "id": 15,
                        "code": "TB004_Test",
                        "genreId": 48,
                        "publisherId": 2,
                        "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "isbn10": "",
                        "isbn13": "",
                        "name": "Book4_Test",
                        "translator": "Book_Translator_Test",
                        "imageUrl": "https://salt.tikicdn.com/cache/w1200/ts/product/88/3b/22/5911d66164e96a2d8b3c77bcee059983.jpg",
                        "coverPrice": 18000,
                        "description": "Book4_Description_Test",
                        "language": "VN",
                        "size": "Book4_Size_Test",
                        "releasedYear": 2022,
                        "page": 200,
                        "isSeries": false,




                        "status": 1,
                        "statusName": "Phát hành",
                        "fullPdfAndAudio": false,
                        "onlyPdf": false,
                        "onlyAudio": false,
                        "genre": {
                            "id": 48,
                            "parentId": 1,
                            "name": "Tiểu thuyết",
                            "displayIndex": 8,
                            "status": true,
                            "statusName": "Hoạt động"
                        },
                        "issuer": {
                            "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                            "user": {
                                "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                                "code": "I307304132",
                                "name": "BIZBOOK",
                                "email": "ngcphungnguyn@gmail.com",
                                "address": "",
                                "phone": "0123456789",
                                "roleName": "Issuer",
                                "role": 2,
                                "status": true,
                                "statusName": "Hoạt động",
                                "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                            }
                        },
                        "publisher": {
                            "id": 2,
                            "code": "NXBKD",
                            "name": "NXB Kim Đồng",
                            "email": "cskh_online@nxbkimdong.com.vn",
                            "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                            "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                            "phone": "01900571595"
                        },
                        "bookAuthors": [
                            {
                                "id": 27,
                                "bookId": 15,
                                "authorId": 1,
                                "author": {
                                    "id": 1,
                                    "name": "Nguyễn Nhật Ánh",


                                }
                            },
                            {
                                "id": 28,
                                "bookId": 15,
                                "authorId": 2,
                                "author": {
                                    "id": 2,
                                    "name": "Sven Carlsson, Jonas",


                                }
                            }
                        ],
                        "bookItems": []
                    }
                }
            ],
        },
        {
            "id": "bd79de22-405b-468f-992f-9f82c1097252",
            "bookId": 11,
            "campaignId": 5,
            "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
            "title": "Book1_Test",
            "description": "Book1_Description_Test",
            "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
            "saleQuantity": 5,
            "discount": 0,
            "salePrice": 50000,
            "type": 1,
            "typeName": "Sách lẻ",
            "format": 1,
            "formatName": "Sách giấy",
            "withPdf": false,


            "withAudio": false,


            "status": 1,
            "statusName": "Đang bán",
            "book": {
                "id": 11,
                "code": "TB001_Test",
                "genreId": 48,
                "publisherId": 2,
                "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                "isbn10": "",
                "isbn13": "",
                "name": "Book1_Test",
                "translator": "Book1_Translator_Test",
                "imageUrl": "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg",
                "coverPrice": 50000,
                "description": "Book1_Description_Test",
                "language": "VN",
                "size": "Book1_Size_Test",
                "releasedYear": 2021,
                "page": 200,
                "isSeries": false,




                "status": 1,
                "statusName": "Phát hành",
                "fullPdfAndAudio": false,
                "onlyPdf": false,
                "onlyAudio": false,
                "genre": {
                    "id": 48,
                    "parentId": 1,
                    "name": "Tiểu thuyết",
                    "displayIndex": 8,
                    "status": true,
                    "statusName": "Hoạt động"
                },
                "issuer": {
                    "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                    "user": {
                        "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "code": "I307304132",
                        "name": "BIZBOOK",
                        "email": "ngcphungnguyn@gmail.com",
                        "address": "",
                        "phone": "0123456789",
                        "roleName": "Issuer",
                        "role": 2,
                        "status": true,
                        "statusName": "Hoạt động",
                        "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                    }
                },
                "publisher": {
                    "id": 2,
                    "code": "NXBKD",
                    "name": "NXB Kim Đồng",
                    "email": "cskh_online@nxbkimdong.com.vn",
                    "address": "Số 55 Quang Trung, Nguyễn Du, Hai Bà Trưng, Hà Nội",
                    "imageUrl": "https://theme.hstatic.net/200000343865/1000938429/14/logo.png?v=180",
                    "phone": "01900571595"
                },
                "bookAuthors": [
                    {
                        "id": 22,
                        "bookId": 11,
                        "authorId": 1,
                        "author": {
                            "id": 1,
                            "name": "Nguyễn Nhật Ánh",


                        }
                    },
                    {
                        "id": 24,
                        "bookId": 11,
                        "authorId": 3,
                        "author": {
                            "id": 3,
                            "name": "Carl Gustav",


                        }
                    }
                ],
                "bookItems": []
            },
            "bookProductItems": []
        }
    ]


export const mockCampaigns = [
    {
        "id": 1,
        "code": "cb6a648f-8f5a-47ce-abe5-a4b56119d9a0",
        "name": "Hội sách xuyên Việt - Lan tỏa tri thức",
        "description": "Từ ngày 14 đến 18-4 tại sân vận động Hoa Lư (quận 1, TP.HCM) diễn ra hội sách với sự tham gia của hơn 30 thương hiệu sách như AZ Việt Nam, Skybooks, Bloom Books… Các đầu sách được giảm từ 30 - 80%.",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 3,
        "privacy": 1,
        "address": "Sân vận động Hoa Lư (quận 1, TP.HCM)",
        "offlineStatus": 1,
        "startOfflineDate": "2023-01-10T00:00:00",
        "endOfflineDate": "2023-01-15T00:00:00",
        "onlineStatus": 1,
        "startOnlineDate": "2023-01-10T00:00:00",
        "endOnlineDate": "2023-01-13T00:00:00",
        "createdDate": "2022-12-26T00:00:00",
        "updatedDate": null,
        "statusOfflineName": "Chưa bắt đầu",
        "statusOnlineName": "Chưa bắt đầu",
        "formatName": "Trực tiếp và trực tuyến",
        "privacyName": "Công khai",
        "campaignCommissions": [],
        "campaignOrganizations": [],
        "campaignGroups": [],
        "participants": []
    },
    {
        "id": 2,
        "code": "69576c65-6159-4fa0-b854-f5535e5e8e3b",
        "name": "campaign_test_1",
        "description": "campaign_test_desc_1",
        "imageUrl": "https://www.athlosjp.org/wp-content/uploads/2019/11/20191113_155727-Hero.jpg",
        "format": 3,
        "privacy": 1,
        "address": "Hồ Chí Minh",
        "offlineStatus": 4,
        "startOfflineDate": "2023-01-17T00:00:00",
        "endOfflineDate": "2023-01-20T00:00:00",
        "onlineStatus": 4,
        "startOnlineDate": "2023-01-17T00:00:00",
        "endOnlineDate": "2023-01-20T00:00:00",
        "createdDate": "2023-01-04T12:12:56.783",
        "updatedDate": "2023-01-04T16:20:56.147",
        "statusOfflineName": "Hủy hội sách",
        "statusOnlineName": "Hủy hội sách",
        "formatName": "Trực tiếp và trực tuyến",
        "privacyName": "Công khai",
        "campaignCommissions": [
            {
                "id": 1,
                "campaignId": 2,
                "genreId": 1,
                "commission": 10,
                "genre": {
                    "id": 1,
                    "parentId": null,
                    "name": "Văn học",
                    "displayIndex": 1,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            },
            {
                "id": 9,
                "campaignId": 2,
                "genreId": 2,
                "commission": 5,
                "genre": {
                    "id": 2,
                    "parentId": null,
                    "name": "Kinh tế",
                    "displayIndex": 2,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            }
        ],
        "campaignOrganizations": [],
        "campaignGroups": [],
        "participants": []
    },
    {
        "id": 3,
        "code": "b1064e69-ccee-478a-b983-a50e695bc94d",
        "name": "campaign_test_1",
        "description": "campaign_test_desc_1",
        "imageUrl": "https://www.athlosjp.org/wp-content/uploads/2019/11/20191113_155727-Hero.jpg",
        "format": 3,
        "privacy": 1,
        "address": "Hồ Chí Minh",
        "offlineStatus": 1,
        "startOfflineDate": "2023-01-17T00:00:00",
        "endOfflineDate": "2023-01-20T00:00:00",
        "onlineStatus": 1,
        "startOnlineDate": "2023-01-17T00:00:00",
        "endOnlineDate": "2023-01-20T00:00:00",
        "createdDate": "2023-01-05T17:37:23.263",
        "updatedDate": "2023-01-05T17:56:18.41",
        "statusOfflineName": "Chưa bắt đầu",
        "statusOnlineName": "Chưa bắt đầu",
        "formatName": "Trực tiếp và trực tuyến",
        "privacyName": "Công khai",
        "campaignCommissions": [
            {
                "id": 10,
                "campaignId": 3,
                "genreId": 1,
                "commission": 10,
                "genre": {
                    "id": 1,
                    "parentId": null,
                    "name": "Văn học",
                    "displayIndex": 1,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            },
            {
                "id": 11,
                "campaignId": 3,
                "genreId": 2,
                "commission": 5,
                "genre": {
                    "id": 2,
                    "parentId": null,
                    "name": "Kinh tế",
                    "displayIndex": 2,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            }
        ],
        "campaignOrganizations": [
            {
                "id": 4,
                "organizationId": 1,
                "campaignId": 3,
                "organization": {
                    "id": 1,
                    "name": "FPT",
                    "address": "Quận 9",
                    "phone": "0101456789",
                    "imageUrl": "https://i.chungta.vn/2017/12/22/LogoFPT-2017-copy-3042-1513928399.jpg"
                }
            }
        ],
        "campaignGroups": [],
        "participants": []
    },
    {
        "id": 4,
        "code": "1d54b7a9-dc93-46a6-9cfb-fb695a723b62",
        "name": "campaign_test_3",
        "description": "campaign_test_desc_3",
        "imageUrl": "https://www.athlosjp.org/wp-content/uploads/2019/11/20191113_155727-Hero.jpg",
        "format": 3,
        "privacy": 1,
        "address": "Hồ Chí Minh",
        "offlineStatus": 1,
        "startOfflineDate": "2023-01-17T00:00:00",
        "endOfflineDate": "2023-01-20T00:00:00",
        "onlineStatus": 1,
        "startOnlineDate": "2023-01-17T00:00:00",
        "endOnlineDate": "2023-01-20T00:00:00",
        "createdDate": "2023-01-05T23:23:11.963",
        "updatedDate": "2023-01-05T23:39:00.583",
        "statusOfflineName": "Chưa bắt đầu",
        "statusOnlineName": "Chưa bắt đầu",
        "formatName": "Trực tiếp và trực tuyến",
        "privacyName": "Công khai",
        "campaignCommissions": [
            {
                "id": 12,
                "campaignId": 4,
                "genreId": 1,
                "commission": 10,
                "genre": {
                    "id": 1,
                    "parentId": null,
                    "name": "Văn học",
                    "displayIndex": 1,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            },
            {
                "id": 13,
                "campaignId": 4,
                "genreId": 2,
                "commission": 5,
                "genre": {
                    "id": 2,
                    "parentId": null,
                    "name": "Kinh tế",
                    "displayIndex": 2,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            }
        ],
        "campaignOrganizations": [
            {
                "id": 6,
                "organizationId": 1,
                "campaignId": 4,
                "organization": {
                    "id": 1,
                    "name": "FPT",
                    "address": "Quận 9",
                    "phone": "0101456789",
                    "imageUrl": "https://i.chungta.vn/2017/12/22/LogoFPT-2017-copy-3042-1513928399.jpg"
                }
            }
        ],
        "campaignGroups": [
            {
                "id": 2,
                "campaignId": 4,
                "groupId": 1,
                "group": {
                    "id": 1,
                    "name": "Công nghệ",
                    "description": "Nhóm về đề tài công nghệ.",
                    "status": true,
                    "statusName": "Hoạt động"
                }
            }
        ],
        "participants": [
            {
                "id": 3,
                "campaignId": 4,
                "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                "createdDate": "2023-01-10T21:47:19.28",
                "updatedDate": "2023-01-10T21:50:28.053",
                "status": 3,
                "statusName": "Chấp nhận duyệt",
                "issuer": {
                    "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                    "user": {
                        "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "code": "I307304132",
                        "name": "BIZBOOK",
                        "email": "ngcphungnguyn@gmail.com",
                        "address": "",
                        "phone": "0123456789",
                        "roleName": "Issuer",
                        "role": 2,
                        "status": true,
                        "statusName": "Hoạt động",
                        "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                    }
                }
            }
        ]
    },
    {
        "id": 5,
        "code": "0079fbb2-008b-4f3f-b18e-ee1e01e1555c",
        "name": "Campaign_Test_05",
        "description": "Campaign_Test_05_Desc",
        "imageUrl": "https://www.athlosjp.org/wp-content/uploads/2019/11/20191113_155727-Hero.jpg",
        "format": 1,
        "privacy": 1,
        "address": "Hồ Chí Minh",
        "offlineStatus": 1,
        "startOfflineDate": "2023-01-30T00:00:00",
        "endOfflineDate": "2023-02-28T00:00:00",
        "onlineStatus": null,
        "startOnlineDate": null,
        "endOnlineDate": null,
        "createdDate": "2023-01-19T16:46:47.45",
        "updatedDate": null,
        "statusOfflineName": "Chưa bắt đầu",
        "statusOnlineName": null,
        "formatName": "Trực tiếp",
        "privacyName": "Công khai",
        "campaignCommissions": [
            {
                "id": 14,
                "campaignId": 5,
                "genreId": 48,
                "commission": 10,
                "genre": {
                    "id": 48,
                    "parentId": 1,
                    "name": "Tiểu thuyết",
                    "displayIndex": 8,
                    "status": true,
                    "statusName": "Hoạt động"
                }
            }
        ],
        "campaignOrganizations": [
            {
                "id": 7,
                "organizationId": 1,
                "campaignId": 5,
                "organization": {
                    "id": 1,
                    "name": "FPT",
                    "address": "Quận 9",
                    "phone": "0101456789",
                    "imageUrl": "https://i.chungta.vn/2017/12/22/LogoFPT-2017-copy-3042-1513928399.jpg"
                }
            },
            {
                "id": 8,
                "organizationId": 2,
                "campaignId": 5,
                "organization": {
                    "id": 2,
                    "name": "KMS Technology",
                    "address": "2 Tản Viên, Phường 2, Tân Bình, Thành phố Hồ Chí Minh",
                    "phone": "028 3811 9977",
                    "imageUrl": "https://res.cloudinary.com/crunchbase-production/image/upload/c_lpad,f_auto,q_auto:eco,dpr_1/zzp8btlqdchioqnngn9c"
                }
            }
        ],
        "campaignGroups": [],
        "participants": [
            {
                "id": 6,
                "campaignId": 5,
                "issuerId": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                "createdDate": "2023-01-19T16:56:23.71",
                "updatedDate": "2023-01-19T17:01:22.807",
                "status": 5,
                "statusName": "Chấp nhận lời mời",
                "issuer": {
                    "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",

                    "user": {
                        "id": "90bbc35c-b655-4ae1-9bf5-7c3c4aba2ee0",
                        "code": "I307304132",
                        "name": "BIZBOOK",
                        "email": "ngcphungnguyn@gmail.com",
                        "address": "",
                        "phone": "0123456789",
                        "roleName": "Issuer",
                        "role": 2,
                        "status": true,
                        "statusName": "Hoạt động",
                        "imageUrl": "https://lh3.googleusercontent.com/a/ALm5wu0FH3PWgtOhqCmxcKHFIWcYY-6j_C9f7nq9oBcA=s96-c"
                    }
                }
            }
        ]
    }
];

export const mockStaffCampaigns: StaffCampaignMobilesViewModel[] = [
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
    {
        "id": 0,
        "code": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "name": "string",
        "description": "string",
        "imageUrl": "https://static.ladipage.net/5dcdfbb918ed7f6153f62d0b/banner-1200x628px-20210414080426.jpg",
        "format": 0,
        "privacy": 0,
        "address": "string",
        "offlineStatus": 0,
        "startOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOfflineDate": new Date("2023-01-27T14:39:43.790Z"),
        "onlineStatus": 0,
        "startOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "endOnlineDate": new Date("2023-01-27T14:39:43.790Z"),
        "createdDate": new Date("2023-01-27T14:39:43.790Z"),
        "updatedDate": new Date("2023-01-27T14:39:43.790Z"),
        "statusOfflineName": "string",
        "statusOnlineName": "string",
        "formatName": "string",
        "privacyName": "string",
        "issuers": [
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "description": "string",
                "user": {
                    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "code": "string",
                    "name": "string",
                    "email": "user@example.com",
                    "address": "string",
                    "phone": "string",
                    "roleName": "string",
                    "role": 0,
                    "status": true,
                    "statusName": "string",
                    "imageUrl": "string"
                }
            }
        ]
    },
]
export const mockIssuer = mockBooks.map(({ book }) => book?.issuer).slice(0, 6);
