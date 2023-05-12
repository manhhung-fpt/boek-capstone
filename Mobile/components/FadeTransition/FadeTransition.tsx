import React, { useEffect, useState } from 'react'
import { Animated, ViewProps } from 'react-native'
interface FadeTransitionProps extends ViewProps {
    duration?: number;
    initShow?: boolean;
    showed: boolean;
    onHideComplete?: () => void;
    onShowComplete?: () => void;
}
function FadeTransition(props: FadeTransitionProps) {
    const deafultDuration = 600;

    const [display, setDisplay] = useState(props.initShow || false);
    const [opacity] = useState(new Animated.Value(1));
    const animationHide = Animated.timing(opacity, {
        toValue: 0,
        useNativeDriver: true,
        duration: props.duration || deafultDuration
    });
    const animationShow = Animated.timing(opacity, {
        toValue: 1,
        useNativeDriver: true,
        duration: props.duration || deafultDuration
    });

    useEffect(() => {
        if (props.showed) {
            animationHide.stop();
            setDisplay(true);
            animationShow.start(() => props.onShowComplete && props.onShowComplete());
        }
        else {
            animationHide.start(() => { setDisplay(false); props.onHideComplete && props.onHideComplete() });
        }
    }, [props.showed]);

    return (
        <Animated.View
            {...props}
            style={Object.assign({}, props.style,
                {
                    opacity: opacity,
                    display: display ? "flex" : "none"
                }
            )}>
            {props.children}
        </Animated.View>
    )
}

export default FadeTransition