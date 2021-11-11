import * as React from "react";
import { useState } from "react";
import { Alert, StyleSheet, TouchableOpacity, View } from "react-native";
import { Text } from '../../components/Themed';
import { pickingAPI, RootStackScreenProps } from "../../types";
import Loading from "../loading/Loading";

export default function PickingHome({ navigation, route }: RootStackScreenProps<'PickingHome'>) {

    const { user, dataObject } = route.params;

    if (!user)
        navigation.replace('Login');

    const stocks = dataObject.stocks;
    const stock = dataObject.stock;

    const [isLoading, setIsLoading] = useState(false);
    const [orderpicking, setOrderpicking] = useState(null);


    React.useEffect(() => {
        setIsLoading(true);
        fetch(`${pickingAPI}/api/orderpicking?op=${user?.Id}&sector=${stock.Code}&status=2000`)
            .then(data => {
                data.json().then(data => {
                    if (data && data.length > 0) {
                        let opick = data[0];
                        setOrderpicking(opick);
                    }
                });
            })
            .catch(err => {
                Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha ao consultar os order pickings');
            })
            .finally(() => setIsLoading(false))

    }, []);

    const getNextPicking = (opid: string) => {
        fetch(`${pickingAPI}/api/orderpicking?Id=${opid}`)
            .then(async (resp) => {
                let obj = await resp.json();
                navigation.replace('Picking', { user, dataObject: { stocks, stock, orderpicking: obj[0] } })
            })
            .catch((err) => {
                Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha ao iniciar o próximo picking');
                setIsLoading(false)
            });
    }

    const postNextPicking = (op: string) => {
        fetch(`${pickingAPI}/api/process/start`, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: op,
                Sector: stock.Code,
                Operator: user?.Id
            })
        })
            .then(async (resp) => {
                let data = await resp.json();

                if (resp.status != 200 || data != true) {
                    Alert.alert("Erro", data ?? `Não foi possivel iniciar o order picking ${op}`);
                }
                else
                    getNextPicking(op);
            })
            .catch((err) => {
                Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha ao iniciar o próximo picking');
            })
            .finally(() => setIsLoading(false))
    }

    const startNext = () => {
        setIsLoading(true);
        fetch(`${pickingAPI}/api/process/next?sector=${stock.Code}`)
            .then(async data => {
                postNextPicking((await data.text()));
            })
            .catch(err => {
                //Alert.alert("Erro", err);
                Alert.alert("Erro", err && err.error && err.error.Message ? err.error.Message : 'Falha ao inciar o próximo picking');
                setIsLoading(false)
            });
    }

    return (
        <View style={styles.container}>

            <Text style={styles.title}>{stock.Name}</Text>
            {isLoading ? <Loading /> :
                <View style={styles.btnContainer}>
                    {!orderpicking ? <TouchableOpacity
                        onPress={() => startNext()}
                        style={styles.button}>
                        <Text style={styles.buttonText}>Iniciar Nova Separação</Text>
                    </TouchableOpacity>
                        :
                        <TouchableOpacity
                            onPress={() => navigation.replace('Picking', { user, dataObject: { stocks, stock, orderpicking } })}
                            style={[styles.button, styles.bgBlue]}>
                            <Text style={styles.buttonText}>Continuar a Separação</Text>
                        </TouchableOpacity>}
                </View>}
            <View style={styles.footer}>
                <TouchableOpacity
                    onPress={() => navigation.replace('StockList', { user, dataObject: { stocks, stock } })}
                    style={[styles.buttonSmall, styles.bgBlue]}>
                    <Text style={styles.buttonText}>Voltar</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'flex-start',
    },
    title: {
        width: '100%',
        fontSize: 24,
        textAlign: 'center',
        fontWeight: 'bold',
        paddingTop: 30,
        paddingBottom: 20
    },
    btnContainer: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'flex-start',
        width: '100%',
        marginTop: 20
    },
    button: {
        padding: 10,
        paddingVertical: 30,
        marginVertical: 20,
        backgroundColor: 'green',
        borderRadius: 4,
        width: '60%'
    },

    buttonText: {
        fontSize: 20,
        color: 'white',
        width: '100%',
        textAlign: 'center'
    },
    footer: {
        width: '100%',
        paddingVertical: 20,
        paddingHorizontal: 5,
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center'
    },
    buttonSmall: {
        paddingVertical: 10,
        borderRadius: 4,
        width: '30%'
    },
    bgRed: {
        backgroundColor: 'red',
    },
    bgBlue: {
        backgroundColor: 'blue',
    },
});