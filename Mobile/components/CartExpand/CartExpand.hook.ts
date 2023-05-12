import { useEffect, useRef, useState } from "react";
import NumericInput from "react-native-numeric-input";
import useAppContext from "../../context/Context";
import { CampaignInCart } from "../../objects/models/CampaignInCart";
import { ProductInCart } from "../../objects/models/ProductInCart";
import { BasicCampaignViewModel } from "../../objects/viewmodels/Campaigns/BasicCampaignViewModel";

export default function useCartExpandComponent() {
    const { cart, setCart, setTotalProductQuantity } = useAppContext();
    const onQuantityChange = (campaignId: number, issuerId: string, productId: string, quantity: number) => {

        const cloneCart: CampaignInCart[] = Object.create(cart);
        const issuer = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId);
        if (issuer) {
            const product = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId)?.productsInCart.find(p => p.id == productId);
            if (product) {
                const addQuantity = quantity - product.quantity;
                product.quantity = quantity;
                setTotalProductQuantity(pre => pre + addQuantity);

            }
        }
        setCart(cloneCart);
    }
    const onPdfChecked = (campaignId: number, issuerId: string, productId: string) => {
        const cloneCart: CampaignInCart[] = Object.create(cart);
        const issuer = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId);
        if (issuer) {
            const product = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId)?.productsInCart.find(p => p.id == productId);
            if (product) {
                product.pdfChecked = !product.pdfChecked;
            }
        }
        setCart(cloneCart);
    }
    const onAudioChecked = (campaignId: number, issuerId: string, productId: string) => {
        const cloneCart: CampaignInCart[] = Object.create(cart);
        const issuer = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId);
        if (issuer) {
            const product = cloneCart.find(c => c.campaign.id == campaignId)?.issuersInCart.find(i => i.issuer.id == issuerId)?.productsInCart.find(p => p.id == productId);
            if (product) {
                product.audioChecked = !product.audioChecked;
            }
        }
        setCart(cloneCart);
    }
    return {
        event: {
            onQuantityChange,
            onPdfChecked,
            onAudioChecked
        }
    }
}