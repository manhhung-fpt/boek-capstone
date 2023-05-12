import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { View, Text, TouchableOpacity, Slider, ActivityIndicator } from 'react-native';
import { Audio, AVPlaybackStatusSuccess } from 'expo-av';
import { Ionicons } from '@expo/vector-icons';
import useAsyncEffect from 'use-async-effect';
import { primaryColor, primaryTint3, primaryTint8 } from '../../utils/color';
interface AudioPlayerProps {
    audioUri: string;
}
export interface AudioPlayerRefProps {
    play(): Promise<void>;
    pause(): Promise<void>;
    stop(): Promise<void>;
}

const AudioPlayer = forwardRef<AudioPlayerRefProps, AudioPlayerProps>((props, ref) => {
    const [isPlaying, setIsPlaying] = useState<boolean>(false);
    const [sound, setSound] = useState<Audio.Sound | null>(null);
    const [duration, setDuration] = useState<number>(0);
    const [position, setPosition] = useState<number>(0);
    const [volume, setVolume] = useState<number>(1.0);
    const [isMuted, setIsMuted] = useState<boolean>(false);
    const [oldVolume, setOldVolume] = useState(1.0);
    const [loading, setLoading] = useState(false);

    const playSound = async () => {
        if (!isPlaying) {
            if (sound) {
                await sound.playAsync();
                setIsPlaying(true);
            }
        }
    };

    const pauseSound = async () => {
        if (isPlaying && sound) {
            await sound.pauseAsync();
            setIsPlaying(false);
        }
    };

    const stopSound = async () => {
        if (isPlaying && sound) {
            await sound.stopAsync();
            setIsPlaying(false);
        }
    };


    const loadSound = async () => {
        try {
            const audio = await Audio.Sound.createAsync({
                uri: props.audioUri,
            });
            audio.sound.setOnPlaybackStatusUpdate(async (playBackStatus: any) => {
                if (playBackStatus.didJustFinish) {
                    await audio.sound.stopAsync();
                    setIsPlaying(false);
                }
            });
            setSound(audio.sound);
            const playBackStatus = audio.status as AVPlaybackStatusSuccess;
            const durationInSec = Math.floor(playBackStatus.durationMillis as number / 1000);
            setDuration(durationInSec);
        }
        catch (error) {
            console.log(error);
        }

    };

    const onSliderValueChange = async (value: number) => {
        if (sound) {
            await sound.setPositionAsync(value * 1000);
            setPosition(value);
        }
    };

    const onVolumeSliderValueChange = async (value: number) => {
        if (sound) {
            await sound.setVolumeAsync(value);
            setVolume(value);
            setIsMuted(value === 0);
            setOldVolume(value);
        }
    };

    const onMutePress = async () => {
        if (sound) {
            const newVolume = isMuted ? oldVolume : 0.0;
            await sound.setVolumeAsync(newVolume);
            setVolume(newVolume);
            setIsMuted(!isMuted);
        }
    };

    const formatTime = (seconds: number) => {
        const minutes = Math.floor(seconds / 60);
        const remainingSeconds = seconds - minutes * 60;
        const formattedSeconds = remainingSeconds < 10 && remainingSeconds < 100 ? `0${remainingSeconds.toFixed(0)}` : remainingSeconds.toFixed(0);
        return `${minutes}:${formattedSeconds}`;
    };

    useImperativeHandle(ref,
        () => ({
            async play() {
                await playSound();
            },
            async pause() {
                await pauseSound();
            },
            async stop() {
                await stopSound();
            }
        }));

    useAsyncEffect(async () => {
        let interval: NodeJS.Timer;
        if (!sound) {
            setLoading(true);
            await loadSound();
            setLoading(false);
        }
        else {
            interval = setInterval(async () => {
                const status = (await sound.getStatusAsync()) as AVPlaybackStatusSuccess;
                const currentTime = status.positionMillis;
                setPosition(currentTime / 1000);
            }, 200);
        }
        return sound
            ? () => {
                sound.unloadAsync();
                clearInterval(interval);
            }
            : undefined;

    }, [sound, props.audioUri]);

    return (
        <View style={{
            flexDirection: 'row',
            borderRadius: 8,
            alignItems: 'center',
            backgroundColor: primaryTint8,
            padding: 10,
            shadowColor: "#000",
            shadowOffset: {
                width: 0,
                height: 12,
            },
            shadowOpacity: 0.58,
            shadowRadius: 16.00,
            elevation: 8
        }}>
            <TouchableOpacity style={{ width: "8%" }} onPress={isPlaying ? pauseSound : playSound}>
                {
                    loading ?
                        <ActivityIndicator />
                        :
                        <Ionicons size={25} name={isPlaying ? 'pause-circle' : 'play-circle'} style={{ backgroundColor: "transparent" }} />
                }
            </TouchableOpacity>
            <TouchableOpacity style={{ width: "8%" }} onPress={onMutePress}>
                <Ionicons
                    onPress={onMutePress}
                    name={isMuted ? 'volume-mute' : 'volume-high'}
                    size={25}
                    color="black" />
            </TouchableOpacity>
            <Slider
                style={{ width: "14%", margin: "-3%" }}
                value={volume}
                minimumValue={0}
                maximumValue={1}
                minimumTrackTintColor={primaryColor}
                maximumTrackTintColor="#000000"
                thumbTintColor={primaryTint3}
                onSlidingComplete={onVolumeSliderValueChange}
            />
            <Slider
                style={{ width: "56%" }}
                value={position}
                minimumValue={0}
                maximumValue={duration}
                minimumTrackTintColor={primaryColor}
                maximumTrackTintColor="#000000"
                thumbTintColor={primaryTint3}
                onValueChange={onSliderValueChange}
            />
            <View style={{ width: "20%", flexDirection: "row" }}>
                <Text style={{ marginHorizontal: 2 }}>{formatTime(position)}</Text>
                <Text style={{ marginHorizontal: 2 }}>/</Text>
                <Text style={{ marginHorizontal: 2 }}>{formatTime(duration)}</Text>
            </View>
        </View>
    )
});

// const AudioPlayer: React.FC<AudioPlayerProps> = ({ audioUri }) => {
//     const [isPlaying, setIsPlaying] = useState<boolean>(false);
//     const [sound, setSound] = useState<Audio.Sound | null>(null);
//     const [duration, setDuration] = useState<number>(0);
//     const [position, setPosition] = useState<number>(0);
//     const [volume, setVolume] = useState<number>(1.0);
//     const [isMuted, setIsMuted] = useState<boolean>(false);
//     const [oldVolume, setOldVolume] = useState(1.0);
//     const [loading, setLoading] = useState(false);

//     useAsyncEffect(async () => {
//         let interval: NodeJS.Timer;
//         if (!sound) {
//             setLoading(true);
//             await loadSound();
//             setLoading(false);
//         }
//         else {
//             interval = setInterval(async () => {
//                 const status = (await sound.getStatusAsync()) as AVPlaybackStatusSuccess;
//                 const currentTime = status.positionMillis;
//                 setPosition(currentTime / 1000);
//             }, 200);
//         }
//         return sound
//             ? () => {
//                 sound.unloadAsync();
//                 clearInterval(interval);
//             }
//             : undefined;

//     }, [sound]);

//     const loadSound = async () => {
//         try {
//             const audio = await Audio.Sound.createAsync({
//                 uri: audioUri,
//             });
//             audio.sound.setOnPlaybackStatusUpdate(async (playBackStatus: any) => {
//                 if (playBackStatus.didJustFinish) {
//                     await audio.sound.stopAsync();
//                     setIsPlaying(false);
//                 }
//             });
//             setSound(audio.sound);
//             const playBackStatus = audio.status as AVPlaybackStatusSuccess;
//             const durationInSec = Math.floor(playBackStatus.durationMillis as number / 1000);
//             setDuration(durationInSec);
//         }
//         catch (error) {
//             console.log(error);
//         }

//     };
//     const playSound = async () => {
//         if (!isPlaying) {
//             if (sound) {
//                 await sound.playAsync();
//                 setIsPlaying(true);
//             }
//         }
//     };

//     const pauseSound = async () => {
//         if (isPlaying && sound) {
//             await sound.pauseAsync();
//             setIsPlaying(false);
//         }
//     };

//     const onSliderValueChange = async (value: number) => {
//         if (sound) {
//             await sound.setPositionAsync(value * 1000);
//             setPosition(value);
//         }
//     };

//     const onVolumeSliderValueChange = async (value: number) => {
//         if (sound) {
//             await sound.setVolumeAsync(value);
//             setVolume(value);
//             setIsMuted(value === 0);
//             setOldVolume(value);
//         }
//     };

//     const onMutePress = async () => {
//         if (sound) {
//             const newVolume = isMuted ? oldVolume : 0.0;
//             await sound.setVolumeAsync(newVolume);
//             setVolume(newVolume);
//             setIsMuted(!isMuted);
//         }
//     };

//     const formatTime = (seconds: number) => {
//         const minutes = Math.floor(seconds / 60);
//         const remainingSeconds = seconds - minutes * 60;
//         const formattedSeconds = remainingSeconds < 10 && remainingSeconds < 100 ? `0${remainingSeconds.toFixed(0)}` : remainingSeconds.toFixed(0);
//         return `${minutes}:${formattedSeconds}`;
//     };

//     return (
//         <View style={{
//             flexDirection: 'row',
//             borderRadius: 8,
//             alignItems: 'center',
//             backgroundColor: primaryTint8,
//             padding: 5,
//             shadowColor: "#000",
//             shadowOffset: {
//                 width: 0,
//                 height: 12,
//             },
//             shadowOpacity: 0.58,
//             shadowRadius: 16.00,
//             elevation: 24
//         }}>
//             <TouchableOpacity style={{ width: "8%" }} onPress={isPlaying ? pauseSound : playSound}>
//                 {
//                     loading ?
//                         <ActivityIndicator />
//                         :
//                         <Ionicons size={25} name={isPlaying ? 'pause-circle' : 'play-circle'} style={{ backgroundColor: "transparent" }} />
//                 }
//             </TouchableOpacity>
//             <TouchableOpacity style={{ width: "8%" }} onPress={onMutePress}>
//                 <Ionicons
//                     onPress={onMutePress}
//                     name={isMuted ? 'volume-mute' : 'volume-high'}
//                     size={25}
//                     color="black" />
//             </TouchableOpacity>
//             <Slider
//                 style={{ width: "14%", margin: "-3%" }}
//                 value={volume}
//                 minimumValue={0}
//                 maximumValue={1}
//                 minimumTrackTintColor={primaryColor}
//                 maximumTrackTintColor="#000000"
//                 thumbTintColor={primaryTint3}
//                 onSlidingComplete={onVolumeSliderValueChange}
//             />
//             <Slider
//                 style={{ width: "58%" }}
//                 value={position}
//                 minimumValue={0}
//                 maximumValue={duration}
//                 minimumTrackTintColor={primaryColor}
//                 maximumTrackTintColor="#000000"
//                 thumbTintColor={primaryTint3}
//                 onValueChange={onSliderValueChange}
//             />
//             <View style={{ width: "20%", flexDirection: "row" }}>
//                 <Text style={{ marginHorizontal: 2 }}>{formatTime(position)}</Text>
//                 <Text style={{ marginHorizontal: 2 }}>/</Text>
//                 <Text style={{ marginHorizontal: 2 }}>{formatTime(duration)}</Text>
//             </View>
//         </View>
//     );
// };

export default AudioPlayer;