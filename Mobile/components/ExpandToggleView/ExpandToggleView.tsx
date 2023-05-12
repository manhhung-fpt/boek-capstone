import React, { PropsWithChildren, useEffect, useState } from 'react'
import { View, Image, TouchableOpacity } from 'react-native'
import { Text } from "@react-native-material/core";
import useBoolean from '../../libs/hook/useBoolean'
import { primaryTint4 } from '../../utils/color';
import expandMoreBlack from "../../assets/icons/expand-more-black.png";
import expandLessBlack from "../../assets/icons/expand-less-black.png";
import useAsyncEffect from 'use-async-effect';
interface ExpandToggleViewProps extends PropsWithChildren {
    label: string;
    onExpand?: () => void;
    onCollapse?: () => void;
    initExpanded?: boolean;
}
function ExpandToggleView(props: ExpandToggleViewProps) {
    const [expanded, toggleExpanded] = useBoolean(props.initExpanded);
    useAsyncEffect(() => {
        if (expanded) {
            props.onExpand && props.onExpand();
        }
        else {
            props.onCollapse && props.onCollapse();
        }
    }, [expanded]);
    return (
        <View
            style={{
                borderColor: primaryTint4,
                backgroundColor: "white",
                //borderBottomWidth: 1
            }}>
            <TouchableOpacity
                onPress={() => toggleExpanded()}
                style={{

                    padding: 15,
                    flexDirection: "row"
                }}>
                <View style={{ width: "85%" }}>
                    <Text
                        style={{
                            fontSize: 16,
                        }}>
                        {props.label}
                    </Text>
                </View>
                <View style={{ width: "15%", alignItems: "center", justifyContent: "center" }}>
                    <Image source={expanded ? expandLessBlack : expandMoreBlack} style={{ height: 25 }} resizeMode="contain" />
                </View>
            </TouchableOpacity>
            {
                expanded &&
                props.children
            }
        </View>
    )
}

export default ExpandToggleView