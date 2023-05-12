import * as Notifications from 'expo-notifications';

export default async function pushNotification<T>(title: string, body: string, data?: T) {
  await Notifications.scheduleNotificationAsync({
    content: {
      title: title,
      body: body,
      data: data as any
    },
    trigger: { seconds: 1 }
  });
}