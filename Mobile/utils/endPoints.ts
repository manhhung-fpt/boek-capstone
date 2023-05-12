const endPont =
{
    admin: {
        authors: "/api/admin/authors",
        genres: "/api/admin/genres",
        publishers: "/api/admin/publishers",
        users: "/api/admin/users",

    },
    issuer:
    {
        authors: "/api/issuer/authors",
        books: {
            index: "/api/issuer/books",
            bookCombo: "/api/issuer/books/book-combo",
            bookSeries: "/api/issuer/books/book-series",
            enableBook: "/api/issuer/books/enabled-book",
            disabledBook: "/api/issuer/books/disabled-book"
        },
        formats: "/api/issuers/formats"
    },
    staff: {
        campaigns: "/api/staff/campaigns",
        orders: {
            index: "/api/staff/orders",
            received: "/api/staff/orders/received"
        }
    },
    users: {
        index: "/api/users",
        me: "/api/users/me",
        staff: "/api/users/staff",
        customer: "/api/users/customer",
        issuer: "/api/users/issuer"
    },
    public: {
        login: "/api/login",
        author: "/api/authors",
        books:
        {
            customer: {
                products: "/api/books/customer/products"
            },
            index: "/api/books",
            genre: "/api/books/genre",
            mobile: {
                products: {
                    index: "/api/books/mobile/products",
                    unhierarchicalBookProducts: "/api/books/mobile/products/unhierarchical-book-products"
                }
            }
        },
        campaigns:
        {
            index: "/api/campaigns",
            customer: {
                index: "/api/campaigns/customer",
                homePage: "/api/campaigns/customer/home-page"
            }
        },
        formats: "/api/formats",
        genres: "/api/genres",
        publishers: "/api/publishers",
        languages: "/api/languages",
        provinces: "/api/provinces",
        districts: "/api/districts",
        groups: {
            index: "/api/groups",
            customer: "/api/groups/customer"
        },
        organizations: {
            index: "/api/organizations",
            customer: "/api/organizations/customer"
        }
    },
    orders: {
        index: "/api/orders",
        zaloPay: "/api/orders/zalopay",
        customer: {
            pickUp: "/api/orders/customer/pick-up",
            shipping: "/api/orders/customer/shipping"

        },
        calculation: {
            pickUp: "/api/orders/calculation/pick-up",
            shipping: "/api/orders/calculation/shipping"
        }
    },
    levels: {
        index: "/api/levels"
    },
    lead: {
        districts: "/districts",
        ward: "/wards"
    }
}
export default endPont;