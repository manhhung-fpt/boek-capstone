import React from 'react'
import { View, Image, TouchableOpacity, Text, Dimensions } from 'react-native'
import { paletteGray, paletteGrayLight, paletteGrayShade3, paletteGrayShade5, paletteGrayTint6, paletteGrayTint7, paletteGrayTint8, paletteGreenShade1, palettePink, paletteRed, primaryColor, primaryTint2, primaryTint7 } from '../../utils/color';;
import useRouter from '../../libs/hook/useRouter';
import { MobileBookProductViewModel } from '../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
import formatNumber from '../../libs/functions/formatNumber';
import Shadow from '../Shadow/Shadow';
import { BookProductStatus } from '../../objects/enums/BookProductStatus'
import CampaignStatus from '../../objects/enums/CampaignStatus';
interface BookCardProps {
  book: MobileBookProductViewModel;
}
function BookCard({ book }: BookCardProps) {
  const getStatusBackgrundColor = (statusId: number) => {
    if (
      statusId == BookProductStatus.NotSale ||
      statusId == BookProductStatus.NotSaleDueCancelledCampaign ||
      statusId == BookProductStatus.NotSaleDueEndDate ||
      statusId == BookProductStatus.NotSaleDuePostponedCampaign ||
      statusId == BookProductStatus.Rejected) {
      return paletteGrayShade5;
    }
    if (
      statusId == BookProductStatus.OutOfStock ||
      statusId == BookProductStatus.Pending ||
      statusId == BookProductStatus.Unreleased) {
      return paletteGrayShade5;
    }
    if (statusId == BookProductStatus.Sale) {
      return primaryTint2;
    }
    return "blue";
  }
  const getStatusText = () => {
    if (book?.campaign.status != CampaignStatus.start) {
      return CampaignStatus.toString(book.campaign.status as number);
    }
    if (book.withLevel && !book.allowPurchasingByLevel) {
      return "Không đủ cấp";
    }
    if (book.status != BookProductStatus.Sale) {
      return CampaignStatus.toString(book.campaign.status as number);
    }
    return "";
    // if (book.campaign.status != CampaignStatus.start) {
    //   return CampaignStatus.toString(book.campaign.status as number);
    // }
    // if (book.status == BookProductStatus.Sale) {
    //   if (book.withLevel && book.allowPurchasingByLevel) {
    //     return "Không đủ cấp";
    //   }
    // }
  }

  const getDisplay = () => {
    const flex = "flex";
    const none = "none";
    if (book?.campaign.status != CampaignStatus.start) {
      return flex;
    }
    if (book.withLevel && !book.allowPurchasingByLevel) {
      return flex;
    }
    if (book.status != BookProductStatus.Sale) {
      return flex;
    }
    return none;
    // if (book.campaign.status != CampaignStatus.start) {
    //   return "flex";
    // }
    // if (book.status == BookProductStatus.Sale) {
    //   if (book.withLevel && book.allowPurchasingByLevel) {
    //     return "flex";
    //   }
    // }
    // return "none";
  }

  const { push } = useRouter();
  return (
    <View
      style={{
        width: 195,
        height: 240,
        alignItems: "center",
        justifyContent: "center"
      }}>
      <Shadow style={{
        display: getDisplay(),
        backgroundColor: getStatusBackgrundColor(book.status as number),
        position: "absolute",
        borderRadius: 8,
        top: 20,
        right: 0,
        zIndex: 1,
        padding: 10
      }}>
        <Text style={{ color: "white", fontSize: 15 }}>
          {getStatusText()}
        </Text>
      </Shadow>
      <TouchableOpacity
        onPress={() => push("BookDetail", { bookId: book.id })}
        style={{
          borderColor: primaryTint7,
          borderWidth: 1,
          borderRadius: 8,
          height: "97%",
          width: "95%",
        }}>
        <View
          style={{
            height: "60%",
            alignItems: "center",
            justifyContent: "center"
          }}>
          <View
            style={{
              height: "95%",
              width: "95%",
              alignItems: "center",
              justifyContent: "center"
            }}>
            <Image
              source={{ uri: book.imageUrl }}
              resizeMode="cover"
              style={{ height: "90%", width: "90%" }} />
          </View>
        </View>
        <View
          style={{
            width: "100%",
            height: "40%",
            alignItems: "center",
            justifyContent: "center"
          }}>
          <View style={{
            width: "95%",
            height: "90%"
          }}>
            <View style={{ height: "30%", paddingLeft: 2 }}>
              <Text style={{ fontSize: 16, fontWeight: "600" }}>{book.title || ""}</Text>
            </View>
            <View style={{ height: "70%", width: "100%", flexDirection: "row" }}>
              <View style={{ width: "60%", height: "100%", justifyContent: "center", paddingLeft: 2 }}>
                <Text style={{ color: palettePink, fontSize: 18, fontWeight: "700" }}>{formatNumber(book.salePrice)}đ</Text>
                {
                  book.discount ?
                    <Text style={{ color: paletteGray, fontSize: 16, textDecorationLine: "line-through" }}>{formatNumber(book.book?.coverPrice)}đ</Text>
                    :
                    null
                }
              </View>
              <View style={{ width: "40%", height: "100%", alignItems: "flex-start", justifyContent: "flex-start" }}>
                {
                  book.discount ?
                    <View style={{
                      width: "90%",
                      backgroundColor: palettePink,
                      alignItems: "center"
                    }}>
                      <Text style={{ color: "white", fontSize: 18, padding: 5 }}>-{book.discount}%</Text>
                    </View>
                    :
                    null
                }
              </View>
            </View>

          </View>
        </View>
      </TouchableOpacity>
    </View>
  )
}

export default BookCard