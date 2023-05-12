import React from 'react'
import { ScrollView, View, Image, StyleSheet, FlatList, TouchableOpacity, Pressable, Dimensions, RefreshControl } from 'react-native'
import { Text } from '@react-native-material/core'
import { paletteGray, paletteGrayLight, paletteGrayShade2, paletteGrayShade5, paletteGreen, paletteGreenBold, paletteGreenShade1, palettePink, paletteRed, primaryTint1, primaryTint2, primaryTint4, primaryTint7 } from '../../../utils/color'
import useStaffOrdersPage from './StaffOrders.hook';
import image from "../../../assets/hsxv.webp";
import addWhite from "../../../assets/icons/add-white.png";
import Paging from '../../../components/Paging/Paging';
import { Button } from '@rneui/base';
import SelectedChip from '../../../components/SeletedChip/SelectedChip';
import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import FloatActionButton from '../../../components/FloatActionButton/FloatActionButton';
import range from '../../../libs/functions/range';
import useRouter from '../../../libs/hook/useRouter';
import { Menu, MenuOption, MenuOptions, MenuTrigger } from 'react-native-popup-menu';
import Shadow from '../../../components/Shadow/Shadow';
import { OrderStatus } from '../../../objects/enums/OrderStatus';
import formatNumber from '../../../libs/functions/formatNumber';
import moment from 'moment';
import truncateString from '../../../libs/functions/truncateString';
import { dateTimeFormat } from '../../../utils/format';
import PageLoader from '../../../components/PageLoader/PageLoader';
import StaffOrderCard from '../../../components/StaffOrderCard/StaffOrderCard';
import { ParamListBase } from '@react-navigation/native';
import { BottomTabScreenProps } from '@react-navigation/bottom-tabs';
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar';

const Tab = createMaterialTopTabNavigator();

function StaffOrders(props: BottomTabScreenProps<ParamListBase>) {
  const hook = useStaffOrdersPage();
  const { push } = useRouter();

  return (
    <>
      <PageLoader loading={hook.ui.loading} />
      <FloatActionButton bottom={70} right={10}>
        <Menu>
          <MenuTrigger>
            <View style={{
              height: "100%",
              justifyContent: "center",
              alignItems: "center"
            }}>
              <Image source={addWhite} style={{ width: "50%", height: "50%" }} />
            </View>
          </MenuTrigger>
          <MenuOptions customStyles={{ optionsContainer: { width: "55%" } }} optionsContainerStyle={{ padding: 10 }}>
            <MenuOption
              style={{
                borderBottomWidth: 1,
                borderBottomColor: paletteGray,
                padding: 10
              }}
              onSelect={() => push("CreateOrderScanQr")}>
              <Text style={{ fontSize: 17 }}>Quét mã QR đơn hàng</Text>
            </MenuOption>
            <MenuOption
              style={{
                padding: 10
              }}
              onSelect={() => props.navigation.jumpTo("StaffCampaigns")}>
              <Text style={{ fontSize: 17 }}>Tạo đơn hàng</Text>
            </MenuOption>
          </MenuOptions>
        </Menu>
      </FloatActionButton>
      <ScrollView
        ref={hook.ref.scrollViewRef}
        stickyHeaderHiddenOnScroll
        stickyHeaderIndices={[0]}
        style={{
          backgroundColor: "white",
          width: "100%",
          height: "100%",
        }}
        refreshControl={
          <RefreshControl refreshing={hook.ui.refreshing}
            onRefresh={hook.event.onRefresh} />}
      >
        <View>
          <FlatList
            style={{
              height: 50,
              backgroundColor: "white",
              borderBottomWidth: 1,
              borderBottomColor: paletteGray,
              //justifyContent: "center",
              paddingLeft: 10
            }}
            horizontal
            data={[
              {
                label: "Tất cả",
                input: 0
              },
              {
                label: OrderStatus.getLabel(OrderStatus.Processing),
                input: OrderStatus.Processing
              },
              {
                label: OrderStatus.getLabel(OrderStatus.PickUpAvailable),
                input: OrderStatus.PickUpAvailable
              },
              {
                label: OrderStatus.getLabel(OrderStatus.Received),
                input: OrderStatus.Received
              },
              {
                label: OrderStatus.getLabel(OrderStatus.Cancelled),
                input: OrderStatus.Cancelled
              }]}
            renderItem={item =>
              <View style={{ height: "100%", justifyContent: "center", marginRight: 2 }}>
                <SelectedChip
                  onPress={() => hook.input.orderStatus.set(item.item.input)}
                  selected={hook.input.orderStatus.value == item.item.input}
                  label={item.item.label} />
              </View>
            } />
          <StickyHeaderSearchBar onChangeText={hook.input.search.set} onSubmit={hook.event.onSearchSubmit} value={hook.input.search.value} />
        </View>
        <View style={{ padding: 5, alignItems: "center" }}>
          {
            hook.data.orders.length == 0 &&
            <View style={{
              alignItems: "center",
              justifyContent: "center",
              width: "100%",
              height: Dimensions.get("screen").height * 80 / 100,
            }}>
              <Text variant='h5'>Không có đơn hàng</Text>
            </View>
          }
          {
            hook.data.orders?.map(item =>
              item.orderDetails && item.orderDetails[0] &&
              <StaffOrderCard order={item} />
            )
          }

        </View>
        <View style={{ marginBottom: 20 }}>
          <Paging currentPage={hook.paging.currentPage} maxPage={hook.paging.maxPage} onPageNavigation={hook.paging.onPageNavigation} />
        </View>
      </ScrollView>
    </>
  )
}

const styles = StyleSheet.create({
  marginBot: {
    marginBottom: 6
  }
});

export default StaffOrders