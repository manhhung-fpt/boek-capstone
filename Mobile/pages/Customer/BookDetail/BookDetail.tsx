import { Button } from '@rneui/base';
import React from 'react';
import { Text } from "@react-native-material/core";
import { View, Image, ScrollView, TouchableOpacity } from 'react-native'
import useBookDetailPage from './BookDetail.hook';
import avatar from "../../../assets/avatar.jpg";
import navigateRightBlack from "../../../assets/icons/navigate-right-black.png";
import useRouter from '../../../libs/hook/useRouter';
import { paletteGray, paletteGrayShade2, palettePink, primaryColor, primaryTint1, primaryTint10, primaryTint4, primaryTint5, primaryTint8, primaryTint9 } from '../../../utils/color';
import LinearGradient from 'react-native-linear-gradient';
import ShowMoreButton from '../../../components/ShowMoreButton/ShowMoreButton';
import TitleTabedFlatBooks from '../../../components/TitleTabedFlatBooks/TitleTabedFlatBooks';
import formatNumber from '../../../libs/functions/formatNumber';
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import PageLoader from '../../../components/PageLoader/PageLoader';
import useAppContext from '../../../context/Context';
import { MobileBookProductViewModel } from '../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
import truncateString from '../../../libs/functions/truncateString';
import LibraryBooks from '../../../assets/SvgComponents/LibraryBooks';
import AudioPlayer from '../../../components/AudioPlayer/AudioPlayer';
import TitleFlatBooks from '../../../components/TitleFlatBooks/TitleFlatBooks';
import { BookProductStatus } from '../../../objects/enums/BookProductStatus';
import Shadow from '../../../components/Shadow/Shadow';


function BookDetail(props: StackScreenProps<ParamListBase>) {
    const hook = useBookDetailPage(props);
    const { addToCart } = useAppContext();
    const { push } = useRouter();
    return (
        <>
            <PageLoader loading={hook.ui.loading} opacity={1} />
            <ScrollView style={{}}>
                <View style={{
                    backgroundColor: "white",
                    padding: 15
                }}>
                    <Shadow style={{
                        padding: 10,
                        borderRadius: 8,
                        backgroundColor: "white",
                    }}>
                        <View style={{ alignItems: "center" }}>
                            <View style={{ width: "60%", height: 400, paddingTop: 20 }}>
                                <View style={{ borderRadius: 24, borderWidth: 1, borderColor: primaryTint5, overflow: "hidden" }}>
                                    <Image
                                        style={{ width: "100%", height: "100%" }}
                                        source={{ uri: hook.data.book?.imageUrl }}
                                        resizeMode="cover"
                                    />
                                </View>
                            </View>
                        </View>
                        <View style={{

                        }}>
                            <Text variant='h6' style={{ marginBottom: 10 }}>{hook.data.book?.title}</Text>
                            {
                                hook.data.book && hook.data.book.book?.genre &&
                                <View style={{
                                    borderWidth: 1,
                                    borderColor: paletteGray,
                                    padding: 7,
                                    width: "50%",
                                    borderRadius: 8,
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}>
                                    <Text style={{ fontSize: 15, color: primaryColor }}>{hook.data.book?.book?.genre.name}</Text>
                                </View>
                            }
                            <View style={{ flexDirection: "row", paddingTop: 20, paddingBottom: 10 }}>
                                <View style={{ width: "10%" }}>
                                    <LibraryBooks />
                                </View>
                                <View style={{ width: "90%" }}>
                                    <Text style={{ fontSize: 15 }} >{hook.data.book?.campaign?.name}</Text>
                                </View>
                            </View>
                        </View>
                        {
                            hook.data.book?.otherMobileBookProducts &&
                            hook.data.book?.otherMobileBookProducts.length > 0 &&
                            <View style={{ justifyContent: 'flex-start', alignItems: "flex-end", marginBottom: 5 }}>
                                <Text style={{ fontSize: 16 }}>Có <Text style={{ color: primaryColor, fontWeight: "600" }}>{hook.data.book.otherMobileBookProducts.length} </Text>giá khác</Text>
                            </View>
                        }
                        <View style={{ marginBottom: 20, flexDirection: "row" }}>
                            <View style={{ width: "40%", alignItems: "flex-start", justifyContent: "center" }}>
                                <Text style={{ color: palettePink, fontSize: 20, fontWeight: "700" }}>{formatNumber(hook.data.book?.salePrice)} đ</Text>
                                {
                                    hook.data.book?.book?.coverPrice &&
                                    <Text style={{ color: paletteGray, fontSize: 18, textDecorationLine: "line-through" }}>{formatNumber(hook.data.book?.book?.coverPrice)} đ</Text>
                                }
                            </View>

                            <View style={{ width: "25%", alignItems: "center", justifyContent: "center" }}>
                                {
                                    hook.data.book?.discount ?
                                        <View style={{
                                            alignItems: "flex-start",
                                            justifyContent: "flex-start"
                                        }}>
                                            <View style={{ width: "90%", backgroundColor: palettePink, alignItems: "center" }}>
                                                <Text style={{ color: "white", fontSize: 20, padding: 7 }}>-{hook.data.book.discount}%</Text>
                                            </View>
                                        </View>
                                        :
                                        null
                                }
                            </View>
                            {
                                hook.data.book?.otherMobileBookProducts &&
                                hook.data.book?.otherMobileBookProducts.length > 0 &&
                                <View style={{ width: "35%", justifyContent: "center" }}>
                                    <View>
                                        <Button
                                            onPress={() => push("PriceComparison", { data: hook.data.book })}
                                            buttonStyle={{ borderRadius: 8, backgroundColor: primaryTint1 }}>So sánh giá</Button>
                                    </View>
                                </View>
                            }
                        </View>
                        <View style={{
                            display: hook.ui.showPdfOrAudio() ? "flex" : "none",
                            height: 70,
                            flexDirection: "row",
                            justifyContent: "center"
                        }}>
                            {
                                hook.data.book?.withPdf &&
                                <View style={{ padding: 5, width: "50%" }}>
                                    <Button
                                        onPress={() => push("PdfShower", { title: "Pdf mẫu", url: hook.data.book?.book?.pdfTrialUrl })}
                                        buttonStyle={{ backgroundColor: primaryColor }}>PDF mẫu - {formatNumber(hook.data.book.pdfExtraPrice)}đ</Button>
                                </View>
                            }
                            {
                                hook.data.book?.withAudio &&
                                <View style={{ padding: 5, width: "50%" }}>
                                    <Button
                                        onPress={() => { hook.ui.setTrialAudioVisible(!hook.ui.trialAudioVisible); hook.ref.audioPlayerRef.current?.stop(); }}
                                        buttonStyle={{ backgroundColor: primaryColor }}>Audio mẫu - {formatNumber(hook.data.book.audioExtraPrice)}đ</Button>
                                </View>
                            }
                        </View>
                        {
                            hook.ui.trialAudioVisible &&
                            <View style={{
                                alignItems: "center",
                                width: "100%",
                                marginBottom: 30
                            }}>
                                <View style={{ width: "95%" }}>
                                    <AudioPlayer ref={hook.ref.audioPlayerRef} audioUri={hook.data.book?.book?.audioTrialUrl as string} />
                                </View>
                            </View>
                        }
                    </Shadow>
                    {
                        hook.data.book?.bookProductItems && hook.data.book.bookProductItems.length > 0 &&
                        <Shadow style={{
                            backgroundColor: "white",
                            borderRadius: 8,
                            padding: 10,
                            marginTop: 20
                        }}>
                            <TouchableOpacity
                                onPress={() => push("BookItems", { data: hook.data.book })}
                                style={{ marginBottom: 20, flexDirection: "row" }}>
                                <View style={{ width: "20%", height: 120 }}>
                                    <View style={{ borderRadius: 4, borderWidth: 1, borderColor: primaryTint5, overflow: "hidden" }}>
                                        <Image
                                            style={{ width: "100%", height: "100%" }}
                                            source={{ uri: hook.data.book?.imageUrl }}
                                            resizeMode="cover"
                                        />
                                    </View>
                                </View>
                                <View style={{ width: "70%", paddingLeft: "5%", justifyContent: "center" }}>
                                    <Text style={{ fontSize: 18, marginBottom: "5%" }}>Cùng khám phá</Text>
                                    <Text style={{ fontSize: 16, color: paletteGrayShade2 }}>{hook.data.book.bookProductItems.length} cuốn trong {hook.data.book.typeName.toLowerCase()}</Text>
                                </View>
                                <View style={{
                                    width: "10%",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}>
                                    <View>
                                        <Image source={navigateRightBlack} />
                                    </View>
                                </View>

                            </TouchableOpacity>
                        </Shadow>
                    }

                    <Shadow style={{
                        backgroundColor: "white",
                        borderRadius: 8,
                        padding: 10,
                        marginTop: 20
                    }}>
                        {
                            hook.data.book?.book?.bookAuthors && hook.data.book.book.bookAuthors.length > 0 &&
                            <View style={{ marginBottom: 20, flexDirection: "row" }}>
                                <View style={{ width: "20%", justifyContent: 'center' }}>
                                    <View style={{
                                        borderRadius: 999,
                                        overflow: "hidden",
                                        width: 60,
                                        height: 60
                                    }}>
                                        <Image
                                            source={{ uri: hook.data.book.book.bookAuthors.at(0) && hook.data.book.book.bookAuthors[0].author.imageUrl }}
                                            resizeMode="cover"
                                            style={{ width: 60, height: 60 }} />
                                    </View>

                                </View>
                                <View style={{ width: "80%", justifyContent: "center" }}>
                                    <Text variant='h6'>Tác giả</Text>
                                    <Text style={{ fontSize: 16 }}>{hook.data.book?.book?.bookAuthors.map(item => item.author.name).join(", ")}</Text>
                                </View>
                            </View>
                        }

                        <Text variant='h6' style={{ marginBottom: 30 }}>Thông tin chi tiết</Text>

                        {
                            hook.data.book?.book ?
                                <>
                                    <View>
                                        {
                                            !hook.ui.informationExpanded &&
                                            <LinearGradient
                                                start={{ x: 0.5, y: 0.4 }}
                                                end={{ x: 0.5, y: 1 }}
                                                colors={['rgba(255,255,255,0)', 'rgba(255,255,255,1)']}
                                                style={{
                                                    position: "absolute",
                                                    width: "100%",
                                                    height: "100%",
                                                    zIndex: 1
                                                }}>
                                            </LinearGradient>
                                        }
                                        {
                                            hook.ui.informationExpanded ?
                                                <View>
                                                    {
                                                        hook.data.book?.bookId &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.bookId}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.isbn10 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.isbn10}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.isbn13 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book.book.isbn13}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.issuer?.user.name &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.issuer?.user.name}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.publisher &&
                                                        hook.data.book?.book?.publisher.name &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.publisher.name}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.releasedYear &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.releasedYear}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.size &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Kích thước:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.size}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.translator &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Dịch giả:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.translator}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Định dạng:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>Sách giấy{hook.data.book?.book?.onlyPdf && ", PDF"}{hook.data.book?.book?.onlyAudio && ", Audio"}{hook.data.book?.book?.fullPdfAndAudio && ", PDF, Audio"}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.book?.book?.page &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Số trang:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.page}</Text>
                                                        </View>
                                                    }
                                                </View>
                                                :
                                                hook.data.book.book.isbn10 && hook.data.book.book.isbn13 ?
                                                    <View>
                                                        {
                                                            hook.data.book?.bookId &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.bookId}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.isbn10 &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.isbn10}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.isbn13 &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book.book.isbn13}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.issuer?.user.name &&
                                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.issuer?.user.name}</Text>
                                                            </View>
                                                        }
                                                    </View>
                                                    :
                                                    <View>
                                                        {
                                                            hook.data.book?.bookId &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.bookId}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.isbn10 &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.isbn10}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.isbn13 &&
                                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book.book.isbn13}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.issuer?.user.name &&
                                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.issuer?.user.name}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.publisher &&
                                                            hook.data.book?.book?.publisher.name &&
                                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.publisher.name}</Text>
                                                            </View>
                                                        }
                                                        {
                                                            hook.data.book?.book?.releasedYear &&
                                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.releasedYear}</Text>
                                                            </View>
                                                        }
                                                    </View>
                                        }
                                    </View>
                                    <ShowMoreButton expanded={hook.ui.informationExpanded} onPress={() => hook.ui.setInformationExpanded(!hook.ui.informationExpanded)} />
                                </>
                                :
                                <>
                                    <View>
                                        {
                                            hook.data.book?.bookId &&
                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.bookId}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.isbn10 &&
                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.isbn10}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.isbn13 &&
                                            <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book.book.isbn13}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.issuer?.user.name &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.issuer?.user.name}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.publisher &&
                                            hook.data.book?.book?.publisher.name &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.publisher.name}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.releasedYear &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.releasedYear}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.size &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Kích thước:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.size}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.translator &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Dịch giả:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.translator}</Text>
                                            </View>
                                        }
                                        {
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Định dạng:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>Sách giấy{hook.data.book?.book?.onlyPdf && ", PDF"}{hook.data.book?.book?.onlyAudio && ", Audio"}{hook.data.book?.book?.fullPdfAndAudio && ", PDF, Audio"}</Text>
                                            </View>
                                        }
                                        {
                                            hook.data.book?.book?.page &&
                                            <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Số trang:</Text>
                                                <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.book?.book?.page}</Text>
                                            </View>
                                        }
                                    </View>
                                </>
                        }

                        <View style={{ marginBottom: 30 }} />

                        <Text variant='h6' style={{ marginBottom: 5 }}>Mô tả sản phẩm</Text>

                        <View>
                            {
                                !hook.ui.descriptionExpanded && hook.data.book && hook.data.book.description && hook.data.book?.description.length > 100 &&
                                <LinearGradient
                                    start={{ x: 0.5, y: 0 }}
                                    end={{ x: 0.5, y: 1 }}
                                    colors={['rgba(255,255,255,0)', 'rgba(255,255,255,1)']}
                                    style={{
                                        position: "absolute",
                                        width: "100%",
                                        height: "100%",
                                        zIndex: 1
                                    }}>
                                </LinearGradient>
                            }
                            <Text style={{ marginBottom: 10 }}>{hook.data.book?.description && hook.data.book?.description.length > 100 ?
                                hook.ui.descriptionExpanded ?
                                    hook.data.book.description
                                    :
                                    truncateString(hook.data.book?.description || "", 20)
                                :
                                hook.data.book?.description}</Text>
                        </View>
                        {
                            hook.data.book && hook.data.book.description && hook.data.book?.description.length > 100 &&
                            <ShowMoreButton
                                expanded={hook.ui.descriptionExpanded}
                                onPress={() => hook.ui.setDescriptionExpanded(!hook.ui.descriptionExpanded)} />
                        }
                        <View style={{ marginBottom: 20 }} />
                    </Shadow>
                    {
                        hook.data.book?.unhierarchicalBookProducts.length != 0 &&
                        <Shadow style={{
                            backgroundColor: "white",
                            borderRadius: 8,
                            padding: 10,
                            marginTop: 20
                        }}>
                            <Text variant='h5' style={{ marginBottom: 5 }}>Có thể bạn quan tâm</Text>
                            {
                                hook.data.book?.unhierarchicalBookProducts?.map(item =>
                                    item.bookProducts.length > 0 &&
                                    <TitleFlatBooks
                                        title={item.title}
                                        data={item.bookProducts as MobileBookProductViewModel[]} />)
                            }
                        </Shadow>
                    }
                </View>
            </ScrollView>
            <Button
                disabled={hook.ui.getDisabled()}
                onPress={() => addToCart(hook.data.book!, 1)}
                buttonStyle={{
                    backgroundColor: primaryColor,
                    padding: 12
                }}>{hook.ui.getButtonText()}</Button>
        </>

    )
}

export default BookDetail