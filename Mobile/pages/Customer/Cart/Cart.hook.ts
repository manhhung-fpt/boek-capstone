import { useState } from "react";
import useAppContext from "../../../context/Context";
import { OrderPayment } from "../../../objects/enums/OrderPayment";
import { CampaignInCart } from "../../../objects/models/CampaignInCart";
import { CreateOrderDetailsRequestModel } from "../../../objects/requests/OrderDetails/CreateOrderDetailsRequestModel";
import { CreateCustomerPickUpOrderRequestModel } from "../../../objects/requests/Orders/CreateCustomerPickUpOrderRequestModel";
import { MobileBookProductViewModel } from "../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";

export default function useCartPage() {
    const { cart, totalProductQuantity, setTotalProductQuantity, setCart } = useAppContext();
    const [seletedCampaignId, setSeletedCampaignId] = useState(0);
    const getTotalPrice = () => {
        let totalPrice = 0;
        cart.find(c => seletedCampaignId == c.campaign.id as number)?.issuersInCart.map(i => i.productsInCart.map(product => {
            let productPrice = product.salePrice;
            if (product.audioChecked) {
                productPrice += product.audioExtraPrice;
            }
            if (product.pdfChecked) {
                productPrice += product.pdfExtraPrice;
            }
            productPrice *= product.quantity;
            totalPrice += productPrice;
        }));
        return totalPrice;
    }


    return {
        getTotalPrice,
        input: {
            seletedCampaignId: {
                value: seletedCampaignId,
                set: setSeletedCampaignId
            }
        }
    }
}