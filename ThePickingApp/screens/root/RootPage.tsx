import * as React from "react";
import { StyleSheet, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { RootStackScreenProps } from "../../types";

export default function RootPage({ navigation }: RootStackScreenProps<'Root'>) {
    return (
        <View style={styles.container}>
            <TouchableOpacity onPress={() => navigation.replace('Login')} style={styles.button}>
                <Text style={styles.buttonText}>PIN</Text>
            </TouchableOpacity>

            {/* <TouchableOpacity onPress={() => navigation.replace('Root')} style={styles.button}>
                <Text style={styles.buttonText}>Estações</Text>
            </TouchableOpacity> */}
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
    },
    title: {
        fontSize: 30,
        fontWeight: 'bold',
    },
    button: {
        padding: 10,
        paddingVertical: 50,
        marginVertical: 20,
        backgroundColor: 'black',
        borderRadius: 4,
        width: '60%'
    },
    buttonText: {
        fontSize: 30,
        color: 'white',
        width: '100%',
        textAlign: 'center'
    },
});