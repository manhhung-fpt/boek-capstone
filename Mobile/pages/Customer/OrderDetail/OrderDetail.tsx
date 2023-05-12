import React from 'react'
import { ScrollView, View, Image, StyleSheet, TouchableOpacity, Pressable } from 'react-native';
import { Text, Button } from "@react-native-material/core";
import { paletteGray, paletteGrayShade1, paletteGrayShade2, paletteGrayShade3, paletteGreenBold, paletteOrange, palettePink, primaryTint1, primaryTint4 } from '../../../utils/color';
import ExpandToggleView from '../../../components/ExpandToggleView/ExpandToggleView';
import formatNumber from '../../../libs/functions/formatNumber';
import BarCode from '../../../assets/SvgComponents/BarCode';
import packageBlack from "../../../assets/icons/package-black.png";
import navigateRightBlack from "../../../assets/icons/navigate-right-black.png";
import Place from '../../../assets/SvgComponents/Place';
import useRouter from '../../../libs/hook/useRouter';
import LayoutModal from '../../../components/LayoutModal/LayoutModal';
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import useOrderDetailPage from './OrderDetail.hook';
import Shadow from '../../../components/Shadow/Shadow';
import DelimiterLine from '../../../components/DelimiterLine/DelimiterLine';
import { OrderType } from '../../../objects/enums/OrderType';
import AuthorizeView from '../../../libs/AuthorizeView';
import { Role } from '../../../objects/enums/Role';
import PageLoader from '../../../components/PageLoader/PageLoader';
import { OrderStatus } from '../../../objects/enums/OrderStatus';

function OrderDetail(props: StackScreenProps<ParamListBase>) {
  const { push } = useRouter();
  const hook = useOrderDetailPage(props);
  return (
    <>
      <PageLoader loading={hook.ui.loading} />
      <LayoutModal visible={hook.ui.infoModalVisible} onClose={() => hook.ui.setInfoModalVisible(!hook.ui.infoModalVisible)}>
        <Pressable
          onPress={() => hook.ui.setInfoModalVisible(false)}
          style={{
            width: "100%",
            height: "100%",
            alignItems: "center",
            justifyContent: "center",
            backgroundColor: "rgba(0,0,0,0.6)"
          }}>
          <View style={{
            backgroundColor: "white",
            borderRadius: 24,
            width: "95%",
            height: "35%",
            padding: 20,
            shadowColor: "#000",
            shadowOffset: {
              width: 0,
              height: 12,
            },
            shadowOpacity: 0.58,
            shadowRadius: 16.00,
            elevation: 24,
            rowGap: 15,
            justifyContent: "center"
          }}>


            <Text style={{
              display: hook.data.order.type == OrderType.Shipping ? "flex" : "none"
            }}>
              • NPH sẽ phụ trách giao hàng đến địa chỉ của
              bạn. {"\n\n"}
              • Phí vận chuyển của đơn phụ thuộc vào nội
              thành hay ngoại thành đối với các nơi hội
              sách đang tổ chức {"\n\n"}
              {"\t"}o Nội thành: 15,000 đ {"\n"}
              {"\t"}o Ngoại thành: 30,000 đ {"\n\n"}
              • Lưu ý: Boek không chịu trách nhiệm về đơn
              đổi trả của khách hàng. Xin liên hệ NPH về
              vấn đề này.{"\n\n"}
            </Text>

            <Text
              style={{
                display: hook.data.order.type == OrderType.PickUp ? "flex" : "none"
              }}>
              • NPH sẽ phụ trách thông báo địa điểm của
              đơn nhận tại quầy cho bạn khi đơn hàng
              sẵn sàng. {"\n\n"}
              • Nếu bạn không nhận hàng tại quầy trong
              thời gian hội sách diễn ra, thì đơn hàng sẽ
              bị hủy.{"\n\n"}
              • Lưu ý: Boek không chịu trách nhiệm về đơn
              đổi trả của khách hàng. Xin liên hệ NPH về
              vấn đề này.
            </Text>

          </View>
        </Pressable>
      </LayoutModal>
      <View style={{
        height: "85%",
        backgroundColor: "white",
        padding: 15
      }}>
        <Shadow style={{
          backgroundColor: "white",
          borderRadius: 12,
          overflow: "hidden",
        }}>
          <ScrollView
            style={{
              backgroundColor: "white",
              padding: 15
            }}>
            <View style={{
              flexDirection: "row"
            }}>
              <View style={{
                //borderWidth: 1,
                width: "15%",
                alignItems: "center",
                paddingTop: 2
              }}>
                <BarCode width={30} height={30} />
              </View>
              <View style={{
                rowGap: 5,
                padding: 10
              }}>
                <Text style={{ fontSize: 16 }}>{hook.data.order.code}</Text>
                <Text style={{ fontSize: 16, color: paletteGray }}>Giao hàng</Text>
              </View>
            </View>
            <DelimiterLine />
            <TouchableOpacity
              onPress={() => push("TrackOrder", { order: hook.data.order })}
              style={{
                padding: 10,
                flexDirection: "row"
              }}>
              <View style={{
                //borderWidth: 1,
                width: "15%",
              }}>
                <Image source={packageBlack} style={{ width: 35, height: 35 }} />
              </View>
              <View style={{
                //borderWidth: 1,
                width: "75%",
                rowGap: 7
              }}>
                <Text style={{ fontSize: 16 }}>Theo dõi đơn hàng</Text>
                <Text>{hook.data.order.address ? `Địa chỉ nhận hàng: ${hook.data.order.address}` : "Địa chỉ nhận hàng sẽ được cập nhật sau"}</Text>
                <Text style={{ color: hook.ui.getStatusColor() }}>{hook.data.order.statusName}</Text>
              </View>
              <View style={{
                //borderWidth: 1,
                width: "10%",
                alignItems: "center",
                justifyContent: "flex-start"
              }}>
                <Image source={navigateRightBlack} style={{ width: 25, height: 25 }} resizeMode="contain" />
              </View>
            </TouchableOpacity>
            <DelimiterLine />
            <View style={{
              padding: 10,
              flexDirection: "row"
            }}>
              <View style={{
                width: "15%"
              }}>
                <Place width={50} height={50} />
              </View>
              <View style={{
                //borderWidth: 1,
                width: "75%",
                rowGap: 5
              }}>
                <Text style={{ fontSize: 16 }}>{hook.data.order.customerName}</Text>
                <Text style={{ fontSize: 16, color: paletteGrayShade3 }}>SĐT: {hook.data.order.customerPhone}</Text>
                <Text style={{ fontSize: 16, color: paletteGrayShade3 }}>Địa chỉ: {hook.data.order.customer?.user.address}</Text>
              </View>
            </View>
            <DelimiterLine />
            <View style={{
              borderColor: primaryTint4,
              backgroundColor: "white",
              borderRadius: 8,
              overflow: "hidden",
              borderWidth: 2
            }}>
              <ExpandToggleView initExpanded label={hook.data.order.campaign?.name as string}>
                <View style={{ padding: 10, borderTopWidth: 1, borderTopColor: primaryTint4 }}>
                  {
                    hook.data.order.orderDetails?.map(item =>
                      <>
                        <Text style={{ fontSize: 16, marginBottom: 10, color: paletteGrayShade2 }}>{item.bookProduct?.issuer.user.name}</Text>
                        <View style={{
                          flexDirection: "row",
                          marginBottom: 20
                        }}>
                          <View style={{
                            borderWidth: 1,
                            borderColor: primaryTint1,
                            width: "20%",
                            height: 100,
                            borderRadius: 8
                          }}>
                            <Image resizeMode='contain' style={{ width: "100%", height: "100%" }} source={{ uri: item.bookProduct?.imageUrl }} />
                          </View>
                          <View style={{
                            width: "50%",
                            paddingLeft: 10,
                            justifyContent: "center"
                          }}>
                            <Text style={{ fontSize: 16 }}>{item.bookProduct?.title}</Text>
                            <Text>Số lượng: x{item.quantity}</Text>
                          </View>
                          <View style={{ width: "30%", alignItems: "flex-end", justifyContent: "center" }}>
                            <Text style={{ color: palettePink, fontSize: 18, fontWeight: "700" }}>{formatNumber(item.price)}đ</Text>
                          </View>
                        </View>
                      </>
                    )
                  }
                </View>
              </ExpandToggleView>
            </View>
            <View style={{ marginTop: 20 }}>
              <DelimiterLine />
            </View>
            <View style={{
              rowGap: 5,
              padding: 10
            }}>
              <Text style={{ fontSize: 16 }}>Phương thức thanh toán</Text>
              <Text style={{ color: paletteGray }}>{hook.data.order.paymentName}</Text>
            </View>
            <DelimiterLine />
            <View style={{
              padding: 10,
              marginBottom: 10
            }}>
              <View style={{ flexDirection: "row", marginBottom: 10 }}>
                <View style={{ width: "50%" }}>
                  <Text style={{ fontSize: 16 }}>Tạm tính</Text>
                </View>
                <View style={{ width: "50%", alignItems: "flex-end" }}>
                  <Text style={{ fontSize: 16 }}>{formatNumber(hook.data.order.total)}đ</Text>
                </View>
              </View>
              {
                hook.data.order.freight != 0 &&
                <View style={{ flexDirection: "row", marginBottom: 10 }}>
                  <View style={{ width: "50%" }}>
                    <Text style={{ fontSize: 16 }}>Phí vận chuyển</Text>
                  </View>
                  <View style={{ width: "50%", alignItems: "flex-end" }}>
                    <Text style={{ fontSize: 16 }}>{formatNumber(hook.data.order.freight)}đ</Text>
                  </View>
                </View>
              }
              {/* <View style={{ flexDirection: "row" ,  marginBottom: 10 }}>
                <View style={{ width: "50%" }}>
                  <Text style={{ fontSize: 16, color: palettePink }}>Tích điểm</Text>
                </View>
                <View style={{ width: "50%", alignItems: "flex-end" }}>
                  <Text style={{ fontSize: 16, color: paletteOrange }}>{formatNumber(100000)}đ</Text>
                </View>
              </View> */}
            </View>
          </ScrollView>
        </Shadow>
      </View>
      <View style={{
        backgroundColor: "white",
        padding: 10,
        height: "15%",
        //borderWidth: 1
      }}>
        <View style={{
          //borderWidth: 1,
          height: "100%",
          flexDirection: "row"
        }}>
          <View style={{
            //borderWidth : 1,
            width: "60%",
            justifyContent: "center",
            rowGap: 10,
            paddingLeft: 10
          }}>
            <Text style={{ fontSize: 16, color: palettePink }}>Tổng tiền</Text>
            <Text style={{ fontSize: 16 }}>(Giá đã bao gồm VAT)</Text>
            <Text style={{ fontSize: 20, color: paletteGreenBold }}>{formatNumber(hook.data.order.subTotal)}đ</Text>
          </View>
          {
            <AuthorizeView roles={[Role.staff.toString()]}>
              <View style={{
                width: "40%",
                alignItems: "flex-end",
                justifyContent: "center",
              }}>
                {
                  hook.data.order.type == OrderType.PickUp && hook.data.order.status == OrderStatus.PickUpAvailable &&
                  <Button
                    title="Thanh toán"
                    onPress={hook.event.onOrderSubmit}
                    style={{ marginTop: 10, height: 50, alignItems: "center", justifyContent: "center", backgroundColor: palettePink }} />
                }
              </View>
            </AuthorizeView>
          }
        </View>
      </View>
    </>

  )
}
const styles = StyleSheet.create({
  infoText: {
    marginTop: 20
  },
});

export default OrderDetail