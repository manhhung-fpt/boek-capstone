import { ParamListBase } from '@react-navigation/native';
import { StackScreenProps } from '@react-navigation/stack';
import moment from 'moment';
import React from 'react'
import { FlatList, View, ScrollView, Image } from 'react-native'
import { Text } from '@react-native-material/core'
import LabeledImage from '../../../components/LabeledImage/LabeledImage';
import Shadow from '../../../components/Shadow/Shadow';
import { paletteGray, primaryTint1 } from '../../../utils/color';
import { dateFormat, dateTimeFormat } from '../../../utils/format';
import useRecurringCampaignPage from './RecurringCampaign.hook'

function RecurringCampaign(props: StackScreenProps<ParamListBase>) {
  const hook = useRecurringCampaignPage(props);
  return (
    <ScrollView style={{ backgroundColor: "white", minHeight: "100%" }}>
      <View style={{ backgroundColor: "white", padding: 15 }}>
        {
          hook.data.campaign?.organizations?.map(organization =>
            <Shadow style={{
              backgroundColor: "white",
              marginBottom: 20,
              borderRadius: 8,
              padding: 10
            }}>
              <View style={{
                flexDirection: "row",
                alignItems: "center",
                padding: 5
              }}>
                <View
                  style={{
                    width: 50,
                    height: 50,
                    borderRadius: 999,
                    overflow: "hidden",
                  }}>
                  <Image source={{ uri: organization.imageUrl }} style={{ width: 50, height: 50 }} resizeMode="cover" />
                </View>
                <View style={{ paddingLeft: 20 }}>
                  <Text variant='subtitle1'>{organization.name}</Text>
                </View>
              </View>
              {
                organization.schedules?.map(schedule =>
                  <View style={{
                    marginTop: 10,
                    borderTopWidth: 1,
                    borderTopColor: paletteGray,
                    padding: 10,
                    rowGap: 5
                  }}>
                    <Text style={{ fontSize: 16, marginBottom: 10 }}><Text style={{ fontWeight: "600" }}>Địa điểm: </Text>{schedule.address}</Text>
                    <Text style={{ fontSize: 16, marginBottom: 10 }}><Text style={{ fontWeight: "600" }}>Thời gian diễn ra: </Text>{moment(schedule.startDate).format(dateTimeFormat)}</Text>
                    <Text style={{ fontSize: 16, marginBottom: 10 }}><Text style={{ fontWeight: "600" }}>Trạng thái: </Text> {schedule.statusName}</Text>
                  </View>
                )
              }
            </Shadow>
          )
        }

      </View>
      {/* <Text style={{ fontSize: 20, fontWeight: "600", marginBottom: 3, marginTop: 10 }}>Tổ chức</Text> */}

    </ScrollView>
  )
}

export default RecurringCampaign