import { Chip, HStack, Stack } from '@react-native-material/core';
import React, { PropsWithChildren, ReactNode, useEffect, useState } from 'react'
import { View, ViewProps, Text, TouchableOpacity, FlatList } from 'react-native';
import useIsFirstRender from '../../libs/hook/useIsFirstRender';
import { paletteGrayTint4, paletteGrayTint6, paletteGrayTint8, primaryColor, primaryTint3, primaryTint4, primaryTint6, primaryTint7, primaryTint8, primaryTint9 } from '../../utils/color';
import FadeTransition from '../FadeTransition/FadeTransition';
import SelectedChip from '../SeletedChip/SelectedChip';
interface PreLoadTabViewProps {
    childrens: ReactNode[];
    titles: string[];
}
function PreLoadTabView(props: PreLoadTabViewProps) {
    const [index, setIndex] = useState<number>(0);
    const [showed, setShowed] = useState(true);
    const [inputIndex, setInputIndex] = useState(0);
    const onIndexChange = (index: number) => {
        setInputIndex(index);
        setShowed(false);
    }
    return (
        <View>
            <HStack
                spacing={6}
                style={{ marginBottom: 15 }}>
                {
                    props.titles.map((item, index) =>
                        <View>
                            <SelectedChip
                                unSelectedBackgroundColor={paletteGrayTint4}
                                selectedBackgroundColor={primaryColor}
                                onPress={() => onIndexChange(index)}
                                label={item}
                                selected={inputIndex == index} />
                        </View>
                    )
                }
            </HStack>

            <FadeTransition
                showed={showed}
                duration={300}
                onHideComplete={() => { setIndex(inputIndex); setShowed(true) }}>
                {
                    props.childrens[index]
                }
            </FadeTransition>

        </View>
    )
}

export default PreLoadTabView