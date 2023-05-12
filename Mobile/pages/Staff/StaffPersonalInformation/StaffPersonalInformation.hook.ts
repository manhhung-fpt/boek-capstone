import { useEffect, useRef, useState } from "react";
import { TextInput } from "react-native";
import SelectDropdown from "react-native-select-dropdown";
import Toast from "react-native-toast-message";
import appxios from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useUpdateDepsEffect from "../../../libs/hook/useUpdateDepsEffect";
import { District } from "../../../objects/enums/Districts";
import { Province } from "../../../objects/enums/Province";
import { Ward } from "../../../objects/enums/Ward";
import { UpdateUserRequestModel } from "../../../objects/requests/Users/UpdateUserRequestModel";
import { UserViewModel } from "../../../objects/viewmodels/Users/UserViewModel";
import endPont from "../../../utils/endPoints";
import { maxLength, required, validate, ValidationMessages } from "../../../utils/Validators";

export default function useStaffPersonalInformationPage() {

    const { user } = useAppContext();

    const inputNameRef = useRef<TextInput>(null);
    const inputProvinceRef = useRef<SelectDropdown>(null);
    const inputDistrictRef = useRef<SelectDropdown>(null);
    const inputWardRef = useRef<SelectDropdown>(null);
    const inputAddressRef = useRef<TextInput>(null);
    const inputPhoneRef = useRef<TextInput>(null);
    const inputCityRef = useRef<SelectDropdown>(null);

    const [loading, setLoading] = useState(false);
    const [buttonShowed, setButtonShowed] = useState(false);
    const [email, setEmail] = useState("");
    const [opacity, setOpacity] = useState(1);

    const [provincesSelect, setProvincesSelect] = useState<Province[]>([]);
    const [districtSelect, setDistrictSelect] = useState<District[]>([]);
    const [wardSelect, setWardSelect] = useState<Ward[]>([]);

    const [name, setName] = useState("");
    const [phone, setPhone] = useState("");
    const [birth, setBirth] = useState<Date>(new Date());
    const [gender, setGender] = useState(false);
    const [province, setProvince] = useState<Province>();
    const [district, setDistrict] = useState<District>();
    const [ward, setWard] = useState<Ward>();
    const [address, setAddress] = useState("");

    const [validator, setValidator] = useState<ValidationMessages>();

    const onProvinceSelected = (selectedProvince: Province) => {
        if (!province || province && province.code != selectedProvince.code) {
            setProvince(selectedProvince);
            setDistrict(undefined);
            setWard(undefined);
            inputDistrictRef.current?.reset();
            setLoading(true);
            appxios.get<District[]>(`${endPont.public.provinces}/${selectedProvince.code}${endPont.lead.districts}`).then(response => {
                setDistrictSelect(response.data);
                setLoading(false);
            });
        }
    }
    const onDistrictSelected = (seletedDistrict: District) => {
        if (!district || district.code != seletedDistrict.code) {
            setDistrict(seletedDistrict);
            setWard(undefined);
            inputWardRef.current?.reset();
            setLoading(true);
            appxios.get<Ward[]>(`${endPont.public.districts}/${seletedDistrict.code}${endPont.lead.ward}`).then(response => {
                setWardSelect(response.data);
                setLoading(false);
            });
        }
    }
    const onDistrictSelectedFocus = () => {
        if (!province) {
            Toast.show({
                text1: "Thông báo",
                text2: "Vui lòng chọn tỉnh trước"
            });
            inputDistrictRef.current?.closeDropdown();

        }
    }
    const onWardSelected = (selectedWard: Ward) => {
        if (!ward || ward.code != selectedWard.code) {
            setWard(selectedWard);
        }
    }
    const onWardSelectedFocus = () => {
        if (!district) {
            Toast.show({
                text1: "Thông báo",
                text2: "Vui lòng chọn quận trước"
            });
            inputWardRef.current?.closeDropdown();
        }
    }

    const onSubmit = () => {
        if (!user) {
            return;
        }
        const staffV = {
            name: [
                required(name, "Tên không được trống"),
                maxLength(name, 128, "Tên không vượt quá 128 ký tự")
            ],
            province: [
                required(province, "Tỉnh không được trống"),
            ],
            district: [
                required(district, "Quận không được trống"),
            ],
            ward: [
                required(ward, "Phường không được trống"),
            ],
            address: [
                required(address, "Địa chỉ không được trống"),
            ],
            phone: [
                required(phone, "Số điện thoại không được trống")
            ]
        };
        if (validate(staffV)) {
            const request: UpdateUserRequestModel = {
                id: user.id,
                name: name,
                email: user.email,
                imageUrl: user.imageUrl,
                phone: phone,
                addressRequest: {
                    detail: address,
                    districtCode: district?.code as number,
                    provinceCode: province?.code as number,
                    wardCode: ward?.code as number,
                },
                role: user.role,
                status: true
            };
            appxios.put<UserViewModel>(endPont.users.staff, request).then(response => {
                Toast.show({
                    text1: "Thông báo",
                    text2: "Lưu thành công"
                });
                setButtonShowed(false);
            });
        }
        setValidator(staffV);
    }

    useEffect(() => {
        setLoading(true);
        appxios.get<Province[]>(endPont.public.provinces).then(getProvinceResponse => {
            setProvincesSelect(getProvinceResponse.data);
            appxios.get<UserViewModel>(endPont.users.me).then(async getUserResponse => {
                try {
                    const districtsResponse = await appxios.get<District[]>(`${endPont.public.provinces}/${getUserResponse.data.addressViewModel.provinceCode}${endPont.lead.districts}`);
                    const wardsResponse = await appxios.get<Ward[]>(`${endPont.public.districts}/${getUserResponse.data.addressViewModel.districtCode}${endPont.lead.ward}`);
                    setDistrictSelect(districtsResponse.data);
                    setWardSelect(wardsResponse.data);
                    setDistrict(districtsResponse.data.find(d => d.code == getUserResponse.data.addressViewModel.districtCode));
                    setWard(wardsResponse.data.find(w => w.code == getUserResponse.data.addressViewModel.wardCode));
                    
                } catch (error) {

                }
                setEmail(getUserResponse.data.email);
                setName(getUserResponse.data.name);
                setPhone(getUserResponse.data.phone);
                setProvince(getProvinceResponse.data.find(p => p.code == getUserResponse.data.addressViewModel.provinceCode));
                setAddress(getUserResponse.data.addressViewModel.detail);
            })
                .finally(() => {
                    setLoading(false);
                });

        });

    }, []);

    useUpdateDepsEffect(() => {
        if (!loading) {
            if (!buttonShowed) {
                setButtonShowed(true);
            }
        }
    }, [name, province, district, ward, address, phone]);


    return {
        buttonShowed,
        loading,
        validator,
        opacity,
        ref: {
            inputNameRef,
            inputProvinceRef,
            inputDistrictRef,
            inputWardRef,
            inputAddressRef,
            inputPhoneRef,
            inputCityRef
        },
        data: {
            email,
            provincesSelect,
            districtSelect,
            wardSelect
        },
        event: {
            onSubmit,
            onDistrictSelected,
            onDistrictSelectedFocus,
            onProvinceSelected,
            onWardSelected,
            onWardSelectedFocus
        },
        input:
        {
            name:
            {
                value: name,
                set: setName
            },
            phone: {
                value: phone,
                set: setPhone
            },
            birth: {
                value: birth,
                set: setBirth
            },
            province: {
                value: province,
                set: setProvince
            },
            district: {
                value: district,
                set: setDistrict
            },
            ward: {
                value: ward,
                set: setWard
            },
            address: {
                value: address,
                set: setAddress
            },
            gender:
            {
                value: gender,
                set: setGender
            }
        }
    };
}