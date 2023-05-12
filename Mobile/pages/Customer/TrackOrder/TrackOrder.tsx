import { color } from '@rneui/base'
import React, { useEffect } from 'react'
import { View, ScrollView } from 'react-native';
import { Text } from "@react-native-material/core";
import VerticalSlider from '../../../components/VerticalSlider/VerticalSlider'
import { paletteGray, paletteGrayShade2, paletteGrayShade3, paletteGrayShade4, paletteGreen, paletteGreenBold, palettePink, paletteRed, primaryTint1 } from '../../../utils/color'
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import { OrderViewModel } from '../../../objects/viewmodels/Orders/OrderViewModel';
import moment from 'moment';
import { dateTimeFormat } from '../../../utils/format';
import { OrderType } from '../../../objects/enums/OrderType';
import Shadow from '../../../components/Shadow/Shadow';

function TrackOrder(props: StackScreenProps<ParamListBase>) {
  const params = props.route.params as { order: OrderViewModel };
  return (
    <View style={{
      padding: 15
    }}>
      <Shadow style={{
        backgroundColor: "white",
        height: "100%",
        borderRadius: 8
      }}>
        <ScrollView style={{ padding: 10 }}>
          {
            params.order.type == OrderType.PickUp &&
            <>
              {
                params.order.orderDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteGrayShade3}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đặt hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.orderDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.availableDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={primaryTint1}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Chờ nhận hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.availableDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.receivedDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteGreenBold}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đã nhận hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.receivedDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.cancelledDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteRed}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đã hủy</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.cancelledDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
            </>
          }
          {
            params.order.type == OrderType.Shipping &&
            <>
              {
                params.order.orderDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteGrayShade2}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đặt hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.orderDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.shippingDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={primaryTint1}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đang giao hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.shippingDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.shippedDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteGreenBold}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đã nhận hàng</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.shippedDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
              {
                params.order.cancelledDate &&
                <View style={{
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    height: 100,
                    width: "20%"
                  }}>
                    <VerticalSlider
                      upSideDown
                      disabled
                      thumbTintColor={paletteRed}
                      maximumValue={100}
                      minimumValue={0}
                      value={30}
                      thumbStyle={{
                        width: 15,
                        height: 15
                      }}
                      trackStyle={{
                        height: 2
                      }}
                      maximumTrackTintColor={paletteGray}
                      minimumTrackTintColor={paletteGray}
                      style={{
                        width: 100
                      }}
                    />
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "80%",
                    justifyContent: "center",
                    rowGap: 10
                  }}>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>Đã hủy</Text>
                    <Text style={{ fontSize: 16, color: paletteGrayShade4 }}>{moment(params.order.cancelledDate).format(dateTimeFormat)}</Text>
                  </View>
                </View>
              }
            </>
          }
          <View style={{ marginTop: 20, padding : 10 }}>
            <Text style={{ fontSize: 18, fontWeight: "600" }}>Ghi chú:</Text>
            <Text>{params.order.note}</Text>
          </View>
        </ScrollView>
      </Shadow>
    </View>
  )
}

export default TrackOrder