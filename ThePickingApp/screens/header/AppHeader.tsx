import * as React from "react";
import { StyleSheet, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { RootStackScreenProps } from "../../types";

export default function AppHeader({ navigation }: any) {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>The Picking App</Text>

            <TouchableOpacity onPress={() => navigation.replace('Login')}>
                <Text style={styles.title}>Inicio</Text>
            </TouchableOpacity>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        height: 40,
        marginTop: 30,
        paddingLeft: 5,
        paddingRight: 5,
    },
    title: {
        fontSize: 20
    }
});