import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import React, { useContext, useEffect } from 'react'
import { ActivityIndicator, ScrollView, View, Image, TouchableOpacity } from 'react-native'
import { Text } from '@react-native-material/core';
import { Menu, MenuOption, MenuOptions, MenuTrigger } from 'react-native-popup-menu';
import PageLoader from '../../../components/PageLoader/PageLoader';
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar';
import { paletteGray, palettePink, paletteRed, primaryColor, primaryTint1, primaryTint4 } from '../../../utils/color';
import sortBlack from "../../../assets/icons/sort-black.png";
import { useCreateChooseProductsOrderBooksPage, useCreateChooseProductsOrderSelectedBooksPage } from './CreateChooseProductsOrder.hook';
import DrawerLayout from 'react-native-drawer-layout';
import ExpandToggleView from '../../../components/ExpandToggleView/ExpandToggleView';
import SelectedLabel from '../../../components/SelectedLabel/SelectedLabel';
import { BookFormat } from '../../../objects/enums/BookFormat';
import { Button, CheckBox } from '@rneui/base';
import filterBlack from "../../../assets/icons/filter-black.png";
import SeletedBookCard from '../../../components/SeletedBookCard/SeletedBookCard';
import Shadow from '../../../components/Shadow/Shadow';
import Close from '../../../assets/SvgComponents/Close';
import truncateString from '../../../libs/functions/truncateString';
import useAppContext from '../../../context/Context';
import formatNumber from '../../../libs/functions/formatNumber';
import useRouter from '../../../libs/hook/useRouter';
import BouncyCheckbox from 'react-native-bouncy-checkbox';
import NumericInput from 'react-native-numeric-input';
import StackScreenContext from '../../../context/StackScreenContext';
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import Paging from '../../../components/Paging/Paging';


const Tab = createMaterialTopTabNavigator();

function CreateChooseProductsOrder(props: StackScreenProps<ParamListBase>) {
  return (
    <StackScreenContext.Provider value={props}>
      <Tab.Navigator
        screenOptions={{
          tabBarLabelStyle: {
            color: primaryColor,
            fontWeight: "500"
          },
          tabBarIndicatorStyle: {
            backgroundColor: primaryColor
          },
          swipeEnabled: false,
          lazy: true,
          lazyPlaceholder: () => <ActivityIndicator size="large" style={{ height: "100%" }} />
        }}>
        <Tab.Screen options={{ title: "Sách" }} name="Books" component={Books} />
        <Tab.Screen options={{ title: "Sách đã chọn" }} name="BookFairs" component={SeletedBooks} />
      </Tab.Navigator>
    </StackScreenContext.Provider>
  )
}


function Books() {
  const { staffCart } = useAppContext();
  const { push } = useRouter();
  const stackContextProps = useContext(StackScreenContext);
  const hook = useCreateChooseProductsOrderBooksPage(stackContextProps);
  return (
    <>
      <PageLoader
        zIndex={1}
        loading={hook.ui.loading}
      />
      <ScrollView
        stickyHeaderIndices={[0]}
        stickyHeaderHiddenOnScroll
        ref={hook.ref.booksScrollViewRef}
        scrollEnabled={!hook.ui.loading}
        style={{
          backgroundColor: "white",
        }}>
        <StickyHeaderSearchBar
          onChangeText={hook.input.search.set}
          value={hook.input.search.value}
          onSubmit={hook.event.onSearchSubmit} />
        <View style={{ padding: 10, flexDirection: "row", flexWrap: "wrap" }}>
          {
            hook.data.books && hook.data.books.map(item =>
              <View>
                <SeletedBookCard
                  onPress={() => hook.event.onBookSeleted(item)}
                  seleted={staffCart.find(p => item.id == p.id) != undefined}
                  book={item} />
              </View>
            )
          }

        </View>
        <View style={{ marginBottom: 20 }}>
          <Paging maxPage={hook.paging.maxPage} currentPage={hook.paging.currentPage} onPageNavigation={hook.paging.onPageNavigation} />
        </View>
      </ScrollView>

      <Shadow
        style={{
          display: staffCart.length > 0 ? "flex" : "none",
          backgroundColor: "white",
          height: "10%",
          alignItems: "center",
          justifyContent: "center"
        }}>
        <Button
          onPress={() => push("CreateConfirmOrder", stackContextProps.route.params)}
          buttonStyle={{
            backgroundColor: primaryTint1,
            width: 150,
            height: 50,
            shadowColor: "#000",
            shadowOffset: {
              width: 0,
              height: 12,
            },
            shadowOpacity: 0.58,
            shadowRadius: 16.00,
            elevation: 24
          }}>
          Tiếp theo
        </Button>
      </Shadow>
    </>
  )
}


function SeletedBooks() {
  const { staffCart } = useAppContext();
  const stackContextProps = useContext(StackScreenContext);
  const { push } = useRouter();
  const hook = useCreateChooseProductsOrderSelectedBooksPage();
  return (
    <>
      <ScrollView
        style={{
          backgroundColor: "white",
          height: "100%"
        }}>
        <View style={{
          padding: 15,
          height: "100%",
          paddingBottom: 30
        }}>
          {
            staffCart.map(product =>
              <Shadow style={{
                alignItems: "center",
                marginBottom: 20,
                borderRadius: 8,
                padding: 10,
                backgroundColor: "white"
              }}>
                <View style={{
                  height: 220,
                }}>
                  <View style={{
                    height: "70%",
                    flexDirection: "row",
                    marginTop: "5%"
                  }}>
                    {/* <View style={{
                      width: "15%",
                      alignItems: "center",
                      justifyContent: "center"
                    }}>
                      <BouncyCheckbox
                        isChecked={product.checked}
                        onPress={() => hook.event.onToggleChecked(product)}
                        disableBuiltInState
                        fillColor={primaryTint1} />
                    </View> */}
                    <View style={{
                      width: "40%",
                      height: "100%",
                      borderWidth: 1,
                      borderColor: primaryTint4
                    }}>
                      <Image source={{ uri: product.imageUrl }} style={{ height: "100%" }} resizeMode="cover" />
                    </View>
                    <View style={{
                      //borderWidth: 1,
                      width: "50%",
                      height: "100%",
                      padding: 10,
                      justifyContent: "center"
                    }}>
                      <Text style={{
                        fontSize: 20
                      }}>{truncateString(product.title, 3)}</Text>
                      <Text style={{ color: paletteGray }}>{product.issuerName}</Text>
                      <Text
                        style={{
                          paddingTop: 2,
                          color: palettePink,
                          fontSize: 18,
                          fontWeight: "700"
                        }}>{formatNumber(product.salePrice)}đ</Text>
                      <Text style={{ fontSize: 16, color: paletteGray }}>SL bán: {formatNumber(product.saleQuantity)}</Text>

                      <View style={{
                        flexDirection: "row",
                        height: 20,
                        paddingTop: 2
                      }}>
                        <NumericInput
                          minValue={1}
                          maxValue={product.saleQuantity}
                          totalWidth={90}
                          value={product.quantity}
                          onChange={(value) => hook.event.onQuantityChange(product.id, value)}
                        />
                      </View>
                    </View>
                    <View style={{
                      //borderWidth: 1,
                      width: "10%",
                      alignItems: 'center',
                      justifyContent: "center"
                    }}>
                      <TouchableOpacity
                        onPress={() => hook.event.removeFromCart(product.id)}>
                        <Close width={25} height={25} color={paletteRed} />
                      </TouchableOpacity>
                    </View>
                  </View>
                  <View style={{
                    //borderWidth : 1,
                    flexDirection: "row",
                    alignItems: "center"
                  }}>
                    {
                      product.withPdf &&
                      <CheckBox
                        title="PDF"
                        checked
                        center
                        containerStyle={{
                          backgroundColor: "transparent",
                          alignItems: "flex-start"
                        }}
                      />
                    }
                    {
                      product.withAudio &&
                      <CheckBox
                        title="Audio"
                        checked
                        center
                        containerStyle={{ backgroundColor: "transparent", alignItems: "flex-start" }} />
                    }
                  </View>
                </View>
              </Shadow>
            )
          }
        </View>
      </ScrollView>
      <Shadow
        style={{
          display: staffCart.length > 0 ? "flex" : "none",
          backgroundColor: "white",
          height: "10%",
          alignItems: "center",
          justifyContent: "center"
        }}>
        <Button
          onPress={() => push("CreateConfirmOrder", stackContextProps.route.params)}
          buttonStyle={{
            backgroundColor: primaryTint1,
            width: 150,
            height: 50,
            shadowColor: "#000",
            shadowOffset: {
              width: 0,
              height: 12,
            },
            shadowOpacity: 0.58,
            shadowRadius: 16.00,
            elevation: 24
          }}>
          Tiếp theo
        </Button>
      </Shadow>
    </>
  )
}

export default CreateChooseProductsOrder