import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { AxiosInterceptor } from './components/AxiosInterceptor';
import Forbidden from './pages/Forbidden';
import React from 'react';
import { route } from './libs/hook/useRouter';
import { primaryColor, primaryTint1 } from './utils/color';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { Icon } from '@rneui/base';
import { Image } from 'react-native';
import accountWhite from "./assets/icons/account-circle-white.png";
import workHistoryWhite from "./assets/icons/work-history-white.png";
import useAuth from './libs/hook/useAuth';
import useInit from './context/useInit';
import Organizations from './pages/Customer/Organizations/Organizations';
import PersonalInformation from './pages/Authenticated/PersonalInformation/PersonalInformation';
import Orders from './pages/Customer/Orders/Orders';
import OrderDetail from './pages/Customer/OrderDetail/OrderDetail';
import AskGenres from './pages/Customer/AskGenres/AskGenres';
import BookDetail from './pages/Customer/BookDetail/BookDetail';
import AskOrganizations from './pages/Customer/AskOrganizations/AskOrganizations';
import AskPersonalInformation from './pages/Customer/AskPersonalInformation/AskPersonalInformation';
import StaffCampaigns from './pages/Staff/StaffCampaigns/StaffCampaigns';
import { Role } from './objects/enums/Role';
import StaffOrders from './pages/Staff/StaffOrders/StaffOrders';
import StaffCampagin from './pages/Staff/StaffCampagin/StaffCampagin';
import PageLoader from './components/PageLoader/PageLoader';
import PdfShower from './pages/Customer/PdfShower/PdfShower';
import TrackOrder from './pages/Customer/TrackOrder/TrackOrder';
import OrderType from './pages/Customer/OrderType/OrderType';
import OrderConfirm from './pages/Customer/OrderConfirm/OrderConfirm';
import CreateChooseCampaignOrder from './pages/Staff/CreateChooseCampaignOrder/CreateChooseCampaignOrder';
import CreateChooseHaveAccountOrder from './pages/Staff/CreateChooseHaveAccountOrder/CreateChooseHaveAccountOrder';
import CreateChooseCustomerOrder from './pages/Staff/CreateChooseCustomerOrder/CreateChooseCustomerOrder';
import CreateChooseProductsOrder from './pages/Staff/CreateChooseProductsOrder/CreateChooseProductsOrder';
import CreateConfirmOrder from './pages/Staff/CreateConfirmOrder/CreateConfirmOrder';
import Cart from './pages/Customer/Cart/Cart';
import IssuerMoreBook from './pages/Customer/IssuerMoreBook/IssuerMoreBook';
import PriceComparison from './pages/Customer/PriceComparison/PriceComparison';
import CampaignDetail from './pages/Customer/CampaignDetail/CampaignDetail';
import IssuerDetail from './pages/Customer/IssuerDetail/IssuerDetail';
import RecurringCampaign from './pages/Customer/RecurringCampaign/RecurringCampaign';
import BookItems from './pages/Customer/BookItems/BookItems';
import BookItemDetail from './pages/Customer/BookItemDetail/BookItemDetail';
import Index from './pages/Customer/Index/Index';
import Campaigns from './pages/Customer/Campaigns/Campaigns';
import Search from './pages/Customer/Search/Search';
import Profile from './pages/Customer/Profile/Profile';
import Login from './pages/Public/Login/Login';
import StaffPersonalInformation from './pages/Staff/StaffPersonalInformation/StaffPersonalInformation';
import CreateOrderScanQr from './pages/Staff/CreateOrderScanQr/CreateOrderScanQr';

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();
function Routers() {
  useInit();
  //useAuthorizeInit();
  return (
    <NavigationContainer ref={route}>
      <AxiosInterceptor>
        <StackNavigator />
      </AxiosInterceptor>
    </NavigationContainer>
  );
}
function StackNavigator() {
  const { initLoading, isInRole, authenticated } = useAuth();
  return (
    <Stack.Navigator screenOptions={{ headerStyle: { backgroundColor: primaryColor }, headerTitleStyle: { color: "white" }, headerTintColor: "white" }}>
      <Stack.Screen options={{ headerShown: false }} name={"Index"}>{(props) =>
        !initLoading ?
          authenticated ?
            isInRole([Role.staff.toString()]) ?
              <StaffTabNavigator />
              :
              <TabNavigator />
            :
            <Login {...props} />
          :
          <PageLoader loading />
      }</Stack.Screen>

      {/* Public */}
      {/* <Stack.Screen options={{ title: "" }} name={"Login"} component={Login} /> */}

      {/* Customer */}
      <Stack.Screen options={{ title: "Giỏ hàng" }} name={"Cart"}>{(props) => <Cart {...props} />}</Stack.Screen>
      <Stack.Screen options={{ title: "So sánh giá" }} name={"PriceComparison"}>{(props) => <PriceComparison {...props} />}</Stack.Screen>
      <Stack.Screen options={{ title: "" }} name={"IssuerMoreBook"}>{(props) => <IssuerMoreBook {...props} />}</Stack.Screen>
      <Stack.Screen options={{ title: "Chi tiết" }} name={"IssuerDetail"}>{(props) => <IssuerDetail {...props} />}</Stack.Screen>
      <Stack.Screen options={{ title: "" }} name={"CampaignDetail"}>{(props) => <CampaignDetail {...props} />}</Stack.Screen>
      <Stack.Screen options={{ title: "" }} name={"BookDetail"} component={BookDetail} />
      <Stack.Screen options={{ title: "Địa điểm" }} name={"RecurringCampaign"} component={RecurringCampaign} />
      <Stack.Screen options={{ title: "" }} name={"PdfShower"} component={PdfShower} />
      <Stack.Screen options={{ title: "" }} name={"BookItems"} component={BookItems} />
      <Stack.Screen options={{ title: "" }} name={"BookItemDetail"} component={BookItemDetail} />
      <Stack.Screen options={{ title: "Tổ chức" }} name={"Organizations"}>{() => <Organizations />}</Stack.Screen>
      <Stack.Screen options={{ title: "Thông tin cá nhân" }} name={"PersonalInformation"}>{() => <PersonalInformation />}</Stack.Screen>
      <Stack.Screen options={{ title: "Đơn hàng của tôi" }} name={"Orders"} component={Orders} />
      <Stack.Screen options={{ title: "Đơn hàng", headerTitleAlign: "center" }} name={"OrderDetail"} component={OrderDetail} />
      <Stack.Screen options={{ title: "Thể loại sách yêu thích" }} name={"AskGenres"}>{() => <AskGenres />}</Stack.Screen>
      <Stack.Screen options={{ title: "Theo dõi đơn hàng" }} name={"TrackOrder"} component={TrackOrder} />
      <Stack.Screen options={{ title: "Thanh toán" }} name="OrderType" component={OrderType} />
      <Stack.Screen options={{ title: "Xác nhận đơn hàng" }} name="OrderConfirm" component={OrderConfirm} />


      <Stack.Screen options={{ headerShown: false }} name={"AskGenresWizard"}>{() => <AskGenres skiped />}</Stack.Screen>
      <Stack.Screen options={{ headerShown: false }} name={"AskOrganizations"}>{() => <AskOrganizations />}</Stack.Screen>
      <Stack.Screen options={{ headerShown: false }} name={"AskPersonalInformation"}>{(props) => <AskPersonalInformation  {...props} />}</Stack.Screen>

      {/* Staff */}
      <Stack.Screen options={{ title: "", headerShown: false }} name="StaffCampagin" component={StaffCampagin} />
      <Stack.Screen options={{ title: "Thông tin cá nhân" }} name="StaffPersonalInformation" component={StaffPersonalInformation} />
      <Stack.Screen options={{ title: "Tạo đơn hàng" }} name="CreateChooseCampaignOrder" component={CreateChooseCampaignOrder} />
      <Stack.Screen options={{ title: "Tạo đơn hàng" }} name="CreateChooseHaveAccountOrder" component={CreateChooseHaveAccountOrder} />
      <Stack.Screen options={{ title: "Tạo đơn hàng" }} name="CreateChooseCustomerOrder" component={CreateChooseCustomerOrder} />
      <Stack.Screen options={{ title: "Tạo đơn hàng" }} name="CreateChooseProductsOrder" component={CreateChooseProductsOrder} />
      <Stack.Screen options={{ title: "Xác nhận hơn hàng" }} name="CreateConfirmOrder" component={CreateConfirmOrder} />
      <Stack.Screen options={{ title: "Xác nhận hơn hàng" }} name="CreateOrderScanQr" component={CreateOrderScanQr} />

      <Stack.Screen name={"Forbidden"}>{() => <Forbidden />}</Stack.Screen>
    </Stack.Navigator>
  );
}
function TabNavigator() {
  return (
    <Tab.Navigator
      backBehavior='none'
      safeAreaInsets={{ bottom: 0 }}
      screenOptions={{
        tabBarStyle: {
          height: 60
        },
        headerShown: false,
        tabBarLabelStyle:
        {
          fontSize: 13,
          color: "white",
          marginBottom: "10%"
        },
        tabBarIconStyle: {
          //marginTop: 5
        },
        tabBarInactiveBackgroundColor: primaryColor,
        tabBarActiveBackgroundColor: primaryTint1,
        lazy: true
      }}>
      <Tab.Screen options={{ title: "Hội sách", tabBarIcon: () => <Icon name='book' color={"white"} size={17} /> }} name="Campaigns" component={Campaigns} />
      <Tab.Screen options={{ unmountOnBlur: true, title: "Tìm kiếm", tabBarIcon: () => <Icon name='book' type='entypo' color={"white"} size={17} /> }} name="Search" component={Search} />
      <Tab.Screen options={{ title: "Cá nhân", tabBarIcon: () => <Image source={accountWhite} style={{ height: 17 }} resizeMode={"contain"} /> }} name="Profile" component={Profile} />
      {
        __DEV__ &&
        <Tab.Screen name="Test" options={{ title: "Test" }} component={Index} />
      }
    </Tab.Navigator>
  );
}

function StaffTabNavigator() {
  return (
    <Tab.Navigator
      backBehavior='none'
      safeAreaInsets={{ bottom: 0 }}
      screenOptions={{
        tabBarStyle: {
          height: 60
        },
        headerShown: false,
        tabBarLabelStyle:
        {
          fontSize: 13,
          color: "white",
          marginBottom: "10%"
        },
        tabBarIconStyle: {
          //marginTop: 5
        },
        tabBarInactiveBackgroundColor: primaryColor,
        tabBarActiveBackgroundColor: primaryTint1,
        lazy: true
      }}>
      <Tab.Screen options={{ title: "Hội sách", tabBarIcon: () => <Icon name='book' color={"white"} size={17} /> }} name="StaffCampaigns" component={StaffCampaigns} />
      <Tab.Screen options={{ title: "Đơn hàng", tabBarIcon: () => <Image source={workHistoryWhite} style={{ height: 17 }} resizeMode={"contain"} /> }} name="StaffOrders" component={StaffOrders} />
      <Tab.Screen options={{ title: "Cá nhân", tabBarIcon: () => <Image source={accountWhite} style={{ height: 17 }} resizeMode={"contain"} /> }} name="Profile" component={Profile} />
    </Tab.Navigator>
  );
}

export default Routers
