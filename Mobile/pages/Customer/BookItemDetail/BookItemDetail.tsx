import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import { Button } from '@rneui/base'
import React from 'react'
import { ScrollView, View, Image } from 'react-native';
import { Text } from "@react-native-material/core";
import LinearGradient from 'react-native-linear-gradient'
import AudioPlayer from '../../../components/AudioPlayer/AudioPlayer'
import ShowMoreButton from '../../../components/ShowMoreButton/ShowMoreButton'
import formatNumber from '../../../libs/functions/formatNumber'
import truncateString from '../../../libs/functions/truncateString'
import useRouter from '../../../libs/hook/useRouter'
import { paletteGray, primaryColor, primaryTint5 } from '../../../utils/color'
import useBookItemDetailPage from './BookItemDetail.hook'
import Shadow from '../../../components/Shadow/Shadow';

function BookItemDetail(props: StackScreenProps<ParamListBase>) {
    const hook = useBookItemDetailPage(props);
    const { push } = useRouter();
    return (
        <ScrollView style={{ backgroundColor: "white" }}>
            <View style={{
                padding: 15,
                backgroundColor: "white"
            }}>
                <Shadow style={{
                    backgroundColor: "white",
                    borderRadius: 8,
                    padding: 10
                }}>
                    <View style={{ alignItems: "center" }}>
                        <View style={{ width: "60%", height: 400, paddingTop: 20 }}>
                            <View style={{ borderRadius: 24, borderWidth: 1, borderColor: primaryTint5, overflow: "hidden" }}>
                                <Image
                                    style={{ width: "100%", height: "100%" }}
                                    source={{ uri: hook.data.bookItem?.book?.imageUrl }}
                                    resizeMode="cover"
                                />
                            </View>
                        </View>
                    </View>
                    <Text style={{ marginBottom: 10, fontSize: 20, fontWeight: "600" }}>{hook.data.bookItem?.book?.name}</Text>
                    {
                        hook.data.bookItem?.book && hook.data.bookItem.book?.genre &&
                        <View style={{
                            borderWidth: 1,
                            borderColor: paletteGray,
                            padding: 7,
                            width: "40%",
                            borderRadius: 8,
                            alignItems: "center",
                            justifyContent: "center",
                            marginBottom: 10
                        }}>
                            <Text style={{ fontSize: 15, color: primaryColor }}>{hook.data.bookItem.book?.genre.name}</Text>
                        </View>
                    }
                    <View style={{
                        display: hook.ui.showPdfOrAudio() ? "flex" : "none",
                        height: 70,
                        flexDirection: "row",
                        justifyContent: "center"
                    }}>
                        {
                            hook.data.bookItem?.withPdf &&
                            <View style={{ padding: 5, width: "50%" }}>
                                <Button
                                    onPress={() => push("PdfShower", { title: "Pdf mẫu", url: hook.data.bookItem?.book?.pdfTrialUrl })}
                                    buttonStyle={{ backgroundColor: primaryColor }}>PDF mẫu - {formatNumber(hook.data.bookItem.pdfExtraPrice)}đ</Button>
                            </View>
                        }
                        {
                            hook.data.bookItem?.withAudio &&
                            <View style={{ padding: 5, width: "50%" }}>
                                <Button
                                    onPress={() => hook.ui.setTrialAudioVisible(!hook.ui.trialAudioVisible)}
                                    buttonStyle={{ backgroundColor: primaryColor }}>Audio mẫu - {formatNumber(hook.data.bookItem.audioExtraPrice)}đ</Button>
                            </View>
                        }
                    </View>
                    {
                        hook.ui.trialAudioVisible &&
                        <View style={{
                            alignItems: "center",
                            width: "100%",
                            marginBottom: 30,
                            display: hook.ui.trialAudioVisible ? "flex" : "none"
                        }}>
                            <View style={{ width: "95%" }}>
                                <AudioPlayer ref={hook.ref.audioPlayerRef} audioUri={hook.data.bookItem?.book?.audioTrialUrl as string} />
                            </View>
                        </View>
                    }
                </Shadow>
                <Shadow style={{
                    backgroundColor: "white",
                    borderRadius: 8,
                    padding: 10,
                    marginTop : 20
                }}>
                    {
                        hook.data.bookItem?.book?.bookAuthors && hook.data.bookItem.book.bookAuthors.length > 0 &&
                        <View style={{ marginBottom: 20, flexDirection: "row" }}>
                            <View style={{ width: "20%", justifyContent: 'center' }}>
                                <View style={{
                                    borderRadius: 999,
                                    overflow: "hidden",
                                    width: 60,
                                    height: 60
                                }}>
                                    <Image
                                        source={{ uri: hook.data.bookItem.book.bookAuthors.at(0) && hook.data.bookItem.book.bookAuthors[0].author.imageUrl }}
                                        resizeMode="cover"
                                        style={{ width: 60, height: 60 }} />
                                </View>

                            </View>
                            <View style={{ width: "80%", justifyContent: "center" }}>
                                <Text style={{ fontSize: 18, fontWeight: "600" }}>Tác giả</Text>
                                <Text style={{ fontSize: 16 }}>{hook.data.bookItem?.book?.bookAuthors.map(item => item.author.name).join(", ")}</Text>
                            </View>
                        </View>
                    }
                    <Text style={{ marginBottom: 30, fontSize: 22, fontWeight: "600" }}>Thông tin chi tiết</Text>


                    {
                        hook.data.bookItem?.book ?
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
                                                    hook.data.bookItem?.bookId &&
                                                    <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.bookId}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.isbn10 &&
                                                    <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.isbn10}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.isbn13 &&
                                                    <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem.book.isbn13}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.issuer?.user.name &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book.issuer?.user.name}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.publisher &&
                                                    hook.data.bookItem?.book?.publisher.name &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.publisher.name}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.releasedYear &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.releasedYear}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.size &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Kích thước:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.size}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.translator &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Dịch giả:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.translator}</Text>
                                                    </View>
                                                }
                                                {
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Định dạng:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>Sách giấy{hook.data.bookItem?.book?.onlyPdf && ", PDF"}{hook.data.bookItem?.book?.onlyAudio && ", Audio"}{hook.data.bookItem?.book?.fullPdfAndAudio && ", PDF, Audio"}</Text>
                                                    </View>
                                                }
                                                {
                                                    hook.data.bookItem?.book?.page &&
                                                    <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                        <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Số trang:</Text>
                                                        <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.page}</Text>
                                                    </View>
                                                }
                                            </View>
                                            :
                                            hook.data.bookItem?.book?.isbn10 && hook.data.bookItem.book.isbn13 ?
                                                <View>
                                                    {
                                                        hook.data.bookItem?.bookId &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.bookId}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.isbn10 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.isbn10}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.isbn13 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem.book.isbn13}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book.issuer?.user.name &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book.issuer?.user.name}</Text>
                                                        </View>
                                                    }
                                                </View>
                                                :
                                                <View>
                                                    {
                                                        hook.data.bookItem?.bookId &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.bookId}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.isbn10 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.isbn10}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.isbn13 &&
                                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem.book.isbn13}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.issuer.user.name &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book.issuer?.user.name}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.publisher &&
                                                        hook.data.bookItem?.book?.publisher.name &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.publisher.name}</Text>
                                                        </View>
                                                    }
                                                    {
                                                        hook.data.bookItem?.book?.releasedYear &&
                                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.releasedYear}</Text>
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
                                        hook.data.bookItem?.bookId &&
                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Mã sách:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.bookId}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.isbn10 &&
                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN10:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.isbn10}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.isbn13 &&
                                        <View style={{ flexDirection: "row", marginBottom: 25 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>ISBN13:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem.book.isbn13}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.issuer?.user.name &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NPH:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book.issuer?.user.name}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.publisher &&
                                        hook.data.bookItem?.book?.publisher.name &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>NXB:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.publisher.name}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.releasedYear &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Năm xuất bản:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.releasedYear}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.size &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Kích thước:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.size}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.translator &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Dịch giả:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.translator}</Text>
                                        </View>
                                    }
                                    {
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Định dạng:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>Sách giấy{hook.data.bookItem?.book?.onlyPdf && ", PDF"}{hook.data.bookItem?.book?.onlyAudio && ", Audio"}{hook.data.bookItem?.book?.fullPdfAndAudio && ", PDF, Audio"}</Text>
                                        </View>
                                    }
                                    {
                                        hook.data.bookItem?.book?.page &&
                                        <View style={{ flexDirection: "row", marginBottom: 20 }}>
                                            <Text style={{ fontSize: 16, fontWeight: "600", width: "30%" }}>Số trang:</Text>
                                            <Text style={{ fontSize: 16, width: "75%" }}>{hook.data.bookItem?.book?.page}</Text>
                                        </View>
                                    }
                                </View>
                            </>
                    }
                    <View style={{ marginBottom: 30 }} />

                    <Text style={{ marginBottom: 5, fontSize: 22, fontWeight: "600" }}>Mô tả sản phẩm</Text>

                    <View>
                        {
                            !hook.ui.descriptionExpanded && hook.data.bookItem && hook.data.bookItem.book?.description && hook.data.bookItem.book?.description.length > 100 &&
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
                        <Text style={{ marginBottom: 10 }}>{hook.data.bookItem?.book?.description && hook.data.bookItem.book?.description.length > 100 ?
                            hook.ui.descriptionExpanded ?
                                hook.data.bookItem.book.description
                                :
                                truncateString(hook.data.bookItem.book.description || "", 20)
                            :
                            hook.data.bookItem?.book?.description}</Text>
                    </View>
                    {
                        hook.data.bookItem && hook.data.bookItem.book?.description && hook.data.bookItem.book?.description.length > 100 &&
                        <ShowMoreButton
                            expanded={hook.ui.descriptionExpanded}
                            onPress={() => hook.ui.setDescriptionExpanded(!hook.ui.descriptionExpanded)} />
                    }
                </Shadow>
            </View>

            <View style={{ padding: 10, paddingTop: 20 }}>

            </View>
        </ScrollView>
    )
}

export default BookItemDetail