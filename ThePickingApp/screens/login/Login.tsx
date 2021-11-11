import * as React from "react";
import { useState } from "react";
import { Alert, StyleSheet, TextInput, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { adminAPI, AllAppData, RootStackScreenProps } from "../../types";
import Loading from "../loading/Loading";


export default function RootPage({ navigation }: RootStackScreenProps<'Login'>) {

    const [loginIsLoading, setloginIsLoading] = useState(false);
    const [pinInput, setpinInput] = useState("");

    const aad: AllAppData = {};

    const login_execLogin = async (pin: string) => {
        try {
            setloginIsLoading(true);
            let response = await fetch(`${adminAPI}/api/user?username=${pin}`);
            let user = await response.json();
            if (!user) {
                Alert.alert("Usuario não encontrado", "Usuario não encontrado");
            } else {
                aad.user = user;
                navigation.replace('StockList', aad);
            }
        } catch (err: any) {
            console.log(JSON.stringify(err))
            Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha no login');
            setloginIsLoading(false);
        }
    };

    return (
        <View style={styles.container}>

            <Text style={styles.title}>The Picking App</Text>

            {!loginIsLoading ?
                <View style={styles.forms}>
                    <Text style={styles.label}>PIN</Text>
                    <TextInput style={styles.input}
                        onChangeText={val => setpinInput(val)}></TextInput>

                    <TouchableOpacity onPress={() => login_execLogin(pinInput)} style={styles.button}>
                        <Text style={styles.buttonText}>Login</Text>
                    </TouchableOpacity>
                </View>
                : <Loading></Loading>}

        </View>
    );
}


const styles = StyleSheet.create({
    loading: {
        fontSize: 18,
        width: '100%',
        textAlign: 'center'
    },
    container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center'
    },
    title: {
        fontSize: 30,
        fontWeight: 'bold',
        width: '80%',
        textAlign: 'center',
        marginBottom: 65,
    },
    button: {
        padding: 10,
        paddingVertical: 10,
        marginVertical: 20,
        backgroundColor: 'green',
        borderRadius: 4,
        width: '60%'
    },
    input: {
        fontSize: 18,
        width: '60%',
        borderWidth: 1,
        borderColor: 'black',
        backgroundColor: 'white',
        paddingLeft: 5
    },
    label: {
        fontSize: 16,
        width: '60%'
    },
    buttonText: {
        fontSize: 20,
        color: 'white',
        width: '100%',
        textAlign: 'center'
    },
    forms: {
        width: '100%',
        alignItems: 'center',
        justifyContent: 'center'
    }
});